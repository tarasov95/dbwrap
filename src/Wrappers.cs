using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace DbWrap {

	public class WrapType<T> {

		public static readonly WrapType<T> NullValue = new WrapType<T>(DBNull.Value);
		public static readonly WrapType<T> DefaultValue = null;

		T m_val = default(T);
		bool m_fNull = true;

		public WrapType() : this(DBNull.Value) { }

		public WrapType(WrapType<T> src) {
			if (src == null) {
				m_fNull = true;
			} else {
				m_val = src.m_val;
				m_fNull = src.m_fNull;
			}
		}

		public WrapType(T val) {
			m_val = val;
			m_fNull = (val==null || val is DBNull);
		}

		public WrapType(DBNull val) {
			m_fNull = true;
		}

		public WrapType(object val) {
			m_fNull = (val == null || val is DBNull);
			m_val = m_fNull ? default(T) : (T)val;
		}

		public bool IsNull {
			get { return m_fNull; }
			set { m_fNull = value; }//to support SOAP serialization
		}

		public static implicit operator WrapType<T>(T i) {
			return new WrapType<T>(i);
		}

		public static implicit operator T(WrapType<T> w) {
			return w.Value;
		}

		public static implicit operator WrapType<T>(System.DBNull _param) {
			return new WrapType<T>(_param);
		}

		public T Value { get { return m_val; } set { m_val = value; } }
		public object ObjectValue {
			get {
				return IsNull ? (object)DBNull.Value : (object)Value;
			}
		}

	}

	public static class WrapUtils {
		public static WrapType<Guid> GuidFromString(String sVal) {
			WrapType<Guid> res = DBNull.Value;
			if (!String.IsNullOrEmpty(sVal)) {
				res = Guid.Parse(sVal);
			}
			return res;
		}
		public static string ConnStr4Net(string sAnyConnStr)
		{
			return new Regex("(Provider|DataTypeCompatibility)=[^;]+;", RegexOptions.IgnoreCase).Replace(new Regex("'SSPI'", RegexOptions.IgnoreCase).Replace(sAnyConnStr, "SSPI"), "");
		}
	}

	public class SqlParam {
		public static SqlParameter New(string sName, SqlDbType dbType, int size, ParameterDirection dir) {
			var param = new SqlParameter(sName, dbType, size);
			param.Direction = dir;
			return param;
		}
	}

	public abstract class StoredProc : IDisposable {

		SqlCommand m_cmd = null;
		SqlConnection m_conn;
		String m_sName;
		protected StoredProc(SqlConnection conn, string sProcName) {
			m_conn = conn;
			m_sName = sProcName;
		}

		public SqlCommand cmd {
			get {
				if (m_cmd == null) {
					m_cmd = new SqlCommand(m_sName, m_conn);
					m_cmd.CommandType = System.Data.CommandType.StoredProcedure;
					AllocateParameters(m_cmd);
				}
				return m_cmd;
			}
		}

		public SqlTransaction trans {
			get { return cmd.Transaction; }
			set { cmd.Transaction = value; }
		}

		abstract protected void AllocateParameters(SqlCommand cmd);

		public void Dispose() {
			if (m_cmd != null) {
				m_cmd.Dispose();
				m_cmd = null;
			}
			m_conn = null;
		}

		//public delegate void TransactionBody(SqlCommand self);
		//public void ExecInTrans(TransactionBody fnBody=null) {
		//	cmd.Connection.RunInTrans((trans) => {
		//		cmd.Transaction = trans;
		//		if (fnBody != null) {
		//			fnBody(cmd);
		//		} else {
		//			cmd.ExecuteNonQuery();
		//		}
		//	});
		//}
	}

	public static class WrapAdhoc {
		public delegate void TransactionBody(SqlTransaction trans);
		public static void WrapTrans(this SqlConnection conn, TransactionBody fnBody) {
			WrapTrans(conn, System.Data.IsolationLevel.ReadCommitted, fnBody);
		}
		public static void WrapTrans(this SqlConnection conn, System.Data.IsolationLevel iso, TransactionBody fnBody) {
			int cMaxTry = 5;
			while (cMaxTry > 0) {
				cMaxTry--;
				using (var trans = conn.BeginTransaction(iso)) {
					try {
						fnBody(trans);
						trans.Commit();
						return;
					} catch (Exception e) {
						try {
							trans.Rollback();
							HandleTransError(e);
						} catch (Exception re) {
							throw new Exception(re.Message, e);
						}
					}
				}
			}
		}

		public delegate void ScopeBody(TransactionScope tr);
		public static void WrapTransScope(ScopeBody fnBody) {
			int cMaxTry = 5;
			while (cMaxTry > 0) {
				cMaxTry--;
				try {
					using (var tr = new TransactionScope()) {
						fnBody(tr);
						tr.Complete();
					}
				} catch (Exception e) {
					HandleTransError(e);
				}
			}
		}

		static public SqlParameter SqlOutParameter(string sName, SqlDbType type) {
			var param = new SqlParameter(sName, type);
			param.Direction = ParameterDirection.Output;
			return param;
		}

		static public SqlParameter SqlOutParameter(string sName, SqlDbType type, int size) {
			var param = SqlOutParameter(sName, type);
			param.Size = size;
			return param;
		}

		static public SqlParameter SqlInParameter(string sName, SqlDbType type, object value) {
			var param = new SqlParameter(sName, type);
			if (value != null) {
				param.Value = value;
			} else {
				param.Value = DBNull.Value;
			}
			return param;
		}

		static public SqlParameter SqlInParameter(string sName, SqlDbType type, int size, object value) {
			var param = SqlInParameter(sName, type, value);
			param.Size = size;
			return param;
		}

		static public SqlCommand WrapCommand(this SqlConnection conn, string sSql, CommandType type, params SqlParameter[] rgParam) {
			var cmd = new SqlCommand(sSql);
			cmd.CommandType = type;
			int cParam = rgParam.Length;
			if (cmd.CommandType == CommandType.StoredProcedure) {
				cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
			}
			for (int ix = 0; ix < cParam; ix++) {
				cmd.Parameters.Add(rgParam[ix]);
			}
			cmd.Connection = conn;
			return cmd;
		}

		static public SqlCommand WrapStoredProcedure(this SqlConnection conn, string sName, params SqlParameter[] rgParam) {
			return conn.WrapCommand(sName, CommandType.StoredProcedure, rgParam);
		}

		static public SqlCommand WrapTextQuery(this SqlConnection conn, string sQuery, params SqlParameter[] rgParam) {
			return conn.WrapCommand(sQuery, CommandType.Text, rgParam);
		}

		static public Dictionary<string, object> WrapResultRow(this SqlDataReader rs) {
			Dictionary<string, object> mapRes = new Dictionary<string, object>();
			var cField = rs.FieldCount;
			for (int ix = 0; ix < cField; ix++) {
				mapRes[rs.GetName(ix)] = rs.GetValue(ix);
			}
			return mapRes;
		}

		static public IEnumerable<Dictionary<string, object>> WrapResultItems(this SqlDataReader rs) {
			while (rs.Read()) {
				yield return rs.WrapResultRow();
			}
		}

		static public Dictionary<string, object> WrapResultRow(this SqlCommand cmd) {
			using (var rs = cmd.ExecuteReader()) {
				if (rs.Read()) {
					return rs.WrapResultRow();
				} else {
					return null;
				}
			}
		}

		static public List<Dictionary<string, object>> WrapResultList(this SqlCommand cmd) {
			using (var rs = cmd.ExecuteReader()) {
				return rs.WrapResultItems().ToList();
			}
		}


		static private bool IsDeadlock(int nCode, string sMessage) {
			return nCode == 1205 || nCode == 71205 || (sMessage ?? "").IndexOf("{1205}") > -1;
		}

		static private void HandleTransError(Exception e) {
			var fRerun = false;
			if (e is SqlException) {
				var oEx = (e as SqlException);
				if (IsDeadlock(oEx.Number, oEx.Message)) {
					fRerun = true;
				} else {
					var rgError = oEx.Errors;
					foreach (SqlError oErr in rgError) {
						if (IsDeadlock(oErr.Number, oErr.Message)) {
							fRerun = true;
							break;
						}
					}
				}
			}
			if (!fRerun) {
				throw e;
			}
		}

	}
}
