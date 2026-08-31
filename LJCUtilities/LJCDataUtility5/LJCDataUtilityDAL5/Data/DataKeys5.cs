// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKeys5.cs
using LJCNetCommon5;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL5
{
  // Represents a collection of DataKey objects.
  /// <include file='Doc/DataKeys.xml'
  ///  path='members/DataKeys/*'/>
  [XmlRoot("DataKeys")]
  public class DataKeys : List<DataKey>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataKeys? LJCDeserialize(string? fileSpec = null)
    {
      DataKeys? retValue;

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
        retValue = LJC.XmlDeserialize(typeof(DataKeys), fileSpec)
          as DataKeys;
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataKeys? LJCGetCollection(List<DataKey> list)
    {
      DataKeys? retValue = null;

      if (LJC.HasListItems(list))
      {
        retValue = [.. list];
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataKeys()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataKeys");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataKeys(DataKeys items) : this()
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
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataKeys Clone()
    {
      var retValue = new DataKeys();
      foreach (DataKey dataKey in this)
      {
        var newDataKey = dataKey.Clone();
        if (newDataKey != null)
        {
          retValue.Add(newDataKey);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/Add/*'/>
    public DataKey Add(short dbId, long id, short dataTableDbId
      , long dataTableId, string name)
    {
      DataKey? retValue = null;

      _ArgError.IDCheck(dbId, id);
      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      // Prevent search from sorting current items.
      var checkTables = Clone();
      var duplicate = checkTables.LJCGetUnique(dataTableDbId, dataTableId
        , name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

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
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCGetWithID/*'/>
    public DataKey? LJCGetWithID(short dbID, long id)
    {
      DataKey? retValue = null;

      _ArgError.IDCheck(dbID, id);
      _ArgError.ThrowError();

      LJCSortID();
      var searchItem = new DataKey()
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
    ///  path='members/LJCGetUnique/*'/>
    public DataKey? LJCGetUnique(short dataTableDbId, long dataTableId
      , string name)
    {
      DataKey? retValue = null;

      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      var comparer = new DataKeyUnique();
      LJCSortUnique(comparer);
      var searchItem = new DataKey()
      {
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
    public void LJCRemove(short dataTableDbId, long dataTableId, string name)
    {
      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      //DataKey? item = Find(x => x.DataTableDbId == dataTableDbId
      //  && x.DataTableId == dataTableId
      //  && x.Name == name);
      var item = LJCGetUnique(dataTableDbId, dataTableId, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Checks the unique parameters.
    private void UniqueCheck(short dataTableDbId
      , long dataTableId, string name)
    {
      string message = "";
      if (dataTableDbId <= 0)
      {
        message += "dataTableDbID must be greater than zero.\r\n";
      }
      if (dataTableId <= 0)
      {
        message += "dataTableID must be greater than zero.\r\n";
      }
      _ArgError.Add(message);
      _ArgError.Add(name, "name");
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataKeys.xml'
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
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataKeyUnique comparer)
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
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataKeys.xml"; }
    }

    // The item for the supplied name.
    /// <include file='Doc/DataKeys.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataKey? this[short dataTableDbID, long dataTableID, string name]
    {
      get => LJCGetUnique(dataTableDbID, dataTableID, name);
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

