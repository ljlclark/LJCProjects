// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AccountManager.cs
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Account specific data manipulation methods.</summary>
	public class AccountManager : ObjectManager<Account, Accounts>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public AccountManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Account") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(Account.ColumnPersonID, caption: "Person ID");
			MapNames(Account.ColumnBusinessID, caption: "Business ID");
			MapNames(Account.ColumnIDNumber, caption: "ID Number");

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(Account.ColumnBusinessName, caption: "Business Name");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				Account.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Account.ColumnPersonID,
				Account.ColumnBusinessID,
				Account.ColumnDescription
			});
		}

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoins retValue = new DbJoins();
			DbJoin dbJoin = new DbJoin
			{
				TableName = Business.TableName,
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Account.ColumnBusinessID, Business.ColumnID }},
				Columns = new DbColumns() {
					{ Account.ColumnBusinessName }}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
	}
}
