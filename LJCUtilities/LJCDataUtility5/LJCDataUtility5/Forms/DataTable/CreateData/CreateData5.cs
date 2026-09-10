// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateData5.cs
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  // Provides methods to generate the CreateData procedure.
  internal class CreateData
  {
    #region Constructors

    // Initializes an object instance.
    internal CreateData(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      var comboCode = ParentObject.ConfigComboCode;
      var config = comboCode.DataConfigItem();
      if (config != null)
      {
        Config = config;
      }
      TableGridCode = ParentObject.TableGridCode;
      Managers = ParentObject.Managers;
      TableManager = Managers.DataTableManager;
    }
    #endregion

    #region Properties

    // Gets or sets the DataConfig value.
    private LJCDataConfig Config { get; set; } = null!;

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Table grid code reference.
    private DataTableGridCode TableGridCode { get; set; }

    // Gets or sets the Table grid code reference.
    private DataTableManager TableManager { get; set; }
    #endregion

    #region Methods

    // Generates the CreateData procedure.
    internal void CreateDataProc()
    {
      var tableName = TableGridCode.RowName();
      switch (tableName)
      {
        case "DataModule":
          ModuleDataProc();
          break;

        case "DataTable":
          TableDataProc();
          break;

        case "DataColumn":
          ColumnDataProc();
          break;

        case "DataKey":
          KeyDataProc();
          break;
      }
    }

    // Generates the Create Column data procedure.
    private void ColumnDataProc()
    {
      var columnManager = Managers.DataColumnManager;
      var dataColumns = columnManager.Load();
      if (LJC.HasListItems(dataColumns))
      {
        string dbName = GetConfigDbName();
        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataColumnData");
        proc.BodyBegin();

        var tableManager = Managers.DataTableManager;
        foreach (DataUtilColumn dataColumn in dataColumns)
        {
          var dataTable = tableManager.RetrieveWithId(dataColumn.DataTableDbId
            , dataColumn.DataTableId);
          if (null == dataTable
            || !LJC.HasText(dataTable.Name))
          {
            continue;
          }
          proc.Line($"EXEC sp_DataColumnAdd {dataTable.Name}");
          proc.Line($" , '{dataColumn.Name}', '{dataColumn.Description}'");
          proc.Text($" , {dataColumn.Sequence}, {dataColumn.TypeName}");
          proc.Text($", {dataColumn.IdentityStart}, {dataColumn.IdentityIncrement}");
          proc.Line($", {dataColumn.MaxLength}, {dataColumn.AllowNull}");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoControlValue = ParentObject.InfoControlValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(value
          , "Column Data Procedure", infoControlValue);
        ParentObject.InfoControlValue = controlValue;
      }
    }

    // Gets the config db name.
    private string GetConfigDbName()
    {
      string retName = "";

      if (LJC.HasText(Config.Database))
      {
        retName = Config.Database;
      }
      return retName;
    }

    // Generates the Create Key data procedure.
    private void KeyDataProc()
    {
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (LJC.HasListItems(dataKeys))
      {
        string dbName = GetConfigDbName();
        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataKeyData");
        proc.BodyBegin();

        var tableManager = Managers.DataTableManager;
        foreach (var data in dataKeys)
        {
          var dataTable = tableManager.RetrieveWithId(data.DataTableDbId
            , data.DataTableId);
          if (null == dataTable
            || !LJC.HasText(dataTable.Name))
          {
            continue;
          }
          proc.Line($"EXEC sp_DataKeyAdd {dataTable.Name}");
          proc.Line($" , '{data.Name}', {data.KeyType}");
          proc.Text($" , {data.SourceColumnName}, {data.TargetTableName}");
          proc.Text($", {data.TargetColumnName}, {data.IsClustered}");
          proc.Line($", {data.IsAscending}");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoControlValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(value
          , "Key Data Procedure", infoValue);
        ParentObject.InfoControlValue = controlValue;
      }
    }

    // Generates the Create Module data procedure.
    private void ModuleDataProc()
    {
      var moduleManager = Managers.DataModuleManager;
      var dataTables = moduleManager.Load();
      if (LJC.HasListItems(dataTables))
      {
        string dbName = GetConfigDbName();
        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataModuleData");
        proc.BodyBegin();

        foreach (var dataTable in dataTables)
        {
          proc.Line("EXEC sp_DataModuleAdd");
          proc.Line($" '{dataTable.Name}', '{dataTable.Description}'");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoControlValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(value
          , "Module Data Procedure", infoValue);
        ParentObject.InfoControlValue = controlValue;
      }
    }

    // Generates the Create Table data procedure.
    private void TableDataProc()
    {
      var tableManager = Managers.DataTableManager;
      var dataTables = tableManager.Load();
      if (LJC.HasListItems(dataTables))
      {
        string dbName = GetConfigDbName();
        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataTableData");
        proc.BodyBegin();

        var moduleManager = Managers.DataModuleManager;
        foreach (var dataTable in dataTables)
        {
          var dataModule = moduleManager.RetrieveWithId(dataTable.DataModuleDbId
            , dataTable.DataModuleId);
          if (dataModule != null)
          {
            proc.Line($"EXEC sp_DataTableAdd {dataModule.Name}");
            proc.Line($" , '{dataTable.Name}', '{dataTable.Description}'");
          }
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoControlValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(value
          , "Table Data Procedure", infoValue);
        ParentObject.InfoControlValue = controlValue;
      }
    }
    #endregion
  }
}
