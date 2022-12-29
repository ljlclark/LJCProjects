// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJCWinFormControls;
using LJCDataAccess;
using LJCNetCommon;
using LJCWinFormCommon;

namespace CVRManager
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
	internal partial class CVVisitList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal CVVisitList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "CVRManager.chm";
			LJCHelpPageList = @"CVVisit\CVVisitList.html";
			LJCHelpPageDetail = @"CVVisit\CVVisitDetail.html";

			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void CVVisitList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		// Shows the Facility List.
		private void FacilityMenuFacility_Click(object sender, EventArgs e)
		{
			FacilityList list = new FacilityList();
			list.ShowDialog();
		}

		// Calls the New method.
		private void MainToolsNew_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoNewCVVisit();
		}

		// Calls the Edit method.
		private void MainToolsEdit_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoEditCVVisit();
		}

		// Calls the Enter method.
		private void MainToolsEnter_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoEnterCVVisit();
		}

		// Calls the Exit method.
		private void MainToolsExit_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoExitCVVisit();
		}

		// Calls the New method.
		private void MainMenuNew_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoNewCVVisit();
		}

		// Calls the Edit method.
		private void MainMenuEdit_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoEditCVVisit();
		}

		// Sets the Customer Enter time.
		private void MainMenuEnter_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoEnterCVVisit();
		}

		// Sets the Customer Exit time.
		private void MainMenuExit_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoExitCVVisit();
		}

		// Calls the Delete method.
		private void MainMenuDelete_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoDeleteCVVisit();
		}

		// Calls the Refresh method.
		private void MainMenuRefresh_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoRefreshCVVisit();
		}

		// Export the list items to a text file.
		private void MainMenuExportText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\CVVisit.{Settings.ExportTextExtension}";
			CVVisitGrid.LJCExportData(fileSpec);
		}

		// Export the list items to a CSV file.
		private void MainMenuExportCSV_Click(object sender, EventArgs e)
		{
			CVVisitGrid.LJCExportData(@"ExportFiles\CVVisit.csv");
		}

		// Allow for edit of text files.
		private void MainFileEdit_Click(object sender, EventArgs e)
		{
			FormCommon.ShellFile("NotePad.exe");
		}

		// Shows the Facility List.
		private void MainMenuFacility_Click(object sender, EventArgs e)
		{
			FacilityList list = new FacilityList();
			list.ShowDialog();
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

		#region Facility

		// Handles the SelectionChanged event.
		private void FacilityCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			mFacilityID = FacilityCombo.LJCSelectedItemID();
			TimedChange(Change.Facility);
		}
		#endregion

		#region Visit List

		// Handles the Grid keys.
		private void CVVisitGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					mCVVisitGridCode.DoEditCVVisit();
					e.Handled = true;
					break;

				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, LJCHelpPageList);
					e.Handled = true;
					break;

				case Keys.F5:
					mCVVisitGridCode.DoRefreshCVVisit();
					e.Handled = true;
					break;

				case Keys.M:
					if (e.Control)
					{
						var position = FormCommon.GetMenuScreenPoint(CVVisitGrid
							, MousePosition);
						MainMenu.Show(position);
						MainMenu.Select();
						e.Handled = true;
					}
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						MainTools.Select();
					}
					else
					{
						MainTools.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void CVVisitGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& CVVisitGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				CVVisitGrid.LJCSetCurrentRow(e);
				TimedChange(Change.CVVisit);
			}
		}

		// Handles the SelectionChanged event.
		private void CVVisitGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (CVVisitGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.CVVisit);
			}
			CVVisitGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void CVVisitGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (CVVisitGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mCVVisitGridCode.DoEditCVVisit();
			}
		}
		#endregion

		#region Filters

		// Show the Calendar control.
		private void DateButton_Click(object sender, EventArgs e)
		{
			DateMask.LJCText = ControlCommon.GetSelectedDate(DateMask.Text);
		}

		// Display the filtered list.
		private void ShowButton_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoShow();
		}

		// Clear the list filters.
		private void ClearFiltersButton_Click(object sender, EventArgs e)
		{
			mCVVisitGridCode.DoClearFilters();
			mCVVisitGridCode.DoShow();
		}

		// Display the Filters dialog.
		private void FiltersButton_Click(object sender, EventArgs e)
		{
			VisitFilterDetail detail = new VisitFilterDetail()
			{
				BeginDate = DataCommon.GetDbDate(DateMask.Text),
				EndDate = DataCommon.GetDbDate(mEndDateString),
				LastName = mLastName,
				MiddleName = mMiddleName,
				FirstName = mFirstName,
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = @"CVVisit\VisitFilterDetail.html"
			};
			if (DialogResult.OK == detail.ShowDialog())
			{
				DateMask.LJCText = null;
				if (detail.BeginDate != DataCommon.GetMinDateTime())
				{
					DateMask.LJCText = DataCommon.GetUIDateString(detail.BeginDate);
				}
				mEndDateString = null;
				if (detail.EndDate != DataCommon.GetMinDateTime())
				{
					//mEndDateString = DataCommon.GetUIDateString(detail.EndDate);
					mEndDateString = DataCommon.GetDbDateString(detail.EndDate);
				}
				mLastName = detail.LastName;
				mMiddleName = detail.MiddleName;
				mFirstName = detail.FirstName;
				ClearFiltersButton.Visible = true;
				mCVVisitGridCode.DoShow();
			}
		}
		#endregion
		#endregion
	}
}
