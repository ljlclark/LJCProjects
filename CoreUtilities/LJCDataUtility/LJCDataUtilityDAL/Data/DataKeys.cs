// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKeys.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataKey objects.
  /// <include file='Doc/DataKeys.xml'
  ///  path='members/DataKeys/*'/>
  [XmlRoot("DataKeys")]
  public class DataKeys : List<DataKey>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataKeys LJCDeserialize(string fileSpec = null)
    {
      DataKeys retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DataKeys), fileSpec)
        as DataKeys;
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataKeys LJCGetCollection(List<DataKey> list)
    {
      DataKeys retValue = null;

      if (LJC.HasListItems(list))
      {
        retValue = new DataKeys();
        foreach (DataKey item in list)
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
    public DataKeys()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataKeys");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataKeys(DataKeys items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataKey(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataKeys Clone()
    {
      var retValue = new DataKeys();
      foreach (DataKey dataKey in this)
      {
        retValue.Add(dataKey.Clone());
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

    // Creates and adds the object from the provided values.
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/Add/*'/>
    public DataKey Add(short dbId, long id, short dataTableDbId
      , long dataTableId , string name)
    {
      DataKey retValue;

      string message = "";
      if (dbId <= 0)
      {
        message += "dbID must be greater than zero.\r\n";
      }
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (dataTableDbId <= 0)
      {
        message += "dataTableDbID must be greater than zero.\r\n";
      }
      if (dataTableId <= 0)
      {
        message += "dataTableID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      retValue = LJCGetWithUnique(dataTableDbId, dataTableId, name);
      if (null == retValue)
      {
        retValue = new DataKey()
        {
          DbId = dbId,
          Id = id,
          DataTableDbId = dataTableDbId,
          DataTableId = dataTableId,
          Name = name,
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithID/*'/>
    public DataKey LJCGetWithID(short dbID, long id)
    {
      DataKey retValue = null;

      LJCSortID();
      DataKey searchItem = new DataKey()
      {
        DbId = dbID,
        Id = id,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/LJCGetWithUnique/*'/>
    public DataKey LJCGetWithUnique(short dataTableDbID, long dataTableID
      , string name)
    {
      DataKey retValue = null;

      var comparer = new DataKeyUniqueComparer();
      LJCSortUnique(comparer);
      DataKey searchItem = new DataKey()
      {
        DataTableDbId = dataTableDbID,
        DataTableId = dataTableID,
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
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(short dataTableDbID, long dataTableID, string name)
    {
      DataKey item = Find(x => x.DataTableDbId == dataTableDbID
        && x.DataTableId == dataTableID
        && x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataKeys.xml'
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
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataKeyUniqueComparer comparer)
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
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataKeys.xml"; }
    }

    // The item for the supplied name.
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataKey this[short dataTableDbID, long dataTableID, string name]
    {
      get { return LJCGetWithUnique(dataTableDbID, dataTableID, name); }
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

