// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SelectList.cs
using System;
using System.Windows.Forms;
using System.IO;
using LJCDBClientLib;
using LJCDBMessage;
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormCommon;

namespace DataDetail
{
  /// <summary>The Selection List window</summary>
  public partial class SelectList : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public SelectList()
    {
      InitializeComponent();
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void SelectList_Load(object sender, EventArgs e)
    {
      string errorText = MissingValues();
      if (NetString.HasValue(errorText))
      {
        throw new MissingMemberException(errorText);
      }
      else
      {
        InitializeControls();
      }
      CenterToScreen();
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
    private void DataRetrieve()
    {
      DbResult dbResult;

      Cursor = Cursors.WaitCursor;

      // Get the data.
      dbResult = mDataManager.Load();

      // Create the Grid Columns.
      if (DbResult.HasData(dbResult))
      {
        DbColumns gridColumns = dbResult.Columns.Clone();
        var idColumn = gridColumns.LJCSearchPropertyName("ID");
        gridColumns.Remove(idColumn);

        // Configure the grid.
        DataGrid.LJCRowsClear();
        DataGrid.LJCAddColumns(gridColumns);

        // Display the data.
        foreach (DbRow dbRow in dbResult.Rows)
        {
          var ljcGridRow = DataGrid.LJCRowAdd();
          ljcGridRow.LJCSetValues(DataGrid, dbRow.Values);
        }
      }

      Cursor = Cursors.Default;
    }
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

          // Load first control.
          DataRetrieve();
          DataGrid.LJCRestoreColumnValues(ControlValues);
          break;

        case Change.List:
          break;
      }
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      List
    }

    #region Item Change Support

    // Change Event Handler
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Gets or sets the ChangeTimer object.
    internal ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;

      // Initialize Class Data.
      mDataManager = new DataManager(LJCDbServiceRef, LJCDataConfigName, LJCTableName);

      InitialControlValues();
      if (mDataManager != null)
      {
        // Start Change processing.
        ChangeTimer = new ChangeTimer();
        ChangeTimer.ItemChange += ChangeTimer_ItemChange;
        ChangeTimer.DoChange(Change.Startup.ToString());
      }
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = $"ControlValues\\{LJCTableName}.xml";
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
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      DataGrid.LJCSaveColumnValues(controlValues);

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Gets or sets the ControlValues item.
    private ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Checks for missing class values.
    private string MissingValues()
    {
      string retValue = null;

      if (false == NetString.HasValue(LJCDataConfigName))
      {
        retValue += "Missing the DataConfigName value.\r\n";
      }

      if (null == LJCDbServiceRef)
      {
        retValue += "Missing the DbServiceRef value.\r\n";
      }

      if (false == NetString.HasValue(LJCPrimaryKeyName))
      {
        retValue += "Missing the PrimaryKeyName value.\r\n";
      }

      if (false == NetString.HasValue(LJCTableName))
      {
        retValue += "Missing the TableName value.\r\n";
      }
      return retValue;
    }
    #endregion

    #region Action Event Handlers

    // Performs the Close function.
    private void DataMenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataConfigName value.</summary>
    public string LJCDataConfigName { get; set; }

    /// <summary>Gets or sets the DbServiceRef value.</summary>
    public DbServiceRef LJCDbServiceRef { get; set; }

    /// <summary>Gets or sets the ID value.</summary>
    public int LJCID { get; set; }

    /// <summary>Gets or sets the PrimaryKeyName value.</summary>
    public string LJCPrimaryKeyName { get; set; }

    /// <summary>Gets or sets the TableName value.</summary>
    public string LJCTableName { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private DataManager mDataManager;
    #endregion
  }
}
