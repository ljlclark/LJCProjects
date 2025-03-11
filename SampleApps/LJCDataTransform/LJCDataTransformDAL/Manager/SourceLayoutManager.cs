// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceLayoutManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class SourceLayoutManager
		: ObjectManager<SourceLayout, SourceLayouts>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/LayoutManagerC/*' file='Doc/LayoutManager.xml'/>
		public SourceLayoutManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "SourceLayout")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					SourceLayout.ColumnSourceLayoutID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					SourceLayout.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public SourceLayout RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ SourceLayout.ColumnSourceLayoutID, id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetNameKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetNameKey(string name)
		{
			var retValue = new DbColumns()
			{
				{ SourceLayout.ColumnName, (object)name }
			};
			return retValue;
		}
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(SourceLayout lookupRecord, SourceLayout currentRecord
			, bool isUpdate = false)
		{
			bool retValue = false;

			if (lookupRecord != null)
			{
				if (!isUpdate)
				{
					// Duplicate for "New" record that already exists.
					retValue = true;
				}
				else
				{
					if (lookupRecord.SourceLayoutID != currentRecord.SourceLayoutID)
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
