// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityList.cs
using System;
using System.Windows.Forms;

namespace CVRManager
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
	internal partial class FacilityList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		internal FacilityList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "CVRManager.chm";
			LJCHelpPageList = @"Facility\FacilityList.html";
			LJCHelpPageDetail = @"Facility\FacilityDetail.html";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void FacilityList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void MainMenuNew_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoNewFacility();
		}

		// Calls the Edit method.
		private void MainMenuEdit_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoEditFacility();
		}

		// Calls the Delete method.
		private void MainMenuDelete_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoDeleteFacility();
		}

		// Calls the Refresh method.
		private void MainMenuRefresh_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoRefreshFacility();
		}

		// Export the list items to a text file.
		private void MainMenuExportText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\Facility.{mSettings.ExportTextExtension}";
			FacilityGrid.LJCExportData(fileSpec);
		}

		// Export the list items to a CSV file.
		private void MainMenuExportCSV_Click(object sender, EventArgs e)
		{
			FacilityGrid.LJCExportData(@"ExportFiles\Facility.csv");
		}

		// Closes the application.
		private void MainMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void MainMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, LJCHelpPageList);
		}
		#endregion

		#region Control Event Handlers

		// Handles the Grid keys.
		private void FacilityGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, LJCHelpPageList);
					break;

				case Keys.F5:
					mFacilityGridCode.DoRefreshFacility();
					e.Handled = true;
					break;

				case Keys.Enter:
					mFacilityGridCode.DoEditFacility();
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void FacilityGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& FacilityGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				FacilityGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Facility);
			}
		}

		// Handles the SelectionChanged event.
		private void FacilityGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (FacilityGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Facility);
			}
			FacilityGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void FacilityGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (FacilityGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mFacilityGridCode.DoEditFacility();
			}
		}
		#endregion
	}
}
