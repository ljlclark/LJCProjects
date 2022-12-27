// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataManagerTemplate.cs
using System;
using LJCDBClientLib;
using LJCDBMessage;

// #SectionBegin Class
// #Value _ClassName_
// #Value _CollectionName_
// #Value _JoinTable_
// #Value _Namespace_
// #Value _TableName_
namespace _Namespace_
{
  /// <summary>Provides _TableName_ DataManager data methods.</summary>
  public class _ClassName_DataManager : DataManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/DataManager.xml'/>
    public _ClassName_DataManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "_ClassName_", string schemaName = null)
      : base(dbServiceRef, dataConfigName, tableName, schemaName)
    {
      // Map table names with property names or captions
      // that differ from the column names.
      MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID, caption: "_ClassName_ ID");

      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        _ClassName_.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        _ClassName_.ColumnName
      });
    }
    #endregion

    #region Retrieve/Load Methods

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_ RetrieveWithID(int id, List<string> columnNames = null)
    {
      _ClassName_ retValue;

      var keyRecord = GetIDKey(id);
      retValue = Retrieve(keyRecord, columnNames);
      return retValue;
    }

    // Retrieves a Data Record with the supplied name value.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_ RetrieveWithName(string name, List<string> columnNames = null)
    {
      _ClassName_ retValue;

      var keyColumns = GetNameKey(name);
      retValue = Retrieve(keyColumns, columnNames);
      return retValue;
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

    #region ObjectManager Related Public Create Data Methods

    // Creates the object from the first data row. 
    /// <include path='items/CreateObject/*' file='../../LJCDocLib/Common/DataManager.xml'/>
    public _ClassName_ Create_ClassName_(DbResult dbResult)
    {
      ResultConverter<_ClassName_, _CollectionName_> dataConverter;
      _ClassName_ retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        dataConverter = new ResultConverter<_ClassName_, _CollectionName_>();
        retValue = dataConverter.CreateData(dbResult.DbRecords[0]);
      }
      return retValue;
    }

    // Creates the collection from a DbResult object. 
    /// <include path='items/CreateCollection/*' file='../../LJCDocLib/Common/DataManager.xml'/>
    public _CollectionName_ Create_CollectionName_(DbResult dbResult)
    {
      ResultConverter<_ClassName_, _CollectionName_> dataConverter;
      _CollectionName_ retValue = null;

      //if (dbResult != null && dbResult.HasRecords)
      if (DbResult.HasRows(dbResult))
      {
        dataConverter = new ResultConverter<_ClassName_, _CollectionName_>();
        retValue = dataConverter.CreateCollection(dbResult);
      }
      return retValue;
    }
    #endregion
  }
}
// #SectionEnd Class
