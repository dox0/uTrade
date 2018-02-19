using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Maticsoft.DBUtility;


namespace uTrade.Data
{
	/// <summary>
	/// 数据访问类:Rank
	/// </summary>
	public partial class RankDAL
	{
        private RankDAL()
        { }
        static RankDAL inst = null;
        public static RankDAL Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new RankDAL();
                }
                return inst;
            }
        }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
		{
		    return DbHelperSQL.GetMaxID("Symbol", "tbl_fund_RankInfo"); 
		}

        public static bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_fund_RankInfo ");
            
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tbl_fund_RankInfo");
            strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = strSymbol;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Rank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tbl_fund_RankInfo(");
            strSql.Append("Symbol,Name,Shorthand,CurDate,NetAssetValue,AccumulatedNet,DailyGrowth,LastWeek,LastMonth,Last3Month,Last6Month,LastYear,Last2Year,Last3Year,ThisYear,SinceBuilt,BuiltDate,Custom,Poundage,PoundageCount,Favorite)");
			strSql.Append(" values (");
            strSql.Append("@Symbol,@Name,@Shorthand,@CurDate,@NetAssetValue,@AccumulatedNet,@DailyGrowth,@LastWeek,@LastMonth,@Last3Month,@Last6Month,@LastYear,@Last2Year,@Last3Year,@ThisYear,@SinceBuilt,@BuiltDate,@Custom,@Poundage,@PoundageCount,@Favorite)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Shorthand", SqlDbType.NVarChar,50),
					new SqlParameter("@CurDate", SqlDbType.NVarChar,20),
					new SqlParameter("@NetAssetValue", SqlDbType.Decimal,10),
					new SqlParameter("@AccumulatedNet", SqlDbType.Decimal,10),
					new SqlParameter("@DailyGrowth", SqlDbType.Decimal,10),
					new SqlParameter("@LastWeek", SqlDbType.Decimal,10),
					new SqlParameter("@LastMonth", SqlDbType.Decimal,10),
					new SqlParameter("@Last3Month", SqlDbType.Decimal,10),
					new SqlParameter("@Last6Month", SqlDbType.Decimal,10),
					new SqlParameter("@LastYear", SqlDbType.Decimal,10),
					new SqlParameter("@Last2Year", SqlDbType.Decimal,10),
					new SqlParameter("@Last3Year", SqlDbType.Decimal,10),
					new SqlParameter("@ThisYear", SqlDbType.Decimal,10),
					new SqlParameter("@SinceBuilt", SqlDbType.Decimal,10),
					new SqlParameter("@BuiltDate", SqlDbType.NVarChar,20),
					new SqlParameter("@Custom", SqlDbType.NVarChar,20),
					new SqlParameter("@Poundage", SqlDbType.NVarChar,20),
                    new SqlParameter("@PoundageCount", SqlDbType.NVarChar,20),
                    new SqlParameter("@Favorite", SqlDbType.Int, 4)};
        parameters[0].Value = model.Symbol;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Shorthand;
            parameters[3].Value = model.CurDate;
            parameters[4].Value = model.NetAssetValue;
            parameters[5].Value = model.AccumulatedNet;
            parameters[6].Value = model.DailyGrowth;
            parameters[7].Value = model.LastWeek;
            parameters[8].Value = model.LastMonth;
            parameters[9].Value = model.Last3Month;
            parameters[10].Value = model.Last6Month;
            parameters[11].Value = model.LastYear;
            parameters[12].Value = model.Last2Year;
            parameters[13].Value = model.Last3Year;
            parameters[14].Value = model.ThisYear;
            parameters[15].Value = model.SinceBuilt;
            parameters[16].Value = model.BuiltDate;
            parameters[17].Value = model.Custom;
            parameters[18].Value = model.Poundage;
            parameters[19].Value = model.PoundageCount;
            parameters[20].Value = model.Favorite;

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
        public bool Update(Rank model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update tbl_fund_RankInfo set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Shorthand=@Shorthand,");
            strSql.Append("CurDate=@CurDate,");
            strSql.Append("NetAssetValue=@NetAssetValue,");
            strSql.Append("AccumulatedNet=@AccumulatedNet,");
            strSql.Append("DailyGrowth=@DailyGrowth,");
            strSql.Append("LastWeek=@LastWeek,");
            strSql.Append("LastMonth=@LastMonth,");
            strSql.Append("Last3Month=@Last3Month,");
            strSql.Append("Last6Month=@Last6Month,");
            strSql.Append("LastYear=@LastYear,");
            strSql.Append("Last2Year=@Last2Year,");
            strSql.Append("Last3Year=@Last3Year,");
            strSql.Append("ThisYear=@ThisYear,");
            strSql.Append("SinceBuilt=@SinceBuilt,");
            strSql.Append("BuiltDate=@BuiltDate,");
            strSql.Append("Custom=@Custom,");
            strSql.Append("Poundage=@Poundage,");
            strSql.Append("PoundageCount=@PoundageCount,");
            strSql.Append("Favorite=@Favorite");
            strSql.Append(" where Symbol=@Symbol");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Shorthand", SqlDbType.NVarChar,50),
					new SqlParameter("@CurDate", SqlDbType.NVarChar,20),
					new SqlParameter("@NetAssetValue", SqlDbType.Decimal,10),
					new SqlParameter("@AccumulatedNet", SqlDbType.Decimal,10),
					new SqlParameter("@DailyGrowth", SqlDbType.Decimal,10),
					new SqlParameter("@LastWeek", SqlDbType.Decimal,10),
					new SqlParameter("@LastMonth", SqlDbType.Decimal,10),
					new SqlParameter("@Last3Month", SqlDbType.Decimal,10),
					new SqlParameter("@Last6Month", SqlDbType.Decimal,10),
					new SqlParameter("@LastYear", SqlDbType.Decimal,10),
					new SqlParameter("@Last2Year", SqlDbType.Decimal,10),
					new SqlParameter("@Last3Year", SqlDbType.Decimal,10),
					new SqlParameter("@ThisYear", SqlDbType.Decimal,10),
					new SqlParameter("@SinceBuilt", SqlDbType.Decimal,10),
					new SqlParameter("@BuiltDate", SqlDbType.NVarChar,20),
					new SqlParameter("@Custom", SqlDbType.NVarChar,20),
					new SqlParameter("@Poundage", SqlDbType.NVarChar,20),
					new SqlParameter("@PoundageCount", SqlDbType.NVarChar,20),
                    new SqlParameter("@Favorite", SqlDbType.Int, 4)};

            parameters[0].Value = model.Symbol;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Shorthand;
            parameters[3].Value = model.CurDate;
            parameters[4].Value = model.NetAssetValue;
            parameters[5].Value = model.AccumulatedNet;
            parameters[6].Value = model.DailyGrowth;
            parameters[7].Value = model.LastWeek;
            parameters[8].Value = model.LastMonth;
            parameters[9].Value = model.Last3Month;
            parameters[10].Value = model.Last6Month;
            parameters[11].Value = model.LastYear;
            parameters[12].Value = model.Last2Year;
            parameters[13].Value = model.Last3Year;
            parameters[14].Value = model.ThisYear;
            parameters[15].Value = model.SinceBuilt;
            parameters[16].Value = model.BuiltDate;
            parameters[17].Value = model.Custom;
            parameters[18].Value = model.Poundage;
            parameters[19].Value = model.PoundageCount;
            parameters[20].Value = model.Favorite;


            int rows =DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tbl_fund_RankInfo ");
            strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = strSymbol;

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
		public bool DeleteList(string strSymbollist)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tbl_fund_RankInfo ");
            strSql.Append(" where Symbol in (" + strSymbollist + ")  ");
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
        public Rank GetModel(string strSymbol)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Symbol,Name,Shorthand,CurDate,NetAssetValue,AccumulatedNet,DailyGrowth,LastWeek,LastMonth,Last3Month,Last6Month,LastYear,Last2Year,Last3Year,ThisYear,SinceBuilt,BuiltDate,Custom,Poundage,PoundageCount,Favorite from tbl_fund_RankInfo ");
            strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
            parameters[0].Value = strSymbol;

            Rank model = new Rank();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(),parameters);
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
        public Rank DataRowToModel(DataRow row)
		{
            Rank model = new Rank();
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
                if (row["Shorthand"] != null)
				{
                    model.Shorthand = row["Shorthand"].ToString();
				}
                if (row["CurDate"] != null)
				{
                    model.CurDate = row["CurDate"].ToString();
				}
                if (row["NetAssetValue"] != null && row["NetAssetValue"].ToString() != "")
				{
                    model.NetAssetValue = decimal.Parse(row["NetAssetValue"].ToString());
				}
                if (row["AccumulatedNet"] != null)
                {
                    model.AccumulatedNet = decimal.Parse(row["AccumulatedNet"].ToString());
                }
                if (row["DailyGrowth"] != null)
                {
                    model.DailyGrowth = decimal.Parse(row["DailyGrowth"].ToString());
                }
                if (row["LastWeek"] != null)
                {
                    model.LastWeek = decimal.Parse(row["LastWeek"].ToString());
                }
                if (row["LastMonth"] != null)
                {
                    model.LastMonth = decimal.Parse(row["LastMonth"].ToString());
                }
                if (row["Last3Month"] != null)
                {
                    model.Last3Month = decimal.Parse(row["Last3Month"].ToString());
                }
                if (row["Last6Month"] != null)
                {
                    model.Last6Month = decimal.Parse(row["Last6Month"].ToString());
                }
                if (row["LastYear"] != null)
                {
                    model.LastYear = decimal.Parse(row["LastYear"].ToString());
                }
                if (row["Last2Year"] != null)
                {
                    model.Last2Year = decimal.Parse(row["Last2Year"].ToString());
                }
                if (row["Last3Year"] != null)
                {
                    model.Last3Year = decimal.Parse(row["Last3Year"].ToString());
                }
                if (row["ThisYear"] != null)
                {
                    model.ThisYear = decimal.Parse(row["ThisYear"].ToString());
                }
                if (row["SinceBuilt"] != null)
                {
                    model.SinceBuilt = decimal.Parse(row["SinceBuilt"].ToString());
                }
                if (row["BuiltDate"] != null)
                {
                    model.BuiltDate = row["BuiltDate"].ToString();
                }
                if (row["Custom"] != null)
                {
                    model.Custom = row["Custom"].ToString();
                }
                if (row["Poundage"] != null)
                {
                    model.Poundage = row["Poundage"].ToString();
                }
                if (row["PoundageCount"] != null)
                {
                    model.PoundageCount = row["PoundageCount"].ToString();
                }
                if (row["Favorite"] != null)
                {
                    model.Favorite = int.Parse(row["Favorite"].ToString());
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
            strSql.Append("select Symbol,Name,Shorthand,CurDate,NetAssetValue,AccumulatedNet,DailyGrowth,LastWeek,LastMonth,Last3Month,Last6Month,LastYear,Last2Year,Last3Year,ThisYear,SinceBuilt,BuiltDate,Custom,Poundage,PoundageCount,Favorite ");
			strSql.Append(" FROM tbl_fund_RankInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top, string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" Symbol,Name,Shorthand,CurDate,NetAssetValue,AccumulatedNet,DailyGrowth,LastWeek,LastMonth,Last3Month,Last6Month,LastYear,Last2Year,Last3Year,ThisYear,SinceBuilt,BuiltDate,Custom,Poundage,PoundageCount,Favorite ");
			strSql.Append(" FROM tbl_fund_RankInfo ");
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
			strSql.Append("select count(1) FROM tbl_fund_RankInfo ");
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
        public List<Rank> DataTableToList(DataTable dt)
        {
            List<Rank> modelList = new List<Rank>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Rank model;
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
        #endregion  BasicMethod

        public DataSet GetFavoriateList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" fav.Symbol,Name,Shorthand,CurDate,NetAssetValue,AccumulatedNet,DailyGrowth,LastWeek,LastMonth,Last3Month,Last6Month,LastYear,Last2Year,Last3Year,ThisYear,SinceBuilt,BuiltDate,Custom,Poundage,PoundageCount,Favorite ");
            strSql.Append(" from tbl_Favoriate fav left join tbl_fund_RankInfo rank on (fav.Symbol = rank.Symbol)");
            strSql.Append(" where fav.Type = " + ((int)TradeType.Fund).ToString());

            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}

