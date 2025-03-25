/* DataUtilityData.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

EXEC sp_DataModuleData;
EXEC sp_DataTableData;
EXEC sp_DataColumnData;
EXEC sp_DataKeyData;
GO

