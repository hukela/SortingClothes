USE [ClothesServer]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 07/17/2020 10:56:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Place]    Script Date: 07/17/2020 10:56:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Place](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_site] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clothes]    Script Date: 07/17/2020 10:56:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clothes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [int] NOT NULL,
	[place] [int] NOT NULL,
	[seat] [varchar](20) NULL,
	[describe] [varchar](200) NULL,
	[grade] [tinyint] NULL,
	[condition] [char](1) NULL,
	[changeTime] [date] NULL,
 CONSTRAINT [PK_clothes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大概位置，地点id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'place'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'具体位置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'seat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'describe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表示等级，范围1-3，3，表示经常使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'a:干净，b:需要清洗，c:正在使用，d:其它。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'condition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据变更时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'COLUMN',@level2name=N'changeTime'
GO
/****** Object:  Check [CK_clothes_condition]    Script Date: 07/17/2020 10:56:57 ******/
ALTER TABLE [dbo].[Clothes]  WITH CHECK ADD  CONSTRAINT [CK_clothes_condition] CHECK  (([condition]='a' OR [condition]='b' OR [condition]='c' OR [condition]='d'))
GO
ALTER TABLE [dbo].[Clothes] CHECK CONSTRAINT [CK_clothes_condition]
GO
/****** Object:  Check [CK_clothes_grade]    Script Date: 07/17/2020 10:56:57 ******/
ALTER TABLE [dbo].[Clothes]  WITH CHECK ADD  CONSTRAINT [CK_clothes_grade] CHECK  (([grade]<=(3) AND [grade]>=(1)))
GO
ALTER TABLE [dbo].[Clothes] CHECK CONSTRAINT [CK_clothes_grade]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clothes', @level2type=N'CONSTRAINT',@level2name=N'CK_clothes_grade'
GO
/****** Object:  ForeignKey [FK_clothes_site]    Script Date: 07/17/2020 10:56:57 ******/
ALTER TABLE [dbo].[Clothes]  WITH CHECK ADD  CONSTRAINT [FK_clothes_site] FOREIGN KEY([place])
REFERENCES [dbo].[Place] ([id])
GO
ALTER TABLE [dbo].[Clothes] CHECK CONSTRAINT [FK_clothes_site]
GO
/****** Object:  ForeignKey [FK_clothes_Type]    Script Date: 07/17/2020 10:56:57 ******/
ALTER TABLE [dbo].[Clothes]  WITH CHECK ADD  CONSTRAINT [FK_clothes_Type] FOREIGN KEY([type])
REFERENCES [dbo].[Type] ([id])
GO
ALTER TABLE [dbo].[Clothes] CHECK CONSTRAINT [FK_clothes_Type]
GO
