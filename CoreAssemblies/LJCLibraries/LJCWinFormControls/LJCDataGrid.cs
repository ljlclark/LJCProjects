// Copyright(c) Lester J. Clark and Contributors.
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
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
    #endregion

    #region Column Get Methods

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
    #endregion

    #region Row Get Methods

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

    // Retrieves the row for a DragOver or DragDrop event.
    /// <include path='items/LJCGetDragRowIndex/*' file='Doc/LJCDataGrid.xml'/>
    public int LJCGetDragRowIndex(Point dragPoint)
    {
      int retValue;

      var adjustPoint = PointToClient(dragPoint);
      retValue = LJCGetMouseRowIndex(adjustPoint.X, adjustPoint.Y);
      return retValue;
    }

    // Gets the row at the cursor location.
    /// <include path='items/LJCGetMouseRow/*' file='Doc/LJCDataGrid.xml'/>
    public LJCGridRow LJCGetMouseRow(int x, int y)
    {
      LJCGridRow retValue = null;

      int rowIndex = LJCGetMouseRowIndex(x, y);
      if (rowIndex >= 0)
      {
        retValue = Rows[rowIndex] as LJCGridRow;
      }
      return retValue;
    }

    // Retrieves the row where the mouse was clicked
    /// <include path='items/LJCGetMouseRow1/*' file='Doc/LJCDataGrid.xml'/>
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

    // Retrieves the row index where the mouse was clicked.
    /// <include path='items/LJCGetMouseRowIndex/*' file='Doc/LJCDataGrid.xml'/>
    public int LJCGetMouseRowIndex(MouseEventArgs e)
    {
      int retValue;

      retValue = LJCGetMouseRowIndex(e.X, e.Y);
      return retValue;
    }

    // Retrieves the row index for the X and Y values.
    /// <include path='items/LJCGetMouseRowIndex1/*' file='Doc/LJCDataGrid.xml'/>
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
    #endregion

    #region Row Set Methods

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

    // Adds a column to the grid.
    /// <include path='items/LJCAddColumn1/*' file='Doc/LJCDataGrid.xml'/>
    public DataGridViewColumn LJCAddColumn(DbColumn column)
    {
      DataGridViewColumn retVal = null;

      if (column != null)
      {
        // Grid columns are named after the object property names.
        retVal = LJCAddColumn(column.PropertyName, column.Caption
          , column.MaxLength);
      }
      return retVal;
    }

    // Adds a grid column.
    /// <include path='items/LJCAddColumn2/*' file='Doc/LJCDataGrid.xml'/>
    public DataGridViewColumn LJCAddColumn(string name, string caption
      , int textLength = 0, int averageCapsWordSize = 0)
    {
      int columnIndex;
      DataGridViewColumn retValue;

      columnIndex = Columns.Add(name, caption);
      retValue = Columns[columnIndex];
      retValue.ReadOnly = true;

      LJCSetColumnWidth(retValue, textLength, averageCapsWordSize);
      return retValue;
    }

    // Adds grid columns.
    /// <include path='items/LJCAddColumns/*' file='Doc/LJCDataGrid.xml'/>
    public void LJCAddColumns(DbColumns columns)
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

    // Sets the column width from the supplied character width value.
    /// <summary>
    /// Sets the column width from the supplied character width value.
    /// </summary>
    /// <param name="gridColumn"></param>
    /// <param name="textLength"></param>
    /// <param name="averageCapsWordSize"></param>
    public void LJCSetColumnWidth(DataGridViewColumn gridColumn, int textLength
      , int averageCapsWordSize = 0)
    {
      if (textLength < 5)
      {
        textLength = 5;
      }

      var calcLength = textLength;
      if (NetString.HasValue(gridColumn.HeaderText)
        && gridColumn.HeaderText.Length > textLength)
      {
        calcLength = gridColumn.HeaderText.Length;
      }
      var capsCount = 0;
      if (averageCapsWordSize > 0)
      {
        capsCount = calcLength / averageCapsWordSize;
      }
      var grid = gridColumn.DataGridView;
      gridColumn.Width = ControlCommon.TextUnitWidth(grid, calcLength
        , capsCount);
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
    #endregion

    #region Other Methods

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
    #endregion

    #region Control Event Handlers

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

    /// <summary>Sets the initial NoFocus colors.</summary>
    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      SetNoFocus();
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

    #region DragDrop Event Handlers

    /// <summary>
    /// 
    /// </summary>
    /// <param name="drgevent"></param>
    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      base.OnDragDrop(drgevent);

      SetDragOverBackground(null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnDragLeave(EventArgs e)
    {
      base.OnDragLeave(e);

      SetDragOverBackground(null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="drgevent"></param>
    protected override void OnDragOver(DragEventArgs drgevent)
    {
      base.OnDragOver(drgevent);

      drgevent.Effect = DragDropEffects.None;

      if (drgevent.Data.GetDataPresent(typeof(LJCGridRow)))
      {
        var targetIndex = LJCGetDragRowIndex(new Point(drgevent.X, drgevent.Y));
        if (targetIndex >= 0 && targetIndex < RowCount)
        {
          var sourceRow = drgevent.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
          var dragDataName = sourceRow.LJCGetString("DragDataName");
          if (dragDataName != null
            && dragDataName == LJCDragDataName)
          {
            if (Rows[targetIndex] is LJCGridRow targetRow
              && targetRow != sourceRow)
            {
              SetDragOverBackground(targetRow);
              drgevent.Effect = DragDropEffects.Move;
            }
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);

      if (e.Button == MouseButtons.Left)
      {
        if (LJCAllowDrag)
        {
          // Initializes the drag and drop values.
          mSourceRow = LJCGetMouseRow(e.X, e.Y);
          if (mSourceRow != null)
          {
            mIsDragStart = true;
            mDragStartBounds = CreateDragStartBounds(e.X, e.Y, 8, 6);
            mSourceRow.LJCSetString("DragDataName", LJCDragDataName);
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (LJCAllowDrag)
      {
        // Starts the drag operation if the mouse moves outside
        // the drag start bounds.
        Point mousePoint = new Point(e.X, e.Y);
        if (mIsDragStart
          && mSourceRow != null
          && mDragStartBounds.Contains(mousePoint) == false)
        {
          mIsDragStart = false;
          DoDragDrop(mSourceRow, DragDropEffects.Move);
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);

      // Reset the drag start flag.
      mIsDragStart = false;
    }

    // Creates a bounding rectangle to determine if the move operation should start.
    private Rectangle CreateDragStartBounds(int x, int y, int width, int height)
    {
      Rectangle retVal;

      retVal = new Rectangle(x - (width / 2), y - (width / 2), width, height);
      return retVal;
    }

    // Sets the DragOver background.
    private void SetDragOverBackground(LJCGridRow currentRow)
    {
      if (mPrevRow != null)
      {
        mPrevRow.DefaultCellStyle.BackColor = Color.White;
      }
      if (currentRow != null)
      {
        mPrevRow = currentRow;
        var color = Color.FromArgb(0xe0, 0xe8, 0xee);
        currentRow.DefaultCellStyle.BackColor = color;
      }
    }
    #endregion

    #region Properties

    /// <summary></summary>
    [DefaultValue(false)]
    public bool LJCAllowDrag { get; set; }

    /// <summary>Gets or sets the allow SelectionChange indicator.</summary>
    [Browsable(false)]
    public bool LJCAllowSelectionChange { get; set; }

    /// <summary></summary>
    [Browsable(false)]
    public string LJCDragDataName { get; set; }

    /// <summary>The last changed row index.</summary>
    [Browsable(false)]
    public int LJCLastRowIndex { get; set; }

    /// <summary>Gets or sets the Row Height value.</summary>
    [DefaultValue(18)]
    public int LJCRowHeight { get; set; }
    #endregion

    #region Class Data

    private Timer mTimer;

    private Rectangle mDragStartBounds;
    private bool mIsDragStart;
    private LJCGridRow mPrevRow;
    private LJCGridRow mSourceRow;
    #endregion
  }
}
