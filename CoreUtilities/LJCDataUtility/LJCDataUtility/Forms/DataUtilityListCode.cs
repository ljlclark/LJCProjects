// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityListCode.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // The list form code.
  internal partial class DataUtilityList : Form
  {
    #region DataColumn value methods.

    // Gets the current Column Grid row.
    internal LJCGridRow DataColumnRow()
    {
      LJCGridRow retRow = ColumnGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long DataColumnID(LJCGridRow row = null)
    {
      long retColumnID = 0;

      if (null == row)
      {
        row = DataColumnRow();
      }
      if (row is LJCGridRow
        && "ColumnGrid" == row.DataGridView.Name)
      {
        retColumnID = row.LJCGetInt64(DataUtilColumn.ColumnID);
      }
      return retColumnID;
    }

    // Gets the selected row Name.
    internal string DataColumnName(LJCGridRow row = null)
    {
      string retColumnName = null;

      if (null == row)
      {
        row = DataColumnRow();
      }
      if (row is LJCGridRow
        && "ColumnGrid" == row.DataGridView.Name)
      {
        retColumnName = row.LJCGetString(DataUtilColumn.ColumnName);
      }
      return retColumnName;
    }

    // Gets the selected row SiteID.
    internal long DataColumnSiteID(LJCGridRow row = null)
    {
      long retColumnSiteID = 0;

      if (null == row)
      {
        row = DataColumnRow();
        if (null == row)
        {
          retColumnSiteID = Settings.SiteID;
        }
      }

      if (row is LJCGridRow
        && "ColumnGrid" == row.DataGridView.Name)
      {
        retColumnSiteID = row.LJCGetInt64(DataUtilColumn.ColumnDataSiteID);
      }
      return retColumnSiteID;
    }
    #endregion

    #region DataConfig value methods.

    // Gets the DataConfig object.
    internal DataConfig ComboDataConfig()
    {
      var configCombo = DataConfigCombo;
      var retConfig = configCombo.SelectedItem as DataConfig;
      return retConfig;
    }

    // Gets the DataConfig object.
    internal string ComboConfigValue(string propertyName)
    {
      string retValue = null;

      var dataConfig = ComboDataConfig();
      if (dataConfig != null)
      {
        switch (propertyName.ToLower())
        {
          case "connectiontype":
            retValue = dataConfig.ConnectionType;
            break;
          case "database":
            retValue = dataConfig.Database;
            break;
          case "dbserver":
            retValue = dataConfig.DbServer;
            break;
          case "name":
            retValue = dataConfig.Name;
            break;
          case "providername":
            retValue = dataConfig.GetProviderName();
            break;
          case "pswd":
            retValue = dataConfig.Pswd;
            break;
          case "userid":
            retValue = dataConfig.UserID;
            break;
        }
      }
      return retValue;
    }
    #endregion

    #region DataKey value methods.

    // Gets the current DataKey grid row.
    internal LJCGridRow DataKeyRow()
    {
      LJCGridRow retRow = KeyGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long DataKeyID(out long siteID, LJCGridRow row = null)
    {
      long retKeyID = 0;

      siteID = 0;
      if (null == row)
      {
        row = DataKeyRow();
      }
      if (row is LJCGridRow
        && "KeyGrid" == row.DataGridView.Name)
      {
        retKeyID = row.LJCGetInt64(DataKey.ColumnID);
        retKeyID = row.LJCGetInt64(DataKey.ColumnDataSiteID);
      }
      return retKeyID;
    }

    // Gets the selected row Name.
    internal string DataKeyName(LJCGridRow row = null)
    {
      string retKeyName = null;

      if (null == row)
      {
        row = DataKeyRow();
      }
      if (row is LJCGridRow
        && "KeyGrid" == row.DataGridView.Name)
      {
        retKeyName = row.LJCGetString(DataKey.ColumnName);
      }
      return retKeyName;
    }

    // Gets the selected row SiteID.
    internal long DataKeySiteID(LJCGridRow row = null)
    {
      long retKeySiteID = 0;

      if (null == row)
      {
        row = DataKeyRow();
        if (null == row)
        {
          retKeySiteID = Settings.SiteID;
        }
      }

      if (row is LJCGridRow
        && "KeyGrid" == row.DataGridView.Name)
      {
        retKeySiteID = row.LJCGetInt64(DataKey.ColumnDataSiteID);
      }
      return retKeySiteID;
    }

    /// <summary>Retrieve the Foreign keys.</summary>
    /// <returns>The foreign key collection.</returns>
    internal DataKeys ForeignKeys()
    {
      var tableID = DataTableID(out long siteID);
      var keyManager = Managers.DataKeyManager;
      var retKeys = keyManager.LoadWithType(tableID, siteID
        , (short)KeyType.Foreign);
      return retKeys;
    }

    /// <summary>Retrieve the Primary key list.</summary>
    /// <returns>The primary key values text.</returns>
    internal string PrimaryKeyValues()
    {
      string retList = null;

      var parentID = DataTableID(out long parentSiteID);
      var keyManager = Managers.DataKeyManager;
      var dataKey = keyManager.RetrieveWithType(parentID, parentSiteID
        , (short)KeyType.Primary);
      if (dataKey != null)
      {
        retList = dataKey.SourceColumnName;
      }
      return retList;
    }

    /// <summary>Retrieve the Unique key list.</summary>
    /// <returns>The unique key values text.</returns>
    internal string UniqueKeyValues()
    {
      string retList = null;

      var parentID = DataTableID(out long parentSiteID);
      var keyManager = Managers.DataKeyManager;
      var dataKey = keyManager.RetrieveWithType(parentID, parentSiteID
        , (short)KeyType.Unique);
      if (dataKey != null)
      {
        retList = dataKey.SourceColumnName;
      }
      return retList;
    }
    #endregion

    #region DataModule value methods.

    internal int DataModuleID(LJCItem item = null)
    {
      int retModuleID = 0;

      if (null == item)
      {
        item = ModuleCombo.SelectedItem as LJCItem;
      }
      if (item is LJCItem)
      {
        retModuleID = item.ID;
      }
      return retModuleID;
    }

    // Gets the selected row Name.
    internal string DataModuleName(LJCItem item = null)
    {
      string retModuleName = null;

      if (null == item)
      {
        item = ModuleCombo.SelectedItem as LJCItem;
      }
      if (item is LJCItem)
      {
        retModuleName = item.Text;
      }
      return retModuleName;
    }

    // Gets the selected row SiteID.
    internal long DataModuleSiteID(LJCItem item = null)
    {
      long retModuleSiteID = 0;

      if (null == item)
      {
        item = ModuleCombo.SelectedItem as LJCItem;
      }
      if (item is LJCItem)
      {
        retModuleSiteID = Settings.SiteID;
      }
      return retModuleSiteID;
    }
    #endregion

    #region DataTable value methods.

    internal LJCGridRow DataTableRow()
    {
      LJCGridRow retRow = TableGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long DataTableID(out long siteID, LJCGridRow row = null)
    {
      long retTableID = 0;

      siteID = 0;
      if (row == null)
      {
        row = DataTableRow();
      }
      if (row is LJCGridRow
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableID = row.LJCGetInt64(DataUtilTable.ColumnID);
        siteID = row.LJCGetInt64(DataUtilTable.ColumnDataSiteID);
      }
      return retTableID;
    }

    // Gets the target table ID.
    internal long ParentDataTableID(string targetTableName, out long siteID)
    {
      long retTableID = 0;

      siteID = 0;
      var moduleID = DataModuleID();
      var tableManager = Managers.DataTableManager;
      var targetTable = tableManager.RetrieveWithUnique(moduleID
        , targetTableName);
      if (targetTable != null)
      {
        retTableID = targetTable.ID;
        siteID = targetTable.DataSiteID;
      }
      return retTableID;
    }

    // Gets the selected row Name.
    internal string DataTableName(LJCGridRow row = null)
    {
      string retTableName = null;

      if (row == null)
      {
        row = DataTableRow();
      }
      if (row is LJCGridRow
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableName = row.LJCGetString(DataUtilTable.ColumnName);
      }
      return retTableName;
    }

    // Gets the selected row SiteID.
    internal long DataTableSiteID(LJCGridRow row = null)
    {
      long retTableSiteID = 0;

      if (row == null)
      {
        row = DataTableRow();
        if (null == row)
        {
          retTableSiteID = Settings.SiteID;
        }
      }

      if (row is LJCGridRow
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableSiteID = row.LJCGetInt64(DataUtilTable.ColumnDataSiteID);
      }
      return retTableSiteID;
    }
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Dpi)
      {
        MainSplit.SplitterWidth = 4;
        ljcHeaderBox1.Height = 30;

        ListHelper.SetPanelControls(MainSplit.Panel1, ljcHeaderBox1
          , null, TableGrid);
        //_ClassName_Grid.Height = ClientSize.Height - _ClassName_Tools.Height;

        //ListHelper.SetPanelControls(_ClassName_Split.Panel2, ChildHeading
        //	, ChildToolPanel, ChildGrid);
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      ColumnTabs.MouseDown += ColumnTabs_MouseDown;
      TileTabs.MouseDown += TileTabs_MouseDown;
      ColumnTabMove.Click += ColumnTabMove_Click;
      KeyTabMove.Click += KeyTabMove_Click;

      InitializeClassData();
      SetupControlCode();
      LoadControlData();
      ControlSetup();
      InitialControlValues();
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
      if (NetString.HasValue(errors))
      {
        MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      ConnectionType = values.ConnectionType;
      Managers = values.Managers;
      Settings = values.StandardSettings;
      Text += $" - {Settings.DataConfigName}";
    }

    // Setup the grid code references.
    private void SetupControlCode()
    {
      ModuleComboCode = new DataModuleComboCode(this);
      TableGridCode = new DataTableGridCode(this);
      ColumnGridCode = new DataColumnGridCode(this);
      KeyGridCode = new DataKeyGridCode(this);
    }

    // Loads the initial Control data.
    private void LoadControlData()
    {
      // Get DataConfigs
      mDataConfigs = new DataConfigs();
      mDataConfigs.LJCLoadData();
      foreach (DataConfig dataConfig in mDataConfigs)
      {
        DataConfigCombo.Items.Add(dataConfig);
      }
      if (DataConfigCombo.Items.Count > 0)
      {
        DataConfigCombo.SelectedIndex = 0;
      }
    }

    // Initial Control setup.
    private void ControlSetup()
    {
      // Provides additional Drag features between split LJCTabControls.
      var _ = new LJCPanelManager(ColumnsSplit, ColumnTabs, TileTabs);
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      ControlValuesFileName = @"ControlValues\DataUtility.xml";
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      TableGridCode.SetupGrid();
      ColumnGridCode.SetupGrid();
      KeyGridCode.SetupGrid();
    }
    #endregion

    #region Private Methods

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(ControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , ControlValuesFileName) as ControlValues;

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

          // Restore Grid column sizes.
          TableGrid.LJCRestoreColumnValues(ControlValues);
          ColumnGrid.LJCRestoreColumnValues(ControlValues);
          KeyGrid.LJCRestoreColumnValues(ControlValues);

          // Restore Font sizes.
          FormCommon.RestoreTabsFontSize(ColumnTabs, ControlValues);
          TableGrid.LJCRestoreFontSize(ControlValues);
          ColumnGrid.LJCRestoreFontSize(ControlValues);
          KeyGrid.LJCRestoreFontSize(ControlValues);

          // Restore Menu Font sizes.
          FormCommon.RestoreMenuFontSize(ModuleMenu, ControlValues);
          FormCommon.RestoreMenuFontSize(TableMenu, ControlValues);
          FormCommon.RestoreMenuFontSize(ColumnMenu, ControlValues);
          FormCommon.RestoreMenuFontSize(KeyMenu, ControlValues);

          FormCommon.RestoreSplitDistance(MainSplit, ControlValues);
          InfoValue = ControlValues.LJCSearchName("AddProc");
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      // Save Grid column sizes.
      TableGrid.LJCSaveColumnValues(controlValues);
      ColumnGrid.LJCSaveColumnValues(controlValues);
      KeyGrid.LJCSaveColumnValues(controlValues);

      // Save Font sizes.
      FormCommon.SaveTabFontSize(ColumnTabs, controlValues);
      TableGrid.LJCSaveFontSize(controlValues);
      ColumnGrid.LJCSaveFontSize(controlValues);
      KeyGrid.LJCSaveFontSize(controlValues);

      // Save Menu Font sizes.
      FormCommon.SaveMenuFontSize(ModuleMenu, controlValues);
      FormCommon.SaveMenuFontSize(TableMenu, controlValues);
      FormCommon.SaveMenuFontSize(ColumnMenu, controlValues);
      FormCommon.SaveMenuFontSize(KeyMenu, controlValues);

      controlValues.Add("MainSplit.SplitterDistance", 0, 0, 0
        , MainSplit.SplitterDistance);
      controlValues.Add(InfoValue);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , ControlValuesFileName);
    }

    // Sets the tab initial focus control.
    private void SetFocusTab(MouseEventArgs e)
    {
      var tabPage = ColumnTabs.LJCGetTabPage(e);
      switch (tabPage.Name)
      {
        case "ColumnPage":
          ColumnGrid.Select();
          break;
        case "KeyPage":
          KeyGrid.Select();
          break;
      }
    }
    #endregion

    #region Action Event Handlers

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void ColumnTabMove_Click(object sender, EventArgs e)
    {
      ColumnTabs.LJCMoveTabPageRight(TileTabs, ColumnsSplit);
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void KeyTabMove_Click(object sender, EventArgs e)
    {
      TileTabs.LJCMoveTabPageLeft(ColumnTabs, ColumnsSplit);
    }
    #endregion

    #region Control Event Handlers

    // Handles the MouseDown event.
    private void ColumnTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ColumnTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }
    #endregion

    #region Properties

    // Gets or sets the connection type value.
    internal string ConnectionType { get; set; }

    // Gets or sets the ControlValues file name.
    internal string ControlValuesFileName { get; set; }

    // Gets or sets the InfoValue item.
    internal ControlValue InfoValue { get; set; }

    // Gets or sets the Managers object.
    internal ManagersDataUtility Managers { get; set; }

    // Gets or sets the configuration settings.
    internal StandardUISettings Settings { get; set; }

    // Gets or sets the ModuleComboCode reference.
    private DataModuleComboCode ModuleComboCode { get; set; }

    // Gets or sets the DataColumnGridCode reference.
    private DataColumnGridCode ColumnGridCode { get; set; }

    // Gets or sets the KeyGridCode reference.
    private DataKeyGridCode KeyGridCode { get; set; }

    // Gets or sets the DataTableGridCode reference.
    private DataTableGridCode TableGridCode { get; set; }

    // Gets or sets the ControlValues object.
    private ControlValues ControlValues { get; set; }
    #endregion

    private DataConfigs mDataConfigs;
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
