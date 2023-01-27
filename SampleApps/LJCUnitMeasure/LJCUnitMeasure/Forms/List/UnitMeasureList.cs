// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitMeasureList.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;

namespace LJCUnitMeasure
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
	internal partial class UnitMeasureList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitMeasureList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "UnitMeasure.chm";
			LJCHelpPageList = "UnitMeasureList.html";
			LJCHelpPageDetail = "UnitMeasureDetail.html";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void UnitMeasureList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		#region UnitSystem

		// Calls the New method.
		private void SystemMenuNew_Click(object sender, EventArgs e)
		{
			mUnitSystemComboCode.DoNew();
		}

		// Calls the Edit method.
		private void SystemMenuEdit_Click(object sender, EventArgs e)
		{
			mUnitSystemComboCode.DoEdit();
		}

		// Calls the Delete method.
		private void SystemMenuDelete_Click(object sender, EventArgs e)
		{
			mUnitSystemComboCode.DoDelete();
		}

		// Calls the Refresh method.
		private void SystemMenuRefresh_Click(object sender, EventArgs e)
		{
			mUnitSystemComboCode.DoRefresh();
		}

		// Export a text file.
		private void SystemMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\UnitSystem.{mSettings.ExportTextExtension}";
			SystemCombo.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void SystemMenuCSV_Click(object sender, EventArgs e)
		{
			SystemCombo.LJCExportData(@"ExportFiles\UnitSystem.csv");
		}

		// Performs the Close function.
		private void SystemMenuExit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void SystemMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "UnitSystemDropdown.html");
		}
		#endregion

		#region UnitCategory

		// Calls the New method.
		private void CategoryMenuNew_Click(object sender, EventArgs e)
		{
			mUnitCategoryComboCode.DoNew();
		}

		// Calls the Edit method.
		private void CategoryMenuEdit_Click(object sender, EventArgs e)
		{
			mUnitCategoryComboCode.DoEdit();
		}

		// Calls the Delete method.
		private void CategoryMenuDelete_Click(object sender, EventArgs e)
		{
			mUnitCategoryComboCode.DoDelete();
		}

		// Calls the Refresh method.
		private void CategoryMenuRefresh_Click(object sender, EventArgs e)
		{
			mUnitCategoryComboCode.DoRefresh();
		}

		// Export a text file.
		private void CategoryMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\UnitCategory.{mSettings.ExportTextExtension}";
			CategoryCombo.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void CategoryMenuCSV_Click(object sender, EventArgs e)
		{
			CategoryCombo.LJCExportData(@"ExportFiles\UnitCategory.csv");
		}

		// Performs the Close function.
		private void CategoryMenuExit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void CategoryMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "UnitCategoryDropdown.html");
		}
		#endregion

		#region UnitMeasure

		// Calls the New method.
		private void MeasureMenuNew_Click(object sender, EventArgs e)
		{
			mUnitMeasureGridCode.DoNew();
		}

		// Calls the Edit method.
		private void MeasureMenuEdit_Click(object sender, EventArgs e)
		{
			mUnitMeasureGridCode.DoEdit();
		}

		// Calls the Delete method.
		private void MeasureMenuDelete_Click(object sender, EventArgs e)
		{
			mUnitMeasureGridCode.DoDelete();
		}

		// Calls the Refresh method.
		private void MeasureMenuRefresh_Click(object sender, EventArgs e)
		{
			mUnitMeasureGridCode.DoRefresh();
		}

		// Export a text file.
		private void MeasureMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\UnitMeasure.{mSettings.ExportTextExtension}";
			UnitMeasureGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void MeasureMenuCSV_Click(object sender, EventArgs e)
		{
			UnitMeasureGrid.LJCExportData(@"ExportFiles\UnitMeasure.csv");
		}

		// Performs the Close function.
		private void MeasureMenuExit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void MeasureMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, LJCHelpPageList);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region System Combo

		// Handles the SelectedIndexChanged event.
		private void SystemCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.UnitSystem);
		}
		#endregion

		#region Category Combo

		// Handles the SelectedIndexChanged event.
		private void CategoryCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.UnitCategory);
		}
		#endregion

		#region UnitMeasure

		// Handles the form keys.
		private void UnitMeasureGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, LJCHelpPageList);
					break;

				case Keys.F5:
					mUnitMeasureGridCode.DoRefresh();
					e.Handled = true;
					break;

				case Keys.Enter:
					mUnitMeasureGridCode.DoEdit();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						CategoryCombo.Select();
					}
					else
					{
						SystemCombo.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void UnitMeasureGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& UnitMeasureGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				UnitMeasureGrid.LJCSetCurrentRow(e);
				TimedChange(Change.UnitMeasure);
			}
		}

		// Handles the SelectionChanged event.
		private void UnitMeasureGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (UnitMeasureGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.UnitMeasure);
			}
			UnitMeasureGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void UnitMeasureGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (UnitMeasureGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mUnitMeasureGridCode.DoEdit();
			}
		}
		#endregion
		#endregion
	}
}
