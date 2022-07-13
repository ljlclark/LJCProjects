// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Data;

namespace LJCDBMessage
{
  // Represents a row of data in a DataTable.
  internal class InfoDataRow
  {
    // Initializes an object instance.
    internal InfoDataRow()
    {
      DataTable dataTable = new DataTable("TableName");
      DataRow dataRow = dataTable.NewRow();
      DataColumn dataColumn = new DataColumn("ColumnName", typeof(string));
      int columnIndex = 1;

      #region Methods

      // Provides strongly-typed access to each of the column values in the	row.
      dataRow.Field<string>(dataColumn);

      // Provides strongly-typed access to each of the column values in the	row.
      dataRow.Field<string>(columnIndex);

      // Provides strongly-typed access to each of the column values in the	row.
      dataRow.Field<string>("ColumnName");

      // Gets a value that indicates whether the specified DataColumn contains a
      // null value.
      dataRow.IsNull(dataColumn);

      // Gets a value that indicates whether the column at the specified index
      // contains a null value.
      dataRow.IsNull(columnIndex);

      // Gets a value that indicates whether the named column contains a null
      // value.
      dataRow.IsNull("ColumnName");

      // Sets a new value for the specified column in the DataRow.
      dataRow.SetField<string>(dataColumn, "NewValue");

      // Sets a new value for the column with the specifiec index in the
      // DataRow.
      dataRow.SetField<string>(columnIndex, "NewValue");

      // Sets a new value for the specified column in the DataRow.
      dataRow.SetField<string>("ColumnName", "NewValue");

      // Returns a string that represents the current object.
      dataRow.ToString();
      #endregion

      #region Properties

      // Gets a value that indicates whether there are errors in a row.
      //bool hasErrors = dataRow.HasErrors;
      #endregion
    }
  }
}
