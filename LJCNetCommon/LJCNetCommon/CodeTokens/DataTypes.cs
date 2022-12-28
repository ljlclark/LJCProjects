﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTypes.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  /// <summary>Represents a collection of Data Types.</summary>
  public class DataTypes : IEnumerable<string>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static List<string> Deserialize(string fileSpec = null)
    {
      List<string> retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      if (File.Exists(fileSpec))
      {
        retValue = NetCommon.XmlDeserialize(typeof(List<string>), fileSpec
          , RootName) as List<string>;
      }
      else
      {
        // The default list of common Modifiers.
        retValue = new List<string>
        {
          "byte",
          "Byte",
          "char",
          "Char",
          "decimal",
          "Decimal",
          "double",
          "Double",
          "float",
          "Int64",
          "long",
          "Single"
        };
        NetCommon.XmlSerialize(typeof(List<string>), retValue, null, fileSpec
          , RootName);
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(DataTypes collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataTypes()
    {
      Items = new List<string>();
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataTypes(DataTypes items)
    {
      Items = new List<string>();
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(item);
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the specified object.
    /// <include path='items/Add1/*' file='Doc/KeyItems.xml'/>
    public void Add(string item)
    {
      if (NetString.HasValue(item))
      {
        Items.Add(item);
      }
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataTypes Clone()
    {
      DataTypes retValue = MemberwiseClone() as DataTypes;
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

    // Serializes the collection
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public void Serialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      NetCommon.XmlSerialize(typeof(List<string>), this, null, fileSpec
        , RootName);
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/SearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public string SearchName(string name)
    {
      string retValue = null;

      SortName();
      int index = Items.BinarySearch(name);
      if (index >= 0)
      {
        retValue = Items[index];
      }
      return retValue;
    }

    // Sort on Name.
    /// <include path='items/SortName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public void SortName()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Items.Sort();
      }
    }
    #endregion

    #region IEnumerable Methods

    // Gets the Collection Enumerator.
    /// <include path='items/GetEnumerator/*' file='Doc/KeyItems.xml'/>
    public IEnumerator<string> GetEnumerator()
    {
      return ((IEnumerable<string>)Items).GetEnumerator();
    }

    // Gets the Collection Enumerator.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<string>)Items).GetEnumerator();
    }
    #endregion

    #region IEnumerable Properties

    /// <summary>The Collection count.</summary>
    public int Count
    {
      get { return Items.Count; }
    }

    /// <summary>The KeyItem items.</summary>
    [XmlArray(RootName)]
    public List<string> Items { get; set; }

    // Gets the item by index value.
    /// <include path='items/Indexer/*' file='Doc/KeyItems.xml'/>
    public string this[int index]
    {
      get
      {
        string retValue = null;

        if (index >= 0 && index < Count)
        {
          retValue = Items[index];
        }
        return retValue;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string DefaultFileName
    {
      get { return @"Keywords\DataTypes.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "DataTypes";
    #endregion
  }
}
