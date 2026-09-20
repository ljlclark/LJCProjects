// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbGroups5.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCDataSiteDAL5
{
  // Represents a collection of DbGroup objects.
  /// <include file='Doc/DbGroups.xml'
  ///  path='members/DbGroups/*'/>
  [XmlRoot("DbGroups")]
  public class DbGroups : List<DbGroup>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DbGroups? LJCDeserialize(string? fileSpec = null)
    {
      DbGroups? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = LJC.XmlDeserialize(typeof(DbGroups), fileSpec)
        as DbGroups;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DbGroups? LJCGetCollection(List<DbGroup> list)
    {
      DbGroups? retValue = null;

      if (LJC.HasListItems(list))
      {
        //retValue = new DbGroups();
        //foreach (DbGroup item in list)
        //{
        //  retValue.Add(item);
        //}
        retValue = [.. list];
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DbGroups()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataSites");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DbGroups(DbGroups items) : this()
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbGroup(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DbGroups? Clone()
    {
      var retValue = MemberwiseClone() as DbGroups;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCHasItems2/*'/>
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

    // Creates and adds the object from the provided values.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/Add/*'/>
    public DbGroup Add(short id, string name)
    {
      DbGroup? retValue = null;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      _ArgError.Add(message);
      _ArgError.Add((object)name, "name");
      LJCNetString.ThrowArgError(message);

      // Prevent search from sorting current items.
      var checkTables = Clone();
      if (checkTables != null)
      {
        var duplicate = checkTables.LJCGetUnique(name);
        if (duplicate != null)
        {
          retValue = duplicate.Clone();
        }
      }

      if (null == retValue)
      {
        retValue = new DbGroup()
        {
          ID = id,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithId/*'/>
    public DbGroup? LJCGetWithId(short id)
    {
      DbGroup? retValue = null;

      LJCSortId();
      var searchItem = new DbGroup()
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
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetUnique/*'/>
    public DbGroup? LJCGetUnique(string name)
    {
      DbGroup? retValue = null;

      var comparer = new DataSiteUnique();
      LJCSortUnique(comparer);
      var searchItem = new DbGroup()
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

    // Removes an item by keys.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(string name)
    {
      DbGroup? item = Find(x => x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    // Perform default sort.
    /// <include file='Doc/DataGroups.xml'
    ///  path='members/LJCSortId/*'/>
    public void LJCSortId()
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.Id) != 0)
      {
        _PrevCount = Count;
        Sort();
        _SortType = SortType.Id;
      }
    }

    // Sort on Unique values.
    /// <include file='Doc/DataGroups.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataSiteUnique comparer)
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
    /// <include file='Doc/DataGroups.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataSites.xml"; }
    }

    // The item for the specified name.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/NameIndexer/*'/>
    public DbGroup? this[string name]
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
      Id,
      Unique
    }
    #endregion
  }
}

