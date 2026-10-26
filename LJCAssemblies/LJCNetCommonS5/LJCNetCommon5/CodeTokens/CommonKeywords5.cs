// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CommonKeywords.cs
using System.Collections;
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  /// <summary>Represents a collection of Common Key Words.</summary>
  /// <include file='Doc/CommonKeywords.xml'
  ///  path='members/CommonKeywords/*'/>
  public class CommonKeywords : IEnumerable<string>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
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
        // The default list of common Keywords.
        retValue =
        [
          "break",
          "case",
          "else",
          "false",
          "for",
          "foreach",
          "get",
          "if",
          "in",
          "namespace",
          "new",
          "null",
          "return",
          "set",
          "switch",
          "true",
          "using",
          "while"
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
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/HasItems1/*'/>
    public static bool HasItems(CommonKeywords collection)
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
    /// <include file='Doc/CommonKeywords.xml'
    ///  path='members/DefaultFileName/*'/>
    public static string DefaultFileName
    {
      get => @"Keywords\CommonKeywords.xml";
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "CommonKeywords";
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public CommonKeywords()
    {
      Items = [];
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/ConstructorCopy/*'/>
    public CommonKeywords(CommonKeywords items) : this()
    {
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
    /// <include file='Doc/CommonKeywords.xml'
    ///  path='members/Add/*'/>
    public void Add(string item)
    {
      if (LJC.HasText(item))
      {
        Items.Add(item);
      }
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public CommonKeywords Clone()
    {
      var retValue = new CommonKeywords();
      foreach (string keyword in this)
      {
        retValue.Add(keyword);
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='Doc/CommonKeywords.xml'
    ///  path='members/Count/*'/>
    public int Count
    {
      get => Items.Count;
    }

    // Gets the Collection Enumerator.
    /// <include file='Doc/CommonKeywords.xml'
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
    /// <include file='Doc/CommonKeywords.xml'
    ///  path='members/Items/*'/>
    [XmlArray(RootName)]
    public List<string> Items { get; set; }

    // Gets the item by index value.
    /// <include file='Doc/CommonKeywords.xml'
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
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
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
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
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
