// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RenameTable.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCNetCommon;

namespace LJCDataUtility
{
  // Provides methods to generate the RenameTable SQL.
  internal class RenameTable
  {
    #region Constructors

    // Initializes an object instance.
    internal RenameTable(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Config = ParentObject.DataConfigItem();
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the Rename Table SQL.
    internal void RenameTableSQL()
    {
      var parentID = ParentObject.DataTableRowID(out short parentDbID);
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
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
            var myProc = new MyProcBuilder(ParentObject, dbName
              , parentTableName);
            showText = myProc.RenameTableSQL(parentID, parentDbID);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            showText = proc.RenameTableSQL(parentID, parentDbID, dataKeys);
            break;
        }

        var infoValue = ParentObject.InfoValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(showText
          , "Rename Table SQL", infoValue);
        ParentObject.InfoValue = controlValue;
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
