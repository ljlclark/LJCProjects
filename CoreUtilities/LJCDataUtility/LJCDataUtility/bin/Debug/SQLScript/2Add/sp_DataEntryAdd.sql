/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataEntryAdd.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataEntryAdd]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataEntryAdd];
GO
CREATE PROCEDURE [dbo].[sp_DataEntryAdd]
  @dataSiteName nvarchar(60),
  @entryTime datetime,
  @entryType varchar(10),
  @dataConfigName varchar(60),
  @publishTime datetime,
  @entryData varchar(4000)
AS
BEGIN
DECLARE @dataSiteID bigint = (SELECT ID FROM DataSite
 WHERE Name = @dataSiteName);

IF @dataSiteID IS NOT NULL
IF NOT EXISTS(SELECT ID FROM DataEntry
 WHERE Name = @name)
  INSERT INTO DataEntry
    (SourceSiteID, EntryTime, ModuleID, TableID, EntryType, DataConfigName
     , PublishTime, EntryData)
    VALUES(@dataSiteID, @entryTime, @entryType, @dataConfigName, @publishTime
     , @entryData);
END