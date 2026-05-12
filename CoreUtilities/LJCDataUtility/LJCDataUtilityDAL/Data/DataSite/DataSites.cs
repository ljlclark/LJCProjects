// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataSites.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of DataSite objects.</summary>
  [XmlRoot("DataSites")]
  public class DataSites : List<DataSite>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DataSites LJCDeserialize(string fileSpec = null)
    {
      DataSites retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = NetCommon.XmlDeserialize(typeof(DataSites), fileSpec)
       as DataSites;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataSites()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataSites");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataSites(DataSites items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataSite(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataSite Add(int id, string name)
    {
      DataSite retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add((object)name, "name");
      NetString.ThrowArgError(message);

      retValue = LJCSearchUnique(name);
      if (null == retValue)
      {
        retValue = new DataSite()
        {
          ID = id,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataSites Clone()
    {
      var retValue = MemberwiseClone() as DataSites;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataSites GetCollection(List<DataSite> list)
    {
      DataSites retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataSites();
        foreach (DataSite item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Removes an item by name.
    /// <summary>
    /// Removes an item by name.
    /// </summary>
    /// <param name="name">The item unique Name value.</param>
    public void LJCRemove(string name)
    {
      DataSite item = Find(x => x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/LJCSearchCode/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataSite LJCSearchPrimary(long id)
    {
      DataSite retValue = null;

      LJCSortPrimary();
      DataSite searchItem = new DataSite()
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
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public DataSite LJCSearchUnique(string name)
    {
      DataSite retValue = null;

      var comparer = new DataSiteUniqueComparer();
      LJCSortUnique(comparer);
      DataSite searchItem = new DataSite()
      {
        Name = name
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Perform default sort.</summary>
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
    public void LJCSortUnique(DataSiteUniqueComparer comparer)
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
      get { return "DataSites.xml"; }
    }

    // The item for the specified name.
    /// <include path='items/Item/*' file='Doc/DbColumns.xml'/>
    public DataSite this[string name]
    {
      get { return LJCSearchUnique(name); }
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

