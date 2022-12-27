// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLManagerTemplate.cs
using System;
using System.Collections.Generic;
using System.Data;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

// #SectionBegin Class
// #Value _ClassName_
// #Value _CollectionName_
// #Value _JoinTable_
// #Value _Namespace_
// #Value _TableNames_
namespace _Namespace_
{
  /// <summary>Provides _TableName_ SQL data methods.</summary>
  public class _ClassName_SQLManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public _ClassName_SQLManager(string dataConfigName)
    {
      SQLManager = new SQLManager(dataConfigName, "_ClassName_");
      if (SQLManager.DataDefinition != null)
      {
        var baseDataDefinition = SQLManager.DataDefinition;
        var dataDefinition = SQLManager.DataDefinition;

        // Map table names with property names or captions
        // that differ from the column names.
        baseDataDefinition.MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID);
        dataDefinition.MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID);

        // Create the list of lookup column names.
        SQLManager.SetLookupColumns(new string[]
        {
          _ClassName_.ColumnName
        });
      }
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { _ClassName_.ColumnID, id }
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
        { _ClassName_.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region DataManager and ObjectManager Related Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public _ClassName_ Add(_ClassName_ dataObject, List<string> columnNames = null)
    {
      _ClassName_ retValue = null;

      // The data record must not contain a value for DB Assigned columns.
      DataTable dataTable = SQLManager.Add(dataObject, columnNames);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = CreatePerson(dataTable);
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
    public _CollectionName_ Load(DbColumns keyRecord
      , List<string> columnNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _CollectionName_ retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyRecord, columnNames
        , filters, joins);
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = Create_CollectionName_(dataTable);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public _ClassName_ Retrieve(DbColumns keyRecord
      , List<string> columnNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _ClassName_ retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyRecord, columnNames
        , filters, joins);
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = Create_ClassName_(dataTable);
      }
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Update(_ClassName_ dataRecord, DbColumns keyRecord
      , List<string> columnNames = null, DbFilters filters = null)
    {
      SQLManager.Update(dataRecord, keyRecord, columnNames, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }
    #endregion

    #region DataManager and ObjectManager Related Create Data Methods

    // Creates the object from the first data row.
    private _ClassName_ Create_ClassName_(DataTable dataTable)
    {
      ResultConverter<_ClassName_, _CollectionName_> resultConverter;
      _ClassName_ retValue = null;

      if (dataTable.Rows != null && dataTable.Rows.Count > 0)
      {
        resultConverter = new ResultConverter<_ClassName_, _CollectionName_>();
        retValue = resultConverter.CreateDataFromTable(dataTable, dataTable.Rows[0]
          , SQLManager.DataDefinition);
      }
      return retValue;
    }

    // Creates the collection from a DataTable.
    private _CollectionName_ Create_CollectionName_(DataTable dataTable)
    {
      ResultConverter<_ClassName_, _CollectionName_> resultConverter;
      _CollectionName_ retValue = null;

      if (dataTable.Rows != null && dataTable.Rows.Count > 0)
      {
        resultConverter = new ResultConverter<_ClassName_, _CollectionName_>();
        retValue = resultConverter.CreateCollectionFromTable(dataTable
          , SQLManager.DataDefinition);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the SQLManager reference.</summary>
    public SQLManager SQLManager { get; private set; }

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string SQLStatement { get; set; }
    #endregion
  }
}
// #SectionEnd Class
