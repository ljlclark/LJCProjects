// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskSourceModule.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormControls;
using LJCDBClientLib;
using LJCDataTransformDAL;
using LJCDBMessage;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LJCDataTransformProcess;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The tab composite user control.
	/// <include path='items/Module/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class TaskSourceModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskSourceModule()
		{
			InitializeComponent();
			string errorText = TransformCommon.CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				MessageBox.Show(errorText);
				ClosePage(TaskSourcePage);
			}
		}
		#endregion

		#region Action Event Handlers

		#region Task

		// Calls the New method.
		private void TaskToolNew_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoNewTask();
		}

		// Calls the Edit method.
		private void TaskToolEdit_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoEditTask();
		}

		// Calls the Delete method.
		private void TaskToolDelete_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoDeleteTask();
		}

		// Calls the New method.
		private void TaskMenuNew_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoNewTask();
		}

		// Calls the Edit method.
		private void TaskMenuEdit_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoEditTask();
		}

		// Calls the Delete method.
		private void TaskMenuDelete_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoDeleteTask();
		}

		// Calls the Refresh method.
		private void TaskMenuRefresh_Click(object sender, EventArgs e)
		{
			mTaskGridCode.DoRefreshTask();
		}

		// Executes a StepTask.
		private void TaskMenuRun_Click(object sender, EventArgs e)
		{
			LJCTransformProcess transformProcess;

			int dataProcessID = DataProcessCombo.LJCSelectedItemID();

			if (TaskGrid.CurrentRow is LJCGridRow row)
			{
				//int stepTaskID = row.LJCGetInt32(StepTask.ColumnStepTaskID);
				//StepTask stepTask = mTaskManager.RetrieveWithID(stepTaskID);
				StepTask stepTask = GetRowStepTask(row);

				transformProcess = new LJCTransformProcess(mSettings.DataConfigName)
				{
					DataProcessID = dataProcessID
				};
				bool success = transformProcess.ProcessStepTask(stepTask);
				if (success)
				{
					MessageBox.Show("Run Task was successful.", "Task Run Confirmation"
						, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		// Show the Data Transform Process log.
		private void TaskMenuProcessLog_Click(object sender, EventArgs e)
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

		// Performs the Close function.
		private void TaskMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(TaskSourcePage);
		}

		// Gets the Process log file specifications.
		private string[] GetProcessLogFileSpecs()
		{
			// Note: Also in: LJCTransformService.cs, LJCTransformProcess.cs,
			//			 CommonModule.cs and ProcessGroupModule.cs.
			int processID;
			bool success = true;
			string[] retValue = null;

			if (null == mDataProcess)
			{
				processID = DataProcessCombo.LJCSelectedItemID();
				DataProcessManager dataProcessManager = Managers.DataProcessManager;
				mDataProcess = dataProcessManager.RetrieveWithID(processID);
			}

			if (success)
			{
				retValue = new string[]
				{
					$"Logs\\DataProcess{mDataProcess.DataProcessID}.txt",
					$"Logs\\{mDataProcess.Name}Process.txt"
				};
			}
			return retValue;
		}
		#endregion

		#region TaskTransform

		// Calls the New method.
		private void TransformToolNew_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoNewTransform();
		}

		// Calls the Edit method.
		private void TransformToolEdit_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoEditTransform();
		}

		// Calls the Delete method.
		private void TransformToolDelete_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoDeleteTransform();
		}

		// Calls the New method.
		private void TransformMenuNew_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoNewTransform();
		}

		// Calls the Edit method.
		private void TransformMenuEdit_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoEditTransform();
		}

		// Calls the Delete method.
		private void TransformMenuDelete_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoDeleteTransform();
		}

		// Calls the Refresh method.
		private void TransformMenuRefresh_Click(object sender, EventArgs e)
		{
			mTransformGridCode.DoRefreshTransform();
		}

		// Display the Match List.
		private void TransformMenuMatch_Click(object sender, EventArgs e)
		{
			if (TransformGrid.CurrentRow is LJCGridRow row)
			{
				// Data from parent window or list.
				int parentID = row.LJCGetInt32(TaskTransform.ColumnTransformID);

				MatchList list = new MatchList
				{
					LJCParentID = parentID
				};
				list.ShowDialog();
			}
		}

		// Display the MergeMap List.
		private void TransformMenuMerge_Click(object sender, EventArgs e)
		{
			if (TransformGrid.CurrentRow is LJCGridRow row)
			{
				// Data from parent window or list.
				int parentID = row.LJCGetInt32(TaskTransform.ColumnTransformID);

				MergeMapList list = new MergeMapList
				{
					LJCParentID = parentID
				};
				list.ShowDialog();
			}
		}

		// Performs the Close function.
		private void TransformMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(TaskSourcePage);
		}
		#endregion

		#region DataSource

		// Calls the New method.
		private void SourceToolNew_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoAddDataSource();
		}

		// Calls the Edit method.
		private void SourceToolEdit_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoEditDataSource();
		}

		// Calls the Delete method.
		private void SourceToolDelete_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoRemoveDataSource();
		}

		// Calls the New method.
		private void DataSourceMenuNew_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoAddDataSource();
		}

		// Calls the Edit method.
		private void DataSourceMenuEdit_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoEditDataSource();
		}

		// Calls the Delete method.
		private void DataSourceMenuDelete_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoRemoveDataSource();
		}

		// Calls the Refresh method.
		private void DataSourceMenuRefresh_Click(object sender, EventArgs e)
		{
			mSourceGridCode.DoRefreshDataSource();
		}

		// Select the Layout.
		private void DataSourceMenuLayoutSelect_Click(object sender, EventArgs e)
		{
			SourceLayoutManager sourceLayoutManager;
			DataSource dataSource = null;
			SourceLayout sourceLayout = null;
			LayoutColumnList list = null;
			int dataSourceID = 0;
			bool success = true;

			if (DataSourceGrid.CurrentRow is LJCGridRow row)
			{
				dataSource = mSourceGridCode.GetRowDataSource(row);
				if (null == dataSource)
				{
					success = false;
				}
				else
				{
					dataSourceID = dataSource.DataSourceID;
					int id = dataSource.SourceLayoutID;
					sourceLayoutManager = Managers.SourceLayoutManager;
					sourceLayout = sourceLayoutManager.RetrieveWithID(id);
					if (null == sourceLayout)
					{
						success = false;
					}
				}
			}

			if (success)
			{
				list = new LayoutColumnList()
				{
					LJCIsSelect = true,
					LJCSelectedRecord = sourceLayout
				};
				list.ShowDialog();

				if (list.DialogResult != DialogResult.OK
					|| null == list.LJCSelectedRecord)
				{
					success = false;
				}
			}

			if (success)
			{
				// Create data record.
				int sourceLayoutID = list.LJCSelectedRecord.SourceLayoutID;
				dataSource.SourceLayoutID = sourceLayoutID;

				// Define update columns.
				List<string> columnNames = new List<string>()
				{
					DataSource.ColumnSourceLayoutID
				};

				DataSourceManager dataSourceManager = Managers.DataSourceManager;
				var keyColumns = dataSourceManager.GetIDKey(dataSourceID);
				dataSourceManager.Update(dataSource, keyColumns, columnNames);
			}
		}

		// Views the Data.
		private void DataSourceMenuView_Click(object sender, EventArgs e)
		{
			if (DataSourceGrid.CurrentRow is LJCGridRow row)
			{
				// Data from parent window or list.
				int dataSourceID = row.LJCGetInt32(DataSource.ColumnDataSourceID);

				mDataViewer.Show(dataSourceID);
			}
		}

		// Performs the Drop Table function.
		private void DataSourceMenuDrop_Click(object sender, EventArgs e)
		{
			string message;

			if (DataSourceGrid.CurrentRow is LJCGridRow row)
			{
				string title = "Drop Table Confirmation";
				message = "Are you sure you want to Drop the table?";
				if (DialogResult.Yes == MessageBox.Show(message, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					DataSource dataSource = mSourceGridCode.GetRowDataSource(row);
					if (dataSource != null
						&& TableDataSource == dataSource.SourceTypeID)
					{
						string tableName = dataSource.SourceItemName;
						DataManager dataManager = new DataManager(mSettings.DbServiceRef
							, mSettings.DataConfigName, tableName);
						string sql = $"drop table {tableName}";
						dataManager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
					}
				}
			}
		}

		// Performs the Close function.
		private void DataSourceMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(TaskSourcePage);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region DataProcess

		// Handles the SelectedIndexChanged event.
		private void DataProcessCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.Process);
		}

		// Handles the DataProcess selection button.
		private void DataProcessButton_Click(object sender, EventArgs e)
		{
			DataProcess dataProcess = null;
			int id = -1;

			if (DataProcessCombo.SelectedIndex > -1)
			{
				// Data from items.
				id = DataProcessCombo.LJCSelectedItemID();
				dataProcess = Managers.DataProcessManager.RetrieveWithID(id);
			}

			DataProcessList list = new DataProcessList()
			{
				LJCIsSelect = true,
				LJCSelectedRecord = dataProcess
			};
			list.ShowDialog();

			// Reload in case item added or deleted.
			mSourceGridCode.DataRetrieveDataSource();

			if (DialogResult.OK == list.DialogResult)
			{
				// Select new item.
				if (list.LJCSelectedRecord != null)
				{
					int selectedID = list.LJCSelectedRecord.DataProcessID;
					DataProcessCombo.LJCSetByItemID(selectedID);
				}
			}
			else
			{
				// Reselect original item.
				DataProcessCombo.LJCSetByItemID(id);
			}
		}
		#endregion

		#region Step

		// Handles the SelectedIndexChanged event.
		private void StepCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.Step);
		}

		// Handles the Step selection button.
		private void StepButton_Click(object sender, EventArgs e)
		{
			Step step = null;
			int id = -1;

			if (StepCombo.SelectedIndex > -1)
			{
				id = StepCombo.LJCSelectedItemID();
				step = Managers.StepManager.RetrieveWithID(id);
			}

			if (DataProcessCombo.SelectedIndex > -1)
			{
				// Data from items.
				int parentID = DataProcessCombo.LJCSelectedItemID();
				string parentName = DataProcessCombo.Text;

				StepList list = new StepList()
				{
					LJCIsSelect = true,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCSelectedRecord = step
				};
				list.ShowDialog();

				// Reload in case item added or deleted.
				DataRetrieveStep();

				if (DialogResult.OK == list.DialogResult)
				{
					// Select new item.
					if (list.LJCSelectedRecord != null)
					{
						int selectedID = list.LJCSelectedRecord.StepID;
						StepCombo.LJCSetByItemID(selectedID);
					}
				}
				else
				{
					// Reselect original item.
					StepCombo.LJCSetByItemID(id);
				}
			}
		}
		#endregion

		#region Task

		// Handles the form keys.
		private void TaskGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mTaskGridCode.DoRefreshTask();
					e.Handled = true;
					break;

				case Keys.Enter:
					mTaskGridCode.DoEditTask();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						StepCombo.Select();
					}
					else
					{
						DataSourceGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void TaskGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& TaskGrid.LJCIsDifferentRow(e))
			{
				TaskGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Task);
			}
		}

		// Handles the SelectionChanged event.
		private void TaskGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (TaskGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Task);
			}
			TaskGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void TaskGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (TaskGrid.LJCGetMouseRow(e) != null)
			{
				mTaskGridCode.DoEditTask();
			}
		}
		#endregion

		#region TaskTransform

		// Handles the form keys.
		private void TransformGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mTransformGridCode.DoRefreshTransform();
					e.Handled = true;
					break;

				case Keys.Enter:
					mTransformGridCode.DoEditTransform();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						TaskGrid.Select();
					}
					else
					{
						DataProcessCombo.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void TransformGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& TransformGrid.LJCIsDifferentRow(e))
			{
				TransformGrid.LJCSetCurrentRow(e);
				TimedChange(Change.TaskTransform);
			}
		}

		// Handles the SelectionChanged event.
		private void TransformGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (TransformGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.TaskTransform);
			}
			TransformGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void TransformGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (TransformGrid.LJCGetMouseRow(e) != null)
			{
				mTransformGridCode.DoEditTransform();
			}
		}
		#endregion

		#region DataSource

		// Handles the form keys.
		private void DataSourceGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mSourceGridCode.DoRefreshDataSource();
					e.Handled = true;
					break;

				case Keys.Enter:
					mSourceGridCode.DoEditDataSource();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						TaskGrid.Select();
					}
					else
					{
						DataProcessCombo.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void DataSourceGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& DataSourceGrid.LJCIsDifferentRow(e))
			{
				DataSourceGrid.LJCSetCurrentRow(e);
				TimedChange(Change.DataSource);
			}
		}

		// Handles the SelectionChanged event.
		private void DataSourceGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (DataSourceGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.DataSource);
			}
			DataSourceGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void DataSourceGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (DataSourceGrid.LJCGetMouseRow(e) != null)
			{
				mSourceGridCode.DoEditDataSource();
			}
		}
		#endregion
		#endregion
	}
}
