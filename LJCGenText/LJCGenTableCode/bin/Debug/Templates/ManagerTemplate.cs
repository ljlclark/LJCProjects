// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// ManagerTemplate.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

// #SectionBegin Class
// #Value _ClassName_
// #Value _CollectionName_
// #Value _JoinTableName_
// #Value _Namespace_
// #Value _TableName_
namespace _Namespace_
{
  /// <summary>Provides table specific data methods.</summary>
  public class _ClassName_Manager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_Manager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "_TableName_", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<_ClassName_, _CollectionName_>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(_ClassName_.ColumnID, caption: "_ClassName_ ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      Manager.DataDefinition.Add(_JoinTableName_.ColumnDescription
        , "_JoinTableName_Description", caption: "_JoinTableName_ Description");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        _ClassName_.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        _ClassName_.ColumnName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_ Add(_ClassName_ dataObject, List<string> propertyNames = null)
    {
      _ClassName_ retValue = null;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = addedRecord.ID;
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _CollectionName_ Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _CollectionName_ retValue = null;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_ Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      _ClassName_ retValue = null;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(_ClassName_ dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Retrieve/Load Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_ RetrieveWithID(int id, List<string> propertyNames = null)
    {
      _ClassName_ retValue = null;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = Result.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_ RetrieveWithName(string name, List<string> propertyNames = null)
    {
      _ClassName_ retValue = null;

      var keyColumns = GetNameKey(name);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, joins: joins);
      retValue = Result.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
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

    // Gets the ID key columns.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetNameKey(string name)
    {
      // Needs cast to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { _ClassName_.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include path='items/GetLoadJoins/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbJoins GetJoins()
    {
      DbJoin dbJoin;
      DbJoins retValue = new DbJoins();

      // Note: JoinOn Columns must have properties in the DataObject
      // to recieve the join values.
      // The RenameAs property is required if there is another table column
      // with the same name.
      // Note: dbColumns.Add(string columnName, string propertyName = null
      // , string renameAs = null, string dataTypeName = "String", string caption = null) 

      // CVPerson.FirstName,
      // CVPerson.MiddleName,
      // CVPerson.LastName
      //left join CVPerson
      // on ((CVVisit.CVPersonID = CVPerson.ID))
      //dbJoin = new DbJoin
      //{
      //	TableName = "CVPerson",
      //	JoinType = "left",
      //	JoinOns = new DbJoinOns()
      //	{
      //		{ CVVisit.ColumnCVPersonID, CVPerson.ColumnID }
      //	},
      //	Columns = new DbColumns()
      //	{
      //    // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
      //		{ CVPerson.ColumnFirstName },
      //		{ CVPerson.ColumnMiddleName },
      //		{ CVPerson.ColumnLastName }
      //	}
      //};
      //retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<_ClassName_, _CollectionName_> ResultConverter { get; set; }
    #endregion
  }
}
