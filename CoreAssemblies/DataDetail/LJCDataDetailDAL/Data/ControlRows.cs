// ControlRows.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDataDetailDAL
{
  /// <summary>Represents a collection of ControlRow objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("ControlRows")]
  public class ControlRows : List<ControlRow>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(ControlRows collectionObject)
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
    public static ControlRows LJCDeserialize(string fileSpec = null)
    {
      ControlRows retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(ControlRows), fileSpec)
        as ControlRows;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlRows()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='Doc/ControlRows.xml'/>
    public ControlRows(ControlRows items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ControlRow(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/ControlRows.xml'/>
    public ControlRow Add(int id, int controlColumnID, string dataValueName)
    {
      ControlRow retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (controlColumnID <= 0)
      {
        message += "controlColumnID must be greater than zero.\r\n";
      }
      NetString.AddMissingArgument(message, dataValueName);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCSearchUnique(controlColumnID, dataValueName);
      if (null == retValue)
      {
        retValue = new ControlRow()
        {
          ID = id,
          ControlColumnID = controlColumnID,
          DataValueName = dataValueName
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlRows Clone()
    {
      var retValue = MemberwiseClone() as ControlRows;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public ControlRows GetCollection(List<ControlRow> list)
    {
      ControlRows retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new ControlRows();
        foreach (ControlRow item in list)
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

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='Doc/ControlRows.xml'/>
    public ControlRow LJCSearchID(long id)
    {
      ControlRow retValue = null;

      LJCSortID();
      ControlRow searchItem = new ControlRow()
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
    /// <include path='items/LJCSearchUnique/*' file='Doc/ControlRows.xml'/>
    public ControlRow LJCSearchUnique(long controlColumnID, string dataValueName)
    {
      ControlRowUniqueComparer comparer;
      ControlRow retValue = null;

      comparer = new ControlRowUniqueComparer();
      LJCSortUnique(comparer);
      ControlRow searchItem = new ControlRow()
      {
        ControlColumnID = controlColumnID,
        DataValueName = dataValueName
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
    public void LJCSortUnique(ControlRowUniqueComparer comparer)
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
      get { return "ControlRows.xml"; }
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

