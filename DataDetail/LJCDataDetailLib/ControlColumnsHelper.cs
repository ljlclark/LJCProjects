using System;
using LJCNetCommon;

namespace LJCDataDetailLib
{
	/// <summary>ControlColumns helper class.</summary>
	public class ControlColumnsHelper
	{
		#region Create ControlColumn configuration.

		// Adjust the control for usability.
		/// <include path='items/AdjustedWidth/*' file='Doc/ControlColumnsHelper.xml'/>
		public int AdjustedWidth(DbColumn dataColumn)
		{
			ControlRows cfg = ConfigRows;
			int minWidth = 4;
			int intWidth = 12;
			int retValue = minWidth * cfg.CharacterPixels;

			switch (dataColumn.DataTypeName.ToLower())
			{
				case "string":
					retValue = dataColumn.MaxLength * cfg.CharacterPixels;
					if (dataColumn.MaxLength > cfg.MaxControlCharacters)
					{
						retValue = cfg.MaxControlCharacters * cfg.CharacterPixels;
					}
					break;

				case "boolean":
					retValue = dataColumn.Caption.Length * cfg.CharacterPixels;
					if (dataColumn.Caption.Length > cfg.MaxControlCharacters)
					{
						retValue = cfg.MaxControlCharacters * cfg.CharacterPixels;
					}
					break;

				case "int16":
				case "int32":
				case "int64":
					retValue = intWidth * cfg.CharacterPixels;
					break;

				default:
					retValue = minWidth * cfg.CharacterPixels;
					break;
			}
			return retValue;
		}

		// Sets the ControlColumn values using a configuration XML file.
		/// <include path='items/CreateConfigControlColumns/*' file='Doc/ControlColumnsHelper.xml'/>
		public ControlColumns CreateConfigControlColumns(DbColumns dataColumns)
		{
			ControlColumns retValue = null;

			if (dataColumns != null && dataColumns.Count > 0
				&& CheckDataColumnCount(dataColumns.Count))
			{
				retValue = GetControlColumns(ConfigRows.ControlColumnsCount
					, dataColumns);
			}
			return retValue;
		}

		// Configure the New controls.
		/// <include path='items/CreateNewControlColumns/*' file='Doc/ControlColumnsHelper.xml'/>
		public ControlColumns CreateNewControlColumns(DbColumns dataColumns)
		{
			ControlColumns retValue = null;

			//ConfigRows = configRows;
			var cfg = ConfigRows;
			cfg.ControlColumnsCount = 0;
			if (dataColumns != null && dataColumns.Count > 0)
			{
				cfg.ControlColumnsCount = CalculateColumnsCount(dataColumns.Count);
				retValue = GetControlColumns(cfg.ControlColumnsCount, dataColumns);
			}

			// Calculate config values.
			cfg.DataValueCount = dataColumns.Count;
			cfg.ControlsWidth = Width(retValue);
			if (dataColumns != null)
			{
				cfg.ControlsHeight = Height(dataColumns.Count);
			}
			return retValue;
		}

		// Gets the Controls display height.
		/// <include path='items/Height/*' file='Doc/ControlColumnsHelper.xml'/>
		public int Height(int dataColumnsCount)
		{
			int rowCount;
			int retValue;

			ControlRows cfg = ConfigRows;

			// Top and bottom spacing.
			retValue = cfg.BorderVertical * 2;

			// Get the actual row count.
			rowCount = cfg.ColumnRowsLimit;
			if (dataColumnsCount < rowCount)
			{
				rowCount = dataColumnsCount;
			}

			// Space between each ControlRow.
			retValue += cfg.ControlRowSpacing * (rowCount - 1);

			// Height of each ControlRow.
			retValue += cfg.ControlRowHeight * rowCount;
			return retValue;
		}

		// Gets the Controls display width.
		/// <include path='items/Width/*' file='Doc/ControlColumnsHelper.xml'/>
		public int Width(ControlColumns controlColumns)
		{
			int currentWidth = 0;
			int retValue = 0;

			int count = controlColumns.Count;
			foreach (ControlColumn controlColumn in controlColumns)
			{
				// First column of controls for this page.
				var cfg = ConfigRows;
				if (0 == controlColumn.Index % cfg.PageColumnsLimit)
				{
					// Start with border before each ControlColumn and border after last.
					currentWidth = cfg.BorderHorizontal * (count + 1);
					if (count > cfg.PageColumnsLimit)
					{
						currentWidth = cfg.BorderHorizontal * (cfg.PageColumnsLimit + 1);
					}
				}

				currentWidth += controlColumn.Width;
				if (currentWidth > retValue)
				{
					// Save widest page.
					retValue = currentWidth;
				}
			}
			return retValue;
		}

		// Gets the ControlColumn column count. 
		/// <include path='items/CalculateColumnsCount/*' file='Doc/ControlColumnsHelper.xml'/>
		private int CalculateColumnsCount(int dataColumnsCount)
		{
			int retValue = 1;

			ControlRows cfg = ConfigRows;

			// There are more data values than can fit in one ControlColumn.
			if (dataColumnsCount > cfg.ColumnRowsLimit)
			{
				// Divide the data values into columns.
				retValue = dataColumnsCount / cfg.ColumnRowsLimit;
				if (dataColumnsCount % cfg.ColumnRowsLimit != 0)
				{
					// An extra row is left so add another column.
					retValue++;
				}

				// Evenly distribute rows.
				cfg.ColumnRowsLimit = (dataColumnsCount / retValue);
				if (dataColumnsCount % retValue != 0)
				{
					cfg.ColumnRowsLimit++;
				}
			}
			return retValue;
		}
		#endregion

		#region Private Methods

		// Check the DataValue value.
		private bool CheckDataColumnCount(int dataColumnsCount)
		{
			string message;
			bool retValue = true;

			if (dataColumnsCount != ConfigRows.DataValueCount)
			{
				retValue = false;
				message = $"DataValue count ({dataColumnsCount}) does not equal "
					+ "Configuration DataValue count ({ConfigRows.DataValueCount}).";
			}
			return retValue;
		}

		// Gets the ControlColumns object.
		private ControlColumns GetControlColumns(int controlColumnsCount, DbColumns dataColumns)
		{
			var retValue = new ControlColumns();

			for (int index = 0; index < controlColumnsCount; index++)
			{
				// Calculate ControlColumn values.
				var cfg = ConfigRows;
				int currentRowsCount = cfg.ColumnRowsLimit;
				int startRowDataIndex = cfg.ColumnRowsLimit * index;
				int endRowDataIndex = startRowDataIndex + (currentRowsCount - 1);

				// More rows available for this ControlColumn than remaining data elements.
				// Set the end row to the number of data elements remaining.
				if (endRowDataIndex > dataColumns.Count - 1)
				{
					int excessRowCount = endRowDataIndex - (dataColumns.Count - 1);
					currentRowsCount = currentRowsCount - excessRowCount;
					endRowDataIndex = dataColumns.Count - 1;
				}

				// get the ControlColumn widths.
				GetWidths(dataColumns, startRowDataIndex, endRowDataIndex, out int labelsWidth
					, out int controlsWidth);

				// Create and add the ControlColumn.
				ControlColumn controlColumn = new ControlColumn()
				{
					ControlsWidth = controlsWidth,
					Index = index,
					LabelsWidth = labelsWidth,
					RowCount = currentRowsCount
				};
				retValue.Add(controlColumn);
			}
			return retValue;
		}

		// Get labels and controls width for the specified start and stop
		// DataColumn indexes.
		private void GetWidths(DbColumns dataColumns, int startIndex, int stopIndex, out int labelsWidth
			, out int controlsWidth)
		{
			DbColumn dbColumn;
			int width;

			labelsWidth = 100;
			controlsWidth = 120;

			if (dataColumns != null && dataColumns.Count > 0)
			{
				for (int index = startIndex; index <= stopIndex; index++)
				{
					if (index > dataColumns.Count - 1)
					{
						break;
					}
					else
					{
						dbColumn = dataColumns[index];

						//// Do not include if not AllowDisplay.
						//if (DetailConfigColumns != null && DetailConfigColumns.Count > 0)
						//{
						//	ConfigRow configColumn;
						//	configColumn = DetailConfigColumns.SearchName(dbColumn.ColumnName);
						//	if (configColumn != null && false == configColumn.AllowDisplay)
						//	{
						//		continue;
						//	}
						//}

						if (dbColumn.Caption != null)
						{
							width = dbColumn.Caption.Length * ConfigRows.CharacterPixels;
							if (width > labelsWidth)
							{
								labelsWidth = width;
							}

							width = AdjustedWidth(dbColumn);
							if (width > controlsWidth)
							{
								controlsWidth = width;
							}
						}
					}
				}
			}
		}
		#endregion

		#region Properties

		// The Configuration Rows.
		/// <summary>Gets or sets the ConfigRows value.</summary>
		public ControlRows ConfigRows { get; set; }
		#endregion
	}
}
