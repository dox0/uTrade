using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace uTrade.Data
{
    class StockZCFZInfoManager
    {
		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		    return DbHelperSQL.GetMaxID("Symbol", "StockZCFZInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Symbol)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from StockZCFZInfo");
			strSql.Append(" where Symbol=@Symbol ");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.Int,4)			};
			parameters[0].Value = Symbol;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        public int Add(StockZCFZInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StockZCFZInfo(");
            strSql.Append("Symbol,ReportDate,HBZJ,JSBFJ,CCZJ,JYXJRZC,YSJRZC,YSPJ,YSZK,YFKX,YSBF,YSFBZK,YSFBHTZBJ,YSLX,YSGL,QTYSK,YSCKTS,YSBTK,YSBZJ,NBYSK,MRFSJRZC,CH,DTFY,DCLLDZCSS,YNNDQDFLDZC,QTLDZC,LDZCHJ,FCDKJDK,KGCSJRZC,CYZDQTZ,CQYSK,CQGQTZ,QTCQTZ,TZXFDC,GDZCYZ,LJZJ,GDZCJZ,GDZCJZZB,GDZC,ZJGC,GCWZ,GDZCQL,SCXSWZC,GYXSWZC,QYZC,WXZC,KFZC,SY,CQDTFY,GQFZLTQ,DYSDSZC,QTFLDZC,FLDZCHJ,ZCZJ,DQJK,XZYYHJK,XSCKJTYCF,CRZJ,JYXJRFZ,YSJRFZ,YFPJ,YFZK,YuSZK,MCHGJRZCK,YFSXFJYJ,YFZGXC,YJSF,YFLX,YFGL,QTYJK,YFBZJ,NBYFK,QTYFK,YTFY,YJLDFZ,YFFBZK,BXHTZBJ,DLMMZQK,DLCXZQK,GJPZJS,GNPZJS,DYSY,YFDQZQ,YNDDQDFLDFZ,QTLDFZ,LDFZHJ,CQJQ,YFZQ,CQYFZQ,ZXYFK,YJFLDFZ,CQDYSY,DYSDSFZ,QTFLDFZ,FLDFZHJ,FZHJ,SSZB,ZBGJ,JKCG,ZXCB,YYGJ,YBFXZB,WQDDTZSS,WFPLR,NFPXJGL,WBBBZSCE,GSYMGSGDQYHJ,SSGDQY,SYZQY,FZHSYZQY)");
            strSql.Append(" values (");
            strSql.Append("@Symbol,@ReportDate,@HBZJ,@JSBFJ,@CCZJ,@JYXJRZC,@YSJRZC,@YSPJ,@YSZK,@YFKX,@YSBF,@YSFBZK,@YSFBHTZBJ,@YSLX,@YSGL,@QTYSK,@YSCKTS,@YSBTK,@YSBZJ,@NBYSK,@MRFSJRZC,@CH,@DTFY,@DCLLDZCSS,@YNNDQDFLDZC,@QTLDZC,@LDZCHJ,@FCDKJDK,@KGCSJRZC,@CYZDQTZ,@CQYSK,@CQGQTZ,@QTCQTZ,@TZXFDC,@GDZCYZ,@LJZJ,@GDZCJZ,@GDZCJZZB,@GDZC,@ZJGC,@GCWZ,@GDZCQL,@SCXSWZC,@GYXSWZC,@QYZC,@WXZC,@KFZC,@SY,@CQDTFY,@GQFZLTQ,@DYSDSZC,@QTFLDZC,@FLDZCHJ,@ZCZJ,@DQJK,@XZYYHJK,@XSCKJTYCF,@CRZJ,@JYXJRFZ,@YSJRFZ,@YFPJ,@YFZK,@YuSZK,@MCHGJRZCK,@YFSXFJYJ,@YFZGXC,@YJSF,@YFLX,@YFGL,@QTYJK,@YFBZJ,@NBYFK,@QTYFK,@YTFY,@YJLDFZ,@YFFBZK,@BXHTZBJ,@DLMMZQK,@DLCXZQK,@GJPZJS,@GNPZJS,@DYSY,@YFDQZQ,@YNDDQDFLDFZ,@QTLDFZ,@LDFZHJ,@CQJQ,@YFZQ,@CQYFZQ,@ZXYFK,@YJFLDFZ,@CQDYSY,@DYSDSFZ,@QTFLDFZ,@FLDFZHJ,@FZHJ,@SSZB,@ZBGJ,@JKCG,@ZXCB,@YYGJ,@YBFXZB,@WQDDTZSS,@WFPLR,@NFPXJGL,@WBBBZSCE,@GSYMGSGDQYHJ,@SSGDQY,@SYZQY,@FZHSYZQY)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,50),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@HBZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JSBFJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CCZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JYXJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@YSJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@YSPJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YSZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YFKX", SqlDbType.NVarChar,50),
					new SqlParameter("@YSBF", SqlDbType.NVarChar,50),
					new SqlParameter("@YSFBZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YSFBHTZBJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YSLX", SqlDbType.NVarChar,50),
					new SqlParameter("@YSGL", SqlDbType.NVarChar,50),
					new SqlParameter("@QTYSK", SqlDbType.NVarChar,50),
					new SqlParameter("@YSCKTS", SqlDbType.NVarChar,50),
					new SqlParameter("@YSBTK", SqlDbType.NVarChar,50),
					new SqlParameter("@YSBZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@NBYSK", SqlDbType.NVarChar,50),
					new SqlParameter("@MRFSJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@CH", SqlDbType.NVarChar,50),
					new SqlParameter("@DTFY", SqlDbType.NVarChar,50),
					new SqlParameter("@DCLLDZCSS", SqlDbType.NVarChar,50),
					new SqlParameter("@YNNDQDFLDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@QTLDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@LDZCHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@FCDKJDK", SqlDbType.NVarChar,50),
					new SqlParameter("@KGCSJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@CYZDQTZ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQYSK", SqlDbType.NVarChar,50),
					new SqlParameter("@CQGQTZ", SqlDbType.NVarChar,50),
					new SqlParameter("@QTCQTZ", SqlDbType.NVarChar,50),
					new SqlParameter("@TZXFDC", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCYZ", SqlDbType.NVarChar,50),
					new SqlParameter("@LJZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCJZ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCJZZB", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@ZJGC", SqlDbType.NVarChar,50),
					new SqlParameter("@GCWZ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCQL", SqlDbType.NVarChar,50),
					new SqlParameter("@SCXSWZC", SqlDbType.NVarChar,50),
					new SqlParameter("@GYXSWZC", SqlDbType.NVarChar,50),
					new SqlParameter("@QYZC", SqlDbType.NVarChar,50),
					new SqlParameter("@WXZC", SqlDbType.NVarChar,50),
					new SqlParameter("@KFZC", SqlDbType.NVarChar,50),
					new SqlParameter("@SY", SqlDbType.NVarChar,50),
					new SqlParameter("@CQDTFY", SqlDbType.NVarChar,50),
					new SqlParameter("@GQFZLTQ", SqlDbType.NVarChar,50),
					new SqlParameter("@DYSDSZC", SqlDbType.NVarChar,50),
					new SqlParameter("@QTFLDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@FLDZCHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZCZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@DQJK", SqlDbType.NVarChar,50),
					new SqlParameter("@XZYYHJK", SqlDbType.NVarChar,50),
					new SqlParameter("@XSCKJTYCF", SqlDbType.NVarChar,50),
					new SqlParameter("@CRZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JYXJRFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YSJRFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFPJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YuSZK", SqlDbType.NVarChar,50),
					new SqlParameter("@MCHGJRZCK", SqlDbType.NVarChar,50),
					new SqlParameter("@YFSXFJYJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFZGXC", SqlDbType.NVarChar,50),
					new SqlParameter("@YJSF", SqlDbType.NVarChar,50),
					new SqlParameter("@YFLX", SqlDbType.NVarChar,50),
					new SqlParameter("@YFGL", SqlDbType.NVarChar,50),
					new SqlParameter("@QTYJK", SqlDbType.NVarChar,50),
					new SqlParameter("@YFBZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@NBYFK", SqlDbType.NVarChar,50),
					new SqlParameter("@QTYFK", SqlDbType.NVarChar,50),
					new SqlParameter("@YTFY", SqlDbType.NVarChar,50),
					new SqlParameter("@YJLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFFBZK", SqlDbType.NVarChar,50),
					new SqlParameter("@BXHTZBJ", SqlDbType.NVarChar,50),
					new SqlParameter("@DLMMZQK", SqlDbType.NVarChar,50),
					new SqlParameter("@DLCXZQK", SqlDbType.NVarChar,50),
					new SqlParameter("@GJPZJS", SqlDbType.NVarChar,50),
					new SqlParameter("@GNPZJS", SqlDbType.NVarChar,50),
					new SqlParameter("@DYSY", SqlDbType.NVarChar,50),
					new SqlParameter("@YFDQZQ", SqlDbType.NVarChar,50),
					new SqlParameter("@YNDDQDFLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@QTLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@LDFZHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQJQ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFZQ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQYFZQ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXYFK", SqlDbType.NVarChar,50),
					new SqlParameter("@YJFLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQDYSY", SqlDbType.NVarChar,50),
					new SqlParameter("@DYSDSFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@QTFLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@FLDFZHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@FZHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@SSZB", SqlDbType.NVarChar,50),
					new SqlParameter("@ZBGJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JKCG", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXCB", SqlDbType.NVarChar,50),
					new SqlParameter("@YYGJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YBFXZB", SqlDbType.NVarChar,50),
					new SqlParameter("@WQDDTZSS", SqlDbType.NVarChar,50),
					new SqlParameter("@WFPLR", SqlDbType.NVarChar,50),
					new SqlParameter("@NFPXJGL", SqlDbType.NVarChar,50),
					new SqlParameter("@WBBBZSCE", SqlDbType.NVarChar,50),
					new SqlParameter("@GSYMGSGDQYHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@SSGDQY", SqlDbType.NVarChar,50),
					new SqlParameter("@SYZQY", SqlDbType.NVarChar,50),
					new SqlParameter("@FZHSYZQY", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Code;
            parameters[1].Value = model.ReportDate;
            parameters[2].Value = model.HBZJ;
            parameters[3].Value = model.JSBFJ;
            parameters[4].Value = model.CCZJ;
            parameters[5].Value = model.JYXJRZC;
            parameters[6].Value = model.YSJRZC;
            parameters[7].Value = model.YSPJ;
            parameters[8].Value = model.YSZK;
            parameters[9].Value = model.YFKX;
            parameters[10].Value = model.YSBF;
            parameters[11].Value = model.YSFBZK;
            parameters[12].Value = model.YSFBHTZBJ;
            parameters[13].Value = model.YSLX;
            parameters[14].Value = model.YSGL;
            parameters[15].Value = model.QTYSK;
            parameters[16].Value = model.YSCKTS;
            parameters[17].Value = model.YSBTK;
            parameters[18].Value = model.YSBZJ;
            parameters[19].Value = model.NBYSK;
            parameters[20].Value = model.MRFSJRZC;
            parameters[21].Value = model.CH;
            parameters[22].Value = model.DTFY;
            parameters[23].Value = model.DCLLDZCSS;
            parameters[24].Value = model.YNNDQDFLDZC;
            parameters[25].Value = model.QTLDZC;
            parameters[26].Value = model.LDZCHJ;
            parameters[27].Value = model.FCDKJDK;
            parameters[28].Value = model.KGCSJRZC;
            parameters[29].Value = model.CYZDQTZ;
            parameters[30].Value = model.CQYSK;
            parameters[31].Value = model.CQGQTZ;
            parameters[32].Value = model.QTCQTZ;
            parameters[33].Value = model.TZXFDC;
            parameters[34].Value = model.GDZCYZ;
            parameters[35].Value = model.LJZJ;
            parameters[36].Value = model.GDZCJZ;
            parameters[37].Value = model.GDZCJZZB;
            parameters[38].Value = model.GDZC;
            parameters[39].Value = model.ZJGC;
            parameters[40].Value = model.GCWZ;
            parameters[41].Value = model.GDZCQL;
            parameters[42].Value = model.SCXSWZC;
            parameters[43].Value = model.GYXSWZC;
            parameters[44].Value = model.QYZC;
            parameters[45].Value = model.WXZC;
            parameters[46].Value = model.KFZC;
            parameters[47].Value = model.SY;
            parameters[48].Value = model.CQDTFY;
            parameters[49].Value = model.GQFZLTQ;
            parameters[50].Value = model.DYSDSZC;
            parameters[51].Value = model.QTFLDZC;
            parameters[52].Value = model.FLDZCHJ;
            parameters[53].Value = model.ZCZJ;
            parameters[54].Value = model.DQJK;
            parameters[55].Value = model.XZYYHJK;
            parameters[56].Value = model.XSCKJTYCF;
            parameters[57].Value = model.CRZJ;
            parameters[58].Value = model.JYXJRFZ;
            parameters[59].Value = model.YSJRFZ;
            parameters[60].Value = model.YFPJ;
            parameters[61].Value = model.YFZK;
            parameters[62].Value = model.YuSZK;
            parameters[63].Value = model.MCHGJRZCK;
            parameters[64].Value = model.YFSXFJYJ;
            parameters[65].Value = model.YFZGXC;
            parameters[66].Value = model.YJSF;
            parameters[67].Value = model.YFLX;
            parameters[68].Value = model.YFGL;
            parameters[69].Value = model.QTYJK;
            parameters[70].Value = model.YFBZJ;
            parameters[71].Value = model.NBYFK;
            parameters[72].Value = model.QTYFK;
            parameters[73].Value = model.YTFY;
            parameters[74].Value = model.YJLDFZ;
            parameters[75].Value = model.YFFBZK;
            parameters[76].Value = model.BXHTZBJ;
            parameters[77].Value = model.DLMMZQK;
            parameters[78].Value = model.DLCXZQK;
            parameters[79].Value = model.GJPZJS;
            parameters[80].Value = model.GNPZJS;
            parameters[81].Value = model.DYSY;
            parameters[82].Value = model.YFDQZQ;
            parameters[83].Value = model.YNDDQDFLDFZ;
            parameters[84].Value = model.QTLDFZ;
            parameters[85].Value = model.LDFZHJ;
            parameters[86].Value = model.CQJQ;
            parameters[87].Value = model.YFZQ;
            parameters[88].Value = model.CQYFZQ;
            parameters[89].Value = model.ZXYFK;
            parameters[90].Value = model.YJFLDFZ;
            parameters[91].Value = model.CQDYSY;
            parameters[92].Value = model.DYSDSFZ;
            parameters[93].Value = model.QTFLDFZ;
            parameters[94].Value = model.FLDFZHJ;
            parameters[95].Value = model.FZHJ;
            parameters[96].Value = model.SSZB;
            parameters[97].Value = model.ZBGJ;
            parameters[98].Value = model.JKCG;
            parameters[99].Value = model.ZXCB;
            parameters[100].Value = model.YYGJ;
            parameters[101].Value = model.YBFXZB;
            parameters[102].Value = model.WQDDTZSS;
            parameters[103].Value = model.WFPLR;
            parameters[104].Value = model.NFPXJGL;
            parameters[105].Value = model.WBBBZSCE;
            parameters[106].Value = model.GSYMGSGDQYHJ;
            parameters[107].Value = model.SSGDQY;
            parameters[108].Value = model.SYZQY;
            parameters[109].Value = model.FZHSYZQY;

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
		public bool Update(StockZCFZInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update StockZCFZInfo set ");
			strSql.Append("Symbol=@Symbol,");
			strSql.Append("ReportDate=@ReportDate,");
			strSql.Append("HBZJ=@HBZJ,");
			strSql.Append("JSBFJ=@JSBFJ,");
			strSql.Append("CCZJ=@CCZJ,");
			strSql.Append("JYXJRZC=@JYXJRZC,");
			strSql.Append("YSJRZC=@YSJRZC,");
			strSql.Append("YSPJ=@YSPJ,");
			strSql.Append("YSZK=@YSZK,");
			strSql.Append("YFKX=@YFKX,");
			strSql.Append("YSBF=@YSBF,");
			strSql.Append("YSFBZK=@YSFBZK,");
			strSql.Append("YSFBHTZBJ=@YSFBHTZBJ,");
			strSql.Append("YSLX=@YSLX,");
			strSql.Append("YSGL=@YSGL,");
			strSql.Append("QTYSK=@QTYSK,");
			strSql.Append("YSCKTS=@YSCKTS,");
			strSql.Append("YSBTK=@YSBTK,");
			strSql.Append("YSBZJ=@YSBZJ,");
			strSql.Append("NBYSK=@NBYSK,");
			strSql.Append("MRFSJRZC=@MRFSJRZC,");
			strSql.Append("CH=@CH,");
			strSql.Append("DTFY=@DTFY,");
			strSql.Append("DCLLDZCSS=@DCLLDZCSS,");
			strSql.Append("YNNDQDFLDZC=@YNNDQDFLDZC,");
			strSql.Append("QTLDZC=@QTLDZC,");
			strSql.Append("LDZCHJ=@LDZCHJ,");
			strSql.Append("FCDKJDK=@FCDKJDK,");
			strSql.Append("KGCSJRZC=@KGCSJRZC,");
			strSql.Append("CYZDQTZ=@CYZDQTZ,");
			strSql.Append("CQYSK=@CQYSK,");
			strSql.Append("CQGQTZ=@CQGQTZ,");
			strSql.Append("QTCQTZ=@QTCQTZ,");
			strSql.Append("TZXFDC=@TZXFDC,");
			strSql.Append("GDZCYZ=@GDZCYZ,");
			strSql.Append("LJZJ=@LJZJ,");
			strSql.Append("GDZCJZ=@GDZCJZ,");
			strSql.Append("GDZCJZZB=@GDZCJZZB,");
			strSql.Append("GDZC=@GDZC,");
			strSql.Append("ZJGC=@ZJGC,");
			strSql.Append("GCWZ=@GCWZ,");
			strSql.Append("GDZCQL=@GDZCQL,");
			strSql.Append("SCXSWZC=@SCXSWZC,");
			strSql.Append("GYXSWZC=@GYXSWZC,");
			strSql.Append("QYZC=@QYZC,");
			strSql.Append("WXZC=@WXZC,");
			strSql.Append("KFZC=@KFZC,");
			strSql.Append("SY=@SY,");
			strSql.Append("CQDTFY=@CQDTFY,");
			strSql.Append("GQFZLTQ=@GQFZLTQ,");
			strSql.Append("DYSDSZC=@DYSDSZC,");
			strSql.Append("QTFLDZC=@QTFLDZC,");
			strSql.Append("FLDZCHJ=@FLDZCHJ,");
			strSql.Append("ZCZJ=@ZCZJ,");
			strSql.Append("DQJK=@DQJK,");
			strSql.Append("XZYYHJK=@XZYYHJK,");
			strSql.Append("XSCKJTYCF=@XSCKJTYCF,");
			strSql.Append("CRZJ=@CRZJ,");
			strSql.Append("JYXJRFZ=@JYXJRFZ,");
			strSql.Append("YSJRFZ=@YSJRFZ,");
			strSql.Append("YFPJ=@YFPJ,");
			strSql.Append("YFZK=@YFZK,");
			strSql.Append("YuSZK=@YuSZK,");
			strSql.Append("MCHGJRZCK=@MCHGJRZCK,");
			strSql.Append("YFSXFJYJ=@YFSXFJYJ,");
			strSql.Append("YFZGXC=@YFZGXC,");
			strSql.Append("YJSF=@YJSF,");
			strSql.Append("YFLX=@YFLX,");
			strSql.Append("YFGL=@YFGL,");
			strSql.Append("QTYJK=@QTYJK,");
			strSql.Append("YFBZJ=@YFBZJ,");
			strSql.Append("NBYFK=@NBYFK,");
			strSql.Append("QTYFK=@QTYFK,");
			strSql.Append("YTFY=@YTFY,");
			strSql.Append("YJLDFZ=@YJLDFZ,");
			strSql.Append("YFFBZK=@YFFBZK,");
			strSql.Append("BXHTZBJ=@BXHTZBJ,");
			strSql.Append("DLMMZQK=@DLMMZQK,");
			strSql.Append("DLCXZQK=@DLCXZQK,");
			strSql.Append("GJPZJS=@GJPZJS,");
			strSql.Append("GNPZJS=@GNPZJS,");
			strSql.Append("DYSY=@DYSY,");
			strSql.Append("YFDQZQ=@YFDQZQ,");
			strSql.Append("YNDDQDFLDFZ=@YNDDQDFLDFZ,");
			strSql.Append("QTLDFZ=@QTLDFZ,");
			strSql.Append("LDFZHJ=@LDFZHJ,");
			strSql.Append("CQJQ=@CQJQ,");
			strSql.Append("YFZQ=@YFZQ,");
			strSql.Append("CQYFZQ=@CQYFZQ,");
			strSql.Append("ZXYFK=@ZXYFK,");
			strSql.Append("YJFLDFZ=@YJFLDFZ,");
			strSql.Append("CQDYSY=@CQDYSY,");
			strSql.Append("DYSDSFZ=@DYSDSFZ,");
			strSql.Append("QTFLDFZ=@QTFLDFZ,");
			strSql.Append("FLDFZHJ=@FLDFZHJ,");
			strSql.Append("FZHJ=@FZHJ,");
			strSql.Append("SSZB=@SSZB,");
			strSql.Append("ZBGJ=@ZBGJ,");
			strSql.Append("JKCG=@JKCG,");
			strSql.Append("ZXCB=@ZXCB,");
			strSql.Append("YYGJ=@YYGJ,");
			strSql.Append("YBFXZB=@YBFXZB,");
			strSql.Append("WQDDTZSS=@WQDDTZSS,");
			strSql.Append("WFPLR=@WFPLR,");
			strSql.Append("NFPXJGL=@NFPXJGL,");
			strSql.Append("WBBBZSCE=@WBBBZSCE,");
			strSql.Append("GSYMGSGDQYHJ=@GSYMGSGDQYHJ,");
			strSql.Append("SSGDQY=@SSGDQY,");
			strSql.Append("SYZQY=@SYZQY,");
			strSql.Append("FZHSYZQY=@FZHSYZQY");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@HBZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JSBFJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CCZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JYXJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@YSJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@YSPJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YSZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YFKX", SqlDbType.NVarChar,50),
					new SqlParameter("@YSBF", SqlDbType.NVarChar,50),
					new SqlParameter("@YSFBZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YSFBHTZBJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YSLX", SqlDbType.NVarChar,50),
					new SqlParameter("@YSGL", SqlDbType.NVarChar,50),
					new SqlParameter("@QTYSK", SqlDbType.NVarChar,50),
					new SqlParameter("@YSCKTS", SqlDbType.NVarChar,50),
					new SqlParameter("@YSBTK", SqlDbType.NVarChar,50),
					new SqlParameter("@YSBZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@NBYSK", SqlDbType.NVarChar,50),
					new SqlParameter("@MRFSJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@CH", SqlDbType.NVarChar,50),
					new SqlParameter("@DTFY", SqlDbType.NVarChar,50),
					new SqlParameter("@DCLLDZCSS", SqlDbType.NVarChar,50),
					new SqlParameter("@YNNDQDFLDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@QTLDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@LDZCHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@FCDKJDK", SqlDbType.NVarChar,50),
					new SqlParameter("@KGCSJRZC", SqlDbType.NVarChar,50),
					new SqlParameter("@CYZDQTZ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQYSK", SqlDbType.NVarChar,50),
					new SqlParameter("@CQGQTZ", SqlDbType.NVarChar,50),
					new SqlParameter("@QTCQTZ", SqlDbType.NVarChar,50),
					new SqlParameter("@TZXFDC", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCYZ", SqlDbType.NVarChar,50),
					new SqlParameter("@LJZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCJZ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCJZZB", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@ZJGC", SqlDbType.NVarChar,50),
					new SqlParameter("@GCWZ", SqlDbType.NVarChar,50),
					new SqlParameter("@GDZCQL", SqlDbType.NVarChar,50),
					new SqlParameter("@SCXSWZC", SqlDbType.NVarChar,50),
					new SqlParameter("@GYXSWZC", SqlDbType.NVarChar,50),
					new SqlParameter("@QYZC", SqlDbType.NVarChar,50),
					new SqlParameter("@WXZC", SqlDbType.NVarChar,50),
					new SqlParameter("@KFZC", SqlDbType.NVarChar,50),
					new SqlParameter("@SY", SqlDbType.NVarChar,50),
					new SqlParameter("@CQDTFY", SqlDbType.NVarChar,50),
					new SqlParameter("@GQFZLTQ", SqlDbType.NVarChar,50),
					new SqlParameter("@DYSDSZC", SqlDbType.NVarChar,50),
					new SqlParameter("@QTFLDZC", SqlDbType.NVarChar,50),
					new SqlParameter("@FLDZCHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZCZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@DQJK", SqlDbType.NVarChar,50),
					new SqlParameter("@XZYYHJK", SqlDbType.NVarChar,50),
					new SqlParameter("@XSCKJTYCF", SqlDbType.NVarChar,50),
					new SqlParameter("@CRZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JYXJRFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YSJRFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFPJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFZK", SqlDbType.NVarChar,50),
					new SqlParameter("@YuSZK", SqlDbType.NVarChar,50),
					new SqlParameter("@MCHGJRZCK", SqlDbType.NVarChar,50),
					new SqlParameter("@YFSXFJYJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFZGXC", SqlDbType.NVarChar,50),
					new SqlParameter("@YJSF", SqlDbType.NVarChar,50),
					new SqlParameter("@YFLX", SqlDbType.NVarChar,50),
					new SqlParameter("@YFGL", SqlDbType.NVarChar,50),
					new SqlParameter("@QTYJK", SqlDbType.NVarChar,50),
					new SqlParameter("@YFBZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@NBYFK", SqlDbType.NVarChar,50),
					new SqlParameter("@QTYFK", SqlDbType.NVarChar,50),
					new SqlParameter("@YTFY", SqlDbType.NVarChar,50),
					new SqlParameter("@YJLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFFBZK", SqlDbType.NVarChar,50),
					new SqlParameter("@BXHTZBJ", SqlDbType.NVarChar,50),
					new SqlParameter("@DLMMZQK", SqlDbType.NVarChar,50),
					new SqlParameter("@DLCXZQK", SqlDbType.NVarChar,50),
					new SqlParameter("@GJPZJS", SqlDbType.NVarChar,50),
					new SqlParameter("@GNPZJS", SqlDbType.NVarChar,50),
					new SqlParameter("@DYSY", SqlDbType.NVarChar,50),
					new SqlParameter("@YFDQZQ", SqlDbType.NVarChar,50),
					new SqlParameter("@YNDDQDFLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@QTLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@LDFZHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQJQ", SqlDbType.NVarChar,50),
					new SqlParameter("@YFZQ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQYFZQ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXYFK", SqlDbType.NVarChar,50),
					new SqlParameter("@YJFLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@CQDYSY", SqlDbType.NVarChar,50),
					new SqlParameter("@DYSDSFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@QTFLDFZ", SqlDbType.NVarChar,50),
					new SqlParameter("@FLDFZHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@FZHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@SSZB", SqlDbType.NVarChar,50),
					new SqlParameter("@ZBGJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JKCG", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXCB", SqlDbType.NVarChar,50),
					new SqlParameter("@YYGJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YBFXZB", SqlDbType.NVarChar,50),
					new SqlParameter("@WQDDTZSS", SqlDbType.NVarChar,50),
					new SqlParameter("@WFPLR", SqlDbType.NVarChar,50),
					new SqlParameter("@NFPXJGL", SqlDbType.NVarChar,50),
					new SqlParameter("@WBBBZSCE", SqlDbType.NVarChar,50),
					new SqlParameter("@GSYMGSGDQYHJ", SqlDbType.NVarChar,50),
					new SqlParameter("@SSGDQY", SqlDbType.NVarChar,50),
					new SqlParameter("@SYZQY", SqlDbType.NVarChar,50),
					new SqlParameter("@FZHSYZQY", SqlDbType.NVarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.ReportDate;
			parameters[2].Value = model.HBZJ;
			parameters[3].Value = model.JSBFJ;
			parameters[4].Value = model.CCZJ;
			parameters[5].Value = model.JYXJRZC;
			parameters[6].Value = model.YSJRZC;
			parameters[7].Value = model.YSPJ;
			parameters[8].Value = model.YSZK;
			parameters[9].Value = model.YFKX;
			parameters[10].Value = model.YSBF;
			parameters[11].Value = model.YSFBZK;
			parameters[12].Value = model.YSFBHTZBJ;
			parameters[13].Value = model.YSLX;
			parameters[14].Value = model.YSGL;
			parameters[15].Value = model.QTYSK;
			parameters[16].Value = model.YSCKTS;
			parameters[17].Value = model.YSBTK;
			parameters[18].Value = model.YSBZJ;
			parameters[19].Value = model.NBYSK;
			parameters[20].Value = model.MRFSJRZC;
			parameters[21].Value = model.CH;
			parameters[22].Value = model.DTFY;
			parameters[23].Value = model.DCLLDZCSS;
			parameters[24].Value = model.YNNDQDFLDZC;
			parameters[25].Value = model.QTLDZC;
			parameters[26].Value = model.LDZCHJ;
			parameters[27].Value = model.FCDKJDK;
			parameters[28].Value = model.KGCSJRZC;
			parameters[29].Value = model.CYZDQTZ;
			parameters[30].Value = model.CQYSK;
			parameters[31].Value = model.CQGQTZ;
			parameters[32].Value = model.QTCQTZ;
			parameters[33].Value = model.TZXFDC;
			parameters[34].Value = model.GDZCYZ;
			parameters[35].Value = model.LJZJ;
			parameters[36].Value = model.GDZCJZ;
			parameters[37].Value = model.GDZCJZZB;
			parameters[38].Value = model.GDZC;
			parameters[39].Value = model.ZJGC;
			parameters[40].Value = model.GCWZ;
			parameters[41].Value = model.GDZCQL;
			parameters[42].Value = model.SCXSWZC;
			parameters[43].Value = model.GYXSWZC;
			parameters[44].Value = model.QYZC;
			parameters[45].Value = model.WXZC;
			parameters[46].Value = model.KFZC;
			parameters[47].Value = model.SY;
			parameters[48].Value = model.CQDTFY;
			parameters[49].Value = model.GQFZLTQ;
			parameters[50].Value = model.DYSDSZC;
			parameters[51].Value = model.QTFLDZC;
			parameters[52].Value = model.FLDZCHJ;
			parameters[53].Value = model.ZCZJ;
			parameters[54].Value = model.DQJK;
			parameters[55].Value = model.XZYYHJK;
			parameters[56].Value = model.XSCKJTYCF;
			parameters[57].Value = model.CRZJ;
			parameters[58].Value = model.JYXJRFZ;
			parameters[59].Value = model.YSJRFZ;
			parameters[60].Value = model.YFPJ;
			parameters[61].Value = model.YFZK;
			parameters[62].Value = model.YuSZK;
			parameters[63].Value = model.MCHGJRZCK;
			parameters[64].Value = model.YFSXFJYJ;
			parameters[65].Value = model.YFZGXC;
			parameters[66].Value = model.YJSF;
			parameters[67].Value = model.YFLX;
			parameters[68].Value = model.YFGL;
			parameters[69].Value = model.QTYJK;
			parameters[70].Value = model.YFBZJ;
			parameters[71].Value = model.NBYFK;
			parameters[72].Value = model.QTYFK;
			parameters[73].Value = model.YTFY;
			parameters[74].Value = model.YJLDFZ;
			parameters[75].Value = model.YFFBZK;
			parameters[76].Value = model.BXHTZBJ;
			parameters[77].Value = model.DLMMZQK;
			parameters[78].Value = model.DLCXZQK;
			parameters[79].Value = model.GJPZJS;
			parameters[80].Value = model.GNPZJS;
			parameters[81].Value = model.DYSY;
			parameters[82].Value = model.YFDQZQ;
			parameters[83].Value = model.YNDDQDFLDFZ;
			parameters[84].Value = model.QTLDFZ;
			parameters[85].Value = model.LDFZHJ;
			parameters[86].Value = model.CQJQ;
			parameters[87].Value = model.YFZQ;
			parameters[88].Value = model.CQYFZQ;
			parameters[89].Value = model.ZXYFK;
			parameters[90].Value = model.YJFLDFZ;
			parameters[91].Value = model.CQDYSY;
			parameters[92].Value = model.DYSDSFZ;
			parameters[93].Value = model.QTFLDFZ;
			parameters[94].Value = model.FLDFZHJ;
			parameters[95].Value = model.FZHJ;
			parameters[96].Value = model.SSZB;
			parameters[97].Value = model.ZBGJ;
			parameters[98].Value = model.JKCG;
			parameters[99].Value = model.ZXCB;
			parameters[100].Value = model.YYGJ;
			parameters[101].Value = model.YBFXZB;
			parameters[102].Value = model.WQDDTZSS;
			parameters[103].Value = model.WFPLR;
			parameters[104].Value = model.NFPXJGL;
			parameters[105].Value = model.WBBBZSCE;
			parameters[106].Value = model.GSYMGSGDQYHJ;
			parameters[107].Value = model.SSGDQY;
			parameters[108].Value = model.SYZQY;
			parameters[109].Value = model.FZHSYZQY;
			parameters[110].Value = model.id;

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
			strSql.Append("delete from StockZCFZInfo ");
			strSql.Append(" where Symbol=@Symbol ");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)			};
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
		public bool DeleteList(string SymbolList )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StockZCFZInfo ");
			strSql.Append(" where id in (" + SymbolList + ")  ");
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
		public StockZCFZInfo GetModel(string Symbol)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Symbol,ReportDate,HBZJ,JSBFJ,CCZJ,JYXJRZC,YSJRZC,YSPJ,YSZK,YFKX,YSBF,YSFBZK,YSFBHTZBJ,YSLX,YSGL,QTYSK,YSCKTS,YSBTK,YSBZJ,NBYSK,MRFSJRZC,CH,DTFY,DCLLDZCSS,YNNDQDFLDZC,QTLDZC,LDZCHJ,FCDKJDK,KGCSJRZC,CYZDQTZ,CQYSK,CQGQTZ,QTCQTZ,TZXFDC,GDZCYZ,LJZJ,GDZCJZ,GDZCJZZB,GDZC,ZJGC,GCWZ,GDZCQL,SCXSWZC,GYXSWZC,QYZC,WXZC,KFZC,SY,CQDTFY,GQFZLTQ,DYSDSZC,QTFLDZC,FLDZCHJ,ZCZJ,DQJK,XZYYHJK,XSCKJTYCF,CRZJ,JYXJRFZ,YSJRFZ,YFPJ,YFZK,YuSZK,MCHGJRZCK,YFSXFJYJ,YFZGXC,YJSF,YFLX,YFGL,QTYJK,YFBZJ,NBYFK,QTYFK,YTFY,YJLDFZ,YFFBZK,BXHTZBJ,DLMMZQK,DLCXZQK,GJPZJS,GNPZJS,DYSY,YFDQZQ,YNDDQDFLDFZ,QTLDFZ,LDFZHJ,CQJQ,YFZQ,CQYFZQ,ZXYFK,YJFLDFZ,CQDYSY,DYSDSFZ,QTFLDFZ,FLDFZHJ,FZHJ,SSZB,ZBGJ,JKCG,ZXCB,YYGJ,YBFXZB,WQDDTZSS,WFPLR,NFPXJGL,WBBBZSCE,GSYMGSGDQYHJ,SSGDQY,SYZQY,FZHSYZQY from StockZCFZInfo ");
			strSql.Append(" where Symbol=@Symbol ");
			SqlParameter[] parameters = {
					new SqlParameter("@Symbol", SqlDbType.NVarChar,10)			};
			parameters[0].Value = Symbol;

			StockZCFZInfo model = new StockZCFZInfo();
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
        public StockZCFZInfo DataRowToModel(DataRow row)
		{
			StockZCFZInfo model = new StockZCFZInfo();
			if (row != null)
			{
				//if(row["id"]!=null && row["id"].ToString()!="")
				//{
				//	model.id=int.Parse(row["id"].ToString());
				//}
				if(row["Symbol"] != null)
				{
					model.Code = row["Symbol"].ToString();
				}
				if(row["ReportDate"]!=null && row["ReportDate"].ToString()!="")
				{
					model.ReportDate=DateTime.Parse(row["ReportDate"].ToString());
				}
				if(row["HBZJ"]!=null)
				{
					model.HBZJ=row["HBZJ"].ToString();
				}
				if(row["JSBFJ"]!=null)
				{
					model.JSBFJ=row["JSBFJ"].ToString();
				}
				if(row["CCZJ"]!=null)
				{
					model.CCZJ=row["CCZJ"].ToString();
				}
				if(row["JYXJRZC"]!=null)
				{
					model.JYXJRZC=row["JYXJRZC"].ToString();
				}
				if(row["YSJRZC"]!=null)
				{
					model.YSJRZC=row["YSJRZC"].ToString();
				}
				if(row["YSPJ"]!=null)
				{
					model.YSPJ=row["YSPJ"].ToString();
				}
				if(row["YSZK"]!=null)
				{
					model.YSZK=row["YSZK"].ToString();
				}
				if(row["YFKX"]!=null)
				{
					model.YFKX=row["YFKX"].ToString();
				}
				if(row["YSBF"]!=null)
				{
					model.YSBF=row["YSBF"].ToString();
				}
				if(row["YSFBZK"]!=null)
				{
					model.YSFBZK=row["YSFBZK"].ToString();
				}
				if(row["YSFBHTZBJ"]!=null)
				{
					model.YSFBHTZBJ=row["YSFBHTZBJ"].ToString();
				}
				if(row["YSLX"]!=null)
				{
					model.YSLX=row["YSLX"].ToString();
				}
				if(row["YSGL"]!=null)
				{
					model.YSGL=row["YSGL"].ToString();
				}
				if(row["QTYSK"]!=null)
				{
					model.QTYSK=row["QTYSK"].ToString();
				}
				if(row["YSCKTS"]!=null)
				{
					model.YSCKTS=row["YSCKTS"].ToString();
				}
				if(row["YSBTK"]!=null)
				{
					model.YSBTK=row["YSBTK"].ToString();
				}
				if(row["YSBZJ"]!=null)
				{
					model.YSBZJ=row["YSBZJ"].ToString();
				}
				if(row["NBYSK"]!=null)
				{
					model.NBYSK=row["NBYSK"].ToString();
				}
				if(row["MRFSJRZC"]!=null)
				{
					model.MRFSJRZC=row["MRFSJRZC"].ToString();
				}
				if(row["CH"]!=null)
				{
					model.CH=row["CH"].ToString();
				}
				if(row["DTFY"]!=null)
				{
					model.DTFY=row["DTFY"].ToString();
				}
				if(row["DCLLDZCSS"]!=null)
				{
					model.DCLLDZCSS=row["DCLLDZCSS"].ToString();
				}
				if(row["YNNDQDFLDZC"]!=null)
				{
					model.YNNDQDFLDZC=row["YNNDQDFLDZC"].ToString();
				}
				if(row["QTLDZC"]!=null)
				{
					model.QTLDZC=row["QTLDZC"].ToString();
				}
				if(row["LDZCHJ"]!=null)
				{
					model.LDZCHJ=row["LDZCHJ"].ToString();
				}
				if(row["FCDKJDK"]!=null)
				{
					model.FCDKJDK=row["FCDKJDK"].ToString();
				}
				if(row["KGCSJRZC"]!=null)
				{
					model.KGCSJRZC=row["KGCSJRZC"].ToString();
				}
				if(row["CYZDQTZ"]!=null)
				{
					model.CYZDQTZ=row["CYZDQTZ"].ToString();
				}
				if(row["CQYSK"]!=null)
				{
					model.CQYSK=row["CQYSK"].ToString();
				}
				if(row["CQGQTZ"]!=null)
				{
					model.CQGQTZ=row["CQGQTZ"].ToString();
				}
				if(row["QTCQTZ"]!=null)
				{
					model.QTCQTZ=row["QTCQTZ"].ToString();
				}
				if(row["TZXFDC"]!=null)
				{
					model.TZXFDC=row["TZXFDC"].ToString();
				}
				if(row["GDZCYZ"]!=null)
				{
					model.GDZCYZ=row["GDZCYZ"].ToString();
				}
				if(row["LJZJ"]!=null)
				{
					model.LJZJ=row["LJZJ"].ToString();
				}
				if(row["GDZCJZ"]!=null)
				{
					model.GDZCJZ=row["GDZCJZ"].ToString();
				}
				if(row["GDZCJZZB"]!=null)
				{
					model.GDZCJZZB=row["GDZCJZZB"].ToString();
				}
				if(row["GDZC"]!=null)
				{
					model.GDZC=row["GDZC"].ToString();
				}
				if(row["ZJGC"]!=null)
				{
					model.ZJGC=row["ZJGC"].ToString();
				}
				if(row["GCWZ"]!=null)
				{
					model.GCWZ=row["GCWZ"].ToString();
				}
				if(row["GDZCQL"]!=null)
				{
					model.GDZCQL=row["GDZCQL"].ToString();
				}
				if(row["SCXSWZC"]!=null)
				{
					model.SCXSWZC=row["SCXSWZC"].ToString();
				}
				if(row["GYXSWZC"]!=null)
				{
					model.GYXSWZC=row["GYXSWZC"].ToString();
				}
				if(row["QYZC"]!=null)
				{
					model.QYZC=row["QYZC"].ToString();
				}
				if(row["WXZC"]!=null)
				{
					model.WXZC=row["WXZC"].ToString();
				}
				if(row["KFZC"]!=null)
				{
					model.KFZC=row["KFZC"].ToString();
				}
				if(row["SY"]!=null)
				{
					model.SY=row["SY"].ToString();
				}
				if(row["CQDTFY"]!=null)
				{
					model.CQDTFY=row["CQDTFY"].ToString();
				}
				if(row["GQFZLTQ"]!=null)
				{
					model.GQFZLTQ=row["GQFZLTQ"].ToString();
				}
				if(row["DYSDSZC"]!=null)
				{
					model.DYSDSZC=row["DYSDSZC"].ToString();
				}
				if(row["QTFLDZC"]!=null)
				{
					model.QTFLDZC=row["QTFLDZC"].ToString();
				}
				if(row["FLDZCHJ"]!=null)
				{
					model.FLDZCHJ=row["FLDZCHJ"].ToString();
				}
				if(row["ZCZJ"]!=null)
				{
					model.ZCZJ=row["ZCZJ"].ToString();
				}
				if(row["DQJK"]!=null)
				{
					model.DQJK=row["DQJK"].ToString();
				}
				if(row["XZYYHJK"]!=null)
				{
					model.XZYYHJK=row["XZYYHJK"].ToString();
				}
				if(row["XSCKJTYCF"]!=null)
				{
					model.XSCKJTYCF=row["XSCKJTYCF"].ToString();
				}
				if(row["CRZJ"]!=null)
				{
					model.CRZJ=row["CRZJ"].ToString();
				}
				if(row["JYXJRFZ"]!=null)
				{
					model.JYXJRFZ=row["JYXJRFZ"].ToString();
				}
				if(row["YSJRFZ"]!=null)
				{
					model.YSJRFZ=row["YSJRFZ"].ToString();
				}
				if(row["YFPJ"]!=null)
				{
					model.YFPJ=row["YFPJ"].ToString();
				}
				if(row["YFZK"]!=null)
				{
					model.YFZK=row["YFZK"].ToString();
				}
				if(row["YuSZK"]!=null)
				{
					model.YuSZK=row["YuSZK"].ToString();
				}
				if(row["MCHGJRZCK"]!=null)
				{
					model.MCHGJRZCK=row["MCHGJRZCK"].ToString();
				}
				if(row["YFSXFJYJ"]!=null)
				{
					model.YFSXFJYJ=row["YFSXFJYJ"].ToString();
				}
				if(row["YFZGXC"]!=null)
				{
					model.YFZGXC=row["YFZGXC"].ToString();
				}
				if(row["YJSF"]!=null)
				{
					model.YJSF=row["YJSF"].ToString();
				}
				if(row["YFLX"]!=null)
				{
					model.YFLX=row["YFLX"].ToString();
				}
				if(row["YFGL"]!=null)
				{
					model.YFGL=row["YFGL"].ToString();
				}
				if(row["QTYJK"]!=null)
				{
					model.QTYJK=row["QTYJK"].ToString();
				}
				if(row["YFBZJ"]!=null)
				{
					model.YFBZJ=row["YFBZJ"].ToString();
				}
				if(row["NBYFK"]!=null)
				{
					model.NBYFK=row["NBYFK"].ToString();
				}
				if(row["QTYFK"]!=null)
				{
					model.QTYFK=row["QTYFK"].ToString();
				}
				if(row["YTFY"]!=null)
				{
					model.YTFY=row["YTFY"].ToString();
				}
				if(row["YJLDFZ"]!=null)
				{
					model.YJLDFZ=row["YJLDFZ"].ToString();
				}
				if(row["YFFBZK"]!=null)
				{
					model.YFFBZK=row["YFFBZK"].ToString();
				}
				if(row["BXHTZBJ"]!=null)
				{
					model.BXHTZBJ=row["BXHTZBJ"].ToString();
				}
				if(row["DLMMZQK"]!=null)
				{
					model.DLMMZQK=row["DLMMZQK"].ToString();
				}
				if(row["DLCXZQK"]!=null)
				{
					model.DLCXZQK=row["DLCXZQK"].ToString();
				}
				if(row["GJPZJS"]!=null)
				{
					model.GJPZJS=row["GJPZJS"].ToString();
				}
				if(row["GNPZJS"]!=null)
				{
					model.GNPZJS=row["GNPZJS"].ToString();
				}
				if(row["DYSY"]!=null)
				{
					model.DYSY=row["DYSY"].ToString();
				}
				if(row["YFDQZQ"]!=null)
				{
					model.YFDQZQ=row["YFDQZQ"].ToString();
				}
				if(row["YNDDQDFLDFZ"]!=null)
				{
					model.YNDDQDFLDFZ=row["YNDDQDFLDFZ"].ToString();
				}
				if(row["QTLDFZ"]!=null)
				{
					model.QTLDFZ=row["QTLDFZ"].ToString();
				}
				if(row["LDFZHJ"]!=null)
				{
					model.LDFZHJ=row["LDFZHJ"].ToString();
				}
				if(row["CQJQ"]!=null)
				{
					model.CQJQ=row["CQJQ"].ToString();
				}
				if(row["YFZQ"]!=null)
				{
					model.YFZQ=row["YFZQ"].ToString();
				}
				if(row["CQYFZQ"]!=null)
				{
					model.CQYFZQ=row["CQYFZQ"].ToString();
				}
				if(row["ZXYFK"]!=null)
				{
					model.ZXYFK=row["ZXYFK"].ToString();
				}
				if(row["YJFLDFZ"]!=null)
				{
					model.YJFLDFZ=row["YJFLDFZ"].ToString();
				}
				if(row["CQDYSY"]!=null)
				{
					model.CQDYSY=row["CQDYSY"].ToString();
				}
				if(row["DYSDSFZ"]!=null)
				{
					model.DYSDSFZ=row["DYSDSFZ"].ToString();
				}
				if(row["QTFLDFZ"]!=null)
				{
					model.QTFLDFZ=row["QTFLDFZ"].ToString();
				}
				if(row["FLDFZHJ"]!=null)
				{
					model.FLDFZHJ=row["FLDFZHJ"].ToString();
				}
				if(row["FZHJ"]!=null)
				{
					model.FZHJ=row["FZHJ"].ToString();
				}
				if(row["SSZB"]!=null)
				{
					model.SSZB=row["SSZB"].ToString();
				}
				if(row["ZBGJ"]!=null)
				{
					model.ZBGJ=row["ZBGJ"].ToString();
				}
				if(row["JKCG"]!=null)
				{
					model.JKCG=row["JKCG"].ToString();
				}
				if(row["ZXCB"]!=null)
				{
					model.ZXCB=row["ZXCB"].ToString();
				}
				if(row["YYGJ"]!=null)
				{
					model.YYGJ=row["YYGJ"].ToString();
				}
				if(row["YBFXZB"]!=null)
				{
					model.YBFXZB=row["YBFXZB"].ToString();
				}
				if(row["WQDDTZSS"]!=null)
				{
					model.WQDDTZSS=row["WQDDTZSS"].ToString();
				}
				if(row["WFPLR"]!=null)
				{
					model.WFPLR=row["WFPLR"].ToString();
				}
				if(row["NFPXJGL"]!=null)
				{
					model.NFPXJGL=row["NFPXJGL"].ToString();
				}
				if(row["WBBBZSCE"]!=null)
				{
					model.WBBBZSCE=row["WBBBZSCE"].ToString();
				}
				if(row["GSYMGSGDQYHJ"]!=null)
				{
					model.GSYMGSGDQYHJ=row["GSYMGSGDQYHJ"].ToString();
				}
				if(row["SSGDQY"]!=null)
				{
					model.SSGDQY=row["SSGDQY"].ToString();
				}
				if(row["SYZQY"]!=null)
				{
					model.SYZQY=row["SYZQY"].ToString();
				}
				if(row["FZHSYZQY"]!=null)
				{
					model.FZHSYZQY=row["FZHSYZQY"].ToString();
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
			strSql.Append("select Symbol,ReportDate,HBZJ,JSBFJ,CCZJ,JYXJRZC,YSJRZC,YSPJ,YSZK,YFKX,YSBF,YSFBZK,YSFBHTZBJ,YSLX,YSGL,QTYSK,YSCKTS,YSBTK,YSBZJ,NBYSK,MRFSJRZC,CH,DTFY,DCLLDZCSS,YNNDQDFLDZC,QTLDZC,LDZCHJ,FCDKJDK,KGCSJRZC,CYZDQTZ,CQYSK,CQGQTZ,QTCQTZ,TZXFDC,GDZCYZ,LJZJ,GDZCJZ,GDZCJZZB,GDZC,ZJGC,GCWZ,GDZCQL,SCXSWZC,GYXSWZC,QYZC,WXZC,KFZC,SY,CQDTFY,GQFZLTQ,DYSDSZC,QTFLDZC,FLDZCHJ,ZCZJ,DQJK,XZYYHJK,XSCKJTYCF,CRZJ,JYXJRFZ,YSJRFZ,YFPJ,YFZK,YuSZK,MCHGJRZCK,YFSXFJYJ,YFZGXC,YJSF,YFLX,YFGL,QTYJK,YFBZJ,NBYFK,QTYFK,YTFY,YJLDFZ,YFFBZK,BXHTZBJ,DLMMZQK,DLCXZQK,GJPZJS,GNPZJS,DYSY,YFDQZQ,YNDDQDFLDFZ,QTLDFZ,LDFZHJ,CQJQ,YFZQ,CQYFZQ,ZXYFK,YJFLDFZ,CQDYSY,DYSDSFZ,QTFLDFZ,FLDFZHJ,FZHJ,SSZB,ZBGJ,JKCG,ZXCB,YYGJ,YBFXZB,WQDDTZSS,WFPLR,NFPXJGL,WBBBZSCE,GSYMGSGDQYHJ,SSGDQY,SYZQY,FZHSYZQY ");
			strSql.Append(" FROM StockZCFZInfo ");
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
			strSql.Append(" Symbol,ReportDate,HBZJ,JSBFJ,CCZJ,JYXJRZC,YSJRZC,YSPJ,YSZK,YFKX,YSBF,YSFBZK,YSFBHTZBJ,YSLX,YSGL,QTYSK,YSCKTS,YSBTK,YSBZJ,NBYSK,MRFSJRZC,CH,DTFY,DCLLDZCSS,YNNDQDFLDZC,QTLDZC,LDZCHJ,FCDKJDK,KGCSJRZC,CYZDQTZ,CQYSK,CQGQTZ,QTCQTZ,TZXFDC,GDZCYZ,LJZJ,GDZCJZ,GDZCJZZB,GDZC,ZJGC,GCWZ,GDZCQL,SCXSWZC,GYXSWZC,QYZC,WXZC,KFZC,SY,CQDTFY,GQFZLTQ,DYSDSZC,QTFLDZC,FLDZCHJ,ZCZJ,DQJK,XZYYHJK,XSCKJTYCF,CRZJ,JYXJRFZ,YSJRFZ,YFPJ,YFZK,YuSZK,MCHGJRZCK,YFSXFJYJ,YFZGXC,YJSF,YFLX,YFGL,QTYJK,YFBZJ,NBYFK,QTYFK,YTFY,YJLDFZ,YFFBZK,BXHTZBJ,DLMMZQK,DLCXZQK,GJPZJS,GNPZJS,DYSY,YFDQZQ,YNDDQDFLDFZ,QTLDFZ,LDFZHJ,CQJQ,YFZQ,CQYFZQ,ZXYFK,YJFLDFZ,CQDYSY,DYSDSFZ,QTFLDFZ,FLDFZHJ,FZHJ,SSZB,ZBGJ,JKCG,ZXCB,YYGJ,YBFXZB,WQDDTZSS,WFPLR,NFPXJGL,WBBBZSCE,GSYMGSGDQYHJ,SSGDQY,SYZQY,FZHSYZQY ");
			strSql.Append(" FROM StockZCFZInfo ");
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
			strSql.Append("select count(1) FROM StockZCFZInfo ");
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
			strSql.Append(")AS Row, T.*  from StockZCFZInfo T ");
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
			parameters[0].Value = "StockZCFZInfo";
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
