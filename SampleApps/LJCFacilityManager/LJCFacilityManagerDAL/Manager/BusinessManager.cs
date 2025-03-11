// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Business specific data manipulation methods.</summary>
	public class BusinessManager : ObjectManager<Business, Businesses>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public BusinessManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Business") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				Business.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Business.ColumnName
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
					{ Business.ColumnCodeTypeID, CodeType.ColumnID }},
				Columns = new DbColumns() {
					{ CodeType.ColumnDescription, Business.ColumnTypeDescription
						, Business.ColumnTypeDescription }}
			};
			retValue.Add(dbJoin);

			return retValue;
		}

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByName() => DataManager.OrderByNames = new List<string>() {
				Business.ColumnName};
	}
}
