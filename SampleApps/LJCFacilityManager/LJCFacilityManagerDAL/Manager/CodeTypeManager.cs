// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides CodeType specific data manipulation methods.</summary>
	public class CodeTypeManager : ObjectManager<CodeType, CodeTypes>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public CodeTypeManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "CodeType") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(CodeType.ColumnCodeTypeClassDescription, caption: "CodeType Class");

			// Create the list of database assigned columns.
			// And make sure the AutoIncrement value is set.
			SetDbAssignedColumns(new string[]
			{
				CodeType.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				CodeType.ColumnDescription
			});
		}

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoins retValue = new DbJoins();
			DbJoin dbJoin = new DbJoin
			{
				TableName = CodeTypeClass.TableName,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ CodeType.ColumnCodeTypeClassID, CodeTypeClass.ColumnID }},
				Columns = new DbColumns() {
					{ CodeTypeClass.ColumnDescription, CodeType.ColumnCodeTypeClassDescription
						, CodeType.ColumnCodeTypeClassDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>() {
				CodeType.ColumnDescription};

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByClassDescription() => DataManager.OrderByNames = new List<string>() {
				CodeType.ColumnCodeTypeClassDescription,
				CodeType.ColumnCode};
	}
}
