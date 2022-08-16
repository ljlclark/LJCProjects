// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCSQLUtilLibDAL
{
	// Provides Foreign Key specific data manipulation methods.
	/// <include path='items/ForeignKeyManager/*' file='Doc/ForeignKeyManager.xml'/>
	public class ForeignKeyManager : ObjectManager<ForeignKey, ForeignKeys>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ForeignKeyManagerC/*' file='Doc/ForeignKeyManager.xml'/>
		public ForeignKeyManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Constraint_Column_Usage", string schemaName = "Information_Schema")
			: base(dbServiceRef, dataConfigName, tableName, schemaName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames("table_catalog", "TableCatalog", caption: "Table Catalog");
			MapNames("table_schema", "TableSchema", caption: "Table Schema");
			MapNames("table_name", "SourceTable", caption: "Source Table");
			MapNames("column_name", "SourceColumn", caption: "Source Column");
			MapNames("constraint_name", "SourceConstraint", caption: "Source Constraint");
			MapNames("ordinal_position", "OrdinalPosition", caption: "Ordinal Position");

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add("TargetConstraint", caption: "Target Constraint");
			DataDefinition.Add("UpdateRule", caption: "Update Rule");
			DataDefinition.Add("DeleteRule", caption: "Delete Rule");
			DataDefinition.Add("TargetTable", caption: "Target Table");
			DataDefinition.Add("TargetColumn", caption: "Target Column");
		}
		#endregion

		#region Data Load/Retrieve Methods

		// Returns all Foreign and Primary key definitions.
		/// <include path='items/LoadAll/*' file='Doc/ForeignKeyManager.xml'/>
		public ForeignKeys LoadAll()
		{
			DbJoins dbJoins = GetLoadJoins();
			return Load(joins: dbJoins);
		}

		// Returns the Primary key definitions.
		/// <include path='items/LoadPrimaryKeys/*' file='Doc/ForeignKeyManager.xml'/>
		public ForeignKeys LoadPrimaryKeys(string tableName = null)
		{
			ForeignKeys retValue = null;

			DbFilters dbFilters = GetPrimaryKeyFilters(tableName);
			DbJoins dbJoins = GetLoadJoins();
			retValue = Load(filters: dbFilters, joins: dbJoins);
			return retValue;
		}

		// Returns the Foreign key definitions.
		/// <include path='items/LoadForeignKeys/*' file='Doc/ForeignKeyManager.xml'/>
		public ForeignKeys LoadForeignKeys(string tableName = null)
		{
			ForeignKeys retValue = null;

			DbJoins dbJoins = GetLoadJoins();
			DbFilters dbFilters = GetForeignKeyFilters(tableName);
			retValue = Load(filters: dbFilters, joins: dbJoins);
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load join object.
		/// <include path='items/GetLoadJoins/*' file='Doc/ForeignKeyManager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			//left join[Information_Schema].[Referential_Constraints]
			// on[Constraint_Column_Usage].[constraint_name]
			//  = [Referential_Constraints].[constraint_name]
			dbJoin = new DbJoin
			{
				TableName = "Referential_Constraints",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "constraint_name", "constraint_name" }},
				Columns = new DbColumns() {
					{ "unique_constraint_name", "TargetConstraint" },
					{ "update_rule", "UpdateRule" },
					{ "delete_rule", "DeleteRule" }}
			};
			retValue.Add(dbJoin);

			//left join [Information_Schema].[Key_Column_Usage] 
			// on[Referential_Constraints].[unique_constraint_name]
			//  = [Key_Column_Usage].[constraint_name]
			dbJoin = new DbJoin
			{
				TableName = "Key_Column_Usage",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "[Referential_Constraints].[unique_constraint_name]", "constraint_name" }},
				Columns = new DbColumns() {
					{ "table_name", "TargetTable", "TargetTable" },
					{ "column_name", "TargetColumn", "TargetColumn" },
					{ "ordinal_position", "OrdinalPosition", null, "Int32" }}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region Filters

		// Creates and returns the PrimaryKey Filters object.
		/// <include path='items/GetPrimaryKeyFilters/*' file='Doc/ForeignKeyManager.xml'/>
		public DbFilters GetPrimaryKeyFilters(string tableName = null)
		{
			DbFilters retValue;

			// where ([Key_Column_Usage].column_name is null)
			DbFilter dbFilter = new DbFilter();
			dbFilter.ConditionSet.Conditions.Add("Key_Column_Usage.column_name", null
				, "is null");
			retValue = new DbFilters {
				dbFilter};

			// and ([Constraint_Column_Usage].[TABLE_NAME] = 'tableName')
			if (tableName != null)
			{
				dbFilter = new DbFilter();
				dbFilter.ConditionSet.BooleanOperator = "or";
				dbFilter.ConditionSet.Conditions.Add("[Constraint_Column_Usage].[TABLE_NAME]"
					, $"'{tableName}'");
				retValue.Add(dbFilter);
			}
			return retValue;
		}

		// Creates and returns the ForeignKey Filters object.
		/// <include path='items/GetForeignKeyFilters/*' file='Doc/ForeignKeyManager.xml'/>
		public DbFilters GetForeignKeyFilters(string tableName = null)
		{
			DbFilters retValue;

			// where ([Key_Column_Usage].column_name is not null)
			DbFilter dbFilter = new DbFilter();
			dbFilter.ConditionSet.Conditions.Add("[Key_Column_Usage].column_name", null
				, "is not null");
			retValue = new DbFilters {
				dbFilter};

			// and ([Constraint_Column_Usage].[TABLE_NAME] = 'tableName'
			// or [Key_Column_Usage].[table_name] = 'tableName')
			if (tableName != null)
			{
				dbFilter = new DbFilter();
				dbFilter.ConditionSet.BooleanOperator = "or";
				dbFilter.ConditionSet.Conditions.Add("[Constraint_Column_Usage].[TABLE_NAME]"
					, $"'{tableName}'");
				dbFilter.ConditionSet.Conditions.Add("[Key_Column_Usage].[table_name]"
					, $"'{tableName}'");
				retValue.Add(dbFilter);
			}
			return retValue;
		}
		#endregion

		#region Class Data

		//SELECT
		//	ccu.table_catalog as TableCatalog, ccu.table_schema as TableSchema,
		//	ccu.table_name AS SourceTable, ccu.constraint_name AS SourceConstraint, ccu.column_name AS SourceColumn,
		//	kcu.table_name AS TargetTable, rc.unique_constraint_name as TargetConstraint, kcu.column_name AS TargetColumn,
		//	rc.update_rule as UpdateRule, rc.delete_rule as DeleteRule, kcu.ordinal_position as OrdinalPosition
		//FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu
		//INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
		//  ON ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
		//INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
		//  ON rc.UNIQUE_CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
		//where ccu.table_name = 'Unit' or kcu.table_name = 'Unit'
		//ORDER BY ccu.table_name, kcu.ordinal_position
		#endregion
	}
}
