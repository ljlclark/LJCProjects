// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCDBViewDAL
{
	/// <summary>Provides table specific data manipulation methods.</summary>
	public class DbViewColumnManager : ObjectManager<DbColumn, DbColumns>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ViewColumnManagerC/*' file='Doc/ViewColumnManager.xml'/>
		public DbViewColumnManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ViewColumn") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					ViewColumn.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					ViewColumn.ColumnViewDataID,
					ViewColumn.ColumnPropertyName,
					ViewColumn.ColumnRenameAs
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieve the record with ID.
		/// <include path='items/RetrieveWithID/*' file='Doc/ViewColumnManager.xml'/>
		public DbColumn RetrieveWithID(int id)
		{
			DbColumns keyColumns = GetIDKey(id);
			return Retrieve(keyColumns);
		}

		// Retrieves a collection of Data records for the specified parent ID.
		/// <include path='items/LoadWithParentID/*' file='Doc/ViewColumnManager.xml'/>
		public DbColumns LoadWithParentID(int viewDataID)
		{
			DbColumns retValue;

			var keyColumns = GetParentKey(viewDataID);
			retValue = Load(keyColumns);
			return retValue;
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ViewColumn.ColumnID, id }
			};
			return retValue;
		}

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetParentKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ViewColumn.ColumnViewDataID, id }
			};
			return retValue;
		}
		#endregion
	}
}
