USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DBMetaDataTable')
BEGIN
CREATE TABLE [dbo].[DBMetaDataTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](60) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[PluralName] [nvarchar](60) NOT NULL,
	[Caption][nvarchar](40) NOT NULL,
	[Description] [nvarchar](100) NULL,
	CONSTRAINT [PK_DBMetaDataTable]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DBMetaDataColumn')
BEGIN
CREATE TABLE [dbo].[DBMetaDataColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBMetaDataTableID] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[ColumnName] [nvarchar](60) NOT NULL,
	[PropertyName] [nvarchar](60) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[ShortCaption] [nvarchar](30) NOT NULL,
	[Caption] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[DataTypeName] [nvarchar](20) NOT NULL,
	[MaxLength] [int] NOT NULL DEFAULT(0),
	[AllowDBNull] [bit] NOT NULL DEFAULT(0),
	[AutoIncrement] [bit] NOT NULL DEFAULT(0),
	[DefaultValue] [nvarchar](60) NULL,
	[Unique] [bit] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_DBMetaDataColumn]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
ALTER TABLE [dbo].[DBMetaDataColumn] WITH CHECK
  ADD CONSTRAINT [FK_DBMetaDataColumn_DBMetaDataTable]
  FOREIGN KEY([DBMetaDataTableID])
  REFERENCES [dbo].[DBMetaDataTable] ([ID])
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DBMetaDataKeyType')
BEGIN
CREATE TABLE [dbo].[DBMetaDataKeyType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](15) NOT NULL,
	[Description] [nvarchar](40) NULL,
	CONSTRAINT [PK_DBMetaDataKeyType]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
END
GO

IF NOT EXISTS (select ID from DBMetaDataKeyType where Name = 'Primary')
insert into DBMetaDataKeyType
 (Name, Description)
 values('Primary', 'The primary key column');
IF NOT EXISTS (select ID from DBMetaDataKeyType where Name = 'Foreign')
insert into DBMetaDataKeyType
 (Name, Description)
 values('Foreign', 'The foreign key column');

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DBMetaDataKey')
BEGIN
CREATE TABLE [dbo].[DBMetaDataKey](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBMetaDataColumnID] [int] NOT NULL,
	[DBMetaDataKeyTypeID] [int] NOT NULL DEFAULT(1),
	[ToTableID] [int] NULL DEFAULT(0),
	[ToColumnID] [int] NULL DEFAULT(0),
	CONSTRAINT [PK_DBMetaDataKey]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
ALTER TABLE [dbo].[DBMetaDataKey] WITH CHECK
  ADD CONSTRAINT [FK_DBMetaDataKey_DBMetaDataColumn]
  FOREIGN KEY([DBMetaDataColumnID])
  REFERENCES [dbo].[DBMetaDataColumn] ([ID])
ALTER TABLE [dbo].[DBMetaDataKey] WITH CHECK
  ADD CONSTRAINT [FK_DBMetaDataKey_DBMetaDataKeyType]
  FOREIGN KEY([DBMetaDataKeyTypeID])
  REFERENCES [dbo].[DBMetaDataKeyType] ([ID])
END
GO
