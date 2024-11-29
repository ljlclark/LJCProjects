using LJCDataUtilityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJCDataUtility
{
  internal class AddProcData
  {
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

    public string DBName { get; set; }

    public DataColumns ParentColumns { get; set; }

    public string ParentTableName { get; set; }

    public DataColumns TableColumns { get; set; }

    public string TableName { get; set; }
  }
}
