// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddDataProc5.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  // Provides methods to generate the AddData procedure.
  internal class AddDataProc
  {
    #region Constructors

    // Initializes an object instance.
    internal AddDataProc(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      var comboCode = ParentObject.ConfigComboCode;
      var config = comboCode.DataConfigItem();
      if (config != null)
      {
        Config = config;
      }
      TableGridCode = ParentObject.TableGridCode;
      KeyGridCode = ParentObject.KeyGridCode;
      Managers = ParentObject.Managers;
    }

    // Gets or sets the Table grid code reference.
    private DataTableGridCode TableGridCode { get; set; }

    // Gets or sets the Key grid code reference.
    private DataKeyGridCode KeyGridCode { get; set; }
    #endregion

    #region Methods

    // Generates the AddData procedure.
    internal void CreateAddDataProc()
    {
      //var tableId = ParentObject.DataTableRowId(out short tableDbId);
      var tableId = TableGridCode.RowId(out short tableDbId);
      List<string> orderByNames =
      [
        DataUtilColumn.ColumnSequence
      ];

      while (true)
      {
        var dataColumns = Managers.TableDataColumns(tableDbId, tableId
        , orderByNames);
        var tableName = TableGridCode.RowName();
        if (!LJC.HasListItems(dataColumns)
          || !LJC.HasText(tableName)
          || !LJC.HasText(Config.Database))
        {
          break;
        }

        var procData = new AddData(Config.Database, dataColumns, tableName);
        var foreignKeys = KeyGridCode.ForeignKeys();
        if (LJC.HasListItems(foreignKeys))
        {
          procData.ForeignKeys = foreignKeys;
        }

        // Default Value
        string connectionType = "SQLServer";
        if (LJC.HasText(Config.ConnectionType))
        {
          connectionType = Config.ConnectionType;
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
        break;
      }
    }

    // Generates the TSQL AddData procedure.
    private string? CreateAddProc(AddData data)
    {
      string? retString = null;

      do
      {
        if (null == data.TableColumns)
          continue;

        var proc = new ProcBuilder(ParentObject, data.DBName, data.TableName);
        proc.Begin(proc.AddProcName);

        var keyValues = KeyValues(data.ForeignKeys);
        if (LJC.HasListItems(keyValues))
        {
          var uniqueParamValueList = UniqueParamValueList(keyValues);
          proc.Text(uniqueParamValueList);
        }

        // Data parameters.
        var parameters = proc.Parameters(data.TableColumns
          , proc.IsFirst);
        proc.Line(parameters);

        proc.Line("AS");
        proc.Line("BEGIN");

        List<string> foreignKeyVars = [];
        if (LJC.HasListItems(keyValues))
        {
          var line = "";
          foreach (KeyValues keyValueObject in keyValues)
          {
            // Include Referenced table.
            var foreignKeyColumnNames = keyValueObject.ForeignKeyColumnNames;
            for (int index = 0; index < foreignKeyColumnNames.Count - 1; index++)
            {
              var foreignKeyColumnName = foreignKeyColumnNames[index];
              var foreignTableName = keyValueObject.ForeignTableName;
              var uniqueColumnName = UniqueColumnName(keyValueObject, index);
              var uniqueVar = UniqueVar(keyValueObject, index);
              if (LJC.HasText(uniqueColumnName)
                && LJC.HasText(uniqueVar))
              {
                line = proc.IFItem(foreignTableName, foreignKeyColumnName
                  , uniqueColumnName, uniqueVar);
                var foreignKeyVar = ProcBuilder.SQLVarName(foreignKeyColumnName);
                foreignKeyVars.Add(foreignKeyVar);
              }
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
        proc.Line($"IF NOT EXISTS(SELECT Id FROM {data.TableName}");
        proc.Line(" WHERE Name = @name)");
        proc.Line($"  INSERT INTO {data.TableName}");

        var insertList = proc.ColumnsList(data.TableColumns);
        proc.Line(insertList);

        var valuesBuilder = new LJCTextBuilder()
        {
          WrapEnabled = true,
        };
        valuesBuilder.AddIndent(2);
        valuesBuilder.Text("VALUES(");
        valuesBuilder.IsFirst = true;
        foreach (var tableColumn in data.TableColumns)
        {
          if (LJC.IsEqual("Id", tableColumn.Name))
          {
            continue;
          }

          var columnName = tableColumn.Name;
          if (IsForeignKeyColumn(keyValues, columnName))
          {
            columnName = ProcBuilder.SQLVarName(columnName);
          }
          valuesBuilder.Item(columnName, false, false);
        }
        valuesBuilder.AddText(")");
        var valuesList = valuesBuilder.ToString();
        proc.Line(valuesList);
        proc.Line("END");
        retString = proc.ToString();
      } while (false);

      if (LJC.HasText(retString))
      {
        var infoValue = ParentObject.InfoValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(retString
          , "Add Data Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
      return retString;
    }

    // Gets the referenced parameters.
    private List<KeyValues> KeyValues(DataKeys foreignKeys)
    {
      List<KeyValues> retValues = [];

      if (LJC.HasListItems(foreignKeys))
      {
        foreach (DataKey foreignKey in foreignKeys)
        {
          if (!LJC.HasText(foreignKey.SourceColumnName)
            || !LJC.HasText(foreignKey.TargetTableName))
          {
            continue;
          }

          // DataModuleId, DataModuleDbId
          var foreignKeyColumnNames = ToNames(foreignKey.SourceColumnName);

          var targetUniqueKeys = TargetUniqueKeys(foreignKey);
          if (!LJC.HasListItems(targetUniqueKeys))
            continue;

          foreach (var targetUniqueKey in targetUniqueKeys)
          {
            if (!LJC.HasText(targetUniqueKey.SourceColumnName))
            {
              continue;
            }

            // Name
            var uniqueColumnNames = ToNames(targetUniqueKey.SourceColumnName);

            List<string> uniqueParamValues = [];
            List<string> uniqueVarNames = [];
            foreach (var uniqueColumnName in uniqueColumnNames)
            {
              // @dataModuleName
              var uniqueVarName = UniqueVarName(foreignKey, uniqueColumnName
                , out string typeValue);
              uniqueVarNames.Add(uniqueVarName);

              // @dataModuleName nvarchar(60)
              uniqueParamValues.Add($"{uniqueVarName} {typeValue}");

              var foreignKeyValue = new KeyValues()
              {
                ForeignKeyColumnNames = foreignKeyColumnNames,
                UniqueColumnNames = uniqueColumnNames,
                UniqueParamValues = uniqueParamValues,
                UniqueVarNames = uniqueVarNames,
                ForeignTableName = foreignKey.TargetTableName,
              };
              retValues.Add(foreignKeyValue);
            }
          }
        }
      }
      return retValues;
    }

    // Gets the foreign unique var name.
    private string? UniqueVar(KeyValues keyValueObject, int index)
    {
      string? retValue = null;

      var vars = keyValueObject.UniqueVarNames;
      if (LJC.HasListItems(vars)
        && vars.Count > 0
        && vars.Count <= index + 1)
      {
        retValue = vars[index];
      }
      return retValue;
    }

    private bool IsForeignKeyColumn(List<KeyValues> foreignKeyParams
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
    private string? MySQLAddProc(AddData data)
    {
      string? retString = null;

      if (data.TableColumns != null)
      {
        var myProc = new MyProcBuilder(ParentObject, data.DBName
          , data.TableName);
        myProc.Begin(myProc.AddProcName);

        // Referenced table parameters.
        string? parmFindName = null;
        var isFirst = true;
        myProc.IsFirst = true;
        if (LJC.HasListItems(data.ForeignKeys))
        {
          foreach (DataKey dataKey in data.ForeignKeys)
          {
            if (!LJC.HasText(dataKey.TargetTableName))
            {
              continue;
            }

            // "@tableNameFindName"
            var typeValue = TargetColumnType(dataKey);
            parmFindName = myProc.SQLVarName(dataKey.TargetTableName);
            parmFindName += dataKey.TargetColumnName;
            if (!myProc.IsFirst)
            {
              myProc.Line(",");
            }
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
        List<string> varRefNames = [];
        if (LJC.HasListItems(data.ForeignKeys))
        {
          foreach (DataKey dataKey in data.ForeignKeys)
          {
            if (!LJC.HasText(dataKey.TargetTableName)
              || !LJC.HasText(dataKey.TargetColumnName)
              || !LJC.HasText(parmFindName))
            {
              continue;
            }

            // Include Referenced table.
            var parentIdColumnName = dataKey.TargetColumnName;
            var line = myProc.IFItem(dataKey.TargetTableName
              , dataKey.TargetColumnName, dataKey.TargetColumnName
              , parmFindName);
            line += "\r\n";

            var varRefName = myProc.SQLVarName(dataKey.TargetTableName);
            varRefName += parentIdColumnName;
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

      if (LJC.HasText(retString))
      {
        var infoValue = ParentObject.InfoValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(retString
          , "Add Data Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
      return retString;
    }

    // Creates a collection of names from a comma separated string.
    private List<string> ToNames(string nameList, string separator = ",")
    {
      List<string> retItems = [];

      var nameItems = LJCNetString.Split(nameList, separator);
      if (LJC.HasArrayElements(nameItems))
      {
        foreach (var name in nameItems)
        {
          retItems.Add(name.Trim());
        }
      }
      return retItems;
    }

    // Gets the target column name.
    private string? UniqueColumnName(KeyValues keyValues, int index)
    {
      string? retValue = null;

      var names = keyValues.UniqueColumnNames;
      if (LJC.HasListItems(names)
        && names.Count > 0
        && names.Count <= index + 1)
      {
        retValue = names[index];
      }
      return retValue;
    }

    // Gets the target columns collection.
    private DataColumns? TargetColumns(string targetTableName)
    {
      DataColumns? retColumns = null;

      //var tableId = ParentObject.TargetDataTableId(targetTableName
      //  , out short dbId);
      var tableId = TableGridCode.TargetDataTableId(targetTableName
        , out short dbId);
      if (tableId > 0)
      {
        List<string> orderByNames =
        [
          DataUtilColumn.ColumnSequence
        ];
        retColumns = Managers.TableDataColumns(dbId, (long)tableId
          , orderByNames);
      }
      return retColumns;
    }

    // Gets the target column type value.
    private string? TargetColumnType(DataKey dataKey)
    {
      string? retTypeValue = null;

      while (true)
      {
        var targetTableName = dataKey.TargetTableName;
        if (!LJC.HasText(targetTableName)
          || !LJC.HasText(dataKey.TargetColumnName))
        {
          break;
        }

        //var targetTableId = ParentObject.TargetDataTableId(targetTableName
        //  , out short targetTableDbId);
        var targetTableId = TableGridCode.TargetDataTableId(targetTableName
          , out short targetTableDbId);
        if (targetTableId != null
          || targetTableId <= 0)
        {
          break;
        }

        long targetId = targetTableDbId;
        var parentColumns = Managers.TableDataColumns(targetTableDbId
          , targetId);
        if (!LJC.HasListItems(parentColumns))
        {
          break;
        }

        retTypeValue = "nvarchar(5)";
        var findColumn = parentColumns.LJCGetUnique(targetTableDbId
          , targetId, dataKey.TargetColumnName);
        if (findColumn != null)
        {
          retTypeValue = findColumn.TypeName;
          if (findColumn.MaxLength > 0)
          {
            retTypeValue += $"({findColumn.MaxLength})";
          }
        }
        break;
      }
      return retTypeValue;
    }

    // Get the target unique keys.
    private DataKeys? TargetUniqueKeys(DataKey dataKey)
    {
      DataKeys? retUniqueKeys = null;

      var targetTableName = dataKey.TargetTableName;
      while (true)
      {
        if (!LJC.HasText(targetTableName))
        {
          break;
        }

        //var targetTableId = ParentObject.TargetDataTableId(targetTableName
        //  , out short targetDbId);
        var targetTableId = TableGridCode.TargetDataTableId(targetTableName
          , out short targetDbId);
        if (null == targetTableId
          || targetTableId <= 0)
        {
          break;
        }

        long targetId = (long)targetTableId;
        var keyManager = Managers.DataKeyManager;
        retUniqueKeys = keyManager.LoadWithParentType(targetDbId, targetId
          , (int)KeyType.Unique);
        break;
      }
      return retUniqueKeys;
    }

    // Gets the unique key param delimited list.
    private string UniqueParamValueList(List<KeyValues> foreignKeyParams)
    {
      string retText = "";

      var isFirst = true;
      foreach (var foreignKeyParam in foreignKeyParams)
      {
        var uniqueParamValues = foreignKeyParam.UniqueParamValues;
        foreach (var uniqueParamValue in uniqueParamValues)
        {
          if (!isFirst)
          {
            retText += ",\r\n";
          }
          isFirst = false;
          retText += $"  {uniqueParamValue}";
        }
      }
      return retText;
    }

    private string UniqueVarName(DataKey foreignKey, string uniqueColumnName
      , out string typeValue)
    {
      string retVarName = "None";

      typeValue = "None";
      while (true)
      {
        var targetTableName = foreignKey.TargetTableName;
        if (!LJC.HasText(targetTableName))
        {
          break;
        }

        var targetTableColumns = TargetColumns(targetTableName);
        if (!LJC.HasListItems(targetTableColumns))
        {
          break;
        }

        //var targetTableId = ParentObject.TargetDataTableId(targetTableName
        //  , out short targetTableDbId);
        var targetTableId = TableGridCode.TargetDataTableId(targetTableName
          , out short targetTableDbId);
        if (null == targetTableId
          || targetTableId <= 0)
        {
          break;
        }

        var findColumn = targetTableColumns.LJCGetUnique(targetTableDbId
          , (long)targetTableId, uniqueColumnName);
        typeValue = "bigint";
        if (null == findColumn)
        {
          break;
        }

        // nvarchar
        typeValue = findColumn.TypeName;
        if (findColumn.MaxLength > 0)
        {
          // nvarchar(60)
          typeValue += $"({findColumn.MaxLength})";
        }
        retVarName = ProcBuilder.SQLVarName(targetTableName);
        retVarName += uniqueColumnName;
        break;
      }
      return retVarName;
    }
    #endregion

    #region Properties

    // Gets or sets the DataConfig value.
    private LJCDataConfig Config { get; set; } = null!;

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }

  internal class KeyValues
  {

    public List<string> ForeignKeyColumnNames { get; set; } = null!;

    public List<string> UniqueColumnNames { get; set; } = null!;

    public List<string> UniqueParamValues { get; set; } = null!;

    public List<string> UniqueVarNames { get; set; } = null!;

    public string ForeignTableName { get; set; } = null!;
  }
}
