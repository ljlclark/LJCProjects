// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataMapTables.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of DataMapTable objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DataMapTables")]
  public class DataMapTables : List<DataMapTable>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DataMapTables LJCDeserialize(string fileSpec = null)
    {
      DataMapTables retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DataMapTables), fileSpec)
        as DataMapTables;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapTables()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMapTables(DataMapTables items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataMapTable(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMapTable Add(int dataTableID, string newTableName)
    {
      DataMapTable retValue;

      retValue = LJCSearch(dataTableID);
      if (null == retValue)
      {
        retValue = new DataMapTable()
        {
          DataTableID = dataTableID,
          NewTableName = newTableName
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapTables Clone()
    {
      var retValue = MemberwiseClone() as DataMapTables;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMapTables GetCollection(List<DataMapTable> list)
    {
      DataMapTables retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataMapTables();
        foreach (DataMapTable item in list)
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
    public void LJCRemove(int dataTableID)
    {
      DataMapTable item = Find(x => x.DataTableID == dataTableID);
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
    public DataMapTable LJCSearch(int dataTableID)
    {
      DataMapTable retValue = null;

      LJCSort();
      DataMapTable searchItem = new DataMapTable()
      {
        DataTableID = dataTableID
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Code.</summary>
    public void LJCSort()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DataMapTables.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}

