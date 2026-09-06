// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTable5.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
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
      DataConfig = ParentObject.ConfigCombo.SelectedItem as LJCDataConfig;

      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the CreateTable procedure.
    internal void CreateTableProc()
    {
      var tableGridCode = ParentObject.TableGridCode;
      var parentId = tableGridCode.RowId(out short parentDbId);
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(parentDbId, parentId
        , orderByNames);

      while (true)
      {
        if (null == DataConfig
          || !LJC.HasListItems(dataColumns))
        {
          break;
        }

        var parentTableName = tableGridCode.RowName();
        var connectionType = DataConfig.ConnectionType;
        if (!LJC.HasText(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
        }

        var dbName = DataConfig.Database;
        if (!LJC.HasText(dbName))
        {
          break;
        }

        string? procText = null;
        switch (connectionType.ToLower())
        {
          case "mysql":
            //var myProc = new MyProcBuilder(ParentObject, dbName, parentTableName);
            //procText = myProc.CreateTableProc(dataColumns);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            procText = proc.CreateTableProc(dataColumns);
            break;
        }

        // Show CreateTable procedure script.
        if (!LJC.HasText(procText))
        {
          break;
        }

        var infoValue = ParentObject.InfoValue;
        var scriptWindow = new ShowInfoDialog(DataConfig);
        var controlValue = scriptWindow.ShowInfo(procText
          , "Create Table Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
        break;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the DataConfig value.
    private LJCDataConfig? DataConfig { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
