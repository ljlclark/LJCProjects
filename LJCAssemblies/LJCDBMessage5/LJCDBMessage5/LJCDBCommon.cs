// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBCommon.cs
using LJCNetCommon5;
using System.Data.SqlTypes;

namespace LJCDBMessage5
{
  /// <summary>Common data message methods.</summary>
  public partial class LJCDBCommon
  {
    #region Create Request Columns

    // Gets Request columns from the baseDefinition using the propertyNames.
    /// <include path='items/RequestColumns/*' file='Doc/DbCommon.xml'/>
    public static LJCDataColumns? RequestColumns(LJCDataColumns dataColumns
      , List<string>? propertyNames = null)
    {
      LJCDataColumns? retColumns = null;

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
      if (requestColumns != null)
      {
        //retColumns = new LJCDataColumns(requestColumns);
        retColumns = [.. requestColumns];
      }
      return retColumns;
    }

    #region Data Value Columns

    // Gets Request Value columns from the baseDefinition using the propertyNames.
    /// <include path='items/RequestDataColumns/*' file='Doc/DbCommon.xml'/>
    // Similar to RequestDataKeys and RequestLookupKeys()
    public static LJCDataColumns? RequestDataColumns(object dataObject
      , LJCDataColumns baseDefinition, List<string>? propertyNames = null
      , bool? includeNull = false)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null)
      {
        if (!LJC.HasElements(propertyNames))
        {
          //propertyNames = new List<string>();
          propertyNames = [];
          DefaultToChangedNames(dataObject, ref propertyNames);
        }
        var requestColumns = RequestColumns(baseDefinition, propertyNames);
        if (LJC.HasItems(requestColumns))
        {
          retValue = DataColumns(dataObject, requestColumns, includeNull);
        }
      }
      return retValue;
    }

    // Creates LJCDataColumns values from data properties for supplied column list.
    // Similar to RequestKeys(), DataKeys() and LookupKeys()
    private static LJCDataColumns? DataColumns(object dataObject
      , LJCDataColumns requestColumns, bool? includeNull = false)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null
        && LJC.HasItems(requestColumns))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        var reflect = new LJCReflect(dataObject);
        foreach (LJCDataColumn dataColumn in requestColumns)
        {
          // Add LJCDataColumn from request columns and value from dataObject.
          object? value = reflect.GetValue(dataColumn.PropertyName);
          if (null == includeNull)
          {
            includeNull = false;
          }
          if (!(bool)includeNull
            && null == value)
          {
            continue;
          }
          if (value != null)
          {
            var dbValueColumn = CreateDataColumn(dataColumn, value);
            if (dbValueColumn != null)
            {
              if (IsDataColumn(dbValueColumn))
              {
                retValue.Add(dbValueColumn);
              }
            }
          }
        }
      }
      return retValue;
    }

    // Creates the value LJCDataColumn object.
    private static LJCDataColumn? CreateDataColumn(LJCDataColumn dataColumn
      , object value)
    {
      LJCDataColumn? retValue = null;

      // Set value to null if null specifier is present.
      if (value != null
        && "-null" == value.ToString())
      {
        value = "null";
      }

      if (dataColumn != null
        && value != null)
      {
        retValue = new LJCDataColumn(dataColumn)
        {
          Value = value
        };
      }
      return retValue;
    }

    // Checks if the Data column should be included.
    private static bool IsDataColumn(LJCDataColumn dataColumn)
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
    // Similar to DataColumns() and LookupKeys()
    public static LJCDataColumns? RequestKeys(LJCDataColumns? keyColumns
      , LJCDataColumns baseDefinition, LJCDBJoins? dbJoins = null)
    {
      LJCDataColumns? retValue = null;

      if (LJC.HasItems(keyColumns))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (LJCDataColumn keyColumn in keyColumns)
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
    private static LJCDataColumn? GetKeyColumn(LJCDataColumns dataColumns, LJCDataColumn keyColumn)
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
            //if (-1 == keyColumn.ColumnName.IndexOf("."))
            if (!keyColumn.ColumnName.Contains('.'))
            {
              string? qualifier = dbJoin.TableName;
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

      if (LJC.HasItems(keyColumns))
      {
        var requestColumns = RequestKeys(keyColumns, baseDefinition);
        if (LJC.HasItems(requestColumns))
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

      if (LJC.HasItems(keyColumns))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (LJCDataColumn dbColumn in keyColumns)
        {
          if (dbColumn.Value != null)
          {
            // Create the Data Key column.
            var dbValueColumn = dbColumn.Clone();
            if (dbValueColumn != null)
            {
              if (IsKeyColumn(dbValueColumn, true))
              {
                retValue.Add(dbValueColumn);
              }
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
        if (!LJC.HasElements(propertyNames))
        {
          propertyNames = [];
          DefaultToChangedNames(dataObject, ref propertyNames);
        }
        var keyDataColumns = RequestColumns(baseDefinition, propertyNames);
        if (keyDataColumns != null)
        {
          retValue = LookupKeys(dataObject, keyDataColumns);
        }
      }
      return retValue;
    }

    // Creates LJCDataColumns keys from data properties for supplied column list.
    private static LJCDataColumns? LookupKeys(object dataObject
      , LJCDataColumns requestColumns)
    {
      LJCDataColumns? retValue = null;

      if (dataObject != null
        && LJC.HasItems(requestColumns))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        var reflect = new LJCReflect(dataObject);
        foreach (LJCDataColumn dbColumn in requestColumns)
        {
          // Add LJCDataColumn from dataDefinition and value from dataObject.
          object? value = reflect.GetValue(dbColumn.PropertyName);
          if (value != null)
          {
            // Create the Lookup Key column.
            var dbValueColumn = new LJCDataColumn(dbColumn)
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
    private static bool IsKeyColumn(LJCDataColumn? dataColumn
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
      List<string>? changedNames = GetChangedNames(dataObject);
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
      List<string>? changedNames = GetChangedNames(dataObject);
      changedNames?.Clear();
    }

    // Gets the names of the changed properties.
    /// <include path='items/GetChangedNames/*' file='Doc/DbCommon.xml'/>
    public static List<string>? GetChangedNames(object dataObject)
    {
      List<string>? retValue = null;

      if (dataObject != null)
      {
        var reflect = new LJCReflect(dataObject);
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
        List<string>? changedNames = GetChangedNames(dataObject);
        if (changedNames != null)
        {
          propertyNames = changedNames;
        }
      }
    }

    // Checks if there are changed property names and outputs the names.
    /// <include path='items/IsChanged/*' file='Doc/DbCommon.xml'/>
    public static bool IsChanged(object dataObject, out List<string>? propertyNames)
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
        propertyNames = LJCDataColumns.LJCObjectPropertyNames(dataObject);
      }
      return retValue;
    }
    #endregion

    #region Set Object Value Methods

    // Sets the Data Object property values from the data columns object.
    /// <include path='items/SetObjectValues1/*' file='Doc/DbCommon.xml'/>
    public static void SetObjectValues(LJCDataColumns dataColumns, object dataObject)
    {
      LJCReflect reflect;

      reflect = new LJCReflect(dataObject);
      foreach (LJCDataColumn dataColumn in dataColumns)
      {
        // Similar logic in ResultConverter.CreateDataFromTable().
        reflect.SetPropertyValue(dataColumn.PropertyName, dataColumn.Value);
      }
    }

    // Sets the Data Object property values from the DbValues object.
    /// <include path='items/SetObjectValues2/*' file='Doc/DbCommon.xml'/>
    public static void SetObjectValues(LJCDataValues dataValues, object dataObject)
    {
      LJCReflect reflect;

      reflect = new LJCReflect(dataObject);
      foreach (LJCDataValue dbValue in dataValues)
      {
        // Similar logic in ResultConverter.CreateDataFromTable().
        reflect.SetPropertyValue(dbValue.PropertyName!, dbValue.Value);
      }
    }
    #endregion
  }
}
