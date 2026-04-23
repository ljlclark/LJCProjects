// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCResultConverter.cs
using System.Data;
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Converts DbColumns and DbResult objects to data objects.
  /// <include path='items/ResultConverter/*' file='Doc/ResultConverter.xml'/>
  public class LJCResultConverter<TData, TList>
    where TData : class, new()
    where TList : List<TData>, new()
  {
    #region public Methods

    // Creates a collection from the result records.
    /// <include path='items/CreateCollection/*' file='Doc/ResultConverter.xml'/>
    public TList? CreateCollection(LJCDBResult dbResult)
    {
      // Also in LJCDBClientLib.ObjectManager.
      // Used here to allow for different TList and TData.
      // Testing in LJCDBServiceLib.TestDbDataAccess.
      TList? retValue = null;

      if (dbResult != null)
      {
        if (LJCDBResult.HasRows(dbResult))
        {
          retValue = new TList();
          foreach (LJCDBRow dbRow in dbResult.Rows)
          {
            TData? dataRecord = CreateData(dbRow.Values);
            if (dataRecord != null)
            {
              retValue.Add(dataRecord);
            }
          }
        }
      }
      return retValue;
    }

    // Creates a Data Object collection from the Table rows.
    /// <include path='items/CreateCollectionFromTable/*' file='Doc/ResultConverter.xml'/>
    public TList? CreateCollectionFromTable(DataTable dataTable
      , LJCDataColumns? dataColumns = null)
    {
      // Testing in LJCDBServiceLib.TestDbDataAccess.
      TList? retValue = null;

      if (LJC.HasColumns(dataTable)
        && LJC.HasData(dataTable))
      {
        retValue = new TList();
        foreach (DataRow dataRow in dataTable.Rows)
        {
          TData? dataObject = CreateDataFromTable(dataTable, dataRow
            , dataColumns);
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
    public TData CreateData(LJCDataColumns dataColumns)
    {
      TData retValue;

      // Populate a data object with the result values.
      // Uses retValue as an object and processes with reflection.
      retValue = new TData();
      LJCDBCommon.SetObjectValues(dataColumns, retValue);
      LJCDBCommon.ClearChanged(retValue);
      return retValue;
    }

    // Creates a Data Object from the result values.
    /// <include path='items/CreateData2/*' file='Doc/ResultConverter.xml'/>
    public TData? CreateData(LJCDBResult dbResult)
    {
      TData? retValue = null;

      if (LJCDBResult.HasRows(dbResult))
      {
        retValue = CreateData(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Creates a Data Object from the data values.
    /// <include path='items/CreateData3/*' file='Doc/ResultConverter.xml'/>
    public TData? CreateData(LJCDataValues? dataValues)
    {
      // Also in LJCDBClientLib.ObjectManager.
      // Used here to allow for different TData.
      TData? retValue = null;

      if (LJC.HasItems(dataValues))
      {
        // Populate a data object with the result values.
        // Uses retValue as an object and processes with reflection.
        retValue = new TData();
        LJCDBCommon.SetObjectValues(dataValues, retValue);
        LJCDBCommon.ClearChanged(retValue);
      }
      return retValue;
    }

    // Creates a Data Object from the row values.
    /// <include path='items/CreateDataFromTable/*' file='Doc/ResultConverter.xml'/>
    public TData? CreateDataFromTable(DataTable dataTable, DataRow? dataRow = null
      , LJCDataColumns? dataColumns = null)
    {
      LJCReflect reflect;
      string columnName;
      string propertyName;
      TData? retValue = null;

      if (LJC.HasColumns(dataTable)
        && LJC.HasData(dataTable)
        && LJC.HasItems(dataColumns))
      {
        retValue = new TData();

        if (null == dataRow)
        {
          dataRow = dataTable.Rows[0];
        }
        if (null == dataColumns)
        {

        }
        reflect = new LJCReflect(retValue);
        for (int index = 0; index < dataTable.Columns.Count; index++)
        {
          columnName = dataTable.Columns[index].ColumnName;
          propertyName = GetPropertyName(dataColumns, columnName);
          object value = dataRow[index];

          // Similar logic in LJCDBMessage.DbCommon.SetObjectValues().
          reflect.SetPropertyValue(propertyName, value);
        }
        LJCDBCommon.ClearChanged(retValue);
      }
      return retValue;
    }
    #endregion

    // Gets the property name.
    private string GetPropertyName(LJCDataColumns? propertyMapping, string columnName)
    {
      // Similar logic in LJCDBMessage.DbResult.GetRowValues().
      LJCDataColumn? dataColumn;
      string retValue = columnName;

      if (propertyMapping != null)
      {
        dataColumn = propertyMapping?.LJCSearchRenameAs(columnName);
        if (dataColumn != null)
        {
          retValue = dataColumn.PropertyName;
        }
        else
        {
          dataColumn = propertyMapping?.LJCSearchColumnName(columnName);
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
