// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessStepModule.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The tab composite user control.
	/// <include path='items/Module/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class ProcessStepModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProcessStepModule()
		{
			InitializeComponent();
			string errorText = TransformCommon.CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				MessageBox.Show(errorText);
				ClosePage(ProcessStepPage);
			}
		}
		#endregion

		#region Action Event Handlers

		#region Step

		// Calls the New method.
		private void StepToolNew_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoNewStep();
		}

		// Calls the Edit method.
		private void StepToolEdit_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoEditStep();
		}

		// Calls the Delete method.
		private void StepToolDelete_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoDeleteStep();
		}

		// Calls the New method.
		private void StepMenuNew_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoNewStep();
		}

		// Calls the Edit method.
		private void StepMenuEdit_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoEditStep();
		}

		// Calls the Delete method.
		private void StepMenuDelete_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoDeleteStep();
		}

		// Calls the Refresh method.
		private void StepMenuRefresh_Click(object sender, EventArgs e)
		{
			mStepGridCode.DoRefreshStep();
		}

		// Performs the Close function.
		private void StepMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(ProcessStepPage);
		}
		#endregion

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

		// Performs the Close function.
		private void TaskMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(ProcessStepPage);
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
		#endregion

		#region Step

		// Handles the form keys.
		private void StepGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mStepGridCode.DoRefreshStep();
					e.Handled = true;
					break;

				case Keys.Enter:
					mStepGridCode.DoEditStep();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						DataProcessCombo.Select();
					}
					else
					{
						TaskGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void StepGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& StepGrid.LJCIsDifferentRow(e))
			{
				StepGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Step);
			}
		}

		// Handles the SelectionChanged event.
		private void StepGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (StepGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Step);
			}
			StepGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void StepGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (StepGrid.LJCGetMouseRow(e) != null)
			{
				mStepGridCode.DoEditStep();
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
						StepGrid.Select();
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
		#endregion
	}
}
