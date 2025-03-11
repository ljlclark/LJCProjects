// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StepManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class StepManager
		: ObjectManager<Step, Steps>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/StepManagerC/*' file='Doc/StepManager.xml'/>
		public StepManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Step")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					Step.ColumnStepID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					Step.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a data record with the supplied value.
		/// <include path='items/LoadWithProcessID/*' file='Doc/StepManager.xml'/>
		public Steps LoadWithProcessID(int processID)
		{
			var keyColumns = GetProcessIDKey(processID);
			SetOrderBySequence();
			return Load(keyColumns);
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public Step RetrieveWithID(int id, List<string> propertyNames = null)
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
				{ Step.ColumnStepID, id }
			};
			return retValue;
		}

		// Get the process ID key record.
		/// <include path='items/GetProcessIDKey/*' file='Doc/StepManager.xml'/>
		public DbColumns GetProcessIDKey(int processID)
		{
			var retValue = new DbColumns()
			{
				{ Step.ColumnDataProcessID, processID }
			};
			return retValue;
		}
		#endregion

		#region OrderBys

		//
		/// <include path='items/SetOrderBySequence/*' file='Doc/StepManager.xml'/>
		public void SetOrderBySequence() => DataManager.OrderByNames = new List<string>() {
			Step.ColumnSequence
		};
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(Step lookupRecord, Step currentRecord
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
					if (lookupRecord.DataProcessID != currentRecord.DataProcessID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// Updates the status of the data record.
		/// <include path='items/UpdateStatus/*' file='Doc/StepTaskManager.xml'/>
		public void UpdateStatus(Step dataRecord)
		{
			var keyColumns = GetIDKey(dataRecord.StepID);
			List<string> columnList = new List<string>()
			{
				Step.ColumnStatusID
			};
			Update(dataRecord, keyColumns, columnList);
		}
		#endregion
	}
}
