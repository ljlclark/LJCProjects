// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityDbManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Facility Specific data manipulation methods.</summary>
	public class FacilityDbManager : ObjectManager<Facility, Facilities>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public FacilityDbManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Facility") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(Facility.ColumnCodeTypeID, caption: "CodeType ID");
			MapNames(Facility.ColumnTypeDescription, caption: "Type Description");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				Facility.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Facility.ColumnDescription
			});
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public Facility RetrieveWithID(int id, List<string> propertyNames = null)
		{
			Facility retValue;

			var keyColumns = GetIDKey(id);
			retValue = Retrieve(keyColumns, propertyNames);
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ Facility.ColumnID, id }
			};
			return retValue;
		}

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoins retValue = new DbJoins();
			DbJoin dbJoin = new DbJoin
			{
				TableName = "CodeType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Facility.ColumnCodeTypeID, CodeType.ColumnID }},
				Columns = new DbColumns() {
					{ CodeType.ColumnDescription, Facility.ColumnTypeDescription
						, Facility.ColumnTypeDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByCode() => DataManager.OrderByNames = new List<string>() {
				Facility.ColumnCode};
	}
}
