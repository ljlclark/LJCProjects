// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppUserManager2.cs
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCAppManagerDAL
{
	// Provides AppUser specific data manipulation methods.
	/// <include path='items/AppUserManager2/*' file='Doc/AppUserManager2.xml'/>
	public class AppUserManager2 : ObjectManager<AppUser, AppUsers>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/AppUserManager2C/*' file='Doc/AppUserManager2.xml'/>
		public AppUserManager2(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "AppManagerUser") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(AppUser.ColumnID, AppUser.PropertyID);
			MapNames(AppUser.ColumnUserID, AppUser.PropertyUserID, caption: "User ID");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					AppUser.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					AppUser.ColumnName,
					AppUser.ColumnUserID
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
				{ AppUser.ColumnID, id }
			};
			return retValue;
		}
		#endregion
	}
}
