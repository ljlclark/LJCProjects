// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CollectionTemplate.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCGenDocDAL
{
  /// <summary>Represents a collection of DocAssembly objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DocAssemblies")]
  public class DocAssemblies : List<DocAssembly>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DocAssemblies LJCDeserialize(string fileSpec = null)
    {
      DocAssemblies retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DocAssemblies), fileSpec)
        as DocAssemblies;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssemblies()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocAssemblies(DocAssemblies items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DocAssembly(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocAssembly Add(short id, string name)
    {
      DocAssembly retValue;

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
        retValue = new DocAssembly()
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
    public DocAssemblies Clone()
    {
      var retValue = new DocAssemblies();
      foreach (DocAssembly docAssembly in this)
      {
        retValue.Add(docAssembly.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocAssemblies GetCollection(List<DocAssembly> list)
    {
      DocAssemblies retValue = null;

      if (list != null && list.Count > 0)
      {
        retValue = new DocAssemblies();
        foreach (DocAssembly item in list)
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

    #region Public Methods
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public DocAssembly LJCSearchUnique(string name)
    {
      DocAssemblyUniqueComparer comparer;
      DocAssembly retValue = null;

      comparer = new DocAssemblyUniqueComparer();
      LJCSortUnique(comparer);
      DocAssembly searchItem = new DocAssembly()
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

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(DocAssemblyUniqueComparer comparer)
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
      get { return "DocAssemblies.xml"; }
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

