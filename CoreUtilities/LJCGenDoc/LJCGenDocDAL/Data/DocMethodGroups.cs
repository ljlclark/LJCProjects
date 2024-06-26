﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CollectionTemplate.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LJCGenDocDAL
{
  /// <summary>Represents a collection of DocMethodGroup objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DocMethodGroups")]
  public class DocMethodGroups : List<DocMethodGroup>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DocMethodGroups LJCDeserialize(string fileSpec = null)
    {
      DocMethodGroups retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DocMethodGroups), fileSpec)
        as DocMethodGroups;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocMethodGroups()
    {
      ArgError = new ArgError("LJCGenDocDAL.DocMethodGroups");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocMethodGroups(DocMethodGroups items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DocMethodGroup(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="docClassID"></param>
    /// <returns></returns>
    public DocMethodGroup Add(short id, short docClassID)
    {
      ArgError.MethodName = "Add(id, docClassID)";
      if (id <= 0)
      {
        ArgError.Add("id must be greater than zero.");
      }
      if (docClassID <= 0)
      {
        ArgError.Add("docClassID must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = LJCSearchUnique(id, docClassID);
      if (null == retValue)
      {
        retValue = new DocMethodGroup()
        {
          ID = id,
          DocClassID = docClassID
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocMethodGroups Clone()
    {
      var retValue = new DocMethodGroups();
      foreach (DocMethodGroup item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocMethodGroups GetCollection(List<DocMethodGroup> list)
    {
      DocMethodGroups retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DocMethodGroups();
        foreach (DocMethodGroup item in list)
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

    // Retrieve the collection element with name.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="docClassID"></param>
    /// <returns></returns>
    public DocMethodGroup LJCSearchUnique(short id, short docClassID)
    {
      ArgError.MethodName = "LJCSearchUnique(id, docClassID)";
      if (id <= 0)
      {
        ArgError.Add("id must be greater than zero.");
      }
      if (docClassID <= 0)
      {
        ArgError.Add("docClassID must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var comparer = new DocMethodGroupUniqueComparer();
      LJCSortUnique(comparer);
      DocMethodGroup searchItem = new DocMethodGroup()
      {
        ID = id,
        DocClassID = docClassID
      };
      DocMethodGroup retValue = null;
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Name.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DocMethodGroupUniqueComparer comparer)
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
      get { return "DocMethodGroups.xml"; }
    }

    // Gets or sets the ArgError object.
    private ArgError ArgError { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Unique
    }
    #endregion
  }
}

