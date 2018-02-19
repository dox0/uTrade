using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace uTrade.Data
{
    class StockInfoManager
    {

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Symbol", "StockInfo");
        }

        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockInfo ");

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
            strSql.Append("select count(1) from StockInfo");
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
        public int Add(StockInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StockInfo(");
            strSql.Append("Symbol,Type,OneHand,Name,PointIndex,YestClose,Unknow1,Unknow2,Unknow3)");
            strSql.Append(" values (");
            strSql.Append("@Symbol,@Type,@OneHand,@Name,@PointIndex,@YestClose,@Unknow1,@Unknow2,@Unknow3)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@OneHand", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@PointIndex", SqlDbType.NVarChar,50),
					new SqlParameter("@YestClose", SqlDbType.Decimal,9),
					new SqlParameter("@Unknow1", SqlDbType.NVarChar,50),
					new SqlParameter("@Unknow2", SqlDbType.NVarChar,50),
					new SqlParameter("@Unknow3", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.stockcode;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.OneHand;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.PointIndex;
            parameters[5].Value = model.YestClose;
            parameters[6].Value = model.Unknow1;
            parameters[7].Value = model.Unknow2;
            parameters[8].Value = model.Unknow3;

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
        public bool Update(StockInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StockInfo set ");
            strSql.Append("Symbol=@Symbol,");
            strSql.Append("Type=@Type,");
            strSql.Append("OneHand=@OneHand,");
            strSql.Append("Name=@Name,");
            strSql.Append("PointIndex=@PointIndex,");
            strSql.Append("YestClose=@YestClose,");
            strSql.Append("Unknow1=@Unknow1,");
            strSql.Append("Unknow2=@Unknow2,");
            strSql.Append("Unknow3=@Unknow3");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@OneHand", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@PointIndex", SqlDbType.NVarChar,50),
					new SqlParameter("@YestClose", SqlDbType.Decimal,9),
					new SqlParameter("@Unknow1", SqlDbType.NVarChar,50),
					new SqlParameter("@Unknow2", SqlDbType.NVarChar,50),
					new SqlParameter("@Unknow3", SqlDbType.NVarChar,50),
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)};
            parameters[0].Value = model.stockcode;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.OneHand;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.PointIndex;
            parameters[5].Value = model.YestClose;
            parameters[6].Value = model.Unknow1;
            parameters[7].Value = model.Unknow2;
            parameters[8].Value = model.Unknow3;
            parameters[9].Value = model.id;

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
            strSql.Append("delete from stockinfo ");
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
            strSql.Append("delete from stockinfo ");
            strSql.Append(" where Symbol in (" + SymbolList + ")  ");
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
        public StockInfo GetModel(string Symbol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Symbol,Type,OneHand,Name,PointIndex,YestClose,Unknow1,Unknow2,Unknow3 from StockInfo ");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = Symbol;

            StockInfo model = new StockInfo();
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
        public StockInfo DataRowToModel(DataRow row)
        {
            StockInfo model = new StockInfo();
            if (row != null)
            {
                //if (row["id"] != null && row["id"].ToString() != "")
                //{
                //    model.id = int.Parse(row["id"].ToString());
                //}
                if (row["Symbol"] != null)
                {
                    model.stockcode = row["Symbol"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["OneHand"] != null)
                {
                    model.OneHand = row["OneHand"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["PointIndex"] != null)
                {
                    model.PointIndex = row["PointIndex"].ToString();
                }
                if (row["YestClose"] != null && row["YestClose"].ToString() != "")
                {
                    model.YestClose = decimal.Parse(row["YestClose"].ToString());
                }
                if (row["Unknow1"] != null)
                {
                    model.Unknow1 = row["Unknow1"].ToString();
                }
                if (row["Unknow2"] != null)
                {
                    model.Unknow2 = row["Unknow2"].ToString();
                }
                if (row["Unknow3"] != null)
                {
                    model.Unknow3 = row["Unknow3"].ToString();
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
            strSql.Append("select Symbol,Type,OneHand,Name,PointIndex,YestClose,Unknow1,Unknow2,Unknow3 ");
            strSql.Append(" FROM StockInfo ");
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
            strSql.Append(" Symbol,Type,OneHand,Name,PointIndex,YestClose,Unknow1,Unknow2,Unknow3 ");
            strSql.Append(" FROM StockInfo ");
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
            strSql.Append("select count(1) FROM StockInfo ");
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
                strSql.Append("order by T.Symbol desc");
            }
            strSql.Append(")AS Row, T.*  from StockInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StockInfo> DataTableToList(DataTable dt)
        {
            List<StockInfo> modelList = new List<StockInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                StockInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StockInfo> GetStockCodeList(string strWhere)
        {
            DataSet ds = GetList("Unknow2<>0 and Name not like '%指数%' ");
            return DataTableToList(ds.Tables[0]);
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
            parameters[0].Value = "StockInfo";
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
