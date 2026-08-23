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

    //#region DataKey value methods.

    //// Gets the current DataKey grid row.
    //internal LJCGridRow? DataKeyRow()
    //{
    //  var retRow = KeyGrid.CurrentRow as LJCGridRow;
    //  return retRow;
    //}

    //// Gets the selected row ID.
    //internal long DataKeyRowId(out short dbId, LJCGridRow? row = null)
    //{
    //  long retKeyId = 0;

    //  dbId = 0;
    //  row ??= DataKeyRow();
    //  if (row != null
    //    && row.DataGridView != null
    //    && "KeyGrid" == row.DataGridView.Name)
    //  {
    //    retKeyId = row.LJCGetInt64(DataKey.ColumnId);
    //    dbId = row.LJCGetInt16(DataKey.ColumnDbId);
    //  }
    //  return retKeyId;
    //}

    //// Gets the selected row Name.
    //internal string? DataKeyRowName(LJCGridRow? row = null)
    //{
    //  string? retKeyName = null;

    //  row ??= DataColumnRow();
    //  if (row != null
    //    && row.DataGridView != null
    //    && "KeyGrid" == row.DataGridView.Name)
    //  {
    //    retKeyName = row.LJCGetString(DataKey.ColumnName);
    //  }
    //  return retKeyName;
    //}

    //// Retrieve the Foreign keys.
    //internal DataKeys? ForeignKeys()
    //{
    //  DataKeys? retKeys = null;

    //  var tableId = TableGridCode.RowId(out short tableDbId);
    //  var keyManager = Managers.DataKeyManager;
    //  var keys = keyManager.LoadWithParentType(tableDbId, tableId
    //    , (int)KeyType.Foreign);
    //  if (keys != null)
    //  {
    //    retKeys = keys;
    //  }
    //  return retKeys;
    //}

    //// Retrieve the Primary key column list.
    //internal string? PrimaryKeyList()
    //{
    //  string? retList = null;

    //  var tableId = TableGridCode.RowId(out short tableDbId);
    //  var keyManager = Managers.DataKeyManager;
    //  var dataKey = keyManager.RetrieveWithParentType(tableDbId, tableId
    //    , (int)KeyType.Primary);
    //  if (dataKey != null)
    //  {
    //    retList = dataKey.SourceColumnName;
    //  }
    //  return retList;
    //}

    //// Retrieve the Unique key column list.
    //internal string? UniqueKeyList()
    //{
    //  string? retList = null;

    //  long tableId = TableGridCode.RowId(out short tableDbId);
    //  var keyManager = Managers.DataKeyManager;
    //  var dataKey = keyManager.RetrieveWithParentType(tableDbId, tableId
    //    , (int)KeyType.Unique);
    //  if (dataKey != null)
    //  {
    //    retList = dataKey.SourceColumnName;
    //  }
    //  return retList;
    //}
    //#endregion
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
