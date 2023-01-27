// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MainList.cs
using System;
using System.Windows.Forms;

namespace DataHelper
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
	public partial class MainList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public MainList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void MainList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToScreen();
		}
		#endregion

		#region Action Event Handlers

		#region Tabs Menus

		// Move the active DataTabs Page to the DataTileTabs.
		private void DataTabMenuMove_Click(object sender, EventArgs e)
		{
			if (DataTabs.TabPages.Count > 1)
			{
				TableTabsSplit.Panel2Collapsed = false;

				// Calls Event LJCPanelManager.LJCTabs_ControlAdded().
				DataTabs.SelectedTab.Parent = DataTileTabs;
			}
		}

		// Move the active DataTileTabs Page to the DataTabs.
		private void DataTileTabsMenuMove_Click(object sender, EventArgs e)
		{
			DataTileTabs.SelectedTab.Parent = DataTabs;
			if (0 == DataTileTabs.TabPages.Count)
			{
				TableTabsSplit.Panel2Collapsed = true;
			}
		}

		// Move the active ColumnTabs Page to the ColumnTileTabs.
		private void ColumnTabsMenuMove_Click(object sender, EventArgs e)
		{
			if (ColumnTabs.TabPages.Count > 1)
			{
				ColumnTabsSplit.Panel2Collapsed = false;

				// Calls Event LJCPanelManager.LJCTabs_ControlAdded().
				ColumnTabs.SelectedTab.Parent = ColumnTileTabs;
			}
		}

		// Move the active ColumnTileTabs Page to the ColumnTabs.
		private void ColumnTileTabsMenuMove_Click(object sender, EventArgs e)
		{
			ColumnTileTabs.SelectedTab.Parent = ColumnTabs;
			if (0 == ColumnTileTabs.TabPages.Count)
			{
				ColumnTabsSplit.Panel2Collapsed = true;
			}
		}
		#endregion

		#region Table

		// Calls the New method.
		private void TableMenuNew_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoNewTable();
		}

		// Calls the Edit method.
		private void TableMenuEdit_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoEditTable();
		}

		// Calls the Delete method.
		private void TableMenuDelete_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoDeleteTable();
		}

		// Calls the Refresh method.
		private void TableMenuRefresh_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoRefreshTable();
		}

		// Calls the Export Text method.
		private void TableMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\ExportMDTable.{mSettings.ExportTextExtension}";
			TableGrid.LJCExportData(fileSpec);
		}

		// Calls the Export CSV method.
		private void TableMenuCSV_Click(object sender, EventArgs e)
		{
			TableGrid.LJCExportData(@"ExportFiles\ExportMDTable.csv");
		}

		// Performs the Close function.
		private void TableMenuExit_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoCloseTable();
			mColumnGridCode.DoCloseColumn();
			mKeyGridCode.DoCloseKey();
			SaveControlValues();
			Close();
		}
		#endregion

		#region Column

		// Calls the New method.
		private void ColumnMenuNew_Click(object sender, EventArgs e)
		{
			mColumnGridCode.DoNewColumn();
		}

		// Calls the Edit method.
		private void ColumnMenuEdit_Click(object sender, EventArgs e)
		{
			mColumnGridCode.DoEditColumn();
		}

		// Calls the Delete method.
		private void ColumnMenuDelete_Click(object sender, EventArgs e)
		{
			mColumnGridCode.DoDeleteColumn();
		}

		// Calls the Refresh method.
		private void ColumnMenuRefresh_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoRefreshTable();
		}

		// Calls the Export Text method.
		private void ColumnMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\ExportMDColumn.{mSettings.ExportTextExtension}";
			ColumnGrid.LJCExportData(fileSpec);
		}

		// Calls the Export CSV method.
		private void ColumnMenuCSV_Click(object sender, EventArgs e)
		{
			ColumnGrid.LJCExportData(@"ExportFiles\ExportMDColumn.csv");
		}

		// Performs the Close function.
		private void ColumnMenuExit_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoCloseTable();
			mColumnGridCode.DoCloseColumn();
			mKeyGridCode.DoCloseKey();
			SaveControlValues();
			Close();
		}
		#endregion

		#region Key

		// Calls the New method.
		private void KeyMenuNew_Click(object sender, EventArgs e)
		{
			mKeyGridCode.DoNewKey();
		}

		// Calls the Edit method.
		private void KeyMenuEdit_Click(object sender, EventArgs e)
		{
			mKeyGridCode.DoEditKey();
		}

		// Calls the Delete method.
		private void KeyMenuDelete_Click(object sender, EventArgs e)
		{
			mKeyGridCode.DoDeleteKey();
		}

		// Calls the Refresh method.
		private void KeyMenuRefresh_Click(object sender, EventArgs e)
		{
			mKeyGridCode.DoRefreshKey();
		}

		// Calls the Export Text method.
		private void KeyMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\ExportMDKey.{mSettings.ExportTextExtension}";
			KeyGrid.LJCExportData(fileSpec);
		}

		// Calls the Export CSV method.
		private void KeyMenuCSV_Click(object sender, EventArgs e)
		{
			KeyGrid.LJCExportData(@"ExportFiles\ExportMDKey.csv");
		}

		// Performs the Close function.
		private void KeyMenuExit_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoCloseTable();
			mColumnGridCode.DoCloseColumn();
			mKeyGridCode.DoCloseKey();
			SaveControlValues();
			Close();
		}
		#endregion

		#region Data

		// Calls the New method.
		private void DataMenuNew_Click(object sender, EventArgs e)
		{
			mDataGridCode.DoNewData();
		}

		// Calls the Edit method.
		private void DataMenuEdit_Click(object sender, EventArgs e)
		{
			mDataGridCode.DoEditData();
		}

		// Calls the Delete method.
		private void DataMenuDelete_Click(object sender, EventArgs e)
		{
			mDataGridCode.DoDeleteData();
		}

		// Calls the Refresh method.
		private void DataMenuRefresh_Click(object sender, EventArgs e)
		{
			mDataGridCode.DoRefreshData();
		}

		// Calls the Export Text method.
		private void DataMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\ExportMDData.{mSettings.ExportTextExtension}";
			DataGrid.LJCExportData(fileSpec);
		}

		// Calls the Export CSV method.
		private void DataMenuCSV_Click(object sender, EventArgs e)
		{
			DataGrid.LJCExportData(@"ExportFiles\ExportMDData.csv");
		}

		// Performs the Close function.
		private void DataMenuExit_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoCloseTable();
			mColumnGridCode.DoCloseColumn();
			mKeyGridCode.DoCloseKey();
			SaveControlValues();
			Close();
		}
		#endregion

		#region Child

		// Calls the New method.
		private void ChildMenuNew_Click(object sender, EventArgs e)
		{
			mChildGridCode.DoNewChild();
		}

		// Calls the Edit method.
		private void ChildMenuEdit_Click(object sender, EventArgs e)
		{
			mChildGridCode.DoEditChild();
		}

		// Calls the Delete method.
		private void ChildMenuDelete_Click(object sender, EventArgs e)
		{
			mChildGridCode.DoDeleteChild();
		}

		// Calls the Refresh method.
		private void ChildMenuRefresh_Click(object sender, EventArgs e)
		{
			mChildGridCode.DoRefreshChild();
		}

		// Calls the Export Text method.
		private void ChildMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\ExportMDChile.{mSettings.ExportTextExtension}";
			ChildGrid.LJCExportData(fileSpec);
		}

		// Calls the Export CSV method.
		private void ChildMenuCSV_Click(object sender, EventArgs e)
		{
			ChildGrid.LJCExportData(@"ExportFiles\ExportMDChild.csv");
		}

		// Performs the Close function.
		private void ChildMenuExit_Click(object sender, EventArgs e)
		{
			mTableGridCode.DoCloseTable();
			mColumnGridCode.DoCloseColumn();
			mKeyGridCode.DoCloseKey();
			SaveControlValues();
			Close();
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Table

		// Handles the form keys.
		private void TableGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mTableGridCode.DoRefreshTable();
					e.Handled = true;
					break;

				case Keys.Enter:
					mTableGridCode.DoEditTable();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						ColumnGrid.Select();
					}
					else
					{
						ColumnGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void TableGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& TableGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				TableGrid.LJCSetCurrentRow(e);
				ChangeTimer.DoChange(ChangeTable);
			}
		}

		// Handles the SelectionChanged event.
		private void TableGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (TableGrid.LJCAllowSelectionChange)
			{
				ChangeTimer.DoChange(ChangeTable);
			}
			TableGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void TableGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (KeyGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mTableGridCode.DoEditTable();
			}
		}
		#endregion

		#region Column

		// Handles the form keys.
		private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mColumnGridCode.DoRefreshColumn();
					e.Handled = true;
					break;

				case Keys.Enter:
					mColumnGridCode.DoEditColumn();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						TableGrid.Select();
					}
					else
					{
						TableGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& ColumnGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				ColumnGrid.LJCSetCurrentRow(e);
				ChangeTimer.DoChange(ChangeColumn);
			}
		}

		// Handles the SelectionChanged event.
		private void ColumnGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (ColumnGrid.LJCAllowSelectionChange)
			{
				ChangeTimer.DoChange(ChangeColumn);
			}
			ColumnGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (KeyGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mColumnGridCode.DoEditColumn();
			}
		}
		#endregion

		#region Key

		// Handles the form keys.
		private void KeyGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mKeyGridCode.DoRefreshKey();
					e.Handled = true;
					break;

				case Keys.Enter:
					mKeyGridCode.DoEditKey();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						TableGrid.Select();
					}
					else
					{
						TableGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void KeyGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& KeyGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				KeyGrid.LJCSetCurrentRow(e);
				ChangeTimer.DoChange(ChangeKey);
			}
		}

		// Handles the SelectionChanged event.
		private void KeyGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (KeyGrid.LJCAllowSelectionChange)
			{
				ChangeTimer.DoChange(ChangeKey);
			}
			KeyGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void KeyGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (KeyGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mKeyGridCode.DoEditKey();
			}
		}
		#endregion

		#region Data

		// Handles the form keys.
		private void DataGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mDataGridCode.DoRefreshData();
					e.Handled = true;
					break;

				case Keys.Enter:
					mDataGridCode.DoEditData();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						ChildGrid.Select();
					}
					else
					{
						ChildGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void DataGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& DataGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				DataGrid.LJCSetCurrentRow(e);
				ChangeTimer.DoChange(ChangeData);
			}
		}

		// Handles the SelectionChanged event.
		private void DataGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (DataGrid.LJCAllowSelectionChange)
			{
				ChangeTimer.DoChange(ChangeData);
			}
			DataGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void DataGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (DataGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mDataGridCode.DoEditData();
			}
		}
		#endregion

		#region Child

		// Handles the form keys.
		private void ChildGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mChildGridCode.DoRefreshChild();
					e.Handled = true;
					break;

				case Keys.Enter:
					mChildGridCode.DoEditChild();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						DataGrid.Select();
					}
					else
					{
						DataGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void ChildGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& ChildGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				ChildGrid.LJCSetCurrentRow(e);
				ChangeTimer.DoChange(ChangeChild);
			}
		}

		// Handles the SelectionChanged event.
		private void ChildGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (ChildGrid.LJCAllowSelectionChange)
			{
				ChangeTimer.DoChange(ChangeChild);
			}
			ChildGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void ChildGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (ChildGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mChildGridCode.DoEditChild();
			}
		}
		#endregion
		#endregion
	}
}
