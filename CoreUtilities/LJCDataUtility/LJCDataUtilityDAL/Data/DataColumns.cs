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
  /// <summary>Represents a collection of DataColumn objects.</summary>
  [XmlRoot("DataColumns")]
  public class DataColumns : List<DataUtilColumn>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataColumns()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataUtilColumn Add(int id, string name)
    {
      DataUtilColumn retValue = null;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      NetString.ArgError(ref message, name);
      NetString.ThrowArgError(message);

      // Prevent search from sorting current items.
      var checkItems = this.Clone();
      var duplicate = checkItems.LJCSearchUnique(name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          ID = id,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // <summary>Creates and adds the object from the provided values.</summary>
    public DataUtilColumn Add(string name, string typeName
      , bool allowNull = true, short maxLength = 0
      , string defaultValue = null, short identityIncrement = 0)
    {
      DataUtilColumn retValue = null;

      // Prevent search from sorting current items.
      var checkItems = Clone();
      var duplicate = checkItems.LJCSearchUnique(name);
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
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
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
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
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

    // Removes an item by name.
    /// <summary>
    /// Removes an item by name.
    /// </summary>
    /// <param name="name">The item unique Name value.</param>
    public void LJCRemove(string name)
    {
      DataUtilColumn item = Find(x => x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/LJCSearchCode/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public DataUtilColumn LJCSearchUnique(string name)
    {
      DataUtilColumn retValue = null;

      var comparer = new DataColumnUniqueComparer();
      LJCSortUnique(comparer);
      DataUtilColumn searchItem = new DataUtilColumn()
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

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DataColumnUniqueComparer comparer)
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
    /// <include path='items/Item/*' file='Doc/DbColumns.xml'/>
    public DataUtilColumn this[string name]
    {
      get { return LJCSearchUnique(name); }
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

