// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCDataGrid.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;

namespace LJCWinFormControls
{
  // Provides custom functionality for a DataGridView control. (D)
  /// <include path='items/LJCDataGrid/*' file='Doc/LJCDataGrid.xml'/>
  public partial class LJCDataGrid : DataGridView
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LJCDataGrid()
    {
      InitializeComponent();
      LJCSetPlain();
      BackgroundColor = Color.AliceBlue;
      LJCSetLastRow();
    }

    // Initializes an object instance and adds it to a container.
    /// <include path='items/LJCDataGridC/*' file='Doc/LJCDataGrid.xml'/>
    public LJCDataGrid(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      LJCSetPlain();
      BackgroundColor = Color.AliceBlue;
      LJCSetLastRow();
    }
    #endregion

    ///// <summary>
    ///// The overridden OnSelectionChanged event.
    ///// </summary>
    ///// <param name="e">The event arguments.</param>
    //protected override void OnSelectionChanged(EventArgs e)
    //{
    //	base.OnSelectionChanged(e);
    //	//SetLastRow();
    //}

    #region Row Data Methods

    // Clears the rows without allowing SelectionChange.
    /// <include path='items/LJCRowsClear/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCRowsClear()
    {
      LJCAllowSelectionChange = false;
      Rows.Clear();
      LJCAllowSelectionChange = true;
    }

    // Adds a GridRow control to the grid. 
    /// <include path='items/LJCRowAdd/*' file='Doc/LJCDataGrid.xml'/>
    public LJCGridRow LJCRowAdd()
    {
      LJCGridRow retValue;
      int index;

      retValue = new LJCGridRow();
      LJCAllowSelectionChange = false;
      index = Rows.Add(retValue);
      LJCAllowSelectionChange = true;
      retValue = Rows[index] as LJCGridRow;

      // Create minimum height;
      retValue.Height = LJCRowHeight;
      if (retValue.Height < Font.Height + 4)
      {
        retValue.Height = Font.Height + 4;
      }
      if (retValue.Height < 18)
      {
        retValue.Height = 18;
      }
      return retValue;
    }

    // Inserts a GridRow control into the grid. 
    /// <include path='items/LJCRowInsert/*' file='Doc/LJCDataGrid.xml'/>
    public LJCGridRow LJCRowInsert(int index)
    {
      LJCGridRow retValue;

      retValue = new LJCGridRow();
      Rows.Insert(index, retValue);
      retValue = Rows[index] as LJCGridRow;

      // Create minimum height;
      retValue.Height = Font.Height + 4;

      return retValue;
    }

    // Updates a grid row with the record values.
    /// <include path='items/LJCRowSetValues/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCRowSetValues(LJCGridRow row, object record
      , DbColumns DisplayColumns = null)
    {
      LJCReflect reflect;
      string value;

      reflect = new LJCReflect(record);
      if (DisplayColumns != null)
      {
        // Attempt to populate the specified columns.
        foreach (DbColumn column in DisplayColumns)
        {
          // Grid columns are named after the object property names.
          value = GetPropertyValue(reflect, column.PropertyName);
          row.LJCSetCellText(column.PropertyName, value);
        }
      }
      else
      {
        // Attempt to populate all existing columns.
        foreach (DataGridViewColumn column in Columns)
        {
          // Use the existing column names which were set to the object property names.
          value = GetPropertyValue(reflect, column.Name);
          row.LJCSetCellText(column.Name, value);
        }
      }
    }

    // Gets the Data object property value.
    private string GetPropertyValue(LJCReflect reflect, string propertyName)
    {
      string retValue;

      Type propertyType = reflect.GetPropertyType(propertyName);
      if (propertyType != null
        && propertyType.FullName.Contains("DateTime"))
      {
        DateTime dateValue = reflect.GetDateTime(propertyName);
        retValue = GetUiDateString(dateValue);
      }
      else
      {
        retValue = reflect.GetString(propertyName);
      }
      return retValue;
    }

    // Format the date for display.
    private string GetUiDateString(DateTime dateTime)
    {
      string retVal = null;

      if (false == IsDbMinDate(dateTime))
      {
        retVal = dateTime.ToString("MM/dd/yyyy");
      }
      return retVal;
    }

    // Check for DB Minimum date or less.
    private static bool IsDbMinDate(DateTime dateTime)
    {
      bool retValue = false;
      if (dateTime.Year < 1753)
      {
        retValue = true;
      }
      if (1753 == dateTime.Year
        && 1 == dateTime.Month
        && 1 == dateTime.Day)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Row and Column Selection Methods

    ///<summary>Returns the current or first row.</summary>
    public LJCGridRow LJCGetCurrentRow()
    {
      LJCGridRow retValue;

      if (null == CurrentRow
        && Rows.Count > 0)
      {
        LJCSetCurrentRow(Rows[0]);
      }
      retValue = CurrentRow as LJCGridRow;
      return retValue;
    }

    // Retrieves the column index where the mouse was clicked.
    /// <include path='items/LJCGetMouseColumnIndex/*' file='Doc/LJCDataGrid.xml'/>
    public int LJCGetMouseColumnIndex(MouseEventArgs e)
    {
      int retValue = -1;

      HitTestInfo info = HitTest(e.X, e.Y);
      if (info.RowIndex >= 0
        && info.RowIndex < Rows.Count)
      {
        retValue = info.ColumnIndex;
      }
      return retValue;
    }

    // Retrieves the row index where the mouse was clicked.
    /// <include path='items/LJCGetMouseRowIndex/*' file='Doc/LJCDataGrid.xml'/>
    public int LJCGetMouseRowIndex(MouseEventArgs e)
    {
      int retValue;

      retValue = LJCGetMouseRowIndex(e.X, e.Y);
      return retValue;
    }

    // Retrieves the row index for the X and Y values.
    /// <summary>
    /// Retrieves the row index for the X and Y values.
    /// </summary>
    /// <param name="x">The mouse X value.</param>
    /// <param name="y">The mouse Y value.</param>
    /// <returns>The mouse row index.</returns>
    public int LJCGetMouseRowIndex(int x, int y)
    {
      int retValue = -1;

      HitTestInfo info = HitTest(x, y);
      if (info.RowIndex >= 0
        && info.RowIndex < Rows.Count)
      {
        retValue = info.RowIndex;
      }
      return retValue;
    }

    // Retrieves the column where the mouse was clicked.
    /// <include path='items/LJCGetMouseColumn/*' file='Doc/LJCDataGrid.xml'/>
    public DataGridViewColumn LJCGetMouseColumn(MouseEventArgs e)
    {
      DataGridViewColumn retValue = null;

      int columnIndex = LJCGetMouseColumnIndex(e);
      if (columnIndex >= 0)
      {
        retValue = Columns[columnIndex];
      }
      return retValue;
    }

    // Retrieves the row where the mouse was clicked
    /// <include path='items/LJCGetMouseRow/*' file='Doc/LJCDataGrid.xml'/>
    public LJCGridRow LJCGetMouseRow(MouseEventArgs e)
    {
      LJCGridRow retValue = null;

      int rowIndex = LJCGetMouseRowIndex(e);
      if (rowIndex >= 0)
      {
        retValue = Rows[rowIndex] as LJCGridRow;
      }
      return retValue;
    }

    // Sets the current row to the mouse row.
    /// <include path='items/LJCSetMouseCurrentRow/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetCurrentRow(MouseEventArgs e
      , bool allowSelectionChange = false)
    {
      if (LJCGetMouseRow(e) is DataGridViewRow row)
      {
        LJCSetCurrentRow(row.Index, allowSelectionChange);
      }
    }

    // Sets the current row.
    /// <include path='items/LJCSetCurrentRow1/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetCurrentRow(DataGridViewRow row
      , bool allowSelectionChange = false)
    {
      if (row != null)
      {
        LJCSetCurrentRow(row.Index, allowSelectionChange);
      }
    }

    // Sets the current row to the specified index.
    /// <include path='items/LJCSetCurrentRow2/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetCurrentRow(int rowIndex, bool allowSelectionChange = false)
    {
      if (Rows[rowIndex] is DataGridViewRow row)
      {
        LJCAllowSelectionChange = allowSelectionChange;
        CurrentCell = row.Cells[0];
      }
    }
    #endregion

    #region Row Selection Changed Methods

    // Compares the current row against the last selected row.
    /// <include path='items/LJCIsDifferentRow/*' file='Doc/LJCDataGrid.xml'/>
    public bool LJCIsDifferentRow(MouseEventArgs e)
    {
      bool retValue = false;

      if (LJCGetMouseRow(e) is LJCGridRow row
        && row.Index != LJCLastRowIndex)
      {
        retValue = true;
        LJCLastRowIndex = row.Index;
      }
      return retValue;
    }

    // Saves the last selected row index.
    /// <include path='items/LJCSetLastRow/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetLastRow(LJCGridRow row = null)
    {
      if (row != null)
      {
        LJCLastRowIndex = row.Index;
      }
      else
      {
        if (CurrentRow != null)
        {
          LJCLastRowIndex = CurrentRow.Index;
        }
        else
        {
          LJCLastRowIndex = -1;
        }
      }
    }
    #endregion

    #region Grid Configuration Methods

    // Sets the grid to a simple read-only grid.
    /// <include path='items/LJCSetPlain/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetPlain()
    {
      AllowUserToAddRows = false;
      AllowUserToDeleteRows = false;
      AllowUserToResizeRows = false;
      BackgroundColor = Color.White;
      EditMode = DataGridViewEditMode.EditOnEnter;
      MultiSelect = false;
      ReadOnly = false;
      RowHeadersVisible = false;
      SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      ShowCellToolTips = false;
    }

    // Adds the display columns to the grid.
    /// <include path='items/LJCAddDisplayColumns/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCAddDisplayColumns(DbColumns columns)
    {
      if (columns != null)
      {
        foreach (DbColumn column in columns)
        {
          LJCAddColumn(column);
        }
        LJCSetLastColumnAutoSizeFill();
      }
    }

    // Sets the last column AutoSizeMode to "Fill" if the columns width is less
    // than the grid width.
    /// <include path='items/LJCSetLastColumnAutoSizeFill/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetLastColumnAutoSizeFill()
    {
      int columnsWidth = 0;

      foreach (DataGridViewColumn gridColumn in Columns)
      {
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        columnsWidth += gridColumn.Width;
      }

      if (Columns.Count > 0)
      {
        DataGridViewColumn lastColumn = Columns[Columns.Count - 1];
        if (columnsWidth < Width)
        {
          lastColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        else
        {
          if (lastColumn.Width < 100)
          {
            lastColumn.Width = 100;
          }
        }
      }
    }

    // Adds a column to the grid.
    /// <include path='items/LJCAddColumn1/*' file='Doc/LJCDataGrid.xml'/>
    public DataGridViewColumn LJCAddColumn(DbColumn column)
    {
      DataGridViewColumn retVal = null;

      if (column != null)
      {
        int characterWidth = 5;
        if (column.MaxLength > 15)
        {
          characterWidth = (int)(column.MaxLength * 0.4);
        }

        // Grid columns are named after the object property names.
        retVal = LJCAddColumn(column.PropertyName, column.Caption, characterWidth);
      }
      return retVal;
    }

    // Adds a grid column.
    /// <include path='items/LJCAddColumn2/*' file='Doc/LJCDataGrid.xml'/>
    public DataGridViewColumn LJCAddColumn(string name, string caption
      , int characterWidth = 0)
    {
      int columnIndex;
      DataGridViewColumn retValue;

      columnIndex = Columns.Add(name, caption);
      retValue = Columns[columnIndex];
      retValue.ReadOnly = true;

      SetColumnWidth(retValue, characterWidth);
      return retValue;
    }

    // <summary>Sets the column width from the supplied character width value.</summary>
    private void SetColumnWidth(DataGridViewColumn gridColumn, int characterWidth)
    {
      Graphics graphics;
      SizeF headingSize;
      int headerLength;

      if (characterWidth < 5)
      {
        characterWidth = 5;
      }

      if (null == gridColumn.HeaderText)
      {
        gridColumn.Width = characterWidth * mAverageCharWidth;
      }
      else
      {
        headerLength = gridColumn.HeaderText.Length;
        if (characterWidth > headerLength)
        {
          gridColumn.Width = characterWidth * mAverageCharWidth;
        }
        else
        {
          if (headerLength < 20)
          {
            gridColumn.Width = headerLength * mAverageCharWidth;
          }
          else
          {
            // Size for large heading.
            graphics = CreateGraphics();
            headingSize = graphics.MeasureString(gridColumn.HeaderText, Font);
            gridColumn.Width = (int)headingSize.Width + (5 * mAverageCharWidth);
          }
        }
      }
    }

    // Adds a Checkbox column.
    /// <include path='items/LJCAddCheckColumn/*' file='Doc/LJCDataGrid.xml'/>
    public DataGridViewColumn LJCAddCheckColumn(string name, string caption = null)
    {
      DataGridViewColumn retValue;

      DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn
      {
        Name = name,
        ValueType = typeof(bool),
        ReadOnly = false,
        Width = 30
      };
      int columnIndex = Columns.Add(checkColumn);
      retValue = Columns[columnIndex];
      retValue.HeaderText = null;
      if (false == string.IsNullOrWhiteSpace(caption))
      {
        retValue.HeaderText = caption;
      }
      return retValue;
    }

    // Saves the grid column values.
    /// <include path='items/LJCSaveColumnValues/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSaveColumnValues(ControlValues controlValues)
    {
      foreach (DataGridViewColumn column in Columns)
      {
        string controlName = $"{Name}.{column.Name}";
        controlValues.Add(controlName, 0, 0, column.Width, 0);
      }
    }

    // Restores the grid column values.
    /// <include path='items/LJCRestoreColumnValues/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCRestoreColumnValues(ControlValues controlValues)
    {
      if (controlValues != null)
      {
        foreach (ControlValue controlValue in controlValues)
        {
          string[] items = controlValue.ControlName.Split(".".ToCharArray()
            , StringSplitOptions.RemoveEmptyEntries);
          if (items[0] == Name)
          {
            DataGridViewColumn column = Columns[items[1]];
            if (column != null)
            {
              column.Width = controlValue.Width;
            }
          }
        }
      }
    }
    #endregion

    #region Other Methods

    // Sets the grid count in the counter label.
    /// <include path='items/LJCSetCounter/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCSetCounter(Label counter)
    {
      int current = 0;
      int count = 0;

      if (CurrentRow != null)
      {
        current = CurrentRow.Index + 1;
        count = RowCount;
      }
      counter.Text = $"Row {current} of {count}";
    }

    // Exports the grid values to a data file.
    /// <include path='items/LJCExportData/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCExportData(string fileName)
    {
      StringBuilder builder;
      string separator;
      string line;

      separator = "\t";
      if (".csv" == Path.GetExtension(fileName).ToLower())
      {
        separator = ", ";
      }

      // Write heading line.
      builder = new StringBuilder(128);
      foreach (DataGridViewColumn column in Columns)
      {
        if (builder.Length > 0)
        {
          builder.Append(separator);
        }
        builder.Append($"{column.Name}");
      }
      builder.AppendLine();
      line = builder.ToString();
      File.WriteAllText(fileName, line);

      // Write data rows.
      //builder = new StringBuilder(128);
      foreach (DataGridViewRow row in Rows)
      {
        builder = new StringBuilder(128);
        for (int index = 0; index < row.Cells.Count; index++)
        {
          if (index > 0)
          {
            builder.Append(separator);
          }
          object value = row.Cells[index].Value;
          if (value != null)
          {
            builder.Append($"{row.Cells[index].Value}");
          }
        }
        builder.AppendLine();
        line = builder.ToString();
        File.AppendAllText(fileName, line);
      }

      FormCommon.ShellProgram(null, fileName);
    }
    #endregion

    #region Control Event Handlers

    /// <summary>Sets the initial NoFocus colors.</summary>
    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      SetNoFocus();
    }

    // The OnColumnWidthChanged event method.
    /// <include path='items/OnColumnWidthChanged/*' file='Doc/LJCDataGrid.xml'/>
    protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
    {
      base.OnColumnWidthChanged(e);
      if (null == mTimer)
      {
        mTimer = new Timer
        {
          Interval = 200
        };
        mTimer.Tick += Timer_Tick;
      }
      mTimer.Start();
    }

    // Sets the focus colors.
    /// <include path='items/OnEnter/*' file='Doc/LJCDataGrid.xml'/>
    protected override void OnEnter(EventArgs e)
    {
      base.OnEnter(e);
      DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
      DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
    }

    // Sets the NoFocus colors.
    /// <include path='items/OnLeave/*' file='Doc/LJCDataGrid.xml'/>
    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      SetNoFocus();
    }

    // The OnResize event method.
    /// <include path='items/OnResize/*' file='Doc/LJCDataGrid.xml'/>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      LJCSetLastColumnAutoSizeFill();
    }

    // Sets the Not Focused colors.
    private void SetNoFocus()
    {
      DefaultCellStyle.SelectionForeColor = Color.Black;
      DefaultCellStyle.SelectionBackColor = SystemColors.ControlLight;
    }

    // The timer event handler.
    private void Timer_Tick(object sender, EventArgs e)
    {
      mTimer.Stop();
      LJCSetLastColumnAutoSizeFill();
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Row Height value.</summary>
    [DefaultValue(18)]
    public int LJCRowHeight { get; set; }

    /// <summary>Gets or sets the allow SelectionChange indicator.</summary>
    [Browsable(false)]
    public bool LJCAllowSelectionChange { get; set; }

    /// <summary>The last changed row index.</summary>
    [Browsable(false)]
    public int LJCLastRowIndex { get; set; }
    #endregion

    #region Class Data

    private readonly int mAverageCharWidth = 8;
    private Timer mTimer;
    #endregion
  }
}
