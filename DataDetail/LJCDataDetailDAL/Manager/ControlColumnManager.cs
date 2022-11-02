// ControlColumnManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class ControlColumnManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlColumnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ControlColumn", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<ControlColumn, ControlColumns>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(ControlColumn.ColumnID, caption: "ControlColumn ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        ControlColumn.ColumnID
      });

      // Create the list of unique key lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        ControlColumn.ColumnControlTabID,
        ControlColumn.ColumnColumnIndex
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlColumn Add(ControlColumn dataObject, List<string> propertyNames = null)
    {
      ControlColumn retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
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
    public ControlColumns Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      ControlColumns retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlColumn Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      ControlColumn retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(ControlColumn dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Custom Load/Retrieve Methods

    // Loads the parent records.
    /// <include path='items/LoadWithParentID/*' file='Doc/ControlColumnManager.xml'/>
    public ControlColumns LoadWithParentID(long controlTabID)
    {
      var keyColumns = GetParentKey(controlTabID);
      Manager.OrderByNames = new List<string>()
      {
        ControlColumn.ColumnControlTabID,
        ControlColumn.ColumnColumnIndex
      };
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlColumn RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied values.
    /// <include path='items/RetrieveWithUnique/*' file='Doc/ControlColumnManager.xml'/>
    public ControlColumn RetrieveWithUnique(int controlTabID, int columnIndex
      , List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueKey(controlTabID, columnIndex);
      var retValue = Retrieve(keyColumns, propertyNames);
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
      // Needs (object) cast for string value to select correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlColumn.ColumnID, id }
      };
      return retValue;
    }

    // Gets the Parent ID key columns.
    /// <include path='items/GetParentKey/*' file='Doc/ControlColumnManager.xml'/>
    public DbColumns GetParentKey(long controlTabID)
    {
      var retValue = new DbColumns()
      {
        { ControlColumn.ColumnControlTabID, controlTabID }
      };
      return retValue;
    }

    // Gets the unique key columns.
    /// <include path='items/GetUniqueKey/*' file='Doc/ControlColumnManager.xml'/>
    public DbColumns GetUniqueKey(int controlTabID, int controlColumnIndex)
    {
      var retValue = new DbColumns()
      {
        { ControlColumn.ColumnControlTabID, controlTabID },
        { ControlColumn.ColumnColumnIndex, controlColumnIndex }
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
      // to receive the join values.
      // The RenameAs property is required if there is another table column
      // with the same name.
      // Note: dbColumns.Add(string columnName, string propertyName = null
      // , string renameAs = null, string dataTypeName = "String"
      // , string caption = null)

      // ControlTab.TabIndex
      //left join ControlTab
      // on ((ControlColumn.ControlTabID = ControlTab.ID))
      dbJoin = new DbJoin
      {
        TableName = "ControlTab",
        JoinType = "left",
        JoinOns = new DbJoinOns()
        {
          { ControlColumn.ColumnControlTabID, ControlTab.ColumnID }
        },
        Columns = new DbColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //  , dataTypeName = "String", caption = null
      		{ ControlTab.ColumnTabIndex }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlColumn, ControlColumns> ResultConverter
    { get; set; }
    #endregion
  }
}
