using LJCNetCommon5;
using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace LJCControls5
{
  public partial class LJCDataGrid5 : DataGridView
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataGrid5()
    {
      InitializeComponent();
      LJCSetPlain();
      BackgroundColor = Color.AliceBlue;
      _Timer = new Timer
      {
        Interval = 200
      };
      _Timer.Tick += Timer_Tick;
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
    #endregion

    #region Row Get Methods

    ///<summary>Returns the current or first row.</summary>
    public LJCGridRow5? LJCGetCurrentRow()
    {
      if (null == CurrentRow
        && Rows.Count > 0)
      {
        LJCSetCurrentRow(Rows[0]);
      }
      var retValue = CurrentRow as LJCGridRow5;
      return retValue;
    }

    // Gets the row at the cursor location.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='items/LJCGetMouseRow/*'/>
    public LJCGridRow5? LJCGetMouseRow(int x, int y)
    {
      LJCGridRow5? retValue = null;

      int rowIndex = LJCGetMouseRowIndex(x, y);
      if (rowIndex >= 0)
      {
        retValue = Rows[rowIndex] as LJCGridRow5;
      }
      return retValue;
    }

    // Retrieves the row where the mouse was clicked
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='items/LJCGetMouseRow1/*'/>
    public LJCGridRow5? LJCGetMouseRow(MouseEventArgs e)
    {
      LJCGridRow5? retValue = null;

      int rowIndex = LJCGetMouseRowIndex(e);
      if (rowIndex >= 0)
      {
        retValue = Rows[rowIndex] as LJCGridRow5;
      }
      return retValue;
    }

    // Retrieves the row index where the mouse was clicked.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCGetMouseRowIndex/*'/>
    public int LJCGetMouseRowIndex(MouseEventArgs e)
    {
      int retValue;

      retValue = LJCGetMouseRowIndex(e.X, e.Y);
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
    ///  path='items/LJCSetMouseCurrentRow/*'/>
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
    ///  path='items/LJCSetCurrentRow1/*'/>
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
    ///  path='items/LJCSetCurrentRow2/*'/>
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
    ///  path='items/LJCIsDifferentRow/*'/>
    public bool LJCIsDifferentRow(MouseEventArgs e)
    {
      bool retValue = false;

      var row = LJCGetMouseRow(e);
      //if (LJCGetMouseRow(e) is LJCGridRow row
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
    ///  path='items/LJCSetLastRow/*'/>
    public void LJCSetLastRow(LJCGridRow5? row = null)
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
        gridColumn.Width = ControlCommon5.TextUnitWidth(grid, calcLength
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

    #region Properties

    // Gets or sets the allow SelectionChange indicator.
    [Browsable(false)]
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCAllowSelectionChange/*'/>
    public bool LJCAllowSelectionChange { get; set; }

    // The last changed row index.
    /// <include file='Doc/LJCDataGrid5.xml'
    ///  path='members/LJCLastRowIndex/*'/>
    [Browsable(false)]
    public int LJCLastRowIndex { get; set; }
    #endregion

    #region Class Data

    private readonly Timer _Timer;
    #endregion
  }
}
