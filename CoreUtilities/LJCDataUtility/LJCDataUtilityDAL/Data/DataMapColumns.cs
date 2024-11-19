// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataMapColumns.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of DataMapColumn objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DataMapColumns")]
  public class DataMapColumns : List<DataMapColumn>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DataMapColumns LJCDeserialize(string fileSpec = null)
    {
      DataMapColumns retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DataMapColumns), fileSpec)
        as DataMapColumns;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapColumns()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMapColumns(DataMapColumns items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataMapColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    //public DataMapColumn Add(int dataTableMapID, int dataColumnID)
    public DataMapColumn Add(int dataColumnID)
    {
      DataMapColumn retValue;

      //retValue = LJCSearch(dataTableMapID, dataColumnID);
      retValue = LJCSearch(dataColumnID);
      if (null == retValue)
      {
        retValue = new DataMapColumn()
        {
          //DataTableMapID = dataTableMapID,
          DataColumnID = dataColumnID
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapColumns Clone()
    {
      var retValue = MemberwiseClone() as DataMapColumns;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMapColumns GetCollection(List<DataMapColumn> list)
    {
      DataMapColumns retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataMapColumns();
        foreach (DataMapColumn item in list)
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
    //public void LJCRemove(int dataTableMapID, int dataColumnID)
    public void LJCRemove(int dataColumnID)
    {
      //DataMapColumn item = Find(x => x.DataColumnID == dataColumnID
      //  && x.DataTableMapID == dataTableMapID);
      DataMapColumn item = Find(x => x.DataColumnID == dataColumnID);
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
    //public DataMapColumn LJCSearch(int dataTableMapID, int dataColumnID)
    public DataMapColumn LJCSearch(int dataColumnID)
    {
      DataMapColumn retValue = null;

      LJCSort();
      DataMapColumn searchItem = new DataMapColumn()
      {
        //DataTableMapID = dataTableMapID,
        DataColumnID = dataColumnID
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
      get { return "DataMapColumns.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}

