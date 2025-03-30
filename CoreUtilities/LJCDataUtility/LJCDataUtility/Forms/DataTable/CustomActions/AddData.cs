// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddData.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
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
      var tableID = ParentObject.DataTableRowID(out long tableSiteID);
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , tableSiteID, orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        var tableName = ParentObject.DataTableRowName();
        var dbName = ParentObject.DataConfigItemValue("Database");
        var procData = new AddProcData(dbName, dataColumns, tableName)
        {
          ForeignKeys = ParentObject.ForeignKeys()
        };

        var connectionType = ParentObject.DataConfigItemValue("ConnectionType");
        if (!NetString.HasValue(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
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

    // Generates the TSQL AddData procedure.
    private string CreateAddProc(AddProcData data)
    {
      string retString = null;

      do
      {
        if (null == data.TableColumns)
          continue;

        var proc = new ProcBuilder(ParentObject, data.DBName, data.TableName);
        proc.Begin(proc.AddProcName);

        var foreignKeyParams = ForeignKeyParams(data);
        if (NetCommon.HasItems(foreignKeyParams))
        {
          var uniqueKeyParams = UniqueKeyParamText(foreignKeyParams);
          proc.Text(uniqueKeyParams);
        }

        // Data parameters.
        var parameters = proc.Parameters(data.TableColumns
          , proc.IsFirst);
        proc.Line(parameters);

        proc.Line("AS");
        proc.Line("BEGIN");

        List<string> foreignKeyVars = new List<string>();
        if (NetCommon.HasItems(foreignKeyParams))
        {
          var line = "";
          foreach (ForeignKeyParam foreignKeyParam in foreignKeyParams)
          {
            // Include Referenced table.
            //var foreignKeyVar = foreignKeyParam.TargetTableName;
            var keyColumnNames = foreignKeyParam.ForeignKeyColumnNames;
            var targetColumnNames = foreignKeyParam.TargetSourceColumnNames;
            for (int index = 0; index < keyColumnNames.Count - 1; index++)
            {
              var foreignKeyColumnName = keyColumnNames[index];
              var targetColumnName = TargetColumnName(targetColumnNames
                , index);
              var foreignKeyVar = ProcBuilder.SQLVarName(foreignKeyColumnName);
              foreignKeyVars.Add(foreignKeyVar);

              var foreignUniqueVars = foreignKeyParam.ForeignUniqueVars;
              var foreignUniqueVar = ForeignUniqueVar(foreignUniqueVars
                , index);
              line = proc.IFItem(foreignKeyParam.TargetTableName
                , foreignKeyColumnName, targetColumnName
                , foreignUniqueVar);
            }
          }

          var isFirstKey = true;
          foreach (var foreignKeyVar in foreignKeyVars)
          {
            var prefix = "IF";
            if (isFirstKey)
            {
              line += "\r\n";
            }
            else
            {
              prefix = "\r\n  AND";
            }
            isFirstKey = false;
            line += $"{prefix} {foreignKeyVar} IS NOT NULL";
          }
          proc.Line(line);
        }

        // Table
        proc.Line($"IF NOT EXISTS(SELECT ID FROM {data.TableName}");
        proc.Line(" WHERE Name = @name)");
        proc.Line($"  INSERT INTO {data.TableName}");

        var insertList = proc.ColumnsList(data.TableColumns);
        proc.Line(insertList);

        var valuesBuilder = new TextBuilder()
        {
          IndentCount = 2,
          WrapEnabled = true,
        };
        valuesBuilder.Text("Values(");
        valuesBuilder.IsFirst = true;
        foreach (var tableColumn in data.TableColumns)
        {
          if ("ID" == tableColumn.Name)
            continue;

          var columnName = tableColumn.Name;
          if (IsForeignKeyColumn(foreignKeyParams, columnName))
          {
            columnName = ProcBuilder.SQLVarName(columnName);
          }
          valuesBuilder.Item(columnName);
        }
        valuesBuilder.Add(")");
        var valuesList = valuesBuilder.ToString();
        proc.Line(valuesList);
        proc.Line("END");
        retString = proc.ToString();
      } while (false);

      var infoValue = ParentObject.InfoValue;
      var controlValue = DataUtilityCommon.ShowInfo(retString
        , "Add Data Procedure", infoValue);
      ParentObject.InfoValue = controlValue;
      return retString;
    }

    // Gets the referenced parameters.
    private List<ForeignKeyParam> ForeignKeyParams(AddProcData data)
    {
      List<ForeignKeyParam> retParams = new List<ForeignKeyParam>();

      do
      {
        if (!NetCommon.HasItems(data.ForeignKeys))
          continue;

        foreach (DataKey foreignKey in data.ForeignKeys)
        {
          var targetUniqueKeys = TargetUniqueKeys(foreignKey);
          if (!NetCommon.HasItems(targetUniqueKeys))
            continue;

          foreach (DataKey targetUniqueKey in targetUniqueKeys)
          {
            if (!NetString.HasValue(targetUniqueKey.SourceColumnName))
              continue;

            var targetSourceColumns = NetString.Split(targetUniqueKey.SourceColumnName, ",");
            if (!NetCommon.HasElements(targetSourceColumns))
              continue;

            var targetTableName = foreignKey.TargetTableName;
            var targetTableID = ParentObject.TargetDataTableID(targetTableName
              , out long targetSiteID);

            List<string> foreignUniqueParams = new List<string>();
            List<string> foreignUniqueVars = new List<string>();
            List<string> targetSourceColumnNames = new List<string>();
            string typeValue = "int";
            foreach (var targetSourceColumnName in targetSourceColumns)
            {
              // Name
              targetSourceColumnNames.Add(targetSourceColumnName);
              var targetTableColumns = TargetColumns(foreignKey.TargetTableName);
              var findColumn = targetTableColumns.LJCSearchUnique(targetTableID
                , targetSiteID, targetSourceColumnName);
              if (findColumn != null)
              {
                // nvarchar
                typeValue = findColumn.TypeName;
                if (findColumn.MaxLength > 0)
                {
                  // nvarchar(60)
                  typeValue += $"({findColumn.MaxLength})";
                }
              }
              var foreignUniqueVar = ProcBuilder.SQLVarName(foreignKey.TargetTableName);
              // @dataModuleName
              foreignUniqueVar += targetSourceColumnName;
              foreignUniqueVars.Add(foreignUniqueVar);

              // @dataModuleName nvarchar(60)
              var foreignUniqueParam = $"{foreignUniqueVar} {typeValue}";
              foreignUniqueParams.Add(foreignUniqueParam);

              // Get foreign key columns.
              var foreignKeyColumnNames = new List<string>();
              var targetKeyColumns = NetString.Split(foreignKey.SourceColumnName, ",");
              if (NetCommon.HasElements(targetKeyColumns))
              {
                foreach (var targetKeyColumn in targetKeyColumns)
                {
                  foreignKeyColumnNames.Add(targetKeyColumn);
                }
              }
              var refParam = new ForeignKeyParam()
              {
                ForeignKeyColumnNames = foreignKeyColumnNames,
                ForeignUniqueParams = foreignUniqueParams,
                ForeignUniqueVars = foreignUniqueVars,
                TargetSourceColumnNames = targetSourceColumnNames,
                TargetTableName = foreignKey.TargetTableName,
              };
              retParams.Add(refParam);
            }
          }
        }
      } while (false);
      return retParams;
    }

    // Gets the foreign unique var name.
    private string ForeignUniqueVar(List<string> vars, int index)
    {
      string retValue = null;

      if (NetCommon.HasItems(vars)
        && vars.Count > 0
        && vars.Count <= index + 1)
      {
        retValue = vars[index];
      }
      return retValue;
    }

    private bool IsForeignKeyColumn(List<ForeignKeyParam> foreignKeyParams
      , string columnName)
    {
      bool retValue = false;

      foreach (var foreignKeyParam in foreignKeyParams)
      {
        var foreignKeyColumnNames = foreignKeyParam.ForeignKeyColumnNames;
        foreach (var foreignKeyColumnName in foreignKeyColumnNames)
        {
          if (columnName == foreignKeyColumnName)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Generates the MySQL AddData procedure.
    private string MySQLAddProc(AddProcData data)
    {
      string retString = null;

      if (data.TableColumns != null)
      {
        var myProc = new MyProcBuilder(ParentObject, data.DBName
          , data.TableName);
        myProc.Begin(myProc.AddProcName);

        // Referenced table parameters.
        string parmFindName = null;
        var isFirst = true;
        myProc.IsFirst = true;
        if (NetCommon.HasItems(data.ForeignKeys))
        {
          foreach (DataKey dataKey in data.ForeignKeys)
          {
            // "@tableNameFindName"
            var typeValue = TargetColumnType(dataKey);
            parmFindName = myProc.SQLVarName(dataKey.TargetTableName);
            parmFindName += dataKey.TargetColumnName;
            // *** Begin *** Add
            if (!myProc.IsFirst)
            {
              myProc.Line(",");
            }
            // *** End   *** Add
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
        List<string> varRefNames = new List<string>();
        if (NetCommon.HasItems(data.ForeignKeys))
        {
          foreach (DataKey dataKey in data.ForeignKeys)
          {
            // Include Referenced table.
            var parentIDColumnName = dataKey.TargetColumnName;
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
        myProc.Line("END$$");
        myProc.Line("DELIMITER ;");
        retString = myProc.ToString();
      }

      var infoValue = ParentObject.InfoValue;
      var controlValue = DataUtilityCommon.ShowInfo(retString
        , "Add Data Procedure", infoValue);
      ParentObject.InfoValue = controlValue;
      return retString;
    }

    // Gets the target column name.
    private string TargetColumnName(List<string> names, int index)
    {
      string retValue = null;

      if (NetCommon.HasItems(names)
        && names.Count > 0
        && names.Count <= index + 1)
      {
        retValue = names[index];
      }
      return retValue;
    }

    // Gets the target columns collection.
    private DataColumns TargetColumns(string targetTableName)
    {
      DataColumns retColumns = null;

      var tableID = ParentObject.TargetDataTableID(targetTableName
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
      var targetTableID = ParentObject.TargetDataTableID(targetTableName
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

    // Get the target unique keys.
    private DataKeys TargetUniqueKeys(DataKey dataKey)
    {
      var targetTableName = dataKey.TargetTableName;
      var targetTableID = ParentObject.TargetDataTableID(targetTableName
        , out long targetSiteID);
      var keyManager = Managers.DataKeyManager;
      var retKeys = keyManager.LoadWithParentType(targetTableID
        , targetSiteID, (int)KeyType.Unique);
      return retKeys;
    }

    // Gets the unique key param text.
    private string UniqueKeyParamText(List<ForeignKeyParam> foreignKeyParams)
    {
      string retText = "";

      var isFirst = true;
      foreach (var foreignKeyParam in foreignKeyParams)
      {
        var foreignUniqueParams = foreignKeyParam.ForeignUniqueParams;
        foreach (var foreignUniqueParam in foreignUniqueParams)
        {
          if (!isFirst)
          {
            retText += ",\r\n";
          }
          isFirst = false;
          retText += $"  {foreignUniqueParam}";
        }
      }
      return retText;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }

  internal class ForeignKeyParam
  {

    public List<string> ForeignKeyColumnNames { get; set; }

    public List<string> ForeignUniqueParams { get; set; }

    public List<string> ForeignUniqueVars { get; set; }

    public List<string> TargetSourceColumnNames { get; set; }

    public string TargetTableName { get; set; }
  }
}
