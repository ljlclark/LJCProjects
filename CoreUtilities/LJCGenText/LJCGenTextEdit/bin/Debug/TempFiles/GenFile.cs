// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocEdit.cs
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCGenDocEditDAL;
using System;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../LJCGenDoc/Common/List.xml'/>
	internal partial class LJCGenDocEditList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal LJCGenDocEditList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Initialize property values.
			LJCIsSelect = false;

			// Set default class data.
			// Set DAL config before using anywhere in the program.
			var configValues = ValuesGenDocEdit.Instance;
			configValues.SetConfigFile("LJCGenDocEdit.exe.config");
			var settings = configValues.StandardSettings;
			Text += $" - {settings.DataConfigName}";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void DocAssemblyGroupList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		#region Tabs

		// Performs a Move of the selected Main Tab to the TileTabs control.
		private void MainTabsMove_Click(object sender, EventArgs e)
		{
			MainTabs.LJCMoveTabPageRight(TileTabs, TabsSplit);
		}

		// Performs a Move of the selected Tile Tab to the MainTabs control.
		private void TileTabsMove_Click(object sender, EventArgs e)
		{
			TileTabs.LJCMoveTabPageLeft(MainTabs, TabsSplit);
		}
		#endregion

		#region DocAssemblyGroup

		// Calls the New method.
		private void DocAssemblyGroupToolNew_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoNew();
		}

		// Calls the Edit method.
		private void DocAssemblyGroupToolEdit_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoEdit();
		}

		// Calls the Delete method.
		private void DocAssemblyGroupToolDelete_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoDelete();
		}

		// Calls the New method.
		private void DocAssemblyGroupNew_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoNew();
		}

		// Calls the Edit method.
		private void DocAssemblyGroupEdit_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoEdit();
		}

		// Calls the Delete method.
		private void DocAssemblyGroupDelete_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoDelete();
		}

		// Calls the Refresh method.
		private void DocAssemblyGroupRefresh_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoRefresh();
		}

		// Export a text file.
		private void DocAssemblyGroupText_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\DocAssemblyGroup.{extension}";
			DocAssemblyGroupGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void DocAssemblyGroupCSV_Click(object sender, EventArgs e)
		{
			string fileSpec = $@"ExportFiles\DocAssemblyGroup.csv";
			DocAssemblyGroupGrid.LJCExportData(fileSpec);
		}

		// Calls the Select method.
		private void DocAssemblyGroupSelect_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoSelect();
		}

		// Performs the Close function.
		private void DocAssemblyGroupClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void DocAssemblyGroupMenuHelp_Click(object sender, EventArgs e)
		{
			DocAssemblyGroupGridCode.DoHelp();
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Tabs

		// Handles the MouseDown event.
		private void MainTabs_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				MainTabs.LJCSetCurrentTabPage(e);
			}
			SetFocusTab(e);
		}

		// Handles the MouseDown event.
		private void TileTabs_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TileTabs.LJCSetCurrentTabPage(e);
			}
			SetFocusTab(e);
		}
		#endregion

		#region Combo

		//// Handles the SelectedIndexChanged event.
		//private void Combo_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	ChangeTimer.DoChange(Change.Startup.ToString());
		//}
		#endregion

		#region DocAssemblyGroup

		// Handles the form keys.
		private void DocAssemblyGroupGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					DoDefaultDocAssemblyGroup();
					e.Handled = true;
					break;

				case Keys.F1:
					DocAssemblyGroupGridCode.DoHelp();
					e.Handled = true;
					break;

				case Keys.F5:
					DocAssemblyGroupGridCode.DoRefresh();
					e.Handled = true;
					break;

				case Keys.M:
					if (e.Control)
					{
						var position = FormCommon.GetMenuScreenPoint(DocAssemblyGroupGrid
							, MousePosition);
						DocAssemblyGroupMenu.Show(position);
						DocAssemblyGroupMenu.Select();
						e.Handled = true;
					}
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						//Combo.Select();
					}
					else
					{
						//Combo.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDoubleClick event.
		private void DocAssemblyGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (DocAssemblyGroupGrid.LJCGetMouseRow(e) != null)
			{
				DocAssemblyGroupGridCode.DoDefault();
			}
		}

		// Handles the MouseDown event.
		private void DocAssemblyGroupGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
				DocAssemblyGroupGrid.Select();
				if (DocAssemblyGroupGrid.LJCIsDifferentRow(e))
				{
					// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
					DocAssemblyGroupGrid.LJCSetCurrentRow(e);
					TimedChange(Change.DocAssemblyGroup);
				}
			}
		}

		// Handles the SelectionChanged event.
		private void DocAssemblyGroupGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (DocAssemblyGroupGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.DocAssemblyGroup);
			}
			DocAssemblyGroupGrid.LJCAllowSelectionChange = true;
		}
		#endregion
		#endregion
	}
}
