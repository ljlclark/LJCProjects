USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
drop table ViewOrderBy;
drop table ViewCondition;
drop table ViewConditionSet;
drop table ViewFilter;
drop table ViewJoinColumn;
drop table ViewJoinOn;
drop table ViewJoin;
drop table ViewGridColumn;
drop table ViewColumn;
drop table DataType;
drop table ViewData;
drop table ViewTable;
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewTable')
BEGIN
CREATE TABLE [dbo].[ViewTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	CONSTRAINT [PK_ViewTable]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewData')
BEGIN
CREATE TABLE [dbo].[ViewData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewTableID] [int] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	CONSTRAINT [PK_DBView]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewTable]
	FOREIGN KEY ([ViewTableID])
	REFERENCES [dbo].[ViewTable]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DataType')
BEGIN
CREATE TABLE [dbo].[DataType](
  [DataTypeID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [SQLName] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_DataType] PRIMARY KEY CLUSTERED
  (
    [DataTypeID] ASC
  )
)
END
GO

IF NOT EXISTS (select Name from DataType where Name = 'Boolean')
insert into DataType
 (Name, SQLName, Description)
 values('Boolean', 'bit', 'True or false. 0 or 1.')

IF NOT EXISTS (select Name from DataType where Name = 'Byte')
insert into DataType
 (Name, SQLName, Description)
 values('Byte', 'tinyint', 'An eight bit value.');

IF NOT EXISTS (select Name from DataType where Name = 'DateTime')
insert into DataType
 (Name, SQLName, Description)
 values('DateTime', 'datetime', 'A Date and Time value.');

IF NOT EXISTS (select Name from DataType where Name = 'Decimal')
insert into DataType
 (Name, SQLName, Description)
 values('Decimal', 'decimal', 'A decimal value.');

IF NOT EXISTS (select Name from DataType where Name = 'Double')
insert into DataType
 (Name, SQLName, Description)
 values('Double', 'float', 'A double precision value.');

IF NOT EXISTS (select Name from DataType where Name = 'Int16')
insert into DataType
 (Name, SQLName, Description)
 values('Int16', 'smallint', 'A two byte integer.');

IF NOT EXISTS (select Name from DataType where Name = 'Int32')
insert into DataType
 (Name, SQLName, Description)
 values('Int32', 'int', 'A four byte integer.');

IF NOT EXISTS (select Name from DataType where Name = 'Int64')
insert into DataType
 (Name, SQLName, Description)
 values('Int64', 'bigint', 'An eight byte integer.');

IF NOT EXISTS (select Name from DataType where Name = 'Single')
insert into DataType
 (Name, SQLName, Description)
 values('Single', 'real', 'A single precision value.');

IF NOT EXISTS (select Name from DataType where Name = 'String')
insert into DataType
 (Name, SQLName, Description)
 values('String', 'nvarchar', 'A string value.');
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewColumn')
BEGIN
CREATE TABLE [dbo].[ViewColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewDataID] [int] NOT NULL,
	[Sequence] [int] NULL,
	[ColumnName] [nvarchar](60) NOT NULL,
	[PropertyName] [nvarchar](60) NULL,
	[RenameAs] [nvarchar](60) NULL,
	[Caption] [nvarchar](60) NULL,
	[DataTypeName] [nvarchar](60) NOT NULL,
	[Value] [nvarchar](60) NULL,
	[Width] [int] NULL,
	[IsPrimaryKey] [bit] NOT NULL,
	CONSTRAINT [PK_ViewColumn]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewColumnDBView]
	FOREIGN KEY ([ViewDataID])
	REFERENCES [dbo].[ViewData]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewGridColumn')
BEGIN
CREATE TABLE [dbo].[ViewGridColumn](
	[ViewDataID] [int] NOT NULL,
	[ViewColumnID] [int] NOT NULL,
	[Sequence] [int] NULL,
	[Caption] [nvarchar](60) NULL,
	[Width] [int] NULL,
	CONSTRAINT [PK_ViewGridColumn]
	PRIMARY KEY CLUSTERED (
	  [ViewDataID], [ViewColumnID] ASC),
	CONSTRAINT [FK_ViewGridColumnDBView]
	FOREIGN KEY ([ViewDataID])
	REFERENCES [dbo].[ViewData]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_ViewGridColumnViewColumn]
	FOREIGN KEY ([ViewColumnID])
	REFERENCES [dbo].[ViewColumn]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewJoin')
BEGIN
CREATE TABLE [dbo].[ViewJoin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewDataID] [int] NOT NULL,
	[TableName] [nvarchar](60) NOT NULL,
	[JoinType] [nvarchar](60) NULL,
	[TableAlias] [nvarchar](60) NULL,
	CONSTRAINT [PK_ViewJoin]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewJoinDBView]
	FOREIGN KEY ([ViewDataID])
	REFERENCES [dbo].[ViewData]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewJoinOn')
BEGIN
CREATE TABLE [dbo].[ViewJoinOn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewJoinID] [int] NOT NULL,
	[FromColumnName] [nvarchar](60) NOT NULL,
	[ToColumnName] [nvarchar](60) NOT NULL,
	[JoinOnOperator] [nvarchar](5) NULL,
	CONSTRAINT [PK_ViewJoinOn]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewJoinOnViewJoin]
	FOREIGN KEY ([ViewJoinID])
	REFERENCES [dbo].[ViewJoin]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewJoinColumn')
BEGIN
CREATE TABLE [dbo].[ViewJoinColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewJoinID] [int] NOT NULL,
	[ColumnName] [nvarchar](60) NOT NULL,
	[PropertyName] [nvarchar](60) NULL,
	[RenameAs] [nvarchar](60) NULL,
	[Caption] [nvarchar](60) NULL,
	[DataTypeName] [nvarchar](60) NOT NULL,
	[Value] [nvarchar](60) NULL,
	CONSTRAINT [PK_ViewJoinColumn]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewJoinColumnViewJoin]
	FOREIGN KEY ([ViewJoinID])
	REFERENCES [dbo].[ViewJoin]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewFilter')
BEGIN
CREATE TABLE [dbo].[ViewFilter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewDataID] [int] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[BooleanOperator] [nvarchar](3) not null,
	CONSTRAINT [PK_ViewFilter]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewFilterDBView]
	FOREIGN KEY ([ViewDataID])
	REFERENCES [dbo].[ViewData]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewConditionSet')
BEGIN
CREATE TABLE [dbo].[ViewConditionSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewFilterID] [int] NOT NULL,
	[BooleanOperator] [nvarchar](3) NOT NULL,
	CONSTRAINT [PK_ViewConditionSet]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewConditionSetViewFilter]
	FOREIGN KEY ([ViewFilterID])
	REFERENCES [dbo].[ViewFilter]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewCondition')
BEGIN
CREATE TABLE [dbo].[ViewCondition](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewConditionSetID] [int] NOT NULL,
	[FirstValue] [nvarchar](60) NOT NULL,
	[SecondValue] [nvarchar](60) NOT NULL,
	[ComparisonOperator] [nvarchar](10) NULL,
	CONSTRAINT [PK_ViewCondition]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_ViewConditionViewConditionSet]
	FOREIGN KEY ([ViewConditionSetID])
	REFERENCES [dbo].[ViewConditionSet]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ViewOrderBy')
BEGIN
CREATE TABLE [dbo].[ViewOrderBy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ViewDataID] [int] NOT NULL,
	[ColumnName] [nvarchar] (60) NOT NULL,
	CONSTRAINT [PK_DBOrderBy]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
ALTER TABLE [dbo].[ViewOrderBy] WITH CHECK
  ADD CONSTRAINT [FK_ViewOrderByDBView]
  FOREIGN KEY([ViewDataID])
  REFERENCES [dbo].[ViewData] ([ID])
END
GO
