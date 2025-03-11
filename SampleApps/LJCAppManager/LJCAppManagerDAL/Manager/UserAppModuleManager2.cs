// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserAppModuleManager2.cs
using LJCNetCommon;
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	// Provides UserAppModule specific data manipulation methods.
	/// <include path='items/UserAppModuleManager2/*' file='Doc/UserAppModuleManager2.xml'/>
	public class UserAppModuleManager2 : ObjectManager<UserAppModule, UserAppModules>
	{
		#region Constructors

		/// Initializes an object instance.
		/// <include path='items/UserAppModuleManager2C/*' file='Doc/UserAppModuleManager2.xml'/>
		public UserAppModuleManager2(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "UserAppModule") : base(dbServiceRef, dataConfigName, tableName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;

			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(UserAppModule.ColumnAppManagerUserID, UserAppModule.PropertyAppManagerUserID);
			MapNames(UserAppModule.ColumnAppProgramID, UserAppModule.PropertyAppProgramID);
			MapNames(UserAppModule.ColumnAppModuleID, UserAppModule.PropertyAppModuleID);

			// Add calculated and join columns.
			DataDefinition.Add("TypeName", caption: "Module Name");
			DataDefinition.Add("ModuleTitle", caption: "Module Title");
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKeys/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKeys(int parentID, int childID)
		{
			var retValue = new DbColumns()
			{
				{ UserAppModule.ColumnAppProgramID, parentID },
				{ UserAppModule.ColumnAppModuleID, childID }
			};
			return retValue;
		}

		// Gets the ID key record.
		/// <include path='items/GetIDKeys/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetUserIDKeys(int parentID, int childID)
		{
			var retValue = new DbColumns()
			{
				{ UserAppModule.ColumnAppManagerUserID, parentID },
				{ UserAppModule.ColumnAppProgramID, childID }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			//DbJoins retValue = new DbJoins() {
			//	{ "Left", AppUser.TableName },
			//	{ "Left", AppProgram.TableName },
			//	{ "Left", AppModule.TableName }};
			DbJoins retValue = new DbJoins() {
				{ AppUser.TableName },
				{ AppProgram.TableName },
				{ AppModule.TableName }};

			// Join to AppManagerUser.
			DbJoin userJoin = retValue[0];
			userJoin.JoinOns = new DbJoinOns() {
				{ UserAppModule.ColumnAppManagerUserID, AppUser.ColumnID }};
			List<string> columnNames = new List<string> {
				AppUser.ColumnName,
				AppUser.ColumnUserID};
			AppUserManager2 userManager = new AppUserManager2(mDbServiceRef, mDataConfigName);
			userJoin.Columns = userManager.GetColumns(columnNames);
			DbColumn userIDColumn = userJoin.Columns[0];
			userIDColumn.PropertyName = "UserName";

			// Join to AppProgram.
			DbJoin programJoin = retValue[1];
			programJoin.JoinOns = new DbJoinOns() {
				{ UserAppModule.ColumnAppProgramID, AppProgram.ColumnID }};
			columnNames = new List<string> {
				AppProgram.ColumnFileName,
				AppProgram.ColumnTitle};
			AppProgramManager2 programManager = new AppProgramManager2(mDbServiceRef, mDataConfigName);
			programJoin.Columns = programManager.GetColumns(columnNames);

			// Join to AppModule.
			DbJoin moduleJoin = retValue[2];
			moduleJoin.JoinOns = new DbJoinOns() {
				{ UserAppModule.ColumnAppModuleID, AppModule.ColumnID }};
			columnNames = new List<string> {
				AppModule.ColumnTypeName,
				AppModule.ColumnTitle};
			AppModuleManager2 moduleManager = new AppModuleManager2(mDbServiceRef, mDataConfigName);
			moduleJoin.Columns = moduleManager.GetColumns(columnNames);
			DbColumn titleColumn = moduleJoin.Columns[1];
			titleColumn.PropertyName = "ModuleTitle";
			titleColumn.RenameAs = "ModuleTitle";

			return retValue;
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;
		#endregion
	}
}
