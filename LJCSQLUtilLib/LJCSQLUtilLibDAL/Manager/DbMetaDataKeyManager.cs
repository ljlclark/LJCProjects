// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCSQLUtilLibDAL
{
	// Provides DBMetaDataKey specific data manipulation methods.
	/// <include path='items/DbMetaDataKeyManager/*' file='Doc/DbMetaDataKeyManager.xml'/>
	public class DbMetaDataKeyManager
		: ObjectManager<DbMetaDataKey, DbMetaDataKeys>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DbMetaDataKeyManagerC/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKeyManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DBMetaDataKey") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(DbMetaDataKey.ColumnID, caption: "ID");
			MapNames(DbMetaDataKey.ColumnDbMetaDataColumnID
				, DbMetaDataKey.PropertyDbMetaDataColumnID, caption: "Column ID");
			MapNames(DbMetaDataKey.ColumnDbMetaDataKeyTypeID
				, DbMetaDataKey.PropertyDbMetaDataKeyTypeID, caption: "Key Type ID");
			MapNames(DbMetaDataKey.ColumnToTableID, caption: "To Table");
			MapNames(DbMetaDataKey.ColumnToColumnID, caption: "To Column");

			// Add Calculated and Join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(DbMetaDataKey.ColumnColumnName
			 , caption: "Column Name");
			DataDefinition.Add(DbMetaDataKey.ColumnToTableName
			 , caption: "To Table Name");
			DataDefinition.Add(DbMetaDataKey.ColumnToColumnName
			 , caption: "To Column Name");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					DbMetaDataKey.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					DbMetaDataKey.ColumnDbMetaDataColumnID,
					DbMetaDataKey.ColumnDbMetaDataKeyTypeID
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records.
		/// <include path='items/LoadPrimaryKeys/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKeys LoadPrimaryKeys()
		{
			DbJoins dbJoins = GetLoadJoins();
			return Load(null, joins: dbJoins);
		}

		// Loads a collection of data records.
		/// <include path='items/LoadForeignKey/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKeys LoadForeignKey(int columnID)
		{
			var keyColumns = GetForeignKey(columnID);
			DbJoins dbJoins = GetLoadJoins();
			return Load(keyColumns, joins: dbJoins);
		}

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByDescription/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKeys LoadByDescription()
		{
			DbJoins dbJoins = GetLoadJoins();
			SetOrderByDescription();
			return Load(null, joins: dbJoins);
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbMetaDataKey RetrieveWithID(int id, List<string> propertyNames = null)
		{
			DbJoins dbJoins = GetLoadJoins();
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames, joins: dbJoins);
		}

		// Retrieves a data record with the supplied values.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKey RetrieveWithUniqueKey(int columnID
			, int keyTypeID)
		{
			var keyColumns = new DbColumns()
			{
				{ DbMetaDataKey.ColumnDbMetaDataColumnID, columnID },
				{ DbMetaDataKey.ColumnDbMetaDataKeyTypeID, keyTypeID }
			};
			return Retrieve(keyColumns);
		}
		#endregion

		#region Save record methods.

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public bool SaveData(DbMetaDataKey mdKey)
		{
			bool retValue = true;

			if (mdKey.ID > 0)
			{
				// Update record.
				DbMetaDataKey retrieveData = RetrieveWithID(mdKey.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					var keyColumns = GetIDKey(retrieveData.ID);
					Update(mdKey, keyColumns);
					if (AffectedCount < 1)
					{
						retValue = false;
					}
				}
			}
			else
			{
				// Create record.
				if (null == AddData(mdKey))
				{
					retValue = false;
				}
			}
			return retValue;
		}

		// Adds a data record.
		/// <include path='items/AddData/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKey AddData(DbMetaDataKey mdKey)
		{
			DbMetaDataKey retValue;

			retValue = Add(mdKey);
			if (retValue != null)
			{
				mdKey.ID = retValue.ID;
			}
			return retValue;
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ DbMetaDataKey.ColumnID, id }
			};
			return retValue;
		}

		// Get the Primary key record.
		/// <include path='items/GetPrimaryKey/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbMetaDataKey GetPrimaryKey(int columnID)
		{
			//where [DbMetaDataKey].[DbMetaDataColumnID] = 1
			// and [DbMetaDataKey].[DbMetaDataKeyTypeID] = 1
			DbMetaDataKey retValue = new DbMetaDataKey()
			{
				DbMetaDataColumnID = columnID,
				DbMetaDataKeyTypeID = 1
			};
			return retValue;
		}

		// Get the Foreign key record.
		/// <include path='items/GetForeignKey/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbColumns GetForeignKey(int columnID)
		{
			//where [DbMetaDataKey].[DbMetaDataColumnID] = 6
			// and [DbMetaDataKey].[DbMetaDataKeyTypeID] = 2
			var retValue = new DbColumns()
			{
				{ DbMetaDataKey.ColumnDbMetaDataColumnID, columnID },
				{ DbMetaDataKey.ColumnDbMetaDataKeyTypeID, 2 }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load join object.
		/// <include path='items/GetLoadJoins/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			string alias;
			string joinFromColumnName;
			string joinToColumnName;
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the main table object
			// to recieve the join values.
			// The RenameAs property is required if there is another table column
			// with the same name.
			// DbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 

			// 1.
			//[fc].[ColumnName],
			//left join [DBMetaDataColumn] as fc
			// on [DBMetaDataKey].[DBMetaDataColumnID] = fc.ID
			alias = "fc";
			joinToColumnName = CreateColumnName(alias, DbMetaDataColumn.ColumnID);
			dbJoin = new DbJoin
			{
				TableName = DbMetaDataColumn.TableName,
				TableAlias = alias,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ DbMetaDataKey.ColumnDbMetaDataColumnID, joinToColumnName }},
				Columns = new DbColumns() {
					{ DbMetaDataColumn.ColumnColumnName }}
			};
			retValue.Add(dbJoin);

			// 2.
			//[ft].[Name] as FromTableName,
			//left join [DBMetaDataTable] as [ft]
			//	on [fc].[DBMetaDataTableID] = [ft].[ID]
			alias = "ft";
			joinFromColumnName = CreateColumnName("fc"
				, DbMetaDataColumn.ColumnDbMetaDataTableID);
			joinToColumnName = CreateColumnName(alias, DbMetaDataTable.ColumnID);
			dbJoin = new DbJoin
			{
				TableName = DbMetaDataTable.TableXName,
				TableAlias = alias,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ joinFromColumnName, joinToColumnName }},
				Columns = new DbColumns() {
					{ DbMetaDataTable.ColumnName, "FromTableName", "FromTableName" }}
			};
			retValue.Add(dbJoin);

			// 3.
			//[tc].[ColumnName] as ToColumnName
			//left join [DBMetaDataColumn] as [tc]
			//	on [DBMetaDataKey].[ToColumnID] = tc].[ID]
			alias = "tc";
			joinToColumnName = CreateColumnName(alias, DbMetaDataColumn.ColumnID);
			dbJoin = new DbJoin
			{
				TableName = DbMetaDataColumn.TableName,
				TableAlias = alias,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ DbMetaDataKey.ColumnToColumnID, joinToColumnName }},
				Columns = new DbColumns() {
					{ DbMetaDataColumn.ColumnColumnName, "ToColumnName", "ToColumnName" }}
			};
			retValue.Add(dbJoin);

			// 4.
			//[tt].[Name] as ToTableName,
			//left join [DBMetaDataTable] as [tt]
			//	on [tc].[DBMetaDataTableID] = [tt].[ID]
			alias = "tt";
			joinFromColumnName = CreateColumnName("tc"
				, DbMetaDataColumn.ColumnDbMetaDataTableID);
			joinToColumnName = CreateColumnName(alias, DbMetaDataTable.ColumnID);
			dbJoin = new DbJoin
			{
				TableName = DbMetaDataTable.TableXName,
				TableAlias = alias,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ joinFromColumnName, joinToColumnName }},
				Columns = new DbColumns() {
					{ DbMetaDataTable.ColumnName, "ToTableName", "ToTableName" }}
			};
			retValue.Add(dbJoin);
			return retValue;
		}

		// 
		private string CreateColumnName(string alias, string columnName)
		{
			return $"[{alias}].[{columnName}]";
		}
		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetTextFilters/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public DbFilters GetTextFilters()
		{
			DbFilters retValue = null;

			DbFilter dbFilter = new DbFilter();
			//dbFilter.ConditionSet.Conditions.Add(DbMetaDataKey.ColumnDescription, "'Text'");
			//retValue = new DbFilters {
			//	dbFilter};
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderByDescription/*' file='Doc/DbMetaDataKeyManager.xml'/>
		public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
		{
			//DbMetaDataKey.ColumnDescription
		};

		#endregion
	}
}
