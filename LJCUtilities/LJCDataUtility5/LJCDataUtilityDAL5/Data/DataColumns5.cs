// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumns5.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL5
{
  // Represents a collection of DataColumn objects.
  /// <include file='Doc/DataColumns.xml'
  ///  path='members/DataColumns/*'/>
  [XmlRoot("DataColumns")]
  public class DataColumns : List<DataUtilColumn>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataColumns? LJCDeserialize(string? fileSpec = null)
    {
      DataColumns? retValue;

      if (!LJC.HasText(fileSpec))
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
        retValue = LJC.XmlDeserialize(typeof(DataColumns), fileSpec)
         as DataColumns;
      }
      return retValue;
    }

    // Get a custom collection from List<T>.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataColumns? LJCGetCollection(List<DataUtilColumn> list)
    {
      DataColumns? retValue = null;

      if (LJC.HasListItems(list))
      {
        retValue = [.. list];
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataColumns()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataColumns");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataColumns(DataColumns items) : this()
    {
      if (LJC.HasListItems(items))
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
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataColumns Clone()
    {
      var retValue = new DataColumns();
      foreach (DataUtilColumn dataColumn in this)
      {
        var newDataColumn = dataColumn.Clone();
        if (newDataColumn != null)
        {
          retValue.Add(newDataColumn);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/Add1/*'/>
    public DataUtilColumn Add(short dbId, long id, short dataTableDbId
      , long dataTableId, string name)
    {
      DataUtilColumn? retValue = null;

      _ArgError.IDCheck(dbId, id);
      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      // Prevent search from sorting current items.
      var checkColumns = Clone();
      var duplicate = checkColumns.LJCGetUnique(dataTableDbId
        , dataTableId, name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          DbId = dbId,
          Id = id,
          DataTableDbId = dataTableDbId,
          DataTableId = dataTableId,
          Name = name,
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and adds the object from the provided values.
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/Add2/*'/>
    public DataUtilColumn Add(short dbId, short dataTableDbId
      , long dataTableId, string name, string typeName
      , bool allowNull = true, short maxLength = 0, string? defaultValue = null
      , short identityIncrement = 0)
    {
      DataUtilColumn? retValue = null;

      _ArgError.DbIDCheck(dbId);
      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      // Prevent search from sorting current items.
      var checkColumns = Clone();
      var duplicate = checkColumns.LJCGetUnique(dataTableDbId, dataTableId
        , name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilColumn()
        {
          DbId = dbId,
          DataTableDbId = dataTableDbId,
          DataTableId = dataTableId,
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

    // Retrieve the collection item.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCGetWithId/*'/>
    public DataUtilColumn? LJCGetWithId(short dbId, long id)
    {
      DataUtilColumn? retValue = null;

      _ArgError.IDCheck(dbId, id);
      _ArgError.ThrowError();

      LJCSortId();
      var searchItem = new DataUtilColumn()
      {
        DbId = dbId,
        Id = id,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/LJCGetUnique/*'/>
    public DataUtilColumn? LJCGetUnique(short dataTableDbId
      , long dataTableId, string name)
    {
      DataUtilColumn? retValue = null;

      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      var comparer = new DataColumnUnique();
      LJCSortUnique(comparer);
      var searchItem = new DataUtilColumn()
      {
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
    public void LJCRemove(short dataTableDbId, long dataTableId, string name)
    {
      UniqueCheck(dataTableDbId, dataTableId, name);
      _ArgError.ThrowError();

      //DataUtilColumn? item = Find(x => x.DataTableDbId == dataTableDbId
      //  && x.DataTableId == dataTableId
      //  && x.Name == name);
      var item = LJCGetUnique(dataTableDbId, dataTableId, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Checks the unique parameters.
    private void UniqueCheck(short dataTableDbId
      , long dataTableId, string name)
    {
      string message = "";
      if (dataTableDbId <= 0)
      {
        message += "dataTableDbID must be greater than zero.\r\n";
      }
      if (dataTableId <= 0)
      {
        message += "dataTableID must be greater than zero.\r\n";
      }
      _ArgError.Add(message);
      _ArgError.Add(name, "name");
    }
    #endregion

    #region Sort Methods

    // Sort on IDs.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataColumnUnique comparer)
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
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DataColumns.xml"; }
    }

    // The item for the specified name.
    /// <include file='Doc/DataColumns.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataUtilColumn? this[short dataTableDbId, long dataTableId
      , string name]
    {
      get => LJCGetUnique(dataTableDbId, dataTableId, name);
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

