using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references

using uTrade.Data;
using uTrade.Models;

namespace uTrade.DAL
{
    /// <summary>
    /// 数据访问类:Rank
    /// </summary>
    public partial class SelfSelectedDAL
    {
        static SelfSelectedDAL inst = null;
        public static SelfSelectedDAL Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new SelfSelectedDAL();
                }
                return inst;
            }
        }

        public SelfSelectedDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Symbol", "tbl_SelfSelected");
        }

        public static bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_SelfSelected ");

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
        public bool Exists(string strSymbol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_SelfSelected");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
                    new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
            };
            parameters[0].Value = strSymbol;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfSelectedModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_SelfSelected(");
            strSql.Append("Symbol,Name,BuyDate,BuyPrice,BuyQuant,SaleDate,SalePrice,CurQuant,CurProfit,Rank,Kind,Custom)");
            strSql.Append(" values (");
            strSql.Append("@Symbol,@Name,@BuyDate,@BuyPrice,@BuyQuant,@SaleDate,@SalePrice,@CurQuant,@CurProfit,@Rank,@Kind,@Custom)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Symbol", SqlDbType.VarChar,10),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuyDate", SqlDbType.Date),
                    new SqlParameter("@BuyPrice", SqlDbType.Float),
                    new SqlParameter("@BuyQuant", SqlDbType.Float),
                    new SqlParameter("@SaleDate", SqlDbType.Date),
                    new SqlParameter("@SalePrice", SqlDbType.Float),
                    new SqlParameter("@CurQuant", SqlDbType.Float),
                    new SqlParameter("@CurProfit", SqlDbType.Float),
                    new SqlParameter("@Rank", SqlDbType.VarChar,20),
                    new SqlParameter("@Kind", SqlDbType.VarChar,10),
                    new SqlParameter("@Custom", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Symbol;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.BuyDate;
            parameters[3].Value = model.BuyPrice;
            parameters[4].Value = model.BuyQuant;
            parameters[5].Value = model.SaleDate;
            parameters[6].Value = model.SalePrice;
            parameters[7].Value = model.CurQuant;
            parameters[8].Value = model.CurProfit;
            parameters[9].Value = model.Rank;
            parameters[10].Value = model.Kind;
            parameters[11].Value = model.Custom;

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
        public int Update(SelfSelectedModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_SelfSelected set ");
            strSql.Append("Name=@Name,");
            strSql.Append("BuyDate=@BuyDate,");
            strSql.Append("BuyPrice=@BuyPrice,");
            strSql.Append("BuyQuant=@BuyQuant,");
            strSql.Append("CurQuant=@CurQuant,");
            strSql.Append("CurProfit=@CurProfit,");
            strSql.Append("Rank=@Rank,");
            strSql.Append("Kind=@Kind,");
            strSql.Append("Custom=@Custom,");
            strSql.Append(" where Symbol=@Symbol,");
            SqlParameter[] parameters = {
                    new SqlParameter("@Symbol", SqlDbType.VarChar,10),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuyDate", SqlDbType.Date),
                    new SqlParameter("@BuyPrice", SqlDbType.Float),
                    new SqlParameter("@BuyQuant", SqlDbType.Float),
                    new SqlParameter("@SaleDate", SqlDbType.Date),
                    new SqlParameter("@SalePrice", SqlDbType.Float),
                    new SqlParameter("@CurQuant", SqlDbType.Float),
                    new SqlParameter("@CurProfit", SqlDbType.Float),
                    new SqlParameter("@Rank", SqlDbType.VarChar,20),
                    new SqlParameter("@Kind", SqlDbType.VarChar,10),
                    new SqlParameter("@Custom", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Symbol;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.BuyDate;
            parameters[3].Value = model.BuyPrice;
            parameters[4].Value = model.BuyQuant;
            parameters[5].Value = model.SaleDate;
            parameters[6].Value = model.SalePrice;
            parameters[7].Value = model.CurQuant;
            parameters[8].Value = model.CurProfit;
            parameters[9].Value = model.Rank;
            parameters[10].Value = model.Kind;
            parameters[11].Value = model.Custom;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

            return rows;

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string strSymbol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_SelfSelected ");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
                    new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
            };
            parameters[0].Value = strSymbol;

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
        public bool DeleteList(string strSymbollist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_SelfSelected ");
            strSql.Append(" where Symbol in (" + strSymbollist + ")  ");
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
        public SelfSelectedModel GetModel(string strSymbol)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Symbol,Name,BuyDate,BuyPrice,BuyQuant,SaleDate,SalePrice,CurQuant,CurProfit,Rank,Kind,Custom ");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
                    new SqlParameter("@Symbol", SqlDbType.Int,4)
            };
            parameters[0].Value = strSymbol;

            Rank model = new Rank();
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
        public SelfSelectedModel DataRowToModel(DataRow row)
        {
            SelfSelectedModel model = new SelfSelectedModel();
            if (row != null)
            {
                if (row["Symbol"] != null && row["Symbol"].ToString() != "")
                {
                    model.Symbol = row["Symbol"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["BuyDate"] != null)
                {
                    model.BuyDate = Convert.ToDateTime(row["BuyDate"]);
                }
                if (row["BuyPrice"] != null)
                {
                    model.BuyPrice = float.Parse(row["BuyPrice"].ToString());
                }
                if (row["BuyQuant"] != null && row["BuyQuant"].ToString() != "")
                {
                    model.BuyQuant = float.Parse(row["BuyQuant"].ToString());
                }
                if (row["SaleDate"] != null)
                {
                    model.BuyDate = Convert.ToDateTime(row["BuyDate"]);
                }
                if (row["SalePrice"] != null)
                {
                    model.BuyPrice = float.Parse(row["BuyPrice"].ToString());
                }
                if (row["CurQuant"] != null)
                {
                    model.CurQuant = float.Parse(row["CurQuant"].ToString());
                }
                if (row["CurProfit"] != null)
                {
                    model.CurProfit = float.Parse(row["CurProfit"].ToString());
                }
                if (row["Rank"] != null)
                {
                    model.Rank = row["Rank"].ToString();
                }
                if (row["Kind"] != null)
                {
                    model.Kind = row["Kind"].ToString();
                }
                if (row["Custom"] != null)
                {
                    model.Custom = row["Custom"].ToString();
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
            strSql.Append("select Symbol,Name,BuyDate,BuyPrice,BuyQuant,SaleDate,SalePrice,CurQuant,CurProfit,Rank,Kind,Custom ");
            strSql.Append(" from tbl_SelfSelected ");
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
            strSql.Append(" Symbol,Name,BuyDate,BuyPrice,BuyQuant,SaleDate,SalePrice,CurQuant,CurProfit,Rank,Kind,Custom ");
            strSql.Append(" FROM tbl_SelfSelected ");
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
            strSql.Append("select count(1) FROM tbl_SelfSelected ");
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
                strSql.Append("order by T.CID desc");
            }
            strSql.Append(")AS Row, T.*  from Course T ");
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
			parameters[0].Value = "Course";
			parameters[1].Value = "CID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
    }
}

