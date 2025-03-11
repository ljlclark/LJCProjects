SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P'
-- AND OBJECT_ID = OBJECT_ID('dbo.sp_GetForeignKeys'))
--   exec('CREATE PROCEDURE [dbo].[sp_GetForeignKeys] AS BEGIN SET NOCOUNT ON; END')
--GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P'
 AND OBJECT_ID = OBJECT_ID('dbo.sp_GetForeignKeys'))
  DROP PROCEDURE [dbo].[sp_GetForeignKeys]
GO

CREATE PROCEDURE [dbo].[sp_GetForeignKeys] 
	@TableName nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	select 
	 [Constraint_Column_Usage].[TABLE_CATALOG], 
	 [Constraint_Column_Usage].[TABLE_SCHEMA], 
	 [Constraint_Column_Usage].[TABLE_NAME], 
	 [Constraint_Column_Usage].[COLUMN_NAME], 
	 [Constraint_Column_Usage].[CONSTRAINT_CATALOG], 
	 [Constraint_Column_Usage].[CONSTRAINT_SCHEMA], 
	 [Constraint_Column_Usage].[CONSTRAINT_NAME], 
	 [Referential_Constraints].[unique_constraint_name], 
	 [Referential_Constraints].[update_rule], 
	 [Referential_Constraints].[delete_rule], 
	 [Key_Column_Usage].[table_name] as TargetTable, 
	 [Key_Column_Usage].[column_name] as TargetColumn, 
	 [Key_Column_Usage].[ordinal_position] 
	from [Information_Schema].[Constraint_Column_Usage] 
	left join [Information_Schema].[Referential_Constraints] 
	 on  [Constraint_Column_Usage].[constraint_name] = [Referential_Constraints].[constraint_name] 
	left join [Information_Schema].[Key_Column_Usage] 
	 on  [Referential_Constraints].[unique_constraint_name] = [Key_Column_Usage].[constraint_name] 
	where ([Key_Column_Usage].[column_name] is not null) 
	 and ([Constraint_Column_Usage].[TABLE_NAME] = @TableName or [Key_Column_Usage].[table_name] = @TableName) 
END
GO
