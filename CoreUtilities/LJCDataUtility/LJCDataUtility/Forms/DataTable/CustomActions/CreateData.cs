// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateData.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;

namespace LJCDataUtility
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
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the CreateData procedure.
    internal void CreateDataProc()
    {
      var tableName = ParentObject.DataTableName();
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
      if (NetCommon.HasItems(dataColumns))
      {
        var dbName = ParentObject.ComboConfigValue("Database");

        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataColumnData");
        proc.BodyBegin();

        var tableManager = Managers.DataTableManager;
        foreach (var dataColumn in dataColumns)
        {
          var dataTable = tableManager.RetrieveWithID(dataColumn.DataTableID);
          proc.Line($"EXEC sp_DataColumnAdd {dataTable.Name}");
          proc.Line($" , '{dataColumn.Name}', '{dataColumn.Description}'");
          proc.Text($" , {dataColumn.Sequence}, {dataColumn.TypeName}");
          proc.Text($", {dataColumn.IdentityStart}, {dataColumn.IdentityIncrement}");
          proc.Line($", {dataColumn.MaxLength}, {dataColumn.AllowNull}");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Column Data Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }

    // Generates the Create Key data procedure.
    private void KeyDataProc()
    {
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var dbName = ParentObject.ComboConfigValue("Database");

        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataKeyData");
        proc.BodyBegin();

        var tableManager = Managers.DataTableManager;
        foreach (var data in dataKeys)
        {
          var dataTable = tableManager.RetrieveWithID(data.DataTableID);
          proc.Line($"EXEC sp_DataKeyAdd {dataTable.Name}");
          proc.Line($" , '{data.Name}', {data.KeyType}");
          proc.Text($" , {data.SourceColumnName}, {data.TargetTableName}");
          proc.Text($", {data.TargetColumnName}, {data.IsClustered}");
          proc.Line($", {data.IsAscending}");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Key Data Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }

    // Generates the Create Module data procedure.
    private void ModuleDataProc()
    {
      var moduleManager = Managers.DataModuleManager;
      var dataTables = moduleManager.Load();
      if (NetCommon.HasItems(dataTables))
      {
        var dbName = ParentObject.ComboConfigValue("Database");

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

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Module Data Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }

    // Generates the Create Table data procedure.
    private void TableDataProc()
    {
      var tableManager = Managers.DataTableManager;
      var dataTables = tableManager.Load();
      if (NetCommon.HasItems(dataTables))
      {
        var dbName = ParentObject.ComboConfigValue("Database");

        var proc = new ProcBuilder(ParentObject, dbName, null);
        proc.Begin("sp_DataTableData");
        proc.BodyBegin();

        var moduleManager = Managers.DataModuleManager;
        foreach (var dataTable in dataTables)
        {
          var dataModule = moduleManager.RetrieveWithID(dataTable.DataModuleID);
          if (dataModule != null)
          {
            proc.Line($"EXEC sp_DataTableAdd {dataModule.Name}");
            proc.Line($" , '{dataTable.Name}', '{dataTable.Description}'");
          }
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Table Data Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
