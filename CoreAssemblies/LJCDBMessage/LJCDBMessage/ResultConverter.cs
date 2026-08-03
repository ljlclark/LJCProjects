// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ResultConverter.cs
using System.Collections.Generic;
using System.Data;
using LJCNetCommon;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBMessage
{
  // Converts LJCDataColumns and DbResult objects to data objects.
  /// <include file='Doc/ResultConverter.xml'
  ///  path='items/ResultConverter/*'/>
  public class ResultConverter<TData, TList>
    where TData : class, new()
    where TList : List<TData>, new()
  {
    #region public Methods

    // Creates a collection from the result records.
    /// <include file='Doc/ResultConverter.xml'
    ///  path='items/CreateCollection/*'/>
    public TList CreateCollection(DbResult dbResult)
    {
      // Also in LJCDBClientLib.ObjectManager.
      // Used here to allow for different TList and TData.
      // Testing in LJCDBServiceLib.TestDbDataAccess.
      TList retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        retValue = new TList();
        foreach (DbRow dbRow in dbResult.Rows)
        {
          TData dataRecord = CreateData(dbRow.Values);
          retValue.Add(dataRecord);
        }
      }
      return retValue;
    }

    // Creates a Data Object collection from the Table rows.
    /// <include file='Doc/ResultConverter.xml'
    ///  path='items/CreateCollectionFromTable/*'/>
    public TList CreateCollectionFromTable(DataTable dataTable
      , LJCDataColumns dataDefinition = null)
    {
      // Testing in LJCDBServiceLib.TestDbDataAccess.
      TList retValue = null;

      if (NetCommon.HasColumns(dataTable)
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

    // Creates a Data Object from the result LJCDataColumns object.
    /// <include file='Doc/ResultConverter.xml'
    ///  path='items/CreateData1/*'/>
    public TData CreateData(LJCDataColumns dataColumns)
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
    /// <include file='Doc/ResultConverter.xml'
    ///  path='items/CreateData2/*'/>
    public TData CreateData(DbResult dbResult)
    {
      TData retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateData(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Creates a Data Object from the data values.
    /// <include file='Doc/ResultConverter.xml'
    ///  path='items/CreateData3/*'/>
    public TData CreateData(LJCDataValues dataValues)
    {
      // Also in LJCDBClientLib.ObjectManager.
      // Used here to allow for different TData.
      TData retValue = null;

      if (LJC.HasListItems(dataValues))
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
    /// <include file='Doc/ResultConverter.xml'
    ///  path='items/CreateDataFromTable/*'/>
    public TData CreateDataFromTable(DataTable dataTable, DataRow dataRow = null
      , LJCDataColumns dataDefinition = null)
    {
      LJCReflect reflect;
      string columnName;
      string propertyName;
      TData retValue = null;

      if (NetCommon.HasColumns(dataTable)
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
    private string GetPropertyName(LJCDataColumns propertyMapping, string columnName)
    {
      // Similar logic in LJCDBMessage.DbResult.GetRowValues().
      LJCDataColumn dataColumn;
      string retValue = columnName;

      if (propertyMapping != null)
      {
        //dataColumn = propertyMapping.LJCSearchRenameAs(columnName);
        var keys = LJC.Keys(LJCDataColumn.ColumnRenameAs, columnName);
        dataColumn = propertyMapping.LJCGetUnique(keys);
        if (dataColumn != null)
        {
          retValue = dataColumn.PropertyName;
        }
        else
        {
          //dataColumn = propertyMapping.LJCSearchColumnName(columnName);
          keys = LJC.Keys(LJCDataColumn.ColumnColumnName, columnName);
          dataColumn = propertyMapping.LJCGetUnique(keys);
          if (dataColumn != null)
          {
            retValue = dataColumn.PropertyName;
          }
        }
      }
      return retValue;
    }
  }
}
