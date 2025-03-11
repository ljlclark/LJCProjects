// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserAppProgramManager2.cs
using LJCNetCommon;
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	// Provides UserAppProgram specific data manipulation methods.
	/// <include path='items/UserAppProgramManager2/*' file='Doc/UserAppProgramManager2.xml'/>
	public class UserAppProgramManager2 : ObjectManager<UserAppProgram, UserAppPrograms>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/UserAppProgramManager2C/*' file='Doc/UserAppProgramManager2.xml'/>
		public UserAppProgramManager2(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "UserAppProgram") : base(dbServiceRef, dataConfigName, tableName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;

			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(UserAppProgram.ColumnAppManagerUserID, UserAppProgram.PropertyAppManagerUserID);
			MapNames(UserAppProgram.ColumnAppProgramID, UserAppProgram.PropertyAppProgramID);

			// Add calculated and join columns.
			DataDefinition.LJCAddPropertyAs("FileName", caption: "File Name");
			DataDefinition.LJCAddPropertyAs("Title", caption: "Program Title");
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKeys/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKeys(int parentID, int childID)
		{
			var retValue = new DbColumns()
			{
				{ UserAppProgram.ColumnAppManagerUserID, parentID },
				{ UserAppProgram.ColumnAppProgramID, childID }
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
			//	{ "Left", AppProgram.TableName }};
			DbJoins retValue = new DbJoins() {
				{ AppUser.TableName },
				{ AppProgram.TableName }};

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

			return retValue;
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;
		#endregion
	}
}
