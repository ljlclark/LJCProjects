// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModules.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of DataModule objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DataModules")]
  public class DataModules : List<DataModule>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DataModules LJCDeserialize(string fileSpec = null)
    {
      DataModules retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DataModules), fileSpec)
        as DataModules;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataModules()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataModules");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataModules(DataModules items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataModule(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataModule Add(long id, long dataSiteID, string name)
    {
      DataModule retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (dataSiteID <= 0)
      {
        message += "dataSiteID must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(name, "name");
      NetString.ThrowArgError(mArgError.ToString());

      retValue = LJCSearchUnique(name);
      if (null == retValue)
      {
        retValue = new DataModule()
        {
          ID = id,
          DataSiteID = dataSiteID,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataModules Clone()
    {
      var retValue = MemberwiseClone() as DataModules;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataModules GetCollection(List<DataModule> list)
    {
      DataModules retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataModules();
        foreach (DataModule item in list)
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
      DataModule item = Find(x => x.Name == name);
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
    public DataModule LJCSearchID(int id)
    {
      DataModule retValue = null;

      LJCSortID();
      DataModule searchItem = new DataModule()
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
    public DataModule LJCSearchUnique(string name)
    {
      DataModule retValue = null;

      var comparer = new DataModuleUniqueComparer();
      LJCSortUnique(comparer);
      DataModule searchItem = new DataModule()
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
    public void LJCSortUnique(DataModuleUniqueComparer comparer)
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
      get { return "DataModules.xml"; }
    }

    // The item for the specified name.
    /// <include path='items/Item/*' file='Doc/DbColumns.xml'/>
    public DataModule this[string name]
    {
      get { return LJCSearchUnique(name); }
    }
    #endregion

    #region Class Data

    private ArgError mArgError;
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

