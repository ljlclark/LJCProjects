// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTypes.cs
using System.Collections;
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Represents a collection of Data Types.
  /// <include file='Doc/DataTypes.xml'
  ///  path='members/DataTypes/*'/>
  public class DataTypes : IEnumerable<string>
  {
    #region Static Methods

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
    public static bool HasItems(DataTypes collection)
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
    /// <include file='Doc/DataTypes.xml'
    ///  path='members/DefaultFileName/*'/>
    public static string DefaultFileName
    {
      get => @"Keywords\DataTypes.xml";
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "DataTypes";
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTypes()
    {
      Items = [];
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/ConstructorCopy/*'/>
    public DataTypes(DataTypes items) : this()
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
    /// <include file='Doc/DataTypes.xml'
    ///  path='members/Add/*'/>
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
    public DataTypes Clone()
    {
      var retValue = new DataTypes();
      foreach (string dataType in this)
      {
        retValue.Add(dataType);
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
    ///  path='members/Serialize/*'/>
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

    #region Search and Sort Methods

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

    #region IEnumerable Methods

    // Gets the Collection Enumerator.
    /// <include file='Doc/DataTypes.xml'
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
    #endregion

    #region IEnumerable Properties

    // The Collection count.
    /// <include file='Doc/DataTypes.xml'
    ///  path='members/Count/*'/>
    public int Count
    {
      get => Items.Count;
    }

    // The KeyItem items.
    /// <include file='Doc/DataTypes.xml'
    ///  path='members/Items/*'/>
    [XmlArray(RootName)]
    public List<string> Items { get; set; }

    // Gets the item by index value.
    /// <include file='Doc/DataTypes.xml'
    ///  path='members/Indexer/*'/>
    public string? this[int index]
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
  }
}
