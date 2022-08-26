// ControlColumnManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace DataDetailDAL
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

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        ControlColumn.ColumnDetailConfigID,
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

    #region Retrieve/Load Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlColumn RetrieveWithID(int id, List<string> propertyNames = null)
    {
      ControlColumn retValue;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlColumn RetrieveWithUnique(int detailConfigID, int columnIndex
      , List<string> propertyNames = null)
    {
      ControlColumn retValue;

      var keyColumns = GetUniqueKey(detailConfigID, columnIndex);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Loads the parent records.
    /// <summary>
    /// Loads the parent records.
    /// </summary>
    /// <param name="detailConfigID">The DetailConfig parent ID.</param>
    /// <returns>The ControlColumns related to the parent.</returns>
    public ControlColumns LoadWithParentID(long detailConfigID)
    {
      ControlColumns retValue;

      var keyColumns = GetParentKey(detailConfigID);
      var dbResult = Manager.Load(keyColumns);
      retValue = ResultConverter.CreateCollection(dbResult);
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
        { ControlColumn.ColumnID, id }
      };
      return retValue;
    }

    /// <summary>
    /// Gets the Parent ID key columns.
    /// </summary>
    /// <param name="detailConfigID">The parent ID value.</param>
    /// <returns>The Parent KeyColumns object.</returns>
    public DbColumns GetParentKey(long detailConfigID)
    {
      var retValue = new DbColumns()
      {
        { ControlColumn.ColumnDetailConfigID, detailConfigID }
      };
      return retValue;
    }

    // Gets the unique key columns.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetUniqueKey(int detailConfigID, int columnIndex)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlColumn.ColumnDetailConfigID, detailConfigID },
        { ControlColumn.ColumnColumnIndex, columnIndex }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlColumn, ControlColumns> ResultConverter { get; set; }
    #endregion
  }
}
