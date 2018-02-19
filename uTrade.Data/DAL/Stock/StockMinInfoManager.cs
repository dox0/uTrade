using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace uTrade.Data
{
    class StockMinInfoManager
    {

        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockMinInfo ");

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
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StockMinInfo");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(StockMinInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StockMinInfo(");
            strSql.Append("[Code],[Symbol],[Name],[Type],[Open],[High],[Low],[Atatus],[Price],[Yestclose],[Percent],[Updown],[Arrow],[Volume],[Turnover],[Ak1],[Ask2],[Ask3],[Ask4],[Ask5],[Askvol1],[Askvol2],[Askvol3],[Askvol4],[Askvol5],[Bid1],[Bid2],[Bid3],[Bid4],[Bid5],[Bidvol1],[Bidvol2],[Bidvol3],[Bidvol4],[Bidvol5],[Update],[Time])");
            strSql.Append(" values (");
            strSql.Append("@code,@Symbol,@Name,@Type,@Open,@high,@Low,@Status,@Price,@Yestclose,@Percent,@Updown,@Arrow,@Volume,@Turnover,@Ask1,@Ask2,@Ask3,@Ask4,@Ask5,@Askvol1,@Askvol2,@Askvol3,@Askvol4,@Askvol5,@Bid1,@Bid2,@Bid3,@Bid4,@Bid5,@Bidvol1,@Bidvol2,@Bidvol3,@Bidvol4,@Bidvol5,@Update,@Time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@Symbol", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.VarChar,-1),
					new SqlParameter("@Type", SqlDbType.NChar,10),
					new SqlParameter("@Open", SqlDbType.Decimal,9),
					new SqlParameter("@high", SqlDbType.Decimal,9),
					new SqlParameter("@Low", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Yestclose", SqlDbType.Decimal,9),
					new SqlParameter("@Percent", SqlDbType.Float,8),
					new SqlParameter("@Updown", SqlDbType.Float,8),
					new SqlParameter("@Arrow", SqlDbType.NChar,10),
					new SqlParameter("@Volume", SqlDbType.NVarChar,50),
					new SqlParameter("@Turnover", SqlDbType.Decimal,9),
					new SqlParameter("@Ask1", SqlDbType.Decimal,9),
					new SqlParameter("@Ask2", SqlDbType.Decimal,9),
					new SqlParameter("@Ask3", SqlDbType.Decimal,9),
					new SqlParameter("@Ask4", SqlDbType.Decimal,9),
					new SqlParameter("@Ask5", SqlDbType.Decimal,9),
					new SqlParameter("@Askvol1", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol2", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol3", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol4", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol5", SqlDbType.NVarChar,50),
					new SqlParameter("@Bid1", SqlDbType.Decimal,9),
					new SqlParameter("@Bid2", SqlDbType.Decimal,9),
					new SqlParameter("@Bid3", SqlDbType.Decimal,9),
					new SqlParameter("@Bid4", SqlDbType.Decimal,9),
					new SqlParameter("@Bid5", SqlDbType.Decimal,9),
					new SqlParameter("@Bidvol1", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol2", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol3", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol4", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol5", SqlDbType.NVarChar,50),
					new SqlParameter("@Update", SqlDbType.NVarChar,50),
					new SqlParameter("@Time", SqlDbType.DateTime)};
            parameters[0].Value = model.Code;
            parameters[1].Value = model.Symbol;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Open;
            parameters[5].Value = model.High;
            parameters[6].Value = model.Low;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.Yestclose;
            parameters[10].Value = model.Percent;
            parameters[11].Value = model.Updown;
            parameters[12].Value = model.Arrow;
            parameters[13].Value = model.Volume;
            parameters[14].Value = model.Turnover;
            parameters[15].Value = model.Ask1;
            parameters[16].Value = model.Ask2;
            parameters[17].Value = model.Ask3;
            parameters[18].Value = model.Ask4;
            parameters[19].Value = model.Ask5;
            parameters[20].Value = model.Askvol1;
            parameters[21].Value = model.Askvol2;
            parameters[22].Value = model.Askvol3;
            parameters[23].Value = model.Askvol4;
            parameters[24].Value = model.Askvol5;
            parameters[25].Value = model.Bid1;
            parameters[26].Value = model.Bid2;
            parameters[27].Value = model.Bid3;
            parameters[28].Value = model.Bid4;
            parameters[29].Value = model.Bid5;
            parameters[30].Value = model.Bidvol1;
            parameters[31].Value = model.Bidvol2;
            parameters[32].Value = model.Bidvol3;
            parameters[33].Value = model.Bidvol4;
            parameters[34].Value = model.Bidvol5;
            parameters[35].Value = model.Update;
            parameters[36].Value = model.Time;

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
        public bool Update(StockMinInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update StockMinInfo set ");
            strSql.Append("[code]=@code,");
            strSql.Append("[Symbol]=@Symbol,");
            strSql.Append("[Name]=@Name,");
            strSql.Append("[Type]=@Type,");
            strSql.Append("[Open]=@Open,");
            strSql.Append("[high]=@high,");
            strSql.Append("[Low]=@Low,");
            strSql.Append("[Status]=@Status,");
            strSql.Append("[Price]=@Price,");
            strSql.Append("[Yestclose]=@Yestclose,");
            strSql.Append("[Percent]=@Percent,");
            strSql.Append("[Updown]=@Updown,");
            strSql.Append("[Arrow]=@Arrow,");
            strSql.Append("[Volume]=@Volume,");
            strSql.Append("[Turnover]=@Turnover,");
            strSql.Append("[Ask1]=@Ask1,");
            strSql.Append("[Ask2]=@Ask2,");
            strSql.Append("[Ask3]=@Ask3,");
            strSql.Append("[Ask4]=@Ask4,");
            strSql.Append("[Ask5]=@Ask5,");
            strSql.Append("[Askvol1]=@Askvol1,");
            strSql.Append("[Askvol2]=@Askvol2,");
            strSql.Append("[Askvol3]=@Askvol3,");
            strSql.Append("[Askvol4]=@Askvol4,");
            strSql.Append("[Askvol5]=@Askvol5,");
            strSql.Append("[Bid1]=@Bid1,");
            strSql.Append("[Bid2]=@Bid2,");
            strSql.Append("[Bid3]=@Bid3,");
            strSql.Append("[Bid4]=@Bid4,");
            strSql.Append("[Bid5]=@Bid5,");
            strSql.Append("[Bidvol1]=@Bidvol1,");
            strSql.Append("[Bidvol2]=@Bidvol2,");
            strSql.Append("[Bidvol3]=@Bidvol3,");
            strSql.Append("[Bidvol4]=@Bidvol4,");
            strSql.Append("[Bidvol5]=@Bidvol5,");
            strSql.Append("[Update]=@Update,");
            strSql.Append("[Time]=@Time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@Symbol", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.VarChar,-1),
					new SqlParameter("@Type", SqlDbType.NChar,10),
					new SqlParameter("@Open", SqlDbType.Decimal,9),
					new SqlParameter("@high", SqlDbType.Decimal,9),
					new SqlParameter("@Low", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Yestclose", SqlDbType.Decimal,9),
					new SqlParameter("@Percent", SqlDbType.Float,8),
					new SqlParameter("@Updown", SqlDbType.Float,8),
					new SqlParameter("@Arrow", SqlDbType.NChar,10),
					new SqlParameter("@Volume", SqlDbType.NVarChar,50),
					new SqlParameter("@Turnover", SqlDbType.Decimal,9),
					new SqlParameter("@Ask1", SqlDbType.Decimal,9),
					new SqlParameter("@Ask2", SqlDbType.Decimal,9),
					new SqlParameter("@Ask3", SqlDbType.Decimal,9),
					new SqlParameter("@Ask4", SqlDbType.Decimal,9),
					new SqlParameter("@Ask5", SqlDbType.Decimal,9),
					new SqlParameter("@Askvol1", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol2", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol3", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol4", SqlDbType.NVarChar,50),
					new SqlParameter("@Askvol5", SqlDbType.NVarChar,50),
					new SqlParameter("@Bid1", SqlDbType.Decimal,9),
					new SqlParameter("@Bid2", SqlDbType.Decimal,9),
					new SqlParameter("@Bid3", SqlDbType.Decimal,9),
					new SqlParameter("@Bid4", SqlDbType.Decimal,9),
					new SqlParameter("@Bid5", SqlDbType.Decimal,9),
					new SqlParameter("@Bidvol1", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol2", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol3", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol4", SqlDbType.NVarChar,50),
					new SqlParameter("@Bidvol5", SqlDbType.NVarChar,50),
					new SqlParameter("@Update", SqlDbType.NVarChar,50),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.Code;
            parameters[1].Value = model.Symbol;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Open;
            parameters[5].Value = model.High;
            parameters[6].Value = model.Low;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.Yestclose;
            parameters[10].Value = model.Percent;
            parameters[11].Value = model.Updown;
            parameters[12].Value = model.Arrow;
            parameters[13].Value = model.Volume;
            parameters[14].Value = model.Turnover;
            parameters[15].Value = model.Ask1;
            parameters[16].Value = model.Ask2;
            parameters[17].Value = model.Ask3;
            parameters[18].Value = model.Ask4;
            parameters[19].Value = model.Ask5;
            parameters[20].Value = model.Askvol1;
            parameters[21].Value = model.Askvol2;
            parameters[22].Value = model.Askvol3;
            parameters[23].Value = model.Askvol4;
            parameters[24].Value = model.Askvol5;
            parameters[25].Value = model.Bid1;
            parameters[26].Value = model.Bid2;
            parameters[27].Value = model.Bid3;
            parameters[28].Value = model.Bid4;
            parameters[29].Value = model.Bid5;
            parameters[30].Value = model.Bidvol1;
            parameters[31].Value = model.Bidvol2;
            parameters[32].Value = model.Bidvol3;
            parameters[33].Value = model.Bidvol4;
            parameters[34].Value = model.Bidvol5;
            parameters[35].Value = model.Update;
            parameters[36].Value = model.Time;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockMinInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockMinInfo ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public StockMinInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 [code],[Symbol],[Name],[Type],[Open],[high],[Low],[Status],[Price],[Yestclose],[Percent],[Updown],[Arrow],[Volume],[Turnover],[Ask1],[Ask2],[Ask3],[Ask4],[Ask5],[Askvol1],[Askvol2],[Askvol3],[Askvol4],[Askvol5],[Bid1],[Bid2],[Bid3],[Bid4],[Bid5],[Bidvol1],[Bidvol2],[Bidvol3],[Bidvol4],[Bidvol5],[Update],[Time] from StockMinInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            StockMinInfo model = new StockMinInfo();
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
        public StockMinInfo DataRowToModel(DataRow row)
        {
            StockMinInfo model = new StockMinInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["code"] != null && row["code"].ToString() != "")
                {
                    model.Code = int.Parse(row["code"].ToString());
                }
                if (row["Symbol"] != null && row["Symbol"].ToString() != "")
                {
                    model.Symbol = row["Symbol"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["Open"] != null && row["Open"].ToString() != "")
                {
                    model.Open = decimal.Parse(row["Open"].ToString());
                }
                if (row["high"] != null && row["high"].ToString() != "")
                {
                    model.High = decimal.Parse(row["high"].ToString());
                }
                if (row["Low"] != null && row["Low"].ToString() != "")
                {
                    model.Low = decimal.Parse(row["Low"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["Yestclose"] != null && row["Yestclose"].ToString() != "")
                {
                    model.Yestclose = decimal.Parse(row["Yestclose"].ToString());
                }
                if (row["Percent"] != null && row["Percent"].ToString() != "")
                {
                    model.Percent = decimal.Parse(row["Percent"].ToString());
                }
                if (row["Updown"] != null && row["Updown"].ToString() != "")
                {
                    model.Updown = decimal.Parse(row["Updown"].ToString());
                }
                if (row["Arrow"] != null)
                {
                    model.Arrow = row["Arrow"].ToString();
                }
                if (row["Volume"] != null)
                {
                    model.Volume = row["Volume"].ToString();
                }
                if (row["Turnover"] != null && row["Turnover"].ToString() != "")
                {
                    model.Turnover = decimal.Parse(row["Turnover"].ToString());
                }
                if (row["Ask1"] != null && row["Ask1"].ToString() != "")
                {
                    model.Ask1 = decimal.Parse(row["Ask1"].ToString());
                }
                if (row["Ask2"] != null && row["Ask2"].ToString() != "")
                {
                    model.Ask2 = decimal.Parse(row["Ask2"].ToString());
                }
                if (row["Ask3"] != null && row["Ask3"].ToString() != "")
                {
                    model.Ask3 = decimal.Parse(row["Ask3"].ToString());
                }
                if (row["Ask4"] != null && row["Ask4"].ToString() != "")
                {
                    model.Ask4 = decimal.Parse(row["Ask4"].ToString());
                }
                if (row["Ask5"] != null && row["Ask5"].ToString() != "")
                {
                    model.Ask5 = decimal.Parse(row["Ask5"].ToString());
                }
                if (row["Askvol1"] != null)
                {
                    model.Askvol1 = row["Askvol1"].ToString();
                }
                if (row["Askvol2"] != null)
                {
                    model.Askvol2 = row["Askvol2"].ToString();
                }
                if (row["Askvol3"] != null)
                {
                    model.Askvol3 = row["Askvol3"].ToString();
                }
                if (row["Askvol4"] != null)
                {
                    model.Askvol4 = row["Askvol4"].ToString();
                }
                if (row["Askvol5"] != null)
                {
                    model.Askvol5 = row["Askvol5"].ToString();
                }
                if (row["Bid1"] != null && row["Bid1"].ToString() != "")
                {
                    model.Bid1 = decimal.Parse(row["Bid1"].ToString());
                }
                if (row["Bid2"] != null && row["Bid2"].ToString() != "")
                {
                    model.Bid2 = decimal.Parse(row["Bid2"].ToString());
                }
                if (row["Bid3"] != null && row["Bid3"].ToString() != "")
                {
                    model.Bid3 = decimal.Parse(row["Bid3"].ToString());
                }
                if (row["Bid4"] != null && row["Bid4"].ToString() != "")
                {
                    model.Bid4 = decimal.Parse(row["Bid4"].ToString());
                }
                if (row["Bid5"] != null && row["Bid5"].ToString() != "")
                {
                    model.Bid5 = decimal.Parse(row["Bid5"].ToString());
                }
                if (row["Bidvol1"] != null)
                {
                    model.Bidvol1 = row["Bidvol1"].ToString();
                }
                if (row["Bidvol2"] != null)
                {
                    model.Bidvol2 = row["Bidvol2"].ToString();
                }
                if (row["Bidvol3"] != null)
                {
                    model.Bidvol3 = row["Bidvol3"].ToString();
                }
                if (row["Bidvol4"] != null)
                {
                    model.Bidvol4 = row["Bidvol4"].ToString();
                }
                if (row["Bidvol5"] != null)
                {
                    model.Bidvol5 = row["Bidvol5"].ToString();
                }
                if (row["Update"] != null && row["Update"].ToString() != "")
                {
                    model.Update = row["Update"].ToString();
                }
                if (row["Time"] != null && row["Time"].ToString() != "")
                {
                    model.Time = DateTime.Parse(row["Time"].ToString());
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
            strSql.Append("select [code],[Symbol],[Name],[Type],[Open],[high],[Low],[Status],[Price],[Yestclose],[Percent],[Updown],[Arrow],[Volume],[Turnover],[Ask1],[Ask2],[Ask3],[Ask4],[Ask5],[Askvol1],[Askvol2],[Askvol3],[Askvol4],[Askvol5],[Bid1],[Bid2],[Bid3],[Bid4],[Bid5],[Bidvol1],[Bidvol2],[Bidvol3],[Bidvol4],[Bidvol5],[Update],[Time] ");
            strSql.Append(" FROM StockMinInfo ");
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
            strSql.Append(" [code],[Symbol],[Name],[Type],[Open],[high],[Low],[Status],[Price],[Yestclose],[Percent],[Updown],[Arrow],[Volume],[Turnover],[Ask1],[Ask2],[Ask3],[Ask4],[Ask5],[Askvol1],[Askvol2],[Askvol3],[Askvol4],[Askvol5],[Bid1],[Bid2],[Bid3],[Bid4],[Bid5],[Bidvol1],[Bidvol2],[Bidvol3],[Bidvol4],[Bidvol5],[Update],[Time] ");
            strSql.Append(" FROM StockMinInfo ");
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
            strSql.Append("select count(1) FROM StockMinInfo ");
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from StockMinInfo T ");
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
        public List<StockMinInfo> DataTableToList(DataTable dt)
        {
            List<StockMinInfo> modelList = new List<StockMinInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                StockMinInfo model;
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
            parameters[0].Value = "StockMinInfo";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
        #region  ExtensionMethod

        #endregion  ExtensionMethod

    }
}
