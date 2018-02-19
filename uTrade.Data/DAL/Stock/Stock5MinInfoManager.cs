using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Maticsoft.DBUtility;

namespace uTrade.Data
{
    class Stock5MinInfoManager
    {
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Symbol", "Stock5MinInfo");
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Stock5MinInfo ");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return (rows > 0);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Stock5MinInfo");
			strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Stock5MinInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Stock5MinInfo(");
			strSql.Append("Symbol,Time,[open],[close],High,Low,Volume,Turnover,UpNum,DownNum)");
			strSql.Append(" values (");
			strSql.Append("@Symbol,@Time,@open,@Close,@High,@Low,@Volume,@Turnover,@UpNum,@DownNum)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@Open", SqlDbType.Decimal,9),
					new SqlParameter("@Close", SqlDbType.Decimal,9),
					new SqlParameter("@High", SqlDbType.Decimal,9),
					new SqlParameter("@Low", SqlDbType.Decimal,9),
					new SqlParameter("@Volume", SqlDbType.NVarChar,50),
					new SqlParameter("@Turnover", SqlDbType.NVarChar,50),
					new SqlParameter("@UpNum", SqlDbType.NVarChar,50),
					new SqlParameter("@DownNum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.StockCode;
			parameters[1].Value = model.Time;
			parameters[2].Value = model.open;
			parameters[3].Value = model.Close;
			parameters[4].Value = model.High;
			parameters[5].Value = model.Low;
			parameters[6].Value = model.Volume;
			parameters[7].Value = model.Turnover;
			parameters[8].Value = model.UpNum;
			parameters[9].Value = model.DownNum;

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
		public bool Update(Stock5MinInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Stock5MinInfo set ");
			strSql.Append("Symbol=@Symbol,");
			strSql.Append("Time=@Time,");
			strSql.Append("[open]=@open,");
			strSql.Append("[close]=@Close,");
			strSql.Append("High=@High,");
			strSql.Append("Low=@Low,");
			strSql.Append("Volume=@Volume,");
			strSql.Append("Turnover=@Turnover,");
			strSql.Append("UpNum=@UpNum,");
			strSql.Append("DownNum=@DownNum");
			strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@open", SqlDbType.Decimal,9),
					new SqlParameter("@Close", SqlDbType.Decimal,9),
					new SqlParameter("@High", SqlDbType.Decimal,9),
					new SqlParameter("@Low", SqlDbType.Decimal,9),
					new SqlParameter("@Volume", SqlDbType.NVarChar,50),
					new SqlParameter("@Turnover", SqlDbType.NVarChar,50),
					new SqlParameter("@UpNum", SqlDbType.NVarChar,50),
					new SqlParameter("@DownNum", SqlDbType.NVarChar,50),
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)};
			parameters[0].Value = model.StockCode;
			parameters[1].Value = model.Time;
			parameters[2].Value = model.open;
			parameters[3].Value = model.Close;
			parameters[4].Value = model.High;
			parameters[5].Value = model.Low;
			parameters[6].Value = model.Volume;
			parameters[7].Value = model.Turnover;
			parameters[8].Value = model.UpNum;
			parameters[9].Value = model.DownNum;
			parameters[10].Value = model.id;

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
		public bool Delete(string Symbol)

        {
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Stock5MinInfo ");
			strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
			parameters[0].Value = Symbol;

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
		public bool DeleteList(string SymbolList)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Stock5MinInfo ");
			strSql.Append(" where Symbol in (" + SymbolList + ")  ");
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
		public Stock5MinInfo GetModel(string Symbol)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Symbol,Time,[open],[close],High,Low,Volume,Turnover,UpNum,DownNum from Stock5MinInfo ");
			strSql.Append(" where Symbol=@Symbol");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)
			};
			parameters[0].Value = Symbol;

			Stock5MinInfo model=new Stock5MinInfo();
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
		public Stock5MinInfo DataRowToModel(DataRow row)
		{
			Stock5MinInfo model=new Stock5MinInfo();
			if (row != null)
			{
				if(row["Symbol"] !=null)
				{
					model.StockCode=row["Symbol"].ToString();
				}
				if(row["Time"]!=null && row["Time"].ToString()!="")
				{
					model.Time=DateTime.Parse(row["Time"].ToString());
				}
				if(row["open"]!=null && row["open"].ToString()!="")
				{
					model.open=decimal.Parse(row["open"].ToString());
				}
				if(row["Close"]!=null && row["Close"].ToString()!="")
				{
					model.Close=decimal.Parse(row["Close"].ToString());
				}
				if(row["High"]!=null && row["High"].ToString()!="")
				{
					model.High=decimal.Parse(row["High"].ToString());
				}
				if(row["Low"]!=null && row["Low"].ToString()!="")
				{
					model.Low=decimal.Parse(row["Low"].ToString());
				}
				if(row["Volume"]!=null)
				{
					model.Volume=row["Volume"].ToString();
				}
				if(row["Turnover"]!=null)
				{
					model.Turnover=row["Turnover"].ToString();
				}
				if(row["UpNum"]!=null)
				{
					model.UpNum=row["UpNum"].ToString();
				}
				if(row["DownNum"]!=null)
				{
					model.DownNum=row["DownNum"].ToString();
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
			strSql.Append("select Symbol,Time,[open],[close],High,Low,Volume,Turnover,UpNum,DownNum ");
			strSql.Append(" FROM Stock5MinInfo ");
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
			strSql.Append(" Symbol,Time,[open],[close],High,Low,Volume,Turnover,UpNum,DownNum ");
			strSql.Append(" FROM Stock5MinInfo ");
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
			strSql.Append("select count(1) FROM Stock5MinInfo ");
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
				strSql.Append("order by T.Symbol desc");
			}
			strSql.Append(")AS Row, T.*  from Stock5MinInfo T ");
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
			parameters[0].Value = "Stock5MinInfo";
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
