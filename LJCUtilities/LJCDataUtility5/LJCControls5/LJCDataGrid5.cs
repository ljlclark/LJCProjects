// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataGrid5.cs
using LJCNetCommon5;
using System.ComponentModel;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace LJCControls5
{
  // Provides custom functionality for a DataGridView control. (D)
  /// <include file='Doc/LJCDataGrid5.xml'
  ///  path='members/LJCDataGrid5/*'/>
  public partial class LJCDataGrid : DataGridView
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor1/*'/>
    public LJCDataGrid()
    {
      InitializeComponent();
      LJCSetPlain();
      BackgroundColor = Color.AliceBlue;
      LJCSetLastRow();

      LJCDragDataName = "";
      _Timer = new Timer
      {
        Interval = 200
      };
      _Timer.Tick += Timer_Tick;
    }

    // Initializes an object instance and adds it to a container.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/Constructor2/*'/>
    public LJCDataGrid(IContainer container) : this()
    {
      container.Add(this);
    }
    #endregion

    #region Row Data Methods

    // Clears the rows without allowing SelectionChange.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCRowsClear/*'/>
    public void LJCRowsClear()
    {
      LJCAllowSelectionChange = false;
      Rows.Clear();
      LJCAllowSelectionChange = true;
    }

    // Adds a GridRow control to the grid. 
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='members/LJCRowAdd/*'/>
    public LJCGridRow? LJCRowAdd()
    {
      LJCGridRow? retValue;

      retValue = new LJCGridRow();
      LJCAllowSelectionChange = false;
      var index = Rows.Add(retValue);
      LJCAllowSelectionChange = true;
      retValue = Rows[index] as LJCGridRow;

      // Create minimum height;
      if (retValue != null)
      {
        retValue.Height = LJCRowHeight;
        if (retValue.Height < Font.Height + 4)
        {
          retValue.Height = Font.Height + 4;
        }
        if (retValue.Height < 18)
        {
          retValue.Height = 18;
        }
      }
      return retValue;
    }

    // Inserts a GridRow control into the grid. 
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='members/LJCRowInsert/*'/>
    public LJCGridRow? LJCRowInsert(int index)
    {
      LJCGridRow? retValue;

      retValue = new LJCGridRow();
      Rows.Insert(index, retValue);
      retValue = Rows[index] as LJCGridRow;

      // Create minimum height;
      if (retValue != null)
      {
        retValue.Height = Font.Height + 4;
      }
      return retValue;
    }

    // Exports the grid values to a data file.
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='items/LJCExportData/*'/>
    public void LJCExportData(string fileName)
    {
      StringBuilder builder;
      string separator;
      string line;

      separator = "\t";
      if (LJC.IsEqual(".csv", Path.GetExtension(fileName)))
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

    #region Column Get Methods

    // Retrieves the column where the mouse was clicked.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseColumn/*'/>
    public DataGridViewColumn? LJCGetMouseColumn(MouseEventArgs e)
    {
      DataGridViewColumn? retValue = null;

      int columnIndex = LJCGetMouseColumnIndex(e);
      if (columnIndex >= 0)
      {
        retValue = Columns[columnIndex];
      }
      return retValue;
    }

    // Retrieves the column index where the mouse was clicked.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseColumnIndex/*'/>
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

    // Returns the current or first row.
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='members/LJCGetCurrentRow/*'/>
    public LJCGridRow? LJCGetCurrentRow()
    {
      if (null == CurrentRow
        && Rows.Count > 0)
      {
        LJCSetCurrentRow(Rows[0]);
      }
      var retValue = CurrentRow as LJCGridRow;
      return retValue;
    }

    // Retrieves the row for a DragOver or DragDrop event.
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='members/LJCGetDragRowIndex/*'/>
    public int LJCGetDragRowIndex(Point dragPoint)
    {
      int retValue;

      var adjustPoint = PointToClient(dragPoint);
      retValue = LJCGetMouseRowIndex(adjustPoint.X, adjustPoint.Y);
      return retValue;
    }

    // Gets the row at the cursor location.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseRow/*'/>
    public LJCGridRow? LJCGetMouseRow(int x, int y)
    {
      LJCGridRow? retValue = null;

      int rowIndex = LJCGetMouseRowIndex(x, y);
      if (rowIndex >= 0)
      {
        retValue = Rows[rowIndex] as LJCGridRow;
      }
      return retValue;
    }

    // Retrieves the row where the mouse was clicked
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseRow1/*'/>
    public LJCGridRow? LJCGetMouseRow(MouseEventArgs e)
    {
      LJCGridRow? retValue = null;

      int rowIndex = LJCGetMouseRowIndex(e);
      if (rowIndex >= 0)
      {
        retValue = Rows[rowIndex] as LJCGridRow;
      }
      return retValue;
    }

    // Retrieves the row index where the mouse was clicked.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseRowIndex/*'/>
    public int LJCGetMouseRowIndex(MouseEventArgs e)
    {
      var retValue = LJCGetMouseRowIndex(e.X, e.Y);
      return retValue;
    }

    // Retrieves the row index for the X and Y values.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseRowIndex1/*'/>
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
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetMouseCurrentRow/*'/>
    public void LJCSetCurrentRow(MouseEventArgs e
      , bool allowSelectionChange = false)
    {
      if (LJCGetMouseRow(e) is DataGridViewRow row)
      {
        LJCSetCurrentRow(row.Index, allowSelectionChange);
      }
    }

    // Sets the current row.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetCurrentRow1/*'/>
    public void LJCSetCurrentRow(DataGridViewRow row
      , bool allowSelectionChange = false)
    {
      if (row != null)
      {
        LJCSetCurrentRow(row.Index, allowSelectionChange);
      }
    }

    // Sets the current row to the specified index.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetCurrentRow2/*'/>
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
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCIsDifferentRow/*'/>
    public bool LJCIsDifferentRow(MouseEventArgs e)
    {
      bool retValue = false;

      var row = LJCGetMouseRow(e);
      if (row != null
        && row.Index != LJCLastRowIndex)
      {
        retValue = true;
        LJCLastRowIndex = row.Index;
      }
      return retValue;
    }

    // Saves the last selected row index.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetLastRow/*'/>
    public void LJCSetLastRow(LJCGridRow? row = null)
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

    // Sets the column width from the supplied character width value.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetColumnWidth/*'/>
    public static void LJCSetColumnWidth(DataGridViewColumn gridColumn, int textLength
      , int averageCapsWordSize = 0)
    {
      if (textLength < 5)
      {
        textLength = 5;
      }

      var padCount = 4;
      var calcLength = textLength + padCount;
      if (LJC.HasText(gridColumn.HeaderText)
        && gridColumn.HeaderText.Length > textLength)
      {
        calcLength = gridColumn.HeaderText.Length + padCount;
      }
      //var capsCount = 0;
      var capsCount = calcLength / 5;
      if (averageCapsWordSize > 5)
      {
        capsCount = calcLength / averageCapsWordSize;
      }
      var grid = gridColumn.DataGridView;
      if (grid != null)
      {
        gridColumn.Width = ControlCommon.TextUnitWidth(grid, calcLength
          , capsCount);
      }
    }

    // Adds a Checkbox column.
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='members/LJCAddCheckColumn/*'/>
    public DataGridViewColumn LJCAddCheckColumn(string name, string? caption = null)
    {
      DataGridViewColumn retValue;

      var checkColumn = new DataGridViewCheckBoxColumn
      {
        Name = name,
        ValueType = typeof(bool),
        ReadOnly = false,
        Width = 30
      };
      int columnIndex = Columns.Add(checkColumn);
      retValue = Columns[columnIndex];
      retValue.HeaderText = null;
      if (LJC.HasText(caption))
      {
        retValue.HeaderText = caption;
      }
      return retValue;
    }

    // Adds a column to the grid.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCAddColumn1/*'/>
    public DataGridViewColumn? LJCAddColumn(LJCDataColumn dataColumn)
    {
      DataGridViewColumn? retVal = null;

      if (dataColumn != null)
      {
        // Grid columns are named after the object property names.
        retVal = LJCAddColumn(dataColumn.PropertyName, dataColumn.Caption
          , dataColumn.MaxLength);
      }
      return retVal;
    }

    // Adds a grid column.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCAddColumn2/*'/>
    public DataGridViewColumn LJCAddColumn(string name, string? caption
      , int textLength = 0, int averageCapsWordSize = 0)
    {
      var columnIndex = Columns.Add(name, caption);
      var retGridColumn = Columns[columnIndex];
      retGridColumn.ReadOnly = true;

      LJCSetColumnWidth(retGridColumn, textLength, averageCapsWordSize);
      return retGridColumn;
    }

    // Adds grid columns.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCAddColumns/*'/>
    public void LJCAddColumns(LJCDataColumns dataColumns)
    {
      if (dataColumns != null)
      {
        foreach (LJCDataColumn dataColumn in dataColumns)
        {
          LJCAddColumn(dataColumn);
        }
        LJCSetLastColumnAutoSizeFill();
      }
    }

    // Restores the grid column values.
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='items/LJCRestoreColumnValues/*'/>
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
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='items/LJCSaveColumnValues/*'/>
    public void LJCSaveColumnValues(ControlValues controlValues)
    {
      foreach (DataGridViewColumn column in Columns)
      {
        string controlName = $"{Name}.{column.Name}";
        controlValues.Add(controlName, 0, 0, column.Width, 0);
      }
    }

    // Sets the last column AutoSizeMode to "Fill" if the columns width is less
    // than the grid width.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetLastColumnAutoSizeFill/*'/>
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
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCSetPlain/*'/>
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

    #region Control Event Handlers

    // The OnColumnWidthChanged event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnColumnWidthChanged/*'/>
    protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
    {
      base.OnColumnWidthChanged(e);
      _Timer.Start();
    }

    // Sets the initial NoFocus colors.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnCreateControl/*'/>
    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      SetNoFocus();
    }

    // Sets the focus colors.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnEnter/*'/>
    protected override void OnEnter(EventArgs e)
    {
      base.OnEnter(e);
      DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
      DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
    }

    // Sets the NoFocus colors.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnLeave/*'/>
    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      SetNoFocus();
    }

    // The OnResize event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnResize/*'/>
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
    private void Timer_Tick(object? sender, EventArgs e)
    {
      _Timer.Stop();
      LJCSetLastColumnAutoSizeFill();
    }
    #endregion

    #region DragDrop Event Handlers

    // The OnDragDrop event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnDragDrop/*'/>
    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      base.OnDragDrop(drgevent);

      SetDragOverBackground(null);
    }

    // The OnDragLeave event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnDragLeave/*'/>
    protected override void OnDragLeave(EventArgs e)
    {
      base.OnDragLeave(e);

      SetDragOverBackground(null);
    }

    // The OnDragOver event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnDragOver/*'/>
    protected override void OnDragOver(DragEventArgs dragEvent)
    {
      base.OnDragOver(dragEvent);

      dragEvent.Effect = DragDropEffects.None;

      if (dragEvent.Data != null
        && dragEvent.Data.GetDataPresent(typeof(LJCGridRow)))
      {
        var targetIndex = LJCGetDragRowIndex(new Point(dragEvent.X, dragEvent.Y));
        if (targetIndex >= 0 && targetIndex < RowCount)
        {
          if (dragEvent.Data.GetData(typeof(LJCGridRow))
            is LJCGridRow sourceRow)
          {
            var dragDataName = sourceRow.LJCGetString("DragDataName");
            if (dragDataName != null
              && dragDataName == LJCDragDataName)
            {
              if (Rows[targetIndex] is LJCGridRow targetRow
                && targetRow != sourceRow)
              {
                if (targetRow != null)
                {
                  SetDragOverBackground(targetRow);
                  dragEvent.Effect = DragDropEffects.Move;
                }
              }
            }
          }
        }
      }
    }

    // The OnMouseDown event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnMouseDown/*'/>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);

      if (e.Button == MouseButtons.Left)
      {
        if (LJCAllowDrag)
        {
          // Initializes the drag and drop values.
          _SourceRow = LJCGetMouseRow(e.X, e.Y);
          if (_SourceRow != null)
          {
            _IsDragStart = true;
            _DragStartBounds = CreateDragStartBounds(e.X, e.Y, 8, 6);
            _SourceRow.LJCSetString("DragDataName", LJCDragDataName);
          }
        }
      }
    }

    // The OnMouseMove event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnMouseMove/*'/>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (LJCAllowDrag)
      {
        // Starts the drag operation if the mouse moves outside
        // the drag start bounds.
        var mousePoint = new Point(e.X, e.Y);
        if (_IsDragStart
          && _SourceRow != null
          && _DragStartBounds.Contains(mousePoint) == false)
        {
          _IsDragStart = false;
          DoDragDrop(_SourceRow, DragDropEffects.Move);
        }
      }
    }

    // The OnMouseUp event method.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/OnMouseUp/*'/>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);

      // Reset the drag start flag.
      _IsDragStart = false;
    }

    // Creates a bounding rectangle to determine if the move operation should start.
    private static Rectangle CreateDragStartBounds(int x, int y, int width, int height)
    {
      Rectangle retVal;

      retVal = new Rectangle(x - (width / 2), y - (width / 2), width, height);
      return retVal;
    }

    // Sets the DragOver background.
    private void SetDragOverBackground(LJCGridRow? currentRow)
    {
      if (_PrevRow != null)
      {
        _PrevRow.DefaultCellStyle.BackColor = Color.White;
      }
      if (currentRow != null)
      {
        _PrevRow = currentRow;
        var color = Color.FromArgb(0xe0, 0xe8, 0xee);
        currentRow.DefaultCellStyle.BackColor = color;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the allow drag indicator.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCAllowDrag/*'/>
    [DefaultValue(false)]
    public bool LJCAllowDrag { get; set; }

    // Gets or sets the allow SelectionChange indicator.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCAllowSelectionChange/*'/>
    [Browsable(false)]
    public bool LJCAllowSelectionChange { get; set; }

    // Gets or sets the drag data name.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCDragDataName/*'/>
    [Browsable(false)]
    public string LJCDragDataName { get; set; }

    // The last changed row index.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCLastRowIndex/*'/>
    [Browsable(false)]
    public int LJCLastRowIndex { get; set; }

    // Gets or sets the Row Height value.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCRowHeight/*'/>
    [DefaultValue(18)]
    public int LJCRowHeight { get; set; }
    #endregion

    #region Class Data

    private readonly Timer _Timer;

    private Rectangle _DragStartBounds;
    private bool _IsDragStart;
    private LJCGridRow? _PrevRow;
    private LJCGridRow? _SourceRow;
    #endregion
  }
}
