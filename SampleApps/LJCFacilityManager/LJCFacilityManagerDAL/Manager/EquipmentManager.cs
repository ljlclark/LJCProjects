// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EquipmentManager.cs
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Equipment Specific data manipulation methods.</summary>
	public class EquipmentManager : ObjectManager<Equipment, EquipmentItems>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public EquipmentManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Equipment") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(Equipment.ColumnTypeDescription, caption: "Code Type Description");
			DataDefinition.Add(Equipment.ColumnUnitDescription, caption: "Unit Description");

			// Create the list of database assigned columns.
			// And make sure the AutoIncrement value is set.
			SetDbAssignedColumns(new string[]
			{
				Equipment.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Equipment.ColumnDescription
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
				TableName = "CodeType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{Equipment.ColumnCodeTypeID, CodeType.ColumnID }},
				Columns = new DbColumns() {
					{ CodeType.ColumnDescription, Equipment.ColumnTypeDescription
						, Equipment.ColumnTypeDescription }}
			};
			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "Unit",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Equipment.ColumnUnitID, Unit.ColumnID }},
				Columns = new DbColumns() {
					{ Unit.ColumnDescription, Equipment.ColumnUnitDescription
						, Equipment.ColumnUnitDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}
	}
}
