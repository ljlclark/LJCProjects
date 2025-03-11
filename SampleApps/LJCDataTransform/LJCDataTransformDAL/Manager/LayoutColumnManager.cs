// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumnManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class LayoutColumnManager
		: ObjectManager<LayoutColumn, LayoutColumns>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/LayoutColumnManagerC/*' file='Doc/LayoutColumnManager.xml'/>
		public LayoutColumnManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "LayoutColumn")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					LayoutColumn.ColumnLayoutColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					LayoutColumn.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a list of items by LayoutID.
		/// <include path='items/LoadWithLayoutID/*' file='Doc/LayoutColumnManager.xml'/>
		public LayoutColumns LoadWithLayoutID(int layoutID)
		{
			var keyColumns = GetLayoutIDKey(layoutID);
			SetOrderBySequence();
			return Load(keyColumns);
		}

		// Retrieves a list of items by LayoutID where PrimaryKey is true.
		/// <include path='items/LoadWithPrimaryKey/*' file='Doc/LayoutColumnManager.xml'/>
		public LayoutColumns LoadWithPrimaryKey(int layoutID)
		{
			var keyColumns = GetLayoutIDKey(layoutID);
			SetOrderBySequence();
			keyColumns.Add("PrimaryKey", true);
			return Load(keyColumns);
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public LayoutColumn RetrieveWithID(short id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(short id)
		{
			var retValue = new DbColumns()
			{
				{ LayoutColumn.ColumnLayoutColumnID, id }
			};
			return retValue;
		}

		// 
		/// <include path='items/GetLayoutIDKey/*' file='Doc/LayoutColumnManager.xml'/>
		public DbColumns GetLayoutIDKey(int layoutID)
		{
			var retValue = new DbColumns()
			{
				{ LayoutColumn.ColumnSourceLayoutID, layoutID }
			};
			return retValue;
		}
		#endregion

		#region OrderBys

		//
		/// <include path='items/SetOrderBySequence/*' file='Doc/StepManager.xml'/>
		public void SetOrderBySequence() => DataManager.OrderByNames = new List<string>() {
			LayoutColumn.ColumnSequence
		};
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(LayoutColumn lookupRecord, LayoutColumn currentRecord
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
					if (lookupRecord.LayoutColumnID != currentRecord.LayoutColumnID)
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
