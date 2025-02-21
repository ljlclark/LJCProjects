// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumns.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of DataUtilColumn objects.</summary>
  [XmlRoot("DataColumns")]
  public class DataColumns : List<DataUtilColumn>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DataColumns LJCDeserialize(string fileSpec = null)
    {
      DataColumns retValue;

      if (!NetString.HasValue(fileSpec))
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
        retValue = NetCommon.XmlDeserialize(typeof(DataColumns), fileSpec)
        as DataColumns;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataColumns()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataColumns");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataColumns(DataColumns items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataUtilColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <summary>
    /// Creates and adds the object from the provided values.
    /// </summary>
    /// <param name="parentTableID"></param>
    /// <param name="parentSiteID"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public DataUtilColumn Add(long parentTableID, long parentSiteID
      , string name)
    {
      DataUtilColumn retValue = null;

      string message = "";
      if (parentTableID <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      // Prevent search from sorting current items.
      var checkColumns = this.Clone();
      var duplicate = checkColumns.LJCSearchUnique(parentTableID
        , parentSiteID, name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          ID = parentTableID,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    /// <summary>Creates and adds the object from the provided values.</summary>
    /// <param name="parentTableID">The parent table ID.</param>
    /// <param name="parentSiteID"></param>
    /// <param name="name">The item name value.</param>
    /// <param name="typeName"></param>
    /// <param name="allowNull"></param>
    /// <param name="maxLength"></param>
    /// <param name="defaultValue"></param>
    /// <param name="identityIncrement"></param>
    public DataUtilColumn Add(long parentTableID, long parentSiteID
      , string name, string typeName, bool allowNull = true, short maxLength = 0
      , string defaultValue = null, short identityIncrement = 0)
    {
      DataUtilColumn retValue = null;

      // Prevent search from sorting current items.
      var checkColumns = Clone();
      var duplicate = checkColumns.LJCSearchUnique(parentTableID, parentSiteID
        , name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          Name = name,
          TypeName = typeName,
          AllowNull = allowNull,
          MaxLength = maxLength,
          DefaultValue = defaultValue,
          IdentityIncrement = identityIncrement
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataColumns Clone()
    {
      var retValue = new DataColumns();
      foreach (DataUtilColumn dataColumn in this)
      {
        retValue.Add(dataColumn.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataColumns GetCollection(List<DataUtilColumn> list)
    {
      DataColumns retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataColumns();
        foreach (DataUtilColumn item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Removes an item by name.
    /// <summary>
    /// Removes an item by name.
    /// </summary>
    /// <param name="parentTableID"></param>
    /// <param name="name">The item name value.</param>
    public void LJCRemove(long parentTableID, string name)
    {
      DataUtilColumn item = Find(x => x.DataTableID == parentTableID
        && x.Name == name);
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

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DataUtilColumn LJCSearchID(int id)
    {
      DataUtilColumn retValue = null;

      LJCSortID();
      DataUtilColumn searchItem = new DataUtilColumn()
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
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="parentTableID">The parent dataTable ID.</param>
    /// <param name="parentSiteID">The parent dataTable site ID</param>
    /// <param name="name">The dataTable name.</param>
    /// <returns>The data column object.</returns>
    public DataUtilColumn LJCSearchUnique(long parentTableID, long parentSiteID
      , string name)
    {
      DataUtilColumn retValue = null;

      var comparer = new DataColumnUnique();
      LJCSortUnique(comparer);
      DataUtilColumn searchItem = new DataUtilColumn()
      {
        DataTableID = parentTableID,
        DataTableSiteID = parentSiteID,
        Name = name
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

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DataColumnUnique comparer)
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
      get { return "DataColumns.xml"; }
    }

    // The item for the specified name.
    /// <summary>
    /// The item for the supplied name.
    /// </summary>
    /// <param name="parentTableID">The parent ID value.</param>
    /// <param name="parentSiteID"></param>
    /// <param name="name">The item name.</param>
    /// <returns>The selected item.</returns>
    public DataUtilColumn this[long parentTableID, long parentSiteID
      , string name]
    {
      get
      {
        return LJCSearchUnique(parentTableID, parentSiteID, name);
      }
    }
    #endregion

    #region Class Data

    private readonly ArgError mArgError;
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

