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
      var parentTableID = ParentObject.DataTableID();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(parentTableID
        , orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        var tableName = ParentObject.DataTableName();
        string dbName = ParentObject.DataConfigCombo.Text;

        // *** Begin *** Add 1/29/25
        var connectionType = ParentObject.ConnectionType;
        if (!NetString.HasValue(connectionType))
        {
          // Testing
          connectionType = "MySQL";
        }

        string showText = null;
        switch (connectionType.ToLower())
        {
          case "mysql":
            var mySQLBuilder = new MyProcBuilder(ParentObject, dbName, tableName);
            showText = mySQLBuilder.CreateTableProc(dataColumns);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, tableName);
            showText = proc.CreateTableProc(dataColumns);
            break;
        }
        // *** End   *** Add 1/29/25

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
