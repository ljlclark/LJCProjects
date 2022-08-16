// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDBServiceLib;

namespace LJCDBViewDAL
{
	// Provides ViewFilter specific data manipulation methods.
	/// <include path='items/ViewFilterManager/*' file='Doc/ViewFilterManager.xml'/>
	public class ViewFilterManager
		: ObjectManager<ViewFilter, ViewFilters>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ViewFilterManagerC/*' file='Doc/ViewFilterManager.xml'/>
		public ViewFilterManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ViewFilter") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					ViewFilter.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					ViewFilter.ColumnViewDataID,
					ViewFilter.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieve the record by ID.
		/// <include path='items/RetrieveWithID/*' file='Doc/ViewFilterManager.xml'/>
		public ViewFilter RetrieveWithID(int id)
		{
			ViewFilter retValue;

			var keyColumns = GetIDKey(id);
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves the record by the unique key.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewFilterManager.xml'/>
		public ViewFilter RetrieveWithUniqueKey(int viewDataID, string name)
		{
			ViewFilter retValue;

			// Add(columnName, propertyName = null, renameAs = null
			//   , datatypeName = "String", caption = null);
			// Add(columnName, object value, dataTypeName = "String");
			var keyColumns = new DbColumns()
			{
				{ ViewFilter.ColumnViewDataID, viewDataID },
				{ ViewFilter.ColumnName, (object)name }
			};
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves a collection of Data records for the specified parent ID.
		/// <include path='items/LoadWithParentID/*' file='Doc/ViewFilterManager.xml'/>
		public ViewFilters LoadWithParentID(int viewDataID)
		{
			ViewFilters retValue;

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
				{ ViewFilter.ColumnID, id }
			};
			return retValue;
		}

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetParentKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ViewFilter.ColumnViewDataID, id }
			};
			return retValue;
		}
		#endregion

		#region Custom Data Methods

		// Updates the record if it already exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/ViewFilterManager.xml'/>
		public bool SaveData(ViewFilter viewFilter)
		{
			bool retValue = true;

			if (0 == viewFilter.ID)
			{
				// Create record.
				if (null == AddData(viewFilter))
				{
					retValue = false;
				}
			}
			else
			{
				// Update record.
				ViewFilter retrieveData = RetrieveWithID(viewFilter.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					// Note: Changed to update only changed columns.
					if (viewFilter.ChangedNames.Count > 0)
					{
						var keyColumns = GetIDKey(retrieveData.ID);
						Update(viewFilter, keyColumns, viewFilter.ChangedNames);
					}
				}
			}
			return retValue;
		}

		// Adds a data record.
		/// <include path='items/AddData/*' file='Doc/ViewFilterManager.xml'/>
		public ViewFilter AddData(ViewFilter viewFilter)
		{
			ViewFilter retValue;

			retValue = Add(viewFilter);
			if (retValue != null)
			{
				viewFilter.ID = retValue.ID;
			}
			return retValue;
		}
		#endregion
	}
}
