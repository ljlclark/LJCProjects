// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTables.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataTable objects.
  /// <include file='Doc/DataTables.xml'
  ///  path='members/DataTables/*'/>
  [XmlRoot("DataTables")]
  public class DataTables : List<DataUtilTable>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataTables2 LJCDeserialize(string fileSpec = null)
    {
      DataTables2 retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        retValue = NetCommon.XmlDeserialize(typeof(DataTables2), fileSpec)
         as DataTables2;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTables()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataTables");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataTables(DataTables items) : this()
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataUtilTable(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataTables Clone()
    {
      //var retValue = new DataTables();
      //foreach (DataUtilTable dataTable in this)
      //{
      //  retValue.Add(dataTable.Clone());
      //}

      // Testing
      var retValue = MemberwiseClone() as DataTables;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public DataTables LJCGetCollection(List<DataUtilTable> list)
    {
      DataTables retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataTables();
        foreach (DataUtilTable item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems/*'/>
    public bool LJCHasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Serializes the collection to a file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Creates and adds the object from the supplied values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/Add/*'/>
    public DataUtilTable Add(long dbID, long dataModuleID
      , short dataModuleDbID, string name)
    {
      DataUtilTable retValue = null;

      string message = "";
      if (dbID <= 0)
      {
        message += "dbID must be greater than zero.\r\n";
      }
      if (dataModuleID <= 0)
      {
        message += "dataModuleID must be greater than zero.\r\n";
      }
      if (dataModuleDbID <= 0)
      {
        message += "dataModuleDbID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(message);

      // Prevent search from sorting current items.
      var checkTables = Clone();
      var duplicate = checkTables.LJCGetWithUnique(dataModuleID
        , dataModuleDbID, name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilTable()
        {
          DataSiteID = dbID,
          DataModuleID = dataModuleID,
          DataModuleSiteID = dataModuleDbID,
          Name = name,
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithID/*'/>
    public DataUtilTable LJCGetWithID(long id, short dbID)
    {
      DataUtilTable retValue = null;

      LJCSortID();
      DataUtilTable searchItem = new DataUtilTable()
      {
        ID = id,
        DataSiteID = dbID,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCGetWithUnique/*'/>
    public DataUtilTable LJCGetWithUnique(long dataModuleID
      , short dataModuleDbID, string name)
    {
      DataUtilTable retValue = null;

      var comparer = new DataTableUniqueComparer();
      LJCSortUnique(comparer);
      DataUtilTable searchItem = new DataUtilTable()
      {
        DataModuleID = dataModuleID,
        DataModuleSiteID = dataModuleDbID,
        Name = name,
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Removes an item by name.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(long dataModuleID, short dataModuleDbID
      , string name)
    {
      DataUtilTable item = Find(x => x.DataModuleID == dataModuleID
        && x.DataModuleSiteID == dataModuleDbID
        && x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCSortID/*'/>
    public void LJCSortID()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.ID) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.ID;
      }
    }

    // Sort on Unique values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataTableUniqueComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Unique;
      }
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataTables.xml"; }
    }

    // The item for the supplied values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataUtilTable this[long dataTableID, short dataTableDbID
      , string name]
    {
      get => LJCGetWithUnique(dataTableID, dataTableDbID, name);
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

