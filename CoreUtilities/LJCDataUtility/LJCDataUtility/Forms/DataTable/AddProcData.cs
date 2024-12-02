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
    // ******************************
    #region Constructors
    // ******************************

    // Initializes an object instance.
    // ********************
    public AddProcData(string dbName, DataColumns tableColumns
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

    // ******************************
    #region Properties
    // ******************************

    // Gets or sets the Database name.
    public string DBName { get; set; }

    // Gets or sets the Parent columns.
    public DataColumns ParentColumns { get; set; }

    // Gets or sets the Parent table name.
    public string ParentTableName { get; set; }

    // Gets or sets the table columns.
    public DataColumns TableColumns { get; set; }

    // Gets or sets the table name.
    public string TableName { get; set; }
    #endregion
  }
}
