// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MergeMapList.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
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

	// The list form.
	internal partial class MergeMapList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal MergeMapList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void MergeMapList_Load(object sender, EventArgs e)
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
			LayoutColumns layoutColumns;
			DataSource dataSource;
			TransformMaps records;

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
					layoutColumns	= layoutColumnManager.Load(keyColumns);
					string inValues = GetInList(layoutColumns);
					var transformMapManager = Managers.TransformMapManager;
					records = transformMapManager.LoadWithInValues(taskTransform.TransformID
						, SourceOrigin, inValues);
					if (NetCommon.HasItems(records))
					{
						foreach (TransformMap record in records)
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
		private LJCGridRow RowAddSource(TransformMap dataRecord)
		{
			LJCGridRow retValue;

			retValue = SourceGrid.LJCRowAdd();
			SetStoredValuesSource(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(SourceGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateSource(TransformMap dataRecord)
		{
			if (SourceGrid.CurrentRow is LJCGridRow row)
			{
				string mapTypeName = Enum.GetName(typeof(MapType), dataRecord.MapTypeID);
				row.LJCSetCellText(TransformMap.ColumnMapTypeName, mapTypeName);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesSource(LJCGridRow row, TransformMap dataRecord)
		{
			row.LJCSetInt32(LayoutColumn.ColumnLayoutColumnID, dataRecord.LayoutColumnID);
		}
		#endregion

		#region Target

		// Retrieves the list rows.
		private void DataRetrieveTarget()
		{
			TaskTransform taskTransform;
			LayoutColumns layoutColumns;
			DataSource dataSource;
			TransformMaps records;

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
					layoutColumns
						= Managers.LayoutColumnManager.Load(keyColumns);
					string inValues = GetInList(layoutColumns);
					var transformMapManager = Managers.TransformMapManager;
					records = transformMapManager.LoadWithInValues(taskTransform.TransformID
						, TargetOrigin, inValues);
					if (NetCommon.HasItems(records))
					{
						foreach (TransformMap record in records)
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
		private LJCGridRow RowAddTarget(TransformMap dataRecord)
		{
			LJCGridRow retValue;

			retValue = TargetGrid.LJCRowAdd();
			SetStoredValuesTarget(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(TargetGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateTarget(TransformMap dataRecord)
		{
			if (TargetGrid.CurrentRow is LJCGridRow row)
			{
				string mapTypeName = Enum.GetName(typeof(MapType), dataRecord.MapTypeID);
				row.LJCSetCellText(TransformMap.ColumnMapTypeName, mapTypeName);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesTarget(LJCGridRow row, TransformMap dataRecord)
		{
			row.LJCSetInt32(LayoutColumn.ColumnLayoutColumnID, dataRecord.LayoutColumnID);
		}
		#endregion

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			TransformMap lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;

			foreach (TransformMap changeMap in mChangeMaps)
			{
				lookupRecord = GetDbMap(changeMap.TransformID, changeMap.SourceColumnID
					, changeMap.TargetColumnID);

				var transformMapManager = Managers.TransformMapManager;
				switch (changeMap.ChangeStatus)
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
							transformMapManager.Add(changeMap);
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
								{ TransformMap.ColumnTransformID, changeMap.TransformID },
								{ TransformMap.ColumnSourceColumnID, changeMap.SourceColumnID },
								{ TransformMap.ColumnTargetColumnID, changeMap.TargetColumnID }
							};
							transformMapManager.Update(changeMap, keyColumns);
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
								{ TransformMap.ColumnTransformID, changeMap.TransformID },
								{ TransformMap.ColumnSourceColumnID, changeMap.SourceColumnID },
								{ TransformMap.ColumnTargetColumnID, changeMap.TargetColumnID }
							};
							transformMapManager.Delete(keyColumns);
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
				sourceTypeName = GetSourceTypeName(dataSource.SourceTypeID);
				itemName = GetItemName(dataSource.SourceItemName);
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

		#region Custom DataRetrieve Methods

		// Creates the LayoutColumn in list.
		private string GetInList(LayoutColumns layoutColumns)
		{
			string retValue;

			List<string> items = new List<string>();
			foreach (LayoutColumn record in layoutColumns)
			{
				items.Add(record.LayoutColumnID.ToString());
			}
			retValue = CreateList(items);
			return retValue;
		}

		// Creates a delimited list of items.
		private string CreateList(List<string> items)
		{
			string retValue;

			StringBuilder builder = new StringBuilder(64);
			foreach (string item in items)
			{
				if (builder.Length == 0)
				{
					builder.Append("(");
				}
				else
				{
					builder.Append(", ");
				}
				builder.Append($"{item}");
			}
			if (builder.Length > 0)
			{
				builder.Append(")");
			}
			retValue = builder.ToString();
			return retValue;
		}
		#endregion
		#endregion

		#region Row Action Methods

		// Selects the current Target row based on the mapped SourceRow.
		// Execute with Click on SourceGrid item.
		private void SelectTargetRowWithSource(LJCGridRow sourceRow)
		{
			LJCGridRow targetRow = GetTargetRowWithSource(sourceRow);
			if (targetRow != null)
			{
				TargetGrid.LJCSetCurrentRow(targetRow, false);
			}
		}

		// Selects the current Source row based on the mapped TargetRow.
		// Execute with Click on TargetGrid item.
		private void SelectSourceRowWithTarget(LJCGridRow targetRow)
		{
			LJCGridRow sourceRow = GetSourceRowWithTarget(targetRow);
			if (sourceRow != null)
			{
				SourceGrid.LJCSetCurrentRow(sourceRow, false);
			}
		}

		// Sets map values using the Source row.
		// Executed with Double-Click on SourceGrid item or Context Menu.
		private void SetMapChangesWithSource(LJCGridRow sourceRow)
		{
			bool isAvailable = true;

			if (sourceRow != null)
			{
				// Source row is not selected.
				if (!IsRowSelected(sourceRow))
				{
					// Checks for ChangeMap or TransformMap.
					if (IsSourceRowMapped(sourceRow))
					{
						isAvailable = false;
					}
				}

				if (isAvailable)
				{
					LJCGridRow targetRow = GetTargetRowWithSource(sourceRow);

					// New map so use current Target row.
					if (null == targetRow)
					{
						targetRow = TargetGrid.CurrentRow as LJCGridRow;
					}

					if (IsRowSelected(sourceRow))
					{
						isAvailable = SetChangeMapDelete(sourceRow, targetRow);
					}
					else
					{
						isAvailable = SetChangeMapAdd(sourceRow, targetRow);
					}
					if (isAvailable)
					{
						ToggleRowColors(sourceRow, targetRow);
					}
				}
			}
		}

		// Sets map values using the Target row.
		// Executed with Double-Click on TargeGrid item or Context Menu.
		private void SetMapChangesWithTarget(LJCGridRow targetRow)
		{
			bool isAvailable = true;

			if (targetRow != null)
			{
				// Target row is not selected.
				if (!IsRowSelected(targetRow))
				{
					// Checks for ChangeMap or TransformMap.
					if (IsTargetRowMapped(targetRow))
					{
						isAvailable = false;
					}
				}

				if (isAvailable)
				{
					LJCGridRow sourceRow = GetSourceRowWithTarget(targetRow);

					// New map so use current Source row.
					if (null == sourceRow)
					{
						sourceRow = SourceGrid.CurrentRow as LJCGridRow;
					}

					if (IsRowSelected(targetRow))
					{
						isAvailable = SetChangeMapDelete(sourceRow, targetRow);
					}
					else
					{
						isAvailable = SetChangeMapAdd(sourceRow, targetRow);
					}
					if (isAvailable)
					{
						ToggleRowColors(sourceRow, targetRow);
					}
				}
			}
		}

		// Performs a ChangeMap "Add".
		private bool SetChangeMapAdd(LJCGridRow sourceRow, LJCGridRow targetRow)
		{
			TransformMap transformMap = null;
			TransformMap changeMap;
			int sourceColumnID = 0;
			int targetColumnID = 0;
			string message;
			bool retValue = true;

			if (IsRowSelected(sourceRow)
				|| IsRowSelected(targetRow))
			{
				retValue = false;
				message = "Source or Target is already selected.";
				MessageBox.Show(message, "Invalid TransformMap", MessageBoxButtons.OK
					, MessageBoxIcon.Information);
			}

			if (retValue)
			{
				// Get TransformMap.
				sourceColumnID = GetRowLayoutColumnID(sourceRow);
				targetColumnID = GetRowLayoutColumnID(targetRow);
				transformMap = GetDbMap(LJCParentID, sourceColumnID, targetColumnID);
				if (null == transformMap)
				{
					// Potential new map so create it.
					transformMap = new TransformMap()
					{
						TransformID = LJCParentID,
						SourceColumnID = (short)sourceColumnID,
						TargetColumnID = (short)targetColumnID,
						MapTypeID = (int)MapType.Merge
					};

					// If not already used in another TransformMap.
					if (!IsAvailableSourceDbMap(LJCParentID, transformMap.SourceColumnID)
						&& !IsAvailableTargetDbMap(LJCParentID, transformMap.TargetColumnID))
					{
						retValue = false;
						message = "Source or Target is already part of another "
							+ "TransformMap.";
						MessageBox.Show(message, "Invalid TransformMap", MessageBoxButtons.OK
							, MessageBoxIcon.Information);
					}
				}
			}

			if (retValue)
			{
				// Get map from Change collection.
				changeMap = GetChangeMap(sourceColumnID, targetColumnID);
				if (changeMap != null)
				{
					// Remove the "Delete" for item that is being added.
					if (changeMap.ChangeStatus == ChangeStatus.Delete)
					{
						mChangeMaps.Remove(changeMap);
					}
				}
				else
				{
					// If not already used in another Change Map.
					retValue = false;
					if (IsAvailableSourceChangeMap(sourceColumnID)
						&& IsAvailableTargetChangeMap(targetColumnID))
					{
						retValue = true;

						// Add an "Add" for item that is being added.
						transformMap.ChangeStatus = ChangeStatus.Add;
						mChangeMaps.Add(transformMap);
					}
				}
			}
			return retValue;
		}

		// Performs a ChangeMap "Delete".
		private bool SetChangeMapDelete(LJCGridRow sourceRow, LJCGridRow targetRow)
		{
			TransformMap transformMap = null;
			TransformMap changeMap;
			int sourceColumnID = 0;
			int targetColumnID = 0;
			string message;
			bool retValue = true;

			if (!IsRowSelected(sourceRow)
				|| !IsRowSelected(targetRow))
			{
				retValue = false;
				message = "Source or Target is not selected.";
				MessageBox.Show(message, "Invalid TransformMap", MessageBoxButtons.OK
					, MessageBoxIcon.Information);
			}

			if (retValue)
			{
				// Get TransformMap.
				sourceColumnID = GetRowLayoutColumnID(sourceRow);
				targetColumnID = GetRowLayoutColumnID(targetRow);
				transformMap = GetDbMap(LJCParentID, sourceColumnID, targetColumnID);
			}

			if (retValue)
			{
				changeMap = GetChangeMap(sourceColumnID, targetColumnID);
				if (changeMap != null)
				{
					// Remove the "Add" for item that is being deleted.
					if (changeMap.ChangeStatus == ChangeStatus.Add)
					{
						mChangeMaps.Remove(changeMap);
					}
				}
				else
				{
					// If not already used in another Change Map.
					retValue = false;
					if (IsAvailableSourceChangeMap(sourceColumnID)
						&& IsAvailableTargetChangeMap(targetColumnID))
					{
						retValue = true;

						// Add a "Delete" for item that is being deleted.
						transformMap.ChangeStatus = ChangeStatus.Delete;
						mChangeMaps.Add(transformMap);
					}
				}
			}
			return retValue;
		}

		// Checks if a MapType change is available.
		private bool IsMapTypeChangeAvailable(LJCGridRow sourceRow, short mapTypeChange)
		{
			short currentMapType;
			bool retValue = false;

			if (sourceRow != null
				&& IsRowSelected(sourceRow))
			{
				currentMapType = GetCurrentMapType(sourceRow);
				if (currentMapType != -1
					&& currentMapType != mapTypeChange)
				{
					retValue = true;
				}
			}
			return retValue;
		}

		// Gets the current MapType.
		private short GetCurrentMapType(LJCGridRow sourceRow)
		{
			short retValue = -1;

			if (sourceRow != null
				&& IsRowSelected(sourceRow))
			{
				TransformMap changeMap = GetSourceRowChangeMap(sourceRow);
				if (changeMap != null)
				{
					if (changeMap.ChangeStatus == ChangeStatus.Add
						|| changeMap.ChangeStatus == ChangeStatus.Change)
					{
						retValue = changeMap.MapTypeID;
					}
				}
				else
				{
					TransformMap transformMap = GetSourceRowDbMap(LJCParentID, sourceRow);
					if (transformMap != null)
					{
						retValue = transformMap.MapTypeID;
					}
				}
			}
			return retValue;
		}

		// Sets the changed MapType value.
		private void SetMapTypeChange(LJCGridRow sourceRow, short mapTypeChange)
		{
			if (IsMapTypeChangeAvailable(sourceRow, mapTypeChange))
			{
				TransformMap changeMap = GetSourceRowChangeMap(sourceRow);
				if (changeMap != null)
				{
					changeMap.MapTypeID = mapTypeChange;
					RowUpdateSource(changeMap);
					RowUpdateTarget(changeMap);
				}
				else
				{
					TransformMap transformMap = GetSourceRowDbMap(LJCParentID, sourceRow);
					if (transformMap != null)
					{
						transformMap.ChangeStatus = ChangeStatus.Change;
						transformMap.MapTypeID = mapTypeChange;
						mChangeMaps.Add(transformMap);
						RowUpdateSource(transformMap);
						RowUpdateTarget(transformMap);
					}
				}
			}
		}
		#endregion

		#region Row Helper Methods

		// Get the mapped Source row using the Target row.
		private LJCGridRow GetSourceRowWithTarget(LJCGridRow targetRow)
		{
			TransformMap changeMap;
			TransformMap transformMap;
			LJCGridRow retValue = null;

			if (IsRowSelected(targetRow))
			{
				// Check ChangeMap first.
				changeMap = GetTargetRowChangeMap(targetRow);
				if (changeMap != null
					&& changeMap.TargetColumnID > 0)
				{
					if (changeMap.ChangeStatus != ChangeStatus.Delete)
					{
						retValue = FindLayoutGridRow(SourceGrid, changeMap.SourceColumnID);
					}
				}
				else
				{
					// No ChangeMap so check TransformMap.
					transformMap = GetTargetRowDbMap(LJCParentID, targetRow);
					if (transformMap != null
						&& transformMap.TargetColumnID > 0)
					{
						retValue = FindLayoutGridRow(SourceGrid, transformMap.SourceColumnID);
					}
				}
			}
			return retValue;
		}

		// Get the mapped Target row using the Source row.
		private LJCGridRow GetTargetRowWithSource(LJCGridRow sourceRow)
		{
			TransformMap changeMap;
			TransformMap transformMap;
			LJCGridRow retValue = null;

			if (IsRowSelected(sourceRow))
			{
				// Check ChangeMap first.
				changeMap = GetSourceRowChangeMap(sourceRow);
				if (changeMap != null
					&& changeMap.TargetColumnID > 0)
				{
					if (changeMap.ChangeStatus != ChangeStatus.Delete)
					{
						retValue = FindLayoutGridRow(TargetGrid, changeMap.TargetColumnID);
					}
				}
				else
				{
					// No ChangeMap so check TransformMap.
					transformMap = GetSourceRowDbMap(LJCParentID, sourceRow);
					if (transformMap != null
						&& transformMap.TargetColumnID > 0)
					{
						retValue = FindLayoutGridRow(TargetGrid, transformMap.TargetColumnID);
					}
				}
			}
			return retValue;
		}

		// Get the Source row ChangeMap.
		private TransformMap GetSourceRowChangeMap(LJCGridRow sourceRow)
		{
			TransformMap retValue = null;

			if (sourceRow != null)
			{
				int layoutColumnID = GetRowLayoutColumnID(sourceRow);
				if (layoutColumnID > 0)
				{
					retValue = GetSourceChangeMap(layoutColumnID);
				}
			}
			return retValue;
		}

		// Get the Target row ChangeMap.
		private TransformMap GetTargetRowChangeMap(LJCGridRow targetRow)
		{
			TransformMap retValue = null;

			if (targetRow != null)
			{
				int layoutColumnID = GetRowLayoutColumnID(targetRow);
				if (layoutColumnID > 0)
				{
					retValue = GetTargetChangeMap(layoutColumnID);
				}
			}
			return retValue;
		}

		// Get the Source row map.
		private TransformMap GetSourceRowDbMap(int transformID, LJCGridRow sourceRow)
		{
			TransformMap retValue = null;

			int layoutColumnID = GetRowLayoutColumnID(sourceRow);
			if (layoutColumnID > 0)
			{
				retValue = GetSourceDbMap(transformID, (short)layoutColumnID);
			}
			return retValue;
		}

		// Get the Target row map.
		private TransformMap GetTargetRowDbMap(int transformID, LJCGridRow targetRow)
		{
			TransformMap retValue = null;

			int layoutColumnID = GetRowLayoutColumnID(targetRow);
			if (layoutColumnID > 0)
			{
				retValue = GetTargetDbMap(transformID, layoutColumnID);
			}
			return retValue;
		}

		// Determines if the Source row has ChangeMap or TransformMap.
		private bool IsSourceRowMapped(LJCGridRow sourceRow)
		{
			TransformMap changeMap;
			TransformMap dbMap;
			bool retValue = false;

			if (sourceRow != null)
			{
				// Check ChangeMap first.
				changeMap = GetSourceRowChangeMap(sourceRow);
				if (changeMap != null)
				{
					switch (changeMap.ChangeStatus)
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
					// No ChangeMap so check TransformMap.
					dbMap = GetSourceRowDbMap(LJCParentID, sourceRow);
					if (dbMap != null)
					{
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// Determines if the Target row hast ChangeMap or TransformMap.
		private bool IsTargetRowMapped(LJCGridRow targetRow)
		{
			TransformMap changeMap;
			TransformMap transformMap;
			bool retValue = false;

			if (targetRow != null)
			{
				// Check ChangeMap first.
				changeMap = GetTargetRowChangeMap(targetRow);
				if (changeMap != null)
				{
					switch (changeMap.ChangeStatus)
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
					// No ChangeMap so check TransformMap.
					transformMap = GetTargetRowDbMap(LJCParentID, targetRow);
					if (transformMap != null)
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

		// Gets the Source ChangeMap.
		private TransformMap GetSourceChangeMap(int sourceColumnID)
		{
			TransformMap retValue;

			retValue = mChangeMaps.LJCSearchSourceID(LJCParentID
				, sourceColumnID);
			return retValue;
		}

		// Gets the Target ChangeMap.
		private TransformMap GetTargetChangeMap(int targetColumnID)
		{
			TransformMap retValue;

			retValue = mChangeMaps.LJCSearchTargetID(LJCParentID
				, targetColumnID);
			return retValue;
		}

		// Gets the ChangeMap.
		private TransformMap GetChangeMap(int sourceColumnID, int targetColumnID)
		{
			TransformMap retValue;

			retValue = mChangeMaps.LJCSearchColumnIDs(LJCParentID
				, sourceColumnID, targetColumnID);
			return retValue;
		}

		// Is the Source not already used in the change map.
		private bool IsAvailableSourceChangeMap(int sourceColumnID)
		{
			TransformMap lookupMap;
			bool retValue = true;

			lookupMap = GetSourceChangeMap(sourceColumnID);
			if (lookupMap != null)
			{
				retValue = false;
			}
			return retValue;
		}

		// Is the Target not already used in the change map.
		private bool IsAvailableTargetChangeMap(int targetColumnID)
		{
			TransformMap lookupMap;
			bool retValue = true;

			lookupMap = GetTargetChangeMap(targetColumnID);
			if (lookupMap != null)
			{
				retValue = false;
			}
			return retValue;
		}

		// Is the Source not already used in the TransformMap.
		private bool IsAvailableSourceDbMap(int transformID, int sourceColumnID)
		{
			TransformMap lookupMap;
			bool retValue = true;

			TransformMap changeMap = GetSourceChangeMap(sourceColumnID);
			if (null == changeMap
				|| changeMap.ChangeStatus != ChangeStatus.Delete)
			{
				lookupMap = GetSourceDbMap(transformID, (short)sourceColumnID);
				if (lookupMap != null)
				{
					retValue = false;
				}
			}
			return retValue;
		}

		// Is the Target not already used in the TransformMap.
		private bool IsAvailableTargetDbMap(int transformID, int targetColumnID)
		{
			TransformMap lookupMap;
			bool retValue = true;

			TransformMap changeMap = GetTargetChangeMap(targetColumnID);
			if (null == changeMap
				|| changeMap.ChangeStatus != ChangeStatus.Delete)
			{
				lookupMap = GetTargetDbMap(transformID, targetColumnID);
				if (lookupMap != null)
				{
					retValue = false;
				}
			}
			return retValue;
		}
		#endregion

		#region Data Helper Methods

		// Gets the TransformMap with Source ID.
		private TransformMap GetSourceDbMap(int transformID, int sourceColumnID)
		{
			TransformMap retValue;

			var keyColumns = new DbColumns()
			{
				{ TransformMap.ColumnTransformID, transformID },
				{ TransformMap.ColumnSourceColumnID, sourceColumnID }
			};
			var transformMapManager = Managers.TransformMapManager;
			DbJoins dbJoins = transformMapManager.GetLoadJoins();
			retValue = transformMapManager.Retrieve(keyColumns, joins: dbJoins);
			return retValue;
		}

		// Gets the TransformMap with Target ID.
		private TransformMap GetTargetDbMap(int transformID, int targetColumnID)
		{
			TransformMap retValue;

			var keyColumns = new DbColumns()
			{
				{ TransformMap.ColumnTransformID, transformID },
				{ TransformMap.ColumnTargetColumnID, targetColumnID }
			};
			var transformMapManager = Managers.TransformMapManager;
			DbJoins dbJoins = transformMapManager.GetLoadJoins();
			retValue = transformMapManager.Retrieve(keyColumns, joins: dbJoins);
			return retValue;
		}

		// Gets the TransformMap with Source ID and Target ID.
		private TransformMap GetDbMap(int transformID, int sourceColumnID
			, int targetColumnID)
		{
			TransformMap retValue;

			var keyColumns = new DbColumns()
			{
				{ TransformMap.ColumnTransformID, transformID },
				{ TransformMap.ColumnSourceColumnID, sourceColumnID },
				{ TransformMap.ColumnTargetColumnID, targetColumnID }
			};
			retValue = Managers.TransformMapManager.Retrieve(keyColumns);
			return retValue;
		}
		#endregion

		#region Map Color Methods

		// Sets the color for the mapped columns.
		private void SelectMappedColumns()
		{
			TransformMaps transformMaps;
			LayoutColumn layoutColumn;

			var transformMapManager = Managers.TransformMapManager;
			transformMaps = transformMapManager.LoadWithTransformID(LJCParentID);
			if (transformMaps != null)
			{
				var layoutColumnManager = Managers.LayoutColumnManager;
				foreach (TransformMap transformMap in transformMaps)
				{
					layoutColumn = layoutColumnManager.RetrieveWithID(transformMap.SourceColumnID);
					SelectMappedColumn(SourceGrid, layoutColumn.LayoutColumnID);
					layoutColumn = layoutColumnManager.RetrieveWithID(transformMap.TargetColumnID);
					SelectMappedColumn(TargetGrid, layoutColumn.LayoutColumnID);
				}
			}
		}

		// Sets the color for the mapped column.
		private bool SelectMappedColumn(LJCDataGrid grid, int layoutColumnID)
		{
			bool retValue = false;

			LJCGridRow row = FindLayoutGridRow(grid, layoutColumnID);
			if (row != null)
			{
				retValue = true;
				SetRowSelected(row, true);
			}
			return retValue;
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
		/// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesTransformManager.Instance;

			mSettings = values.StandardSettings;
			mMappedColor = values.SelectColor;

			// Initialize Class Data.
			Managers = new TransformManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\MergeMapList.xml";
			mChangeMaps = new TransformMaps();

			SetupGridSource();
			SetupGridTarget();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Setup the grid columns.
		private void SetupGridSource()
		{
			SourceGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == SourceGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					TransformMap.ColumnSourceColumnName,
					TransformMap.ColumnSourceDescription,
					TransformMap.ColumnMapTypeName
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsSource
					= Managers.TransformMapManager.GetColumns(propertyNames);

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
				List<string> propertyNames = new List<string> {
					TransformMap.ColumnSourceColumnName,
					TransformMap.ColumnSourceDescription,
					TransformMap.ColumnMapTypeName
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsTarget
					= Managers.TransformMapManager.GetColumns(propertyNames);

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
			SourceMenuMerge.Enabled = false;
			SourceMenuOverwrite.Enabled = false;
			SourceMenuInsert.Enabled = false;
			TargetMenuNew.Enabled = false;
			TargetMenuDelete.Enabled = false;
			TargetMenuMerge.Enabled = false;
			TargetMenuOverwrite.Enabled = false;
			TargetMenuInsert.Enabled = false;

			LJCGridRow sourceRow = SourceGrid.CurrentRow as LJCGridRow;
			LJCGridRow targetRow = TargetGrid.CurrentRow as LJCGridRow;

			// Source Actions
			if (IsRowSelected(sourceRow)
				&& IsRowSelected(targetRow))
			{
				SourceMenuDelete.Enabled = true;
				if (IsMapTypeChangeAvailable(sourceRow, (short)MapType.Merge))
				{
					SourceMenuMerge.Enabled = true;
				}
				if (IsMapTypeChangeAvailable(sourceRow, (short)MapType.Overwrite))
				{
					SourceMenuOverwrite.Enabled = true;
				}
				if (IsMapTypeChangeAvailable(sourceRow, (short)MapType.InsertInclude))
				{
					SourceMenuInsert.Enabled = true;
				}
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
				if (IsMapTypeChangeAvailable(targetRow, (short)MapType.Merge))
				{
					TargetMenuMerge.Enabled = true;
				}
				if (IsMapTypeChangeAvailable(targetRow, (short)MapType.Overwrite))
				{
					TargetMenuOverwrite.Enabled = true;
				}
				if (IsMapTypeChangeAvailable(targetRow, (short)MapType.InsertInclude))
				{
					TargetMenuInsert.Enabled = true;
				}
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

		// Create new map from selected Source row.
		private void SourceMenuNew_Click(object sender, EventArgs e)
		{
			SetMapChangesWithSource(SourceGrid.CurrentRow as LJCGridRow);
		}

		// Delete map from selected Source row.
		private void SourceMenuDelete_Click(object sender, EventArgs e)
		{
			SetMapChangesWithSource(SourceGrid.CurrentRow as LJCGridRow);
		}

		// Sets Map Action to Merge.
		private void SourceMenuMerge_Click(object sender, EventArgs e)
		{
			SetMapTypeChange(SourceGrid.CurrentRow as LJCGridRow
				, (short)MapType.Merge);
		}

		// Sets Map Action to Overwrite.
		private void SourceMenuOverwrite_Click(object sender, EventArgs e)
		{
			SetMapTypeChange(SourceGrid.CurrentRow as LJCGridRow
				, (short)MapType.Overwrite);
		}

		// Sets Map Action to Insert.
		private void SourceMenuInsert_Click(object sender, EventArgs e)
		{
			SetMapTypeChange(SourceGrid.CurrentRow as LJCGridRow
				, (short)MapType.InsertInclude);
		}
		#endregion

		#region Target

		// Create new map from selected Target row.
		private void TargetMenuNew_Click(object sender, EventArgs e)
		{
			SetMapChangesWithTarget(TargetGrid.CurrentRow as LJCGridRow);
		}

		// Delete map from selected Target row.
		private void TargetMenuDelete_Click(object sender, EventArgs e)
		{
			SetMapChangesWithTarget(TargetGrid.CurrentRow as LJCGridRow);
		}

		// Sets Map Action to Merge.
		private void TargetMenuMerge_Click(object sender, EventArgs e)
		{
			LJCGridRow sourceRow;
			sourceRow = GetSourceRowWithTarget(TargetGrid.CurrentRow as LJCGridRow);
			if (sourceRow != null)
			{
				SetMapTypeChange(sourceRow, (short)MapType.Merge);
			}
		}

		// Sets Map Action to Overwrite.
		private void TargetMenuOverwrite_Click(object sender, EventArgs e)
		{
			LJCGridRow sourceRow;
			sourceRow = GetSourceRowWithTarget(TargetGrid.CurrentRow as LJCGridRow);
			if (sourceRow != null)
			{
				SetMapTypeChange(sourceRow, (short)MapType.Overwrite);
			}
		}

		// Sets Map Action to Insert.
		private void TargetMenuInsert_Click(object sender, EventArgs e)
		{
			LJCGridRow sourceRow;
			sourceRow = GetSourceRowWithTarget(TargetGrid.CurrentRow as LJCGridRow);
			if (sourceRow != null)
			{
				SetMapTypeChange(sourceRow, (short)MapType.InsertInclude);
			}
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
				SetMapChangesWithSource(row);
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
				SetMapChangesWithTarget(row);
			}
		}
		#endregion
		#endregion

		#region Properties

		// Gets or sets the parent ID value.
		internal int LJCParentID { get; set; }

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private const int SourceOrigin = 1;
		private const int TargetOrigin = 2;

		private StandardUISettings mSettings;
		private string mControlValuesFileName;

		private Color mMappedColor = Color.FromArgb(222, 238, 255);  // 222, 238, 255
		private TransformMaps mChangeMaps;
		private int mSourceDataID;
		private int mTargetDataID;
		#endregion
	}
}
