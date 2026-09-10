// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddData5.cs
using LJCDataUtilityDAL5;

namespace LJCDataUtility5
{
  // Data for the DataTableGridCode.AddProc() method.
  internal class AddData
  {
    #region Constructors

    // Initializes an object instance.
    internal AddData(string dbName, DataColumns tableColumns
      , string tableName)
    {
      DBName = dbName;
      TableColumns = tableColumns;
      TableName = tableName;

      ForeignKeys = [];
      ParentColumns = [];
      ParentUniqueColumns = [];
    }
    #endregion

    #region Properties

    // Gets or sets the Database name.
    internal string DBName { get; set; }

    // Gets or sets the ParentKeys collection.
    internal DataKeys ForeignKeys { get; set; } = null!;

    // Gets or sets the parent columns.
    internal DataColumns ParentColumns { get; set; } = null!;

    // Gets or sets the parent unique columns.
    internal DataColumns ParentUniqueColumns { get; set; } = null!;

    // Gets or sets the table columns.
    internal DataColumns TableColumns { get; set; }

    // Gets or sets the table name.
    internal string TableName { get; set; }
    #endregion
  }
}
