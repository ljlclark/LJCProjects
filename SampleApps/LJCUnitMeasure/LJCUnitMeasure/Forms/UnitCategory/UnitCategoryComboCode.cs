// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitCategoryComboCode.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCUnitMeasure
{
	// The combo code.
	internal class UnitCategoryComboCode
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitCategoryComboCode(UnitMeasureManagers managers
			, UnitMeasureList parent = null)
		{
			// Allows LoadCombo() to be called without specifying the list parent
			// such as when called from a Detail dialgo.
			if (parent != null)
			{
				mParent = parent;
				UnitCategoryCombo = mParent.CategoryCombo;
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
			UnitCategoryCombo.Items.Clear();

			var unitCategoryManager = mManagers.UnitCategoryManager;
			var dataRecords = unitCategoryManager.Load();

			if (dataRecords != null && dataRecords.Count > 0)
			{
				foreach (UnitCategory dataRecord in dataRecords)
				{
					UnitCategoryCombo.LJCAddItem(dataRecord.ID, dataRecord.Name);
				}
				if (UnitCategoryCombo.Items.Count > 0)
				{
					UnitCategoryCombo.SelectedIndex = 0;
				}
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			UnitCategoryDetail detail;

			if (UnitCategoryCombo.SelectedIndex >= 0)
			{
				detail = new UnitCategoryDetail(mManagers)
				{
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail,
				};
				detail.LJCChange += UnitCategoryDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			UnitCategoryDetail detail;

			if (UnitCategoryCombo.SelectedIndex >= 0)
			{
				// Data from items.
				int id = UnitCategoryCombo.LJCSelectedItemID();

				detail = new UnitCategoryDetail(mManagers)
				{
					LJCID = id,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail,
				};
				detail.LJCChange += UnitCategoryDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates row with changes from the detail dialog.
		private void UnitCategoryDetail_Change(object sender, EventArgs e)
		{
			UnitCategoryDetail detail;
			UnitCategory dataRecord;

			detail = sender as UnitCategoryDetail;
			if (detail.LJCRecord != null)
			{
				dataRecord = detail.LJCRecord;
				if (detail.LJCIsUpdate)
				{
					if (UnitCategoryCombo.SelectedItem is LJCItem ljcItem)
					{
						ljcItem.Text = dataRecord.Name;
						DoRefresh();
					}
				}
				else
				{
					UnitCategoryCombo.LJCAddItem(dataRecord.ID, dataRecord.Name);
					UnitCategoryCombo.LJCSetByItemID(dataRecord.ID);
				}
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;

			if (UnitCategoryCombo.SelectedIndex >= 0)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = UnitCategoryCombo.LJCSelectedItemID();

					var keyColumns = new DbColumns()
					{
						{ UnitCategory.ColumnID, id }
					};
					mManagers.UnitCategoryManager.Delete(keyColumns);
					if (0 == mManagers.UnitCategoryManager.AffectedCount)
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
			if (UnitCategoryCombo.SelectedIndex >= 0)
			{
				id = UnitCategoryCombo.LJCSelectedItemID();
			}
			LoadCombo();

			// Select the original row.
			if (id > 0)
			{
				UnitCategoryCombo.LJCSetByItemID(id);
			}
			mParent.Cursor = Cursors.Default;
		}
		#endregion

		#region Properties

		// The parent form combo.
		internal LJCItemCombo UnitCategoryCombo { get; set; }
		#endregion

		#region Class Data

		private readonly UnitMeasureList mParent;
		private readonly UnitMeasureManagers mManagers;
		#endregion
	}
}
