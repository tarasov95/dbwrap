﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace DbWrap.Generate
{
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class StoredProcTemplate : StoredProcTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\nnamespace ");
            
            #line 3 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sp.SchemaName));
            
            #line default
            #line hidden
            this.Write(" {\r\n\r\n\tusing System;\r\n\tusing System.Data;\r\n\tusing System.Data.SqlClient;\r\n\tusing " +
                    "System.Data.SqlTypes;\r\n\tusing System.Collections.Generic;\r\n\tusing System.Linq;\r\n" +
                    "\r\n\tpublic class ");
            
            #line 12 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sp.ClassName));
            
            #line default
            #line hidden
            this.Write(": DbWrap.StoredProc {\r\n\t\tpublic class Parameter {\r\n");
            
            #line 14 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			foreach (var pa in sp.rgParam) {
            
            #line default
            #line hidden
            this.Write("\t\t\tpublic DbWrap.WrapType<");
            
            #line 15 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.TypeName));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 15 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" = null;//");
            
            #line 15 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.SqlTypeName));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 16 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}//foreach
            
            #line default
            #line hidden
            this.Write("\t\t\tpublic Parameter() {}\r\n\t\t\tpublic Parameter(DBNull val) {\r\n");
            
            #line 19 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			foreach (var pa in sp.rgParam) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t");
            
            #line 20 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" = DBNull.Value;\r\n");
            
            #line 21 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}//foreach
            
            #line default
            #line hidden
            this.Write("\t\t\t}\r\n\t\t}\r\n\r\n");
            
            #line 25 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
		
		int ixResultset=0; 
		foreach (var rs in sp.Resultsets()) {
            
            #line default
            #line hidden
            this.Write("\t\t\tpublic class ResultWrap");
            
            #line 28 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write(": IDisposable {\r\n");
            
            #line 29 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			foreach (var col in rs) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tpublic ");
            
            #line 30 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.FieldType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 30 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Identifier));
            
            #line default
            #line hidden
            this.Write(" {\r\n\t\t\t\t\t\tget {\r\n\t\t\t\t\t\t\tvar ix = rs.GetOrdinal(\"");
            
            #line 32 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Name));
            
            #line default
            #line hidden
            this.Write("\");\r\n\t\t\t\t\t\t\tif(rs.IsDBNull(ix)) {\r\n\t\t\t\t\t\t\t\treturn null;\r\n\t\t\t\t\t\t\t} else {\r\n\t\t\t\t\t\t\t" +
                    "\treturn rs.");
            
            #line 36 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.GetMethodName));
            
            #line default
            #line hidden
            this.Write("(ix);\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t}//");
            
            #line 39 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Name));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 40 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}
            
            #line default
            #line hidden
            this.Write("\t\t\t\tprivate SqlDataReader rs;\r\n\t\t\t\tpublic ResultWrap");
            
            #line 42 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("(SqlDataReader _rs) {\r\n\t\t\t\t\trs = _rs;\r\n\t\t\t\t}\r\n\t\t\t\tpublic void Dispose() {\r\n\t\t\t\t\ti" +
                    "f(rs!=null) {\r\n\t\t\t\t\t\trs.Dispose();\r\n\t\t\t\t\t\trs = null;\r\n\t\t\t\t\t}\r\n\t\t\t\t}\r\n\t\t\t\tpublic " +
                    "bool Read() {\r\n\t\t\t\t\treturn rs.Read();\r\n\t\t\t\t}\r\n\t\t\t\tpublic IEnumerable<ResultCache" +
                    "");
            
            #line 54 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("> Items() {\r\n\t\t\t\t\t\twhile (Read()) {\r\n\t\t\t\t\t\t\tyield return new ResultCache");
            
            #line 56 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("(this);\r\n\t\t\t\t\t\t}\r\n\t\t\t\t}\r\n\r\n");
            
            #line 60 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
				if(ixResultset<sp.ResultsetCount-1) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\tpublic ResultWrap");
            
            #line 61 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((ixResultset+1)));
            
            #line default
            #line hidden
            this.Write(" NextResult() {\r\n\t\t\t\t\treturn new ResultWrap");
            
            #line 62 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((ixResultset+1)));
            
            #line default
            #line hidden
            this.Write("(rs.NextResult());\r\n\t\t\t\t}\r\n");
            
            #line 64 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
				}
            
            #line default
            #line hidden
            this.Write("\t\t\t}\r\n\t\t\t\r\n\t\t\tpublic class ResultCache");
            
            #line 67 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write(" {\r\n");
            
            #line 68 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
				foreach (var col in rs) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\tpublic ");
            
            #line 69 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.FieldType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 69 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Identifier));
            
            #line default
            #line hidden
            this.Write(";//");
            
            #line 69 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Name));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 70 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
				}
            
            #line default
            #line hidden
            this.Write("\t\t\t\tpublic ResultCache");
            
            #line 71 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("() {}\r\n\t\t\t\tpublic ResultCache");
            
            #line 72 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("(ResultWrap");
            
            #line 72 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write(" w) {\r\n");
            
            #line 73 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
					foreach (var col in rs) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t");
            
            #line 74 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Identifier));
            
            #line default
            #line hidden
            this.Write(" = w.");
            
            #line 74 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(col.Identifier));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 75 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
					}
            
            #line default
            #line hidden
            this.Write("\t\t\t\t}\r\n\t\t\t}\r\n");
            
            #line 78 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			if(ixResultset==0) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\tpublic ResultWrap");
            
            #line 79 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write(" ExecuteReader(Parameter param = null) {\r\n\t\t\t\t\tParam2Command(param);\r\n\t\t\t\t\treturn" +
                    " new ResultWrap");
            
            #line 81 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("(cmd.ExecuteReader());\r\n\t\t\t\t}\r\n\t\t\t\tpublic ResultCache");
            
            #line 83 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write(" QueryRow(Parameter param = null) {\r\n\t\t\t\t\tusing (var rs = ExecuteReader(param)) {" +
                    "\r\n\t\t\t\t\t\treturn rs.Read()?new ResultCache");
            
            #line 85 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("(rs):null;\r\n\t\t\t\t\t}\r\n\t\t\t\t}\r\n\t\t\t\tpublic List<ResultCache");
            
            #line 88 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write("> ListRows(Parameter param=null) {\r\n\t\t\t\t\tusing(var rs = ExecuteReader(param)) {\r\n" +
                    "\t\t\t\t\t\treturn rs.Items().ToList<ResultCache");
            
            #line 90 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ixResultset));
            
            #line default
            #line hidden
            this.Write(">();\r\n\t\t\t\t\t}\r\n\t\t\t\t}\r\n");
            
            #line 93 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}//if
            
            #line default
            #line hidden
            
            #line 94 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"

			ixResultset++;
		}//foreach

            
            #line default
            #line hidden
            this.Write("\r\n\t\tpublic ");
            
            #line 99 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sp.ClassName));
            
            #line default
            #line hidden
            this.Write("(SqlConnection conn, Parameter param = null):base(conn, \"");
            
            #line 99 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sp.ProcName));
            
            #line default
            #line hidden
            this.Write("\") {\r\n\t\t\tif(param!=null) {\r\n\t\t\t\tParam2Command(param);\r\n\t\t\t}\r\n\t\t}\r\n\r\n\t\toverride pr" +
                    "otected void AllocateParameters(SqlCommand cmd) {\r\n");
            
            #line 106 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			foreach (var pa in sp.rgParam) {
            
            #line default
            #line hidden
            this.Write("\t\t\tcmd.Parameters.Add(DbWrap.SqlParam.New(\"");
            
            #line 107 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Name));
            
            #line default
            #line hidden
            this.Write("\", SqlDbType.");
            
            #line 107 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.SqlTypeName));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 107 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.param.Size));
            
            #line default
            #line hidden
            this.Write(", ParameterDirection.");
            
            #line 107 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.param.Direction));
            
            #line default
            #line hidden
            this.Write("));\r\n");
            
            #line 108 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}//foreach
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\r\n");
            
            #line 111 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
		foreach (var pa in sp.rgParam) {
            
            #line default
            #line hidden
            this.Write("\t\tpublic SqlParameter param_");
            
            #line 112 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" {\r\n\t\t\tget {return cmd.Parameters[\"");
            
            #line 113 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Name));
            
            #line default
            #line hidden
            this.Write("\"];}\r\n\t\t}\r\n\t\tpublic DbWrap.WrapType<");
            
            #line 115 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.TypeName));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 115 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" {\r\n\t\t\tget {return new DbWrap.WrapType<");
            
            #line 116 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.TypeName));
            
            #line default
            #line hidden
            this.Write(">(param_");
            
            #line 116 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(".Value);}\r\n");
            
            #line 117 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			if(pa.param.Direction!=System.Data.ParameterDirection.ReturnValue) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\tset {\r\n\t\t\t\t\tif(value!=null) {\r\n\t\t\t\t\t\tparam_");
            
            #line 120 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(".Value = value.ObjectValue;\r\n\t\t\t\t\t}\r\n\t\t\t\t}\r\n");
            
            #line 123 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}//if
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n");
            
            #line 125 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
		}//foreach
            
            #line default
            #line hidden
            this.Write("\r\n\t\tprotected void Param2Command(Parameter param) {\r\n\t\t\tif(param==null) {\r\n\t\t\t\tre" +
                    "turn;\r\n\t\t\t}\r\n");
            
            #line 131 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			foreach (var pa in sp.rgParam) {
            
            #line default
            #line hidden
            
            #line 132 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
				if(pa.param.Direction!=System.Data.ParameterDirection.ReturnValue) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tif (param.");
            
            #line 133 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" != null) {\r\n\t\t\t\t\t\tthis.");
            
            #line 134 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" = param.");
            
            #line 134 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\t\t\t\t}\r\n");
            
            #line 136 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
				}//if
            
            #line default
            #line hidden
            
            #line 137 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			}//foreach
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\r\n\t\tprotected void Command2Param(Parameter param) {\r\n\t\t\tif(param==null) {\r\n\t" +
                    "\t\t\treturn;\r\n\t\t\t}\r\n");
            
            #line 144 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			foreach (var pa in sp.rgParam) {
				if(pa.param.Direction!=System.Data.ParameterDirection.Input) {
			
            
            #line default
            #line hidden
            this.Write("\t\t\tparam.");
            
            #line 147 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(" = this.");
            
            #line 147 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pa.Identifier));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 148 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
			
				}//if
			}//foreach
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\r\n\t\tpublic int ExecuteNonQuery(Parameter param = null) {\r\n\t\t\tParam2Command(p" +
                    "aram);\r\n\t\t\tint res = cmd.ExecuteNonQuery();\r\n\t\t\tCommand2Param(param);\r\n\t\t\treturn" +
                    " res;\r\n\t\t}\r\n\r\n\t}//class ");
            
            #line 160 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sp.ClassName));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n}//namespace ");
            
            #line 162 "C:\Mine\dmine\MF\svnt\trunk\DbWrap\StoredProcTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sp.SchemaName));
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class StoredProcTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}