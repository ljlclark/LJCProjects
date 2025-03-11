// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonRelationManager.cs
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides PersonRelation specific data manipulation methods.</summary>
	public class PersonRelationManager : ObjectManager<PersonRelation, PersonRelations>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public PersonRelationManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "PersonRelation") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(PersonRelation.ColumnFirstName, caption: "First Name");
			DataDefinition.Add(PersonRelation.ColumnLastName, caption: "Last Name");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				PersonRelation.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				PersonRelation.ColumnPersonID,
				PersonRelation.ColumnRelationID,
				PersonRelation.ColumnRelationCodeTypeID
			});
		}

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			dbJoin = new DbJoin
			{
				TableName = Person.TableName,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ PersonRelation.ColumnRelationID, Person.ColumnID }},
				Columns = new DbColumns() {
					{ Person.ColumnFirstName },
					{ Person.ColumnMiddleInitial },
					{ Person.ColumnLastName }}
			};
			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "CodeType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ PersonRelation.ColumnRelationCodeTypeID, CodeType.ColumnID }},
				Columns = new DbColumns() {
					{ CodeType.ColumnDescription, PersonRelation.ColumnTypeDescription
						, PersonRelation.ColumnTypeDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}
	}
}
