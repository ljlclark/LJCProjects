﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAssemblyGroups.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCGenDocDAL
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
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DocAssemblyGroups LJCDeserialize(string fileSpec = null)
    {
      DocAssemblyGroups retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DocAssemblyGroups), fileSpec)
        as DocAssemblyGroups;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssemblyGroups()
    {
      ArgError = new ArgError("LJCGenDocDAL.DocAssemblyGroups");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
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
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocAssemblyGroup Add(short id, string name)
    {
      ArgError.MethodName = "Add(id, name)";
      if (id <= 0)
      {
        ArgError.Add("id must be greater than zero.");
      }
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = LJCSearchUnique(name);
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
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssemblyGroups Clone()
    {
      var retValue = new DocAssemblyGroups();
      foreach (DocAssemblyGroup item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocAssemblyGroups GetCollection(List<DocAssemblyGroup> list)
    {
      DocAssemblyGroups retValue = null;

      if (NetCommon.HasItems(list))
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
      if (!NetString.HasValue(fileSpec))
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
      ArgError.MethodName = "LJCSearchUnique(name)";
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var comparer = new DocAssemblyGroupUniqueComparer();
      LJCSortUnique(comparer);
      DocAssemblyGroup searchItem = new DocAssemblyGroup()
      {
        Name = name
      };
      DocAssemblyGroup retValue = null;
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

    // Gets or sets the ArgError object.
    private ArgError ArgError { get; set; }
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

