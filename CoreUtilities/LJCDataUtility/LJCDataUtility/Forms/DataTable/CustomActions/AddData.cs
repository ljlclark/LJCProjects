// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddData.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        // Get DataConfig
        var configCombo = ParentObject.DataConfigCombo;
        var dataConfig = configCombo.SelectedItem as DataConfig;
        var dbName = dataConfig.Database;

        var procData = new AddProcData(dbName, dataColumns
          , tableName, parentColumns, parentTableName);

        var connectionType = ParentObject.ConnectionType;
        if (!NetString.HasValue(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
        }

        // Testing
        if (DialogResult.Yes == MessageBox.Show("Use MySQL?", "MySQL"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          connectionType = "MySQL";
        }

        switch (connectionType.ToLower())
        {
          case "mysql":
            MySQLAddProc(procData);
            break;

          case "sqlserver":
            CreateAddProc(procData);
            break;
        }
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
        // ToDo: Get data.
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
            = data.ParentColumns.LJCSearchName(parentFindColumnName);
          if (findColumn != null)
          {
            typeValue = findColumn.TypeName;
            if (findColumn.MaxLength > 0)
            {
              typeValue += $"({findColumn.MaxLength})";
            }
          }
          parmFindName = proc.SQLVarName(data.ParentTableName);
          parmFindName += parentFindColumnName;
          proc.Text($"  {parmFindName} {typeValue}");
          isFirst = false;
        }

        var parameters = proc.Parameters(data.TableColumns
          , isFirst);
        proc.Line(parameters);

        proc.Line("AS");
        proc.Line("BEGIN");

        // Include Parent table.
        // ToDo: Get data.
        var parentIDColumnName = "ID";
        var varRefName = "";
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          varRefName = proc.SQLVarName(data.ParentTableName);
          varRefName += parentIDColumnName;
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

    private string MySQLAddProc(AddProcData data)
    {
      string retString = null;

      if (data.TableColumns != null)
      {
        var myProc = new MyProcBuilder(ParentObject, data.DBName, data.TableName);
        myProc.Begin(myProc.AddProcName);

        // Parameters
        // ToDo: Get data.
        var parentFindColumnName = "Code";
        var parmFindName = "";

        // Include parent table.
        var isFirst = true;
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          // "@tableNameFindName"
          var typeValue = "nvarchar(5)";
          var findColumn
            = data.ParentColumns.LJCSearchName(parentFindColumnName);
          if (findColumn != null)
          {
            typeValue = findColumn.TypeName;
            if (findColumn.MaxLength > 0)
            {
              typeValue += $"({findColumn.MaxLength})";
            }
          }
          parmFindName = myProc.SQLVarName(data.ParentTableName);
          parmFindName += parentFindColumnName;
          // *** Next Line *** Change 2/16/25
          myProc.Text($"  `{parmFindName}` {typeValue}");
          isFirst = false;
        }

        var parameters = myProc.Parameters(data.TableColumns
          , isFirst);
        myProc.Line(parameters);

        myProc.Line(")");
        myProc.Line("BEGIN");

        // Include Parent table.
        // ToDo: Get data.
        var parentIDColumnName = "ID";
        var varRefName = "";
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          varRefName = myProc.SQLVarName(data.ParentTableName);
          varRefName += parentIDColumnName;
          var line = myProc.IFItem(data.ParentTableName
            , parentIDColumnName, parentFindColumnName
            , parmFindName);
          line += "\r\n";
          line += $"IF {varRefName} IS NOT NULL";
          myProc.Line(line);
        }

        // Table
        myProc.Line($"IF NOT EXISTS(SELECT 1 FROM `{data.TableName}`");
        myProc.Line(" WHERE Name = @name) THEN");
        myProc.Line($"  INSERT INTO `{data.TableName}`");

        // Column list (Parens, not NewName, no IDs).
        var insertList = myProc.ColumnsList(data.TableColumns);
        myProc.Line(insertList);

        // Values list.
        var valuesList
          = myProc.ValuesList(data.TableColumns, varRefName);
        myProc.Line(valuesList);
        myProc.Line("END IF;");

        myProc.Line("END");
        myProc.Line("//");
        myProc.Line("DELIMITER ;");
        retString = myProc.ToString();
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
