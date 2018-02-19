use ocss
go


if exists (select 1
            from  sysobjects
           where  id = object_id('Stock5MinInfo')
            and   type = 'U')
   drop table Stock5MinInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StockCwInfo')
            and   type = 'U')
   drop table StockCwInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StockFinanceInfo')
            and   type = 'U')
   drop table StockFinanceInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StockInfo')
            and   type = 'U')
   drop table StockInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StockMinInfo')
            and   type = 'U')
   drop table StockMinInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StockZCFZInfo')
            and   type = 'U')
   drop table StockZCFZInfo
go

/*==============================================================*/
/* Table: Stock5MinInfo                                         */
/*==============================================================*/
create table Stock5MinInfo (
   Symbol               nvarchar(10)         not null,
   Time                 datetime             not null,
   "Open"               decimal(9)           null,
   "Close"              decimal(9)           null,
   High                 decimal(9)           null,
   Low                  decimal(9)           null,
   Volume               nvarchar(50)         null,
   Turnover             nvarchar(50)         null,
   UpNum                nvarchar(50)         null,
   DownNum              nvarchar(50)         null,
   constraint PK_STOCK5MININFO primary key (Symbol, Time)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Stock5MinInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Stock5MinInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '修改人：dox0
   修改时间：2017-11-11
   修改原因：新建表', 
   'user', @CurrentUser, 'table', 'Stock5MinInfo'
go

/*==============================================================*/
/* Table: StockCwInfo                                           */
/*==============================================================*/
create table StockCwInfo (
   Symbol               nvarchar(10)         not null,
   ReportDate           datetime             null,
   JBMGSY               decimal(9)           null,
   MGJZC                decimal(9)           null,
   MGJYHDCSXJLJE        decimal(9)           null,
   ZYYWSR               nvarchar(50)         null,
   ZYYWLR               nvarchar(50)         null,
   YYLR                 nvarchar(50)         null,
   TZSY                 nvarchar(50)         null,
   YYEYSZJE             nvarchar(50)         null,
   LRZE                 nvarchar(50)         null,
   JLR                  nvarchar(50)         null,
   JLROUT               nvarchar(50)         null,
   JYHDCSDXJLJE         nvarchar(50)         null,
   XJJXJDJWJCJE         nvarchar(50)         null,
   ZZC                  nvarchar(50)         null,
   LDZC                 nvarchar(50)         null,
   ZFZ                  nvarchar(50)         null,
   LDFZ                 nvarchar(50)         null,
   GDQYBHSSGDQY         nvarchar(50)         null,
   JZCSYLJQ             decimal(9)           null,
   constraint PK_STOCKCWINFO primary key (Symbol)
)
go

/*==============================================================*/
/* Table: StockFinanceInfo                                      */
/*==============================================================*/
create table StockFinanceInfo (
   Symbol               nvarchar(10)         not null,
   Type                 nvarchar(50)         null,
   GBLT                 nvarchar(50)         null,
   SSSF                 nvarchar(50)         null,
   SSHY                 nvarchar(50)         null,
   CWUpdateTime         datetime             null,
   ListingDate          datetime             null,
   AllGB                nvarchar(50)         null,
   GJG                  nvarchar(50)         null,
   FQRFRG               nvarchar(50)         null,
   FRG                  nvarchar(50)         null,
   BG                   nvarchar(50)         null,
   HG                   nvarchar(50)         null,
   ZhGG                 nvarchar(50)         null,
   AllZC                nvarchar(50)         null,
   LDZC                 nvarchar(50)         null,
   GDZC                 nvarchar(50)         null,
   WXZC                 nvarchar(50)         null,
   GDRS                 nvarchar(50)         null,
   LDFZ                 nvarchar(50)         null,
   CQFZ                 nvarchar(50)         null,
   ZBGJJ                nvarchar(50)         null,
   JZC                  nvarchar(50)         null,
   ZYSR                 nvarchar(50)         null,
   ZYLR                 nvarchar(50)         null,
   YSZK                 nvarchar(50)         null,
   YYLR                 nvarchar(50)         null,
   TZSY                 nvarchar(50)         null,
   JYXJL                nvarchar(50)         null,
   ZXJL                 nvarchar(50)         null,
   CH                   nvarchar(50)         null,
   LRZE                 nvarchar(50)         null,
   SHLR                 nvarchar(50)         null,
   JLR                  nvarchar(50)         null,
   WFLR                 nvarchar(50)         null,
   Unknow1              nvarchar(50)         null,
   unknow2              nvarchar(50)         null,
   constraint PK_STOCKFINANCEINFO primary key (Symbol)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StockFinanceInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StockFinanceInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '修改人：dox0
   修改时间：2017-11-11
   修改原因：新建表', 
   'user', @CurrentUser, 'table', 'StockFinanceInfo'
go

/*==============================================================*/
/* Table: StockInfo                                             */
/*==============================================================*/
create table StockInfo (
   Symbol               nvarchar(10)         not null,
   Type                 nvarchar(50)         null,
   OneHand              nvarchar(50)         null,
   Name                 nvarchar(50)         null,
   PointIndex           nvarchar(50)         null,
   YestClose            decimal(9)           null,
   Unknow1              nvarchar(50)         null,
   Unknow2              nvarchar(50)         null,
   Unknow3              nvarchar(50)         null,
   Favorite             int                  null,
   constraint PK_STOCKINFO primary key (Symbol)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StockInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StockInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '修改人：dox0
   修改时间：2017-11-11
   修改原因：新建表', 
   'user', @CurrentUser, 'table', 'StockInfo'
go

/*==============================================================*/
/* Table: StockMinInfo                                          */
/*==============================================================*/
create table StockMinInfo (
   Symbol               nvarchar(10)         not null,
   Name                 nvarchar(50)         null,
   Type                 nchar(10)            null,
   "Open"               decimal(9)           null,
   High                 decimal(9)           null,
   Low                  decimal(9)           null,
   Status               int                  null,
   Price                decimal(9)           null,
   Yestclose            decimal(9)           null,
   "Percent"            float                null,
   Updown               float                null,
   Arrow                nchar(10)            null,
   Volume               nvarchar(50)         null,
   Turnover             decimal(9)           null,
   Ask1                 decimal(9)           null,
   Ask2                 decimal(9)           null,
   Ask3                 decimal(9)           null,
   Ask4                 decimal(9)           null,
   Ask5                 decimal(9)           null,
   Askvol1              nvarchar(50)         null,
   Askvol2              nvarchar(50)         null,
   Askvol3              nvarchar(50)         null,
   Askvol4              nvarchar(50)         null,
   Askvol5              nvarchar(50)         null,
   Bid1                 decimal(9)           null,
   Bid2                 decimal(9)           null,
   Bid3                 decimal(9)           null,
   Bid4                 decimal(9)           null,
   Bid5                 decimal(9)           null,
   Bidvol1              nvarchar(50)         null,
   Bidvol2              nvarchar(50)         null,
   Bidvol3              nvarchar(50)         null,
   Bidvol4              nvarchar(50)         null,
   Bidvol5              nvarchar(50)         null,
   "Update"             nvarchar(50)         null,
   Time                 datetime             null,
   constraint PK_STOCKMININFO primary key (Symbol)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StockMinInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StockMinInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '修改人：dox0
   修改时间：2017-11-11
   修改原因：新建表', 
   'user', @CurrentUser, 'table', 'StockMinInfo'
go

/*==============================================================*/
/* Table: StockZCFZInfo                                         */
/*==============================================================*/
create table StockZCFZInfo (
   Symbol               nvarchar(10)         not null,
   ReportDate           datetime             null,
   HBZJ                 nvarchar(50)         null,
   JSBFJ                nvarchar(50)         null,
   CCZJ                 nvarchar(50)         null,
   JYXJRZC              nvarchar(50)         null,
   YSJRZC               nvarchar(50)         null,
   YSPJ                 nvarchar(50)         null,
   YSZK                 nvarchar(50)         null,
   YFKX                 nvarchar(50)         null,
   YSBF                 nvarchar(50)         null,
   YSFBZK               nvarchar(50)         null,
   YSFBHTZBJ            nvarchar(50)         null,
   YSLX                 nvarchar(50)         null,
   YSGL                 nvarchar(50)         null,
   QTYSK                nvarchar(50)         null,
   YSCKTS               nvarchar(50)         null,
   YSBTK                nvarchar(50)         null,
   YSBZJ                nvarchar(50)         null,
   NBYSK                nvarchar(50)         null,
   MRFSJRZC             nvarchar(50)         null,
   CH                   nvarchar(50)         null,
   DTFY                 nvarchar(50)         null,
   DCLLDZCSS            nvarchar(50)         null,
   YNNDQDFLDZC          nvarchar(50)         null,
   QTLDZC               nvarchar(50)         null,
   LDZCHJ               nvarchar(50)         null,
   FCDKJDK              nvarchar(50)         null,
   KGCSJRZC             nvarchar(50)         null,
   CYZDQTZ              nvarchar(50)         null,
   CQYSK                nvarchar(50)         null,
   CQGQTZ               nvarchar(50)         null,
   QTCQTZ               nvarchar(50)         null,
   TZXFDC               nvarchar(50)         null,
   GDZCYZ               nvarchar(50)         null,
   LJZJ                 nvarchar(50)         null,
   GDZCJZ               nvarchar(50)         null,
   GDZCJZZB             nvarchar(50)         null,
   GDZC                 nvarchar(50)         null,
   ZJGC                 nvarchar(50)         null,
   GCWZ                 nvarchar(50)         null,
   GDZCQL               nvarchar(50)         null,
   SCXSWZC              nvarchar(50)         null,
   GYXSWZC              nvarchar(50)         null,
   QYZC                 nvarchar(50)         null,
   WXZC                 nvarchar(50)         null,
   KFZC                 nvarchar(50)         null,
   SY                   nvarchar(50)         null,
   CQDTFY               nvarchar(50)         null,
   GQFZLTQ              nvarchar(50)         null,
   DYSDSZC              nvarchar(50)         null,
   QTFLDZC              nvarchar(50)         null,
   FLDZCHJ              nvarchar(50)         null,
   ZCZJ                 nvarchar(50)         null,
   DQJK                 nvarchar(50)         null,
   XZYYHJK              nvarchar(50)         null,
   XSCKJTYCF            nvarchar(50)         null,
   CRZJ                 nvarchar(50)         null,
   JYXJRFZ              nvarchar(50)         null,
   YSJRFZ               nvarchar(50)         null,
   YFPJ                 nvarchar(50)         null,
   YFZK                 nvarchar(50)         null,
   YuSZK                nvarchar(50)         null,
   MCHGJRZCK            nvarchar(50)         null,
   YFSXFJYJ             nvarchar(50)         null,
   YFZGXC               nvarchar(50)         null,
   YJSF                 nvarchar(50)         null,
   YFLX                 nvarchar(50)         null,
   YFGL                 nvarchar(50)         null,
   QTYJK                nvarchar(50)         null,
   YFBZJ                nvarchar(50)         null,
   NBYFK                nvarchar(50)         null,
   QTYFK                nvarchar(50)         null,
   YTFY                 nvarchar(50)         null,
   YJLDFZ               nvarchar(50)         null,
   YFFBZK               nvarchar(50)         null,
   BXHTZBJ              nvarchar(50)         null,
   DLMMZQK              nvarchar(50)         null,
   DLCXZQK              nvarchar(50)         null,
   GJPZJS               nvarchar(50)         null,
   GNPZJS               nvarchar(50)         null,
   DYSY                 nvarchar(50)         null,
   YFDQZQ               nvarchar(50)         null,
   YNDDQDFLDFZ          nvarchar(50)         null,
   QTLDFZ               nvarchar(50)         null,
   LDFZHJ               nvarchar(50)         null,
   CQJQ                 nvarchar(50)         null,
   YFZQ                 nvarchar(50)         null,
   CQYFZQ               nvarchar(50)         null,
   ZXYFK                nvarchar(50)         null,
   YJFLDFZ              nvarchar(50)         null,
   CQDYSY               nvarchar(50)         null,
   DYSDSFZ              nvarchar(50)         null,
   QTFLDFZ              nvarchar(50)         null,
   FLDFZHJ              nvarchar(50)         null,
   FZHJ                 nvarchar(50)         null,
   SSZB                 nvarchar(50)         null,
   ZBGJ                 nvarchar(50)         null,
   JKCG                 nvarchar(50)         null,
   ZXCB                 nvarchar(50)         null,
   YYGJ                 nvarchar(50)         null,
   YBFXZB               nvarchar(50)         null,
   WQDDTZSS             nvarchar(50)         null,
   WFPLR                nvarchar(50)         null,
   NFPXJGL              nvarchar(50)         null,
   WBBBZSCE             nvarchar(50)         null,
   GSYMGSGDQYHJ         nvarchar(50)         null,
   SSGDQY               nvarchar(50)         null,
   SYZQY                nvarchar(50)         null,
   FZHSYZQY             nvarchar(50)         null,
   constraint PK_STOCKZCFZINFO primary key (Symbol)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StockZCFZInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StockZCFZInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '修改人：dox0
   修改时间：2017-11-11
   修改原因：新建表', 
   'user', @CurrentUser, 'table', 'StockZCFZInfo'
go