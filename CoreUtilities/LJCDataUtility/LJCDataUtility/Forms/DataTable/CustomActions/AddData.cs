// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddData.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
      var tableID = ParentObject.DataTableID(out long tableSiteID);
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , tableSiteID, orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        var tableName = ParentObject.DataTableName();
        var dbName = ParentObject.ComboConfigValue("Database");
        var procData = new AddProcData(dbName, dataColumns, tableName)
        {
          ParentKeys = ParentObject.ForeignKeys()
        };

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
        string parentFindColumnName = null;
        string parmFindName = null;

        // Referenced table parameters.
        proc.IsFirst = true;
        if (NetCommon.HasItems(data.ParentKeys))
        {
          // *** Next Statement *** Add 2/17/25
          foreach (DataKey dataKey in data.ParentKeys)
          {
            // "@tableNameFindName"
            // Set the default typeValue.
            string typeValue = null;

            // Find the target column.
            var targetTableName = dataKey.TargetTableName;
            var targetTableID = ParentObject.ParentDataTableID(targetTableName
              , out long targetSiteID);
            var targetColumns = TargetColumns(dataKey.TargetTableName);
            var findColumn = targetColumns.LJCSearchUnique(targetTableID
              , targetSiteID, dataKey.TargetColumnName);
            if (findColumn != null)
            {
              typeValue = findColumn.TypeName;
              if (findColumn.MaxLength > 0)
              {
                typeValue += $"({findColumn.MaxLength})";
              }
            }

            // Create the 
            parmFindName = proc.SQLVarName(dataKey.TargetTableName);
            parmFindName += parentFindColumnName;
            proc.Text($"  {parmFindName} {typeValue}");
          }
        }

        // Data parameters.
        var parameters = proc.Parameters(data.TableColumns
          , proc.IsFirst);
        proc.Line(parameters);

        proc.Line("AS");
        proc.Line("BEGIN");

        List<string> varRefNames = new List<string>();

        // *** Next Statement *** Change 2/21/25
        if (NetCommon.HasItems(data.ParentKeys))
        {
          foreach (DataKey dataKey in data.ParentKeys)
          {
            // Include Referenced table.
            var parentIDColumnName = dataKey.TargetColumnName;
            var varRefName = proc.SQLVarName(dataKey.TargetTableName);
            varRefName += parentIDColumnName;
            varRefNames.Add(varRefName);
            var line = proc.IFItem(dataKey.TargetTableName
              , dataKey.TargetColumnName, dataKey.TargetColumnName
              , parmFindName);
            line += "\r\n";
            line += $"IF {varRefName} IS NOT NULL";
            proc.Line(line);
          }
        }

        // Table
        proc.Line($"IF NOT EXISTS(SELECT ID FROM {data.TableName}");
        proc.Line(" WHERE Name = @name)");
        proc.Line($"  INSERT INTO {data.TableName}");


        var insertList = proc.ColumnsList(data.TableColumns);
        proc.Line(insertList);

        // ToDo: Handle Multiple
        var valuesList
          = proc.ValuesList(data.TableColumns, varRefNames[0]);
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

        // Referenced table parameters.
        string parentFindColumnName = null;
        string parmFindName = null;
        var isFirst = true;
        if (NetCommon.HasItems(data.ParentKeys))
        {
          foreach (DataKey dataKey in data.ParentKeys)
          {
            // "@tableNameFindName"
            var typeValue = TargetColumnType(dataKey);
            parmFindName = myProc.SQLVarName(dataKey.TargetTableName);
            parmFindName += parentFindColumnName;
            myProc.Text($"  `{parmFindName}` {typeValue}");
            isFirst = false;
          }
        }

        // Data parameters.
        var parameters = myProc.Parameters(data.TableColumns
          , isFirst);
        myProc.Line(parameters);

        myProc.Line(")");
        myProc.Line("BEGIN");

        // Get IF for referenced variables.
        // *** Next Statement *** Add 2/17/25
        List<string> varRefNames = new List<string>();
        if (NetCommon.HasItems(data.ParentKeys))
        {
          foreach (DataKey dataKey in data.ParentKeys)
          {
            // Include Referenced table.
            var parentIDColumnName = dataKey.TargetColumnName;
            if (NetCommon.HasItems(data.ParentColumns))
            {
              var line = myProc.IFItem(dataKey.TargetTableName
                , dataKey.TargetColumnName, dataKey.TargetColumnName
                , parmFindName);
              line += "\r\n";

              var varRefName = myProc.SQLVarName(dataKey.TargetTableName);
              varRefName += parentIDColumnName;
              varRefNames.Add(varRefName);
              line += $"IF {varRefName} IS NOT NULL";
              myProc.Line(line);
            }
          }
        }

        // Table
        myProc.Line($"IF NOT EXISTS(SELECT 1 FROM `{data.TableName}`");
        myProc.Line(" WHERE Name = @name) THEN");
        myProc.Line($"  INSERT INTO `{data.TableName}`");

        var insertList = myProc.ColumnsList(data.TableColumns);
        myProc.Line(insertList);

        // Values list.
        var valuesList
          = myProc.ValuesList(data.TableColumns, varRefNames);
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

    private DataColumns TargetColumns(string targetTableName)
    {
      DataColumns retColumns = null;

      var tableID = ParentObject.ParentDataTableID(targetTableName
        , out long siteID);
      if (tableID > 0)
      {
        var orderByNames = new List<string>()
        {
          DataUtilColumn.ColumnSequence
        };
        retColumns = Managers.TableDataColumns(tableID, siteID, orderByNames);
      }
      return retColumns;
    }

    // Gets the target column type value.
    private string TargetColumnType(DataKey dataKey)
    {
      string retTypeValue = null;

      var targetTableName = dataKey.TargetTableName;
      var targetTableID = ParentObject.ParentDataTableID(targetTableName
        , out long targetSiteID);
      if (targetTableID > 0)
      {
        var parentColumns = Managers.TableDataColumns(targetTableID
          , targetSiteID);
        retTypeValue = "nvarchar(5)";
        var findColumn = parentColumns.LJCSearchUnique(targetTableID
          , targetSiteID, dataKey.TargetColumnName);
        if (findColumn != null)
        {
          retTypeValue = findColumn.TypeName;
          if (findColumn.MaxLength > 0)
          {
            retTypeValue += $"({findColumn.MaxLength})";
          }
        }
      }
      return retTypeValue;
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
