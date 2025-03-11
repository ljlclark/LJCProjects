// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppProgramManager2.cs
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCAppManagerDAL
{
	// Provides AppProgram specific data manipulation methods.
	/// <include path='items/AppProgramManager2/*' file='Doc/AppProgramManager2.xml'/>
	public class AppProgramManager2 : ObjectManager<AppProgram, AppPrograms>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/AppProgramManager2C/*' file='Doc/AppProgramManager2.xml'/>
		public AppProgramManager2(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "AppProgram") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(AppProgram.ColumnID, AppProgram.PropertyID, caption: "Program ID");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					AppProgram.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					AppProgram.ColumnFileName
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
				{ AppProgram.ColumnID, id }
			};
			return retValue;
		}
		#endregion
	}
}
