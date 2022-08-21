// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Provides ViewColumn specific data manipulation methods.
	/// <include path='items/ViewColumnManager/*' file='Doc/ProjectDbViewDAL.xml'/>
	public class ViewColumnManager : ObjectManager<ViewColumn, ViewColumns>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ViewColumnManagerC/*' file='Doc/ViewColumnManager.xml'/>
		public ViewColumnManager(DbServiceRef dbServiceRef, string dataConfigName
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

		#region Data Helper Methods

		// Adds a record including the flag values.
		/// <include path='items/AddWithFlags/*' file='Doc/ViewColumnManager.xml'/>
		public ViewColumn AddWithFlags(ViewColumn dataObject, List<string> propertyNames = null)
		{
			ViewColumn retValue;

			dataObject.ChangedNames.Add(ViewColumn.ColumnIsPrimaryKey);
			retValue = Add(dataObject, propertyNames);
			return retValue;
		}

		// Retrieves a DbColumns collection for the specified parent ID.
		/// <include path='items/LoadDbColumnsWithParentID/*' file='Doc/ViewColumnManager.xml'/>
		public DbColumns LoadDbColumnsWithParentID(int viewDataID)
		{
			DbResult dbResult;
			DbColumns retValue;

			// Load from DataManager to get DbColumns result.
			var keyColumns = GetParentKey(viewDataID);
			dbResult = DataManager.Load(keyColumns);

			// Copies ViewColumn properties to DbColumn objects.
			// Where ViewColumn properties match DbColumn.PropertyName.
			ResultConverter<DbColumn, DbColumns> resultConverter
				= new ResultConverter<DbColumn, DbColumns>();
			retValue = resultConverter.CreateCollection(dbResult);

			// ToDo: Should MaxLength be added to ViewColumn?
			// ViewColumn does not have MaxLength. So Add it to retValue;
			// Process each Data Object column.
			DbColumns recordColumns = dbResult.Columns;
			foreach (DbColumn column in retValue)
			{
				// Search each record.
				foreach (DbRow dbRow in dbResult.Rows)
				{
					// Search each value in the record for Value that matches ColumnName.
					bool success = false;
					foreach (DbValue dbValue in dbRow.Values)
					{
						if (dbValue.Value != null
							&& dbValue.Value.ToString() == column.ColumnName)
						{
							// Get the associated Definition Object.
							var findColumn = recordColumns.LJCSearchName(dbValue.PropertyName);
							if (findColumn != null)
							{
								success = true;
								column.MaxLength = findColumn.MaxLength;
							}
							break;
						}
					}
					if (success)
					{
						break;
					}
				}
			}
			return retValue;
		}

		// Retrieves a collection of Data records for the specified parent ID.
		/// <include path='items/LoadWithParentID/*' file='Doc/ViewColumnManager.xml'/>
		public ViewColumns LoadWithParentID(int viewDataID)
		{
			ViewColumns retValue;

			var keyColumns = GetParentKey(viewDataID);
			retValue = Load(keyColumns);
			return retValue;
		}

		// Retrieve the record with ID.
		/// <include path='items/RetrieveWithID/*' file='Doc/ViewColumnManager.xml'/>
		public ViewColumn RetrieveWithID(int id)
		{
			ViewColumn retValue;

			var keyColumns = GetIDKey(id);
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves the record with the unique key.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewColumnManager.xml'/>
		public ViewColumn RetrieveWithUniqueKey(int viewDataID, string columnName)
		{
			ViewColumn retValue;

			// Add(columnName, propertyName = null, renameAs = null
			//   , datatypeName = "String", caption = null);
			// Add(columnName, object value, dataTypeName = "String");
			var keyColumns = new DbColumns()
			{
				{ ViewColumn.ColumnViewDataID, viewDataID },
				{ ViewColumn.ColumnColumnName, (object)columnName }
			};

			retValue = Retrieve(keyColumns);
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

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public bool IsDuplicate(ViewColumn lookupRecord, ViewColumn currentRecord
			, bool isUpdate = false)
		{
			bool retValue = false;

			if (lookupRecord != null)
			{
				if (false == isUpdate)
				{
					// Duplicate for "New" record that already exists.
					retValue = true;
				}
				else
				{
					if (lookupRecord.ID != currentRecord.ID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
			return retValue;
		}
		#endregion

		#region Custom Data Methods

		// Adds a data record.
		/// <include path='items/AddData/*' file='Doc/ViewColumnManager.xml'/>
		public ViewColumn AddData(ViewColumn viewColumn)
		{
			ViewColumn retValue;

			retValue = AddWithFlags(viewColumn);
			if (retValue != null)
			{
				viewColumn.ID = retValue.ID;
			}
			return retValue;
		}

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/ViewColumnManager.xml'/>
		public bool SaveData(ViewColumn viewColumn)
		{
			bool retValue = true;

			if (0 == viewColumn.ID)
			{
				// Create record.
				if (null == AddData(viewColumn))
				{
					retValue = false;
				}
			}
			else
			{
				// Update record.
				ViewColumn retrieveData = RetrieveWithID(viewColumn.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					// Note: Changed to update only changed columns.
					if (viewColumn.ChangedNames.Count > 0)
					{
						var keyColumns = GetIDKey(retrieveData.ID);
						Update(viewColumn, keyColumns, viewColumn.ChangedNames);
					}
				}
			}
			return retValue;
		}
		#endregion
	}
}
