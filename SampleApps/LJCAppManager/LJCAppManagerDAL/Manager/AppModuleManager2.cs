// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppModuleManager2.cs
using LJCNetCommon;
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	// Provides AppModule specific data manipulation methods.
	/// <include path='items/AppModuleManager2/*' file='Doc/AppModuleManager2.xml'/>
	public class AppModuleManager2 : ObjectManager<AppModule, AppModules>
	{
		#region Constructors

		/// Initializes an object instance.
		/// <include path='items/AppModuleManager2C/*' file='Doc/AppModuleManager2.xml'/>
		public AppModuleManager2(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "AppModule") : base(dbServiceRef, dataConfigName, tableName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;

			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(AppModule.ColumnID, AppModule.PropertyID);
			MapNames(AppModule.ColumnAppProgramID, AppModule.PropertyAppProgramID);

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					AppModule.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					AppModule.ColumnAppProgramID,
					AppModule.ColumnTypeName
				});
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ AppModule.ColumnID, id }
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
			//	{ "Left", AppProgram.TableName }};
			DbJoins retValue = new DbJoins() {
				{ AppProgram.TableName }};

			// Join to AppProgram
			DbJoin programJoin = retValue[0];
			programJoin.JoinOns = new DbJoinOns() {
				{ AppModule.ColumnAppProgramID, AppProgram.ColumnID }};
			List<string> columnNames = new List<string> {
				AppProgram.ColumnFileName,
				AppProgram.ColumnTitle};
			AppProgramManager2 programManager = new AppProgramManager2(mDbServiceRef, mDataConfigName);
			programJoin.Columns = programManager.GetColumns(columnNames);
			DbColumn titleColumn = programJoin.Columns[1];
			titleColumn.PropertyName = "ProgramTitle";
			titleColumn.RenameAs = "ProgramTitle";

			return retValue;
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;
		#endregion
	}
}
