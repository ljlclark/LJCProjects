// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTables2.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataTable objects.
  /// <include file='Doc/DataTables2.xml'
  ///  path='members/DataTables2/*'/>
  [XmlRoot("DataTables2")]
  public class DataTables2 : List<DataTable2>
  {
    #region Static Methods

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataTables2 LJCGetCollection(List<DataTable2> list)
    {
      DataTables2 retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataTables2();
        foreach (DataTable2 item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTables2()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataTables2");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataTables2(DataTables2 items) : this()
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataTable2(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataTables2 Clone()
    {
      var retValue = MemberwiseClone() as DataTables2;
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <include file='Doc/DataTables2.xml'
    ///  path='members/LJCGetWithUnique/*'/>
    public DataTable2 LJCGetWithUnique(DbColumns keyColumns)
    {
      DataTable2 retDataTable = null;

      LJCSortUnique();
      var foundIndex = DbColumns.LJCSearchColumns(this, keyColumns);
      if (foundIndex != -1)
      {
        retDataTable = this[foundIndex];
      }
      return retDataTable;
    }
    #endregion

    #region Sort Methods

    // Sort on unique column values.
    /// <include file='Doc/DataTables2.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        mSortType = SortType.Unique;

        var comparer = new DataTable2UniqueComparer
        {
          LJCColumnNames = new List<string>()
          {
            "DataModuleID",
            "DataModuleSiteID",
            "Name",
          }
        };
        Sort(comparer);
      }
    }
    #endregion

    #region Properties

    // The item for the supplied values.
    /// <include file='Doc/DataTables2.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataTable2 this[long dataModuleID, long dataModuleSiteID
      , object name]
    {
      get
      {
        var keyColumns = new DbColumns()
        {
          { "DataModuleID",  dataModuleID},
          { "DataModuleSiteID",  4},
          { "Name",  name },
        };
        return LJCGetWithUnique(keyColumns);
      }
    }
    #endregion

    #region Class Data

    private readonly ArgError mArgError;

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      ID,
      Unique
    }
    #endregion
  }
}

