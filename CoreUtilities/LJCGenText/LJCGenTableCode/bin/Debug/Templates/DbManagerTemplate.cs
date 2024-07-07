// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _ClassName_
// )ClassName_DbManager.cs
// #SectionEnd Title
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

// #SectionBegin Class
// #Value _ClassName_
// #Value _CollectionName_
// #Value _JoinTable_
// #Value _Namespace_
// #Value _TableName_
namespace _Namespace_
{
  /// <summary>Provides _TableName_ DbDataAccess data methods.</summary>
  public class _ClassName_DbManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public _ClassName_DbManager(string dataConfigName)
    {
      DbManager = new DbManager(dataConfigName, "_ClassName_");
      if (DbManager.DataDefinition != null)
      {
        var dataDefinition = DbManager.DataDefinition;
        var baseDataDefinition = DbManager.BaseDataDefinition;

        // Map table names with property names or captions
        // that differ from the column names.
        baseDataDefinition.MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID);
        dataDefinition.MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID);

        // Create the list of lookup column names.
        DbManager.SetLookupColumns(new string[]
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
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
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
    public _ClassName_ Add(_ClassName_ dataRecord
      , List<string> columnNames = null)
    {
      _ClassName_ retValue = null;

      // The data record must not contain a value for DB Assigned columns.
      // The DbResult contains a record with only the DB Assigned columns.
      var dbResult = DbManager.Add(dataRecord, columnNames);
      AffectedCount = DbManager.AffectedCount;
      //if (dbResult != null)
      if (DbResult.HasRows(dbResult))
      {
        retValue = Create_ClassName_(dbResult);
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      DbManager.Delete(keyColumns, filters);
      AffectedCount = DbManager.AffectedCount;
    }

    // Loads a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public _CollectionName_ Load(DbColumns keyColumns
      , List<string> columnNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _CollectionName_ retValue = null;

      var dbResult = DbManager.Load(keyColumns, columnNames, filters, joins);
      //if (dbResult != null)
      if (DbResult.HasRows(dbResult))
      {
        retValue = Create_CollectionName_(dbResult);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/DbManager.xml'/>
    public _ClassName_ Retrieve(DbColumns keyColumns
      , List<string> columnNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _ClassName_ retValue = null;

      var dbResult = DbManager.Retrieve(keyColumns, columnNames, filters, joins);
      //if (dbResult != null)
      if (DbResult.HasRows(dbResult))
      {
        retValue = Create_ClassName_(dbResult);
      }
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Update(_ClassName_ dataRecord, DbColumns keyColumns
      , List<string> columnNames = null, DbFilters filters = null)
    {
      DbManager.Update(dataRecord, keyColumns, columnNames, filters);
      AffectedCount = DbManager.AffectedCount;
    }
    #endregion

    #region DataManager and ObjectManager Related Create Data Methods

    // Creates the object from the first data row.
    private _ClassName_ Create_ClassName_(DbResult dbResult)
    {
      ResultConverter<_ClassName_, _CollectionName_> resultConverter;
      _ClassName_ retValue = null;

      //if (dbResult != null && dbResult.HasRecords)
      if (DbResult.HasRows(dbResult))
      {
        resultConverter = new ResultConverter<_ClassName_, _CollectionName_>();
        retValue = resultConverter.CreateData(dbResult.DbRecords[0]);
      }
      return retValue;
    }

    // Creates the collection from a DbResult object.
    private _CollectionName_ Create_CollectionName_(DbResult dbResult)
    {
      ResultConverter<_ClassName_, _CollectionName_> resultConverter;
      _CollectionName_ retValue = null;

      //if (dbResult != null && dbResult.HasRecords)
      if (DbResult.HasRows(dbResult))
      {
        resultConverter = new ResultConverter<_ClassName_, _CollectionName_>();
        retValue = resultConverter.CreateCollection(dbResult);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the DbManager reference.</summary>
    public DbManager DbManager { get; private set; }

    /// <summary>Gets the last SQL statement.</summary>
    public string SQLStatement
    {
      get { return DbManager.SQLStatement; }
    }
    #endregion
  }
}
// #SectionEnd Class
