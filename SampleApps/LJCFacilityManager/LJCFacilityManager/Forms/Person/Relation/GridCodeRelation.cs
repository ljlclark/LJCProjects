// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GridCodeRelation.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class GridCodeRelation
	{
		#region Constructors

		// Initializes an object instance.
		internal GridCodeRelation(PersonModule parent)
		{
			mParent = parent;
			mRelationGrid = mParent.RelationGrid;
			mPersonGrid = mParent.PersonGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveRelation()
		{
			PersonRelations records;

			mRelationGrid.LJCRowsClear();
			if (mRelationGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ PersonRelation.ColumnPersonID, parentID }
				};
				var personRelationManager = mManagers.PersonRelationManager;
				DbJoins dbJoins = personRelationManager.GetLoadJoins();
				records = personRelationManager.Load(keyColumns, joins: dbJoins);

				if (NetCommon.HasItems(records))
				{
					foreach (PersonRelation record in records)
					{
						RowAddRelation(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(PersonModule.Change.Relation);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddRelation(PersonRelation dataRecord)
		{
			LJCGridRow retValue;

			retValue = mRelationGrid.LJCRowAdd();
			SetStoredValuesRelation(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mRelationGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateRelation(PersonRelation dataRecord)
		{
			if (mRelationGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesRelation(row, dataRecord);
				row.LJCSetValues(mRelationGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesRelation(LJCGridRow row, PersonRelation dataRecord)
		{
			row.LJCSetInt32(PersonRelation.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectRelation(PersonRelation dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mRelationGrid.Rows)
				{
					rowID = row.LJCGetInt32(PersonRelation.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mRelationGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				mParent.Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoNewRelation()
		{
			RelationDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				detail = new RelationDetail()
				{
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "PersonDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(RelationDetail_Change);
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditRelation()
		{
			RelationDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow
				&& mRelationGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(PersonRelation.ColumnID);
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				detail = new RelationDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "PersonDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(RelationDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RelationDetail_Change(object sender, EventArgs e)
		{
			RelationDetail detail;
			PersonRelation record;
			LJCGridRow row;

			detail = sender as RelationDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateRelation(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddRelation(record);
				mRelationGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(PersonModule.Change.Relation);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDeleteRelation()
		{
			string title;
			string message;

			if (mRelationGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ PersonRelation.ColumnID
							, row.LJCGetInt32(PersonRelation.ColumnID) }
					};
					mManagers.PersonRelationManager.Delete(keyColumns);
					if (mManagers.PersonRelationManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mRelationGrid.Rows.Remove(row);
						mParent.TimedChange(PersonModule.Change.Relation);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshRelation()
		{
			PersonRelation record;
			int id = 0;

			if (mRelationGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Person.ColumnID);
			}
			DataRetrieveRelation();

			// Select the original row.
			if (id > 0)
			{
				record = new PersonRelation()
				{
					ID = id
				};
				RowSelectRelation(record);
			}
		}
		#endregion

		#region Class Data

		private readonly PersonModule mParent;
		private readonly LJCDataGrid mRelationGrid;
		private readonly LJCDataGrid mPersonGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
