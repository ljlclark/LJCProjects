using DataDetailDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataDetailLib
{
  /// <summary>Contains methods for creating ControlColumns.</summary>
  public class ControlColumnsHelper
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="managers">The Managers object.</param>
    public ControlColumnsHelper(DataDetailManagers managers)
    {
      mManagers = managers;
    }
    #endregion

    #region Create ControlColumn configuration.

    // Adjust the control for usability.
    /// <include path='items/AdjustedWidth/*' file='Doc/ControlColumnsHelper.xml'/>
    public int AdjustedWidth(DbColumn dataColumn)
    {
      int minWidth = 4;
      int intWidth = 12;
      int retValue;

      switch (dataColumn.DataTypeName.ToLower())
      {
        case "string":
          retValue = dataColumn.MaxLength * DetailConfig.CharacterPixels;
          if (dataColumn.MaxLength > DetailConfig.MaxControlCharacters)
          {
            retValue = DetailConfig.MaxControlCharacters
              * DetailConfig.CharacterPixels;
          }
          break;

        case "boolean":
          retValue = dataColumn.Caption.Length * DetailConfig.CharacterPixels;
          if (dataColumn.Caption.Length > DetailConfig.MaxControlCharacters)
          {
            retValue = DetailConfig.MaxControlCharacters
              * DetailConfig.CharacterPixels;
          }
          break;

        case "int16":
        case "int32":
        case "int64":
          retValue = intWidth * DetailConfig.CharacterPixels;
          break;

        default:
          retValue = minWidth * DetailConfig.CharacterPixels;
          break;
      }
      return retValue;
    }

    // Gets the Controls display height.
    /// <include path='items/Height/*' file='Doc/ControlColumnsHelper.xml'/>
    public int ControlsHeight(int dataColumnsCount)
    {
      int rowCount;
      int retValue;

      // Top and bottom spacing.
      retValue = DetailConfig.BorderVertical * 2;

      // Get the actual row count.
      rowCount = DetailConfig.ColumnRowsLimit;
      if (dataColumnsCount < rowCount)
      {
        rowCount = dataColumnsCount;
      }

      // Space between each ControlRow.
      retValue += DetailConfig.ControlRowSpacing * (rowCount - 1);

      // Height of each ControlRow.
      retValue += DetailConfig.ControlRowHeight * rowCount;
      return retValue;
    }

    // Gets the Controls display width.
    /// <include path='items/Width/*' file='Doc/ControlColumnsHelper.xml'/>
    public int ControlsWidth(ControlColumns controlColumns)
    {
      int currentWidth = 0;
      int retValue = 0;

      int count = controlColumns.Count;
      foreach (ControlColumn controlColumn in controlColumns)
      {
        // First column of controls for this page.
        if (0 == controlColumn.ColumnIndex % DetailConfig.PageColumnsLimit)
        {
          // Start with border before each ControlColumn and border after last.
          currentWidth = DetailConfig.BorderHorizontal * (count + 1);
          if (count > DetailConfig.PageColumnsLimit)
          {
            currentWidth = DetailConfig.BorderHorizontal
              * (DetailConfig.PageColumnsLimit + 1);
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

    // Sets the ControlColumn values using a configuration XML file.
    /// <include path='items/CreateConfigControlColumns/*' file='Doc/ControlColumnsHelper.xml'/>
    public ControlColumns CreateConfigControlColumns(DbColumns dataColumns)
    {
      DataDetailDAL.ControlColumns retValue = null;

      if (dataColumns != null && dataColumns.Count > 0
        && CheckDataColumnCount(dataColumns.Count))
      {
        //retValue = GetControlColumns(ControlRows.ControlColumnsCount
        //  , dataColumns);
        retValue = GetControlColumns(retValue.Count, dataColumns);
      }
      return retValue;
    }

    // Configure the New controls.
    /// <include path='items/CreateNewControlColumns/*' file='Doc/ControlColumnsHelper.xml'/>
    public ControlColumns CreateNewControlColumns(DbColumns dataColumns)
    {
      ControlColumns retValue = null;

      //ConfigRows = configRows;
      ControlColumns = new ControlColumns();
      if (dataColumns != null && dataColumns.Count > 0)
      {
        int controlColumnsCount = CalculateColumnsCount(dataColumns.Count);
        retValue = GetControlColumns(controlColumnsCount, dataColumns);
      }

      // Calculate config values.
      DetailConfig.ControlsWidth = ControlsWidth(retValue);

      DetailConfig.DataValueCount = 0;
      if (dataColumns != null)
      {
        DetailConfig.DataValueCount = dataColumns.Count;
        DetailConfig.ControlsHeight = ControlsHeight(dataColumns.Count);
      }
      // *** Next Statement *** Add - 9/6
      UpdateDetailConfig(mManagers.DetailConfigManager, DetailConfig);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Gets the ControlColumn column count. 
    /// <include path='items/CalculateColumnsCount/*' file='Doc/ControlColumnsHelper.xml'/>
    private int CalculateColumnsCount(int dataColumnsCount)
    {
      int retValue = 1;

      // There are more data values than can fit in one ControlColumn.
      if (dataColumnsCount > DetailConfig.ColumnRowsLimit)
      {
        // Divide the data values into columns.
        retValue = dataColumnsCount / DetailConfig.ColumnRowsLimit;
        if (dataColumnsCount % DetailConfig.ColumnRowsLimit != 0)
        {
          // An extra row is left so add another column.
          retValue++;
        }

        // Evenly distribute rows.
        DetailConfig.ColumnRowsLimit = (dataColumnsCount / retValue);
        if (dataColumnsCount % retValue != 0)
        {
          DetailConfig.ColumnRowsLimit++;
        }
      }
      return retValue;
    }

    // Check the DataValue value.
    private bool CheckDataColumnCount(int dataColumnsCount)
    {
      //string message;
      bool retValue = true;

      if (dataColumnsCount != DetailConfig.DataValueCount)
      {
        retValue = false;
        //message = $"DataValue count ({dataColumnsCount}) does not equal "
        //	+ "Configuration DataValue count ({ConfigRows.DataValueCount}).";
      }
      return retValue;
    }

    // Gets the ControlColumns object.
    private ControlColumns GetControlColumns(int controlColumnsCount, DbColumns dataColumns)
    {
      var retValue = new ControlColumns();

      int tabPageIndex = 0;
      for (int columnIndex = 0; columnIndex < controlColumnsCount; columnIndex++)
      {
        // Calculate ControlColumn values.
        int currentRowsCount = DetailConfig.ColumnRowsLimit;
        int startRowDataIndex = DetailConfig.ColumnRowsLimit * columnIndex;
        int endRowDataIndex = startRowDataIndex + (currentRowsCount - 1);
        tabPageIndex = GetTabPageIndex(columnIndex, tabPageIndex, startRowDataIndex);
        currentRowsCount = endRowDataIndex - startRowDataIndex + 1;

        // More rows available for this ControlColumn than remaining data
        // elements. Set the end row to the number of data elements remaining.
        if (endRowDataIndex > dataColumns.Count - 1)
        {
          int excessRowCount = endRowDataIndex - (dataColumns.Count - 1);
          currentRowsCount -= excessRowCount;
          endRowDataIndex = dataColumns.Count - 1;
        }

        // get the ControlColumn widths.
        GetWidths(dataColumns, startRowDataIndex, endRowDataIndex
          , out int labelsWidth, out int controlsWidth);

        // Create and add the ControlColumn.
        ControlColumn controlColumn = new ControlColumn()
        {
          DetailConfigID = DetailConfig.ID,
          TabPageIndex = tabPageIndex,
          ColumnIndex = columnIndex,
          LabelsWidth = labelsWidth,
          ControlsWidth = controlsWidth,
          //TabIndex = 0,
          RowCount = currentRowsCount
        };
        var propertyNames = new List<string>()
        {
          ControlColumn.ColumnDetailConfigID,
          ControlColumn.ColumnTabPageIndex,
          ControlColumn.ColumnColumnIndex,
          ControlColumn.ColumnLabelsWidth,
          ControlColumn.ColumnControlsWidth,
        };

        var columnManager = mManagers.ControlColumnManager;
        var addedItem = columnManager.Add(controlColumn, propertyNames);
        if (addedItem != null)
        {
          controlColumn.ID = addedItem.ID;
        }

        // Add collection item.
        retValue.Add(controlColumn);
      }
      return retValue;
    }

    // Gets the current TabPage index.
    private int GetTabPageIndex(int columnIndex, int tabPageIndex
      , int tabbingIndex)
    {
      int retValue = tabPageIndex;

      // If column is on next tab page.
      if (0 == columnIndex % DetailConfig.PageColumnsLimit)
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
              width = dbColumn.Caption.Length * DetailConfig.CharacterPixels;
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

    // 
    private void UpdateDetailConfig(DetailConfigManager configManager
      , DetailConfig dataObject)
    {
      if (dataObject.ID > 0)
      {
        var keyColumns = configManager.GetIDKey(dataObject.ID);

        configManager.Update(dataObject, keyColumns);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DetailConfig object.</summary>
    public DataDetailDAL.DetailConfig DetailConfig { get; set; }

    /// <summary>Gets or sets the ControlColumns collection.</summary>
    public DataDetailDAL.ControlColumns ControlColumns { get; set; }
    #endregion

    readonly DataDetailManagers mManagers;
  }
}
