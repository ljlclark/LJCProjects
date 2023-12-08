// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CommonDataTypes.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  /// <summary>Represents a collection of Common Data Types.</summary>
  public class CommonDataTypes : IEnumerable<string>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public static List<string> Deserialize(string fileSpec = null)
    {
      List<string> retValue;

      if (!NetString.HasValue(fileSpec))
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
        // The default list of common Data Types.
        retValue = new List<string>
        {
          "bool",
          "Boolean",
          "int",
          "Int16",
          "Int32",
          "object",
          "Object",
          "short",
          "string",
          "String",
          "var",
          "void"
        };
        NetCommon.XmlSerialize(typeof(List<string>), retValue, null, fileSpec
          , RootName);
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public static bool HasItems(CommonDataTypes collectionObject)
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
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public CommonDataTypes()
    {
      Items = new List<string>();
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public CommonDataTypes(CommonDataTypes items)
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public CommonDataTypes Clone()
    {
      CommonDataTypes retValue = MemberwiseClone() as CommonDataTypes;
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

    // Serializes the collection
    /// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public void Serialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      NetCommon.XmlSerialize(typeof(List<string>), this, null, fileSpec
        , RootName);
    }
    #endregion

    #region IEnumerable Methods and Properties

    /// <summary>The Collection count.</summary>
    public int Count
    {
      get { return Items.Count; }
    }

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

    #region Sort and Search Methods

    // Retrieve the collection element.
    /// <include path='items/SearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
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
    /// <include path='items/SortName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public void SortName()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Items.Sort();
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string DefaultFileName
    {
      get { return @"Keywords\CommonDataTypes.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "CommonDataTypes";
    #endregion
  }
}
