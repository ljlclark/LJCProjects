// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTablesX.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataTable objects.
  /// <include file='Doc/DataTablesNew.xml'
  ///  path='members/DataTablesNew/*'/>
  [XmlRoot("DataTables")]
  public class DataTablesX : List<DataUtilTableX>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTablesX()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataTables");
      //mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataTablesX(DataTablesX items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataUtilTableX(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataTablesX Clone()
    {
      var retValue = MemberwiseClone() as DataTablesX;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public DataTablesX LJCGetCollection(List<DataUtilTableX> list)
    {
      DataTablesX retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataTablesX();
        foreach (DataUtilTableX item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/LJCGetWithUnique/*'/>
    public DataUtilTableX LJCGetWithUnique(DbColumns keyColumns)
    {
      DataUtilTableX retDataTable = null;

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
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        mSortType = SortType.Unique;

        var comparer = new DataTableComparerX
        {
          ColumnNames = new List<string>()
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

    // The item for the supplied name.
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/NameIndexer/*'/>
    public DataUtilTableX this[int dataTableID, string name]
    {
      get
      {
        return null;
        //return LJCSearchUnique(dataTableID, name);
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

