// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MySqlDataAccess.cs
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using LJCNetCommon;
using MySql.Data.MySqlClient;

namespace LJCDataAccess
{
  /// <summary>Implements an ADO.NET MySQL data access control layer.</summary>
  public class MySqlDataAccess
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/MySqlDataAccessC/*' file='Doc/MySqlDataAccess.xml'/>
    public MySqlDataAccess(string connectionString)
    {
      ConnectionString = connectionString;
    }
    #endregion

    #region Public Methods

    // Executes an Insert, Update or Delete statement.
    /// <include path='items/ExecuteNonQuery/*' file='Doc/MySqlDataAccess.xml'/>
    public int ExecuteNonQuery(string sql)
    {
      MySqlConnection connection;
      MySqlCommand command;
      int retValue = 0;

      if (false == NetString.HasValue(ConnectionString))
      {
        string errorText = "The MySqlDataAccess.ConnectionString value"
          + " is not set.";
        throw new MissingMemberException(errorText);
      }

      // Create connection object.
      connection = new MySqlConnection(ConnectionString);

      try
      {
        using (command = new MySqlCommand(sql, connection))
        {
          connection.Open();
          retValue = command.ExecuteNonQuery();
        }
      }
      finally
      {
        connection.Close();
      }
      return retValue;
    }

    // Executes a Select statement and fills the specified DataTable.
    /// <include path='items/FillDataTable/*' file='Doc/MySqlDataAccess.xml'/>
    public void FillDataTable(string sql, DataTable dataTable
      , DataTableMappingCollection tableMapping = null)
    {
      MySqlConnection connection;
      MySqlCommand command;
      MySqlDataAdapter dataAdapter;

      // Create connection object.
      connection = new MySqlConnection(ConnectionString);

      using (command = new MySqlCommand(sql, connection))
      {
        dataAdapter = new MySqlDataAdapter(command);
        DataCommon.SetTableMappingMySql(dataAdapter, tableMapping);
        dataAdapter.Fill(dataTable);
      }
    }

    // Executes a Select statement and retrieves the MySqlDataReader object.
    /// <include path='items/GetDataReader/*' file='Doc/MySqlDataAccess.xml'/>
    public MySqlDataReader GetDataReader(string sql)
    {
      MySqlConnection connection;
      MySqlCommand command;
      MySqlDataReader retValue;

      if (false == NetString.HasValue(ConnectionString))
      {
        string errorText = "The MySqlDataAccess.ConnectionString value"
          + " is not set.";
        throw new MissingMemberException(errorText);
      }

      // Create connection object.
      connection = new MySqlConnection(ConnectionString);

      // Create command.
      command = new MySqlCommand
      {
        CommandText = sql,
        Connection = connection
      };

      // Open Connection or retrieve from pool.
      connection.Open();

      // Create reader.
      retValue = command.ExecuteReader();

      return retValue;
    }

    // Executes a Select statement and retrieves the DataSet object.
    /// <include path='items/GetDataSet/*' file='Doc/MySqlDataAccess.xml'/>
    public DataSet GetDataSet(string sql
      , DataTableMappingCollection tableMapping = null)
    {
      MySqlConnection connection;
      MySqlCommand command;
      MySqlDataAdapter dataAdapter;
      DataSet retValue = null;

      // Create connection object.
      connection = new MySqlConnection(ConnectionString);

      using (command = new MySqlCommand(sql, connection))
      {
        dataAdapter = new MySqlDataAdapter(command);
        DataCommon.SetTableMappingMySql(dataAdapter, tableMapping);
        retValue = new DataSet();
        dataAdapter.Fill(retValue);
      }
      return retValue;
    }

    // Executes a Select statement and retrieves the DataTable object.
    /// <include path='items/GetDataTable/*' file='Doc/MySqlDataAccess.xml'/>
    public DataTable GetDataTable(string sql
      , DataTableMappingCollection tableMapping = null)
    {
      DataTable retValue = null;

      DataSet dataSet = GetDataSet(sql, tableMapping);
      if (dataSet != null && dataSet.Tables.Count > 0)
      {
        retValue = dataSet.Tables[0];
      }
      return retValue;
    }

    // Executes a Select statement and retrieves the DataTable object.
    /// <include path='items/GetDataTableFromReader/*' file='Doc/MySqlDataAccess.xml'/>
    public DataTable GetDataTableFromReader(string sql)
    {
      MySqlDataReader reader;
      DataColumn lookupDataColumn;
      string columnName;
      Type columnType;
      int maxLength;
      string value;
      DataTable retValue = new DataTable();

      reader = GetDataReader(sql);
      if (reader != null)
      {
        bool first = true;
        while (reader.Read())
        {
          DataRow dataRow = retValue.NewRow();
          for (int index = 0; index < reader.FieldCount; index++)
          {
            columnName = reader.GetName(index);
            columnType = reader[index].GetType();
            maxLength = 0;
            value = reader[index].ToString();
            if (columnType == typeof(string))
            {
              maxLength = value.Length;
              lookupDataColumn = retValue.Columns[columnName];
              if (lookupDataColumn != null
                && maxLength > lookupDataColumn.MaxLength)
              {
                lookupDataColumn.MaxLength = maxLength;
              }
            }

            if (first)
            {
              DataColumn dataColumn = new DataColumn()
              {
                ColumnName = columnName,
                DataType = columnType
              };
              if (maxLength > 0)
              {
                dataColumn.MaxLength = maxLength;
              }
              retValue.Columns.Add(dataColumn);
            }

            dataRow[columnName] = value;
          }
          retValue.Rows.Add(dataRow);
          first = false;
        }
      }

      if (reader != null)
      {
        reader.Close();
      }
      return retValue;
    }

    // Executes a Stored Procedure and retrieves the DataTable object.
    /// <include path='items/GetProcedureDataTable/*' file='Doc/MySqlDataAccess.xml'/>
    public DataTable GetProcedureDataTable(string procedureName
      , ProcedureParameters parameters)
    {
      MySqlConnection connection = null;
      MySqlCommand command;
      MySqlDataAdapter dataAdapter;
      DataTable retValue = null;

      try
      {
        // Create connection object.
        connection = new MySqlConnection(ConnectionString);

        command = new MySqlCommand(procedureName, connection)
        {
          CommandType = CommandType.StoredProcedure
        };
        foreach (ProcedureParameter parameter in parameters)
        {
          MySqlParameter parm = new MySqlParameter()
          {
            ParameterName = parameter.ParameterName,
            MySqlDbType = parameter.MySqlDbType,
            Size = parameter.Size,
            Direction = parameter.Direction,
            Value = parameter.Value
          };
          command.Parameters.Add(parm);
        }

        dataAdapter = new MySqlDataAdapter(command);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        if (dataSet != null && dataSet.Tables.Count > 0)
        {
          retValue = dataSet.Tables[0];
        }
      }
      finally
      {
        if (connection != null)
        {
          connection.Close();
        }
      }
      return retValue;
    }

    // Retrieves the DataTable object with schema only.
    /// <include path='items/GetSchemaOnly/*' file='Doc/MySqlDataAccess.xml'/>
    public DataTable GetSchemaOnly(string sql
      , DataTableMappingCollection tableMapping = null)
    {
      MySqlConnection connection;
      MySqlCommand command;
      MySqlDataAdapter dataAdapter;
      DataSet dataSet;
      DataTable retValue = null;

      if (false == NetString.HasValue(ConnectionString))
      {
        string errorText = "The MySqlDataAccess.ConnectionString value"
          + " is not set.";
        throw new MissingMemberException(errorText);
      }

      // Create connection object.
      connection = new MySqlConnection(ConnectionString);

      using (command = new MySqlCommand(sql, connection))
      {
        dataAdapter = new MySqlDataAdapter(command)
        {
          MissingSchemaAction = MissingSchemaAction.AddWithKey
        };
        DataCommon.SetTableMappingMySql(dataAdapter, tableMapping);
        dataSet = new DataSet();
        dataAdapter.FillSchema(dataSet, SchemaType.Source);
        if (dataSet != null && dataSet.Tables.Count > 0)
        {
          retValue = dataSet.Tables[0];
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConnectionString value.</summary>
    public string ConnectionString { get; set; }
    #endregion
  }
}
