// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Data;

namespace LJCDBMessage
{
  // Represents the schema of a column in a DataTable.
  internal class InfoDataColumn
  {
    // Initializes an object instance.
    internal InfoDataColumn()
    {
      DataColumn dataColumn = new DataColumn("ColumnName", typeof(string));

      #region Methods

      // Gets the Expression of the column, if one exists.
      dataColumn.ToString();
      #endregion

      #region Properties

      // Gets or sets a value that indicates whether null values are allowed
      // in this column for rows that belong to the table.
      dataColumn.AllowDBNull = true;

      // Gets a value that indicates whether the column automatically
      // increments the value of the column for new rows added to the table.
      dataColumn.AutoIncrement = true;

      // Gets or sets the caption for the column.
      dataColumn.Caption = "Caption";

      // Gets or sets the name of the column in the DataColumnCollection.
      dataColumn.ColumnName = "ColumnName";

      // Gets or sets the type of data stored in the column.
      dataColumn.DataType = typeof(string);

      // Gets or sets the default value for the column when you are creating
      // new rows.
      dataColumn.DefaultValue = 0;

      // Gets or sets the maximum length of a text column.
      dataColumn.MaxLength = 1;

      // Gets or sets a value that indicates whether the values in each row of the
      // column must be unique.
      dataColumn.Unique = true;
      #endregion
    }
  }
}
