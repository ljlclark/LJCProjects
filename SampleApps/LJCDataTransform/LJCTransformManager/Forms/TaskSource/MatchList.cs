// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MatchList.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	/// <summary>The list form.</summary>
	internal partial class MatchList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal MatchList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void MatchList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Data Methods

		#region Source

		// Retrieves the list rows.
		private void DataRetrieveSource()
		{
			TaskTransform taskTransform;
			DataSource dataSource;
			LayoutColumns records;

			Cursor = Cursors.WaitCursor;
			SourceGrid.LJCRowsClear();

			var taskTransformManager = Managers.TaskTransformManager;
			taskTransform = taskTransformManager.RetrieveWithID(LJCParentID);
			if (taskTransform != null)
			{
				mSourceDataID = taskTransform.SourceDataID;
				SetSourceHeading(mSourceDataID);
				var dataSourceManager = Managers.DataSourceManager;
				dataSource = dataSourceManager.RetrieveWithID(mSourceDataID);
				if (dataSource != null)
				{
					var keyColumns = new DbColumns()
					{
						{ LayoutColumn.ColumnSourceLayoutID, dataSource.SourceLayoutID }
					};
					var layoutColumnManager = Managers.LayoutColumnManager;
					records = layoutColumnManager.Load(keyColumns);
					if (NetCommon.HasItems(records))
					{
						foreach (LayoutColumn record in records)
						{
							RowAddSource(record);
						}
					}
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Source);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddSource(LayoutColumn dataRecord)
		{
			LJCGridRow retValue;

			retValue = SourceGrid.LJCRowAdd();
			SetStoredValuesSource(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(SourceGrid, dataRecord);
			return retValue;
		}

		// Sets the row stored values.
		private void SetStoredValuesSource(LJCGridRow row
			, LayoutColumn dataRecord)
		{
			row.LJCSetInt32(LayoutColumn.ColumnLayoutColumnID, dataRecord.LayoutColumnID);
		}
		#endregion

		#region Target

		// Retrieves the list rows.
		private void DataRetrieveTarget()
		{
			TaskTransform taskTransform;
			LayoutColumns records;
			DataSource dataSource;

			Cursor = Cursors.WaitCursor;
			TargetGrid.LJCRowsClear();

			var taskTransformManager = Managers.TaskTransformManager;
			taskTransform = taskTransformManager.RetrieveWithID(LJCParentID);
			if (taskTransform != null)
			{
				mTargetDataID = taskTransform.TargetDataID;
				SetTargetHeading(mTargetDataID);
				var dataSourceManager = Managers.DataSourceManager;
				dataSource = dataSourceManager.RetrieveWithID(mTargetDataID);
				if (dataSource != null)
				{
					var keyColumns = new DbColumns()
					{
						{ LayoutColumn.ColumnSourceLayoutID, dataSource.SourceLayoutID }
					};
					var layoutColumnManager = Managers.LayoutColumnManager;
					records = layoutColumnManager.Load(keyColumns);
					if (NetCommon.HasItems(records))
					{
						foreach (LayoutColumn record in records)
						{
							RowAddTarget(record);
						}
					}
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Target);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddTarget(LayoutColumn dataRecord)
		{
			LJCGridRow retValue;

			retValue = TargetGrid.LJCRowAdd();
			SetStoredValuesTarget(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(TargetGrid, dataRecord);
			return retValue;
		}

		// Sets the row stored values.
		private void SetStoredValuesTarget(LJCGridRow row
			, LayoutColumn dataRecord)
		{
			row.LJCSetInt32(LayoutColumn.ColumnLayoutColumnID, dataRecord.LayoutColumnID);
		}
		#endregion

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			TransformMatch lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;

			foreach (TransformMatch changeMatch in mChangeMatches)
			{
				lookupRecord = GetDbMatch(changeMatch.TransformID, changeMatch.SourceColumnID
					, changeMatch.TargetColumnID);

				switch (changeMatch.ChangeStatus)
				{
					case ChangeStatus.Add:
						if (lookupRecord != null)
						{
							title = "Data Entry Error";
							message = "The record already exists.";
							Cursor = Cursors.Default;
							MessageBox.Show(message, title, MessageBoxButtons.OK
								, MessageBoxIcon.Exclamation);
						}
						else
						{
							Managers.TransformMatchManager.Add(changeMatch);
						}
						break;

					case ChangeStatus.Change:
						if (null == lookupRecord)
						{
							title = "Data Entry Error";
							message = "The update record does not exist.";
							Cursor = Cursors.Default;
							MessageBox.Show(message, title, MessageBoxButtons.OK
								, MessageBoxIcon.Exclamation);
						}
						else
						{
							var keyColumns = new DbColumns()
							{
								{ TransformMatch.ColumnTransformID, changeMatch.TransformID },
								{ TransformMatch.ColumnSourceColumnID, changeMatch.SourceColumnID },
								{ TransformMatch.ColumnTargetColumnID, changeMatch.TargetColumnID }
							};
							Managers.TransformMatchManager.Update(changeMatch, keyColumns);
						}
						break;

					case ChangeStatus.Delete:
						if (null == lookupRecord)
						{
							title = "Delete Error";
							message = "The delete record does not exist.";
							Cursor = Cursors.Default;
							MessageBox.Show(message, title, MessageBoxButtons.OK
								, MessageBoxIcon.Exclamation);
						}
						else
						{
							var keyColumns = new DbColumns()
							{
								{ TransformMatch.ColumnTransformID, changeMatch.TransformID },
								{ TransformMatch.ColumnSourceColumnID, changeMatch.SourceColumnID },
								{ TransformMatch.ColumnTargetColumnID, changeMatch.TargetColumnID }
							};
							Managers.TransformMatchManager.Delete(keyColumns);
						}
						break;
				}
			}
			Cursor = Cursors.Default;
			return retValue;
		}

		#region Source Heading methods.

		// Set the SourceHeader text.
		private void SetSourceHeading(int dataSourceID)
		{
			string sourceTypeName;
			string itemName;

			DataSource dataSource
				= Managers.DataSourceManager.RetrieveWithID(dataSourceID);
			if (dataSource != null)
			{
				itemName = GetItemName(dataSource.SourceItemName);
				sourceTypeName = GetSourceTypeName(dataSource.SourceTypeID);
				SourceHeader.Text = $"Source Layout {sourceTypeName} - {itemName}";
			}
		}

		// Set the TargetHeader text.
		private void SetTargetHeading(int dataSourceID)
		{
			string sourceTypeName;
			string itemName;

			DataSource dataSource
				= Managers.DataSourceManager.RetrieveWithID(dataSourceID);
			if (dataSource != null)
			{
				itemName = GetItemName(dataSource.SourceItemName);
				sourceTypeName = GetSourceTypeName(dataSource.SourceTypeID);
				TargetHeader.Text = $"Target Layout {sourceTypeName} - {itemName}";
			}
		}

		// Gets the Item Name from the full path.
		private string GetItemName(string sourceItemName)
		{
			string retValue = sourceItemName;

			int index = retValue.LastIndexOf('\\');
			if (index > -1)
			{
				retValue = retValue.Substring(index + 1);
			}
			return retValue;
		}

		// Gets the SourceType name.
		private string GetSourceTypeName(short sourceTypeID)
		{
			string retValue = null;

			SourceType sourceType
				= Managers.SourceTypeManager.RetrieveWithID(sourceTypeID);
			if (sourceType != null)
			{
				retValue = sourceType.Name;
			}
			return retValue;
		}
		#endregion
		#endregion

		#region Row Action Methods

		// Selects the current Source row based on the matched TargetRow.
		// Execute with Click on SourceGrid item.
		private void SelectSourceRowWithTarget(LJCGridRow targetRow)
		{
			LJCGridRow sourceRow = GetSourceRowWithTarget(targetRow);
			if (sourceRow != null)
			{
				SourceGrid.LJCSetCurrentRow(sourceRow, false);
			}
		}

		// Selects the current Target row based on the matched SourceRow.
		// Execute with Click on TargetGrid item.
		private void SelectTargetRowWithSource(LJCGridRow sourceRow)
		{
			LJCGridRow targetRow = GetTargetRowWithSource(sourceRow);
			if (targetRow != null)
			{
				TargetGrid.LJCSetCurrentRow(targetRow, false);
			}
		}

		// Sets match values using the Source row.
		// Executed with Double-Click on SourceGrid item or Context Menu.
		private void SetMatchChangesWithSource(LJCGridRow sourceRow)
		{
			bool isAvailable = true;

			if (sourceRow != null)
			{
				// Source row is not selected.
				if (!IsRowSelected(sourceRow))
				{
					// Checks for ChangeMatch or TransformMatch.
					if (IsSourceRowMapped(sourceRow))
					{
						isAvailable = false;
					}
				}

				if (isAvailable)
				{
					LJCGridRow targetRow = GetTargetRowWithSource(sourceRow);

					// New match so use current Target row.
					if (null == targetRow)
					{
						targetRow = TargetGrid.CurrentRow as LJCGridRow;
					}

					if (IsRowSelected(sourceRow))
					{
						isAvailable = SetChangeMatchDelete(sourceRow, targetRow);
					}
					else
					{
						isAvailable = SetChangeMatchAdd(sourceRow, targetRow);
					}
					if (isAvailable)
					{
						ToggleRowColors(sourceRow, targetRow);
					}
				}
			}
		}

		// Sets match values using the Target row.
		// Executed with Double-Click on TargeGrid item or Context Menu.
		private void SetMatchChangesWithTarget(LJCGridRow targetRow)
		{
			bool isAvailable = true;

			if (targetRow != null)
			{
				// Target row is not selected.
				if (!IsRowSelected(targetRow))
				{
					// Checks for ChangeMatch or TransformMatch.
					if (IsTargetRowMapped(targetRow))
					{
						isAvailable = false;
					}
				}

				if (isAvailable)
				{
					LJCGridRow sourceRow = GetSourceRowWithTarget(targetRow);

					// New match so use current Source row.
					if (null == sourceRow)
					{
						sourceRow = SourceGrid.CurrentRow as LJCGridRow;
					}

					if (IsRowSelected(targetRow))
					{
						isAvailable = SetChangeMatchDelete(sourceRow, targetRow);
					}
					else
					{
						isAvailable = SetChangeMatchAdd(sourceRow, targetRow);
					}
					if (isAvailable)
					{
						ToggleRowColors(sourceRow, targetRow);
					}
				}
			}
		}

		// Performs a ChangeMatch "Add".
		private bool SetChangeMatchAdd(LJCGridRow sourceRow, LJCGridRow targetRow)
		{
			TransformMatch transformMatch = null;
			TransformMatch changeMatch;
			int sourceColumnID = 0;
			int targetColumnID = 0;
			string message;
			bool retValue = true;

			if (IsRowSelected(sourceRow)
				|| IsRowSelected(targetRow))
			{
				retValue = false;
				message = "Source or Target is already selected.";
				MessageBox.Show(message, "Invalid TransformMatch", MessageBoxButtons.OK
					, MessageBoxIcon.Information);
			}

			if (retValue)
			{
				// Get TransformMatch.
				sourceColumnID = GetRowLayoutColumnID(sourceRow);
				targetColumnID = GetRowLayoutColumnID(targetRow);
				transformMatch = GetDbMatch(LJCParentID, sourceColumnID, targetColumnID);
				if (null == transformMatch)
				{
					// Potential new match so create it.
					transformMatch = new TransformMatch()
					{
						TransformID = LJCParentID,
						SourceColumnID = (short)sourceColumnID,
						TargetColumnID = (short)targetColumnID
					};

					// If not already used in another TransformMatch.
					if (!IsAvailableSourceDbMatch(LJCParentID, transformMatch.SourceColumnID)
						&& !IsAvailableTargetDbMatch(LJCParentID, transformMatch.TargetColumnID))
					{
						retValue = false;
						message = "Source or Target is already part of another "
							+ "TransformMatch.";
						MessageBox.Show(message, "Invalid TransformMatch", MessageBoxButtons.OK
							, MessageBoxIcon.Information);
					}
				}
			}

			if (retValue)
			{
				// Get match from Change collection.
				changeMatch = GetChangeMatch(sourceColumnID, targetColumnID);
				if (changeMatch != null)
				{
					// Remove the "Delete" for item that is being added.
					if (changeMatch.ChangeStatus == ChangeStatus.Delete)
					{
						mChangeMatches.Remove(changeMatch);
					}
				}
				else
				{
					// If not already used in another Change Match.
					retValue = false;
					if (IsAvailableSourceChangeMatch(sourceColumnID)
						&& IsAvailableTargetChangeMatch(targetColumnID))
					{
						retValue = true;

						// Add an "Add" for item that is being added.
						transformMatch.ChangeStatus = ChangeStatus.Add;
						mChangeMatches.Add(transformMatch);
					}
				}
			}
			return retValue;
		}

		// Performs a ChangeMatch "Delete".
		private bool SetChangeMatchDelete(LJCGridRow sourceRow, LJCGridRow targetRow)
		{
			TransformMatch transformMatch = null;
			TransformMatch changeMatch;
			int sourceColumnID = 0;
			int targetColumnID = 0;
			string message;
			bool retValue = true;

			if (!IsRowSelected(sourceRow)
				|| !IsRowSelected(targetRow))
			{
				retValue = false;
				message = "Source or Target is not selected.";
				MessageBox.Show(message, "Invalid TransformMatch", MessageBoxButtons.OK
					, MessageBoxIcon.Information);
			}

			if (retValue)
			{
				// Get TransformMatch.
				sourceColumnID = GetRowLayoutColumnID(sourceRow);
				targetColumnID = GetRowLayoutColumnID(targetRow);
				transformMatch = GetDbMatch(LJCParentID, sourceColumnID, targetColumnID);
			}

			if (retValue)
			{
				changeMatch = GetChangeMatch(sourceColumnID, targetColumnID);
				if (changeMatch != null)
				{
					// Remove the "Add" for item that is being deleted.
					if (changeMatch.ChangeStatus == ChangeStatus.Add)
					{
						mChangeMatches.Remove(changeMatch);
					}
				}
				else
				{
					// If not already used in another Change Match.
					retValue = false;
					if (IsAvailableSourceChangeMatch(sourceColumnID)
						&& IsAvailableTargetChangeMatch(targetColumnID))
					{
						retValue = true;

						// Add a "Delete" for item that is being deleted.
						transformMatch.ChangeStatus = ChangeStatus.Delete;
						mChangeMatches.Add(transformMatch);
					}
				}
			}
			return retValue;
		}
		#endregion

		#region Row Helper Methods

		// Get the matched Source row using the Target row.
		private LJCGridRow GetSourceRowWithTarget(LJCGridRow targetRow)
		{
			TransformMatch changeMatch;
			TransformMatch transformMatch;
			LJCGridRow retValue = null;

			if (IsRowSelected(targetRow))
			{
				// Check ChangeMatch first.
				changeMatch = GetTargetRowChangeMatch(targetRow);
				if (changeMatch != null
					&& changeMatch.TargetColumnID > 0)
				{
					if (changeMatch.ChangeStatus != ChangeStatus.Delete)
					{
						retValue = FindLayoutGridRow(SourceGrid, changeMatch.SourceColumnID);
					}
				}
				else
				{
					// No ChangeMatch so check TransformMatch.
					transformMatch = GetTargetRowDbMatch(LJCParentID, targetRow);
					if (transformMatch != null
						&& transformMatch.TargetColumnID > 0)
					{
						retValue = FindLayoutGridRow(SourceGrid, transformMatch.SourceColumnID);
					}
				}
			}
			return retValue;
		}

		// Get the matched Target row using the Source row.
		private LJCGridRow GetTargetRowWithSource(LJCGridRow sourceRow)
		{
			TransformMatch changeMatch;
			TransformMatch transformMatch;
			LJCGridRow retValue = null;

			if (IsRowSelected(sourceRow))
			{
				// Check ChangeMatch first.
				changeMatch = GetSourceRowChangeMatch(sourceRow);
				if (changeMatch != null
					&& changeMatch.TargetColumnID > 0)
				{
					if (changeMatch.ChangeStatus != ChangeStatus.Delete)
					{
						retValue = FindLayoutGridRow(TargetGrid, changeMatch.TargetColumnID);
					}
				}
				else
				{
					// No ChangeMatch so check TransformMatch.
					transformMatch = GetSourceRowDbMatch(LJCParentID, sourceRow);
					if (transformMatch != null
						&& transformMatch.TargetColumnID > 0)
					{
						retValue = FindLayoutGridRow(TargetGrid, transformMatch.TargetColumnID);
					}
				}
			}
			return retValue;
		}

		// Get the Source row ChangeMatch.
		private TransformMatch GetSourceRowChangeMatch(LJCGridRow sourceRow)
		{
			TransformMatch retValue = null;

			if (sourceRow != null)
			{
				int layoutColumnID = GetRowLayoutColumnID(sourceRow);
				if (layoutColumnID > 0)
				{
					retValue = GetSourceChangeMatch(layoutColumnID);
				}
			}
			return retValue;
		}

		// Get the Target row ChangeMatch.
		private TransformMatch GetTargetRowChangeMatch(LJCGridRow targetRow)
		{
			TransformMatch retValue = null;

			if (targetRow != null)
			{
				int layoutColumnID = GetRowLayoutColumnID(targetRow);
				if (layoutColumnID > 0)
				{
					retValue = GetTargetChangeMatch(layoutColumnID);
				}
			}
			return retValue;
		}

		// Get the Source row match.
		private TransformMatch GetSourceRowDbMatch(int transformID, LJCGridRow sourceRow)
		{
			TransformMatch retValue = null;

			int layoutColumnID = GetRowLayoutColumnID(sourceRow);
			if (layoutColumnID > 0)
			{
				retValue = GetSourceDbMatch(transformID, (short)layoutColumnID);
			}
			return retValue;
		}

		// Get the Target row match.
		private TransformMatch GetTargetRowDbMatch(int transformID, LJCGridRow targetRow)
		{
			TransformMatch retValue = null;

			int layoutColumnID = GetRowLayoutColumnID(targetRow);
			if (layoutColumnID > 0)
			{
				retValue = GetTargetDbMatch(transformID, layoutColumnID);
			}
			return retValue;
		}

		// Determines if the Source row has ChangeMatch or TransformMatch.
		private bool IsSourceRowMapped(LJCGridRow sourceRow)
		{
			TransformMatch changeMatch;
			TransformMatch dbMatch;
			bool retValue = false;

			if (sourceRow != null)
			{
				// Check ChangeMatch first.
				changeMatch = GetSourceRowChangeMatch(sourceRow);
				if (changeMatch != null)
				{
					switch (changeMatch.ChangeStatus)
					{
						case ChangeStatus.Delete:
							retValue = false;
							break;

						case ChangeStatus.Add:
							retValue = true;
							break;
					}
				}
				else
				{
					// No ChangeMatch so check TransformMatch.
					dbMatch = GetSourceRowDbMatch(LJCParentID, sourceRow);
					if (dbMatch != null)
					{
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// Determines if the Target row hast ChangeMatch or TransformMatch.
		private bool IsTargetRowMapped(LJCGridRow targetRow)
		{
			TransformMatch changeMatch;
			TransformMatch transformMatch;
			bool retValue = false;

			if (targetRow != null)
			{
				// Check ChangeMatch first.
				changeMatch = GetTargetRowChangeMatch(targetRow);
				if (changeMatch != null)
				{
					switch (changeMatch.ChangeStatus)
					{
						case ChangeStatus.Delete:
							retValue = false;
							break;

						case ChangeStatus.Add:
							retValue = true;
							break;
					}
				}
				else
				{
					// No ChangeMatch so check TransformMatch.
					transformMatch = GetTargetRowDbMatch(LJCParentID, targetRow);
					if (transformMatch != null)
					{
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// Gets the row LayoutColumnID value.
		private int GetRowLayoutColumnID(LJCGridRow row)
		{
			int retValue = 0;

			if (row != null)
			{
				retValue = row.LJCGetInt32(LayoutColumn.ColumnLayoutColumnID);
			}
			return retValue;
		}

		// Get the LayoutGrid row by LayoutColumn ID.
		private LJCGridRow FindLayoutGridRow(LJCDataGrid grid, int layoutColumnID)
		{
			LJCGridRow retValue = null;

			if (grid != null && layoutColumnID > 0)
			{
				foreach (LJCGridRow row in grid.Rows)
				{
					int rowColumnID = GetRowLayoutColumnID(row);
					if (rowColumnID == layoutColumnID)
					{
						retValue = row;
						break;
					}
				}
			}
			return retValue;
		}
		#endregion

		#region ColumnID Methods

		// Gets the Source ChangeMatch.
		private TransformMatch GetSourceChangeMatch(int sourceColumnID)
		{
			TransformMatch retValue;

			retValue = mChangeMatches.LJCSearchSourceID(LJCParentID
				, sourceColumnID);
			return retValue;
		}

		// Gets the Target ChangeMatch.
		private TransformMatch GetTargetChangeMatch(int targetColumnID)
		{
			TransformMatch retValue;

			retValue = mChangeMatches.LJCSearchTargetID(LJCParentID
				, targetColumnID);
			return retValue;
		}

		// Gets the ChangeMatch.
		private TransformMatch GetChangeMatch(int sourceColumnID, int targetColumnID)
		{
			TransformMatch retValue;

			retValue = mChangeMatches.LJCSearchColumnIDs(LJCParentID
				, sourceColumnID, targetColumnID);
			return retValue;
		}

		// Is the Source not already used in the change match.
		private bool IsAvailableSourceChangeMatch(int sourceColumnID)
		{
			TransformMatch lookupMatch;
			bool retValue = true;

			lookupMatch = GetSourceChangeMatch(sourceColumnID);
			if (lookupMatch != null)
			{
				retValue = false;
			}
			return retValue;
		}

		// Is the Target not already used in the change match.
		private bool IsAvailableTargetChangeMatch(int targetColumnID)
		{
			TransformMatch lookupMatch;
			bool retValue = true;

			lookupMatch = GetTargetChangeMatch(targetColumnID);
			if (lookupMatch != null)
			{
				retValue = false;
			}
			return retValue;
		}

		// Is the Source not already used in the TransformMatch.
		private bool IsAvailableSourceDbMatch(int transformID, int sourceColumnID)
		{
			TransformMatch lookupMatch;
			bool retValue = true;

			TransformMatch changeMatch = GetSourceChangeMatch(sourceColumnID);
			if (null == changeMatch
				|| changeMatch.ChangeStatus != ChangeStatus.Delete)
			{
				lookupMatch = GetSourceDbMatch(transformID, (short)sourceColumnID);
				if (lookupMatch != null)
				{
					retValue = false;
				}
			}
			return retValue;
		}

		// Is the Target not already used in the TransformMatch.
		private bool IsAvailableTargetDbMatch(int transformID, int targetColumnID)
		{
			TransformMatch lookupMatch;
			bool retValue = true;

			TransformMatch changeMatch = GetTargetChangeMatch(targetColumnID);
			if (null == changeMatch
				|| changeMatch.ChangeStatus != ChangeStatus.Delete)
			{
				lookupMatch = GetTargetDbMatch(transformID, targetColumnID);
				if (lookupMatch != null)
				{
					retValue = false;
				}
			}
			return retValue;
		}
		#endregion

		#region Data Helper Methods

		// Gets the TransformMatch with Source ID.
		private TransformMatch GetSourceDbMatch(int transformID, int sourceColumnID)
		{
			TransformMatch retValue;

			var keyColumns = new DbColumns()
			{
				{ TransformMatch.ColumnTransformID, transformID },
				{ TransformMatch.ColumnSourceColumnID, sourceColumnID }
			};
			var transformMatchManager = Managers.TransformMatchManager;
			DbJoins dbJoins = transformMatchManager.GetLoadJoins();
			retValue = transformMatchManager.Retrieve(keyColumns, joins: dbJoins);
			return retValue;
		}

		// Gets the TransformMatch with Target ID.
		private TransformMatch GetTargetDbMatch(int transformID, int targetColumnID)
		{
			TransformMatch retValue;

			var keyColumns = new DbColumns()
			{
				{ TransformMatch.ColumnTransformID, transformID },
				{ TransformMatch.ColumnTargetColumnID, targetColumnID }
			};
			var transformMatchManager = Managers.TransformMatchManager;
			DbJoins dbJoins = transformMatchManager.GetLoadJoins();
			retValue = transformMatchManager.Retrieve(keyColumns, joins: dbJoins);
			return retValue;
		}

		// Gets the TransformMatch with Source ID and Target ID.
		private TransformMatch GetDbMatch(int transformID, int sourceColumnID
			, int targetColumnID)
		{
			TransformMatch retValue;

			var keyColumns = new DbColumns()
			{
				{ TransformMatch.ColumnTransformID, transformID },
				{ TransformMatch.ColumnSourceColumnID, sourceColumnID },
				{ TransformMatch.ColumnTargetColumnID, targetColumnID }
			};
			retValue = Managers.TransformMatchManager.Retrieve(keyColumns);
			return retValue;
		}
		#endregion

		#region Map Color Methods

		// Sets the color for the mapped columns.
		private void SelectMappedColumns()
		{
			TransformMatches transformMatches;
			LayoutColumn layoutColumn;

			transformMatches
				= Managers.TransformMatchManager.LoadWithTransformID(LJCParentID);
			if (transformMatches != null)
			{
				var layoutColumnManager = Managers.LayoutColumnManager;
				foreach (TransformMatch transformMatch in transformMatches)
				{
					layoutColumn = layoutColumnManager.RetrieveWithID(transformMatch.SourceColumnID);
					SelectMappedColumn(SourceGrid, layoutColumn.LayoutColumnID);
					layoutColumn = layoutColumnManager.RetrieveWithID(transformMatch.TargetColumnID);
					SelectMappedColumn(TargetGrid, layoutColumn.LayoutColumnID);
				}
			}
		}

		// Sets the color for the mapped column.
		private void SelectMappedColumn(LJCDataGrid grid, int layoutColumnID)
		{
			LJCGridRow row = FindLayoutGridRow(grid, layoutColumnID);
			if (row != null)
			{
				SetRowSelected(row, true);
			}
		}

		// Toggles the row colors.
		private void ToggleRowColors(LJCGridRow sourceRow, LJCGridRow targetRow)
		{
			if (sourceRow != null && targetRow != null)
			{
				if (IsRowSelected(sourceRow))
				{
					SetRowSelected(sourceRow, false);
					SetRowSelected(targetRow, false);
				}
				else
				{
					SetRowSelected(sourceRow);
					SetRowSelected(targetRow);
				}
			}
		}

		// Indicates if the specified row is marked as mapped.
		private bool IsRowSelected(LJCGridRow row)
		{
			bool retValue = false;

			if (row != null)
			{
				retValue = row.DefaultCellStyle.BackColor == mMappedColor;
			}
			return retValue;
		}

		// Adds or removes the Mapped color marking.
		private void SetRowSelected(LJCGridRow row, bool selected = true)
		{
			if (selected)
			{
				row.DefaultCellStyle.BackColor = mMappedColor;
			}
			else
			{
				row.DefaultCellStyle.BackColor = Color.White;
			}
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesTransformManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mMappedColor = values.SelectColor;
			mChangeMatches = new TransformMatches();

			Managers = new TransformManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\MatchList.xml";

			SetupGridSource();
			SetupGridTarget();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				// Modify MainSplit.Panel1 controls.
				//ListHelper.SetPanelControls(_ClassName_Split.Panel1, _ClassName_Heading
				//	, _ClassName_ToolPanel, _ClassName_Grid);

				// Modify MainSplit.Panel2 controls.
				//ListHelper.SetPanelControls(_ClassName_Split.Panel2, ChildHeading
				//	, ChildToolPanel, ChildGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGridSource()
		{
			SourceGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == SourceGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					LayoutColumn.ColumnName,
					LayoutColumn.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsSource
					= Managers.LayoutColumnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				SourceGrid.LJCAddColumns(mGridColumnsSource);
			}
		}
		private DbColumns mGridColumnsSource;

		// Setup the grid columns.
		private void SetupGridTarget()
		{
			TargetGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == TargetGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					LayoutColumn.ColumnName,
					LayoutColumn.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsTarget
					= Managers.LayoutColumnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				TargetGrid.LJCAddColumns(mGridColumnsTarget);
			}
		}
		private DbColumns mGridColumnsTarget;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			SourceGrid.LJCSaveColumnValues(controlValues);
			TargetGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("MainSplit.SplitterDistance", 0, 0, 0
				, MainSplit.SplitterDistance);

			// Save Window values.
			controlValues.Add(Name, Left, Top, Width, Height);

			// Save other values.

			NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName);
		}

		// Restores the control values.
		private void RestoreControlValues()
		{
			ControlValue controlValue;

			if (File.Exists(mControlValuesFileName))
			{
				ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
					, mControlValuesFileName) as ControlValues;
				if (ControlValues != null)
				{
					// Restore Window values.
					controlValue = ControlValues.LJCSearchName(Name);

					if (controlValue != null)
					{
						Left = controlValue.Left;
						Top = controlValue.Top;
						Width = controlValue.Width;
						Height = controlValue.Height;
					}

					// Restore Splitter, Grid and other values.
					FormCommon.RestoreSplitDistance(MainSplit, ControlValues);

					SourceGrid.LJCRestoreColumnValues(ControlValues);
					TargetGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		// Gets or sets the ControlValues item.
		internal ControlValues ControlValues { get; set; }
		#endregion

		#region Item Change Processing

		// Execute the list and related item functions.
		internal void DoChange(Change change)
		{
			Cursor = Cursors.WaitCursor;
			switch (change)
			{
				case Change.Startup:
					ConfigureControls();
					RestoreControlValues();

					// Load first Lists.
					DataRetrieveSource();
					DataRetrieveTarget();
					SelectMappedColumns();
					break;

				case Change.Source:
					SourceGrid.LJCSetLastRow();
					SelectTargetRowWithSource(SourceGrid.CurrentRow as LJCGridRow);
					break;

				case Change.Target:
					TargetGrid.LJCSetLastRow();
					SelectSourceRowWithTarget(TargetGrid.CurrentRow as LJCGridRow);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Source,
			Target
		}

		#region Item Change Support

		// Start the Change processing.
		private void StartItemChange()
		{
			ChangeTimer = new ChangeTimer();
			ChangeTimer.ItemChange += ChangeTimer_ItemChange;
			TimedChange(Change.Startup);
		}

		// Change Event Handler
		private void ChangeTimer_ItemChange(object sender, EventArgs e)
		{
			Change changeType;

			changeType = (Change)Enum.Parse(typeof(Change)
				, ChangeTimer.ChangeName);
			DoChange(changeType);
		}

		// Starts the Timer with the Change value.
		internal void TimedChange(Change change)
		{
			ChangeTimer.DoChange(change.ToString());
		}

		// Gets or sets the ChangeTimer object.
		internal ChangeTimer ChangeTimer { get; set; }
		#endregion
		#endregion

		#region Private Methods

		// Sets the control states based on the current control values.
		private void SetControlState()
		{
			SourceMenuNew.Enabled = false;
			SourceMenuDelete.Enabled = false;
			TargetMenuNew.Enabled = false;
			TargetMenuDelete.Enabled = false;

			LJCGridRow sourceRow = SourceGrid.CurrentRow as LJCGridRow;
			LJCGridRow targetRow = TargetGrid.CurrentRow as LJCGridRow;

			// Source Actions
			if (IsRowSelected(sourceRow)
				&& IsRowSelected(targetRow))
			{
				SourceMenuDelete.Enabled = true;
			}
			else
			{
				if (!IsRowSelected(targetRow))
				{
					SourceMenuNew.Enabled = true;
				}
			}

			// Target Actions
			if (IsRowSelected(targetRow)
				&& IsRowSelected(sourceRow))
			{
				TargetMenuDelete.Enabled = true;
			}
			else
			{
				if (!IsRowSelected(sourceRow))
				{
					TargetMenuNew.Enabled = true;
				}
			}
		}
		#endregion

		#region Action Event Handlers

		#region Source

		// Create new match from selected Source row.
		private void SourceMenuNew_Click(object sender, EventArgs e)
		{
			SetMatchChangesWithSource(SourceGrid.CurrentRow as LJCGridRow);
		}

		// Delete match from selected Source row.
		private void SourceMenuDelete_Click(object sender, EventArgs e)
		{
			SetMatchChangesWithSource(SourceGrid.CurrentRow as LJCGridRow);
		}
		#endregion

		#region Target

		// Create new match from selected Target row.
		private void TargetMenuNew_Click(object sender, EventArgs e)
		{
			SetMatchChangesWithTarget(TargetGrid.CurrentRow as LJCGridRow);
		}

		// Delete match from selected Target row.
		private void TargetMenuDelete_Click(object sender, EventArgs e)
		{
			SetMatchChangesWithTarget(TargetGrid.CurrentRow as LJCGridRow);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Form

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (DataSave())
			{
				SaveControlValues();
				DialogResult = DialogResult.OK;
			}
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Source

		// Handles the form keys.
		private void SourceGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					//DoRefreshSource();
					e.Handled = true;
					break;

				case Keys.Enter:
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						TargetGrid.Select();
					}
					else
					{
						TargetGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void SourceGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (SourceGrid.LJCGetMouseRowIndex(e) > -1)
			{
				SourceGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Source);
			}
		}

		// Handles the SelectionChanged event.
		private void SourceGrid_SelectionChanged(object sender, EventArgs e)
		{
			TimedChange(Change.Source);
		}

		// Handles the MouseDoubleClick event.
		private void SourceGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (SourceGrid.LJCGetMouseRow(e) is LJCGridRow row)
			{
				SetMatchChangesWithSource(row);
			}
		}
		#endregion

		#region Target

		// Handles the form keys.
		private void TargetGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					//DoRefreshTarget();
					e.Handled = true;
					break;

				case Keys.Enter:
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						SourceGrid.Select();
					}
					else
					{
						SourceGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void TargetGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (TargetGrid.LJCGetMouseRowIndex(e) > -1)
			{
				TargetGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Target);
			}
		}

		// Handles the SelectionChanged event.
		private void TargetGrid_SelectionChanged(object sender, EventArgs e)
		{
			TimedChange(Change.Target);
		}

		// Handles the MouseDoubleClick event.
		private void TargetGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (TargetGrid.LJCGetMouseRow(e) is LJCGridRow row)
			{
				SetMatchChangesWithTarget(row);
			}
		}
		#endregion
		#endregion

		#region Properties

		// The Managers object.
		internal TransformManagers Managers { get; set; }

		/// <summary>Gets or sets the parent ID value.</summary>
		public int LJCParentID { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;

		private Color mMappedColor = Color.FromArgb(222, 238, 255);  // 222, 238, 255
		private TransformMatches mChangeMatches;
		private int mSourceDataID;
		private int mTargetDataID;
		#endregion
	}
}
