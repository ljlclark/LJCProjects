// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RenameTable.cs
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
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the Rename Table SQL.
    internal void RenameTableSQL()
    {
      var parentID = ParentObject.DataTableRowID(out long parentSiteID);
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var parentTableName = ParentObject.DataTableRowName();
        string dbName = ParentObject.DataConfigCombo.Text;

        var connectionType = ParentObject.DataConfigItemValue("ConnectionType");
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
            showText = myProc.RenameTableSQL(parentID, parentSiteID);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            showText = proc.RenameTableSQL(parentID, parentSiteID, dataKeys);
            break;
        }

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , "Rename Table SQL", infoValue);
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
