// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCtransformProcess.cs
using System;
using System.IO;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCDataTransformDAL;

namespace LJCDataTransformProcess
{
	// The DataTransform processing.
	/// <include path='items/TransformProcess/*' file='Doc/LJCTransformProcess.xml'/>
	public class LJCTransformProcess
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TransformProcessC/*' file='Doc/LJCTransformProcess.xml'/>
		public LJCTransformProcess(string dataConfigName)
		{
			// Started as a windows process from LJCTransformService.StartTransform().

			// Set the work variables
			mDbServiceRef = new DbServiceRef
			{
				DbService = new DbService()
			};
			mDataConfigName = dataConfigName;

			// Set the first Manager work variable.
			mDataProcessManager = GetDataProcessManager(mDbServiceRef, mDataConfigName);

			// Set additional Manager work variables.
			mStepManager = GetStepManager(mDbServiceRef, mDataConfigName);
			mStepTaskManager = GetStepTaskManager(mDbServiceRef, mDataConfigName);
			mLJCDataProcessHelper = GetLJCDataProcessHelper(mDbServiceRef, mDataConfigName);
		}
		#endregion

		#region Public Methods

		// Starts the DataTransform processing.
		/// <include path='items/StartProcess/*' file='Doc/LJCTransformProcess.xml'/>
		public bool StartProcess(int processGroupID, int dataProcessID)
		{
			DataProcesses dataProcesses;
			DataProcess dataProcess = null;
			bool retValue = true;

			// Set the work variables
			DataProcessID = dataProcessID;
			mLogFileSpec = GetProcessLogFileSpec(DataProcessID, true);

			string errorText = CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				retValue = false;
				File.AppendAllText(mLogFileSpec, errorText);
			}

			if (retValue)
			{
				// Get the DataProcess object.
				try
				{
					dataProcess = mDataProcessManager.RetrieveWithID(DataProcessID);
				}
				catch (Exception e)
				{
					retValue = false;
					WriteLogLine(e.Message);
				}

				if (dataProcess != null)
				{
					mLogFileSpec = GetProcessLogFileSpec(DataProcessID);
					if (dataProcess.ProcessStatusID != (int)ProcessStatusType.Active)
					{
						retValue = false;
						WriteLogBlankLine();
						WriteLogLine("Process '{0}' in status '{1}' is not active."
							, dataProcess.Name, DataProcess.StatusName(dataProcess.ProcessStatusID));
					}
					else
					{
						// Execute the Active process.
						dataProcess.ProcessStatusID = (int)ProcessStatusType.Inprocess;
						mDataProcessManager.UpdateStatus(dataProcess);
						WriteLogBlankLine();
						WriteLogLine("Begin Process: {0}", dataProcess.Name);

						// ************** Testing *********************
						if (TestingDelay > 0)
						{
							for (int i = 0; i < TestingDelay; i++)
							{
								for (int j = 0; j < TestingDelay / 100; j++) ;
							}
						}

						// Execute the DataProcess steps.
						retValue = ProcessSteps();
						if (!retValue)
						{
							dataProcess.ProcessStatusID = (int)ProcessStatusType.Failed;
							mDataProcessManager.UpdateStatus(dataProcess);
						}
						else
						{
							// Update the current DataProcess to Ready.
							dataProcess.ProcessStatusID = (int)ProcessStatusType.Ready;
							mDataProcessManager.UpdateStatus(dataProcess);

							// If all group DataProcesses are ready, set them to available.
							dataProcesses = mLJCDataProcessHelper.LoadGroupProcesses(processGroupID);
							if (null == dataProcess)
							{
								retValue = false;
							}
							SetAllToAvailable(dataProcesses);
						}
					}
				}
			}
			return retValue;
		}
		#endregion

		#region Private Methods

		// Check for Depencency files.
		private string CheckDependencies()
		{
			string retValue = "";

			string fileTypeName = "Dependency";

			string fileSpec = "LJC.Net.Common.dll";
			if (!File.Exists(fileSpec))
			{
				retValue += $"{fileTypeName} '{fileSpec}' is not found.";
			}
			retValue += CheckFile("LJC.DBMessage.dll", fileTypeName);
			retValue += CheckFile("LJC.DBClientLib.dll", fileTypeName);
			retValue += CheckFile("LJC.DBServiceLib.dll", fileTypeName);
			retValue += CheckFile("LJCDataTransformDAL.dll", fileTypeName);
			return retValue;
		}

		// Checks for file.
		private string CheckFile(string fileSpec, string fileTypeName = "File")
		{
			string retValue = null;

			if (!NetString.HasValue(fileSpec))
			{
				retValue = $"{fileTypeName} name is missing.";
			}
			else
			{
				if (!File.Exists(fileSpec))
				{
					retValue = $"{fileTypeName} '{fileSpec}' is not found.";
				}
			}
			return retValue;
		}
		#endregion

		#region Processing Methods

		// Executes a Step Task.
		/// <include path='items/ProcessStepTask/*' file='Doc/LJCTransformProcess.xml'/>
		public bool ProcessStepTask(StepTask stepTask)
		{
			bool retValue = true;

			switch (stepTask.TaskTypeID)
			{
				case (int)StepTaskType.Program:
					retValue = DoProgram(stepTask);
					break;

				case (int)StepTaskType.SQLScript:
					//retValue = DoSQLScript(stepTask);
					break;

				case (int)StepTaskType.StoredProcedure:
					//retValue = DoStoredProcedure(stepTask);
					break;
			}

			if (!retValue)
			{
				stepTask.TaskStatusID = (int)StepTaskStatus.Failed;
				mStepTaskManager.UpdateStatus(stepTask);
			}
			else
			{
				stepTask.TaskStatusID = (int)StepTaskStatus.Ready;
				mStepTaskManager.UpdateStatus(stepTask);
			}
			return retValue;
		}

		// Executes a StepTask program.
		private bool DoProgram(StepTask stepTask)
		{
			bool retValue = true;

			string assemblyName;
			string className;
			string methodName;

			// Run program module.
			string[] tokens = stepTask.ActionItemName.Split('/');
			if (tokens.Length < 3)
			{
				WriteLogLine(stepTask.ActionItemName);
				WriteLogLine("Program module did not have the correct parameters.");
				retValue = false;
			}
			else
			{
				assemblyName = tokens[0];
				className = tokens[1];
				methodName = tokens[2];

				if (null == mLJCAssembly
					|| mLJCAssembly.AssemblyNameSpace != assemblyName)
				{
					mLJCAssembly = new LJCAssembly();
					string fileName = assemblyName + ".dll";
					mLJCAssembly.SetAssembly(assemblyName, fileName);
				}

				if (retValue)
				{
					if (null == mLJCAssembly.ObjectType
						|| mLJCAssembly.ObjectType.Name != className)
					{
						mLJCAssembly.SetObjectType(className);

						if (retValue)
						{
							mLJCAssembly.SetObjectInstance();
						}

						if (retValue)
						{
							mLJCAssembly.SetMethodInfo("Init");
						}
					}

					if (retValue)
					{
						object[] parameters = new object[4];
						parameters[0] = DataProcessID;
						parameters[1] = stepTask.StepTaskID;
						parameters[2] = methodName;
						parameters[3] = mDataConfigName;
						retValue = mLJCAssembly.MethodInvoke(parameters);
						if (!retValue)
						{
							WriteLogLine("Error in invoked 'Init' method.");
						}
					}
				}
			}
			return retValue;
		}

		//// Executes a StepTask script file.
		//private bool DoSQLScript(StepTask stepTask)
		//{
		//	bool retValue = true;

		//	return retValue;
		//}

		//// Executes a StepTask stored procedure.
		//private bool DoStoredProcedure(StepTask stepTask)
		//{
		//	bool retValue = true;

		//	return retValue;
		//}

		// Executes the DataProcess steps.
		private bool ProcessSteps()
		{
			bool retValue = true;

			// Get the DataProcess Step objects.
			Steps steps = mStepManager.LoadWithProcessID(DataProcessID);
			if (null == steps)
			{
				retValue = false;
			}

			if (retValue)
			{
				foreach (Step step in steps)
				{
					step.StatusID = (int)StepTaskStatus.Active;
					mStepManager.UpdateStatus(step);
				}
			}

			while (retValue)
			{
				// Process the Steps.
				foreach (Step step in steps)
				{
					WriteLogLine("  Begin Step: {0}", step.Name);
					step.StatusID = (int)StepTaskStatus.Inprocess;
					mStepManager.UpdateStatus(step);

					// ************** Testing *********************
					if (TestingDelay > 0)
					{
						for (int i = 0; i < TestingDelay; i++)
						{
							for (int j = 0; j < TestingDelay / 100; j++) ;
						}
					}

					StepTasks stepTasks = mStepTaskManager.LoadWithStepID(step.StepID);
					if (null == stepTasks)
					{
						retValue = false;
						break;
					}

					// Set the StepTasks to Active.
					if (stepTasks != null)
					{
						foreach (StepTask stepTask in stepTasks)
						{
							stepTask.TaskStatusID = (int)StepTaskStatus.Active;
							mStepTaskManager.UpdateStatus(stepTask);
						}

						// Process the StepTasks.
						retValue = ProcessStepTasks(stepTasks);
						if (!retValue)
						{
							break;
						}
					}

					if (retValue)
					{
						step.StatusID = (int)StepTaskStatus.Ready;
						mStepManager.UpdateStatus(step);
					}
				}
				break;
			}
			return retValue;
		}

		// Executes the DataProcess StepTasks.
		private bool ProcessStepTasks(StepTasks stepTasks)
		{
			bool retValue = true;

			// Process the StepTasks.
			foreach (StepTask stepTask in stepTasks)
			{
				// Change status to In-process.
				WriteLogLine("    Begin StepTask: {0}", stepTask.Name);
				stepTask.TaskStatusID = (int)StepTaskStatus.Inprocess;
				mStepTaskManager.UpdateStatus(stepTask);

				// ************** Testing *********************
				if (TestingDelay > 0)
				{
					for (int i = 0; i < TestingDelay; i++)
					{
						for (int j = 0; j < TestingDelay / 100; j++) ;
					}
				}

				retValue = ProcessStepTask(stepTask);
				if (!retValue)
				{
					break;
				}
			}
			return retValue;
		}
		#endregion

		#region Set Status Methods

		// Checks if all DataProcesses are in the provided status.
		private bool AllProcessesInStatus(DataProcesses processes
			, ProcessStatusType processStatusType)
		{
			bool retValue = true;

			if (null == processes)
			{
				retValue = false;
			}

			if (retValue && processes.Count > 0)
			{
				// Check if all group DataProcesses are Ready.
				foreach (DataProcess groupProcess in processes)
				{
					if (groupProcess.ProcessStatusID != (int)processStatusType)
					{
						retValue = false;
						break;
					}
				}
			}
			return retValue;
		}

		// Sets Processes, Steps and Tasks to Available if in status Ready.
		private void SetAllToAvailable(DataProcesses processes)
		{
			bool success = true;

			if (null == processes)
			{
				success = false;
			}

			if (success)
			{
				success = AllProcessesInStatus(processes, ProcessStatusType.Ready);
				if (success)
				{
					success = mLJCDataProcessHelper.AllStepsAndTasksInStatus(processes, StepTaskStatus.Ready);
				}
			}

			if (success)
			{
				mLJCDataProcessHelper.ResetToStatus(processes, StepTaskStatus.Available);
			}
		}
		#endregion

		#region Get Manager Methods

		// Gets the DataProcessManager object.
		private DataProcessManager GetDataProcessManager(DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			DataProcessManager retValue;

			try
			{
				retValue = new DataProcessManager(dbServiceRef, dataConfigName);
			}
			catch (Exception e)
			{
				retValue = null;
				WriteLogLine(e.Message);
			}
			return retValue;
		}

		// Gets the LJCProcessHelper object.
		private LJCDataProcessHelper GetLJCDataProcessHelper(DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			LJCDataProcessHelper retValue;

			retValue = new LJCDataProcessHelper(dbServiceRef, dataConfigName);
			return retValue;
		}

		// Gets the StepManager object.
		private StepManager GetStepManager(DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			StepManager retValue;

			retValue = new StepManager(dbServiceRef, dataConfigName);
			return retValue;
		}

		// Gets the TaskManager object.
		private StepTaskManager GetStepTaskManager(DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			StepTaskManager retValue;

			retValue = new StepTaskManager(dbServiceRef, dataConfigName);
			return retValue;
		}
		#endregion

		#region Write Log Methods

		// Gets the Process log names.
		private string GetProcessLogFileSpec(int processID, bool defaultName = false)
		{
			//Note: Also in: LJCTransformService.cs, CommonModule.cs,
			//			ProcessGroupModule.cs and TaskSourceModule.cs.
			var retValue = $"Logs\\DataProcess{processID}.txt";
			if (!defaultName)
			{
				if (null == mDataProcess)
				{
					mDataProcess = mDataProcessManager.RetrieveWithID(processID);
				}
				retValue = $"Logs\\{mDataProcess.Name}Process.txt";
			}
			return retValue;
		}

		// Writes a blank line.
		private void WriteLogBlankLine()
		{
			WriteLog("\r\n");
		}

		// Writes a string format to the Log file with the current date and time.
		private void WriteLog(string formatText = null, params object[] parameters)
		{
			NetFile.WriteLog(mLogFileSpec, formatText, parameters);
		}

		// Writes a string format plus cr/lf to the Log file with the current date
		// and time.
		private void WriteLogLine(string formatText = null, params object[] parameters)
		{
			if (NetString.HasValue(formatText))
			{
				formatText += "\r\n";
				WriteLog(formatText, parameters);
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the DataProcess ID.</summary>
		public int DataProcessID
		{
			get
			{
				return mDataProcessID;
			}
			set
			{
				mDataProcessID = value;
				mLogFileSpec = GetProcessLogFileSpec(mDataProcessID);
			}
		}
		private int mDataProcessID;

		/// <summary>Gets or sets the TestingDelay value.</summary>
		public int TestingDelay { get; set; }
		#endregion

		#region Class Data

		private readonly string mDataConfigName;
		private readonly DbServiceRef mDbServiceRef;

		private readonly DataProcessManager mDataProcessManager;
		private readonly LJCDataProcessHelper mLJCDataProcessHelper;
		private readonly StepManager mStepManager;
		private readonly StepTaskManager mStepTaskManager;

		private DataProcess mDataProcess;
		private LJCAssembly mLJCAssembly;
		private string mLogFileSpec;
		#endregion
	}
}
