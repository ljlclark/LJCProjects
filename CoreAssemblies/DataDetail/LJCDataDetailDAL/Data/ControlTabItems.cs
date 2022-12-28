// CollectionTemplate.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LJCDataDetailDAL
{
  /// <summary>Represents a collection of ControlTab objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("ControlTabItems")]
  public class ControlTabItems : List<ControlTab>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(ControlTabItems collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public static ControlTabItems LJCDeserialize(string fileSpec = null)
    {
      ControlTabItems retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(ControlTabItems), fileSpec)
        as ControlTabItems;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlTabItems()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='Doc/ControlTabs.xml'/>
    public ControlTabItems(ControlTabItems items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ControlTab(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/ControlTabs.xml'/>
    public ControlTab Add(long id, long controlDetailID, int tabIndex)
    {
      ControlTab retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (controlDetailID <= 0)
      {
        message += "controlDetailID must be greater than zero.\r\n";
        NetString.AddMissingArgument(message, ControlDetail.ColumnID);
      }
      NetString.ThrowInvalidArgument(message);

      retValue = LJCSearchUnique(controlDetailID, tabIndex);
      if (null == retValue)
      {
        retValue = new ControlTab()
        {
          ID = id,
          ControlDetailID = controlDetailID,
          TabIndex = tabIndex
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlTabItems Clone()
    {
      var retValue = MemberwiseClone() as ControlTabItems;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public ControlTabItems GetCollection(List<ControlTab> list)
    {
      ControlTabItems retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new ControlTabItems();
        foreach (ControlTab item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/LJCSearchID/*' file='Doc/ControlTabs.xml'/>
    public ControlTab LJCSearchID(long id)
    {
      ControlTab retValue = null;

      LJCSortID();
      ControlTab searchItem = new ControlTab()
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
    /// <include path='items/LJCSearchUnique/*' file='Doc/ControlTabs.xml'/>
    public ControlTab LJCSearchUnique(long controlDetailID, int tabIndex)
    {
      ControlTabUniqueComparer comparer;
      ControlTab retValue = null;

      comparer = new ControlTabUniqueComparer();
      LJCSortUnique(comparer);
      ControlTab searchItem = new ControlTab()
      {
        ControlDetailID = controlDetailID,
        TabIndex = tabIndex
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

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

    /// <summary>Sort on Unique key.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(ControlTabUniqueComparer comparer)
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
      get { return "ControlTabs.xml"; }
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

