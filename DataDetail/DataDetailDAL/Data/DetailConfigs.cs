// DetailDialogs.cs
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;

namespace DataDetailDAL
{
  /// <summary>Represents a collection of DetailDialog objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DetailConfigs")]
  public class DetailConfigs : List<DetailConfig>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(DetailConfigs collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DetailConfigs LJCDeserialize(string fileSpec = null)
    {
      DetailConfigs retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(DetailConfigs), fileSpec)
        as DetailConfigs;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DetailConfigs()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DetailConfigs(DetailConfigs items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DetailConfig(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DetailConfig Add(int id, string userID, string dataConfigName
      , string tableName)
    {
      DetailConfig retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      NetString.AddMissingArgument(message, dataConfigName);
      NetString.ThrowInvalidArgument(message);
      NetString.AddMissingArgument(message, tableName);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCSearchUnique(userID, dataConfigName, tableName);
      if (null == retValue)
      {
        retValue = new DetailConfig()
        {
          ID = id,
          UserID = userID,
          DataConfigName = dataConfigName,
          TableName = tableName
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DetailConfigs Clone()
    {
      var retValue = MemberwiseClone() as DetailConfigs;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DetailConfigs GetCollection(List<DetailConfig> list)
    {
      DetailConfigs retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new DetailConfigs();
        foreach (DetailConfig item in list)
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

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchCode/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DetailConfig LJCSearchID(int id)
    {
      DetailConfig retValue = null;

      LJCSortID();
      DetailConfig searchItem = new DetailConfig()
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

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DetailConfig LJCSearchUnique(string userID, string dataConfigName
      , string tableName)
    {
      DetailDialogUniqueComparer comparer;
      DetailConfig retValue = null;

      comparer = new DetailDialogUniqueComparer();
      LJCSortUnique(comparer);
      DetailConfig searchItem = new DetailConfig()
      {
        UserID = userID,
        DataConfigName = dataConfigName,
        TableName = tableName
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Code.</summary>
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

    /// <summary>Sort on Name.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DetailDialogUniqueComparer comparer)
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
      get { return "DetailDialogs.xml"; }
    }
    #endregion

    #region Class Data

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

