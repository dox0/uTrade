using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Maticsoft.DBUtility;

namespace uTrade.Data
{

    /// <summary>
    /// 数据访问类:FavoriateModel
    /// </summary>
    public class FavoriateInfoManager
    {

        static FavoriateInfoManager inst = null;
        public static FavoriateInfoManager Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new FavoriateInfoManager();
                }
                return inst;
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Symbol", "tbl_Favoriate");
        }

        public static bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_Favoriate ");

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
            strSql.Append("select count(1) from tbl_Favoriate");
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
        public int Add(FavoriateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_Favoriate(");
            strSql.Append("Symbol,Type,AddTime,Reserve)");
            strSql.Append(" values (");
            strSql.Append("@Symbol,@Type,@AddTime,@Reserve)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
                new SqlParameter("@Type", SqlDbType.Int,50),
                new SqlParameter("@AddTime", SqlDbType.Date,20),
                new SqlParameter("@Reserve", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Symbol;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.AddTime;
            parameters[3].Value = model.Reserve;

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
        public bool Update(FavoriateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_Favoriate set ");
            strSql.Append("Type=@Type,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("Reserve=@Reserve,");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
                new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
                new SqlParameter("@Type", SqlDbType.Int,50),
                new SqlParameter("@AddTime", SqlDbType.Date,20),
                new SqlParameter("@Reserve", SqlDbType.NVarChar,50)};

            parameters[0].Value = model.Symbol;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.AddTime;
            parameters[3].Value = model.Reserve;

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
        public bool Delete(string strSymbol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_Favoriate ");
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
            strSql.Append("delete from tbl_Favoriate ");
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
        public FavoriateModel GetModel(string strSymbol)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Symbol,Type,AddTime,Reserve from tbl_Favoriate ");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
                new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
        };
            parameters[0].Value = strSymbol;

            FavoriateModel model = new FavoriateModel();
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
        public FavoriateModel DataRowToModel(DataRow row)
        {
            FavoriateModel model = new FavoriateModel();
            if (row != null)
            {
                if (row["Symbol"] != null && row["Symbol"].ToString() != "")
                {
                    model.Symbol = row["Symbol"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = (TradeType)Enum.ToObject(typeof(TradeType),int.Parse(row["Type"].ToString())) ;
                }
                if (row["AddTime"] != null)
                {
                    model.AddTime = Convert.ToDateTime(row["AddTime"].ToString());
                }
                if (row["Reserve"] != null)
                {
                    model.Reserve = row["Reserve"].ToString();
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
            strSql.Append("select Symbol,Type,AddTime,Reserve ");
            strSql.Append(" FROM tbl_Favoriate ");
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
            strSql.Append(" Symbol,Type,AddTime,Reserve ");
            strSql.Append(" FROM tbl_Favoriate ");
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
            strSql.Append("select count(1) FROM tbl_Favoriate ");
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

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FavoriateModel> DataTableToList(DataTable dt)
        {
            List<FavoriateModel> modelList = new List<FavoriateModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FavoriateModel model;
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
    }
}

