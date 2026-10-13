// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestTableKeyManager5.cs
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the TableKeyManager object.
  internal class TestTableKeyManager
  {
    #region Properties

    // Gets or sets the TestCommon reference.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestTableKeyManager.xml'
    ///  path='members/Constructor/*'/>
    public TestTableKeyManager()
    {
      TestCommon = new LJCTestCommon("TestTableKeyManager");
      Run();
    }
    #endregion

    #region Methods

    // Run the tests.
    /// <include file='Doc/TestTableKeyManager.xml'
    ///  path='members/Run/*'/>
    private void Run()
    {
      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>DBServerName</DbServer>
      //    <Database>DatabaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Data Methods
      LoadForeignKeys();
      LoadTableKeys();
    }
    #endregion

    #region Data Methods

    // Retrieves a collection of foreign key records.
    private void LoadForeignKeys()
    {
      string methodName = "LoadForeignKeys()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var tableKeyManager = new TableKeyManager(dbServiceRef, dataConfigName)
      {
        TableName = "DataTable"
      };

      // Test Method
      var foreignKeys = tableKeyManager.LoadForeignKeys();

      var result = "";
      if (LJC.HasListItems(foreignKeys))
      {
        var foreignKey = foreignKeys[0];
        result = foreignKey.TargetTable;
      }
      var compare = "DataModule";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieves a collection of table key records.
    private void LoadTableKeys()
    {
      string methodName = "LoadTableKeys()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var tableKeyManager = new TableKeyManager(dbServiceRef, dataConfigName)
      {
        TableName = "DataTable"
      };

      // Test Method
      // Defaults to "PRIMARY KEY";
      var tableKeys = tableKeyManager.LoadTableKeys();

      var result = "";
      if (LJC.HasListItems(tableKeys))
      {
        var tableKey = tableKeys[0];
        result = tableKey.ColumnName;
      }
      var compare = "DbId";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
