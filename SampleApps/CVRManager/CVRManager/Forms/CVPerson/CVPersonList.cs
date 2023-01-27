// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVPersonList.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;

namespace CVRManager
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
	internal partial class CVPersonList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal CVPersonList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "CVRManager.chm";
			LJCHelpPageList = @"CVPerson\CVPersonList.html";
			LJCHelpPageDetail = @"CVPerson\CVPersonDetail.html";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void CVPersonList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void MainToolsNew_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoNewCVPerson();
		}

		// Calls the Edit method.
		private void MainToolsEdit_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoEditCVPerson();
		}

		// Calls the Select method.
		private void MainToolsSelect_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoSelectCVPerson();
		}

		// Calls the New method.
		private void PersonMenuNew_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoNewCVPerson();
		}

		// Calls the Edit method.
		private void PersonMenuEdit_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoEditCVPerson();
		}

		// Calls the Delete method.
		private void PersonMenuDelete_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoDeleteCVPerson();
		}

		// Calls the Refresh method.
		private void PersonMenuRefresh_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoRefreshCVPerson();
		}

		// Export the list items to a text file.
		private void PersonMenuExportText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFiles\\CVPerson.{mSettings.ExportTextExtension}";
			CVPersonGrid.LJCExportData(fileSpec);
		}

		// Export the list items to a CSV file.
		private void PersonMenuExportCSV_Click(object sender, EventArgs e)
		{
			CVPersonGrid.LJCExportData(@"ExportFiles\CVPerson.csv");
		}

		// Calls the Select method.
		private void PersonMenuSelect_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			mCVPersonGridCode.DoSelectCVPerson();
		}

		// Performs the Close function.
		private void PersonMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Shows the help page.
		private void PersonMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, LJCHelpPageList);
		}
		#endregion

		#region Control Event Handlers

		// Display the filtered list.
		private void ShowButton_Click(object sender, EventArgs e)
		{
			mCVPersonGridCode.DoShow();
		}

		// Handles the form keys.
		private void CVPersonGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					mCVPersonGridCode.DoEditCVPerson();
					e.Handled = true;
					break;

				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, LJCHelpPageList);
					e.Handled = true;
					break;

				case Keys.F5:
					mCVPersonGridCode.DoRefreshCVPerson();
					e.Handled = true;
					break;

				case Keys.M:
					if (e.Control)
					{
						var position = FormCommon.GetMenuScreenPoint(CVPersonGrid
							, MousePosition);
						PersonMenu.Show(position);
						PersonMenu.Select();
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
		private void CVPersonGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& CVPersonGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				CVPersonGrid.LJCSetCurrentRow(e);
				TimedChange(Change.CVPerson);
			}
		}

		// Handles the SelectionChanged event.
		private void CVPersonGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (CVPersonGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.CVPerson);
			}
			CVPersonGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void CVPersonGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (CVPersonGrid.LJCGetMouseRowIndex(e) > -1)
			{
				mCVPersonGridCode.DoDefaultCVPerson();
			}
		}
		#endregion
	}
}
