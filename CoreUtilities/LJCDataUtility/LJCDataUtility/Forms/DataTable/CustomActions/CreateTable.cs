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
      ParentList = parentList;
      Managers = ParentList.Managers;
    }
    #endregion

    #region Methods

    // Generates the CreateTable procedure.
    internal void CreateTableProc()
    {
      var parentTableID = ParentList.DataTableID();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(parentTableID
        , orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        // ToDo: Get DB name.
        string dbName = "LJCDataUtility";

        var tableName = ParentList.DataTableName();
        var proc = new ProcBuilder(dbName, tableName);

        string primaryKeyList = null;
        string uniqueKeyList = null;
        var keyManager = Managers.DataKeyManager;
        var dataKey = keyManager.RetrieveWithType(parentTableID
          , (short)KeyType.Primary);
        if (dataKey != null)
        {
          primaryKeyList = dataKey.SourceColumnName;
        }
        dataKey = keyManager.RetrieveWithType(parentTableID
          , (short)KeyType.Unique);
        if (dataKey != null)
        {
          uniqueKeyList = dataKey.SourceColumnName;
        }

        var value = proc.CreateTableProc(dataColumns, primaryKeyList
          , uniqueKeyList);

        var infoValue = ParentList.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Create Table Procedure", infoValue);
        ParentList.InfoValue = controlValue;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
