// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Data;

namespace LJCDBMessage
{
  // Represents a collection of rows for a DataTable.
  internal class InfoDataRowCollection
  {
    // Initializes an object instance.
    internal InfoDataRowCollection()
    {
      DataTable dataTable = new DataTable("TableName");
      DataRowCollection dataRows = dataTable.Rows;
      DataRow dataRow;

      #region Methods

      int index = 0;
      dataRow = dataRows[index];

      // Adds the specified DataRow to the DataRowCollection object.
      dataRows.Add(dataRow);

      // Clears the collection of all rows.
      dataRows.Clear();

      // Returns a string that represents the current object.
      dataRows.ToString();
      #endregion
    }
  }
}
