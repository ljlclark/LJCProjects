-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'AppManagerUser')
BEGIN
CREATE TABLE [dbo].[AppManagerUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NULL,
	[UserId] [nvarchar](60) NULL,
	CONSTRAINT [PK_Person]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC)
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'AppProgram')
BEGIN
CREATE TABLE [dbo].[AppProgram](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](40) NOT NULL,
	[Title] [nvarchar](60) NOT NULL,
	CONSTRAINT [PK_AppProgram]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC)
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'AppModule')
BEGIN
CREATE TABLE [dbo].[AppModule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppProgram_Id] [int] NOT NULL,
	[TypeName] [nvarchar](60) NOT NULL,
	[Title] [nvarchar](60) NOT NULL,
	CONSTRAINT [PK_AppModule]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_AppModuleAppPogram]
	FOREIGN KEY ([AppProgram_Id])
	REFERENCES [dbo].[AppProgram]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UserAppProgram')
BEGIN
CREATE TABLE [dbo].[UserAppProgram](
	[AppManagerUser_Id] [int] NOT NULL,
	[AppProgram_Id] [int] NOT NULL,
	[Active] [bit] not null,
	CONSTRAINT [PK_UserAppProgram]
	PRIMARY KEY CLUSTERED (
	  [AppManagerUser_Id], [AppProgram_Id] ASC),
	CONSTRAINT [FK_UserAppProgramUser]
	FOREIGN KEY ([AppManagerUser_Id])
	REFERENCES [dbo].[AppManagerUser]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UserAppProgramProgram]
	FOREIGN KEY ([AppProgram_Id])
	REFERENCES [dbo].[AppProgram]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UserAppModule')
BEGIN
CREATE TABLE [dbo].[UserAppModule](
	[AppManagerUser_Id] [int] NOT NULL,
	[AppProgram_Id] [int] NOT NULL,
	[AppModule_Id] [int] NOT NULL,
	[Active] [bit] not null,
	CONSTRAINT [PK_UserAppModule]
	PRIMARY KEY CLUSTERED (
	  [AppManagerUser_Id], [AppProgram_Id], [AppModule_Id] ASC),
	CONSTRAINT [FK_UserAppModuleUser]
	FOREIGN KEY ([AppManagerUser_Id])
	REFERENCES [dbo].[AppManagerUser]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UserAppModuleProgram]
	FOREIGN KEY ([AppProgram_Id])
	REFERENCES [dbo].[AppProgram]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UserAppModuleModule]
	FOREIGN KEY ([AppModule_Id])
	REFERENCES [dbo].[AppModule]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO
