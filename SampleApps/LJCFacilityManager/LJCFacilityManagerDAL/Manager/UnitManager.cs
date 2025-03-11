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
			DataDefinition.Add(Unit.ColumnTypeDescription, caption: "Code Type Description");
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
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ Unit.ColumnID, id }
			};
			return retValue;
		}

		// Get the Code key record.
		/// <include path='items/GetCodeKey/*' file='Doc/UnitManager.xml'/>
		public DbColumns GetCodeKey(string code)
		{
			var retValue = new DbColumns()
			{
				{ Unit.ColumnCode, (object)code }
			};
			return retValue;
		}

		// Gets the Description key record.
		/// <include path='items/GetDescriptionKey/*' file='Doc/UnitManager.xml'/>
		public DbColumns GetDescriptionKey(string description)
		{
			var retValue = new DbColumns()
			{
				{ Unit.ColumnDescription, (object)description }
			};
			return retValue;
		}

		// Gets the Parent ID key record.
		/// <include path='items/GetParentIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetParentIDKey(int parentID)
		{
			var retValue = new DbColumns()
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
				Columns = new DbColumns() {
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

			dbJoin = new DbJoin
			{
				TableName = "Person",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "UnitPerson.Person_Id", "Id" }},
				Columns = new DbColumns() {
					{ Person.ColumnID, Unit.ColumnPersonID, Unit.PropertyPersonID, "Int32" },
					{ Person.ColumnFirstName, Unit.ColumnFirstName, Unit.ColumnFirstName, "Int32" },
					{ Person.ColumnMiddleInitial, Unit.ColumnMiddleInitial
						, Unit.ColumnMiddleInitial, "Int32" },
					{ Person.ColumnLastName, Unit.ColumnLastName, Unit.ColumnLastName, "Int32" }}
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
