// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableGridCode.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using DataDetail;
using LJCSQLUtilLibDAL;

namespace DataHelper
{
	// Contains the Grid methods.
	internal class TableGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal TableGridCode(MainList parent)
		{
			// Set default class data.
			mParent = parent;
			mManagers = mParent.Managers;
      // ToDo: Convert to new DataDetailDialog.
      //mConfigRows = ControlRows.Deserialize(ConfigRowFileName);
			mDataConfigName = mManagers.DbMetaDataTableManager.DataConfigName;

      mUserID = "Les";
      mTableName = "ColumnGrid";
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieveTable()
		{
			DbMetaDataTables records;

			mParent.Cursor = Cursors.WaitCursor;
			mParent.TableGrid.LJCRowsClear();

			var dataTableManager = mManagers.DbMetaDataTableManager;
			records = dataTableManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (DbMetaDataTable record in records)
				{
					RowAddTable(record);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(MainList.ChangeTable);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddTable(DbMetaDataTable record)
		{
			LJCGridRow retValue;

			retValue = mParent.TableGrid.LJCRowAdd();
			SetStoredValuesTable(retValue, record);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mParent.TableGrid, record);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateTable(DbMetaDataTable record)
		{
			if (mParent.TableGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesTable(gridRow, record);
				gridRow.LJCSetValues(mParent.TableGrid, record);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesTable(LJCGridRow row, DbMetaDataTable record)
		{
			row.LJCSetInt32(DbMetaDataTable.ColumnID, record.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectTable(DbMetaDataTable record)
		{
			int rowID;
			bool retValue = false;

			if (record != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mParent.TableGrid.Rows)
				{
					rowID = row.LJCGetInt32(DbMetaDataTable.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mParent.TableGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewTable()
		{
			DbColumns dataColumns
				= mManagers.DbMetaDataTableManager.DataDefinition;

      // ToDo: Convert to new DataDetailDialog.
      DataDetailDialog detail = new DataDetailDialog(mUserID, mDataConfigName
				, mTableName)
			{
				//LJCConfigRows = mConfigRows,
				LJCDataColumns = dataColumns,
				LJCKeyItems = new KeyItems(),  // 7/23
				LJCIsUpdate = false
			};
			detail.LJCChange += TableDetail_Change;
			if (DialogResult.OK == detail.ShowDialog())
			{
				//mConfigRows = detail.LJCConfigRows;
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditTable()
		{
			if (mParent.TableGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int id = row.LJCGetInt32(DbMetaDataTable.ColumnID);

				// Retrieve Data
				var keyColumns = new DbColumns()
				{
					{ DbMetaDataTable.ColumnID, id }
				};
				DataManager dataTableManager
					= mManagers.DbMetaDataTableManager.DataManager;
				DbResult dbResult = dataTableManager.Retrieve(keyColumns);
				if (DbResult.HasData(dbResult))
				{
					// The Data Definition and Record values are merged. 
					DbColumns dataColumns = dbResult.GetValueColumns();

          // ToDo: Convert to new DataDetailDialog.
          DataDetailDialog detail = new DataDetailDialog(mUserID, mDataConfigName
						, mTableName)
					{
						//LJCConfigRows = mConfigRows,
						LJCDataColumns = dataColumns,
						LJCIsUpdate = true
					};
					detail.LJCChange += TableDetail_Change;
					if (DialogResult.OK == detail.ShowDialog())
					{
						//mConfigRows = detail.LJCConfigRows;
					}
				}
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void TableDetail_Change(object sender, EventArgs e)
		{
			DataDetailDialog detail;
			DbColumns dataColumns;

			detail = sender as DataDetailDialog;
			dataColumns = detail.LJCDataColumns;
			ResultConverter<DbMetaDataTable, DbMetaDataTables> resultConverter
				= new ResultConverter<DbMetaDataTable, DbMetaDataTables>();
			DbMetaDataTable mdTable = resultConverter.CreateData(dataColumns);

			var dataTableManager = mManagers.DbMetaDataTableManager;
			if (detail.LJCIsUpdate)
			{
				var keyColumns = new DbColumns()
				{
					{ DbMetaDataTable.ColumnID, mdTable.ID }
				};
				dataTableManager.Update(mdTable, keyColumns);
				if (dataTableManager.AffectedCount > 0)
				{
					RowUpdateTable(mdTable);
				}
			}
			else
			{
				dataTableManager.Add(mdTable);
				if (dataTableManager.AffectedCount > 0)
				{
					LJCGridRow row = RowAddTable(mdTable);
					mParent.ColumnGrid.LJCSetCurrentRow(row, true);
					mParent.ChangeTimer.DoChange(MainList.ChangeTable);
				}
			}
		}

		// Deletes the selected row.
		internal void DoDeleteTable()
		{
			string title;
			string message;

			if (mParent.TableGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = row.LJCGetInt32(DbMetaDataTable.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ DbMetaDataTable.ColumnID, id }
					};
					var dataTableManager = mManagers.DbMetaDataTableManager;
					dataTableManager.Delete(keyColumns);
					if (dataTableManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mParent.TableGrid.Rows.Remove(row);
						mParent.ChangeTimer.DoChange(MainList.ChangeTable);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshTable()
		{
			DbMetaDataTable record;
			int id = 0;

			if (mParent.TableGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(DbMetaDataTable.ColumnID);
			}
			DataRetrieveTable();

			// Select the original row.
			if (id > 0)
			{
				record = new DbMetaDataTable()
				{
					ID = id
				};
				RowSelectTable(record);
			}
		}

		// Performs the Close function.
		internal void DoCloseTable()
		{
      //ToDo: Convert to new DataDetailDAL.
      //   if (mConfigRows != null)
      //{
      //	//RowOrderComparer comparer = new RowOrderComparer();
      //	//mConfigRows.SortRowOrder(comparer);
      //  ControlRowUniqueComparer comparer = new ControlRowUniqueComparer();
      //  mConfigRows.LJCSortUnique(comparer);
      //	mConfigRows.Serialize(ConfigRowFileName);
      //}
    }
    #endregion

    #region Class Data

    //private readonly string ConfigRowFileName = @"DetailConfigs/TableDetailConfig.xml";
    //private ControlRows mConfigRows;
    private readonly string mDataConfigName;
		private readonly SQLUtilLibManagers mManagers;
		private readonly MainList mParent;

    private readonly string mUserID;
    private readonly string mTableName;
    #endregion
  }
}
