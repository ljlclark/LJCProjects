// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModules5.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL5
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
    public static DataModules? LJCDeserialize(string? fileSpec = null)
    {
      DataModules? retValue;

      if (!LJC.HasText(fileSpec))
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
        retValue = LJC.XmlDeserialize(typeof(DataModules), fileSpec)
        as DataModules;
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataModules? LJCGetCollection(List<DataModule> list)
    {
      DataModules? retValue = null;

      if (LJC.HasListItems(list))
      {
        retValue = [.. list];
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataModules()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataModules");
      _PrevCount = -1;
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
      var retValue = new DataModules();
      foreach (DataModule dataModule in this)
      {
        var newDataModule = dataModule.Clone();
        if (newDataModule != null)
        {
          retValue.Add(newDataModule);
        }
      }

      // Testing?
      //var retValue = MemberwiseClone() as DataModules;
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
    public void LJCSerialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Creates and adds the object from the supplied values.
    /// <include file='Doc/DataModules.xml'
    ///  path='members/Add/*'/>
    public DataModule Add(short dbId, long id, string name)
    {
      DataModule? retValue = null;

      _ArgError.IDCheck(dbId, id);
      UniqueCheck(name);
      _ArgError.ThrowError();

      // Prevent search from sorting current items.
      var checkModules = Clone();
      var duplicate = checkModules.LJCGetUnique(name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

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
    public DataModule? LJCGetWithID(short dbID, long id)
    {
      DataModule? retValue = null;
      
      _ArgError.IDCheck(dbID, id);
      _ArgError.ThrowError();

      LJCSortID();
      var searchItem = new DataModule()
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
    public DataModule? LJCGetUnique(string name)
    {
      DataModule? retValue = null;

      UniqueCheck(name);
      _ArgError.ThrowError();

      var comparer = new DataModuleUnique();
      LJCSortUnique(comparer);
      var searchItem = new DataModule()
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
      UniqueCheck(name);
      _ArgError.ThrowError();

      //DataModule? item = Find(x => x.Name == name);
      var item = LJCGetUnique(name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Checks the unique parameters.
    private void UniqueCheck(string name)
    {
      _ArgError.Add(name, "name");
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataModules.xml'
    ///  path='members/LJCSortID/*'/>
    public void LJCSortID()
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.ID) != 0)
      {
        _PrevCount = Count;
        Sort();
        _SortType = SortType.ID;
      }
    }

    // Sort on Unique values.
    /// <include file='Doc/DataModules.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataModuleUnique comparer)
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.Unique) != 0)
      {
        _PrevCount = Count;
        Sort(comparer);
        _SortType = SortType.Unique;
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
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataModule? this[string name]
    {
      get => LJCGetUnique(name);
    }
    #endregion

    #region Class Data

    private readonly LJCArgError _ArgError;
    private int _PrevCount;
    private SortType _SortType;

    private enum SortType
    {
      ID,
      Unique
    }
    #endregion
  }
}

