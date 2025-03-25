/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataSiteAdd.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataSiteAdd]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataSiteAdd];
GO
CREATE PROCEDURE [dbo].[sp_DataSiteAdd]
  @name varchar(60),
  @description varchar(80),
  @siteURL varchar(100)
AS
BEGIN
IF NOT EXISTS(SELECT ID FROM DataSite
 WHERE Name = @name)
  INSERT INTO DataSite
    (Name, Description, SiteURL)
    VALUES(@name, @description, @siteURL);
END