// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Testing.cs
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using LJCNetCommon;

namespace LJCDBMessage
{
  class DbCommonSBS
  {
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
      , DbColumns baseDefinition, List<string> propertyNames = null)
    {
      DbColumns retValue = null;

      if (dataObject != null)
      {
        GetDefaultPropertyNames(dataObject, ref propertyNames);

        var requestColumns = RequestColumns(baseDefinition, propertyNames);
        retValue = DataColumns(dataObject, requestColumns);
      }
      return retValue;
    }

    // Creates DbColumns values from data properties for supplied column list.
    // Similar to RequestKeys() and LookupKeys()
    private static DbColumns DataColumns(object dataObject
      , DbColumns requestColumns)
    {
      DbColumns retValue = null;

      if (dataObject != null
        && requestColumns != null)
      {
        retValue = new DbColumns();
        LJCReflect reflect = new LJCReflect(dataObject);
        foreach (DbColumn dbColumn in requestColumns)
        {
          // Add DbColumn from request columns and value from dataObject.
          object value = reflect.GetValue(dbColumn.PropertyName);
          if (value != null)
          {
            var dbValueColumn = CreateDataColumn(dbColumn, value);
            if (IsDataColumn(dbValueColumn))
            {
              retValue.Add(dbValueColumn);
            }
          }
        }
      }
      return retValue;
    }

    // Creates the value DbColumn object.
    private static DbColumn CreateDataColumn(DbColumn dataColumn, object value)
    {
      DbColumn retValue = null;

      if (dataColumn != null
        || value != null)
      {
        // Set value to null if null specifier is present.
        if ("-null" == value.ToString())
        {
          value = "null";
        }

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
    // Similar to DataColumns() and LookupKeys()
    public static DbColumns RequestKeys(DbColumns keyColumns
      , DbColumns baseDefinition, DbJoins dbJoins = null)
    {
      DbColumns retValue = null;

      if (keyColumns != null)
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
        if (dbJoin.Columns != null && dbJoin.Columns.Count > 0)
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

      if (keyColumns != null)
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

      if (keyColumns != null)
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
        GetDefaultPropertyNames(dataObject, ref propertyNames);

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
        && requestColumns != null)
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
        else
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

    #region Other Public Methods

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
    public static void GetDefaultPropertyNames(object dataObject
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
    #endregion
  }
}
