// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Modifiers.cs
using System.Collections;
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Represents a collection of Modifiers.
  /// <include file='Doc/ProjectModifiers.xml'
  ///  path='members/Modifiers/*'/>
  public class Modifiers : IEnumerable<string>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static List<string> Deserialize(string? fileSpec = null)
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
        retValue =
        [
          "abstract",
          "extern",
          "internal",
          "protected",
          "sealed",
          "virtual"
        ];
        LJC.XmlSerialize(typeof(List<string>), retValue, null, fileSpec
          , RootName);
      }

      if (null == retValue)
      {
        retValue = [];
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems1/*'/>
    public static bool HasItems(Modifiers collection)
    {
      bool retValue = false;

      if (collection != null && collection.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/Modifiers.xml'
    ///  path='members/DefaultFileName/*'/>
    public static string DefaultFileName
    {
      get => @"Keywords\Modifiers.xml";
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "Modifiers";
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public Modifiers()
    {
      Items = [];
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public Modifiers(Modifiers items)
    {
      Items = [];
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
    /// <include file='Doc/Modifiers.xml'
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
    public Modifiers Clone()
    {
      var retValue = new Modifiers();
      foreach (string modifier in this)
      {
        retValue.Add(modifier);
      }
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
    public void Serialize(string? fileSpec = null)
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
    /// <include file='Doc/Modifiers.xml'
    ///  path='members/Count/*'/>
    public int Count
    {
      get => Items.Count;
    }

    // Gets the Collection Enumerator.
    /// <include file='Doc/Modifiers.xml'
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

    // The KeyItem items.
    /// <include file='Doc/Modifiers.xml'
    ///  path='members/Items/*'/>
    [XmlArray(RootName)]
    public List<string> Items { get; set; }

    // Gets the item by index value.
    /// <include file='Doc/Modifiers.xml'
    ///  path='members/Indexer/*'/>
    public string? this[int index]
    {
      get
      {
        string? retValue = null;

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
    public string? SearchName(string name)
    {
      string? retValue = null;

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
  }
}
