// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataSourceList.cs
using System;
using System.Windows.Forms;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	internal partial class DataSourceList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		internal DataSourceList()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void DataSourceList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void DataSourceToolNew_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoNewDataSource();
		}

		// Calls the Edit method.
		private void DataSourceToolEdit_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoEditDataSource();
		}

		// Calls the Delete method.
		private void DataSourceToolDelete_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoDeleteDataSource();
		}

		// Calls the New method.
		private void DataSourceMenuNew_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoNewDataSource();
		}

		// Calls the Edit method.
		private void DataSourceMenuEdit_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoEditDataSource();
		}

		// Calls the Delete method.
		private void DataSourceMenuDelete_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoDeleteDataSource();
		}

		// Calls the Refresh method.
		private void DataSourceMenuRefresh_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoRefreshDataSource();
		}

		// Calls the Select method.
		private void DataSourceMenuSelect_Click(object sender, EventArgs e)
		{
			mDataSourceGridCode.DoSelectDataSource();
		}

		// Performs the Close function.
		private void DataSourceMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}
		#endregion

		#region Control Event Handlers

		// Handles the form keys.
		private void DataSourceGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mDataSourceGridCode.DoRefreshDataSource();
					e.Handled = true;
					break;

				case Keys.Enter:
					mDataSourceGridCode.DoDefaultDataSource();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						DataSourceTool.Select();
					}
					else
					{
						DataSourceTool.Select();
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
				mDataSourceGridCode.DoDefaultDataSource();
			}
		}
		#endregion
	}
}
