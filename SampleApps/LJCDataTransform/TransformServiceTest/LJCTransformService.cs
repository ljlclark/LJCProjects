// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTransformService.cs
using System;
using System.Diagnostics;
using LJCDataTransformDAL;
using LJCDataTransformProcess;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCNetCommon;

namespace TransformServiceTest
{
	// Represents the Transform Data Service.
	/// <include path='items/LJCTransformService/*' file='Doc/ProjectTransformServiceTest.xml'/>
	public class LJCTransformService
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LJCTransformService()
		{
		}
		#endregion

		#region Public Methods

		// Executes the Transform Processes for the Process Group.
		/// <include path='items/StartTransform/*' file='Doc/LJCTransformService.xml'/>
		public bool StartTransform(int processGroupID, string dataConfigName)
		{
			DataProcesses dataProcesses = null;
			bool retValue = true;

			DbServiceRef dbServiceRef = new DbServiceRef
			{
				DbService = new DbService()
			};

			// Set the work variables
			mProcessHelper = new LJCDataProcessHelper(dbServiceRef, dataConfigName);
			mProcessGroupManager = new ProcessGroupManager(dbServiceRef, dataConfigName);
			mLogFileSpec = GetGroupLogFileSpec(processGroupID, true);
			if (null == mProcessHelper)
			{
				retValue = false;
			}

			if (retValue)
			{
				mLogFileSpec = GetGroupLogFileSpec(processGroupID);
			}

			// Attempt to activate all group processes.
			if (retValue)
			{
				// Attempt to Activate processes.
				retValue = mProcessHelper.ActivateGroupProcesses(processGroupID);
				if (!retValue)
				{
					// Testing - Resets to Available.
					if (mProcessHelper.ResetToStatus(processGroupID
						, StepTaskStatus.Available))
					{
						retValue = mProcessHelper.ActivateGroupProcesses(processGroupID);
						if (retValue)
						{
							WriteLogLine("Reset Group Processes to Available.");
						}
					}
				}
			}

			if (retValue)
			{
				// Get the group processes.
				dataProcesses = mProcessHelper.LoadGroupProcesses(processGroupID);
				if (null == dataProcesses)
				{
					retValue = false;
				}
			}

			if (retValue)
			{
				// Start the hidden processes.
				foreach (DataProcess dataProcess in dataProcesses)
				{
					string arguments = $"{processGroupID} {dataProcess.DataProcessID}"
						+ $" {dataConfigName}";

					bool testing = true;
					if (testing)
					{
						LJCTransformProcess transformProcess;
						transformProcess = new LJCTransformProcess(dataConfigName);
						if (transformProcess != null)
						{
							//transformProcess.TestingDelay = 180000;
							retValue = transformProcess.StartProcess(processGroupID
								, dataProcess.DataProcessID);
							if (!retValue)
							{
								break;
							}
						}
					}
					else
					{
						Process process = new Process()
						{
							StartInfo = new ProcessStartInfo()
							{
								FileName = "LJC.TransformProcess.exe",
								Arguments = arguments,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
					}
				}
			}
			return retValue;
		}
		#endregion

		#region Write Log Methods

		// Gets the Process log names.
		private string GetGroupLogFileSpec(int processGroupID, bool defaultName = false)
		{
			//Note: Also in: LJCTransformProcess.cs, CommonModule.cs,
			//			ProcessGroupModule.cs and TaskSourceModule.cs.
			bool success = true;
			string retValue;

			retValue = $"Logs\\ProcessGroup{processGroupID}.txt";
			if (!defaultName)
			{
				if (null == mProcessGroup)
				{
					mProcessGroup = mProcessGroupManager.RetrieveWithID(processGroupID);
					if (null == mProcessGroup)
					{
						success = false;
					}
				}

				if (success)
				{
					retValue = $"Logs\\{mProcessGroup.Name}Group.txt";
				}
			}
			return retValue;
		}

		// Writes a string format to the Log file with current date and time.
		private void WriteLog(string formatText = null, params object[] parameters)
		{
			NetFile.WriteLog(mLogFileSpec, formatText, parameters);
		}

		//// Writes a blank line.
		//private void WriteLogBlankLine()
		//{
		//	WriteLog("\r\n");
		//}

		// Writes a string format and cr/lf to the Log file with current date and time.
		private void WriteLogLine(string formatText = null
			, params object[] parameters)
		{
			if (NetString.HasValue(formatText))
			{
				formatText += "\r\n";
				WriteLog(formatText, parameters);
			}
		}
		#endregion

		#region Class Data

		private ProcessGroupManager mProcessGroupManager;
		private LJCDataProcessHelper mProcessHelper;

		private ProcessGroup mProcessGroup;

		private string mLogFileSpec;
		#endregion
	}
}
