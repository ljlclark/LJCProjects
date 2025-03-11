// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StepList.cs
using System;
using System.Windows.Forms;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The list form.
	internal partial class StepList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal StepList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void StepList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void StepToolNew_Click(object sender, EventArgs e)
		{
			//DoNewTask();
		}

		// Calls the Edit method.
		private void StepToolEdit_Click(object sender, EventArgs e)
		{
			//DoEditTask();
		}

		// Calls the Delete method.
		private void StepDelete_Click(object sender, EventArgs e)
		{
			//DoDeleteTask();
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
			Close();
		}
		#endregion

		#region Control Event Handlers

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
					mStepGridCode.DoDefaultStep();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						StepTool.Select();
					}
					else
					{
						StepTool.Select();
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
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
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
				mStepGridCode.DoDefaultStep();
			}
		}
		#endregion
	}
}
