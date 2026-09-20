// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntrySites5.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCDataSiteDAL5
{
  // Represents a collection of DataEntrySite objects.
  /// <include file='Doc/DataEntrySites.xml'
  ///  path='members/DataEntrySites/*'/>
  [XmlRoot("DataEntrySites")]
  public class DataEntrySites : List<DataEntrySite>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataEntrySites? LJCDeserialize(string? fileSpec = null)
    {
      DataEntrySites? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = LJC.XmlDeserialize(typeof(DataEntrySites), fileSpec)
        as DataEntrySites;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataEntrySites? LJCGetCollection(List<DataEntrySite> list)
    {
      DataEntrySites? retValue = null;

      if (LJC.HasListItems(list))
      {
        //retValue = new DataEntrySites();
        //foreach (DataEntrySite item in list)
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
    public DataEntrySites()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataEntrySites");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataEntrySites(DataEntrySites items) : this()
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataEntrySite(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataEntrySites? Clone()
    {
      var retValue = MemberwiseClone() as DataEntrySites;
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
    /// <include file='Doc/DataEntrySites.xml'
    ///  path='members/Add/*'/>
    public DataEntrySite Add(long dataEntryId, long dataEntrySiteId
      , long dataSiteID)
    {
      DataEntrySite? retValue = null;

      string message = "";
      if (dataEntryId <= 0)
      {
        message += "dataEntryId must be greater than zero.\r\n";
      }
      if (dataEntrySiteId <= 0)
      {
        message += "dataEntrySiteId must be greater than zero.\r\n";
      }
      if (dataSiteID <= 0)
      {
        message += "dataSiteId must be greater than zero.\r\n";
      }
      _ArgError.Add(message);
      LJCNetString.ThrowArgError(message);

      // Prevent search from sorting current items.
      var checkTables = Clone();
      if (checkTables != null)
      {
        var duplicate = checkTables.LJCGetUnique(dataEntryId, dataEntrySiteId
          , dataSiteID);
        if (duplicate != null)
        {
          retValue = duplicate.Clone();
        }
      }

      if (null == retValue)
      {
        retValue = new DataEntrySite()
        {
          DataEntryID = dataEntryId,
          DataEntrySiteID = dataEntrySiteId,
          DataSiteID = dataSiteID
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element with primary key.
    /// <include file='Doc/DataEntrySites.xml'
    ///  path='members/LJCGetUnique/*'/>
    public DataEntrySite? LJCGetUnique(long dataEntryID, long dataEntrySiteID
      , long dataSiteID)
    {
      DataEntrySite? retValue = null;

      LJCSortId();
      DataEntrySite searchItem = new DataEntrySite()
      {
        DataEntryID = dataEntryID,
        DataEntrySiteID = dataEntrySiteID,
        DataSiteID = dataSiteID
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Removes an item by unique keys.
    /// <include file='Doc/DataEntrySites.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(long dataEntryID, long dataEntrySiteID
      , long dataSiteID)
    {
      DataEntrySite? item = Find(x =>
        x.DataEntryID == dataEntryID
        && x.DataEntrySiteID == dataEntrySiteID
        && x.DataSiteID == dataSiteID);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataEntrySites.xml'
    ///  path='members/LJCSortId/*'/>
    public void LJCSortId()
    {
      if (Count != _PrevCount)
      {
        _PrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/DataEntrySites.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get => "DataEntrySites.xml";
    }
    #endregion

    #region Class Data

    private readonly LJCArgError _ArgError;
    private int _PrevCount;
    #endregion
  }
}

