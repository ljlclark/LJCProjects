// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntries5.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCDataSiteDAL5
{
  // Represents a collection of DataEntry objects.
  /// <include file='Doc/DataEntries.xml'
  ///  path='members/DataEntries/*'/>
  [XmlRoot("DataEntries")]
  public class DataEntries : List<DataEntry>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataEntries? LJCDeserialize(string? fileSpec = null)
    {
      DataEntries? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = LJC.XmlDeserialize(typeof(DataEntries), fileSpec)
        as DataEntries;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataEntries? LJCGetCollection(List<DataEntry> list)
    {
      DataEntries? retValue = null;

      if (LJC.HasListItems(list))
      {
        //retValue = new DataEntries();
        //foreach (DataEntry item in list)
        //{
        //  retValue.Add(item);
        //}
        retValue = [.. list];
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public DataEntries()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataEntries");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataEntries(DataEntries items) : this()
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataEntry(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataEntries? Clone()
    {
      var retValue = MemberwiseClone() as DataEntries;
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

    // Creates and adds the object from the supplied values.
    /// <include file='Doc/DataEntries.xml'
    ///  path='members/Add/*'/>
    public DataEntry Add(long id, long dataSiteId, DateTime entryTime
      , string entryData)
    {
      DataEntry? retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (dataSiteId <= 0)
      {
        message += "dataSiteID must be greater than zero.\r\n";
      }
      _ArgError.Add(message);
      LJCNetString.ThrowArgError(message);

      retValue = LJCGetUnique(dataSiteId, entryTime);
      if (null == retValue)
      {
        retValue = new DataEntry()
        {
          ID = id,
          DataSiteID = dataSiteId,
          EntryTime = entryTime,
          EntryData = entryData
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithId/*'/>
    public DataEntry? LJCGetWithId(long id)
    {
      DataEntry? retValue = null;

      LJCSortId();
      var searchItem = new DataEntry()
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
    /// <include file='Doc/DataEntries.xml'
    ///  path='members/LJCGetUnique/*'/>
    public DataEntry? LJCGetUnique(long dataSiteId, DateTime entryTime)
    {
      DataEntry? retValue = null;

      var comparer = new DataEntryUnique();
      LJCSortUnique(comparer);
      var searchItem = new DataEntry()
      {
        DataSiteID = dataSiteId,
        EntryTime = entryTime,
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Removes an item by keys.
    /// <include file='Doc/DataEntries.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(long dataSiteId, DateTime entryTime)
    {
      DataEntry? item = Find(x =>
        x.DataSiteID == dataSiteId
        && x.EntryTime == entryTime);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataEntries.xml'
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
    /// <include file='Doc/DataEntries.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataEntryUnique comparer)
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
    /// <include file='Doc/DataEntries.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataEntries.xml"; }
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

