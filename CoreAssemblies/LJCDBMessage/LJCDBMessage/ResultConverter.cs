// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ResultConverter.cs
using System;
using System.Collections.Generic;
using System.Data;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Converts DbColumns and DbResult objects to data objects.
  /// <include path='items/ResultConverter/*' file='Doc/ResultConverter.xml'/>
  public class ResultConverter<TData, TList>
    where TData : class, new()
    where TList : List<TData>, new()
  {
    #region public Methods

    // Creates a collection from the result records.
    /// <include path='items/CreateCollection/*' file='Doc/ResultConverter.xml'/>
    public TList CreateCollection(DbResult dbResult)
    {
      // Also in LJCDBClientLib.ObjectManager.
      // Used here to allow for different TList and TData.
      // Testing in LJCDBServiceLib.TestDbDataAccess.
      TList retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        retValue = new TList();
        //foreach (DbValues dbValues in dbResult.Rows)
        foreach (DbRow dbRow in dbResult.Rows)
        {
          //TData dataRecord = CreateData(dbRow);
          TData dataRecord = CreateData(dbRow.Values);
          retValue.Add(dataRecord);
        }
      }
      return retValue;
    }

    // Creates a Data Object collection from the Table rows.
    /// <include path='items/CreateCollectionFromTable/*' file='Doc/ResultConverter.xml'/>
    public TList CreateCollectionFromTable(DataTable dataTable
      , DbColumns dataDefinition = null)
    {
      // Testing in LJCDBServiceLib.TestDbDataAccess.
      TList retValue = null;

      if (dataTable.Columns != null && dataTable.Columns.Count > 0
        && NetCommon.HasData(dataTable))
      {
        retValue = new TList();
        foreach (DataRow dataRow in dataTable.Rows)
        {
          TData dataObject = CreateDataFromTable(dataTable, dataRow
            , dataDefinition);
          if (dataObject != null)
          {
            retValue.Add(dataObject);
          }
        }
      }
      return retValue;
    }

    // Creates a Data Object from the result DbColumns object.
    /// <include path='items/CreateData1/*' file='Doc/ResultConverter.xml'/>
    public TData CreateData(DbColumns dataColumns)
    {
      TData retValue;

      // Populate a data object with the result values.
      // Uses retValue as an object and processes with reflection.
      retValue = new TData();
      DbCommon.SetObjectValues(dataColumns, retValue);
      DbCommon.ClearChanged(retValue);
      return retValue;
    }

    // Creates a Data Object from the result values.
    /// <include path='items/CreateData2/*' file='Doc/ResultConverter.xml'/>
    public TData CreateData(DbResult dbResult)
    {
      TData retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        //retValue = CreateData(dbResult.Rows[0]);
        retValue = CreateData(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Creates a Data Object from the data values.
    /// <include path='items/CreateData3/*' file='Doc/ResultConverter.xml'/>
    public TData CreateData(DbValues dataValues)
    {
      // Also in LJCDBClientLib.ObjectManager.
      // Used here to allow for different TData.
      TData retValue = null;

      if (DbValues.HasItems(dataValues))
      {
        // Populate a data object with the result values.
        // Uses retValue as an object and processes with reflection.
        retValue = new TData();
        DbCommon.SetObjectValues(dataValues, retValue);
        DbCommon.ClearChanged(retValue);
      }
      return retValue;
    }

    // Creates a Data Object from the row values.
    /// <include path='items/CreateDataFromTable/*' file='Doc/ResultConverter.xml'/>
    public TData CreateDataFromTable(DataTable dataTable, DataRow dataRow = null
      , DbColumns dataDefinition = null)
    {
      LJCReflect reflect;
      string columnName;
      string propertyName;
      TData retValue = null;

      if (dataTable.Columns != null && dataTable.Columns.Count > 0
        && NetCommon.HasData(dataTable))
      {
        retValue = new TData();

        if (null == dataRow)
        {
          dataRow = dataTable.Rows[0];
        }
        reflect = new LJCReflect(retValue);
        for (int index = 0; index < dataTable.Columns.Count; index++)
        {
          columnName = dataTable.Columns[index].ColumnName;
          propertyName = GetPropertyName(dataDefinition, columnName);
          object value = dataRow[index];

          // Similar logic in LJCDBMessage.DbCommon.SetObjectValues().
          reflect.SetPropertyValue(propertyName, value);
        }
        DbCommon.ClearChanged(retValue);
      }
      return retValue;
    }
    #endregion

    // Gets the property name.
    private string GetPropertyName(DbColumns propertyMapping, string columnName)
    {
      // Similar logic in LJCDBMessage.DbResult.GetRowValues().
      DbColumn dbColumn;
      string retValue = columnName;

      if (propertyMapping != null)
      {
        dbColumn = propertyMapping.LJCSearchRenameAs(columnName);
        if (dbColumn != null)
        {
          retValue = dbColumn.PropertyName;
        }
        else
        {
          dbColumn = propertyMapping.LJCSearchColumnName(columnName);
          if (dbColumn != null)
          {
            retValue = dbColumn.PropertyName;
          }
        }
      }
      return retValue;
    }
  }
}
