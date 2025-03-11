// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StepTaskManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class StepTaskManager
		: ObjectManager<StepTask, StepTasks>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TaskManagerC/*' file='Doc/StepTaskManager.xml'/>
		public StepTaskManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "StepTask")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					StepTask.ColumnStepTaskID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					StepTask.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public StepTask RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}

		// Loads the StepTasks by StepID.
		/// <include path='items/LoadWithStepID/*' file='Doc/StepTaskManager.xml'/>
		public StepTasks LoadWithStepID(int stepID)
		{
			var keyColumns = GetStepIDKey(stepID);
			SetOrderBySequence();
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
				{ StepTask.ColumnStepTaskID, id }
			};
			return retValue;
		}

		// Get the Step ID key record.
		/// <include path='items/GetStepIDKey/*' file='Doc/StepTaskManager.xml'/>
		public DbColumns GetStepIDKey(int stepID)
		{
			var retValue = new DbColumns()
			{
				{ StepTask.ColumnStepID, stepID }
			};
			return retValue;
		}
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderBySequence() => DataManager.OrderByNames = new List<string>()
		{
			StepTask.ColumnSequence
		};
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(StepTask lookupRecord, StepTask currentRecord
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
					if (lookupRecord.StepTaskID != currentRecord.StepTaskID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// 
		/// <include path='items/UpdateStatus/*' file='Doc/StepTaskManager.xml'/>
		public void UpdateStatus(StepTask dataRecord)
		{
			var keyColumns = GetIDKey(dataRecord.StepTaskID);
			List<string> columnList = new List<string>()
			{
				StepTask.ColumnTaskStatusID
			};
			Update(dataRecord, keyColumns, columnList);
		}
		#endregion
	}
}
