using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace uTrade.Data
{
    class StockCwInfoManager
    {
        public StockCwInfoManager()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from StockCwInfo");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(StockCwInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into StockCwInfo(");
			strSql.Append("Code,ReportDate,JBMGSY,MGJZC,MGJYHDCSXJLJE,ZYYWSR,ZYYWLR,YYLR,TZSY,YYEYSZJE,LRZE,JLR,JLROUT,JYHDCSDXJLJE,XJJXJDJWJCJE,ZZC,LDZC,ZFZ,LDFZ,GDQYBHSSGDQY,JZCSYLJQ)");
			strSql.Append(" values (");
			strSql.Append("@Code,@ReportDate,@JBMGSY,@MGJZC,@MGJYHDCSXJLJE,@ZYYWSR,@ZYYWLR,@YYLR,@TZSY,@YYEYSZJE,@LRZE,@JLR,@JLROUT,@JYHDCSDXJLJE,@XJJXJDJWJCJE,@ZZC,@LDZC,@ZFZ,@LDFZ,@GDQYBHSSGDQY,@JZCSYLJQ)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,10),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@JBMGSY", SqlDbType.Decimal,9),
					new SqlParameter("@MGJZC", SqlDbType.Decimal,9),
					new SqlParameter("@MGJYHDCSXJLJE", SqlDbType.Decimal,9),
					new SqlParameter("@ZYYWSR", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYYWLR", SqlDbType.NVarChar,50),
					new SqlParameter("@YYLR", SqlDbType.NVarChar,50),
					new SqlParameter("@TZSY", SqlDbType.NVarChar,50),
					new SqlParameter("@YYEYSZJE", SqlDbType.NVarChar,50),
					new SqlParameter("@LRZE", SqlDbType.NVarChar,50),
					new SqlParameter("@JLR", SqlDbType.NVarChar,50),
					new SqlParameter("@JLROUT", SqlDbType.NVarChar,50),
					new SqlParameter("@JYHDCSDXJLJE", SqlDbType.NVarChar,50),
					new SqlParameter("@XJJXJDJWJCJE", SqlDbType.NVarChar,50),
					new SqlParameter("@ZZC", SqlDbType.NVarChar,50),
					new SqlParameter("@LDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@ZFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@LDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDQYBHSSGDQY", SqlDbType.NVarChar,50),
					new SqlParameter("@JZCSYLJQ", SqlDbType.Decimal,9)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.ReportDate;
			parameters[2].Value = model.JBMGSY;
			parameters[3].Value = model.MGJZC;
			parameters[4].Value = model.MGJYHDCSXJLJE;
			parameters[5].Value = model.ZYYWSR;
			parameters[6].Value = model.ZYYWLR;
			parameters[7].Value = model.YYLR;
			parameters[8].Value = model.TZSY;
			parameters[9].Value = model.YYEYSZJE;
			parameters[10].Value = model.LRZE;
			parameters[11].Value = model.JLR;
			parameters[12].Value = model.JLROUT;
			parameters[13].Value = model.JYHDCSDXJLJE;
			parameters[14].Value = model.XJJXJDJWJCJE;
			parameters[15].Value = model.ZZC;
			parameters[16].Value = model.LDZC;
			parameters[17].Value = model.ZFZ;
			parameters[18].Value = model.LDFZ;
			parameters[19].Value = model.GDQYBHSSGDQY;
			parameters[20].Value = model.JZCSYLJQ;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(StockCwInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update StockCwInfo set ");
			strSql.Append("Code=@Code,");
			strSql.Append("ReportDate=@ReportDate,");
			strSql.Append("JBMGSY=@JBMGSY,");
			strSql.Append("MGJZC=@MGJZC,");
			strSql.Append("MGJYHDCSXJLJE=@MGJYHDCSXJLJE,");
			strSql.Append("ZYYWSR=@ZYYWSR,");
			strSql.Append("ZYYWLR=@ZYYWLR,");
			strSql.Append("YYLR=@YYLR,");
			strSql.Append("TZSY=@TZSY,");
			strSql.Append("YYEYSZJE=@YYEYSZJE,");
			strSql.Append("LRZE=@LRZE,");
			strSql.Append("JLR=@JLR,");
			strSql.Append("JLROUT=@JLROUT,");
			strSql.Append("JYHDCSDXJLJE=@JYHDCSDXJLJE,");
			strSql.Append("XJJXJDJWJCJE=@XJJXJDJWJCJE,");
			strSql.Append("ZZC=@ZZC,");
			strSql.Append("LDZC=@LDZC,");
			strSql.Append("ZFZ=@ZFZ,");
			strSql.Append("LDFZ=@LDFZ,");
			strSql.Append("GDQYBHSSGDQY=@GDQYBHSSGDQY,");
			strSql.Append("JZCSYLJQ=@JZCSYLJQ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,10),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@JBMGSY", SqlDbType.Decimal,9),
					new SqlParameter("@MGJZC", SqlDbType.Decimal,9),
					new SqlParameter("@MGJYHDCSXJLJE", SqlDbType.Decimal,9),
					new SqlParameter("@ZYYWSR", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYYWLR", SqlDbType.NVarChar,50),
					new SqlParameter("@YYLR", SqlDbType.NVarChar,50),
					new SqlParameter("@TZSY", SqlDbType.NVarChar,50),
					new SqlParameter("@YYEYSZJE", SqlDbType.NVarChar,50),
					new SqlParameter("@LRZE", SqlDbType.NVarChar,50),
					new SqlParameter("@JLR", SqlDbType.NVarChar,50),
					new SqlParameter("@JLROUT", SqlDbType.NVarChar,50),
					new SqlParameter("@JYHDCSDXJLJE", SqlDbType.NVarChar,50),
					new SqlParameter("@XJJXJDJWJCJE", SqlDbType.NVarChar,50),
					new SqlParameter("@ZZC", SqlDbType.NVarChar,50),
					new SqlParameter("@LDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@ZFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@LDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDQYBHSSGDQY", SqlDbType.NVarChar,50),
					new SqlParameter("@JZCSYLJQ", SqlDbType.Decimal,9),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.ReportDate;
			parameters[2].Value = model.JBMGSY;
			parameters[3].Value = model.MGJZC;
			parameters[4].Value = model.MGJYHDCSXJLJE;
			parameters[5].Value = model.ZYYWSR;
			parameters[6].Value = model.ZYYWLR;
			parameters[7].Value = model.YYLR;
			parameters[8].Value = model.TZSY;
			parameters[9].Value = model.YYEYSZJE;
			parameters[10].Value = model.LRZE;
			parameters[11].Value = model.JLR;
			parameters[12].Value = model.JLROUT;
			parameters[13].Value = model.JYHDCSDXJLJE;
			parameters[14].Value = model.XJJXJDJWJCJE;
			parameters[15].Value = model.ZZC;
			parameters[16].Value = model.LDZC;
			parameters[17].Value = model.ZFZ;
			parameters[18].Value = model.LDFZ;
			parameters[19].Value = model.GDQYBHSSGDQY;
			parameters[20].Value = model.JZCSYLJQ;
			parameters[21].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StockCwInfo ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StockCwInfo ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public StockCwInfo GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,Code,ReportDate,JBMGSY,MGJZC,MGJYHDCSXJLJE,ZYYWSR,ZYYWLR,YYLR,TZSY,YYEYSZJE,LRZE,JLR,JLROUT,JYHDCSDXJLJE,XJJXJDJWJCJE,ZZC,LDZC,ZFZ,LDFZ,GDQYBHSSGDQY,JZCSYLJQ from StockCwInfo ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			StockCwInfo model = new StockCwInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public StockCwInfo DataRowToModel(DataRow row)
		{
			StockCwInfo model = new StockCwInfo();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["Code"]!=null)
				{
					model.Code=row["Code"].ToString();
				}
				if(row["ReportDate"]!=null && row["ReportDate"].ToString()!="")
				{
					model.ReportDate=DateTime.Parse(row["ReportDate"].ToString());
				}
				if(row["JBMGSY"]!=null && row["JBMGSY"].ToString()!="")
				{
					model.JBMGSY=decimal.Parse(row["JBMGSY"].ToString());
				}
				if(row["MGJZC"]!=null && row["MGJZC"].ToString()!="")
				{
					model.MGJZC=decimal.Parse(row["MGJZC"].ToString());
				}
				if(row["MGJYHDCSXJLJE"]!=null && row["MGJYHDCSXJLJE"].ToString()!="")
				{
					model.MGJYHDCSXJLJE=decimal.Parse(row["MGJYHDCSXJLJE"].ToString());
				}
				if(row["ZYYWSR"]!=null)
				{
					model.ZYYWSR=row["ZYYWSR"].ToString();
				}
				if(row["ZYYWLR"]!=null)
				{
					model.ZYYWLR=row["ZYYWLR"].ToString();
				}
				if(row["YYLR"]!=null)
				{
					model.YYLR=row["YYLR"].ToString();
				}
				if(row["TZSY"]!=null)
				{
					model.TZSY=row["TZSY"].ToString();
				}
				if(row["YYEYSZJE"]!=null)
				{
					model.YYEYSZJE=row["YYEYSZJE"].ToString();
				}
				if(row["LRZE"]!=null)
				{
					model.LRZE=row["LRZE"].ToString();
				}
				if(row["JLR"]!=null)
				{
					model.JLR=row["JLR"].ToString();
				}
				if(row["JLROUT"]!=null)
				{
					model.JLROUT=row["JLROUT"].ToString();
				}
				if(row["JYHDCSDXJLJE"]!=null)
				{
					model.JYHDCSDXJLJE=row["JYHDCSDXJLJE"].ToString();
				}
				if(row["XJJXJDJWJCJE"]!=null)
				{
					model.XJJXJDJWJCJE=row["XJJXJDJWJCJE"].ToString();
				}
				if(row["ZZC"]!=null)
				{
					model.ZZC=row["ZZC"].ToString();
				}
				if(row["LDZC"]!=null)
				{
					model.LDZC=row["LDZC"].ToString();
				}
				if(row["ZFZ"]!=null)
				{
					model.ZFZ=row["ZFZ"].ToString();
				}
				if(row["LDFZ"]!=null)
				{
					model.LDFZ=row["LDFZ"].ToString();
				}
				if(row["GDQYBHSSGDQY"]!=null)
				{
					model.GDQYBHSSGDQY=row["GDQYBHSSGDQY"].ToString();
				}
				if(row["JZCSYLJQ"]!=null && row["JZCSYLJQ"].ToString()!="")
				{
					model.JZCSYLJQ=decimal.Parse(row["JZCSYLJQ"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,Code,ReportDate,JBMGSY,MGJZC,MGJYHDCSXJLJE,ZYYWSR,ZYYWLR,YYLR,TZSY,YYEYSZJE,LRZE,JLR,JLROUT,JYHDCSDXJLJE,XJJXJDJWJCJE,ZZC,LDZC,ZFZ,LDFZ,GDQYBHSSGDQY,JZCSYLJQ ");
			strSql.Append(" FROM StockCwInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,Code,ReportDate,JBMGSY,MGJZC,MGJYHDCSXJLJE,ZYYWSR,ZYYWLR,YYLR,TZSY,YYEYSZJE,LRZE,JLR,JLROUT,JYHDCSDXJLJE,XJJXJDJWJCJE,ZZC,LDZC,ZFZ,LDFZ,GDQYBHSSGDQY,JZCSYLJQ ");
			strSql.Append(" FROM StockCwInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM StockCwInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from StockCwInfo T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "StockCwInfo";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

    }
}
