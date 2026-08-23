// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTables5.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL5
{
  // Represents a collection of DataTable objects.
  /// <include file='Doc/DataTables.xml'
  ///  path='members/DataTables/*'/>
  [XmlRoot("DataTables")]
  public class DataTables : List<DataUtilTable>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static DataTables? LJCDeserialize(string? fileSpec = null)
    {
      DataTables? retValue;

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
        retValue = LJC.XmlDeserialize(typeof(DataTables), fileSpec)
          as DataTables;
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public static DataTables? LJCGetCollection(List<DataUtilTable> list)
    {
      DataTables? retValue = null;

      if (LJC.HasListItems(list))
      {
        retValue = [.. list];
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTables()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataTables");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataTables(DataTables items) : this()
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataUtilTable(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataTables Clone()
    {
      var retValue = new DataTables();
      foreach (DataUtilTable dataTable in this)
      {
        var newDataTable = dataTable.Clone();
        if (newDataTable != null)
        {
          retValue.Add(newDataTable);
        }
      }

      // Testing?
      //var retValue = MemberwiseClone() as DataTables;
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
    /// <include file='Doc/DataTables5.xml'
    ///  path='members/Add/*'/>
    public DataUtilTable Add(short dbId, long id, short dataModuleDbId
      , long dataModuleId, string name)
    {
      DataUtilTable? retValue = null;

      _ArgError.IDCheck(dbId, id);
      UniqueCheck(dataModuleDbId, dataModuleId, name);

      // Prevent search from sorting current items.
      var checkTables = Clone();
      var duplicate = checkTables.LJCGetUnique(dataModuleDbId, dataModuleId
        , name);
      if (duplicate != null)
      {
        retValue = duplicate.Clone();
      }

      if (null == retValue)
      {
        retValue = new DataUtilTable()
        {
          DbId = dbId,
          Id = id,
          DataModuleDbId = dataModuleDbId,
          DataModuleId = dataModuleId,
          Name = name,
        };
        Add(retValue);
      }
      return retValue;
    }

    // Retrieve the collection item.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetWithID/*'/>
    public DataUtilTable? LJCGetWithID(short dbId, long id)
    {
      DataUtilTable? retValue = null;
      
      _ArgError.IDCheck(dbId, id);

      LJCSortId();
      var searchItem = new DataUtilTable()
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
    /// <include file='Doc/DataTables5.xml'
    ///  path='members/LJCGetUnique/*'/>
    public DataUtilTable? LJCGetUnique(short dataModuleDbId
      , long dataModuleId, string name)
    {
      DataUtilTable? retValue = null;

      UniqueCheck(dataModuleDbId , dataModuleId , name);

      var comparer = new DataTableUnique();
      LJCSortUnique(comparer);
      var searchItem = new DataUtilTable()
      {
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
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
    /// <include file='Doc/DataTables5.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(short dataModuleDbId, long dataModuleId
      , string name)
    {
      UniqueCheck(dataModuleDbId, dataModuleId , name);

      DataUtilTable? item = Find(x => x.DataModuleDbId == dataModuleDbId
        && x.DataModuleId == dataModuleId
        && x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Checks the unique parameters.
    private void UniqueCheck(short dataModuleDbId
      , long dataModuleId, string name)
    {
      string message = "";
      if (dataModuleDbId <= 0)
      {
        message += "dataModuleDbID must be greater than zero.\r\n";
      }
      if (dataModuleId <= 0)
      {
        message += "dataModuleID must be greater than zero.\r\n";
      }
      _ArgError.Add(message);
      _ArgError.Add(name, "name");
      LJCNetString.ThrowArgError(_ArgError.ToString());
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    /// <include file='Doc/DataTables5.xml'
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

    // Sort on unique values.
    /// <include file='Doc/DataTables5.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataTableUnique comparer)
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
    /// <include file='Doc/DataTables5.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get => "DataTables.xml";
    }

    // The item for the supplied values.
    /// <include file='Doc/DataTables5.xml'
    ///  path='members/UniqueIndexer/*'/>
    public DataUtilTable? this[short dataTableDbID, long dataTableID
      , string name]
    {
      get => LJCGetUnique(dataTableDbID, dataTableID, name);
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

