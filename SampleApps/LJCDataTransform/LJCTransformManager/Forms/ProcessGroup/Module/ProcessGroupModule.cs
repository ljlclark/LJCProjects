// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupModule.cs
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCWinFormCommon;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The tab composite user control.
	/// <include path='items/Module/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class ProcessGroupModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProcessGroupModule()
		{
			InitializeComponent();
			string errorText = TransformCommon.CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				MessageBox.Show(errorText);
				ClosePage(ProcessGroupPage);
			}
		}
		#endregion

		#region Action Event Handlers

		#region ProcessGroup

		// Calls the New method.
		private void ProcessGroupToolNew_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoNewProcessGroup();
		}

		// Calls the Edit method.
		private void ProcessGroupToolEdit_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoEditProcessGroup();
		}

		// Calls the Delete method.
		private void ProcessGroupToolDelete_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoDeleteProcessGroup();
		}

		// Calls the New method.
		private void ProcessGroupNew_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoNewProcessGroup();
		}

		// Calls the Edit method.
		private void ProcessGroupEdit_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoEditProcessGroup();
		}

		// Calls the Delete method.
		private void ProcessGroupDelete_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoDeleteProcessGroup();
		}

		// Calls the Refresh method.
		private void ProcessGroupRefresh_Click(object sender, EventArgs e)
		{
			mGroupGridCode.DoRefreshProcessGroup();
		}

		// Exectues the ProcessGroup.
		private void ProcessGroupExecute_Click(object sender, EventArgs e)
		{
			if (ProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				int processGroupID = row.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
				string arguments = $"{processGroupID} {Managers.DataConfigName}";

				Process process = new Process()
				{
					StartInfo = new ProcessStartInfo()
					{
						FileName = "TransformServiceTest.exe",
						Arguments = arguments,
						WindowStyle = ProcessWindowStyle.Hidden
					}
				};
				process.Start();
			}
		}

		// Show the Transform Process Group Log
		private void ProcessGroupLog_Click(object sender, EventArgs e)
		{
			if (ProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				string[] logFileSpecs = GetGroupLogFileSpecs();
				string defaultFileSpec = logFileSpecs[0];
				string logFileSpec = logFileSpecs[1];

				if (File.Exists(defaultFileSpec))
				{
					Process process = new Process()
					{
						StartInfo = new ProcessStartInfo()
						{
							FileName = defaultFileSpec,
						}
					};
					process.Start();
				}

				if (!File.Exists(logFileSpec))
				{
					string message = $"File '{logFileSpec}' was not found.";
					MessageBox.Show(message, "Message Log", MessageBoxButtons.OK
						, MessageBoxIcon.Information);
				}
				else
				{
					Process process = new Process()
					{
						StartInfo = new ProcessStartInfo()
						{
							FileName = logFileSpec,
						}
					};
					process.Start();
				}
			}
		}

		// Allows display and edit of text file.
		private void ProcessGroupFileEdit_Click(object sender, EventArgs e)
		{
			FormCommon.ShellFile("NotePad.exe");
		}

		// Performs the Close function.
		private void ProcessGroupClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(ProcessGroupPage);
		}

		// Gets the ProcessGroup log file specifications.
		private string[] GetGroupLogFileSpecs()
		{
			// Note: Also in: LJCTransformService.cs, LJCTransformProcess.cs,
			//			 TaskSourceModule.cs	and CommonModule.cs.
			int processGroupID;
			string[] retValue = null;

			if (ProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				processGroupID = row.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
				string processGroupName = row.LJCGetString(ProcessGroup.ColumnName);
				retValue = new string[]
				{
				  $"Logs\\ProcessGroup{processGroupID}.txt",
				  $"Logs\\{processGroupName}Group.txt"
				};
			}
			return retValue;
		}
		#endregion

		#region DataProcess

		// Calls the New method.
		private void DataProcessToolNew_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoAddDataProcess();
		}

		// Calls the Edit method.
		private void DataProcessToolEdit_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoEditDataProcess();
		}

		// Calls the Delete method.
		private void DataProcessToolDelete_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoRemoveDataProcess();
		}

		// Calls the New method.
		private void DataProcessMenuNew_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoAddDataProcess();
		}

		// Calls the Edit method.
		private void DataProcessEdit_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoEditDataProcess();
		}

		// Calls the Delete method.
		private void DataProcessDelete_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoRemoveDataProcess();
		}

		// Calls the Refresh method.
		private void DataProcessRefresh_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoRefreshDataProcess();
		}

		// Show the Data Transform Process log.
		private void DataProcessLog_Click(object sender, EventArgs e)
		{
			if (DataProcessGrid.CurrentRow is LJCGridRow row)
			{
				string[] logFileSpecs = GetProcessLogFileSpecs();
				string defaultFileSpec = logFileSpecs[0];
				string logFileSpec = logFileSpecs[1];

				if (File.Exists(defaultFileSpec))
				{
					Process process = new Process()
					{
						StartInfo = new ProcessStartInfo()
						{
							FileName = defaultFileSpec,
						}
					};
					process.Start();
				}

				if (!File.Exists(logFileSpec))
				{
					string message = $"File '{logFileSpec}' was not found.";
					MessageBox.Show(message, "Message Log", MessageBoxButtons.OK
						, MessageBoxIcon.Information);
				}
				else
				{
					Process process = new Process()
					{
						StartInfo = new ProcessStartInfo()
						{
							FileName = logFileSpec,
						}
					};
					process.Start();
				}
			}
		}

		// Performs the Close function.
		private void DataProcessClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(ProcessGroupPage);
		}

		// Gets the Process log file specifications.
		private string[] GetProcessLogFileSpecs()
		{
			// Note: Also in: LJCTransformService.cs, LJCTransformProcess.cs,
			//			 CommonModule.cs and TaskSourceModule.cs.
			int processID;
			string[] retValue = null;

			LJCGridRow row = DataProcessGrid.CurrentRow as LJCGridRow;
			processID = row.LJCGetInt32(DataProcess.ColumnDataProcessID);
			string processName = row.LJCGetString(DataProcess.ColumnName);
			retValue = new string[]
			{
				$"Logs\\DataProcess{processID}.txt",
				$"Logs\\{processName}Process.txt"
			};
			return retValue;
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region ProcessGroup

		// Handles the form keys.
		private void ProcessGroupGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mGroupGridCode.DoRefreshProcessGroup();
					e.Handled = true;
					break;

				case Keys.Enter:
					mGroupGridCode.DoEditProcessGroup();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						DataProcessGrid.Select();
					}
					else
					{
						DataProcessGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void ProcessGroupGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& ProcessGroupGrid.LJCIsDifferentRow(e))
			{
				ProcessGroupGrid.LJCSetCurrentRow(e);
				TimedChange(Change.ProcessGroup);
			}
		}

		// Handles the SelectionChanged event.
		private void ProcessGroupGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (ProcessGroupGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.ProcessGroup);
			}
			ProcessGroupGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void ProcessGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (ProcessGroupGrid.LJCGetMouseRow(e) != null)
			{
				mGroupGridCode.DoEditProcessGroup();
			}
		}
		#endregion

		#region DataProcess

		// Handles the form keys.
		private void DataProcessGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mProcessGridCode.DoRefreshDataProcess();
					e.Handled = true;
					break;

				case Keys.Enter:
					mProcessGridCode.DoEditDataProcess();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						ProcessGroupGrid.Select();
					}
					else
					{
						ProcessGroupGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void DataProcessGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& DataProcessGrid.LJCIsDifferentRow(e))
			{
				DataProcessGrid.LJCSetCurrentRow(e);
				TimedChange(Change.DataProcess);
			}
		}

		// Handles the SelectionChanged event.
		private void DataProcessGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (DataProcessGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.DataProcess);
			}
			DataProcessGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void DataProcessGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (DataProcessGrid.LJCGetMouseRow(e) != null)
			{
				mProcessGridCode.DoEditDataProcess();
			}
		}
		#endregion

		#endregion
	}
}
