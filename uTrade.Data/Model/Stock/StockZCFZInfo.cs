using System;

namespace uTrade.Data
{
    public partial class StockZCFZInfo
    {
 
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 报告日期
        /// </summary>
        public DateTime? ReportDate { get; set; }
        /// <summary>
        /// 货币资金(万元)
        /// </summary>
        public string HBZJ { get; set; }
        /// <summary>
        /// 结算备付金(万元)
        /// </summary>
        public string JSBFJ { get; set; }
        /// <summary>
        /// 拆出资金(万元)
        /// </summary>
        public string CCZJ { get; set; }
        /// <summary>
        /// 交易性金融资产(万元)
        /// </summary>
        public string JYXJRZC { get; set; }
        /// <summary>
        /// 衍生金融资产(万元)
        /// </summary>
        public string YSJRZC { get; set; }
        /// <summary>
        /// 应收票据(万元)
        /// </summary>
        public string YSPJ { get; set; }
        /// <summary>
        /// 应收账款(万元)
        /// </summary>
        public string YSZK { get; set; }
        /// <summary>
        /// 预付款项(万元)
        /// </summary>
        public string YFKX { get; set; }
        /// <summary>
        /// 应收保费(万元)
        /// </summary>
        public string YSBF { get; set; }
        /// <summary>
        /// 应收分保账款(万元)
        /// </summary>
        public string YSFBZK { get; set; }
        /// <summary>
        /// 应收分保合同准备金(万元)
        /// </summary>
        public string YSFBHTZBJ { get; set; }
        /// <summary>
        /// 应收利息(万元)
        /// </summary>
        public string YSLX { get; set; }
        /// <summary>
        /// 应收股利(万元)
        /// </summary>
        public string YSGL { get; set; }
        /// <summary>
        /// 其他应收款(万元)
        /// </summary>
        public string QTYSK { get; set; }
        /// <summary>
        /// 应收出口退税(万元)
        /// </summary>
        public string YSCKTS { get; set; }
        /// <summary>
        /// 应收补贴款(万元)
        /// </summary>
        public string YSBTK { get; set; }
        /// <summary>
        /// 应收保证金(万元)
        /// </summary>
        public string YSBZJ { get; set; }
        /// <summary>
        /// 内部应收款(万元)
        /// </summary>
        public string NBYSK { get; set; }
        /// <summary>
        /// 买入返售金融资产(万元)
        /// </summary>
        public string MRFSJRZC { get; set; }
        /// <summary>
        /// 存货(万元)
        /// </summary>
        public string CH { get; set; }
        /// <summary>
        /// 待摊费用(万元)
        /// </summary>
        public string DTFY { get; set; }
        /// <summary>
        /// 待处理流动资产损益(万元)
        /// </summary>
        public string DCLLDZCSS { get; set; }
        /// <summary>
        /// 一年内到期的非流动资产(万元)
        /// </summary>
        public string YNNDQDFLDZC { get; set; }
        /// <summary>
        /// 其他流动资产(万元)
        /// </summary>
        public string QTLDZC { get; set; }
        /// <summary>
        /// 流动资产合计(万元)
        /// </summary>
        public string LDZCHJ { get; set; }
        /// <summary>
        /// 发放贷款及垫款(万元)
        /// </summary>
        public string FCDKJDK { get; set; }
        /// <summary>
        /// 可供出售金融资产(万元)
        /// </summary>
        public string KGCSJRZC { get; set; }
        /// <summary>
        /// 持有至到期投资(万元)
        /// </summary>
        public string CYZDQTZ { get; set; }
        /// <summary>
        /// 长期应收款(万元)
        /// </summary>
        public string CQYSK { get; set; }
        /// <summary>
        /// 长期股权投资(万元)
        /// </summary>
        public string CQGQTZ { get; set; }
        /// <summary>
        /// 其他长期投资(万元)
        /// </summary>
        public string QTCQTZ { get; set; }
        /// <summary>
        /// 投资性房地产(万元)
        /// </summary>
        public string TZXFDC { get; set; }
        /// <summary>
        /// 固定资产原值(万元)
        /// </summary>
        public string GDZCYZ { get; set; }
        /// <summary>
        /// 累计折旧(万元)
        /// </summary>
        public string LJZJ { get; set; }
        /// <summary>
        /// 固定资产净值(万元)
        /// </summary>
        public string GDZCJZ { get; set; }
        /// <summary>
        /// 固定资产减值准备(万元)
        /// </summary>
        public string GDZCJZZB { get; set; }
        /// <summary>
        /// 固定资产(万元)
        /// </summary>
        public string GDZC { get; set; }
        /// <summary>
        /// 在建工程(万元)
        /// </summary>
        public string ZJGC { get; set; }
        /// <summary>
        /// 工程物资(万元)
        /// </summary>
        public string GCWZ { get; set; }
        /// <summary>
        /// 固定资产清理(万元)
        /// </summary>
        public string GDZCQL { get; set; }
        /// <summary>
        /// 生产性生物资产(万元)
        /// </summary>
        public string SCXSWZC { get; set; }
        /// <summary>
        /// 公益性生物资产(万元)
        /// </summary>
        public string GYXSWZC { get; set; }
        /// <summary>
        /// 油气资产(万元)
        /// </summary>
        public string QYZC { get; set; }
        /// <summary>
        /// 无形资产(万元)
        /// </summary>
        public string WXZC { get; set; }
        /// <summary>
        /// 开发支出(万元)
        /// </summary>
        public string KFZC { get; set; }
        /// <summary>
        /// 商誉(万元)
        /// </summary>
        public string SY { get; set; }
        /// <summary>
        /// 长期待摊费用(万元)
        /// </summary>
        public string CQDTFY { get; set; }
        /// <summary>
        /// 股权分置流通权(万元)
        /// </summary>
        public string GQFZLTQ { get; set; }
        /// <summary>
        /// 递延所得税资产(万元)
        /// </summary>
        public string DYSDSZC { get; set; }
        /// <summary>
        /// 其他非流动资产(万元)
        /// </summary>
        public string QTFLDZC { get; set; }
        /// <summary>
        /// 非流动资产合计(万元)
        /// </summary>
        public string FLDZCHJ { get; set; }
        /// <summary>
        /// 资产总计(万元)
        /// </summary>
        public string ZCZJ { get; set; }
        /// <summary>
        /// 短期借款(万元)
        /// </summary>
        public string DQJK { get; set; }
        /// <summary>
        /// 向中央银行借款(万元)
        /// </summary>
        public string XZYYHJK { get; set; }
        /// <summary>
        /// 吸收存款及同业存放(万元)
        /// </summary>
        public string XSCKJTYCF { get; set; }
        /// <summary>
        /// 拆入资金(万元)
        /// </summary>
        public string CRZJ { get; set; }
        /// <summary>
        /// 交易性金融负债(万元)
        /// </summary>
        public string JYXJRFZ { get; set; }
        /// <summary>
        /// 衍生金融负债(万元)
        /// </summary>
        public string YSJRFZ { get; set; }
        /// <summary>
        /// 应付票据(万元)
        /// </summary>
        public string YFPJ { get; set; }
        /// <summary>
        /// 应付账款(万元)
        /// </summary>
        public string YFZK { get; set; }
        /// <summary>
        /// 预收账款(万元)
        /// </summary>
        public string YuSZK { get; set; }
        /// <summary>
        /// 卖出回购金融资产款(万元)
        /// </summary>
        public string MCHGJRZCK { get; set; }
        /// <summary>
        /// 应付手续费及佣金(万元)
        /// </summary>
        public string YFSXFJYJ { get; set; }
        /// <summary>
        /// 应付职工薪酬(万元)
        /// </summary>
        public string YFZGXC { get; set; }
        /// <summary>
        /// 应交税费(万元)
        /// </summary>
        public string YJSF { get; set; }
        /// <summary>
        /// 应付利息(万元)
        /// </summary>
        public string YFLX { get; set; }
        /// <summary>
        /// 应付股利(万元)
        /// </summary>
        public string YFGL { get; set; }
        /// <summary>
        /// 其他应交款(万元)
        /// </summary>
        public string QTYJK { get; set; }
        /// <summary>
        /// 应付保证金(万元)
        /// </summary>
        public string YFBZJ { get; set; }
        /// <summary>
        /// 内部应付款(万元)
        /// </summary>
        public string NBYFK { get; set; }
        /// <summary>
        /// 其他应付款(万元)
        /// </summary>
        public string QTYFK { get; set; }
        /// <summary>
        /// 预提费用(万元)
        /// </summary>
        public string YTFY { get; set; }
        /// <summary>
        /// 预计流动负债(万元)
        /// </summary>
        public string YJLDFZ { get; set; }
        /// <summary>
        /// 应付分保账款(万元)
        /// </summary>
        public string YFFBZK { get; set; }
        /// <summary>
        /// 保险合同准备金(万元)
        /// </summary>
        public string BXHTZBJ { get; set; }
        /// <summary>
        /// 代理买卖证券款(万元)
        /// </summary>
        public string DLMMZQK { get; set; }
        /// <summary>
        /// 代理承销证券款(万元)
        /// </summary>
        public string DLCXZQK { get; set; }
        /// <summary>
        /// 国际票证结算(万元)
        /// </summary>
        public string GJPZJS { get; set; }
        /// <summary>
        /// 国内票证结算(万元)
        /// </summary>
        public string GNPZJS { get; set; }
        /// <summary>
        /// 递延收益(万元)
        /// </summary>
        public string DYSY { get; set; }
        /// <summary>
        /// 应付短期债券(万元)
        /// </summary>
        public string YFDQZQ { get; set; }
        /// <summary>
        /// 一年内到期的非流动负债(万元)
        /// </summary>
        public string YNDDQDFLDFZ { get; set; }
        /// <summary>
        /// 其他流动负债(万元)
        /// </summary>
        public string QTLDFZ { get; set; }
        /// <summary>
        /// 流动负债合计(万元)
        /// </summary>
        public string LDFZHJ { get; set; }
        /// <summary>
        /// 长期借款(万元)
        /// </summary>
        public string CQJQ { get; set; }
        /// <summary>
        /// 应付债券(万元)
        /// </summary>
        public string YFZQ { get; set; }
        /// <summary>
        /// 长期应付款(万元)
        /// </summary>
        public string CQYFZQ { get; set; }
        /// <summary>
        /// 专项应付款(万元)
        /// </summary>
        public string ZXYFK { get; set; }
        /// <summary>
        /// 预计非流动负债(万元)
        /// </summary>
        public string YJFLDFZ { get; set; }
        /// <summary>
        /// 长期递延收益(万元)
        /// </summary>
        public string CQDYSY { get; set; }
        /// <summary>
        /// 递延所得税负债(万元)
        /// </summary>
        public string DYSDSFZ { get; set; }
        /// <summary>
        /// 其他非流动负债(万元)
        /// </summary>
        public string QTFLDFZ { get; set; }
        /// <summary>
        /// 非流动负债合计(万元)
        /// </summary>
        public string FLDFZHJ { get; set; }
        /// <summary>
        /// 负债合计(万元)
        /// </summary>
        public string FZHJ { get; set; }
        /// <summary>
        /// 实收资本(或股本)(万元)
        /// </summary>
        public string SSZB { get; set; }
        /// <summary>
        /// 资本公积(万元)
        /// </summary>
        public string ZBGJ { get; set; }
        /// <summary>
        /// 减:库存股(万元)
        /// </summary>
        public string JKCG { get; set; }
        /// <summary>
        /// 专项储备(万元)
        /// </summary>
        public string ZXCB { get; set; }
        /// <summary>
        /// 盈余公积(万元)
        /// </summary>
        public string YYGJ { get; set; }
        /// <summary>
        /// 一般风险准备(万元)
        /// </summary>
        public string YBFXZB { get; set; }
        /// <summary>
        /// 未确定的投资损失(万元)
        /// </summary>
        public string WQDDTZSS { get; set; }
        /// <summary>
        /// 未分配利润(万元)
        /// </summary>
        public string WFPLR { get; set; }
        /// <summary>
        /// 拟分配现金股利(万元)
        /// </summary>
        public string NFPXJGL { get; set; }
        /// <summary>
        /// 外币报表折算差额(万元)
        /// </summary>
        public string WBBBZSCE { get; set; }
        /// <summary>
        /// 归属于母公司股东权益合计(万元)
        /// </summary>
        public string GSYMGSGDQYHJ { get; set; }
        /// <summary>
        /// 少数股东权益(万元)
        /// </summary>
        public string SSGDQY { get; set; }
        /// <summary>
        /// 所有者权益(或股东权益)合计(万元)
        /// </summary>
        public string SYZQY { get; set; }
        /// <summary>
        /// 负债和所有者权益(或股东权益)总计(万元)
        /// </summary>
        public string FZHSYZQY { get; set; }
    }
    
}
