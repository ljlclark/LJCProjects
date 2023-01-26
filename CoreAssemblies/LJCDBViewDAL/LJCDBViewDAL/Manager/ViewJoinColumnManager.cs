// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinColumnManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCDBViewDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class ViewJoinColumnManager
		: ObjectManager<ViewJoinColumn, ViewJoinColumns>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public ViewJoinColumnManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ViewJoinColumn") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					ViewJoinColumn.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					ViewJoinColumn.ColumnViewJoinID,
					ViewJoinColumn.ColumnPropertyName,
					ViewJoinColumn.ColumnRenameAs
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public ViewJoinColumn RetrieveWithID(int id, List<string> propertyNames = null)
		{
			ViewJoinColumn retValue;

			var keyColumns = GetIDKey(id);
			retValue = Retrieve(keyColumns, propertyNames);
			return retValue;
		}

		// Retrieves a collection of Data records for the specified parent ID.
		/// <include path='items/LoadWithParentID/*' file='Doc/ViewJoinColumnManager.xml'/>
		public ViewJoinColumns LoadWithParentID(int viewJoinID)
		{
			ViewJoinColumns retValue;

			var keyColumns = GetParentIDKey(viewJoinID);
			retValue = Load(keyColumns);
			return retValue;
		}

		// Retrieves the record by the unique key.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewJoinColumnManager.xml'/>
		public ViewJoinColumn RetrieveWithUniqueKey(ViewJoin viewJoin, DbColumn dbColumn)
		{
			ViewJoinColumn retValue = null;

			// Add(columnName, propertyName = null, renameAs = null
			//   , datatypeName = "String", caption = null);
			// Add(columnName, object value, dataTypeName = "String");
			var keyColumns = new DbColumns()
			{
				{ ViewJoinColumn.ColumnViewJoinID, viewJoin.ID },
				{ ViewJoinColumn.ColumnColumnName, (object)dbColumn.ColumnName },
				{ ViewJoinColumn.ColumnRenameAs, (object)dbColumn.RenameAs }
			};
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves a DbColumns collection for the specified parent ID.
		/// <include path='items/LoadDbColumnsWithParentID/*' file='Doc/ViewJoinColumnManager.xml'/>
		public DbColumns LoadDbColumnsWithParentID(int viewJoinID)
		{
			DbResult dbResult;
			DbColumns retValue;

			// Load from DataManager to get DbColumns result.
			var keyColumns = GetParentIDKey(viewJoinID);
			dbResult = DataManager.Load(keyColumns);
			SQLStatement = DataManager.SQLStatement;
			ResultConverter<DbColumn, DbColumns> resultConverter
				= new ResultConverter<DbColumn, DbColumns>();
			retValue = resultConverter.CreateCollection(dbResult);
			return retValue;
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ViewJoinColumn.ColumnID, id }
			};
			return retValue;
		}

		// Gets the ID key record.
		/// <include path='items/GetParentIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetParentIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ViewJoinColumn.ColumnViewJoinID, id }
			};
			return retValue;
		}
		#endregion

		#region Custom Data Methods

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/ViewJoinColumnManager.xml'/>
		public bool SaveData(ViewJoinColumn viewJoinColumn)
		{
			bool retValue = true;

			if (0 == viewJoinColumn.ID)
			{
				// Create record.
				if (null == AddData(viewJoinColumn))
				{
					retValue = false;
				}
			}
			else
			{
				// Update record.
				ViewJoinColumn retrieveData = RetrieveWithID(viewJoinColumn.ID);
				if (null == retrieveData)
				{
					retValue = false;
				}

				if (retValue)
				{
					// Note: Changed to update only changed columns.
					if (viewJoinColumn.ChangedNames.Count > 0)
					{
						var keyColumns = GetIDKey(retrieveData.ID);
						Update(viewJoinColumn, keyColumns, viewJoinColumn.ChangedNames);
					}
				}
			}
			return retValue;
		}

		// Adds a data record.
		/// <include path='items/AddData/*' file='Doc/ViewJoinColumnManager.xml'/>
		public ViewJoinColumn AddData(ViewJoinColumn viewJoinColumn)
		{
			ViewJoinColumn retValue;

			retValue = Add(viewJoinColumn);
			if (retValue != null)
			{
				viewJoinColumn.ID = retValue.ID;
			}
			return retValue;
		}

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public bool IsDuplicate(ViewJoinColumn lookupRecord, ViewJoinColumn currentRecord
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
	}
}
