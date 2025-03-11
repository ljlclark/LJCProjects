// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDataAccess;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Person specific data manipulation methods.</summary>
	public class PersonManager : ObjectManager<Person, Persons>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public PersonManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Person") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(Person.ColumnFirstName, caption: "First Name");
			MapNames(Person.ColumnMiddleInitial, caption: "Middle Initial");
			MapNames(Person.ColumnLastName, caption: "Last Name");
			MapNames(Person.ColumnPrincipleTitle, caption: "Principle Title");

			// Add calculated and join columns.
			// This allows them to be added to a grid configuration and to populate a Data Object.
			DataDefinition.Add(Person.ColumnFullName, caption: "Name");
			DataDefinition.Add(Person.ColumnTypeDescription, caption: "Type Description");
			DataDefinition.Add(Person.ColumnUnitDescription, caption: "Unit Description");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				Person.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Person.ColumnFirstName,
				Person.ColumnMiddleInitial,
				Person.ColumnLastName
			});
		}

		#region Helper Methods

		/// <summary>
		/// Loads a collection of data records with OrderBy.
		/// </summary>
		/// <returns>The collection of data records.</returns>
		public Persons LoadByFirstLast()
		{
			Persons retValue;

			DbJoins dbJoins = GetLoadJoins();
			SetOrderByFirstLast();
			List<string> columns = new List<string>()
			{
				Person.ColumnID,
				Person.ColumnFirstName,
				Person.ColumnMiddleInitial,
				Person.ColumnLastName
			};
			retValue = Load(propertyNames: columns, joins: dbJoins);
			return retValue;
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public Person RetrieveWithID(int id, List<string> propertyNames = null)
		{
			Person retValue;

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
				{ Person.ColumnID, id }
			};
			return retValue;
		}

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the Data Object
			// to recieve the join values.
			// The RenameAs property is required if there is another table column
			// with the same name.

			string columnName = CodeType.ColumnDescription;
			string propertyName = Person.ColumnTypeDescription;
			string renameAs = Person.ColumnTypeDescription;
			dbJoin = new DbJoin
			{
				TableName = "CodeType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "CodeType_Id", "Id" }},
				Columns = new DbColumns() {
					{ columnName, propertyName, renameAs }}
			};
			retValue.Add(dbJoin);

			//string today = DataCommon.GetDbDateString(DateTime.Now);
			//string minimumDate = DataCommon.GetMinUIDateTimeString();
			dbJoin = new DbJoin
			{
				TableName = "UnitPerson",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "Id", "Person_Id" }
					//{ "EndDate", minimumDate }
				},
			};
			retValue.Add(dbJoin);

			//// Modify first JoinOn.
			//dbJoin.JoinOns[1].BooleanOperator = "or";

			//// Group into first JoinOn.
			//DbJoinOn firstJoinOn = dbJoin.JoinOns[0];
			//firstJoinOn.JoinOns = new DbJoinOns()	{
			//	{ "BeginDate", today, "<=" },
			//	{ "EndDate", today, ">" }
			//};

			dbJoin = new DbJoin
			{
				TableName = "Unit",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ "[UnitPerson].Unit_Id", "Id" }},
				Columns = new DbColumns() {
					{ Unit.ColumnDescription, Person.ColumnUnitDescription
						, Person.ColumnUnitDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}

		/// <summary>
		/// Creates and returns the filters object.
		/// </summary>
		/// <returns>The DbFilter object.</returns>
		public DbFilters GetAdminFilters()
		{
			DbFilters retValue;

			DbFilter dbFilter = new DbFilter();
			dbFilter.ConditionSet.Conditions.Add("UserId", "'Admin'");
			retValue = new DbFilters {
				dbFilter};
			return retValue;
		}

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByFirstLast() => DataManager.OrderByNames = new List<string>() {
			Person.ColumnLastName,
			Person.ColumnFirstName};
		#endregion
	}
}
