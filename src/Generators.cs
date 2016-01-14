using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbWrap;
using System.Text.RegularExpressions;
using System.Transactions;

namespace DbWrap {
	namespace Generate {

		public class StoredProcInfo {

			private string m_sNameProc;
			private SqlConnection m_conn;

			public StoredProcInfo(SqlConnection conn, string sNameProc) {
				m_sNameProc = sNameProc;
				m_conn = conn;
			}

			public string ProcName { get { return m_sNameProc; } }

			public string ClassName {
				get {
					int nPosDot = m_sNameProc.IndexOf(".");
					return m_sNameProc.Substring(nPosDot + 1);
				}
			}

			public string SchemaName {
				get {
					int nPosDot = m_sNameProc.IndexOf(".");
					return nPosDot < 1 ? "dbo" : m_sNameProc.Substring(0, nPosDot);
				}
			}

			public class ParamInfo {
				private SqlParameter m_param;
				public ParamInfo(SqlParameter param) {
					m_param = param;
				}
				public string TypeName {
					get {
						switch (m_param.SqlDbType) {
							case SqlDbType.Int: return "int";
							case SqlDbType.BigInt: return "long";
							case SqlDbType.VarChar:
							case SqlDbType.NVarChar: return "string";
							case SqlDbType.Bit: return "bool";
							case SqlDbType.UniqueIdentifier: return "Guid";
							//case SqlDbType.Xml: return "SqlXml";
							default: return "object";
						}
					}
				}
				public SqlParameter param {
					get { return m_param; }
				}
				public string SqlTypeName {
					get { return m_param.SqlDbType.ToString(); }
				}
				public string Identifier {
					get {
						return Name.Replace("@", "");
					}
				}
				public string Name {
					get { return m_param.ParameterName; }
				}
			}

			private List<ParamInfo> m_rgParam;
			public List<ParamInfo> rgParam {
				get {
					if (m_rgParam == null) {
						m_rgParam = ListParameters(m_sNameProc);
					}
					return m_rgParam;
				}
			}

			private DataTableCollection m_rgResultTables;
			public int ResultsetCount {
				get {
					if (m_rgResultTables == null) {
						m_rgResultTables = ListResultFields();
					}
					return m_rgResultTables.Count;
				}
			}

			private DataColumnCollection getColums(int ix) {
				return ResultsetCount > ix ? m_rgResultTables[ix].Columns : null;
			}

			public class ColumnInfo {
				DataColumn m_col;
				private static Regex m_re = new Regex(@"\W");
				public ColumnInfo(DataColumn col) {
					m_col = col;
				}
				public string Identifier { get { return m_re.Replace(m_col.ColumnName, "_"); } }
				public string FieldType { get { return String.Format(m_col.DataType.Name == "String" ? "{0}" : "Nullable<{0}>", m_col.DataType.Name); } }
				public string Name { get { return m_col.ColumnName; } }
				public DataColumn col { get { return m_col; } }
				public string GetMethodName {
					get {
						return "Get" + m_col.DataType.Name;
					}
				}
			}

			public IEnumerable<ColumnInfo> Columns(int ixResultset) {
				foreach (DataColumn col in getColums(ixResultset)) {
					yield return new ColumnInfo(col);
				}
			}

			public IEnumerable<IEnumerable<ColumnInfo>> Resultsets() {
				for (int ixResultset = 0; ixResultset < ResultsetCount; ixResultset++) {
					yield return Columns(ixResultset);
				}
			}

			private List<ParamInfo> ListParameters(string sProcName) {
				using (var cmd = new SqlCommand(sProcName, m_conn)) {
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					SqlCommandBuilder.DeriveParameters(cmd);
					var rgRes = new List<ParamInfo>();
					foreach (SqlParameter oParam in cmd.Parameters) {
						rgRes.Add(new ParamInfo(oParam));
					}
					return rgRes;
				}
			}

			private DataTableCollection ListResultFields() {
				string sSql = "set fmtonly on;\n set ansi_warnings off;\n set arithabort off;\n set arithignore on;\n";
				sSql += "exec " + m_sNameProc;
				bool fFirst = true;
				foreach (var sp in rgParam) {
					if (sp.param.Direction != ParameterDirection.ReturnValue) {
						sSql += fFirst ? "\n" : ",";
						sSql += sp.Name + "=null\n";
						fFirst = false;
					}
				}
				if (fFirst) {
					sSql += "\n";
				}
				sSql += "set fmtonly off;\n";
				var ds = new DataSet();
				//try {
					using (var tr = new TransactionScope()) {
						using (var cmd = new SqlCommand(sSql, m_conn)) {
							cmd.CommandType = CommandType.Text;
							using (var da = new SqlDataAdapter(cmd)) {
								da.FillSchema(ds, SchemaType.Source);
							}
						}
					}
				//} catch (Exception) {
				//}
				return ds.Tables;
			}

		}

		partial class StoredProcTemplate {
			public StoredProcTemplate(StoredProcInfo _sp) {
				sp = _sp;
			}

			public StoredProcInfo sp { get; set; }
		}

		public class StoredProc {
			public static string Generate(string connectionString, string storedProcName) {
				using (var conn = new SqlConnection(connectionString)) {
					conn.Open();
					var t = new DbWrap.Generate.StoredProcTemplate(new DbWrap.Generate.StoredProcInfo(conn, storedProcName));
					return t.TransformText();
				}
			}
		}
	}
}
