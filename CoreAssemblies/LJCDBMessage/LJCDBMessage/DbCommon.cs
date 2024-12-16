// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbCommon.cs
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Common data message methods.</summary>
  public partial class DbCommon
  {
    #region Create Request Columns

    // Gets Request columns from the baseDefinition using the propertyNames.
    /// <include path='items/RequestColumns/*' file='Doc/DbCommon.xml'/>
    public static DbColumns RequestColumns(DbColumns baseDefinition
      , List<string> propertyNames = null)
    {
      DbColumns requestColumns;
      if (!NetCommon.HasItems(propertyNames))
      {
        // Default to all Data Definition columns.
        requestColumns = baseDefinition;
      }
      else
      {
        requestColumns = baseDefinition.LJCGetColumns(propertyNames);
      }
      var retValue = new DbColumns(requestColumns);
      return retValue;
    }

    #region Data Value Columns

    // Gets Request Value columns from the baseDefinition using the propertyNames.
    // Similar to RequestDataKeys and RequestLookupKeys()
    /// <include path='items/RequestDataColumns/*' file='Doc/DbCommon.xml'/>
    public static DbColumns RequestDataColumns(object dataObject
      , DbColumns baseDefinition, List<string> propertyNames = null
      , bool includeNull = false)
    {
      DbColumns retValue = null;

      if (dataObject != null)
      {
        DefaultToChangedNames(dataObject, ref propertyNames);
        var requestColumns = RequestColumns(baseDefinition, propertyNames);
        retValue = DataColumns(dataObject, requestColumns, includeNull);
      }
      return retValue;
    }

    // Creates DbColumns values from data properties for supplied column list.
    // Similar to RequestKeys(), DataKeys() and LookupKeys()
    private static DbColumns DataColumns(object dataObject
      , DbColumns requestColumns, bool includeNull = false)
    {
      DbColumns retValue = null;

      if (dataObject != null
        && NetCommon.HasItems(requestColumns))
      {
        retValue = new DbColumns();
        LJCReflect reflect = new LJCReflect(dataObject);
        foreach (DbColumn dbColumn in requestColumns)
        {
          // Add DbColumn from request columns and value from dataObject.
          object value = reflect.GetValue(dbColumn.PropertyName);
          // *** Begin *** Add 12/05/24?
          if (!includeNull
            && null == value)
          {
            continue;
          }
          // *** End   *** Add
          var dbValueColumn = CreateDataColumn(dbColumn, value);
          if (IsDataColumn(dbValueColumn))
          {
            retValue.Add(dbValueColumn);
          }
        }
      }
      return retValue;
    }

    // Creates the value DbColumn object.
    private static DbColumn CreateDataColumn(DbColumn dataColumn, object value)
    {
      DbColumn retValue = null;

      // Set value to null if null specifier is present.
      if (value != null
        && "-null" == value.ToString())
      {
        value = "null";
      }

      if (dataColumn != null
        && value != null)
      {
        retValue = new DbColumn(dataColumn)
        {
          Value = value
        };
      }
      return retValue;
    }

    // Checks if the Data column should be included.
    private static bool IsDataColumn(DbColumn dataColumn)
    {
      bool retValue = false;

      if (dataColumn != null
        && dataColumn.Value != null
        && !dataColumn.AutoIncrement)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Key Columns

    // Gets Request Key columns from baseDefinition using keyColumns and dbJoins.
    /// <include path='items/RequestKeys/*' file='Doc/DbCommon.xml'/>
    public static DbColumns RequestKeys(DbColumns keyColumns
      , DbColumns baseDefinition, DbJoins dbJoins = null)
    {
      DbColumns retValue = null;

      if (NetCommon.HasItems(keyColumns))
      {
        retValue = new DbColumns();
        foreach (DbColumn keyColumn in keyColumns)
        {
          // Fill out the remainder of the column definition.
          var dbColumn = CreateKeyColumn(keyColumn, baseDefinition, dbJoins);
          if (dbColumn != null)
          {
            retValue.Add(dbColumn);
          }
        }
      }
      return retValue;
    }

    // Creates the key DbColumn object.
    private static DbColumn CreateKeyColumn(DbColumn keyColumn
      , DbColumns baseDefinition, DbJoins dbJoins = null)
    {
      DbColumn retValue = null;

      bool process = true;
      if (null == keyColumn.Value)
      {
        process = false;
      }

      // Get Key column from base definition.
      if (process)
      {
        retValue = GetKeyColumn(baseDefinition, keyColumn);
        if (retValue != null)
        {
          // Column is found so do not look in joins.
          process = false;
        }
      }

      // Get Key column from Joins definition.
      if (process && dbJoins != null)
      {
        retValue = QualifiedJoinColumn(keyColumn, dbJoins);
      }
      return retValue;
    }

    // Gets the Key Column using the column collection and Key Column values. 
    private static DbColumn GetKeyColumn(DbColumns dataColumns, DbColumn keyColumn)
    {
      DbColumn retValue;

      // Preserve original and potentially user qualified name.
      var columnName = keyColumn.ColumnName;

      // Get column definition by column name.
      var searchName = NetString.GetSearchName(keyColumn.ColumnName);
      retValue = dataColumns.LJCSearchColumnName(searchName);
      if (retValue != null)
      {
        // Create key column with original name.
        retValue = new DbColumn(retValue)
        {
          ColumnName = columnName,
          Value = keyColumn.Value
        };
      }
      return retValue;
    }

    // Attempt to get the Join column qualified with the Join table name.
    private static DbColumn QualifiedJoinColumn(DbColumn keyColumn
      , DbJoins dbJoins)
    {
      DbColumn retValue = null;

      foreach (DbJoin dbJoin in dbJoins)
      {
        if (NetCommon.HasItems(dbJoin.Columns))
        {
          retValue = GetKeyColumn(dbJoin.Columns, keyColumn);
          if (retValue != null)
          {
            if (-1 == keyColumn.ColumnName.IndexOf("."))
            {
              string qualifier = dbJoin.TableName;
              if (dbJoin.TableAlias != null)
              {
                qualifier = dbJoin.TableAlias;
              }
              retValue.ColumnName = $"{qualifier}.{retValue.ColumnName}";
            }
            break;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Data Keys

    // Gets the Request Key columns from the keyColumns and baseDefinition.
    /// <include path='items/RequestDataKeys/*' file='Doc/DbCommon.xml'/>
    // Similar to RequestDataColumns and RequestLookupKeys()
    public static DbColumns RequestDataKeys(DbColumns keyColumns
      , DbColumns baseDefinition)
    {
      DbColumns retValue = null;

      if (NetCommon.HasItems(keyColumns))
      {
        var requestColumns = RequestKeys(keyColumns, baseDefinition);
        retValue = DataKeys(requestColumns);
      }
      return retValue;
    }

    // Creates DbColumns Key columns from valid key columns.
    private static DbColumns DataKeys(DbColumns keyColumns)
    {
      DbColumns retValue = null;

      if (NetCommon.HasItems(keyColumns))
      {
        retValue = new DbColumns();
        foreach (DbColumn dbColumn in keyColumns)
        {
          if (dbColumn.Value != null)
          {
            // Create the Data Key column.
            var dbValueColumn = dbColumn.Clone();
            if (IsKeyColumn(dbValueColumn, true))
            {
              retValue.Add(dbValueColumn);
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Lookup Keys

    // Get Request Value Key columns from the data object for the property names.
    // Creates Add Lookup Data Key Columns.
    /// <include path='items/RequestLookupKeys/*' file='Doc/DbCommon.xml'/>
    // Similar to RequestDataColumns and RequestDataKeys()
    public static DbColumns RequestLookupKeys(object dataObject
      , DbColumns baseDefinition, List<string> propertyNames = null)
    {
      DbColumns retValue = null;

      if (dataObject != null)
      {
        DefaultToChangedNames(dataObject, ref propertyNames);
        var keyDataColumns = RequestColumns(baseDefinition, propertyNames);
        retValue = LookupKeys(dataObject, keyDataColumns);
      }
      return retValue;
    }

    // Creates DbColumns keys from data properties for supplied column list.
    private static DbColumns LookupKeys(object dataObject
      , DbColumns requestColumns)
    {
      DbColumns retValue = null;

      if (dataObject != null
        && NetCommon.HasItems(requestColumns))
      {
        retValue = new DbColumns();
        LJCReflect reflect = new LJCReflect(dataObject);
        foreach (DbColumn dbColumn in requestColumns)
        {
          // Add DbColumn from dataDefinition and value from dataObject.
          object value = reflect.GetValue(dbColumn.PropertyName);
          if (value != null)
          {
            // Create the Lookup Key column.
            var dbValueColumn = new DbColumn(dbColumn)
            {
              Value = value
            };
            if (IsKeyColumn(dbValueColumn, true))
            {
              retValue.Add(dbValueColumn);
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    // Checks if the column should be included.
    private static bool IsKeyColumn(DbColumn dataColumn
      , bool includeAutoIncrement = false)
    {
      bool retValue = true;

      if (null == dataColumn
        || null == dataColumn.Value)
      {
        retValue = false;
      }

      if (retValue)
      {
        // Exclude AutoIncrement column with value of zero.
        if (dataColumn.AutoIncrement
          && !includeAutoIncrement
          && "0" == dataColumn.Value.ToString())
        {
          retValue = false;
        }
      }

      // Exclude invalid DateTime value.
      if (retValue && "DateTime" == dataColumn.DataTypeName)
      {
        if (!DateTime.TryParse(dataColumn.Value.ToString()
          , out DateTime dateTime))
        {
          retValue = false;
        }

        if (retValue)
        {
          DateTime minValue = DateTime.Parse(SqlDateTime.MinValue.ToString());
          if (dateTime <= minValue)
          {
            retValue = false;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region ChangedNames Methods

    // Adds a changed property name.
    /// <include path='items/AddChangedName/*' file='Doc/DbCommon.xml'/>
    public static void AddChangedName(object dataObject, string propertyName)
    {
      List<string> changedNames = GetChangedNames(dataObject);
      if (changedNames != null)
      {
        var name
          = changedNames.Find(x => 0 == string.Compare(x, propertyName, true));
        if (null == name)
        {
          changedNames.Add(propertyName);
        }
      }
    }

    // Clears the changed names.
    /// <include path='items/ClearChanged/*' file='Doc/DbCommon.xml'/>
    public static void ClearChanged(object dataObject)
    {
      List<string> changedNames = GetChangedNames(dataObject);
      changedNames?.Clear();
    }

    // Gets the names of the changed properties.
    /// <include path='items/GetChangedNames/*' file='Doc/DbCommon.xml'/>
    public static List<string> GetChangedNames(object dataObject)
    {
      List<string> retValue = null;

      if (dataObject != null)
      {
        LJCReflect reflect = new LJCReflect(dataObject);
        try
        {
          retValue = reflect.GetValue("ChangedNames") as List<string>;
        }
        catch (ArgumentException)
        {
        }
      }
      return retValue;
    }

    // Gets the ChangedNames if available and propertyNames is null.
    /// <include path='items/GetDefaultPropertyNames/*' file='Doc/DbCommon.xml'/>
    public static void DefaultToChangedNames(object dataObject
      , ref List<string> propertyNames)
    {
      if (null == propertyNames)
      {
        // Default to ChangedNames if available.
        List<string> changedNames = GetChangedNames(dataObject);
        if (changedNames != null)
        {
          propertyNames = changedNames;
        }
      }
    }

    // Checks if there are changed property names and outputs the names.
    /// <include path='items/IsChanged/*' file='Doc/DbCommon.xml'/>
    public static bool IsChanged(object dataObject, out List<string> propertyNames)
    {
      bool retValue = false;

      propertyNames = GetChangedNames(dataObject);
      if (propertyNames != null)
      {
        if (propertyNames.Count > 0)
        {
          retValue = true;
        }
      }
      else
      {
        retValue = true;
        propertyNames = DbColumns.LJCGetPropertyNames(dataObject);
      }
      return retValue;
    }
    #endregion

    #region Set Object Value Methods

    // Sets the Data Object property values from the data columns object.
    /// <include path='items/SetObjectValues1/*' file='Doc/DbCommon.xml'/>
    public static void SetObjectValues(DbColumns dataColumns, object dataObject)
    {
      LJCReflect reflect;

      reflect = new LJCReflect(dataObject);
      foreach (DbColumn dbColumn in dataColumns)
      {
        // Similar logic in ResultConverter.CreateDataFromTable().
        reflect.SetPropertyValue(dbColumn.PropertyName, dbColumn.Value);
      }
    }

    // Sets the Data Object property values from the DbValues object.
    /// <include path='items/SetObjectValues2/*' file='Doc/DbCommon.xml'/>
    public static void SetObjectValues(DbValues dataValues, object dataObject)
    {
      LJCReflect reflect;

      reflect = new LJCReflect(dataObject);
      foreach (DbValue dbValue in dataValues)
      {
        // Similar logic in ResultConverter.CreateDataFromTable().
        reflect.SetPropertyValue(dbValue.PropertyName, dbValue.Value);
      }
    }
    #endregion
  }
}
