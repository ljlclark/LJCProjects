// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SetUtilData.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using System.Data;
using System.Windows.Forms;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDataUtility
{
  // Provides methods to Set the DataUtil data from a table.
  internal class SetUtilData
  {
    #region Constructors

    // Initializes an object instance.
    internal SetUtilData(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Selects the table and sets the data.
    internal void SetData()
    {
      while (true)
      {
        // Select data config and table.
        var detail = new TableNameSelect();
        var result = detail.ShowDialog();
        if (DialogResult.OK == result)
        {
          DataConfigName = detail.DataConfigName;
          TableName = detail.TableName;
        }
        detail.Dispose();
        if (result != DialogResult.OK)
        {
          break;
        }

        if (!CheckRowTable(TableName))
        {
          break;
        }

        var moduleId = ParentObject.DataModuleItemId(out short moduleDbId);
        var dataTable = GetDataTable(moduleDbId, moduleId, TableName);
        var isUpdate = CreatePrompt(dataTable, TableName, out bool isCreate);
        if (isCreate)
        {
          CreateData(moduleId);
          break;
        }

        if (isUpdate)
        {
          if (!UpdatePrompt(TableName))
          {
            break;
          }

          TableDbId = dataTable.DbId;
          TableId = dataTable.Id;
          UpdateColumns();
          RemoveColumns();
          SetKeysPrimary();
          SetKeysUnique();
          SetKeysForeign();
          ParentObject.ModuleCombo.Select();
          MessageBox.Show("Create/Update complete");
        }
        break;
      }
    }

    // Check if the selected table name matches the selected table row name.
    private bool CheckRowTable(string tableName)
    {
      var retValue = true;

      var itemName = ParentObject.DataTableRowName();
      if (itemName != tableName)
      {
        var message = $"Table name '{itemName}' does not match selected name";
        message += $" '{tableName}'.\r\n";
        message += "Are you sure you want to continue?";
        if (DialogResult.No == MessageBox.Show(message, "Create Data"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          retValue = false;
          MessageBox.Show("Set Data was aborted.");
        }
      }
      return retValue;
    }

    // The "Create" prompt.
    private bool CreatePrompt(DataUtilTable dataTable, string tableName
      , out bool isCreate)
    {
      bool retIsUpdate = true;

      isCreate = false;
      if (null == dataTable)
      {
        var message = $"Create data for new DataTable {tableName}?";
        if (DialogResult.No == MessageBox.Show(message, "Create Data"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          retIsUpdate = false;
          MessageBox.Show("Create Data was aborted.");
        }
        else
        {
          isCreate = true;
        }
      }
      return retIsUpdate;
    }

    private DataUtilTable GetDataTable(short moduleDbId, long moduleId
      , string tableName)
    {
      var tableManager = Managers.DataTableManager;
      var retTable = tableManager.RetrieveWithUnique(moduleId, moduleDbId
        , tableName);
      return retTable;
    }

    // The "Update" prompt.
    private bool UpdatePrompt(string tableName)
    {
      var retValue = true;

      var message = $"Table '{tableName}' data already exists.";
      message += "\r\n Do you want to update the columns and constraints?";
      if (DialogResult.No == MessageBox.Show(message, "Update Data"
        , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
      {
        retValue = false;
        MessageBox.Show("Update columns and constraints was aborted.");
      }
      return retValue;
    }
    #endregion

    #region Module Create Methods

    // Creates DataUtilTable and DataUtilColumn data.
    private void CreateData(long moduleID)
    {
      var tableManager = Managers.DataTableManager;
      var dataTable = new DataUtilTable
      {
        DataModuleId = moduleID,
        Name = TableName,
        Description = TableName
      };

      // Set insert property names without auto increment columns.
      var propertyNames = tableManager.PropertyNames();
      propertyNames.Remove("Id");
      dataTable.ChangedNames.AddNames(propertyNames);
      var newTable = tableManager.Add(dataTable);

      CreateColumns(newTable.DbId, newTable.Id);
      CreateKeys(newTable.Id);

      var tableGridCode = new DataTableGridCode(ParentObject);
      tableGridCode.Refresh();
      tableGridCode.RowSelect(newTable.Id);
    }
    #endregion

    #region Column Create Methods

    // Creates column data.
    private void CreateColumn(LJCDataColumn dbColumn, short tableDbId
      , long tableId, int sequence)
    {
      var newColumn = new DataUtilColumn
      {
        DataTableDbId = tableDbId,
        DataTableId = tableId,
        Name = dbColumn.ColumnName,
        Description = dbColumn.ColumnName,
        Sequence = sequence,
        TypeName = dbColumn.SQLTypeName,
        MaxLength = (short)dbColumn.MaxLength,
        AllowNull = dbColumn.AllowDBNull,
        DefaultValue = dbColumn.DefaultValue,
        IdentityStart = -1,
        IdentityIncrement = -1,
        NewName = null,
        NewMaxLength = -1
      };
      if (dbColumn.AutoIncrement)
      {
        newColumn.IdentityStart = 1;
        newColumn.IdentityIncrement = 1;
      }

      var columnManager = Managers.DataColumnManager;

      // Set insert property names without auto increment columns.
      var names = columnManager.PropertyNames();
      names.Remove("Id");
      newColumn.ChangedNames.AddNames(names);

      // Remove property names where the object properties do not exist.
      LJCReflect reflect = new LJCReflect(newColumn);
      foreach (var name in names)
      {
        if (!reflect.HasProperty(name))
        {
          newColumn.ChangedNames.Remove(name);
        }
      }

      columnManager.Add(newColumn, includeNull: true);
    }

    // Creates the new columns.
    private void CreateColumns(short newTableDbId, long newTableId)
    {
      var manager = new DataManager(DataConfigName, TableName);
      var dbColumns = manager.BaseDefinition;
      int sequence = 0;
      foreach (var dbColumn in dbColumns)
      {
        sequence++;
        CreateColumn(dbColumn, newTableDbId, newTableId, sequence);
      }
    }

    // Removes a defined column.
    private void RemoveColumn(short dbId, long id)
    {
      var columnManager = Managers.DataColumnManager;
      var keyColumns = new DataColumnManager().IdKey(id, dbId);
      columnManager.Delete(keyColumns);
    }

    // Removes defined columns that are not in the database table.
    private void RemoveColumns()
    {
      // Get columns from database table.
      var manager = new DataManager(DataConfigName, TableName);
      var tableColumns = manager.BaseDefinition;

      // Get definition columns.
      var columnManager = Managers.DataColumnManager;
      var keyColumns = columnManager.ParentKey(TableId, TableDbId);
      var dataColumns = columnManager.Load(keyColumns);
      foreach (var dataColumn in dataColumns)
      {
        // Find column in database table.
        var columnName = dataColumn.Name;
        var tableColumn = tableColumns[columnName];
        if (null == tableColumn)
        {
          // Remove definition for missing table column.
          var message = $"Remove Definition {columnName}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Remove Column"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            RemoveColumn(dataColumn.DbId, dataColumn.Id);
          }
        }
      }
    }

    // Updates the column values.
    private void UpdateColumn(LJCDataColumn tableColumn, DataUtilColumn dataColumn)
    {
      string compareText = "";
      var updateColumn = new DataUtilColumn();
      if (dataColumn.TypeName != tableColumn.SQLTypeName)
      {
        updateColumn.TypeName = tableColumn.SQLTypeName;
        compareText += $"DataColumn.TypeName: {dataColumn.TypeName}";
        compareText += $" = {tableColumn.SQLTypeName}\r\n";
      }
      //if (-1 == tableColumn.MaxLength)
      //{
      //  tableColumn.MaxLength = -1;
      //}
      if (dataColumn.MaxLength != tableColumn.MaxLength)
      {
        updateColumn.MaxLength = (short)tableColumn.MaxLength;
        compareText += $"DataColumn.MaxLength: {dataColumn.MaxLength}";
        compareText += $" = {tableColumn.MaxLength}\r\n";
      }
      if (dataColumn.AllowNull != tableColumn.AllowDBNull)
      {
        var changes = updateColumn.ChangedNames;
        updateColumn.AllowNull = tableColumn.AllowDBNull;
        if (!changes.Contains("AllowNull"))
        {
          updateColumn.ChangedNames.Add("AllowNull");
        }
        compareText += $"DataColumn.AllowNull: {dataColumn.AllowNull}";
        compareText += $" = {tableColumn.AllowDBNull}\r\n";
      }
      if (NetString.HasValue(compareText))
      {
        var message = $"Update {dataColumn.Name}\r\n {compareText}";
        if (DialogResult.Yes == MessageBox.Show(message, "Update"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          var columnManager = Managers.DataColumnManager;
          var keyColumns = columnManager.IdKey(dataColumn.Id, dataColumn.DbId);
          // *** Add ***
          var propertyNames = updateColumn.ChangedNames.ChangedProperties;
          columnManager.Update(updateColumn, keyColumns, propertyNames);
        }
      }
    }

    // Updates the data columns.
    private void UpdateColumns()
    {
      // Get columns from database table.
      var manager = new DataManager(DataConfigName, TableName);
      var tableColumns = manager.BaseDefinition;
      foreach (var tableColumn in tableColumns)
      {
        // Find column in utility definition.
        var columnName = tableColumn.ColumnName;
        var columnManager = Managers.DataColumnManager;
        var dataColumn = columnManager.RetrieveWithUnique(TableId, TableDbId
          , columnName);
        if (null == dataColumn)
        {
          // Create missing definition.
          var message = $"Create {columnName}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Column"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateColumn(tableColumn, TableDbId, TableId, tableColumns.Count + 1);
          }
          continue;
        }
        UpdateColumn(tableColumn, dataColumn);
      }
    }
    #endregion

    #region Key Create Methods

    // Creates DataKey data.
    private void CreateKey(TableKey tableKey, long tableID, short keyType)
    {
      var newKey = new DataKey()
      {
        DataTableId = tableID,
        Name = tableKey.ConstraintName,
        KeyType = keyType,
        SourceColumnName = tableKey.ColumnName,
        TargetTableName = tableKey.TableName,
        TargetColumnName = tableKey.ColumnName,
        IsClustered = false,
        IsAscending = false
      };

      var keyManager = Managers.DataKeyManager;

      // Set insert property names without auto increment columns.
      var names = keyManager.PropertyNames();
      names.Remove("Id");
      newKey.ChangedNames.AddNames(names);

      // Remove property names where the object properties do not exist.
      LJCReflect reflect = new LJCReflect(newKey);
      foreach (var name in names)
      {
        if (!reflect.HasProperty(name))
        {
          newKey.ChangedNames.Remove(name);
        }
      }

      keyManager.Add(newKey, includeNull: true);
    }

    // Creates the new Keys.
    private void CreateKeys(long newTableID)
    {
      var primaryKeys = GetTableKeys();
      foreach (var primaryKey in primaryKeys)
      {
        CreateKey(primaryKey, newTableID, 1);
      }

      var uniqueKeys = GetTableKeys("UNIQUE");
      foreach (var uniqueKey in uniqueKeys)
      {
        CreateKey(uniqueKey, newTableID, 2);
      }

      var foreignKeys = GetTableKeys("FOREIGN KEY");
      foreach (var foreignKey in foreignKeys)
      {
        CreateKey(foreignKey, newTableID, 3);
      }
    }

    // Loads the keys.
    private TableKeys GetTableKeys(string keyType = "PRIMARY KEY"
      , string constraintName = null)
    {
      TableKeys retKeys = null;

      var dbServiceRef = new DbServiceRef
      {
        DbDataAccess = new DbDataAccess(DataConfigName)
      };
      var keyManager = new TableKeyManager(dbServiceRef, DataConfigName
        , TableName);

      switch (keyType.ToLower())
      {
        case "primary key":
          retKeys = keyManager.LoadTableKeys("PRIMARY KEY", constraintName);
          break;

        case "unique":
          retKeys = keyManager.LoadTableKeys("UNIQUE");
          break;

        case "foreign key":
          retKeys = keyManager.LoadForeignKeys();
          break;
      }
      return retKeys;
    }

    // Sets the foreign keys.
    private void SetKeysForeign()
    {
      // Get the foreign keys for TableName
      var foreignTableKeys = GetTableKeys("FOREIGN KEY");
      if (LJC.HasListItems(foreignTableKeys))
      {
        // Get combined SourceColumnNames.
        var foreignKeyGroup = new TableKeyGroup(foreignTableKeys);
        var sourceColumnNames = foreignKeyGroup.NextGroupNames();
        while (sourceColumnNames != null)
        {
          // Get Current foreign key values.
          var workForeignTableKey = foreignKeyGroup.CurrentTableKey.Clone();
          workForeignTableKey.ColumnName = sourceColumnNames;

          // Get combined TargetColumnNames.
          var uniqueConstraintName = workForeignTableKey.UniqueConstraintName;
          var primaryTableKeys = GetTableKeys("PRIMARY KEY"
            , uniqueConstraintName);
          var targetKeyGroup = new TableKeyGroup(primaryTableKeys);
          var targetColumnNames = targetKeyGroup.NextGroupNames();
          if (NetString.HasValue(targetColumnNames))
          {
            workForeignTableKey.TargetColumn = targetColumnNames;

            // Get TargetTable name.
            var primaryTableKey = targetKeyGroup.CurrentTableKey;
            workForeignTableKey.TargetTable = primaryTableKey.TableName;

            // Get foreignDataKey.
            var dataKeyManager = Managers.DataKeyManager;
            var foreignDataKey = dataKeyManager.RetrieveWithUnique(TableId
              , TableDbId, workForeignTableKey.ConstraintName);
            if (foreignDataKey == null)
            {
              var message = $"Create {workForeignTableKey.ConstraintName}?";
              if (DialogResult.Yes == MessageBox.Show(message, "Create Key"
                  , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
              {
                CreateKey(workForeignTableKey, TableId, 3);
              }
            }
            else
            {
              UpdateKey(workForeignTableKey, foreignDataKey);
            }
            sourceColumnNames = foreignKeyGroup.NextGroupNames();
          }
        }
      }
    }

    // Sets the primary keys.
    private void SetKeysPrimary()
    {
      var keyManager = Managers.DataKeyManager;

      var primaryKeys = GetTableKeys();
      if (LJC.HasListItems(primaryKeys))
      {
        // Create comma delimited string.
        string sourceColumnNames = "";
        foreach (var key in primaryKeys)
        {
          NetString.AddDelimitedValue(ref sourceColumnNames, key.ColumnName);
        }

        var primaryKey = primaryKeys[0];
        primaryKey.ColumnName = sourceColumnNames;
        var constraintName = primaryKey.ConstraintName;
        var dataKey = keyManager.RetrieveWithUnique(TableId, TableDbId
          , constraintName);
        if (dataKey == null)
        {
          var message = $"Create {dataKey.Name}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Key"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateKey(primaryKey, TableId, 1);
          }
        }
        else
        {
          UpdateKey(primaryKey, dataKey);
        }
      }
    }

    // Sets the unique keys.
    private void SetKeysUnique()
    {
      var keyManager = Managers.DataKeyManager;

      var uniqueKeys = GetTableKeys("UNIQUE");
      if (LJC.HasListItems(uniqueKeys))
      {
        // Create comma delimited string.
        string sourceColumnNames = "";
        foreach (var key in uniqueKeys)
        {
          NetString.AddDelimitedValue(ref sourceColumnNames, key.ColumnName);
        }

        var uniqueKey = uniqueKeys[0];
        uniqueKey.ColumnName = sourceColumnNames;
        var constraintName = uniqueKey.ConstraintName;
        var dataKey = keyManager.RetrieveWithUnique(TableId, TableDbId
          , constraintName);
        if (dataKey == null)
        {
          var message = $"Create {dataKey.Name}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Key"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateKey(uniqueKey, TableId, 2);
          }
        }
        else
        {
          UpdateKey(uniqueKey, dataKey);
        }
      }
    }

    // Updates the DataKey values.
    private void UpdateKey(TableKey tableKey, DataKey dataKey)
    {
      string compare = "";
      var updateKey = new DataKey();
      if (dataKey.SourceColumnName != tableKey.ColumnName)
      {
        updateKey.SourceColumnName = tableKey.ColumnName;
        compare += $"DataKey.SourceColumnName: {dataKey.SourceColumnName}";
        compare += $" = {tableKey.ColumnName}\r\n";
      }
      if (dataKey.TargetTableName != tableKey.TargetTable)
      {
        updateKey.TargetTableName = tableKey.TargetTable;
        compare += $"DataKey.TargetTableName: {dataKey.TargetTableName}";
        compare += $" = {tableKey.TargetTable}\r\n";
      }
      if (dataKey.TargetColumnName != tableKey.TargetColumn)
      {
        updateKey.TargetColumnName = tableKey.TargetColumn;
        compare += $"DataKey.TargetColumnName: {dataKey.TargetColumnName}";
        compare += $" = {tableKey.TargetColumn}\r\n";
      }
      if (NetString.HasValue(compare))
      {
        var message = $"Update {dataKey.Name}\r\n {compare}";
        if (DialogResult.Yes == MessageBox.Show(message, "Update"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          var keyManager = Managers.DataKeyManager;
          var keyColumns = keyManager.UniqueKey(dataKey.DataTableId
            , dataKey.DataTableDbId, dataKey.Name);
          // *** Add ***
          var propertyNames = updateKey.ChangedNames.ChangedProperties;
          keyManager.Update(updateKey, keyColumns, propertyNames);
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the DataConfig name.
    private string DataConfigName { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Table database ID.
    private short TableDbId { get; set; }

    // Gets or sets the Table ID.
    private long TableId { get; set; }

    // Gets or sets the Table name.
    private string TableName { get; set; }
    #endregion
  }
}
