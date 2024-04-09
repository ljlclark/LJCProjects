// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CollectionTemplate.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCGenDocDAL
{
  /// <summary>Represents a collection of DocClassGroup objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("DocClassGroups")]
  public class DocClassGroups : List<DocClassGroup>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static DocClassGroups LJCDeserialize(string fileSpec = null)
    {
      DocClassGroups retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DocClassGroups), fileSpec)
        as DocClassGroups;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocClassGroups()
    {
      ArgError = new ArgError("LJCGenDocDAL.DocClassGroups");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocClassGroups(DocClassGroups items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DocClassGroup(item));
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
    /// <param name="docAssemblyID"></param>
    /// <param name="headingName"></param>
    /// <returns></returns>
    public DocClassGroup Add(short id, short docAssemblyID, string headingName)
    {
      DocClassGroup retValue;

      ArgError.MethodName = "Add(short id, string name)";
      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (id <= 0)
      {
        message += "docAssemblyID must be greater than zero.\r\n";
      }
      var errMessage = ArgError.ToString();
      errMessage += message;
      NetString.ThrowArgError(errMessage);

      retValue = LJCSearchUnique(docAssemblyID, headingName);
      if (null == retValue)
      {
        retValue = new DocClassGroup()
        {
          ID = id,
          DocAssemblyID = docAssemblyID,
          HeadingName = headingName
        };
        Add(retValue);
      }
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocClassGroups Clone()
    {
      var retValue = new DocClassGroups();
      foreach (DocClassGroup item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocClassGroups GetCollection(List<DocClassGroup> list)
    {
      DocClassGroups retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DocClassGroups();
        foreach (DocClassGroup item in list)
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
    /// <param name="docAssemblyID"></param>
    /// <param name="headingName"></param>
    /// <returns></returns>
    public DocClassGroup LJCSearchUnique(short docAssemblyID
      , string headingName)
    {
      DocClassGroupUniqueComparer comparer;
      DocClassGroup retValue = null;

      comparer = new DocClassGroupUniqueComparer();
      LJCSortUnique(comparer);
      DocClassGroup searchItem = new DocClassGroup()
      {
        DocAssemblyID = docAssemblyID,
        HeadingName = headingName
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
    public void LJCSortUnique(DocClassGroupUniqueComparer comparer)
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
      get { return "DocClassGroups.xml"; }
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

