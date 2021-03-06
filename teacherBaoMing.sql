USE [teacherBaoMing]
GO
/****** Object:  User [baoming]    Script Date: 2020/3/16 16:56:52 ******/
CREATE USER [baoming] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sas]    Script Date: 2020/3/16 16:56:52 ******/
CREATE USER [sas] FOR LOGIN [sas] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [sas]
GO
/****** Object:  Table [dbo].[SystemSet]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSet](
	[id] [int] NOT NULL,
	[NianFen] [int] NOT NULL,
	[BaoMingTimeStart] [smalldatetime] NOT NULL,
	[BaoMingTimeEnd] [smalldatetime] NOT NULL,
	[ZKZPrintTimeStart] [smalldatetime] NOT NULL,
	[ZKZPrintTimeEnd] [smalldatetime] NOT NULL,
	[BaoMing2TimeStart] [smalldatetime] NOT NULL,
	[BaoMing2TimeEnd] [smalldatetime] NOT NULL,
	[NoLoginTimeStart] [smalldatetime] NULL,
	[NoLoginTimeEnd] [smalldatetime] NULL,
 CONSTRAINT [PK_SystemSet] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_parameter]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_parameter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[paraGroupCode] [varchar](20) NOT NULL,
	[paraName] [nvarchar](20) NOT NULL,
	[paraValue] [varchar](10) NOT NULL,
	[paraDefault] [bit] NOT NULL,
	[paraSort] [int] NOT NULL,
	[paraRemark] [nvarchar](50) NULL,
 CONSTRAINT [PK_t_parameter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Role]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Role](
	[Role] [nvarchar](20) NOT NULL,
	[GangweiCodes] [varchar](80) NULL,
 CONSTRAINT [PK_t_Role] PRIMARY KEY CLUSTERED 
(
	[Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_admin]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin](
	[UserName] [nvarchar](20) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](20) NULL,
	[IDCardPIC] [nvarchar](200) NULL,
 CONSTRAINT [PK_tb_admin_1] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_gangwei]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_gangwei](
	[GangweiCode] [int] NOT NULL,
	[GangWeiName] [nvarchar](50) NOT NULL,
	[bUsed] [bit] NOT NULL,
 CONSTRAINT [PK_tb_gangwei] PRIMARY KEY CLUSTERED 
(
	[GangweiCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_userinfo]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_userinfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[XingMing] [nvarchar](50) NOT NULL,
	[Birthday] [nvarchar](6) NULL,
	[ShengYuanDi] [nvarchar](50) NULL,
	[BiYeSchool] [nvarchar](50) NULL,
	[AuditCode] [char](1) NOT NULL,
	[AuditFeedback] [nvarchar](50) NULL,
	[XueKeCode] [int] NULL,
	[XueLiCode] [varchar](2) NULL,
	[BiYeTime] [datetime2](7) NULL,
	[QuanRiZhi] [nvarchar](50) NULL,
	[ZhuanYe] [nvarchar](50) NULL,
	[ShiFanLei] [nvarchar](50) NULL,
	[ZiGeZheng] [nvarchar](50) NULL,
	[DuSheng] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[ZiGeZhengCode] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[XueQianWork] [nvarchar](50) NULL,
	[BiYeZhengShuCode] [nvarchar](50) NULL,
	[SexCode] [varchar](1) NULL,
	[PTHLevel] [nvarchar](10) NULL,
	[PTHZSNo] [nvarchar](20) NULL,
	[IDPhoto] [nvarchar](200) NULL,
	[MinZuCode] [varchar](2) NULL,
	[PoliticalOrientationCode] [varchar](2) NULL,
	[ZhiYeCode] [varchar](2) NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[picResidenceBooklet] [nvarchar](200) NULL,
	[picDiploma] [nvarchar](200) NULL,
	[picArchiveCertificate] [nvarchar](200) NULL,
	[picZiGeZheng] [nvarchar](200) NULL,
	[picPTH] [nvarchar](200) NULL,
	[picNewGraduates] [nvarchar](200) NULL,
	[picKindergartenCommitment] [nvarchar](200) NULL,
 CONSTRAINT [PK_tb_userinfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_xueke]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_xueke](
	[XueKeCode] [int] NOT NULL,
	[XueKeName] [nvarchar](50) NOT NULL,
	[GangWeiCode] [int] NOT NULL,
	[kskmCode] [varchar](5) NOT NULL,
 CONSTRAINT [PK_tb_xueke] PRIMARY KEY CLUSTERED 
(
	[XueKeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_zunkaozheng]    Script Date: 2020/3/16 16:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_zunkaozheng](
	[zkzCode] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[zuoweiCode] [nvarchar](10) NULL,
	[kaoDian] [nvarchar](50) NULL,
	[publicSubject] [nvarchar](50) NULL,
	[privateSubject] [nvarchar](50) NULL,
	[kaoShiTime] [nvarchar](50) NULL,
	[kaoShiDate] [nvarchar](50) NULL,
	[shiChangCode] [nvarchar](50) NULL,
	[kaoDian1] [nvarchar](50) NULL,
	[shiChangCode1] [nvarchar](50) NULL,
	[zuoweiCode1] [nvarchar](10) NULL,
	[kaoShiDate1] [nvarchar](50) NULL,
	[kaoShiTime1] [nvarchar](50) NULL,
 CONSTRAINT [PK_tb_zunkaozheng] PRIMARY KEY CLUSTERED 
(
	[zkzCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[t_parameter] ADD  CONSTRAINT [DF_t_parameter_paraDefault]  DEFAULT ((0)) FOR [paraDefault]
GO
ALTER TABLE [dbo].[tb_gangwei] ADD  CONSTRAINT [DF_tb_gangwei_bUsed_1]  DEFAULT ((1)) FOR [bUsed]
GO
ALTER TABLE [dbo].[tb_userinfo] ADD  CONSTRAINT [DF_tb_userinfo_AuditCode]  DEFAULT ((0)) FOR [AuditCode]
GO
ALTER TABLE [dbo].[tb_userinfo] ADD  CONSTRAINT [DF_tb_userinfo_CreateDT]  DEFAULT (getdate()) FOR [CreateDT]
GO
ALTER TABLE [dbo].[tb_admin]  WITH CHECK ADD  CONSTRAINT [FK_tb_admin_t_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[t_Role] ([Role])
GO
ALTER TABLE [dbo].[tb_admin] CHECK CONSTRAINT [FK_tb_admin_t_Role]
GO
ALTER TABLE [dbo].[tb_userinfo]  WITH CHECK ADD  CONSTRAINT [FK_tb_userinfo_tb_admin] FOREIGN KEY([UserName])
REFERENCES [dbo].[tb_admin] ([UserName])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tb_userinfo] CHECK CONSTRAINT [FK_tb_userinfo_tb_admin]
GO
ALTER TABLE [dbo].[tb_userinfo]  WITH CHECK ADD  CONSTRAINT [FK_tb_userinfo_tb_xueke] FOREIGN KEY([XueKeCode])
REFERENCES [dbo].[tb_xueke] ([XueKeCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_userinfo] CHECK CONSTRAINT [FK_tb_userinfo_tb_xueke]
GO
ALTER TABLE [dbo].[tb_xueke]  WITH CHECK ADD  CONSTRAINT [FK_tb_xueke_tb_gangwei] FOREIGN KEY([GangWeiCode])
REFERENCES [dbo].[tb_gangwei] ([GangweiCode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tb_xueke] CHECK CONSTRAINT [FK_tb_xueke_tb_gangwei]
GO
ALTER TABLE [dbo].[tb_zunkaozheng]  WITH CHECK ADD  CONSTRAINT [FK_tb_zunkaozheng_tb_userinfo] FOREIGN KEY([UserName])
REFERENCES [dbo].[tb_userinfo] ([UserName])
GO
ALTER TABLE [dbo].[tb_zunkaozheng] CHECK CONSTRAINT [FK_tb_zunkaozheng_tb_userinfo]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'BaoMingTimeStart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'BaoMingTimeEnd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'准考证打印开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'ZKZPrintTimeStart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'准考证打印结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'ZKZPrintTimeEnd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'再次报名开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'BaoMing2TimeStart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'再次报名结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'BaoMing2TimeEnd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁止登录开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'NoLoginTimeStart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁止登录结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemSet', @level2type=N'COLUMN',@level2name=N'NoLoginTimeEnd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_parameter', @level2type=N'COLUMN',@level2name=N'paraGroupCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_parameter', @level2type=N'COLUMN',@level2name=N'paraName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_parameter', @level2type=N'COLUMN',@level2name=N'paraValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为默认参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_parameter', @level2type=N'COLUMN',@level2name=N'paraDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_parameter', @level2type=N'COLUMN',@level2name=N'paraSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_parameter', @level2type=N'COLUMN',@level2name=N'paraRemark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理岗位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_Role'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_admin', @level2type=N'COLUMN',@level2name=N'IDCardPIC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_gangwei', @level2type=N'COLUMN',@level2name=N'bUsed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核结果Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'AuditCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核反馈' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'AuditFeedback'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学历Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'XueLiCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'SexCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'普通话等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'PTHLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'普通话等级证书号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'PTHZSNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件人像照图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'IDPhoto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'民族Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'MinZuCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政治面貌Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'PoliticalOrientationCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职业Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'ZhiYeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'户口簿照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picResidenceBooklet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'毕业证书照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picDiploma'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'档案存档证明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picArchiveCertificate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教师资格证照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picZiGeZheng'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'普通话证书照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picPTH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应届毕业的学校证明照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picNewGraduates'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'幼儿园承诺书照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_userinfo', @level2type=N'COLUMN',@level2name=N'picKindergartenCommitment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_xueke', @level2type=N'COLUMN',@level2name=N'XueKeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_xueke', @level2type=N'COLUMN',@level2name=N'GangWeiCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试科目代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_xueke', @level2type=N'COLUMN',@level2name=N'kskmCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'准考证号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'zkzCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'座位号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'zuoweiCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础知识考点(试场)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'kaoDian'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'教育基础知识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'publicSubject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学科专业科目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'privateSubject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'kaoShiTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'kaoShiDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'试场号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'shiChangCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学科专业考点(试场)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'kaoDian1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'试场号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'shiChangCode1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'座位号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'zuoweiCode1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'kaoShiDate1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng', @level2type=N'COLUMN',@level2name=N'kaoShiTime1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'准考证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_zunkaozheng'
GO
