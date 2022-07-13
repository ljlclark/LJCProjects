// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Common data message methods.</summary>
  public partial class DbCommon
  {
    #region Public Create Column Functions

    // Gets Request columns from the baseDefinition using the propertyNames.
    /// <include path='items/RequestColumns/*' file='Doc/DbCommon.xml'/>
    public static DbColumns RequestColumns(DbColumns baseDefinition
      , List<string> propertyNames = null)
    {
      DbColumns requestColumns;
      DbColumns retValue;

      if (null == propertyNames || 0 == propertyNames.Count)
      {
        // Default to all Data Definition columns.
        requestColumns = baseDefinition;
      }
      else
      {
        requestColumns = baseDefinition.LJCGetColumns(propertyNames);
      }

      //retValue = requestColumns.Clone();
      retValue = new DbColumns(requestColumns);
      return retValue;
    }

    // Gets Request Value columns from data properties with the property names.
    /// <include path='items/RequestDataColumns/*' file='Doc/DbCommon.xml'/>
    public static DbColumns RequestDataColumns(object dataObject
      , DbColumns baseDefinition, List<string> propertyNames = null)
    {
      DbColumns requestColumns;
      DbColumns retValue = null;

      if (dataObject != null)
      {
        GetDefaultPropertyNames(dataObject, ref propertyNames);

        requestColumns = RequestColumns(baseDefinition, propertyNames);
        retValue = DataColumns(dataObject, requestColumns);
      }
      return retValue;
    }

    // Gets the Request Key columns from the keyColumns and baseDefinition.
    /// <include path='items/RequestDataKeys/*' file='Doc/DbCommon.xml'/>
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

    // Gets Request Key columns from baseDefinition using keyColumns and dbJoins.
    /// <include path='items/RequestKeys/*' file='Doc/DbCommon.xml'/>
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

    // Get Request Value Key columns from the data object for the property names.
    // Creates Add Lookup Data Key Columns.
    /// <include path='items/RequestLookupKeys/*' file='Doc/DbCommon.xml'/>
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
    #endregion

    #region Private Create Column Functions

    // Creates DbColumns values from data properties with the property names.
    private static DbColumns DataColumns(object dataObject
      , DbColumns requestColumns)
    {
      DbColumn dbValueColumn;
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
            dbValueColumn = CreateValueColumn(dbColumn, value);
            if (dbValueColumn != null)
            {
              retValue.Add(dbValueColumn);
            }
          }
        }
      }
      return retValue;
    }

    // Creates DbColumns object from key DbColumns.
    private static DbColumns DataKeys(DbColumns keyColumns)
    {
      DbColumn dbValueColumn;
      DbColumns retValue = null;

      if (keyColumns != null)
      {
        retValue = new DbColumns();
        foreach (DbColumn dbColumn in keyColumns)
        {
          if (dbColumn.Value != null)
          {
            // Add DbColumn and value from keyColumns.
            dbValueColumn = dbColumn.Clone();

            // ToDo: Revisit
            // Add this because the keyColumns comes from the data which
            // has a value for the AutoIncrement column and it must be
            // zero to be excluded.
            //if (dbValueColumn.AutoIncrement)
            //{
            //	dbValueColumn.Value = "0";
            //}

            if (IsKeyColumn(dbValueColumn, true))
            {
              retValue.Add(dbValueColumn);
            }
          }
        }
      }
      return retValue;
    }

    // Creates DbColumns object from data properties for specified column list.
    private static DbColumns LookupKeys(object dataObject
      , DbColumns requestColumns)
    {
      DbColumn dbValueColumn;
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
            dbValueColumn = new DbColumn(dbColumn)
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

    #region Other Public Methods

    // Clears the changed names.
    /// <include path='items/ClearChanged/*' file='Doc/DbCommon.xml'/>
    public static void ClearChanged(object dataObject)
    {
      List<string> changedNames = GetChangedNames(dataObject);
      if (changedNames != null)
      {
        changedNames.Clear();
      }
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
    public static void SetObjectValues(DbValues dbValues, object dataObject)
    {
      LJCReflect reflect;

      reflect = new LJCReflect(dataObject);
      foreach (DbValue dbValue in dbValues)
      {
        // Similar logic in ResultConverter.CreateDataFromTable().
        reflect.SetPropertyValue(dbValue.PropertyName, dbValue.Value);
      }
    }
    #endregion

    #region Private Create DbRequest Column Methods

    // Creates the key DbColumn object.
    private static DbColumn CreateKeyColumn(DbColumn keyColumn
      , DbColumns baseDefinition, DbJoins dbJoins = null)
    {
      bool process = true;
      DbColumn retValue = null;

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

    // Creates the value DbColumn object.
    private static DbColumn CreateValueColumn(DbColumn dbColumn, object value)
    {
      DbColumn retValue = null;
      bool include = true;

      if (null == dbColumn
        || null == value)
      {
        include = false;
      }

      if (include)
      {
        // Do not include AutoIncrement columns for "Insert".
        if (true == dbColumn.AutoIncrement)
        {
          include = false;
        }
      }

      if (include)
      {
        // Set value to null if null specifier is present.
        if ("-null" == value.ToString()
          || "-" == value.ToString())
        {
          value = "null";
        }

        retValue = new DbColumn(dbColumn)
        {
          Value = value
        };
      }
      return retValue;
    }

    // Gets the Key Column using the column collection and Key Column values. 
    private static DbColumn GetKeyColumn(DbColumns dbColumns, DbColumn keyColumn)
    {
      DbColumn retValue;

      // Preserve original and potentially user qualified name.
      var columnName = keyColumn.ColumnName;

      // Get column definition by property name.
      var searchPropertyName = GetSearchPropertyName(keyColumn.ColumnName);
      retValue = dbColumns.LJCSearchPropertyName(searchPropertyName);
      if (retValue != null)
      {
        // Create key column with original name.
        //retValue = retValue.Clone();
        retValue = new DbColumn(retValue)
        {
          ColumnName = columnName,
          Value = keyColumn.Value
        };
      }
      return retValue;
    }

    // Gets the Search Property name.
    private static string GetSearchPropertyName(string propertyName)
    {
      var retValue = propertyName;

      var index = propertyName.IndexOf(".");
      if (index > -1)
      {
        // Get property name from qualified name.
        retValue = propertyName.Substring(index + 1);
      }
      return retValue;
    }

    // Checks if the column should be included.
    private static bool IsKeyColumn(DbColumn dbColumn
      , bool includeAutoIncrement = false)
    {
      bool retValue = true;

      if (null == dbColumn
        || null == dbColumn.Value)
      {
        retValue = false;
      }

      if (retValue)
      {
        // Exclude AutoIncrement column with value of zero.
        if (true == dbColumn.AutoIncrement
          && false == includeAutoIncrement
          && "0" == dbColumn.Value.ToString())
        {
          retValue = false;
        }
      }

      // Exclude invalid DateTime value.
      if (retValue && "DateTime" == dbColumn.DataTypeName)
      {
        if (false == DateTime.TryParse(dbColumn.Value.ToString()
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
  }
}
