// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CollectionTemplate.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCGenDocDAL
{
  /// <summary>Represents a collection of DocClassGroupHeading objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DocClassGroupHeadings")]
  public class DocClassGroupHeadings : List<DocClassGroupHeading>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DocClassGroupHeadings LJCDeserialize(string fileSpec = null)
    {
      DocClassGroupHeadings retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (false == File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        retValue = NetCommon.XmlDeserialize(typeof(DocClassGroupHeadings), fileSpec)
        as DocClassGroupHeadings;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocClassGroupHeadings()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocClassGroupHeadings(DocClassGroupHeadings items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DocClassGroupHeading(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocClassGroupHeading Add(short id, string name)
    {
      DocClassGroupHeading retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      NetString.AddMissingArgument(message, name);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCSearchUnique(name);
      if (null == retValue)
      {
        retValue = new DocClassGroupHeading()
        {
          ID = id,
          Name = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocClassGroupHeadings Clone()
    {
      var retValue = new DocClassGroupHeadings();
      foreach (DocClassGroupHeading item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocClassGroupHeadings GetCollection(List<DocClassGroupHeading> list)
    {
      DocClassGroupHeadings retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new DocClassGroupHeadings();
        foreach (DocClassGroupHeading item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
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
    /// <include path='items/LJCSerialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocClassGroupHeading LJCSearchUnique(string name)
    {
      DocClassGroupHeadingUniqueComparer comparer;
      DocClassGroupHeading retValue = null;

      comparer = new DocClassGroupHeadingUniqueComparer();
      LJCSortUnique(comparer);
      DocClassGroupHeading searchItem = new DocClassGroupHeading()
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

    /// <summary>Sort on Name.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DocClassGroupHeadingUniqueComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Name) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Name;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DocClassGroupHeadings.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Name
    }
    #endregion
  }
}

