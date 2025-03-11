// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataProcessHelper.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	// Provides process helper methods.
	/// <include path='items/DataProcessHelper/*' file='Doc/LJCDataProcessHelper.xml'/>
	public class LJCDataProcessHelper
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DataProcessHelperC/*' file='Doc/LJCDataProcessHelper.xml'/>
		public LJCDataProcessHelper(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
		}
		#endregion

		#region Status Methods

		// Attempts to Activate the DataProcesses for a ProcessGroup.
		/// <include path='items/ActivateGroupProcesses/*' file='Doc/LJCDataProcessHelper.xml'/>
		public bool ActivateGroupProcesses(int processGroupID)
		{
			bool retValue = false;

			SetProcessGroupManager();
      DataProcesses dataProcesses = null;
      DataProcess tempHoldProcess = null;
      var processGroup = mProcessGroupManager.RetrieveWithID(processGroupID);
			if (processGroup != null)
			{
				retValue = false;
				dataProcesses = LoadGroupProcesses(processGroupID);
				if (dataProcesses != null)
				{
					retValue = true;
					foreach (DataProcess dataProcess in dataProcesses)
					{
						if (dataProcess.ProcessStatusID != (int)ProcessStatusType.Available)
						{
							retValue = false;

							// ToDo: Put in wait timer. Retry every 10 minutes for 30 minutes?
							string message = "Cannot start Process Group '{0}' because Process"
								+ " '{1}' is in status '{2}'.";
							var errorText = string.Format(message, processGroup.Name, dataProcess.Name
								, DataProcess.StatusName(dataProcess.ProcessStatusID));
							throw new InvalidOperationException(errorText);
						}

						if (retValue)
						{
							// Only do on first Available process.
							if (null == tempHoldProcess)
							{
								// Hold process for potential group run.
								SetDataProcessManager();
								dataProcess.ProcessStatusID = (int)ProcessStatusType.Active;

								bool success = true;
								try
								{
									mDataProcessManager.UpdateStatus(dataProcess);
								}
								catch (Exception)
								{
									success = false;
								}
								if (success)
								{
									tempHoldProcess = dataProcess;
								}
							}
						}

						// Verify that all Steps and Tasks are Available.
						retValue = AllStepsAndTasksInStatus(dataProcesses
							, StepTaskStatus.Available);
						if (!retValue)
						{
							break;
						}
					}
				}
			}

			if (!retValue)
			{
				// Reset temporary hold process status.
				if (tempHoldProcess != null)
				{
					SetDataProcessManager();
					mDataProcessManager.UpdateStatus(tempHoldProcess);
				}
			}

			if (retValue)
			{
				// Set group processes to Active;
				ResetToStatus(dataProcesses, StepTaskStatus.Active, true);
			}
			return retValue;
		}

		// Check if all associated Steps and StepTasks are in the provided status.
		/// <include path='items/AllStepsAndTasksInStatus/*' file='Doc/LJCDataProcessHelper.xml'/>
		public bool AllStepsAndTasksInStatus(DataProcesses dataProcesses
			, StepTaskStatus stepTaskStatus)
		{
			bool retValue = true;

			if (null == dataProcesses)
			{
				retValue = false;
			}

			if (retValue && dataProcesses.Count > 0)
			{
				foreach (DataProcess dataProcess in dataProcesses)
				{
					retValue = false;
					Steps steps = LoadSteps(dataProcess.DataProcessID);
					if (NetCommon.HasItems(steps))
					{
						retValue = true;
						foreach (Step step in steps)
						{
							// Check if all Steps are in the specified status.
							if (step.StatusID != (int)stepTaskStatus)
							{
								retValue = false;
								break;
							}

							// Check if all StepTasks are in the specified status.
							retValue = false;
							StepTasks stepTasks = LoadStepTasks(step.StepID);
							if (NetCommon.HasItems(stepTasks))
							{
								retValue = true;
								foreach (StepTask stepTask in stepTasks)
								{
									if (stepTask.TaskStatusID != (int)stepTaskStatus)
									{
										retValue = false;
										break;
									}
								}
							}
						}

						// Break from outer loop.
						if (!retValue)
						{
							break;
						}
					}
				}
			}
			return retValue;
		}

		// Resets the associated Steps and Tasks to the provided status.
		private bool ResetStepsAndTasksToStatus(int dataProcessID
			, StepTaskStatus stepTaskStatus)
		{
			bool retValue = false;

			Steps steps = LoadSteps(dataProcessID);
			if (NetCommon.HasItems(steps))
			{
				retValue = true;
				SetStepManager();
				foreach (Step step in steps)
				{
					step.StatusID = (short)stepTaskStatus;
					mStepManager.UpdateStatus(step);
					if (mStepManager.AffectedCount > 0)
					{
						retValue = false;
						StepTasks tasks = LoadStepTasks(step.StepID);
						if (NetCommon.HasItems(tasks))
						{
							retValue = true;
							foreach (StepTask stepTask in tasks)
							{
								stepTask.TaskStatusID = (short)stepTaskStatus;
								mStepTaskManager.UpdateStatus(stepTask);
							}
						}
					}
				}
			}
			return retValue;
		}

		// Resets the DataProcesses and associated Steps and StepTasks to the provided status.
		/// <include path='items/ResetToStatus1/*' file='Doc/LJCDataProcessHelper.xml'/>
		public bool ResetToStatus(int processGroupID, StepTaskStatus stepTaskStatus
			, bool resetProcessesOnly = false)
		{
			bool retValue = true;

			DataProcesses processes = LoadGroupProcesses(processGroupID);
			if (processes != null)
			{
				retValue = ResetToStatus(processes, stepTaskStatus, resetProcessesOnly);
			}
			return retValue;
		}

		// Resets the DataProcesses and associated Steps and StepTasks to the provided status.
		/// <include path='items/ResetToStatus2/*' file='Doc/LJCDataProcessHelper.xml'/>
		public bool ResetToStatus(DataProcesses dataProcesses
			, StepTaskStatus stepTaskStatus, bool resetProcessesOnly = false)
		{
			bool retValue = true;

			if (null == dataProcesses)
			{
				retValue = false;
			}

			if (retValue && dataProcesses.Count > 0)
			{
				foreach (DataProcess dataProcess in dataProcesses)
				{
					if (!resetProcessesOnly)
					{
						retValue = ResetStepsAndTasksToStatus(dataProcess.DataProcessID
							, stepTaskStatus);
						if (!retValue)
						{
							break;
						}
					}
					dataProcess.ProcessStatusID = (short)stepTaskStatus;
					mDataProcessManager.UpdateStatus(dataProcess);
				}
			}
			return retValue;
		}
		#endregion

		#region Get Data Methods

		// Loads the ProcessGroup Processes.
		/// <include path='items/LoadGroupProcesses/*' file='Doc/LJCDataProcessHelper.xml'/>
		public DataProcesses LoadGroupProcesses(int processGroupID)
		{
			// Get the group DataProcess records.
			SetDataProcessManager();
			var retValue = mDataProcessManager.LoadWithGroupID(processGroupID);
			return retValue;
		}

		// Retrieves a collection of Step objects.
		private Steps LoadSteps(int dataProcessID)
		{
			SetStepManager();
			var retValue = mStepManager.LoadWithProcessID(dataProcessID);
			return retValue;
		}

		// Retrieves a collection of StepTask objects.
		private StepTasks LoadStepTasks(int stepID)
		{
			SetStepTaskManager();
			var retValue = mStepTaskManager.LoadWithStepID(stepID);
			return retValue;
		}

		// Retrieves the ProcessGroup.
		/// <include path='items/RetrieveProcessGroup/*' file='Doc/LJCDataProcessHelper.xml'/>
		public ProcessGroup RetrieveProcessGroup(int processGroupID)
		{
			SetProcessGroupManager();
			var keyColumns = mProcessGroupManager.GetIDKey(processGroupID);
			var retValue = mProcessGroupManager.Retrieve(keyColumns);
			return retValue;
		}
		#endregion

		#region Get Manager Methods

		// Gets the DataProcessManager object.
		private DataProcessManager GetDataProcessManager()
		{
			DataProcessManager retValue;

			retValue = new DataProcessManager(mDbServiceRef, mDataConfigName);
			return retValue;
		}

		// Gets the ProcessGroupManager object.
		private ProcessGroupManager GetProcessGroupManager()
		{
			ProcessGroupManager retValue;

			retValue = new ProcessGroupManager(mDbServiceRef, mDataConfigName);
			return retValue;
		}

		// Gets the StepManager object.
		private StepManager GetStepManager()
		{
			StepManager retValue;

			retValue = new StepManager(mDbServiceRef, mDataConfigName);
			return retValue;
		}

		// Gets the TaskManager object.
		private StepTaskManager GetStepTaskManager()
		{
			StepTaskManager retValue;

			retValue = new StepTaskManager(mDbServiceRef, mDataConfigName);
			return retValue;
		}

		// 
		private void SetDataProcessManager()
		{
			if (null == mDataProcessManager)
			{
				mDataProcessManager = GetDataProcessManager();
			}
		}

		// 
		private void SetProcessGroupManager()
		{
			if (null == mProcessGroupManager)
			{
				mProcessGroupManager = GetProcessGroupManager();
			}
		}

		// 
		private void SetStepManager()
		{
			if (null == mStepManager)
			{
				mStepManager = GetStepManager();
			}
		}

		// 
		private void SetStepTaskManager()
		{
			if (null == mStepTaskManager)
			{
				mStepTaskManager = GetStepTaskManager();
			}
		}
		#endregion

		#region Class Data

		private readonly string mDataConfigName;
		private readonly DbServiceRef mDbServiceRef;

		private DataProcessManager mDataProcessManager;
		private ProcessGroupManager mProcessGroupManager;
		private StepManager mStepManager;
		private StepTaskManager mStepTaskManager;
		#endregion
	}
}
