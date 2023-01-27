// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataTableManager.cs
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCSQLUtilLibDAL
{
	// Provides DbMetaDataTable specific data manipulation methods.
	/// <include path='items/DbMetaDataTableManager/*' file='Doc/DbMetaDataTableManager.xml'/>
	public class DbMetaDataTableManager
		: ObjectManager<DbMetaDataTable, DbMetaDataTables>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DbMetaDataTableManagerC/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbMetaDataTableManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DBMetaDataTable") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					DbMetaDataTable.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					DbMetaDataTable.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByDescription/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbMetaDataTables LoadByDescription()
		{
			DbJoins dbJoins = GetLoadJoins();
			SetOrderByDescription();
			return Load(null, joins: dbJoins);
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbMetaDataTable RetrieveWithID(int id
			, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}

		// Retrieves a data record with the supplied values.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbMetaDataTable RetrieveWithUniqueKey(string tableName)
		{
			DbMetaDataTable retValue = null;

			if (null != tableName)
			{
				var keyColumns = GetTableNameKey(tableName);
				retValue = Retrieve(keyColumns);
			}
			return retValue;
		}
		#endregion

		#region Save record methods.

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/DbMetaDataTableManager.xml'/>
		public bool SaveData(DbMetaDataTable mdTable)
		{
			bool retValue = true;

			if (mdTable.ID > 0)
			{
				// Update record.
				DbMetaDataTable retrieveData = RetrieveWithID(mdTable.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					var keyColumns = GetIDKey(retrieveData.ID);
					Update(mdTable, keyColumns);
					if (AffectedCount < 1)
					{
						retValue = false;
					}
				}
			}
			else
			{
				// Create record.
				if (null == AddData(mdTable))
				{
					retValue = false;
				}
			}
			return retValue;
		}

		// Adds a data record.
		/// <include path='items/AddData/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbMetaDataTable AddData(DbMetaDataTable mdTable)
		{
			DbMetaDataTable retValue;

			retValue = Add(mdTable);
			if (retValue != null)
			{
				mdTable.ID = retValue.ID;
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
				{ DbMetaDataTable.ColumnID, id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetTableNameKey/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbColumns GetTableNameKey(string tableName)
		{
			var retValue = new DbColumns()
			{
				{ DbMetaDataTable.ColumnTableName, (object)tableName }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load join object.
		/// <include path='items/GetLoadJoins/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbJoins GetLoadJoins()
		{
			//	DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// [CodeType].[Description] as TypeDescription
			//left join [CodeType]
			// on [BaseTable].[CodeTypeID] = CodeType.ID

			// JoinOn Columns must have a properties in the main table object
			// to recieve the join values.
			//	string columnName = CodeType.ColumnDescription;
			//	string propertyName = DbMetaDataTable.ColumnTypeDescription;
			//	string renameAs = DbMetaDataTable.ColumnTypeDescription;
			//	dbJoin = new DbJoin
			//	{
			//		JoinTableName = "CodeType",
			//		JoinType = "left",
			//		JoinOns = new DbJoinOns() {
			//			{ DbMetaDataTable.ColumnCodeTypeID, CodeType.ColumnID }},
			//		Columns = new DbColumns() {
			//			{ columnName, propertyName, renameAs }}
			//	};
			//	//retValue.Add(dbJoin);
			return retValue;
		}

		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetTextFilters/*' file='Doc/DbMetaDataTableManager.xml'/>
		public DbFilters GetTextFilters()
		{
			DbFilters retValue = null;

			//DbFilter dbFilter = new DbFilter();
			//dbFilter.ConditionSet.Conditions.Add(DbMetaDataTable.ColumnDescription, "'Text'");
			//retValue = new DbFilters {
			//	dbFilter};
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderByDescription/*' file='Doc/DbMetaDataTableManager.xml'/>
		public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
		{
			//DbMetaDataTable.ColumnDescription
		};

		#endregion
	}
}
