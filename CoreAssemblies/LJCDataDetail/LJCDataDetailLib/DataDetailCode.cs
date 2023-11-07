// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataDetailCode.cs
using LJCDataDetailDAL;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDataDetailLib
{
  /// <summary>
  /// Contains methods for creating DataDetail data objects based on
  /// a DataColumns collection.
  /// </summary>
  public class DataDetailCode
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ControlColumnsHelperC/*' file='Doc/DataDetailCode.xml'/>
    public DataDetailCode()
    {
    }
    #endregion

    #region Create ControlColumn configuration.

    // Adjust the control for usability.
    /// <include path='items/AdjustedWidth/*' file='Doc/DataDetailCode.xml'/>
    public int AdjustedWidth(DbColumn dataColumn)
    {
      int minWidth = 4;
      int intWidth = 12;
      int retValue;

      switch (dataColumn.DataTypeName.ToLower())
      {
        case "string":
          retValue = dataColumn.MaxLength * ControlDetail.CharacterPixels;
          if (dataColumn.MaxLength > ControlDetail.MaxControlCharacters)
          {
            retValue = ControlDetail.MaxControlCharacters
              * ControlDetail.CharacterPixels;
          }
          break;

        case "boolean":
          retValue = dataColumn.Caption.Length * ControlDetail.CharacterPixels;
          if (dataColumn.Caption.Length > ControlDetail.MaxControlCharacters)
          {
            retValue = ControlDetail.MaxControlCharacters
              * ControlDetail.CharacterPixels;
          }
          break;

        case "int16":
        case "int32":
        case "int64":
          retValue = intWidth * ControlDetail.CharacterPixels;
          break;

        default:
          retValue = minWidth * ControlDetail.CharacterPixels;
          break;
      }
      return retValue;
    }

    // Gets the Contents display height.
    /// <include path='items/ContentHeight/*' file='Doc/DataDetailCode.xml'/>
    public int ContentHeight(int dataColumnsCount)
    {
      int rowCount;
      int retValue;

      // Top and bottom spacing.
      retValue = ControlDetail.BorderVertical * 2;

      // Get the actual row count.
      rowCount = ControlDetail.ColumnRowsLimit;
      if (dataColumnsCount < rowCount)
      {
        // Single page with less than full row count.
        rowCount = dataColumnsCount;
      }

      // Space between each ControlRow.
      retValue += ControlDetail.ControlRowSpacing * (rowCount - 1);

      // Height of each ControlRow.
      retValue += ControlDetail.ControlRowHeight * rowCount;
      return retValue;
    }

    // Gets the Contents display width.
    /// <include path='items/ContentWidth/*' file='Doc/DataDetailCode.xml'/>
    public int ContentWidth()
    {
      int currentWidth = 0;
      int retValue = 0;

      foreach (ControlTab controlTab in ControlDetail.ControlTabItems)
      {
        int count = controlTab.ControlColumns.Count;
        foreach (ControlColumn controlColumn in controlTab.ControlColumns)
        {
          // First column of controls for this page.
          if (0 == controlColumn.ColumnIndex % ControlDetail.PageColumnsLimit)
          {
            // Start with border before each ControlColumn and border after last.
            currentWidth = ControlDetail.BorderHorizontal * (count + 1);
          }

          // Add the ControlColumn width.
          currentWidth += controlColumn.Width;
          if (currentWidth > retValue)
          {
            // Save widest page.
            retValue = currentWidth;
          }
        }
      }
      return retValue;
    }

    // Gets the ControlRow type name.
    /// <include path='items/ControlRowType/*' file='Doc/DataDetailCode.xml'/>
    public string ControlRowType(DbColumn dataColumn, KeyItems keyItems)
    {
      string retValue = null;

      var isNext = true;
      KeyItems items;
      if (keyItems != null)
      {
        items = keyItems.SearchPropertyName(dataColumn.PropertyName);
        if (items != null && items.Count > 0)
        {
          isNext = false;
          if (1 == items.Count)
          {
            retValue = "StaticKey";
            if (NetString.HasValue(items[0].TableName))
            {
              retValue = "SelectList";
            }
          }
          else
          {
            retValue = "StaticCombo";
          }
        }
      }

      if (isNext)
      {
        retValue = "TextBox";
        if ("boolean" == dataColumn.DataTypeName.ToLower())
        {
          retValue = "CheckBox";
        }
      }
      return retValue;
    }

    // Gets the ControlColumn index from X.
    /// <include path='items/GetColumnIndex/*' file='Doc/DataDetailCode.xml'/>
    public int GetColumnIndex(int tabPageIndex, int x)
    {
      int retValue = -1;

      var config = ControlDetail;

      var controlTab = ControlDetail.ControlTabItems[tabPageIndex];

      int columnLeft = 0;
      int columnRight = 0;
      if (controlTab.ControlColumns != null)
      {
        foreach (ControlColumn controlColumn in controlTab.ControlColumns)
        {
          columnRight += config.BorderHorizontal + controlColumn.Width;
          if (x >= columnLeft && x <= columnRight)
          {
            retValue = controlColumn.ColumnIndex;
            break;
          }
          columnLeft = config.BorderHorizontal + columnRight;
        }
      }
      return retValue;
    }

    // Gets the ControlRow index from Y.
    /// <include path='items/GetRowIndex/*' file='Doc/DataDetailCode.xml'/>
    public int GetRowIndex(int y)
    {
      int retValue = 0;

      var config = ControlDetail;

      if (y > config.BorderVertical)
      {
        y -= config.BorderVertical;
        int rowHeight = config.ControlRowSpacing + config.ControlRowHeight;
        retValue = y / rowHeight;
      }
      return retValue;
    }

    // Configure the New controls.
    /// <include path='items/NewControlData/*' file='Doc/DataDetailCode.xml'/>
    public void NewControlData(DbColumns dataColumns, KeyItems keyItems)
    {
      if (NetCommon.HasItems(dataColumns))
      {
        int controlColumnsCount = CalculateColumnsCount(dataColumns.Count);
        NewControlColumns(controlColumnsCount, dataColumns);
        NewControlRows(dataColumns, keyItems);
      }

      // Calculate config values.
      ControlDetail.DataValueCount = 0;
      if (dataColumns != null)
      {
        ControlDetail.DataValueCount = dataColumns.Count;
      }
    }
    #endregion

    #region Private Methods

    // Gets the ControlColumn column count. 
    // <include path='items/CalculateColumnsCount/*' file='Doc/DataDetailCode.xml'/>
    private int CalculateColumnsCount(int dataColumnsCount)
    {
      int retValue = 1;

      // There are more data values than can fit in one ControlColumn.
      if (dataColumnsCount > ControlDetail.ColumnRowsLimit)
      {
        // Divide the data values into columns.
        retValue = dataColumnsCount / ControlDetail.ColumnRowsLimit;
        if (dataColumnsCount % ControlDetail.ColumnRowsLimit != 0)
        {
          // An extra row is left so add another column.
          retValue++;
        }

        // Evenly distribute rows.
        ControlDetail.ColumnRowsLimit = (dataColumnsCount / retValue);
        if (dataColumnsCount % retValue != 0)
        {
          ControlDetail.ColumnRowsLimit++;
        }
      }
      return retValue;
    }

    // Gets the current TabPage index.
    private int GetTabPageIndex(int columnIndex, int tabPageIndex
      , int tabbingIndex)
    {
      int retValue = tabPageIndex;

      // If column is on next tab page.
      if (0 == columnIndex % ControlDetail.PageColumnsLimit)
      {
        // If not the first tab page.
        if (tabbingIndex > 0)
        {
          // Go to the next tab page.
          retValue++;
        }
      }
      return retValue;
    }

    // Get labels and controls width for the specified start and stop
    // DataColumn indexes.
    private void GetWidths(DbColumns dataColumns, int startIndex, int stopIndex
      , out int labelsWidth, out int controlsWidth)
    {
      DbColumn dbColumn;
      int width;

      labelsWidth = 100;
      controlsWidth = 120;

      if (dataColumns != null && dataColumns.Count > 0)
      {
        // Only use data items for the current ControlColumn.
        for (int index = startIndex; index <= stopIndex; index++)
        {
          if (index > dataColumns.Count - 1)
          {
            break;
          }
          else
          {
            dbColumn = dataColumns[index];

            // Do not include if not AllowDisplay.

            if (dbColumn.Caption != null)
            {
              width = dbColumn.Caption.Length * ControlDetail.CharacterPixels;
              if (width > labelsWidth)
              {
                labelsWidth = width;
              }
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

    // Gets the ControlColumns object.
    private void NewControlColumns(int controlColumnsCount, DbColumns dataColumns)
    {
      ControlTab controlTab = null;

      if (null == ControlDetail.ControlTabItems)
      {
        ControlDetail.ControlTabItems = new ControlTabItems();
      }

      int tabPageIndex = 0;
      int prevTabIndex = -1;
      int currentColumnIndex = -1;
      for (int columnIndex = 0; columnIndex < controlColumnsCount; columnIndex++)
      {
        // Calculate ControlColumn values.
        int currentRowsCount = ControlDetail.ColumnRowsLimit;
        int startRowDataIndex = ControlDetail.ColumnRowsLimit * columnIndex;
        int endRowDataIndex = startRowDataIndex + (currentRowsCount - 1);

        tabPageIndex = GetTabPageIndex(columnIndex, tabPageIndex
          , startRowDataIndex);
        if (tabPageIndex != prevTabIndex)
        {
          // New Tab
          currentColumnIndex = 0;
          controlTab = new ControlTab()
          {
            Caption = $"Page {tabPageIndex + 1}",
            Description = $"Tab Page {tabPageIndex + 1}",
            ControlDetailID = ControlDetail.ID,
            TabIndex = tabPageIndex
          };
          controlTab.ChangedNames.Add(ControlTab.ColumnTabIndex, -1
            , tabPageIndex);
          controlTab = DataDetailData.SetControlTab(controlTab);
          ControlDetail.ControlTabItems.Add(controlTab);
          prevTabIndex = tabPageIndex;
        }

        currentRowsCount = endRowDataIndex - startRowDataIndex + 1;

        // More rows available for this ControlColumn than remaining data
        // elements. Set the end row to the number of data elements remaining.
        if (endRowDataIndex > dataColumns.Count - 1)
        {
          int excessRowCount = endRowDataIndex - (dataColumns.Count - 1);
          currentRowsCount -= excessRowCount;
          endRowDataIndex = dataColumns.Count - 1;
          if (0 == tabPageIndex && 0 == currentColumnIndex)
          {
            ControlDetail.ColumnRowCount = currentRowsCount;
          }
        }

        // get the ControlColumn widths.
        GetWidths(dataColumns, startRowDataIndex, endRowDataIndex
          , out int labelsWidth, out int controlsWidth);

        // Create and add the ControlColumn.
        ControlColumn controlColumn = new ControlColumn()
        {
          ControlTabID = controlTab.ID,
          ColumnIndex = currentColumnIndex,
          LabelsWidth = labelsWidth,
          ControlsWidth = controlsWidth,
          RowCount = currentRowsCount
        };
        DbCommon.AddChangedName(controlColumn, ControlColumn.ColumnColumnIndex);
        DataDetailData.AddControlColumn(controlColumn);
        currentColumnIndex++;

        // Add collection item.
        controlTab.ControlColumns.Add(controlColumn);
      }
    }

    // Creates a ControlRow DB and Collection item.
    private ControlRow NewControlRow(ControlColumn controlColumn
      , DbColumn dataColumn, int rowIndex, int tabbingIndex)
    {
      ControlRow retValue;

      var controlRows = controlColumn.ControlRows;

      // Create and add the ControlRow.
      retValue = new ControlRow()
      {
        ControlColumnID = controlColumn.ID,
        DataValueName = dataColumn.ColumnName,
        RowIndex = rowIndex,
        TabbingIndex = tabbingIndex,
        AllowDisplay = true
      };
      DbCommon.AddChangedName(retValue, ControlRow.ColumnRowIndex);
      DbCommon.AddChangedName(retValue, ControlRow.ColumnTabbingIndex);
      DataDetailData.AddControlRow(retValue);

      // Add collection item.
      controlRows.Add(retValue);
      return retValue;
    }

    // Creates the ControlRows DB and Collection data.
    private void NewControlRows(DbColumns dataColumns, KeyItems keyItems)
    {
      // Local references.
      var config = ControlDetail;

      if (NetCommon.HasItems(dataColumns))
      {
        foreach (ControlTab controlTab in config.ControlTabItems)
        {
          foreach (ControlColumn controlColumn in controlTab.ControlColumns)
          {
            int tabbingIndex = 0;

            // Create controls in each ControlColumn.
            int columnIndex = controlColumn.ColumnIndex;
            int startDataIndex = config.ColumnRowsLimit * columnIndex;
            int rowCount = controlColumn.RowCount;
            int endDataIndex = startDataIndex + (rowCount - 1);
            for (int dataIndex = startDataIndex; dataIndex < endDataIndex + 1
              ; dataIndex++)
            {
              if (dataIndex < dataColumns.Count)
              {
                int rowIndex = dataIndex - columnIndex * config.ColumnRowsLimit;
                DbColumn dataColumn = dataColumns[dataIndex];

                NewControlRow(controlColumn, dataColumn, rowIndex, tabbingIndex);
                string controlRowType = ControlRowType(dataColumn, keyItems);
                if (controlRowType != "CheckBox")
                {
                  tabbingIndex++;
                }
                tabbingIndex++;
              }
            }
          }
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets DetailConfig.</summary>
    public ControlDetail ControlDetail { get; set; }

    /// <summary>Gets or sets DataDetailData.</summary>
    public DataDetailData DataDetailData { get; set; }
    #endregion
  }
}
