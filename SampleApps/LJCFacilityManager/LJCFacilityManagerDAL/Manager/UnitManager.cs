// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Unit Specific data manipulation methods.</summary>
	public class UnitManager : ObjectManager<Unit, Units>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public UnitManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Unit") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names.
			MapNames(Unit.ColumnFacilityID, caption: "Facility ID");
			MapNames(Unit.ColumnCodeTypeID, caption: "CodeType ID");

			// Add join and calculated columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(Unit.ColumnTypeDescription
        , caption: "Code Type Description");
      DataDefinition.Add(Unit.ColumnPersonName, caption: "Person Name");

      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
			{
				Unit.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Unit.ColumnDescription
			});
		}

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public Unit RetrieveWithID(int id, List<string> propertyNames = null)
		{
			Unit retValue;

			var keyColumns = GetIDKey(id);
			retValue = Retrieve(keyColumns, propertyNames);
			return retValue;
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithCode/*' file='Doc/UnitManager.xml'/>
		public Unit RetrieveWithCode(string code)
		{
			Unit retValue;

			var keyColumns = GetCodeKey(code);
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithLookup/*' file='Doc/UnitManager.xml'/>
		public Unit RetrieveWithLookup(string description)
		{
			Unit retValue;

			var keyColumns = GetDescriptionKey(description);
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Loads a collection of data records with the supplied value.
		/// <include path='items/LoadWithParentID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public Units LoadWithParentID(int parentID, List<string> propertyNames = null)
		{
			Units retValue;

			var keyColumns = GetParentIDKey(parentID);
			DbJoins dbJoins = GetLoadJoins();
			SetOrderByCode();
			retValue = Load(keyColumns, propertyNames, joins: dbJoins);
			return retValue;
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public LJCDataColumns GetIDKey(int id)
		{
			var retValue = new LJCDataColumns()
			{
				{ Unit.ColumnID, id }
			};
			return retValue;
		}

		// Get the Code key record.
		/// <include path='items/GetCodeKey/*' file='Doc/UnitManager.xml'/>
		public LJCDataColumns GetCodeKey(string code)
		{
			var retValue = new LJCDataColumns()
			{
				{ Unit.ColumnCode, (object)code }
			};
			return retValue;
		}

		// Gets the Description key record.
		/// <include path='items/GetDescriptionKey/*' file='Doc/UnitManager.xml'/>
		public LJCDataColumns GetDescriptionKey(string description)
		{
			var retValue = new LJCDataColumns()
			{
				{ Unit.ColumnDescription, (object)description }
			};
			return retValue;
		}

		// Gets the Parent ID key record.
		/// <include path='items/GetParentIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public LJCDataColumns GetParentIDKey(int parentID)
		{
			var retValue = new LJCDataColumns()
			{
				{ Unit.ColumnFacilityID, parentID }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			dbJoin = new DbJoin
			{
				TableName = "CodeType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Unit.ColumnCodeTypeID, CodeType.ColumnID }},
				Columns = new LJCDataColumns() {
					{ CodeType.ColumnDescription, Unit.ColumnTypeDescription
						, Unit.ColumnTypeDescription }}
			};
			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "UnitPerson",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "Id", "Unit_Id" }},
			};
			retValue.Add(dbJoin);

      var dataColumns = new LJCDataColumns();
      var dataColumn = dataColumns.Add(Person.ColumnID, Unit.ColumnPersonID
        , "Int32");
      dataColumn.RenameAs = Unit.PropertyPersonID;
      dataColumns.Add(Person.ColumnFirstName, dataTypeName: "Int32");
      dataColumns.Add(Person.ColumnMiddleInitial, dataTypeName: "Int32");
      dataColumns.Add(Person.ColumnLastName, dataTypeName: "Int32");

      dbJoin = new DbJoin
			{
				TableName = "Person",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "UnitPerson.Person_Id", "Id" }},
				Columns = dataColumns,
			};
			retValue.Add(dbJoin);

			return retValue;
		}
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByCode() => DataManager.OrderByNames = new List<string>() {
				Unit.ColumnCode};
		#endregion
	}
}
