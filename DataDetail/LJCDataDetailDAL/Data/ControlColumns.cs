// ControlColumns.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDataDetailDAL
{
  /// <summary>Represents a collection of ControlColumn objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("ControlColumns")]
  public class ControlColumns : List<ControlColumn>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(ControlColumns collectionObject)
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
    public static ControlColumns LJCDeserialize(string fileSpec = null)
    {
      ControlColumns retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(ControlColumns), fileSpec)
        as ControlColumns;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumns()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='Doc/ControlColumns.xml'/>
    public ControlColumns(ControlColumns items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ControlColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/ControlColumns.xml'/>
    public ControlColumn Add(int id, int controlTabID, int columnIndex)
    {
      ControlColumn retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (controlTabID <= 0)
      {
        message += "controlTabID must be greater than zero.\r\n";
        NetString.AddMissingArgument(message, ControlColumn.ColumnControlTabID);
      }
      NetString.ThrowInvalidArgument(message);

      retValue = LJCSearchUnique(controlTabID, columnIndex);
      if (null == retValue)
      {
        retValue = new ControlColumn()
        {
          ID = id,
          ControlTabID = controlTabID,
          ColumnIndex = columnIndex
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumns Clone()
    {
      var retValue = MemberwiseClone() as ControlColumns;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public ControlColumns GetCollection(List<ControlColumn> list)
    {
      ControlColumns retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new ControlColumns();
        foreach (ControlColumn item in list)
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

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='Doc/ControlColumns.xml'/>
    public ControlColumn LJCSearchID(long id)
    {
      ControlColumn retValue = null;

      LJCSortID();
      ControlColumn searchItem = new ControlColumn()
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
    /// <include path='items/LJCSearchUnique/*' file='Doc/ControlColumns.xml'/>
    public ControlColumn LJCSearchUnique(long controlTabID, int columnIndex)
    {
      ControlColumnUniqueComparer comparer;
      ControlColumn retValue = null;

      comparer = new ControlColumnUniqueComparer();
      LJCSortUnique(comparer);
      ControlColumn searchItem = new ControlColumn()
      {
        ControlTabID = controlTabID,
        ColumnIndex = columnIndex
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
    public void LJCSortUnique(ControlColumnUniqueComparer comparer)
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
      get { return "ControlColumns.xml"; }
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

