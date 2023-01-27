// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitSystemComboCode.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCUnitMeasure
{
	// The combo code.
	internal class UnitSystemComboCode
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitSystemComboCode(UnitMeasureManagers managers
			, UnitMeasureList parent = null)
		{
			// Allows LoadCombo() to be called without specifying the list parent
			// such as when called from a Detail dialgo.
			if (parent != null)
			{
				mParent = parent;
				UnitSystemCombo = mParent.SystemCombo;
			}
			mManagers = managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the combo items.
		internal void DataRetrieve()
		{
			if (mParent != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				LoadCombo();
				mParent.Cursor = Cursors.Default;
			}
		}

		// Loads the combo items.
		internal void LoadCombo()
		{
			UnitSystemCombo.Items.Clear();

			var unitSystemManager = mManagers.UnitSystemManager;
			var dataRecords = unitSystemManager.Load();

			if (dataRecords != null && dataRecords.Count > 0)
			{
				foreach (UnitSystem dataRecord in dataRecords)
				{
					UnitSystemCombo.LJCAddItem(dataRecord.ID, dataRecord.Name);
				}
				if (UnitSystemCombo.Items.Count > 0)
				{
					UnitSystemCombo.SelectedIndex = 0;
				}
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			UnitSystemDetail detail;

			if (UnitSystemCombo.SelectedIndex >= 0)
			{
				detail = new UnitSystemDetail(mManagers)
				{
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail,
				};
				detail.LJCChange += UnitSystemDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			UnitSystemDetail detail;

			if (UnitSystemCombo.SelectedIndex >= 0)
			{
				// Data from items.
				int id = UnitSystemCombo.LJCSelectedItemID();

				detail = new UnitSystemDetail(mManagers)
				{
					LJCID = id,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail,
				};
				detail.LJCChange += UnitSystemDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates row with changes from the detail dialog.
		private void UnitSystemDetail_Change(object sender, EventArgs e)
		{
			UnitSystemDetail detail;
			UnitSystem dataRecord;

			detail = sender as UnitSystemDetail;
			if (detail.LJCRecord != null)
			{
				dataRecord = detail.LJCRecord;
				if (detail.LJCIsUpdate)
				{
					if (UnitSystemCombo.SelectedItem is LJCItem ljcItem)
					{
						ljcItem.Text = dataRecord.Name;
						DoRefresh();
					}
				}
				else
				{
					UnitSystemCombo.LJCAddItem(dataRecord.ID, dataRecord.Name);
					UnitSystemCombo.LJCSetByItemID(dataRecord.ID);
				}
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;

			if (UnitSystemCombo.SelectedIndex >= 0)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = UnitSystemCombo.LJCSelectedItemID();

					var keyColumns = new DbColumns()
					{
						{ UnitSystem.ColumnID, id }
					};
					mManagers.UnitSystemManager.Delete(keyColumns);
					if (0 == mManagers.UnitSystemManager.AffectedCount)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						DoRefresh();
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			int id = 0;

			mParent.Cursor = Cursors.WaitCursor;
			if (UnitSystemCombo.SelectedIndex >= 0)
			{
				id = UnitSystemCombo.LJCSelectedItemID();
			}
			LoadCombo();

			// Select the original row.
			if (id > 0)
			{
				UnitSystemCombo.LJCSetByItemID(id);
			}
			mParent.Cursor = Cursors.Default;
		}
		#endregion

		#region Properties

		// The parent form combo.
		internal LJCItemCombo UnitSystemCombo { get; set; }
		#endregion

		#region Class Data

		private readonly UnitMeasureList mParent;
		private readonly UnitMeasureManagers mManagers;
		#endregion
	}
}
