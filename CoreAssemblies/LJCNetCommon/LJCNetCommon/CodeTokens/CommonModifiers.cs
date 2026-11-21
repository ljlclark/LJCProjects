// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CommonModifiers.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  /// <summary>Represents a collection of Common Modifiers.</summary>
  public class CommonModifiers : IEnumerable<string>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static List<string> Deserialize(string fileSpec = null)
    {
      List<string> retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      if (File.Exists(fileSpec))
      {
        retValue = LJC.XmlDeserialize(typeof(List<string>), fileSpec
          , RootName) as List<string>;
      }
      else
      {
        // The default list of common Modifiers.
        retValue = new List<string>
        {
          "override",
          "private",
          "protected",
          "public",
          "static"
        };
        LJC.XmlSerialize(typeof(List<string>), retValue, null, fileSpec
          , RootName);
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems1/*'/>
    public static bool HasItems(CommonModifiers collection)
    {
      bool retValue = false;

      if (collection != null && collection.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public CommonModifiers()
    {
      Items = new List<string>();
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public CommonModifiers(CommonModifiers items)
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
    /// <include file='Doc/CommonModifiers.xml'
    ///  path='members/Add1/*'/>
    public void Add(string item)
    {
      if (LJC.HasText(item))
      {
        Items.Add(item);
      }
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public CommonModifiers Clone()
    {
      CommonModifiers retValue = MemberwiseClone() as CommonModifiers;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems2/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    public void Serialize(string fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      LJC.XmlSerialize(typeof(List<string>), this, null, fileSpec
        , RootName);
    }
    #endregion

    #region IEnumerable Methods and Properties

    // The Collection count.
    /// <include file='Doc/CommonModifiers.xml'
    ///  path='members/Count/*'/>
    public int Count
    {
      get { return Items.Count; }
    }

    // Gets the Collection Enumerator.
    /// <include file='Doc/CommonModifiers.xml'
    ///  path='members/GetEnumerator/*'/>
    public IEnumerator<string> GetEnumerator()
    {
      return ((IEnumerable<string>)Items).GetEnumerator();
    }

    // Gets the Collection Enumerator.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<string>)Items).GetEnumerator();
    }

    // The Collection items.
    /// <include file='Doc/CommonModifiers.xml'
    ///  path='members/Items/*'/>
    [XmlArray(RootName)]
    public List<string> Items { get; set; }

    // Gets the item by index value.
    /// <include file='Doc/CommonModifiers.xml'
    ///  path='members/Indexer/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/SearchName/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/SortName/*'/>
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
      get { return @"Keywords\CommonModifiers.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "CommonModifiers";
    #endregion
  }
}
