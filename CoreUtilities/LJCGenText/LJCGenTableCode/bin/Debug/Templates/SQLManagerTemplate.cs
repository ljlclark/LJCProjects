// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLManagerTemplate.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

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
        var dataDefinition = baseDataDefinition.Clone();

        // Map table names with property names or captions
        // that differ from the column names.
        baseDataDefinition.MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID);
        dataDefinition.MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID);

        // Create the list of DB Assigned and Lookup column names.
        SQLManager.DbAssignedColumns = new List<string>()
        {
          _ClassName_.ColumnID
        };
        SQLManager.SetLookupColumns(new string[]
        {
          _ClassName_.ColumnName
        });

        ResultConverter = new ResultConverter<_ClassName_, _CollectionName_>();
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
    public _ClassName_ Add(_ClassName_ dataObject, List<string> propertyNames = null)
    {
      _ClassName_ retValue = null;

      // The data record must not contain a value for DB Assigned columns.
      DataTable dataTable = SQLManager.Add(dataObject, propertyNames);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
      if (dataTable != null)
      {
        retValue = Create_ClassName_(dataTable);
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
    public _CollectionName_ Load(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _CollectionName_ retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyColumns, propertyNames
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
    public _ClassName_ Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _ClassName_ retValue = null;

      DataTable dataTable = SQLManager.GetDataTable(keyColumns, propertyNames
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
    public void Update(_ClassName_ dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      SQLManager.Update(dataObject, keyColumns, propertyNames, filters);
      AffectedCount = SQLManager.AffectedCount;
      SQLStatement = SQLManager.SQLStatement;
    }
    #endregion

    #region DataManager and ObjectManager Related Create Data Methods

    // Creates the object from the first data row.
    private _ClassName_ Create_ClassName_(DataTable dataTable)
    {
      _ClassName_ retValue = null;

      if (dataTable.Rows != null && dataTable.Rows.Count > 0)
      {
        retValue = ResultConverter.CreateDataFromTable(dataTable, dataTable.Rows[0]
          , SQLManager.DataDefinition);
      }
      return retValue;
    }

    // Creates the collection from a DataTable.
    private _CollectionName_ Create_CollectionName_(DataTable dataTable)
    {
      _CollectionName_ retValue = null;

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
    public ResultConverter<_ClassName_, _CollectionName_> ResultConverter { get; set; }

    /// <summary>Gets the SQLManager reference.</summary>
    public SQLManager SQLManager { get; private set; }

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string SQLStatement { get; set; }
    #endregion
  }
}
// #SectionEnd Class
