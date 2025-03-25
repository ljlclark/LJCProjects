/* DataUtilityDropFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

EXEC sp_DataTableDropFK;
EXEC sp_DataColumnDropFK;
EXEC sp_DataKeyDropFK;
GO
