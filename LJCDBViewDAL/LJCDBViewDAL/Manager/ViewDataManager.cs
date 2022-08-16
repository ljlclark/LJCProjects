// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	// Provides ViewData specific data manipulation methods.
	/// <include path='items/ViewDataManager/*' file='Doc/ViewDataManager.xml'/>
	public class ViewDataManager
		: ObjectManager<ViewData, Views>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ViewDataManagerC/*' file='Doc/ViewDataManager.xml'/>
		public ViewDataManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ViewData") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					ViewData.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
					ViewData.ColumnViewTableID,
					ViewData.ColumnName
			});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieve the record by ID.
		/// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public ViewData RetrieveWithID(int id, List<string> propertyNames = null)
		{
			ViewData retValue;

			var keyColumns = GetIDKey(id);
			retValue = Retrieve(keyColumns, propertyNames);
			retValue.ChangedNames.Clear();
			return retValue;
		}

		// Retrieves the record by the unique key.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewDataManager.xml'/>
		public ViewData RetrieveWithUniqueKey(int viewTableID, string name)
		{
			ViewData retValue;

			// Add(columnName, propertyName = null, renameAs = null
			//   , datatypeName = "String", caption = null);
			// Add(columnName, object value, dataTypeName = "String");
			var keyColumns = new DbColumns()
			{
				{ ViewData.ColumnViewTableID, viewTableID },
				{ ViewData.ColumnName, (object)name }
			};
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves a collection of Data records for the specified parent ID.
		/// <include path='items/LoadWithParentID/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public Views LoadWithParentID(int parentID, List<string> propertyNames = null)
		{
			Views retValue;

			var keyColumns = GetParentKey(parentID);
			retValue = Load(keyColumns, propertyNames);
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
				{ ViewData.ColumnID, id }
			};
			return retValue;
		}

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetParentKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ViewData.ColumnViewTableID, id }
			};
			return retValue;
		}
		#endregion

		#region Custom Data Methods

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/ViewDataManager.xml'/>
		public bool SaveData(ViewData viewData)
		{
			bool retValue = true;

			if (0 == viewData.ID)
			{
				// Create record.
				if (null == AddData(viewData))
				{
					retValue = false;
				}
			}
			else
			{
				// Update record.
				ViewData retrieveData = RetrieveWithID(viewData.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					// Note: Changed to update only changed columns.
					if (viewData.ChangedNames.Count > 0)
					{
						var keyColumns = GetIDKey(retrieveData.ID);
						Update(viewData, keyColumns, viewData.ChangedNames);
					}
				}
			}
			return retValue;
		}

		// Adds a ViewData record.
		/// <include path='items/AddData/*' file='Doc/ViewDataManager.xml'/>
		public ViewData AddData(ViewData viewData)
		{
			ViewData retValue;

			retValue = Add(viewData);
			if (retValue != null)
			{
				viewData.ID = retValue.ID;
			}
			return retValue;
		}
		#endregion
	}
}
