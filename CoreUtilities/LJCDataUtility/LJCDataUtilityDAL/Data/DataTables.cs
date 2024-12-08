// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTables.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of DataTable objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DataTables")]
  public class DataTables : List<DataUtilTable>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DataTables LJCDeserialize(string fileSpec = null)
    {
      DataTables retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DataTables), fileSpec)
        as DataTables;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataTables()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataTables");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataTables(DataTables items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataUtilTable(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataUtilTable Add(int id, int dataModuleID, string name)
    {
      DataUtilTable retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add((object)name, "name");
      NetString.ThrowArgError(message);

      retValue = LJCSearchUnique(dataModuleID, name);
      if (null == retValue)
      {
        retValue = new DataUtilTable()
        {
          ID = id,
          DataModuleID = dataModuleID,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataTables Clone()
    {
      var retValue = MemberwiseClone() as DataTables;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataTables GetCollection(List<DataUtilTable> list)
    {
      DataTables retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataTables();
        foreach (DataUtilTable item in list)
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
    public void LJCRemove(int dataModuleID, string name)
    {
      DataUtilTable item = Find(x => x.DataModuleID == dataModuleID
        && x.Name == name);
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
    public DataUtilTable LJCSearchID(int id)
    {
      DataUtilTable retValue = null;

      LJCSortID();
      DataUtilTable searchItem = new DataUtilTable()
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
    public DataUtilTable LJCSearchUnique(int dataModuleID, string name)
    {
      DataUtilTable retValue = null;

      var comparer = new DataTableUniqueComparer();
      LJCSortUnique(comparer);
      DataUtilTable searchItem = new DataUtilTable()
      {
        DataModuleID = dataModuleID,
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
    public void LJCSortUnique(DataTableUniqueComparer comparer)
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
      get { return "DataTables.xml"; }
    }

    // The item for the specified name.
    /// <include path='items/Item/*' file='Doc/DbColumns.xml'/>
    public DataUtilTable this[int dataTableID, string name]
    {
      get { return LJCSearchUnique(dataTableID, name); }
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

