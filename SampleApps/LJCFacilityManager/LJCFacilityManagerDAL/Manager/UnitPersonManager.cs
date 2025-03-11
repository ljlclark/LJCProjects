// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitPersonManager.cs
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides UnitPerson specific data manipulation methods.</summary>
	public class UnitPersonManager : ObjectManager<UnitPerson, UnitPersons>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public UnitPersonManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "UnitPerson") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(UnitPerson.ColumnPersonID, caption: "Person ID");
			MapNames(UnitPerson.ColumnBeginDate, caption: "Begin Date");
			MapNames(UnitPerson.ColumnEndDate, caption: "End Date");

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(UnitPerson.PropertyUnitDescription, caption: "Unit Description");
		}

		#region Joins

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
			// dbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 

			dbJoin = new DbJoin
			{
				TableName = "Unit",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ UnitPerson.ColumnUnitID, Unit.ColumnID }
				},
				Columns = new DbColumns() {
					{ Unit.ColumnDescription, UnitPerson.PropertyUnitDescription, UnitPerson.PropertyUnitDescription },
				}
			};
			retValue.Add(dbJoin);

			return retValue;
		}
		#endregion
	}
}
