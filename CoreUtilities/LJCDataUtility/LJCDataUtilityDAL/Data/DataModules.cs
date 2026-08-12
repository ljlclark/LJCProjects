// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModules.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataModule objects.
  /// <include file='Doc/DataModules.xml'
  ///  path='members/DataModules/*'/>
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

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataModules LJCGetCollection(List<DataModule> list)
    {
      DataModules retValue = null;

      if (LJC.HasListItems(list))
      {
        retValue = new DataModules();
        foreach (DataModule item in list)
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
      if (LJC.HasListItems(items))
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
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataModules Clone()
    {
      var retValue = MemberwiseClone() as DataModules;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCHasItems/*'/>
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
    /// <include file='Doc/DataModules.xml'
    ///  path='members/Add/*'/>
    public DataModule Add(short dbId, long id, string name)
    {
      DataModule retValue;

      string message = "";
      if (dbId <= 0)
      {
        message += "dataSiteID must be greater than zero.\r\n";
      }
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      retValue = LJCGetWithUnique(name);
      if (null == retValue)
      {
        retValue = new DataModule()
        {
          DbId = dbId,
          Id = id,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection item.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithID/*'/>
    public DataModule LJCGetWithID(short dbID, long id)
    {
      DataModule retValue = null;

      LJCSortID();
      DataModule searchItem = new DataModule()
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

    // Retrieve the collection item with unique values.
    /// <include file='Doc/DataModules.xml'
    ///  path='members/LJCGetWithUnique/*'/>
    public DataModule LJCGetWithUnique(string name)
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
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCRemove/*'/>
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

    // Sort on ID.
    /// <include file='Doc/DataModules.xml'
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
    /// <include file='Doc/DataModules.xml'
    ///  path='members/LJCSortUnique/*'/>
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

    // Gets the Default File Name.
    /// <include file='Doc/DataModules.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataModules.xml"; }
    }

    // The item for the supplied name.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/NameIndexer/*'/>
    public DataModule this[string name]
    {
      get => LJCGetWithUnique(name);
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

