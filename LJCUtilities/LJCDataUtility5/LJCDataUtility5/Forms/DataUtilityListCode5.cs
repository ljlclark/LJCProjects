// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Form1Code.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  internal partial class DataUtilityList : Form
  {
    #region Control Item Value Methods
    #endregion

    #region Setup Methods

    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;

      // *** Testing ***
      ColumnsSplit.Panel2Collapsed = true;

      InitializeClassData();
      SetupControlCode();
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesDataUtility.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (LJC.HasText(errors))
      {
        MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      ConnectionType = values.ConnectionType;
      DbGroupId = values.DbGroupId;
      Managers = values.Managers;
      //Settings = values.StandardSettings;
      //Text += $" - {Settings.DataConfigName}";
      Text += $" - {values.DataConfigName}";
    }

    // Setup the grid code references.
    private void SetupControlCode()
    {
      //ModuleComboCode = new DataModuleComboCode(this);
      TableGridCode = new DataTableGridCode(this);
      //ColumnGridCode = new DataColumnGridCode(this);
      //KeyGridCode = new DataKeyGridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      TableGridCode.SetupGrid();
    }
    #endregion

    #region Properties

    internal short DbGroupId { get; set; }

    // Gets or sets the connection type value.
    internal string ConnectionType { get; set; } = null!;

    // Gets or sets the ControlValues file name.
    //internal string ControlValuesFileName { get; set; }

    // Gets or sets the InfoValue item.
    //internal ControlValue InfoValue { get; set; }

    // Gets or sets the Managers object.
    internal ManagersDataUtility Managers { get; set; } = null!;

    // Gets or sets the configuration settings.
    //internal LJCStandardUISettings Settings { get; set; }

    // Gets or sets the DataColumnGridCode reference.
    //private DataColumnGridCode ColumnGridCode { get; set; }

    // Gets or sets the DataTableGridCode reference.
    internal DataTableGridCode TableGridCode { get; set; } = null!;

    // Gets or sets the KeyGridCode reference.
    //private DataKeyGridCode KeyGridCode { get; set; }

    // Gets or sets the ModuleComboCode reference.
    //private DataModuleComboCode ModuleComboCode { get; set; }

    //private ControlValues ControlValues { get; set; }
    #endregion
  }

  /// <summary></summary>
  internal enum KeyType : short
  {
    Primary = 1,
    Unique,
    Foreign,
    Table
  }
}
