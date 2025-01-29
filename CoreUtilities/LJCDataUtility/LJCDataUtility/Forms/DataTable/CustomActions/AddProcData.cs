// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddProcData.cs
using LJCDataUtilityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJCDataUtility
{
  // Data for the DataTableGridCode.AddProc() method.
  internal class AddProcData
  {
    #region Constructors

    // Initializes an object instance.
    internal AddProcData(string dbName, DataColumns tableColumns
      , string tableName, DataColumns parentColumns = null
      , string parentTableName = null)
    {
      DBName = dbName;
      TableColumns = tableColumns;
      TableName = tableName;
      ParentColumns = parentColumns;
      ParentTableName = parentTableName;
    }
    #endregion

    #region Properties

    // Gets or sets the Database name.
    internal string DBName { get; set; }

    // Gets or sets the Parent columns.
    internal DataColumns ParentColumns { get; set; }

    // Gets or sets the Parent table name.
    internal string ParentTableName { get; set; }

    // Gets or sets the table columns.
    internal DataColumns TableColumns { get; set; }

    // Gets or sets the table name.
    internal string TableName { get; set; }
    #endregion
  }
}
