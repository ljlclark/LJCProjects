// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FixtureManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Fixture Specific data manipulation methods.</summary>
	public class FixtureManager : ObjectManager<Fixture, Fixtures>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public FixtureManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Fixture") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(Fixture.ColumnUnitID, caption: "Unit ID");
			MapNames(Fixture.ColumnCodeTypeID, caption: "CodeType ID");

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(Fixture.ColumnTypeDescription, caption: "Code Type Description");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				Fixture.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Fixture.ColumnDescription
			});
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
					{ "CodeType_Id", "Id" }},
				Columns = new DbColumns() {
					{ CodeType.ColumnDescription, Fixture.ColumnTypeDescription
						, Fixture.ColumnTypeDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByCode() => DataManager.OrderByNames = new List<string>() {
				Fixture.ColumnCode};
	}
}
