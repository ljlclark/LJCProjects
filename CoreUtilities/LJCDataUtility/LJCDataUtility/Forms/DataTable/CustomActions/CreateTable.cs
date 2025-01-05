// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTable.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataUtility
{
  // Provides methods to generate the CreateTable procedure.
  internal class CreateTable
  {
    #region Constructors

    // Initializes an object instance.
    public CreateTable(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Managers = Parent.Managers;
    }
    #endregion

    #region Methods

    // Generates the CreateTable procedure.
    internal void CreateTableProc()
    {
      string dbName = "LJCDataUtility";
      var tableID = Parent.DataTableID();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , orderByNames);
      if (NetCommon.HasItems(dataColumns))
      {
        var tableName = Parent.DataTableName();
        var proc = new ProcBuilder(dbName, tableName);
        var primaryKeyList = "ID";
        var uniqueKeyList = "Name";
        var value = proc.CreateTableProc(dataColumns, primaryKeyList
          , uniqueKeyList);

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Create Table Procedure", infoValue);
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
