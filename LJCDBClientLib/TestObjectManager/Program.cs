// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// Program.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCDBDataAccessLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace TestObjectManager
{
  // A program to test the DataObject class.
  /// <include path='items/Main/*' file='Doc/ProjectTestObjectManager.xml'/>
  internal class Program
  {
    // The program entry point function.
    private static void Main()
    {
      TestObjectDataAccess();

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Main Test Functions

    // Test ObjectManager.
    private static void TestObjectDataAccess()
    {
      DbConnectionStringBuilder connectionBuilder;
      DbDataAccess dbDataAccess;

      string databaseName = "LJCData";
      connectionBuilder = new DbConnectionStringBuilder
      {
        { "Data Source", "DESKTOP-PDPBE34\\SQL2016" },
        { "Initial Catalog", databaseName },
        { "Integrated Security", "True"}
      };
      string connectionString = connectionBuilder.ConnectionString;
      string providerName = "System.Data.SqlClient";
      dbDataAccess = new DbDataAccess(databaseName, connectionString
        , providerName);

      // Create the Data Access object for the desired communication type.
      //string endPointConfigurationName = "NetTcpBinding_IDbService";
      DbServiceRef dbServiceRef = new DbServiceRef()
      {
        // Direct Message Data Object communication.
        DbDataAccess = dbDataAccess,

        // Direct Database Service.
        //DbService = new DbService(),

        // Remote Database Service with Client Proxy.
        //DbServiceClient = new DbServiceClient(endPointConfigurationName)
      };

      // Create the ObjectManager.
      //  - New() Creates the DataDefinition object.
      //  - Methods create the DbRequest object with all data and key columns.
      //  - Methods set and returns the database assigned values.
      ObjectManager<Person, Persons> personManager;
      personManager = new ObjectManager<Person, Persons>(dbServiceRef, null
        , "PersonTest");

      if (personManager != null)
      {
        // Map table names with property names or captions
        // that differ from the column names.
        //personManager.MapNames("Id", "ID");

        // Create the list of database assigned columns.
        // And make sure the AutoIncrement value is set.
        personManager.SetDbAssignedColumns(new string[]
          {
            "ID"
          });

        // Create the list of lookup column names.
        personManager.SetLookupColumns(new string[]
          {
            "Name"
          });

        //Persons persons = Load(personManager);
        Add(personManager);
        //Person person = Retrieve(personManager);
        Retrieve(personManager);
        //persons = Load(personManager);
        //LoadProcedure();
        Update(personManager);
        //persons = Load(personManager);
        Load(personManager);
        Delete(personManager);
        //persons = Load(personManager);

        Add(personManager);
        //person = RetrieveClientSql(personManager);
        RetrieveClientSql(personManager);
        //persons = LoadClientSql(personManager);
        LoadClientSql(personManager);
        ExecuteClientSqlUpdate(personManager);
        //persons = LoadClientSql(personManager);
        ExecuteClientSqlDelete(personManager);
        //persons = LoadClientSql(personManager);
      }
    }
    #endregion

    #region Individual Test Functions

    // Adds a Person record.
    private static void Add(ObjectManager<Person, Persons> personManager)
    {
      // The inserted columns must not include the DB assigned columns.
      Person personData = new Person()
      {
        Name = "TestName",
        PrincipleFlag = false
      };

      // Insert record and return the database assigned values.
      Person resultPerson = personManager.Add(personData);
      if (resultPerson != null)
      {
        personData.ID = resultPerson.ID;
      }
    }

    // Retrieves a Person record.
    private static Person Retrieve(ObjectManager<Person, Persons> personManager)
    {
      Person retValue;

      // Create Key Columns.
      DbColumns keyColumns = new DbColumns()
      {
        { "Name", (object)"TestName" }
      };

      // Retrieve the record.
      retValue = personManager.Retrieve(keyColumns);
      return retValue;
    }

    // Loads multiple Person objects.
    private static Persons Load(ObjectManager<Person, Persons> personManager)
    {
      Persons retValue;

      // Create Key Columns.
      //DbColumns keyColumns = new DbColumns()
      //{
      //  { "Name", (object)"TestName" }
      //};
      DbColumns keyColumns = null;

      DbJoins dbJoins = null;

      // Load the records.
      retValue = personManager.Load(keyColumns, joins: dbJoins);
      return retValue;
    }

    // Updates a record.
    private static void Update(ObjectManager<Person, Persons> personManager)
    {
      // Create the list of included columns.
      // This list must not include the DB assigned columns
      // or the database assigned columns must have the AutoIncrement
      // value set to "true".
      List<string> columnNames = new List<string>() { "Name" };

      Person personData = new Person()
      {
        Name = "TestNameUpdated"
      };

      // Create Key Columns.
      var keyColumns = new DbColumns()
      {
        { "Name", (object)"TestName" }
      };

      // Update the record.
      personManager.Update(personData, keyColumns, columnNames);
    }

    // Deletes a record.
    private static void Delete(ObjectManager<Person, Persons> personManager)
    {
      // Create Key Columns.
      var keyColumns = new DbColumns()
      {
        { "Name", (object)"TestName" }
      };

      // Delete the record.
      personManager.Delete(keyColumns);
    }

    // Selects a record with an SQL statement.
    private static Person RetrieveClientSql(ObjectManager<Person, Persons> personManager)
    {
      Person retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select * from PersonTest");
      builder.AppendLine("where Name = 'TestName'");
      string sql = builder.ToString();

      retValue = personManager.RetrieveClientSql(sql);
      return retValue;
    }

    // Selects multiple records with an SQL statement.
    private static Persons LoadClientSql(ObjectManager<Person, Persons> personManager)
    {
      Persons retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select * from PersonTest");
      string sql = builder.ToString();

      // Load the records.
      retValue = personManager.LoadClientSql(sql);
      return retValue;
    }

    // Executes an Update statement.
    private static void ExecuteClientSqlUpdate(ObjectManager<Person, Persons> personManager)
    {
      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("update PersonTest");
      builder.AppendLine("set Name = 'TestNameUpdated'");
      builder.AppendLine("where Name = 'TestName'");
      string sql = builder.ToString();

      // Update the record.
      personManager.ExecuteClientSql(sql);
    }

    // Executes a Delete statement.
    private static void ExecuteClientSqlDelete(ObjectManager<Person, Persons> personManager)
    {
      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("delete from PersonTest");
      builder.AppendLine("where Name = 'TestNameUpdated'");
      string sql = builder.ToString();

      // Update the record.
      personManager.ExecuteClientSql(sql);
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
