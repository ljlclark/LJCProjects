// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTable.cs
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
        var dbName = ParentObject.DataConfigItemValue("Database");
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
            var myProc = new MyProcBuilder(ParentObject, dbName, parentTableName);
            showText = myProc.CreateTableProc(dataColumns);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            showText = proc.CreateTableProc(dataColumns);
            break;
        }

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , "Create Table Procedure", infoValue);
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
