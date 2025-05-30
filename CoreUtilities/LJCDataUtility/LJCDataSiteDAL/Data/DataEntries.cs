﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntries.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataSiteDAL
{
  /// <summary>Represents a collection of DataEntry objects.</summary>
  [XmlRoot("DataEntries")]
  public class DataEntries : List<DataEntry>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DataEntries LJCDeserialize(string fileSpec = null)
    {
      DataEntries retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = NetCommon.XmlDeserialize(typeof(DataEntries), fileSpec)
        as DataEntries;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntries()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataEntries");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataEntries(DataEntries items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataEntry(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the supplied values.
    /// <include path='items/Add/*' file='Doc/DataEntries.xml'/>
    public DataEntry Add(long id, long dataSiteID, DateTime entryTime
      , string entryData)
    {
      DataEntry retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (dataSiteID <= 0)
      {
        message += "dataSiteID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      NetString.ThrowArgError(message);

      retValue = LJCSearchUnique(dataSiteID, entryTime);
      if (null == retValue)
      {
        retValue = new DataEntry()
        {
          ID = id,
          DataSiteID = dataSiteID,
          EntryTime = entryTime,
          EntryData = entryData
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntries Clone()
    {
      var retValue = MemberwiseClone() as DataEntries;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/LJCGetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataEntries LJCGetCollection(List<DataEntry> list)
    {
      DataEntries retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataEntries();
        foreach (DataEntry item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/LJCHasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public bool LJCHasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Removes an item by keys.
    /// <include path='items/LJCRemove/*' file='Doc/DataEntries.xml'/>
    public void LJCRemove(long dataSiteID, DateTime entryTime)
    {
      DataEntry item = Find(x =>
        x.DataSiteID == dataSiteID
        && x.EntryTime == entryTime);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataEntry LJCSearchID(long id)
    {
      DataEntry retValue = null;

      LJCSortPrimary();
      DataEntry searchItem = new DataEntry()
      {
        ID = id
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include path='items/LJCSearchUnique/*' file='Doc/DataEntries.xml'/>
    public DataEntry LJCSearchUnique(long parentID, DateTime entryTime)
    {
      DataEntry retValue = null;

      var comparer = new DataEntryUnique();
      LJCSortUnique(comparer);
      DataEntry searchItem = new DataEntry()
      {
        DataSiteID = parentID,
        EntryTime = entryTime
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Primary key.</summary>
    public void LJCSortPrimary()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Primary) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.Primary;
      }
    }

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DataEntryUnique comparer)
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

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DataEntries.xml"; }
    }
    #endregion

    #region Class Data

    private readonly ArgError mArgError;
    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Primary,
      Unique
    }
    #endregion
  }
}

