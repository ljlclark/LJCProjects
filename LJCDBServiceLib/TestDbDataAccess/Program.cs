// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using LJCDataAccess;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Data;

namespace TestDbDataAccess
{
  // A Program to test the DbDataAccess object.
  /// <include path='items/Program/*' file='Doc/ProjectDBServiceLib.xml'/>
  internal class Program
  {
    // The program entry point function.
    private static void Main()
    {
      TestDbDataAccess();

      // Testing
      //Person person = new Person()
      //{
      //	Name = "Junk"
      //};
      //LJCReflect reflect = new LJCReflect(person);
      //string value = (string)reflect.GetValue("Name");

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Main Test Functions

    // Test DbDataAccess.
    private static void TestDbDataAccess()
    {
      DbConnectionStringBuilder connectionBuilder;
      DbDataAccess dbDataAccess;
      DataAccess ljcDataAccess;

      string databaseName = "LJCData";
      connectionBuilder = new DbConnectionStringBuilder()
      {
        { "Data Source", "DESKTOP-PDPBE34\\SQL2016" },
        { "Initial Catalog", databaseName },
        { "Integrated Security", "True" }
      };
      string connectionString = connectionBuilder.ConnectionString;
      string providerName = "System.Data.SqlClient";
      dbDataAccess = new DbDataAccess(databaseName, connectionString
        , providerName);

      // Get the table data definition.
      DbColumns dataDefinition = SchemaOnly(dbDataAccess);
      dataDefinition.LJCSerialize("DbColumnsLayout.xml");
      var dbColumn = dataDefinition.LJCSearchPropertyName("Id");
      dbColumn.PropertyName = "ID";

      //Persons persons = Load(dbDataAccess, dataDefinition);
      Add(dbDataAccess, dataDefinition);
      //Person person = Retrieve(dbDataAccess, dataDefinition);
      Retrieve(dbDataAccess, dataDefinition);
      //persons = Load(dbDataAccess, dataDefinition);
      Load(dbDataAccess, dataDefinition);
      Update(dbDataAccess, dataDefinition);
      //persons = Load(dbDataAccess, dataDefinition);
      Delete(dbDataAccess, dataDefinition);
      //persons = Load(dbDataAccess, dataDefinition);

      Add(dbDataAccess, dataDefinition);
      //person = RetrieveClientSql(dbDataAccess, dataDefinition);
      RetrieveClientSql(dbDataAccess, dataDefinition);
      //persons = LoadClientSql(dbDataAccess, dataDefinition);
      LoadClientSql(dbDataAccess, dataDefinition);
      ExecuteClientSql(dbDataAccess, dataDefinition);
      //persons = LoadClientSql(dbDataAccess, dataDefinition);
      Delete(dbDataAccess, dataDefinition);
      //persons = LoadClientSql(dbDataAccess, dataDefinition);

      ljcDataAccess = new DataAccess(connectionString, providerName);
      //DataTable dataTable = GetDataTable(ljcDataAccess);
      GetDataTable(ljcDataAccess);
      //ResultConverter<Person, Persons> resultConverter;
      //resultConverter = new ResultConverter<Person, Persons>();
      //persons = resultConverter.CreateCollectionFromTable(dataTable, dataDefinition);
    }
    #endregion

    #region Individual Test Functions

    // Create the Data Definition from the table schema.
    private static DbColumns SchemaOnly(DbDataAccess dbDataAccess)
    {
      DbColumns retValue;

      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.SchemaOnly.ToString(),
        TableName = "PersonTest"
      };

      DbResult dbResult = dbDataAccess.Execute(dbRequest);
      retValue = dbResult.Columns;
      return retValue;
    }

    // Adds a Person record.
    private static void Add(DbDataAccess dbDataAccess
      , DbColumns dataDefinition)
    {
      //DbColumns includedColumns;

      // Create the list of included columns.
      // This list must not include the DB assigned columns
      // or the database assigned columns must have the AutoIncrement
      // value set to "true".
      List<string> columnNames = new List<string>();
      foreach (LJCNetCommon.DbColumn column in dataDefinition)
      {
        if (column.ColumnName != "Id")
        {
          columnNames.Add(column.ColumnName);
        }
      }
      //includedColumns = dataDefinition.LJCGetColumns(columnNames);

      // The inserted columns must not include the DB assigned columns.
      Person dataRecord = new Person()
      {
        Name = "TestName",
        PrincipleFlag = false
      };

      // Create a Data Columns object with the included data definitions
      // and values from the data record.
      DbColumns requestColumns = DbCommon.RequestDataColumns(dataRecord
        , dataDefinition, columnNames);

      // This code is needed only if there are database assigned columns.
      DbColumns dbAssignedColumns = null;
      DbColumns lookupColumns = GetLookupColumns(dataRecord, dataDefinition
        , ref dbAssignedColumns);

      // Create request with columns containing values from the record.
      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.Insert.ToString(),
        TableName = "PersonTest",
        Columns = requestColumns,
        KeyColumns = lookupColumns,
        DbAssignedColumns = dbAssignedColumns
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;

        // This code is needed only if there are database assigned columns.
        if (dbResult.HasRows())
        {
          SetAssignedValues(dataRecord, dbResult.Rows[0].Values);
        }
      }
    }

    // Deletes a record.
    private static void Delete(DbDataAccess dbDataAccess
      , DbColumns _)
    {
      // Create Key Columns.
      var keyColumns = new DbColumns()
      {
        { "Name", (object)"TestNameUpdated" }
      };

      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.Delete.ToString(),
        TableName = "PersonTest",
        KeyColumns = keyColumns
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
      }
    }

    // Executes a non-query SQL statement.
    private static void ExecuteClientSql(DbDataAccess dbDataAccess, DbColumns _)
    {
      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("update PersonTest");
      builder.AppendLine("set Name = 'TestNameUpdated'");
      builder.AppendLine("where Name = 'TestName'");
      string sql = builder.ToString();

      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.ExecuteSQL.ToString(),
        TableName = "PersonTest",
        ClientSql = sql
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
        //int affectedCount = dbResult.AffectedRecords;
      }
    }

    // Executes a Select statement and retrieves the DataTable object.
    private static DataTable GetDataTable(DataAccess ljcDataAccess)
    {
      DataTable retValue = null;

      string sql = "select * from PersonTest";
      try
      {
        retValue = ljcDataAccess.GetDataTable(sql);
      }
      catch (Exception e)
      {
        // Handle error message here.
        Console.WriteLine(e.Message);
      }

      if (retValue != null)
      {
        //foreach (DataRow dataRow in retValue.Rows)
        //{
        //  for (int index = 0; index < retValue.Columns.Count; index++)
        //  {
        //    string value = dataRow[index].ToString();
        //  }
        //}
      }
      return retValue;
    }

    // Gets the key columns if there are database assigned columns.
    private static DbColumns GetLookupColumns(Person _
      , DbColumns dataDefinition, ref DbColumns dbAssignColumns)
    {
      DbColumns retValue;

      dbAssignColumns = dataDefinition.LJCGetColumns(new List<string>()
      {
        "Id"
      });

      retValue = dataDefinition.LJCGetColumns(new List<string>()
      {
        "Name"
      });
      return retValue;
    }

    // Selects multiple records with an SQL statement.
    private static Persons LoadClientSql(DbDataAccess dbDataAccess
      , DbColumns dataDefinition)
    {
      Persons retValue = null;

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select * from PersonTest");
      string sql = builder.ToString();

      DbColumns dataColumns = null;
      bool isColumnModifications = true;
      if (isColumnModifications)
      {
        // Create the list of included columns.
        // This list should include the database assigned columns.
        DbColumns includedColumns = dataDefinition.Clone();

        // Create a Data Columns object with the included data definitions
        // and values from the data record.
        dataColumns = includedColumns.Clone();
      }

      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.LoadSQL.ToString(),
        TableName = "PersonTest",
        Columns = dataColumns,
        ClientSql = sql
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
        if (dbResult.HasRows())
        {
          retValue = GetPersons(dbResult);
        }
      }
      return retValue;
    }

    // Loads multiple Person objects.
    private static Persons Load(DbDataAccess dbDataAccess
      , DbColumns dataDefinition)
    {
      Persons retValue = null;

      // Create the list of included columns.
      // This list should include the database assigned columns.
      DbColumns includedColumns = dataDefinition.Clone();

      // Create a Data Columns object with the included data definitions
      // and values from the data record.
      DbColumns dataColumns = includedColumns.Clone();

      // Create Key Columns.
      //var keyColumns = new DbColumns()
      //{
      //  { "Name", (object)"TestName" }
      //};

      //var requestKeyColumns = DbCommon.RequestKeys(keyColumns, dataDefinition);

      // Create request with retrieved columns.
      // The retrieved columns should include the DB assigned columns.
      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.Load.ToString(),
        TableName = "PersonTest",
        Columns = dataColumns
        //KeyColumns = requestKeyColumns
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
        if (dbResult.HasRows())
        {
          retValue = GetPersons(dbResult);
        }
      }
      return retValue;
    }

    //
    private static Persons GetPersons(DbResult dbResult)
    {
      Persons retValue = null;

      if (dbResult.HasRows())
      {
        retValue = new Persons();
        foreach (DbRow dbRow in dbResult.Rows)
        {
          if (DbValues.HasItems(dbRow.Values))
          {
            Person person = new Person();
            DbCommon.SetObjectValues(dbRow.Values, person);
            retValue.Add(person);
          }
        }
      }
      return retValue;
    }

    // Selects a record with an SQL statement.
    private static Person RetrieveClientSql(DbDataAccess dbDataAccess
      , DbColumns dataDefinition)
    {
      Person retValue = null;

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select * from PersonTest");
      builder.AppendLine("where Name = 'TestName'");
      string sql = builder.ToString();

      DbColumns dataColumns = null;
      bool isColumnModifications = true;
      if (isColumnModifications)
      {
        // Create the list of included columns.
        // This list should include the database assigned columns.
        DbColumns includedColumns = dataDefinition.Clone();

        // Create a Data Columns object with the included data definitions
        // and values from the data record.
        dataColumns = includedColumns.Clone();
      }

      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.RetrieveSQL.ToString(),
        TableName = "PersonTest",
        Columns = dataColumns,
        ClientSql = sql
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
        if (dbResult.HasRows())
        {
          retValue = new Person();
          DbCommon.SetObjectValues(dbResult.Rows[0].Values, retValue);
        }
      }
      return retValue;
    }

    // Retrieves a Person object.
    private static Person Retrieve(DbDataAccess dbDataAccess
      , DbColumns dataDefinition)
    {
      Person retValue = null;

      // Create the list of included columns.
      // This list should include the database assigned columns.
      DbColumns includedColumns = dataDefinition.Clone();

      // Create a Data Columns object with the included data definitions
      // and values from the data record.
      DbColumns dataColumns = includedColumns.Clone();

      // Create Key Columns.
      var keyColumns = new DbColumns()
      {
        { "Name", (object)"TestName" }
      };

      var requestKeyColumns = DbCommon.RequestKeys(keyColumns, dataDefinition);

      // Create a request with the retrieve columns.
      // The retrieved columns should include the DB assigned columns.
      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.Select.ToString(),
        TableName = "PersonTest",
        Columns = dataColumns,
        KeyColumns = requestKeyColumns
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
        if (dbResult.HasRows())
        {
          retValue = new Person();
          DbCommon.SetObjectValues(dbResult.Rows[0].Values, retValue);
        }
      }
      return retValue;
    }

    // Set the database assigned values.
    private static void SetAssignedValues(Person person, DbValues resultRecord)
    {
      Person addedPerson = new Person();
      DbCommon.SetObjectValues(resultRecord, addedPerson);
      person.ID = addedPerson.ID;
    }

    // Updates a record.
    private static void Update(DbDataAccess dbDataAccess
      , DbColumns dataDefinition)
    {
      //DbColumns includedColumns;

      // Create the list of included columns.
      // This list must not include the DB assigned columns
      // or the database assigned columns must have the AutoIncrement
      // value set to "true".
      List<string> columnNames = new List<string>();
      foreach (LJCNetCommon.DbColumn column in dataDefinition)
      {
        if (column.ColumnName != "Id")
        {
          columnNames.Add(column.ColumnName);
        }
      }
      //includedColumns = dataDefinition.LJCGetColumns(columnNames);

      Person dataRecord = new Person()
      {
        Name = "TestNameUpdated"
      };

      // Create a Data Columns object with the included data definitions
      // and values from the data record.
      var columns = DbCommon.RequestDataColumns(dataRecord, dataDefinition
        , columnNames);

      // Create Key Columns.
      var keyColumns = new DbColumns()
      {
        { "Name", (object)"TestName" }
      };

      var requestKeyColumns = DbCommon.RequestKeys(keyColumns, dataDefinition);

      // Create request with columns containing values from the record.
      // The updated columns must not include the DB assigned columns.
      DbRequest dbRequest = new DbRequest()
      {
        RequestTypeName = RequestType.Update.ToString(),
        TableName = "PersonTest",
        Columns = columns,
        KeyColumns = requestKeyColumns
      };
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      if (dbResult != null)
      {
        //string sqlStatement = dbResult.ExecutedSql;
        //int affectedCount = dbResult.AffectedRecords;
      }
    }
    #endregion

    // A Person Data Record class.
    public class Person
    {
      public Int32 ID { get; set; }
      public string Name { get; set; }
      public bool PrincipleFlag { get; set; }
    }
    public class Persons : List<Person> { }
  }
}
