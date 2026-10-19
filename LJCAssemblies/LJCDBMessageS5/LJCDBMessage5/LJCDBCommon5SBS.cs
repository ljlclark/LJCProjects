// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBCommonSBS.cs
using LJCNetCommon5;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace LJCDBMessage5
{
  class LJCDBCommonSBS
  {
    // Gets Request columns from the baseDefinition using the propertyNames.
    /// <include path='items/RequestColumns/*' file='Doc/DbCommon.xml'/>
    public static LJCDataColumns RequestColumns(LJCDataColumns dataColumns
      , List<string>? propertyNames = null)
    {
      LJCDataColumns? requestColumns;
      if (!LJC.HasItems(propertyNames))
      {
        // Default to all Data Definition columns.
        requestColumns = dataColumns;
      }
      else
      {
        requestColumns = dataColumns.LJCGetColumns(propertyNames);
      }

      var retValue = new LJCDataColumns(requestColumns);
      return retValue;
    }

    #region Data Value Columns

    // Gets Request Value columns from the baseDefinition using the propertyNames.
    // Similar to RequestDataKeys and RequestLookupKeys()
    /// <include path='items/RequestDataColumns/*' file='Doc/DbCommon.xml'/>
    public static LJCDataColumns? RequestDataColumns(object dataObject
      , LJCDataColumns baseDefinition, List<string>? propertyNames = null
      , bool includeNull = false)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null)
      {
        GetDefaultPropertyNames(dataObject, ref propertyNames);
        var requestColumns = RequestColumns(baseDefinition, propertyNames);
        retValue = DataColumns(dataObject, requestColumns, includeNull);
      }
      return retValue;
    }

    // Creates LJCDataColumns values from data properties for supplied column list.
    // Similar to RequestKeys() and LookupKeys()
    private static LJCDataColumns? DataColumns(object dataObject
      , LJCDataColumns requestColumns, bool includeNull)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null
        && requestColumns != null)
      {
        retValue = new LJCDataColumns();
        var reflect = new LJCReflect(dataObject);
        foreach (LJCDataColumn dataColumn in requestColumns)
        {
          // Add LJCDataColumn from request columns and value from dataObject.
          object? value = reflect.GetValue(dataColumn.PropertyName);
          if (!includeNull
            && null == value)
          {
            continue;
          }
          var valueDataColumn = CreateDataColumn(dataColumn, value!);
          if (IsDataColumn(valueDataColumn))
          {
            retValue.Add(valueDataColumn);
          }
        }
      }
      return retValue;
    }

    // Creates the value LJCDataColumn object.
    private static LJCDataColumn? CreateDataColumn(LJCDataColumn? dataColumn
      , object value)
    {
      LJCDataColumn? retValue = null;

      if (dataColumn != null
        || value != null)
      {
        // Set value to null if null specifier is present.
        if ("-null" == value.ToString())
        {
          value = "null";
        }

        retValue = new LJCDataColumn(dataColumn!)
        {
          Value = value
        };
      }
      return retValue;
    }

    // Checks if the Data column should be included.
    private static bool IsDataColumn([NotNullWhen(true)] LJCDataColumn? dataColumn)
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
    public static LJCDataColumns? RequestKeys(LJCDataColumns keyColumns
      , LJCDataColumns baseDefinition, LJCDBJoins? dbJoins = null)
    {
      LJCDataColumns? retValue = null;

      if (keyColumns != null)
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (LJCDataColumn keyColumn in keyColumns)
        {
          // Fill out the remainder of the column definition.
          var dataColumn = CreateKeyColumn(keyColumn, baseDefinition, dbJoins);
          if (dataColumn != null)
          {
            retValue.Add(dataColumn);
          }
        }
      }
      return retValue;
    }

    // Creates the key LJCDataColumn object.
    private static LJCDataColumn? CreateKeyColumn(LJCDataColumn keyColumn
      , LJCDataColumns baseDefinition, LJCDBJoins? dbJoins = null)
    {
      LJCDataColumn? retValue = null;

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
    private static LJCDataColumn? GetKeyColumn(LJCDataColumns dataColumns
      , LJCDataColumn keyColumn)
    {
      LJCDataColumn? retValue;

      // Preserve original and potentially user qualified name.
      var columnName = keyColumn.ColumnName;

      // Get column definition by column name.
      var searchName = LJCNetString.GetSearchName(keyColumn.ColumnName);
      retValue = dataColumns.LJCSearchColumnName(searchName);
      if (retValue != null)
      {
        // Create key column with original name.
        retValue = new LJCDataColumn(retValue)
        {
          ColumnName = columnName,
          Value = keyColumn.Value
        };
      }
      return retValue;
    }

    // Attempt to get the Join column qualified with the Join table name.
    private static LJCDataColumn? QualifiedJoinColumn(LJCDataColumn keyColumn
      , LJCDBJoins dbJoins)
    {
      LJCDataColumn? retValue = null;

      foreach (LJCDBJoin dbJoin in dbJoins)
      {
        if (LJC.HasItems(dbJoin.Columns))
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
    public static LJCDataColumns? RequestDataKeys(LJCDataColumns keyColumns
      , LJCDataColumns baseDefinition)
    {
      LJCDataColumns? retValue = null;

      if (keyColumns != null)
      {
        var requestColumns = RequestKeys(keyColumns, baseDefinition);
        if (requestColumns != null)
        {
          retValue = DataKeys(requestColumns);
        }
      }
      return retValue;
    }

    // Creates LJCDataColumns Key columns from valid key columns.
    private static LJCDataColumns? DataKeys(LJCDataColumns keyColumns)
    {
      LJCDataColumns? retValue = null;

      if (keyColumns != null)
      {
        retValue = new LJCDataColumns();
        foreach (LJCDataColumn dataColumn in keyColumns)
        {
          if (dataColumn.Value != null)
          {
            // Create the Data Key column.
            var dbValueColumn = dataColumn.Clone();
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
    public static LJCDataColumns? RequestLookupKeys(object dataObject
      , LJCDataColumns baseDefinition, List<string>? propertyNames = null)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null)
      {
        GetDefaultPropertyNames(dataObject, ref propertyNames);

        var keyDataColumns = RequestColumns(baseDefinition, propertyNames);
        retValue = LookupKeys(dataObject, keyDataColumns);
      }
      return retValue;
    }

    // Creates LJCDataColumns keys from data properties for supplied column list.
    private static LJCDataColumns? LookupKeys(object dataObject
      , LJCDataColumns requestColumns)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null
        && requestColumns != null)
      {
        retValue = new LJCDataColumns();
        LJCReflect reflect = new LJCReflect(dataObject);
        foreach (LJCDataColumn dataColumn in requestColumns)
        {
          // Add LJCDataColumn from dataDefinition and value from dataObject.
          object? value = reflect.GetValue(dataColumn.PropertyName);
          if (value != null)
          {
            // Create the Lookup Key column.
            var dbValueColumn = new LJCDataColumn(dataColumn)
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
    private static bool IsKeyColumn([NotNullWhen(true)] LJCDataColumn? dataColumn
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
        if (dataColumn!.AutoIncrement
          && !includeAutoIncrement
          && "0" == dataColumn.Value!.ToString())
        {
          retValue = false;
        }
      }

      // Exclude invalid DateTime value.
      if (retValue
        && "DateTime" == dataColumn!.DataTypeName)
      {
        if (!DateTime.TryParse(dataColumn.Value!.ToString()
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
    public static List<string>? GetChangedNames(object dataObject)
    {
      List<string>? retValue = null;

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
      , ref List<string>? propertyNames)
    {
      if (null == propertyNames)
      {
        // Default to ChangedNames if available.
        List<string>? changedNames = GetChangedNames(dataObject);
        if (changedNames != null)
        {
          propertyNames = changedNames;
        }
      }
    }
    #endregion
  }
}
