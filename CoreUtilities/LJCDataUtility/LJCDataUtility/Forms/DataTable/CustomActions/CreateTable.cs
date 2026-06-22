// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTable.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtility
{
  // Provides methods to generate the CreateTable procedure.
  internal class CreateTable
  {
    #region Constructors

    // Initializes an object instance.
    internal CreateTable(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Config = ParentObject.DataConfigItem();
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the CreateTable procedure.
    internal void CreateTableProc()
    {
      var parentID = ParentObject.DataTableRowID(out long parentSiteID);
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(parentID, parentSiteID
        , orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        var parentTableName = ParentObject.DataTableRowName();
        var dbName = Config.Database;
        var connectionType = Config.ConnectionType;
        if (!NetString.HasValue(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
        }

        string showText = null;
        switch (connectionType.ToLower())
        {
          case "mysql":
            var myProc = new MyProcBuilder(ParentObject, dbName, parentTableName);
            showText = myProc.CreateTableProc(dataColumns);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            showText = proc.CreateTableProc(dataColumns);
            break;
        }

        // Show CreateTable procedure script.
        var infoValue = ParentObject.InfoValue;
        var scriptWindow = new DataUtilityCommon();
        var controlValue = scriptWindow.ShowInfo(showText
          , "Create Table Procedure", infoValue);
        ParentObject.InfoValue = controlValue;

        // Execute CreateTable procedure script.
        if (scriptWindow.IsExecute)
        {
          var connectionString = Config.GetConnectionString();
          var providerName = Config.GetProviderName();
          DataAccess dataAccess = new DataAccess(connectionString, providerName);
          dataAccess.ExecuteScriptText(showText);
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the DataConfig value.
    private DataConfig Config { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
