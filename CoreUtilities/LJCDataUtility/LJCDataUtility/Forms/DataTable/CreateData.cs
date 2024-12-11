// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateData.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;

namespace LJCDataUtility
{
  // Generates the Create Data Procedure
  internal class CreateData
  {
    #region Constructors

    // Initializes an object instance.
    internal CreateData(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Managers = Parent.Managers;
    }
    #endregion

    #region Methods

    // Generates the Create data procedure.
    internal void CreateDataProc()
    {
      var tableName = Parent.DataTableName();
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
        var proc = new ProcBuilder("LJCDataUtility", null);
        proc.Begin("sp_DataTableData");
        proc.BodyBegin();

        var tableManager = Managers.DataTableManager;
        foreach (var data in dataColumns)
        {
          var dataTable = tableManager.RetrieveWithID(data.DataTableID);
          proc.Line($"EXEC sp_DataColumnAdd {dataTable.Name}");
          proc.Line($" , '{data.Name}', '{data.Description}'");
          proc.Text($" , {data.Sequence}, {data.TypeName}");
          proc.Text($", {data.IdentityStart}, {data.IdentityIncrement}");
          proc.Line($", {data.MaxLength}, {data.AllowNull}");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Generates the Create Key data procedure.
    private void KeyDataProc()
    {
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var proc = new ProcBuilder("LJCDataUtility", null);
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

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Generates the Create Module data procedure.
    private void ModuleDataProc()
    {
      var moduleManager = Managers.DataModuleManager;
      var dataTables = moduleManager.Load();
      if (NetCommon.HasItems(dataTables))
      {
        var proc = new ProcBuilder("LJCDataUtility", null);
        proc.Begin("sp_DataModuleData");
        proc.BodyBegin();

        foreach (var data in dataTables)
        {
          proc.Line("EXEC sp_DataModuleAdd");
          proc.Line($" '{data.Name}', '{data.Description}'");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Generates the Create Table data procedure.
    private void TableDataProc()
    {
      var tableManager = Managers.DataTableManager;
      var dataTables = tableManager.Load();
      if (NetCommon.HasItems(dataTables))
      {
        var proc = new ProcBuilder("LJCDataUtility", null);
        proc.Begin("sp_DataTableData");
        proc.BodyBegin();

        var moduleManager = Managers.DataModuleManager;
        foreach (var data in dataTables)
        {
          var dataModule = moduleManager.RetrieveWithID(data.DataModuleID);
          proc.Line($"EXEC sp_DataTableAdd {dataModule.Name}");
          proc.Line($" , '{data.Name}', '{data.Description}'");
        }
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
