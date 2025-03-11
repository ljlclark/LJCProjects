// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntrySites.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataSiteDAL
{
  /// <summary>Represents a collection of DataEntrySite objects.</summary>
  [XmlRoot("DataEntrySites")]
  public class DataEntrySites : List<DataEntrySite>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DataEntrySites LJCDeserialize(string fileSpec = null)
    {
      DataEntrySites retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = NetCommon.XmlDeserialize(typeof(DataEntrySites), fileSpec)
        as DataEntrySites;
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntrySites()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataEntrySites");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataEntrySites(DataEntrySites items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataEntrySite(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the supplied values.
    /// <include path='items/Add/*' file='Doc/DataEntrySites.xml'/>
    public DataEntrySite Add(long dataEntryID, long dataEntrySiteID
      , long dataSiteID)
    {
      DataEntrySite retValue;

      string message = "";
      if (dataEntryID <= 0)
      {
        message += "dataEntryID must be greater than zero.\r\n";
      }
      if (dataEntrySiteID <= 0)
      {
        message += "dataEntrySiteID must be greater than zero.\r\n";
      }
      if (dataSiteID <= 0)
      {
        message += "dataSiteID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      NetString.ThrowArgError(message);

      retValue = LJCSearchPrimary(dataEntryID, dataEntrySiteID, dataSiteID);
      if (null == retValue)
      {
        retValue = new DataEntrySite()
        {
          DataEntryID = dataEntryID,
          DataEntrySiteID = dataEntrySiteID,
          DataSiteID = dataSiteID
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntrySites Clone()
    {
      var retValue = MemberwiseClone() as DataEntrySites;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/LJCGetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataEntrySites LJCGetCollection(List<DataEntrySite> list)
    {
      DataEntrySites retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataEntrySites();
        foreach (DataEntrySite item in list)
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

    // Removes an item by unique keys.
    /// <include path='items/LJCRemove/*' file='Doc/DataEntrySites.xml'/>
    public void LJCRemove(long dataEntryID, long dataEntrySiteID
      , long dataSiteID)
    {
      DataEntrySite item = Find(x =>
        x.DataEntryID == dataEntryID
        && x.DataEntrySiteID == dataEntrySiteID
        && x.DataSiteID == dataSiteID);
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

    // Retrieve the collection element with primary key.
    /// <include path='items/LJCSearchPrimary/*' file='Doc/DataEntrySites.xml'/>
    public DataEntrySite LJCSearchPrimary(long dataEntryID, long dataEntrySiteID
      , long dataSiteID)
    {
      DataEntrySite retValue = null;

      LJCSortPrimary();
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

    /// <summary>Sort on Primary key.</summary>
    public void LJCSortPrimary()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DataEntrySites.xml"; }
    }
    #endregion

    #region Class Data

    private readonly ArgError mArgError;
    private int mPrevCount;
    #endregion
  }
}

