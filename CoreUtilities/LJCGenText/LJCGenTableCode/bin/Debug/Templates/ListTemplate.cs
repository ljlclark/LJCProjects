// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _FullAppName_
// _FullAppName_.cs
// #SectionEnd Title
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using _FullAppName_DAL;
using System;
using System.Windows.Forms;

// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _NameSpace_
// #Value _VarClassName_
namespace _Namespace_
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../LJCGenDoc/Common/List.xml'/>
	internal partial class _FullAppName_List : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal _FullAppName_List()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Initialize property values.
			LJCIsSelect = false;

			// Set default class data.
			// Set DAL config before using anywhere in the program.
			var configValues = Values_AppName_.Instance;
			configValues.SetConfigFile("_FullAppName_.exe.config");
			var settings = configValues.StandardSettings;
			Text += $" - {settings.DataConfigName}";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void _ClassName_List_Load(object sender, EventArgs e)
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

		#region _ClassName_

		// Calls the New method.
		private void _ClassName_ToolNew_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoNew();
		}

		// Calls the Edit method.
		private void _ClassName_ToolEdit_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoEdit();
		}

		// Calls the Delete method.
		private void _ClassName_ToolDelete_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoDelete();
		}

		// Calls the New method.
		private void _ClassName_New_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoNew();
		}

		// Calls the Edit method.
		private void _ClassName_Edit_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoEdit();
		}

		// Calls the Delete method.
		private void _ClassName_Delete_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoDelete();
		}

		// Calls the Refresh method.
		private void _ClassName_Refresh_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoRefresh();
		}

		// Export a text file.
		private void _ClassName_Text_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\_ClassName_.{extension}";
			_ClassName_Grid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void _ClassName_CSV_Click(object sender, EventArgs e)
		{
			string fileSpec = $@"ExportFiles\_ClassName_.csv";
			_ClassName_Grid.LJCExportData(fileSpec);
		}

		// Calls the Select method.
		private void _ClassName_Select_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoSelect();
		}

		// Performs the Close function.
		private void _ClassName_Close_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void _ClassName_MenuHelp_Click(object sender, EventArgs e)
		{
			_ClassName_GridCode.DoHelp();
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

		#region _ClassName_

		// Handles the grid keys.
		private void _ClassName_Grid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					_ClassName_GridCode.DoDefault();
					e.Handled = true;
					break;

				case Keys.F1:
					_ClassName_GridCode.DoHelp();
					e.Handled = true;
					break;

				case Keys.F5:
					_ClassName_GridCode.DoRefresh();
					e.Handled = true;
					break;

				case Keys.M:
					if (e.Control)
					{
						var position = FormCommon.GetMenuScreenPoint(_ClassName_Grid
							, MousePosition);
						_ClassName_Menu.Show(position);
						_ClassName_Menu.Select();
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
		private void _ClassName_Grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (_ClassName_Grid.LJCGetMouseRow(e) != null)
			{
				_ClassName_GridCode.DoDefault();
			}
		}

		// Handles the MouseDown event.
		private void _ClassName_Grid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
				_ClassName_Grid.Select();
				if (_ClassName_Grid.LJCIsDifferentRow(e))
				{
					// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
					_ClassName_Grid.LJCSetCurrentRow(e);
					TimedChange(Change._ClassName_);
				}
			}
		}

		// Handles the SelectionChanged event.
		private void _ClassName_Grid_SelectionChanged(object sender, EventArgs e)
		{
			if (_ClassName_Grid.LJCAllowSelectionChange)
			{
				TimedChange(Change._ClassName_);
			}
			_ClassName_Grid.LJCAllowSelectionChange = true;
		}
		#endregion
		#endregion
	}
}
// #SectionEnd Class
