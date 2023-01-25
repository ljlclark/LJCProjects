// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InfoDataColumnCollection.cs
using System;
using System.Data;

namespace LJCDBMessage
{
  // Represents a collection of DataColumn objects for a DataTable.
  // The DataColumnCollection defines the schema of a DataTable.
  internal class InfoDataColumnCollection
  {
    // Initializes an object instance.
    internal InfoDataColumnCollection()
    {
      DataTable dataTable = new DataTable("TableName");
      DataColumnCollection dataColumns = dataTable.Columns;
      DataColumn dataColumn = new DataColumn("ColumnName", typeof(string));
      //int index = 0;

      #region Methods

      // Creates and adds the specified DataColumn object to the
      // DataColumnCollection.
      dataColumns.Add(dataColumn);

      // Creates and adds a DataColumn object that has the specified name to the
      // DataColumnCollection.
      dataColumns.Add("ColumnName");

      // Creates and adds a DataColumn object that has the specified name and type
      // to the DataColumnCollection.
      dataColumns.Add("ColumnName", typeof(string));

      // Clears the collection of any columns.
      dataColumns.Clear();

      // Returns a string that represents the current object.
      dataColumns.ToString();
      #endregion

      #region Properties and Indexers

      // Gets the DataColumn from the collection at the specified index.
      //dataColumn = dataColumns[index];

      // Gets the DataColumn from the collection with the specified name.
      //dataColumn = dataColumns["ColumnName"];
      #endregion
    }
  }
}
