// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLManagerTemplate.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCAppName
{
  /// <summary>Provides Province SQL data methods.</summary>
  public class ProvinceSQLManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public ProvinceSQLManager(string dataConfigName)
    {
      SQLManager = new SQLManager(dataConfigName, "Province");
      if (SQLManager.DataDefinition != null)
      {
        var baseDataDefinition = SQLManager.DataDefinition;
        var dataDefinition = baseDataDefinition.Clone();

        // Map table names with property names or captions
        // that differ from the column names.
        baseDataDefinition.MapNames(Province.ColumnID, Province.PropertyID);
        dataDefinition.MapNames(Province.ColumnID, Province.PropertyID);

        // Create the list of DB Assigned and Lookup column names.
        SQLManager.DbAssignedColumns = new List<string>()
        {
          Province.ColumnID
        };
        SQLManager.SetLookupColumns(new string[]
        {
          Province.ColumnName
        });

        ResultConverter = new ResultConverter<Province, Provinces>();
      }
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      var retValue = new DbColumns()
      {
        { Province.ColumnID, id }
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
        { Province.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region DataManager and ObjectManager Related Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public Province Add(Province dataObject, List<string> propertyNames = null)
    {
      Province retValue = null;

      // The data record must not contain a value for DB Assigned columns.
      DataTable dataTable = SQLManager.Add(dataObject, propertyNames);
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
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      SQLManager.Delete(keyColumns, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }

    // Loads a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public Provinces Load(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      Provinces retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyColumns, propertyNames
        , filters, joins);
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreateProvinces(dataTable);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public Province Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      Province retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyColumns, propertyNames
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
    public void Update(Province dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      SQLManager.Update(dataObject, keyColumns, propertyNames, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }
    #endregion

    #region DataManager and ObjectManager Related Create Data Methods

    // Creates the object from the first data row.
    private Province CreateProvince(DataTable dataTable)
    {
      Province retValue = null;

      if (dataTable.Rows != null && dataTable.Rows.Count > 0)
      {
        retValue = ResultConverter.CreateDataFromTable(dataTable, dataTable.Rows[0]
          , SQLManager.DataDefinition);
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

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<Province, Provinces> ResultConverter { get; set; }

    /// <summary>Gets the SQLManager reference.</summary>
    public SQLManager SQLManager { get; private set; }

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string SQLStatement { get; set; }
    #endregion
  }
}
