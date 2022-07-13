// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJCNetCommon;

namespace LJCDBMessage
{
  class Testing
  {
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

      retValue = requestColumns.Clone();
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
          // Add DbColumn from requestColumns and value from dataObject.
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
  }
}
