// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// AppProgramSQLManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCAppName
{
  /// <summary>Provides AppProgram SQL data methods.</summary>
  public class AppProgramSQLManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public AppProgramSQLManager(string dataConfigName, string tableName
      , string connectionString = null, string providerName = null)
    {
      Reset(dataConfigName, tableName, connectionString, providerName);
    }
  }

  /// <summary>Resets the data access configuration.</summary>
  public void Reset(string dataConfigName, string tableName
    , string connectionString = null, string providerName = null)
  {
    if (null == tableName)
    {
      tableName = "AppProgram";
    }

    SQLManager = new SQLManager(dataConfigName, tableName, connectionString
      , providerName);
    if (SQLManager.DataDefinition != null)
    {
      BaseDataDefinition = SQLManager.DataDefinition;
      DataDefinition = baseDataDefinition.Clone();

      // Map table names with property names or captions
      // that differ from the column names.
      baseDataDefinition.MapNames(AppProgram.ColumnID, AppProgram.PropertyID);
      dataDefinition.MapNames(AppProgram.ColumnID, AppProgram.PropertyID);

      // Create the list of DB Assigned and Lookup column names.
      SQLManager.DbAssignedColumns = new List<string>()
      {
        AppProgram.ColumnID
      };
      SQLManager.SetLookupColumns(new string[]
      {
        AppProgram.ColumnName
      });

      ResultConverter = new ResultConverter<AppProgram, AppPrograms>();
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public AppProgram Add(AppProgram dataObject, List<string> propertyNames = null)
    {
      AppProgram retValue = null;

      // The data record must not contain a value for DB Assigned columns.
      DataTable dataTable = SQLManager.Add(dataObject, propertyNames);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreateAppProgram(dataTable);
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      SQLManager.Delete(keyColumns, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }

    // Loads a collection of data records and returns a Data Collection.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public AppPrograms Load(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      AppPrograms retValue = null;

      DataTable dataTable = LoadDataTable(keyColumns, propertyNames
        , filters, joins);
      if (dataTable != null)
      {
        retValue = CreateAppPrograms(dataTable);
      }
      return retValue;
    }

    /// <summary>Loads a collection of data records and returns a DataTable.</summary>
    public DataTable LoadDataTable(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataTable retValue;

      retValue = SQLManager.GetDataTable(keyColumns, propertyNames
        , filters, joins);
      SQLStatement = SQLManager.SQLStatement;
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public AppProgram Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      AppProgram retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyColumns, propertyNames
        , filters, joins);
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreateAppProgram(dataTable);
      }
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Update(AppProgram dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      SQLManager.Update(dataObject, keyColumns, propertyNames, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      var retValue = new DbColumns()
      {
        { AppProgram.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetNameKey(string name)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { AppProgram.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Private Methods

    // Creates the object from the first data row.
    private AppProgram CreateAppProgram(DataTable dataTable)
    {
      AppProgram retValue = null;

      if (NetCommon.HasData(dataTable))
      {
        retValue = ResultConverter.CreateDataFromTable(dataTable
          , dataTable.Rows[0], DataDefinition);
      }
      return retValue;
    }

    // Creates the collection from a DataTable.
    private AppPrograms CreateAppPrograms(DataTable dataTable)
    {
      AppPrograms retValue = null;

      if (NetCommon.HasData(dataTable))
      {
        retValue = ResultConverter.CreateCollectionFromTable(dataTable
          , DataDefinition);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

  /// <summary>Gets or sets the Base Columns definition.</summary>
  public DbColumns BaseDefinition { get; set; }

  /// <summary>Gets or sets the Data Columns definition.</summary>
  public DbColumns DataDefinition { get; set; }

  /// <summary>Gets or sets the ResultConverter reference.</summary>
  public ResultConverter<AppProgram, AppPrograms> ResultConverter { get; set; }

  /// <summary>Gets the SQLManager reference.</summary>
  public SQLManager SQLManager { get; private set; }

  /// <summary>Gets or sets the last SQL statement.</summary>
  public string SQLStatement { get; set; }
  #endregion
}
}
