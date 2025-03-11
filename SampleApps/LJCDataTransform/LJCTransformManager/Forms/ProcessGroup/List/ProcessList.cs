// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessList.cs
using System;
using System.Windows.Forms;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	internal partial class DataProcessList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal DataProcessList()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void ProcessList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void DataProcessToolNew_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoNewDataProcess();
		}

		// Calls the Edit method.
		private void DataProcessToolEdit_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoEditDataProcess();
		}

		// Calls the Delete method.
		private void DataProcessToolDelete_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoDeleteDataProcess();
		}

		// Calls the New method.
		private void DataProcessMenuNew_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoNewDataProcess();
		}

		// Calls the Edit method.
		private void DataProcessMenuEdit_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoEditDataProcess();
		}

		// Calls the Delete method.
		private void DataProcessMenuDelete_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoDeleteDataProcess();
		}

		// Calls the Refresh method.
		private void DataProcessMenuRefresh_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoRefreshDataProcess();
		}

		// Calls the Select method.
		private void DataProcessMenuSelect_Click(object sender, EventArgs e)
		{
			mProcessGridCode.DoSelectDataProcess();
		}

		// Performs the Close function.
		private void DataProcessMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}
		#endregion

		#region Control Event Handlers

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
					mProcessGridCode.DoDefaultDataProcess();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						DataProcessTool.Select();
					}
					else
					{
						DataProcessTool.Select();
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
				mProcessGridCode.DoDefaultDataProcess();
			}
		}
		#endregion
	}
}
