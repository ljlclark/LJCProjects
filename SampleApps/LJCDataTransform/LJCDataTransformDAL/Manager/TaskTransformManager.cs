// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskTransformManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class TaskTransformManager
		: ObjectManager<TaskTransform, TaskTransforms>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TaskTransformManagerC/*' file='Doc/TaskTransformManager.xml'/>
		public TaskTransformManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "TaskTransform")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(TaskTransform.ColumnSourceDataID
				, TaskTransform.PropertySourceDataID, caption: "Source Column ID");
			MapNames(TaskTransform.ColumnTargetDataID
				, TaskTransform.PropertyTargetDataID, caption: "Target Column ID");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					TaskTransform.ColumnTransformID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					TaskTransform.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public TaskTransform RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}

		// Loads a collection of data records ordered by Name.
		/// <include path='items/LoadByName/*' file='Doc/TaskTransformManager.xml'/>
		public TaskTransforms LoadByName()
		{
			SetOrderByName();
			return Load();
		}

		// 
		/// <include path='items/LoadWithTaskID/*' file='Doc/TaskTransformManager.xml'/>
		public TaskTransforms LoadWithTaskID(int stepTaskID)
		{
			var keyColumns = GetTaskIDKey(stepTaskID);
			return Load(keyColumns);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ TaskTransform.ColumnTransformID, id }
			};
			return retValue;
		}

		// 
		/// <include path='items/GetTaskIDKey/*' file='Doc/TaskTransformManager.xml'/>
		public DbColumns GetTaskIDKey(int stepTaskID)
		{
			var retValue = new DbColumns()
			{
				{ TaskTransform.ColumnStepTaskID, stepTaskID }
			};
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderByName/*' file='Doc/TaskTransformManager.xml'/>
		public void SetOrderByName()
		{
			DataManager.OrderByNames = new List<string>()
			{
				TaskTransform.ColumnName
			};
		}
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(TaskTransform lookupRecord, TaskTransform currentRecord
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
					if (lookupRecord.TransformID != currentRecord.TransformID)
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
