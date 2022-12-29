// Copyright (c) Lester J Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	/// <remarks>
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	public class UnitTypeManager
		: ObjectManager<UnitType, UnitTypes>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public UnitTypeManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "UnitType")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			//MapNames(UnitType.ColumnID, UnitType.PropertyID, caption: "ID");

			// Add Calculated and Join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			//DataDefinition.Add(UnitType.ColumnJoinDescription
			// , caption: "Join Description");

			// Create the list of database assigned columns.
			// And make sure the AutoIncrement value is set.
			SetDbAssignedColumns(new string[]
				{
					UnitType.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					UnitType.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public UnitType RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByDescription/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public UnitTypes LoadByDescription(DbColumns keyColumns = null
			, List<string> propertyNames = null, DbFilters filters = null
			, DbJoins joins = null)
		{
			if (null == joins)
			{
				joins = GetLoadJoins();
			}
			SetOrderByDescription();
			return Load(keyColumns, propertyNames, filters, joins);
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
        { UnitType.ColumnID, id }
      };
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			//DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the Data Object
			// to recieve the join values.
			// The RenameAs property is required if there is another table column
			// with the same name.
			// dbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 

			//// [CodeType].[Description] as TypeDescription
			////left join [CodeType]
			//// on (([MainTable].[CodeTypeID] = CodeType.ID))
			//string columnName = CodeType.ColumnDescription;
			//string propertyName = UnitType.ColumnTypeDescription;
			//string renameAs = UnitType.ColumnTypeDescription;
			//dbJoin = new DbJoin
			//{
			//	TableName = "CodeType",
			//	JoinType = "left",
			//	JoinOns = new DbJoinOns()
			//  {
			//		{ UnitType.ColumnCodeTypeID, CodeType.ColumnID }
			//	},
			//	Columns = new DbColumns()
			//  {
			//    // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
			//		{ columnName, propertyName, renameAs }
			//	}
			//};
			//retValue.Add(dbJoin);

			//// [c].Description,
			////left join [JoinTable] as j
			//// on [MainTable].[ForeignID] = c.ID
			//string alias = "j";
			//string joinToName = $"{alias}.{JoinTable.ColumnID}";
			//dbJoin = new DbJoin
			//{
			//	TableName = _JoinTable_.TableName,
			//	TableAlias = alias,
			//	JoinType = "left",
			//	JoinOns = new DbJoinOns() {
			//		{ UnitType.ColumnForeignID, joinToName }
			//	},
			//	Columns = new DbColumns() {
			//    // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
			//		{ _JoinTable_.ColumnDescription }
			//	}
			//};
			//retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderBy/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
		{
			//UnitType.ColumnDescription
		};
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public bool IsDuplicate(UnitType lookupRecord, UnitType currentRecord
			, bool isUpdate = false)
		{
			bool retValue = false;

			if (lookupRecord != null)
			{
				if (false == isUpdate)
				{
					// Duplicate for "New" record that already exists.
					retValue = true;
				}
				else
				{
					if (lookupRecord.ID != currentRecord.ID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
			return retValue;
		}
		#endregion
	}
}
