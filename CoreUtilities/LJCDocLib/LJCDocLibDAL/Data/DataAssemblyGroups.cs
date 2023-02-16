// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CollectionTemplate.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDocLibDAL
{
  /// <summary>Represents a collection of DocAssemblyGroup objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DocAssemblyGroups")]
  public class DocAssemblyGroups : List<DocAssemblyGroup>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DocAssemblyGroups LJCDeserialize(string fileSpec = null)
    {
      DocAssemblyGroups retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DocAssemblyGroups), fileSpec)
        as DocAssemblyGroups;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAssemblyGroups()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DocAssemblyGroups(DocAssemblyGroups items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DocAssemblyGroup(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DocAssemblyGroup Add(short id, string name)
    {
      DocAssemblyGroup retValue;

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
        retValue = new DocAssemblyGroup()
        {
          ID = id,
          Heading = name
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAssemblyGroups Clone()
    {
      var retValue = MemberwiseClone() as DocAssemblyGroups;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DocAssemblyGroups GetCollection(List<DocAssemblyGroup> list)
    {
      DocAssemblyGroups retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new DocAssemblyGroups();
        foreach (DocAssemblyGroup item in list)
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

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
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

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public DocAssemblyGroup LJCSearchUnique(string name)
    {
      DocAssemblyGroupUniqueComparer comparer;
      DocAssemblyGroup retValue = null;

      comparer = new DocAssemblyGroupUniqueComparer();
      LJCSortUnique(comparer);
      DocAssemblyGroup searchItem = new DocAssemblyGroup()
      {
        Heading = name
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DocAssemblyGroupUniqueComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Heading) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Heading;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DocAssemblyGroups.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Heading
    }
    #endregion
  }
}

