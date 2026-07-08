// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModules.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataModule objects.
  /// <include file='Doc/DataModules.xml'
  ///  path='members/DataTables/*'/>
  [XmlRoot("DataModules")]
  public class DataModules : List<DataModule>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataModules LJCDeserialize(string fileSpec = null)
    {
      DataModules retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DataModules), fileSpec)
        as DataModules;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataModules()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataModules");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataModules(DataModules items) : this()
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataModule(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataModules Clone()
    {
      var retValue = MemberwiseClone() as DataModules;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataModules LJCGetCollection(List<DataModule> list)
    {
      DataModules retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataModules();
        foreach (DataModule item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
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

    #region Collection Data Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DataModules.xml'/>
    public DataModule Add(long id, long dataSiteID, string name)
    {
      DataModule retValue;

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
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      retValue = LJCGetWithName(name);
      if (null == retValue)
      {
        retValue = new DataModule()
        {
          ID = id,
          DataSiteID = dataSiteID,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataModule LJCGetWithID(int id)
    {
      DataModule retValue = null;

      LJCSortID();
      DataModule searchItem = new DataModule()
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
    /// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataModule LJCGetWithName(string name)
    {
      DataModule retValue = null;

      var comparer = new DataModuleUniqueComparer();
      LJCSortUnique(comparer);
      DataModule searchItem = new DataModule()
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

    // Removes an item by name.
    /// <include path='items/LJCRemove/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public void LJCRemove(string name)
    {
      DataModule item = Find(x => x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    /// <summary>Sort on ID.</summary>
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

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DataModuleUniqueComparer comparer)
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
      get { return "DataModules.xml"; }
    }

    // The item for the supplied name.
    /// <include path='items/NameIndexer/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataModule this[string name]
    {
      get { return LJCGetWithName(name); }
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

