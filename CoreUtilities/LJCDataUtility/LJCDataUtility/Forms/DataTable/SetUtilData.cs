// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateUtilData.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Set Data methods.
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
      var detail = new TableNameSelect();
      var result = detail.ShowDialog();
      bool success = false;
      if (DialogResult.OK == result)
      {
        success = true;
      }

      var dataConfigName = detail.DataConfigName;
      var tableName = detail.TableName;
      DataUtilTable dataTable = null;
      if (success)
      {
        var tableManager = Managers.DataTableManager;
        var moduleID = Parent.DataModuleID();
        dataTable = tableManager.RetrieveWithUnique(moduleID, tableName);
        if (null == dataTable)
        {
          success = false;
          var message = $"Create data for new DataTable {tableName}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Data"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateData(dataConfigName, tableName, moduleID);
          }
        }
      }

      if (success)
      {
        var message = $"Table '{tableName}' already exists.";
        message += "\r\n Do you want to update the columns?";
        if (DialogResult.Yes == MessageBox.Show(message, "Update Data"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          UpdateData(dataConfigName, tableName, dataTable.ID);
        }
      }
    }

    // Creates the DataUtilTable and DataUtilColumn data.
    private void CreateData(string dataConfigName, string tableName
      , int moduleID)
    {
      var tableManager = Managers.DataTableManager;
      var dataTable = new DataUtilTable
      {
        DataModuleID = moduleID,
        Name = tableName,
        Description = tableName
      };
      var propertyNames = tableManager.PropertyNames();
      propertyNames.Remove("ID");
      dataTable.AddChangedNames(propertyNames);
      var newTable = tableManager.Add(dataTable);

      // Create Columns
      var manager = new DataManager(dataConfigName, tableName);
      var dbColumns = manager.BaseDefinition;
      foreach (var dbColumn in dbColumns)
      {
        CreateColumn(dbColumn, newTable.ID);
      }
    }

    // Creates DataUtilColumn data.
    private void CreateColumn(DbColumn dbColumn, int tableID)
    {
      var newColumn = new DataUtilColumn
      {
        DataTableID = tableID,
        Name = dbColumn.ColumnName,
        Description = dbColumn.ColumnName,
        Sequence = -1,
        TypeName = dbColumn.SQLTypeName,
        IdentityStart = -1,
        IdentityIncrement = -1,
        MaxLength = (short)dbColumn.MaxLength,
        AllowNull = dbColumn.AllowDBNull,
        DefaultValue = dbColumn.DefaultValue,
        NewSequence = -1,
        NewMaxLength = -1
      };
      if (dbColumn.AutoIncrement)
      {
        newColumn.IdentityStart = -1;
        newColumn.IdentityIncrement = -1;
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

    // Update data values.
    private void UpdateData(string dataConfigName, string tableName
      , int dataTableID)
    {
      var columnManager = Managers.DataColumnManager;
      var manager = new DataManager(dataConfigName, tableName);
      var columns = manager.BaseDefinition;
      foreach (var column in columns)
      {
        var updateColumn = new DataUtilColumn();
        string compare = "";
        var dataColumn = columnManager.RetrieveWithUnique(dataTableID
          , column.ColumnName);
        if (null == dataColumn)
        {
          var message = $"Create {column.ColumnName}?";
          if (DialogResult.Yes == MessageBox.Show(message, "Create Column"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            CreateColumn(column, dataTableID);
          }
          continue;
        }
        if (dataColumn.TypeName != column.SQLTypeName)
        {
          updateColumn.TypeName = column.SQLTypeName;
          compare += $"TypeName: {dataColumn.TypeName} = {column.SQLTypeName}";
          compare += "\r\n";
        }
        if (-1 == column.MaxLength)
        {
          column.MaxLength = -1;
        }
        if (dataColumn.MaxLength != column.MaxLength)
        {
          updateColumn.MaxLength = (short)column.MaxLength;
          compare += $"MaxLength: {dataColumn.MaxLength} = {column.MaxLength}";
          compare += "\r\n";
        }
        if (dataColumn.AllowNull != column.AllowDBNull)
        {
          var changes = updateColumn.ChangedNames;
          updateColumn.AllowNull = column.AllowDBNull;
          if (!changes.Contains("AllowNull"))
          {
            updateColumn.ChangedNames.Add("AllowNull");
          }
          compare += $"AllowNull: {dataColumn.AllowNull} = {column.AllowDBNull}";
          compare += "\r\n";
        }
        if (NetString.HasValue(compare))
        {
          var message = $"Update {dataColumn.Name}\r\n {compare}";
          if (DialogResult.Yes == MessageBox.Show(message, "Update"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            var keyColumns = columnManager.IDKey(dataColumn.ID);
            columnManager.Update(updateColumn, keyColumns);
          }
        }
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
