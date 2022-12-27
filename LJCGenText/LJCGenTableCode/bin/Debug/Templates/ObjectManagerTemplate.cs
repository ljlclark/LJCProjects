// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ObjectManagerTemplate.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCDBServiceLib;
using LJCNetCommon;

// #SectionBegin Class
// #Value _ClassName_
// #Value _CollectionName_
// #Value _JoinTable_
// #Value _Namespace_
// #Value _TableName_
namespace _Namespace_
{
  /// <summary>Provides _TableName_ ObjectManager data methods.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  public class _ClassName_Manager
    : ObjectManager<_ClassName_, _CollectionName_>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _ClassName_Manager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "_TableName_")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Map table names with property names or captions
      // that differ from the column names.
      //MapNames(_ClassName_.ColumnID, _ClassName_.PropertyID, caption: "ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      //DataDefinition.Add(_ClassName_.ColumnJoinDescription
      // , caption: "Join Description");

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

    // Loads a collection of data records ordered by Description.
    /// <include path='items/LoadByDescription/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public _CollectionName_ LoadByDescription(DbColumns keyColumns = null
      , List<string> columnNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      _CollectionName_ retValue;

      if (null == joins)
      {
        DbJoins joins = GetLoadJoins();
      }
      SetOrderByDescription();
      retValue = Load(keyColumns, columnNames, filters, joins);
      return retValue;
    }

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
      // Needs cast to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { _ClassName_.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region KeyItem Methods

    // Creates the RecordColumns object.
    /// <include path='items/RecordColumns/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns RecordColumns(long id)
    {
      DbColumns retValue = null;

      // Use common data definitions.
      _ClassName_ keyRecord = GetIDKey((int)id);
      var dbResult = DataManager.Retrieve(keyRecord);
      //if (dbResult != null && dbResult.DbRecords != null
      //	&& dbResult.DbRecords.Count > 0)
      if (DbResult.HasRows(dbResult))
      {
        retValue = dbResult.GetDbColumns(dbResult.DbRecords[0]);
      }
      return retValue;
    }

    // Creates the KeyItem object.
    /// <include path='items/GetKeyItem/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public KeyItem GetKeyItem(string propertyName, long id)
    {
      KeyItem retValue = null;

      var record = RetrieveWithID(id);
      if (record != null)
      {
        retValue = new KeyItem
        {
          Description = record.Name,
          ID = id,
          MaxLength = _ClassName_.LengthName,
          PrimaryKeyName = _ClassName_.ColumnID,
          PropertyName = propertyName,
          TableName = _ClassName_.TableName
        };
      }
      return retValue;
    }

    // Creates the KeyItems collection.
    /// <include path='items/GetKeyItems/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public KeyItems GetKeyItems(string propertyName
      , _ClassName_ keyRecord = null)
    {
      KeyItems retValue = null;

      var records = Load(keyRecord);
      if (records != null)
      {
        retValue = new KeyItems();
        foreach (_ClassName_ record in records)
        {
          retValue.Add(propertyName, record.ID, record.FullName
            , _ClassName_.LengthFullName);
        }
      }
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include path='items/GetLoadJoins/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbJoins GetLoadJoins()
    {
      //DbJoin dbJoin;
      DbJoins retValue = new DbJoins();

      // Note: Join Columns must have properties in the Data Object
      // to recieve the join values.
      // The RenameAs property is required if there is another table with the
      // same column name.
      // dbColumns.Add(string columnName, string propertyName = null
      // , string renameAs = null, string dataTypeName = "String", string caption = null) 

      //// [CodeType].[Description] as TypeDescription
      ////left join [CodeType]
      //// on (([MainTable].[CodeTypeID] = CodeType.ID))
      //string columnName = CodeType.ColumnDescription;
      //string propertyName = _ClassName_.ColumnTypeDescription;
      //string renameAs = _ClassName_.ColumnTypeDescription;
      //dbJoin = new DbJoin
      //{
      //	TableName = "CodeType",
      //	JoinType = "left",
      //	JoinOns = new DbJoinOns()
      //  {
      //    // fromColumnName, toColumnName
      //		{ _ClassName_.ColumnCodeTypeID, CodeType.ColumnID }
      //	},
      //	Columns = new DbColumns()
      //  {
      //		{ columnName, propertyName, renameAs }
      //	}
      //};
      //retValue.Add(dbJoin);

      //// [c].Description,
      ////left join [JoinTable] as j
      //// on [MainTable].[ForeignID] = c.ID
      //string alias = "j";
      //string joinToName = $"{alias}.{JoinTable.ColumnID}";
      //dbJoin = new DbJoin
      //{
      //	TableName = _JoinTable_.TableName,
      //	TableAlias = alias,
      //	JoinType = "left",
      //	JoinOns = new DbJoinOns() {
      //		{ _ClassName_.ColumnForeignID, joinToName }
      //	},
      //	Columns = new DbColumns() {
      //    // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
      //		{ _JoinTable_.ColumnDescription }
      //	}
      //};
      //retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Filters

    /// <summary>
    /// Creates and returns the filters object.
    /// </summary>
    /// <returns>The DbFilters object.</returns>
    public DbFilters GetTextFilters()
    {
      DbFilters retValue = null;

      DbFilter dbFilter = new DbFilter();
      DbConditions conditions = dbFilter.ConditionSet.Conditions;
      //conditions.Add(_ClassName_.ColumnDescription, "'Text'");
      //retValue = new DbFilters
      //{
      //	dbFilter
      //};
      return retValue;
    }
    #endregion

    #region OrderBys

    // Sets the current OrderBy names.
    /// <include path='items/SetOrderBy/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
    {
      //_ClassName_.ColumnDescription
    };
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(_ClassName_ lookupRecord, _ClassName_ currentRecord
      , bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (false == isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          if (lookupRecord.ID != currentRecord.ID)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
// #SectionEnd Class
