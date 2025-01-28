// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddData.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataUtility
{
  // Provides methods to generate the AddData procedure.
  internal class AddData
  {
    #region Constructors

    // Initializes an object instance.
    internal AddData(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the AddData procedure.
    internal void AddDataProc()
    {
      var tableID = ParentObject.DataTableID();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        var tableName = ParentObject.DataTableName();
        string parentTableName = null;
        switch (tableName)
        {
          case "DataTable":
            parentTableName = "DataModule";
            break;

          case "DataColumn":
          case "DataKey":
            parentTableName = "DataUtilTable";
            break;

          case "DataEntry":
          case "DataEntrySite":
            parentTableName = "DataSite";
            break;
        }

        DataColumns parentColumns = null;
        if (tableName != "DataModule"
          && tableName != "DataSite")
        {
          parentColumns = Managers.TableDataColumns(tableID);
        }

        // ToDo: Get DB name.
        string dbName = ParentObject.DataConfigCombo.Text;
        var procData = new AddProcData(dbName, dataColumns
          , tableName, parentColumns, parentTableName);
        CreateAddProc(procData);
      }
    }

    // Generates the AddData procedure.
    private string CreateAddProc(AddProcData data)
    {
      string retString = null;

      if (data.TableColumns != null)
      {
        var proc = new ProcBuilder(ParentObject, data.DBName, data.TableName);
        proc.Begin(proc.AddProcName);

        // Parameters
        var parentFindColumnName = "Name";
        var parmFindName = "";

        // Include parent table.
        var isFirst = true;
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          // "@tableNameFindName"
          var typeValue = "nvarchar(60)";
          var findColumn
            = data.ParentColumns.LJCSearchUnique(parentFindColumnName);
          if (findColumn != null)
          {
            typeValue = findColumn.TypeName;
            if (findColumn.MaxLength > 0)
            {
              typeValue += $"({findColumn.MaxLength})";
            }
          }
          // *** Begin *** Change
          parmFindName = proc.SQLVarName(data.ParentTableName);
          parmFindName += parentFindColumnName;
          // *** End   *** Change
          proc.Text($"  {parmFindName} {typeValue}");
          isFirst = false;
        }

        var parameters = proc.Parameters(data.TableColumns
          , isFirst);
        proc.Line(parameters);

        proc.Line("AS");
        proc.Line("BEGIN");

        // Include Parent table.
        var parentIDColumnName = "ID";
        var varRefName = "";
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          // *** Begin *** Change
          varRefName = proc.SQLVarName(data.ParentTableName);
          varRefName += parentIDColumnName;
          // *** End   *** Change
          var line = proc.IFItem(data.ParentTableName
            , parentIDColumnName, parentFindColumnName
            , parmFindName);
          line += "\r\n";
          line += $"IF {varRefName} IS NOT NULL";
          proc.Line(line);
        }

        // Table
        proc.Line($"IF NOT EXISTS(SELECT ID FROM {data.TableName}");
        proc.Line(" WHERE Name = @name)");
        proc.Line($"  INSERT INTO {data.TableName}");

        // Column list (Parens, not NewName, no IDs).
        var insertList = proc.ColumnsList(data.TableColumns);
        proc.Line(insertList);

        // Values list.
        var valuesList
          = proc.ValuesList(data.TableColumns, varRefName);
        proc.Line(valuesList);

        proc.Line("END");

        retString = proc.ToString();
      }

      var infoValue = ParentObject.InfoValue;
      var controlValue = DataUtilityCommon.ShowInfo(retString
        , "Add Data Procedure", infoValue);
      ParentObject.InfoValue = controlValue;
      return retString;
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
