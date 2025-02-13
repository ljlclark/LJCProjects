﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlDetails.cs
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDataDetailDAL
{
  /// <summary>Represents a collection of DetailDialog objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DetailConfigs")]
  public class ControlDetails : List<ControlDetail>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public static ControlDetails LJCDeserialize(string fileSpec = null)
    {
      ControlDetails retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(ControlDetails), fileSpec)
        as ControlDetails;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDetails()
    {
      mPrevCount = -1;
      mArgError = new ArgError("LJCDataDetail.ControlColumns");
    }
    private ArgError mArgError;

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='Doc/DetailConfigs.xml'/>
    public ControlDetails(ControlDetails items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ControlDetail(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DetailConfigs.xml'/>
    public ControlDetail Add(int id, string userID, string dataConfigName
      , string tableName)
    {
      ControlDetail retValue;

      mArgError.MethodName = "Add";
      string message = "";
      if (id <= 0)
      {
        message = "id must be greater than zero.\r\n";
        mArgError.Add(message);
      }
      NetString.AddObjectArgError(ref message, dataConfigName
        , "dataConfigName");
      NetString.AddObjectArgError(ref message, dataConfigName
        , "tableName");
      NetString.ThrowArgError(mArgError.ToString());

      retValue = LJCSearchUnique(userID, dataConfigName, tableName);
      if (null == retValue)
      {
        retValue = new ControlDetail()
        {
          ID = id,
          UserID = userID,
          DataConfigName = dataConfigName,
          TableName = tableName
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDetails Clone()
    {
      var retValue = new ControlDetails();
      foreach (ControlDetail controlDetail in this)
      {
        retValue.Add(controlDetail.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public ControlDetails GetCollection(List<ControlDetail> list)
    {
      ControlDetails retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new ControlDetails();
        foreach (ControlDetail item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
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
    /// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='Doc/DetailConfigs.xml'/>
    public ControlDetail LJCSearchID(int id)
    {
      ControlDetail retValue = null;

      LJCSortID();
      ControlDetail searchItem = new ControlDetail()
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
    /// <include path='items/LJCSearchUnique/*' file='Doc/DetailConfigs.xml'/>
    public ControlDetail LJCSearchUnique(string userID, string dataConfigName
      , string tableName)
    {
      ControlDetailUniqueComparer comparer;
      ControlDetail retValue = null;

      comparer = new ControlDetailUniqueComparer();
      LJCSortUnique(comparer);
      ControlDetail searchItem = new ControlDetail()
      {
        UserID = userID,
        DataConfigName = dataConfigName,
        TableName = tableName
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

    /// <summary>Sort on Name.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(ControlDetailUniqueComparer comparer)
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
      get { return "ControlDetails.xml"; }
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

