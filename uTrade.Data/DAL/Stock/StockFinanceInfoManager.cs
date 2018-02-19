using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace uTrade.Data
{
    class StockFinanceInfoManager
    {

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "StockFinanceInfo");
        }

        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockFinanceInfo ");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Symbol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StockFinanceInfo");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = Symbol;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(StockFinanceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StockFinanceInfo(");
            strSql.Append("Symbol,Type,GBLT,SSSF,SSHY,CWUpdateTime,ListingDate,AllGB,GJG,FQRFRG,FRG,BG,HG,ZhGG,AllZC,LDZC,GDZC,WXZC,GDRS,LDFZ,CQFZ,ZBGJJ,JZC,ZYSR,ZYLR,YSZK,YYLR,TZSY,JYXJL,ZXJL,CH,LRZE,SHLR,JLR,WFLR,Unknow1,unknow2)");
            strSql.Append(" values (");
            strSql.Append("@Symbol,@Type,@GBLT,@SSSF,@SSHY,@CWUpdateTime,@ListingDate,@AllGB,@GJG,@FQRFRG,@FRG,@BG,@HG,@ZhGG,@AllZC,@LDZC,@GDZC,@WXZC,@GDRS,@LDFZ,@CQFZ,@ZBGJJ,@JZC,@ZYSR,@ZYLR,@YSZK,@YYLR,@TZSY,@JYXJL,@ZXJL,@CH,@LRZE,@SHLR,@JLR,@WFLR,@Unknow1,@unknow2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,0),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@GBLT", SqlDbType.NVarChar,50),
					new SqlParameter("@SSSF", SqlDbType.NVarChar,50),
					new SqlParameter("@SSHY", SqlDbType.NVarChar,50),
					new SqlParameter("@CWUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ListingDate", SqlDbType.DateTime),
					new SqlParameter("@AllGB", SqlDbType.NVarChar,50),
					new SqlParameter("@GJG", SqlDbType.NVarChar,50),
					new SqlParameter("@FQRFRG", SqlDbType.NVarChar,50),
					new SqlParameter("@FRG", SqlDbType.NVarChar,50),
					new SqlParameter("@BG", SqlDbType.NVarChar,50),
					new SqlParameter("@HG", SqlDbType.NVarChar,50),
					new SqlParameter("@ZhGG", SqlDbType.NVarChar,50),
					new SqlParameter("@AllZC", SqlDbType.NVarChar,50),
					new SqlParameter("@LDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@WXZC", SqlDbType.NVarChar,50),
					new SqlParameter("@GDRS", SqlDbType.NVarChar,50),
					new SqlParameter("@LDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZBGJJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JZC", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYSR", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYLR", SqlDbType.NVarChar,50),
					new SqlParameter("@YSZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YYLR", SqlDbType.NVarChar,50),
					new SqlParameter("@TZSY", SqlDbType.NVarChar,50),
					new SqlParameter("@JYXJL", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXJL", SqlDbType.NVarChar,50),
					new SqlParameter("@CH", SqlDbType.NVarChar,50),
					new SqlParameter("@LRZE", SqlDbType.NVarChar,50),
					new SqlParameter("@SHLR", SqlDbType.NVarChar,50),
					new SqlParameter("@JLR", SqlDbType.NVarChar,50),
					new SqlParameter("@WFLR", SqlDbType.NVarChar,50),
					new SqlParameter("@Unknow1", SqlDbType.NVarChar,50),
					new SqlParameter("@unknow2", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Code;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.GBLT;
            parameters[3].Value = model.SSSF;
            parameters[4].Value = model.SSHY;
            parameters[5].Value = model.CWUpdateTime;
            parameters[6].Value = model.ListingDate;
            parameters[7].Value = model.AllGB;
            parameters[8].Value = model.GJG;
            parameters[9].Value = model.FQRFRG;
            parameters[10].Value = model.FRG;
            parameters[11].Value = model.BG;
            parameters[12].Value = model.HG;
            parameters[13].Value = model.ZhGG;
            parameters[14].Value = model.AllZC;
            parameters[15].Value = model.LDZC;
            parameters[16].Value = model.GDZC;
            parameters[17].Value = model.WXZC;
            parameters[18].Value = model.GDRS;
            parameters[19].Value = model.LDFZ;
            parameters[20].Value = model.CQFZ;
            parameters[21].Value = model.ZBGJJ;
            parameters[22].Value = model.JZC;
            parameters[23].Value = model.ZYSR;
            parameters[24].Value = model.ZYLR;
            parameters[25].Value = model.YSZK;
            parameters[26].Value = model.YYLR;
            parameters[27].Value = model.TZSY;
            parameters[28].Value = model.JYXJL;
            parameters[29].Value = model.ZXJL;
            parameters[30].Value = model.CH;
            parameters[31].Value = model.LRZE;
            parameters[32].Value = model.SHLR;
            parameters[33].Value = model.JLR;
            parameters[34].Value = model.WFLR;
            parameters[35].Value = model.Unknow1;
            parameters[36].Value = model.unknow2;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(StockFinanceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StockFinanceInfo set ");
            strSql.Append("Symbol=@Symbol,");
            strSql.Append("Type=@Type,");
            strSql.Append("GBLT=@GBLT,");
            strSql.Append("SSSF=@SSSF,");
            strSql.Append("SSHY=@SSHY,");
            strSql.Append("CWUpdateTime=@CWUpdateTime,");
            strSql.Append("ListingDate=@ListingDate,");
            strSql.Append("AllGB=@AllGB,");
            strSql.Append("GJG=@GJG,");
            strSql.Append("FQRFRG=@FQRFRG,");
            strSql.Append("FRG=@FRG,");
            strSql.Append("BG=@BG,");
            strSql.Append("HG=@HG,");
            strSql.Append("ZhGG=@ZhGG,");
            strSql.Append("AllZC=@AllZC,");
            strSql.Append("LDZC=@LDZC,");
            strSql.Append("GDZC=@GDZC,");
            strSql.Append("WXZC=@WXZC,");
            strSql.Append("GDRS=@GDRS,");
            strSql.Append("LDFZ=@LDFZ,");
            strSql.Append("CQFZ=@CQFZ,");
            strSql.Append("ZBGJJ=@ZBGJJ,");
            strSql.Append("JZC=@JZC,");
            strSql.Append("ZYSR=@ZYSR,");
            strSql.Append("ZYLR=@ZYLR,");
            strSql.Append("YSZK=@YSZK,");
            strSql.Append("YYLR=@YYLR,");
            strSql.Append("TZSY=@TZSY,");
            strSql.Append("JYXJL=@JYXJL,");
            strSql.Append("ZXJL=@ZXJL,");
            strSql.Append("CH=@CH,");
            strSql.Append("LRZE=@LRZE,");
            strSql.Append("SHLR=@SHLR,");
            strSql.Append("JLR=@JLR,");
            strSql.Append("WFLR=@WFLR,");
            strSql.Append("Unknow1=@Unknow1,");
            strSql.Append("unknow2=@unknow2");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@GBLT", SqlDbType.NVarChar,50),
					new SqlParameter("@SSSF", SqlDbType.NVarChar,50),
					new SqlParameter("@SSHY", SqlDbType.NVarChar,50),
					new SqlParameter("@CWUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ListingDate", SqlDbType.DateTime),
					new SqlParameter("@AllGB", SqlDbType.NVarChar,50),
					new SqlParameter("@GJG", SqlDbType.NVarChar,50),
					new SqlParameter("@FQRFRG", SqlDbType.NVarChar,50),
					new SqlParameter("@FRG", SqlDbType.NVarChar,50),
					new SqlParameter("@BG", SqlDbType.NVarChar,50),
					new SqlParameter("@HG", SqlDbType.NVarChar,50),
					new SqlParameter("@ZhGG", SqlDbType.NVarChar,50),
					new SqlParameter("@AllZC", SqlDbType.NVarChar,50),
					new SqlParameter("@LDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@WXZC", SqlDbType.NVarChar,50),
					new SqlParameter("@GDRS", SqlDbType.NVarChar,50),
					new SqlParameter("@LDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZBGJJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JZC", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYSR", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYLR", SqlDbType.NVarChar,50),
					new SqlParameter("@YSZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YYLR", SqlDbType.NVarChar,50),
					new SqlParameter("@TZSY", SqlDbType.NVarChar,50),
					new SqlParameter("@JYXJL", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXJL", SqlDbType.NVarChar,50),
					new SqlParameter("@CH", SqlDbType.NVarChar,50),
					new SqlParameter("@LRZE", SqlDbType.NVarChar,50),
					new SqlParameter("@SHLR", SqlDbType.NVarChar,50),
					new SqlParameter("@JLR", SqlDbType.NVarChar,50),
					new SqlParameter("@WFLR", SqlDbType.NVarChar,50),
					new SqlParameter("@Unknow1", SqlDbType.NVarChar,50),
					new SqlParameter("@unknow2", SqlDbType.NVarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.Code;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.GBLT;
            parameters[3].Value = model.SSSF;
            parameters[4].Value = model.SSHY;
            parameters[5].Value = model.CWUpdateTime;
            parameters[6].Value = model.ListingDate;
            parameters[7].Value = model.AllGB;
            parameters[8].Value = model.GJG;
            parameters[9].Value = model.FQRFRG;
            parameters[10].Value = model.FRG;
            parameters[11].Value = model.BG;
            parameters[12].Value = model.HG;
            parameters[13].Value = model.ZhGG;
            parameters[14].Value = model.AllZC;
            parameters[15].Value = model.LDZC;
            parameters[16].Value = model.GDZC;
            parameters[17].Value = model.WXZC;
            parameters[18].Value = model.GDRS;
            parameters[19].Value = model.LDFZ;
            parameters[20].Value = model.CQFZ;
            parameters[21].Value = model.ZBGJJ;
            parameters[22].Value = model.JZC;
            parameters[23].Value = model.ZYSR;
            parameters[24].Value = model.ZYLR;
            parameters[25].Value = model.YSZK;
            parameters[26].Value = model.YYLR;
            parameters[27].Value = model.TZSY;
            parameters[28].Value = model.JYXJL;
            parameters[29].Value = model.ZXJL;
            parameters[30].Value = model.CH;
            parameters[31].Value = model.LRZE;
            parameters[32].Value = model.SHLR;
            parameters[33].Value = model.JLR;
            parameters[34].Value = model.WFLR;
            parameters[35].Value = model.Unknow1;
            parameters[36].Value = model.unknow2;
            parameters[37].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string Symbol)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockFinanceInfo ");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = Symbol;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string SymbolList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockFinanceInfo ");
            strSql.Append(" where id in (" + SymbolList + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public StockFinanceInfo GetModel(string Symbol)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Symbol,Type,GBLT,SSSF,SSHY,CWUpdateTime,ListingDate,AllGB,GJG,FQRFRG,FRG,BG,HG,ZhGG,AllZC,LDZC,GDZC,WXZC,GDRS,LDFZ,CQFZ,ZBGJJ,JZC,ZYSR,ZYLR,YSZK,YYLR,TZSY,JYXJL,ZXJL,CH,LRZE,SHLR,JLR,WFLR,Unknow1,unknow2 from StockFinanceInfo ");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = Symbol;

            StockFinanceInfo model = new StockFinanceInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public StockFinanceInfo DataRowToModel(DataRow row)
        {
            StockFinanceInfo model = new StockFinanceInfo();
            if (row != null)
            {
                //if (row["id"] != null && row["id"].ToString() != "")
                //{
                //    model.id = int.Parse(row["id"].ToString());
                //}
                if (row["Symbol"] != null)
                {
                    model.Code = row["Symbol"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["GBLT"] != null)
                {
                    model.GBLT = row["GBLT"].ToString();
                }
                if (row["SSSF"] != null)
                {
                    model.SSSF = row["SSSF"].ToString();
                }
                if (row["SSHY"] != null)
                {
                    model.SSHY = row["SSHY"].ToString();
                }
                if (row["CWUpdateTime"] != null && row["CWUpdateTime"].ToString() != "")
                {
                    model.CWUpdateTime = DateTime.Parse(row["CWUpdateTime"].ToString());
                }
                if (row["ListingDate"] != null && row["ListingDate"].ToString() != "")
                {
                    model.ListingDate = DateTime.Parse(row["ListingDate"].ToString());
                }
                if (row["AllGB"] != null)
                {
                    model.AllGB = row["AllGB"].ToString();
                }
                if (row["GJG"] != null)
                {
                    model.GJG = row["GJG"].ToString();
                }
                if (row["FQRFRG"] != null)
                {
                    model.FQRFRG = row["FQRFRG"].ToString();
                }
                if (row["FRG"] != null)
                {
                    model.FRG = row["FRG"].ToString();
                }
                if (row["BG"] != null)
                {
                    model.BG = row["BG"].ToString();
                }
                if (row["HG"] != null)
                {
                    model.HG = row["HG"].ToString();
                }
                if (row["ZhGG"] != null)
                {
                    model.ZhGG = row["ZhGG"].ToString();
                }
                if (row["AllZC"] != null)
                {
                    model.AllZC = row["AllZC"].ToString();
                }
                if (row["LDZC"] != null)
                {
                    model.LDZC = row["LDZC"].ToString();
                }
                if (row["GDZC"] != null)
                {
                    model.GDZC = row["GDZC"].ToString();
                }
                if (row["WXZC"] != null)
                {
                    model.WXZC = row["WXZC"].ToString();
                }
                if (row["GDRS"] != null)
                {
                    model.GDRS = row["GDRS"].ToString();
                }
                if (row["LDFZ"] != null)
                {
                    model.LDFZ = row["LDFZ"].ToString();
                }
                if (row["CQFZ"] != null)
                {
                    model.CQFZ = row["CQFZ"].ToString();
                }
                if (row["ZBGJJ"] != null)
                {
                    model.ZBGJJ = row["ZBGJJ"].ToString();
                }
                if (row["JZC"] != null)
                {
                    model.JZC = row["JZC"].ToString();
                }
                if (row["ZYSR"] != null)
                {
                    model.ZYSR = row["ZYSR"].ToString();
                }
                if (row["ZYLR"] != null)
                {
                    model.ZYLR = row["ZYLR"].ToString();
                }
                if (row["YSZK"] != null)
                {
                    model.YSZK = row["YSZK"].ToString();
                }
                if (row["YYLR"] != null)
                {
                    model.YYLR = row["YYLR"].ToString();
                }
                if (row["TZSY"] != null)
                {
                    model.TZSY = row["TZSY"].ToString();
                }
                if (row["JYXJL"] != null)
                {
                    model.JYXJL = row["JYXJL"].ToString();
                }
                if (row["ZXJL"] != null)
                {
                    model.ZXJL = row["ZXJL"].ToString();
                }
                if (row["CH"] != null)
                {
                    model.CH = row["CH"].ToString();
                }
                if (row["LRZE"] != null)
                {
                    model.LRZE = row["LRZE"].ToString();
                }
                if (row["SHLR"] != null)
                {
                    model.SHLR = row["SHLR"].ToString();
                }
                if (row["JLR"] != null)
                {
                    model.JLR = row["JLR"].ToString();
                }
                if (row["WFLR"] != null)
                {
                    model.WFLR = row["WFLR"].ToString();
                }
                if (row["Unknow1"] != null)
                {
                    model.Unknow1 = row["Unknow1"].ToString();
                }
                if (row["unknow2"] != null)
                {
                    model.unknow2 = row["unknow2"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Symbol,Type,GBLT,SSSF,SSHY,CWUpdateTime,ListingDate,AllGB,GJG,FQRFRG,FRG,BG,HG,ZhGG,AllZC,LDZC,GDZC,WXZC,GDRS,LDFZ,CQFZ,ZBGJJ,JZC,ZYSR,ZYLR,YSZK,YYLR,TZSY,JYXJL,ZXJL,CH,LRZE,SHLR,JLR,WFLR,Unknow1,unknow2 ");
            strSql.Append(" FROM StockFinanceInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Symbol,Type,GBLT,SSSF,SSHY,CWUpdateTime,ListingDate,AllGB,GJG,FQRFRG,FRG,BG,HG,ZhGG,AllZC,LDZC,GDZC,WXZC,GDRS,LDFZ,CQFZ,ZBGJJ,JZC,ZYSR,ZYLR,YSZK,YYLR,TZSY,JYXJL,ZXJL,CH,LRZE,SHLR,JLR,WFLR,Unknow1,unknow2 ");
            strSql.Append(" FROM StockFinanceInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM StockFinanceInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Symbol, desc");
            }
            strSql.Append(")AS Row, T.*  from StockFinanceInfo T ");
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
            parameters[0].Value = "StockFinanceInfo";
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
