// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Data;

namespace LJCDBMessage
{
  // Represents one table of in-memory data.
  internal class InfoDataTable
  {
    // Initializes an object instance.
    internal InfoDataTable()
    {
      DataTable dataTable = new DataTable("TableName");
      //DataRowCollection dataRows = dataTable.Rows;

      #region Methods

      // Clears the DataTable of all data.
      dataTable.Clear();

      // Clones the structure of the DataTable.
      // Returns: A new DataTable with the same schema as the current DataTable.
      dataTable = dataTable.Clone();

      // Copies both the structure and data for this DataTable.
      //DataTable dataTableCopy = dataTable.Copy();

      //string fileName = null;
      // Reads XML schema and data into the DataTable from the specified file.
      //XmlReadMode mode = dataTable.ReadXml(fileName);

      // Reads an XML scheam into the DataTable from the specified file.
      //dataTable.ReadXmlSchema(fileName);

      // Gets the DataTable.TableName and DisplayExpression, if there is one
      // as a concatenated string. 
      dataTable.ToString();
      #endregion

      #region Properties

      // Gets the collection of columns that belong to this table.
      // DataColumn is the element.
      //DataColumnCollection columns = dataTable.Columns;

      // Gets a value indicating whether there are any errors in any of the
      // rows in any of the tables of the DataSet to which the DataTable
      // belongs.
      //bool hasErrors = dataTable.HasErrors;

      // Gets the collection of rows that belong to this table.
      //dataRows = dataTable.Rows;
      #endregion
    }
  }
}
