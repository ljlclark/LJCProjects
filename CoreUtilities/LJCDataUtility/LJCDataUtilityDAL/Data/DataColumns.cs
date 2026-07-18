// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumns.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataColumn objects.
  /// <include file='Doc/DataColumns.xml'
  ///  path='members/DataColumns/*'/>
  [XmlRoot("DataColumns")]
  public class DataColumns : List<DataUtilColumn>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
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

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataColumns LJCGetCollection(List<DataUtilColumn> list)
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
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataColumns()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataColumns");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataColumns(DataColumns items) : this()
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

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataColumns Clone()
    {
      var retValue = new DataColumns();
      foreach (DataUtilColumn dataColumn in this)
      {
        retValue.Add(dataColumn.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems/*'/>
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
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Creates and adds the object from the supplied values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/Add1/*'/>
    public DataUtilColumn Add(long id, short dbID, long dataTableID
      , short dataTableDbID, string name)
    {
      DataUtilColumn retValue = null;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (dbID <= 0)
      {
        message += "dbID must be greater than zero.\r\n";
      }
      if (dbID <= 0)
      {
        message += "dbID must be greater than zero.\r\n";
      }
      if (dataTableID <= 0)
      {
        message += "dataTableID must be greater than zero.\r\n";
      }
      if (dataTableDbID <= 0)
      {
        message += "dataTableDbID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      // Prevent search from sorting current items.
      var checkColumns = Clone();
      var duplicate = checkColumns.LJCGetWithUnique(dataTableID
        , dataTableDbID, name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          ID = id,
          DataSiteID = dbID,
          DataTableID = dataTableID,
          DataTableSiteID = dataTableDbID,
          Name = name,
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and adds the object from the provided values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/Add2/*'/>
    public DataUtilColumn Add(short dbID, long dataTableID
      , short dataTableDbID, string name, string typeName
      , bool allowNull = true, short maxLength = 0, string defaultValue = null
      , short identityIncrement = 0)
    {
      DataUtilColumn retValue = null;
      string message = "";
      if (dbID <= 0)
      {
        message += "dbID must be greater than zero.\r\n";
      }
      if (dataTableID <= 0)
      {
        message += "dataTableID must be greater than zero.\r\n";
      }
      if (dataTableDbID <= 0)
      {
        message += "dataTableDbID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      // Prevent search from sorting current items.
      var checkColumns = Clone();
      var duplicate = checkColumns.LJCGetWithUnique(dataTableID, dataTableDbID
        , name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          DataSiteID = dbID,
          DataTableID = dataTableID,
          DataTableSiteID = dataTableDbID,
          Name = name,

          TypeName = typeName,
          AllowNull = allowNull,
          MaxLength = maxLength,
          DefaultValue = defaultValue,
          IdentityIncrement = identityIncrement,
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection element.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithID/*'/>
    public DataUtilColumn LJCGetWithID(long id, short dbID)
    {
      DataUtilColumn retValue = null;

      LJCSortID();
      DataUtilColumn searchItem = new DataUtilColumn()
      {
        ID = id,
        DataSiteID = dbID,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithUnique/*'/>
    public DataUtilColumn LJCGetWithUnique(long dataTableID
      , short dataTableDbID, string name)
    {
      DataUtilColumn retValue = null;

      var comparer = new DataColumnUnique();
      LJCSortUnique(comparer);
      DataUtilColumn searchItem = new DataUtilColumn()
      {
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
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
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(long dataTableID, long dataTableDbID, string name)
    {
      DataUtilColumn item = Find(x => x.DataTableID == dataTableID
        && x.DataTableSiteID == dataTableDbID
        && x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCSortID/*'/>
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

    // Sort on Unique values.
    /// <include file='Doc/DataTables.xml'
    ///  path='members/LJCSortUnique/*'/>
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

    // Gets the Default File Name.
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataColumns.xml"; }
    }

    // The item for the specified name.
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataUtilColumn this[long dataTableID, short dataTableDbID
      , string name]
    {
      get => LJCGetWithUnique(dataTableID, dataTableDbID, name);
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

