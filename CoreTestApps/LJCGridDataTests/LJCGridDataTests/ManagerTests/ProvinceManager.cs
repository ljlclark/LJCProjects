// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProvinceManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCGridDataTests
{
  public class ProvinceManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public ProvinceManager(string dataConfigName, string tableName
      , string connectionString = null, string providerName = null)
    {
      Reset(dataConfigName, tableName, connectionString, providerName);

      ResultConverter = new ResultConverter<Province, Provinces>();
    }

    // Resets the data access configuration.
    public void Reset(string dataConfigName, string tableName
      , string connectionString = null, string providerName = null)
    {
      if (null == tableName)
      {
        tableName = "Province";
      }

      SQLManager = new SQLManager(dataConfigName, tableName, connectionString
        , providerName);
      if (SQLManager.DataDefinition != null)
      {
        BaseDefinition = SQLManager.DataDefinition;
        DataDefinition = BaseDefinition.Clone();
      }
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public Province Add(Province dataObject, List<string> columnNames = null)
    {
      Province retValue = null;

      // The data record must not contain a value for DB Assigned columns.
      DataTable dataTable = SQLManager.Add(dataObject, columnNames);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreateProvince(dataTable);
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Delete(DbColumns keyRecord, DbFilters filters = null)
    {
      SQLManager.Delete(keyRecord, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }

    // Loads a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public Provinces Load(DbColumns keyRecord
      , List<string> columnNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      Provinces retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyRecord, columnNames
        , filters, joins);
      AffectedCount = SQLManager.AffectedCount; //?
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreateProvinces(dataTable);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public Province Retrieve(DbColumns keyRecord
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      Province retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyRecord, propertyNames
        , filters, joins);
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreateProvince(dataTable);
      }
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Update(Province dataRecord, DbColumns keyRecord
      , List<string> columnNames = null, DbFilters filters = null)
    {
      SQLManager.Update(dataRecord, keyRecord, columnNames, filters);
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
        { "ID", id }
      };
      return retValue;
    }
    #endregion

    #region Private Methods

    // Creates the object from the first data row.
    private Province CreateProvince(DataTable dataTable)
    {
      Province retValue = null;

      if (dataTable.Rows != null && dataTable.Rows.Count > 0)
      {
        retValue = ResultConverter.CreateDataFromTable(dataTable
          , dataTable.Rows[0], SQLManager.DataDefinition);
      }
      return retValue;
    }

    // Creates the collection from a DataTable.
    private Provinces CreateProvinces(DataTable dataTable)
    {
      Provinces retValue = null;

      if (dataTable.Rows != null && dataTable.Rows.Count > 0)
      {
        retValue = ResultConverter.CreateCollectionFromTable(dataTable
          , SQLManager.DataDefinition);
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
    public ResultConverter<Province, Provinces> ResultConverter { get; set; }

    /// <summary>Gets the SQLManager reference.</summary>
    public SQLManager SQLManager { get; private set; }

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string SQLStatement { get; set; }
    #endregion
  }
}
