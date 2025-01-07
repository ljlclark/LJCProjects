// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SetUtilData.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using System;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides methods to Set the DataUtil data from a table.
  internal class SetUtilData
  {
    #region Constructors

    // Initializes an object instance.
    internal SetUtilData(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Managers = Parent.Managers;
    }
    #endregion

    #region Methods

    // Selects the table and sets the data.
    internal void SetData()
    {
      bool isContinue = false;
      var detail = new TableNameSelect();
      var result = detail.ShowDialog();
      if (DialogResult.OK == result)
      {
        isContinue = true;
        DataConfigName = detail.DataConfigName;
        TableName = detail.TableName;
      }
      detail.Dispose();

      if (isContinue)
      {
        var itemName = Parent.DataTableName();
        if (itemName != TableName)
        {
          var message = $"Table name '{itemName}' does not match selected name";
          message += $" '{TableName}'.\r\n";
          message += "Are you sure you want to continue?";
          if (DialogResult.No == MessageBox.Show(message, "Create Data"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            isContinue = false;
            MessageBox.Show("Set Data was aborted.");
          }
        }
      }

      DataUtilTable dataTable = null;
      if (isContinue)
      {
        var tableManager = Managers.DataTableManager;
        var moduleID = Parent.DataModuleID();
        dataTable = tableManager.RetrieveWithUnique(moduleID, TableName);
        if (null == dataTable)
        {
          isContinue = false;
          var message = $"Create data for new DataTable {TableName}?";
          if (DialogResult.No == MessageBox.Show(message, "Create Data"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            isContinue = false;
            MessageBox.Show("Create Data was aborted.");
          }
          else
          {
            CreateData(moduleID);
          }
        }
      }

      if (isContinue)
      {
        var message = $"Table '{TableName}' data already exists.";
        message += "\r\n Do you want to update the columns and constraints?";
        if (DialogResult.No == MessageBox.Show(message, "Update Data"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          isContinue = false;
          MessageBox.Show("Update columns and constraints was aborted.");
        }
      }

      if (isContinue)
      {
        TableID = dataTable.ID;
        UpdateColumns();
        SetKeysPrimary();
        SetKeysUnique();
        SetKeysForeign();
        Parent.ModuleCombo.Select();
        MessageBox.Show("Create/Update complete");
      }
    }
    #endregion

    #region Module child Methods

    // Creates DataUtilTable and DataUtilColumn data.
    private void CreateData(int moduleID)
    {
      var tableManager = Managers.DataTableManager;
      var dataTable = new DataUtilTable
      {
        DataModuleID = moduleID,
        Name = TableName,
        Description = TableName
      };
      var propertyNames = tableManager.PropertyNames();
      propertyNames.Remove("ID");
      dataTable.AddChangedNames(propertyNames);
      var newTable = tableManager.Add(dataTable);

      CreateColumns(newTable.ID);
      CreateKeys(newTable.ID);

      var tableGridCode = new DataTableGridCode(Parent);
      tableGridCode.Refresh();
      tableGridCode.RowSelect(newTable.ID);
    }
    #endregion

    #region Column Methods

    // Creates DataUtilColumn data.
    private void CreateColumn(DbColumn dbColumn, int tableID
      , int sequence)
    {
      var newColumn = new DataUtilColumn
      {
        DataTableID = tableID,
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
      var names = columnManager.PropertyNames();
      names.Remove("ID");
      newColumn.AddChangedNames(names);
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
    private void CreateColumns(int newTableID)
    {
      var manager = new DataManager(DataConfigName, TableName);
      var dbColumns = manager.BaseDefinition;
      int sequence = 0;
      foreach (var dbColumn in dbColumns)
      {
        sequence++;
        CreateColumn(dbColumn, newTableID, sequence);
      }
    }

    // Updates the DataUtilColumn values.
    private void UpdateColumn(DbColumn dbColumn, DataUtilColumn dataColumn)
    {
      string compareText = "";
      var updateColumn = new DataUtilColumn();
      if (dataColumn.TypeName != dbColumn.SQLTypeName)
      {
        updateColumn.TypeName = dbColumn.SQLTypeName;
        compareText += $"TypeName: {dataColumn.TypeName}";
        compareText += $" = {dbColumn.SQLTypeName}\r\n";
      }
      if (-1 == dbColumn.MaxLength)
      {
        dbColumn.MaxLength = -1;
      }
      if (dataColumn.MaxLength != dbColumn.MaxLength)
      {
        updateColumn.MaxLength = (short)dbColumn.MaxLength;
        compareText += $"MaxLength: {dataColumn.MaxLength}";
        compareText += $" = {dbColumn.MaxLength}\r\n";
      }
      if (dataColumn.AllowNull != dbColumn.AllowDBNull)
      {
        var changes = updateColumn.ChangedNames;
        updateColumn.AllowNull = dbColumn.AllowDBNull;
        if (!changes.Contains("AllowNull"))
        {
          updateColumn.ChangedNames.Add("AllowNull");
        }
        compareText += $"AllowNull: {dataColumn.AllowNull}";
        compareText += $" = {dbColumn.AllowDBNull}\r\n";
      }
      if (NetString.HasValue(compareText))
      {
        var message = $"Update {dataColumn.Name}\r\n {compareText}";
        if (DialogResult.Yes == MessageBox.Show(message, "Update"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          var columnManager = Managers.DataColumnManager;
          var keyColumns = columnManager.IDKey(dataColumn.ID);
          columnManager.Update(updateColumn, keyColumns);
        }
      }
    }

    // Updates the DataUtilColumn columns.
    private void UpdateColumns()
    {
      var columnManager = Managers.DataColumnManager;
      var manager = new DataManager(DataConfigName, TableName);
      var dbColumns = manager.BaseDefinition;
      foreach (var dbColumn in dbColumns)
      {
        var columnName = dbColumn.ColumnName;
        var dataColumn = columnManager.RetrieveWithUnique(TableID
          , columnName);
        if (null == dataColumn)
        {
          var message = $"Create {columnName}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Column"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateColumn(dbColumn, TableID, dbColumns.Count + 1);
          }
          continue;
        }
        UpdateColumn(dbColumn, dataColumn);
      }
    }
    #endregion

    #region Key Methods

    // Creates DataKey data.
    private void CreateKey(TableKey tableKey, int tableID, short keyType)
    {
      var newKey = new DataKey()
      {
        DataTableID = tableID,
        Name = tableKey.ConstraintName,
        KeyType = keyType,
        SourceColumnName = tableKey.ColumnName,
        TargetTableName = tableKey.TableName,
        TargetColumnName = tableKey.ColumnName,
        IsClustered = false,
        IsAscending = false
      };

      var keyManager = Managers.DataKeyManager;
      var names = keyManager.PropertyNames();
      names.Remove("ID");
      newKey.AddChangedNames(names);
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
    private void CreateKeys(int newTableID)
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
      if (NetCommon.HasItems(foreignTableKeys))
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
            workForeignTableKey.TargetColumns = targetColumnNames;

            // Get TargetTable name.
            var primaryTableKey = targetKeyGroup.CurrentTableKey;
            workForeignTableKey.TargetTable = primaryTableKey.TableName;

            // Get foreignDataKey.
            var dataKeyManager = Managers.DataKeyManager;
            var foreignDataKey = dataKeyManager.RetrieveWithUnique(TableID
              , workForeignTableKey.ConstraintName);
            if (foreignDataKey == null)
            {
              var message = $"Create {workForeignTableKey.ConstraintName}?";
              if (DialogResult.Yes == MessageBox.Show(message, "Create Key"
                  , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
              {
                CreateKey(workForeignTableKey, TableID, 3);
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
      if (NetCommon.HasItems(primaryKeys))
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
        var dataKey = keyManager.RetrieveWithUnique(TableID, constraintName);
        if (dataKey == null)
        {
          var message = $"Create {dataKey.Name}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Key"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateKey(primaryKey, TableID, 1);
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
      if (NetCommon.HasItems(uniqueKeys))
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
        var dataKey = keyManager.RetrieveWithUnique(TableID, constraintName);
        if (dataKey == null)
        {
          var message = $"Create {dataKey.Name}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Key"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateKey(uniqueKey, TableID, 2);
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
        compare += $"SourceColumnName: {dataKey.SourceColumnName}";
        compare += $" = {tableKey.ColumnName}\r\n";
      }
      if (dataKey.TargetTableName != tableKey.TargetTable)
      {
        updateKey.TargetTableName = tableKey.TargetTable;
        compare += $"TargetTableName: {dataKey.TargetTableName}";
        compare += $" = {tableKey.TargetTable}\r\n";
      }
      if (dataKey.TargetColumnName != tableKey.TargetColumns)
      {
        updateKey.TargetColumnName = tableKey.TargetColumns;
        compare += $"TargetColumnName: {dataKey.TargetColumnName}";
        compare += $" = {tableKey.TargetColumns}\r\n";
      }
      if (NetString.HasValue(compare))
      {
        var message = $"Update {dataKey.Name}\r\n {compare}";
        if (DialogResult.Yes == MessageBox.Show(message, "Update"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          var keyManager = Managers.DataKeyManager;
          var keyColumns = keyManager.UniqueKey(dataKey.DataTableID, dataKey.Name);
          keyManager.Update(updateKey, keyColumns);
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
    private DataUtilityList Parent { get; set; }

    // Gets or sets the Table ID.
    private int TableID { get; set; }

    // Gets or sets the Table name.
    private string TableName { get; set; }
    #endregion
  }
}
