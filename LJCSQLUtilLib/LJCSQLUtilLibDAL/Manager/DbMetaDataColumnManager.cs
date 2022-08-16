// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCSQLUtilLibDAL
{
	// Provides DbMetaDataColumn specific data manipulation methods.
	/// <include path='items/DbMetaDataColumnManager/*' file='Doc/ProjectSQLUtilLibDAL.xml'/>
	public class DbMetaDataColumnManager
		: ObjectManager<DbMetaDataColumn, DbMetaDataColumns>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DbMetaDataColumnManagerC/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbMetaDataColumnManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DBMetaDataColumn") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(DbMetaDataColumn.ColumnID, caption: "ID");
			MapNames(DbMetaDataColumn.ColumnDbMetaDataTableID
				, DbMetaDataColumn.PropertyDbMetaDataTableID, caption: "Table ID");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					DbMetaDataColumn.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					DbMetaDataColumn.ColumnDbMetaDataTableID,
					DbMetaDataColumn.ColumnColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByTableID/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbMetaDataColumns LoadByTableID(int tableID)
		{
			var keyColumns = GetTableIDKey(tableID);
			return Load(keyColumns);
		}

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByDescription/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbMetaDataColumns LoadByDescription()
		{
			DbJoins dbJoins = GetLoadJoins();
			SetOrderByDescription();
			return Load(null, joins: dbJoins);
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbMetaDataColumn RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}

		// Retrieves a data record with the supplied values.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbMetaDataColumn RetrieveWithUniqueKey(int tableID
			, string columnName)
		{
			var keyColumns = new DbColumns()
			{
				{ DbMetaDataColumn.ColumnDbMetaDataTableID, tableID },
				{ DbMetaDataColumn.ColumnColumnName, (object)columnName }
			};
			return Retrieve(keyColumns);
		}
		#endregion

		#region Save record methods.

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public bool SaveData(DbMetaDataColumn mdColumn)
		{
			bool retValue = true;

			if (mdColumn.ID > 0)
			{
				// Update record.
				DbMetaDataColumn retrieveData = RetrieveWithID(mdColumn.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					DbColumns keyColumns = GetIDKey(retrieveData.ID);
					Update(mdColumn, keyColumns);
				}
			}
			else
			{
				// Create record.
				if (null == AddData(mdColumn))
				{
					retValue = false;
				}
			}
			return retValue;
		}

		// Adds a data record.
		/// <include path='items/AddData/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbMetaDataColumn AddData(DbMetaDataColumn mdColumn)
		{
			DbMetaDataColumn retValue;

			retValue = Add(mdColumn);
			if (retValue != null)
			{
				mdColumn.ID = retValue.ID;
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
				{ DbMetaDataColumn.ColumnID, id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetTableIDKey/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbColumns GetTableIDKey(int tableID)
		{
			var retValue = new DbColumns()
			{
				{ DbMetaDataColumn.ColumnDbMetaDataTableID, tableID }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load join object.
		/// <include path='items/GetLoadJoins/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbJoins GetLoadJoins()
		{
			//DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// [CodeType].[Description] as TypeDescription
			//left join [CodeType]
			// on [BaseTable].[CodeTypeID] = CodeType.ID

			// JoinOn Columns must have a properties in the main table object
			// to receive the join values.
			//string columnName = CodeType.ColumnDescription;
			//string propertyName = DbMetaDataColumn.ColumnTypeDescription;
			//string renameAs = DbMetaDataColumn.ColumnTypeDescription;
			//dbJoin = new DbJoin
			//{
			//	JoinTableName = "CodeType",
			//	JoinType = "left",
			//	JoinOns = new DbJoinOns() {
			//			{ DbMetaDataColumn.ColumnCodeTypeID, CodeType.ColumnID }},
			//	Columns = new DbColumns() {
			//			{ columnName, propertyName, renameAs }}
			//};
			//retValue.Add(dbJoin);
			return retValue;
		}

		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetTextFilters/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public DbFilters GetTextFilters()
		{
			DbFilters retValue = null;

			DbFilter dbFilter = new DbFilter();
			//dbFilter.ConditionSet.Conditions.Add(DbMetaDataColumn.ColumnDescription, "'Text'");
			//retValue = new DbFilters {
			//	dbFilter};
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderByDescription/*' file='Doc/DbMetaDataColumnManager.xml'/>
		public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
		{
			//DbMetaDataColumn.ColumnDescription
		};
		#endregion
	}
}
