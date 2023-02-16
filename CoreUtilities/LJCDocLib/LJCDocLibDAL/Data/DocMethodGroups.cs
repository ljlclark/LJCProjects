// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CollectionTemplate.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCDocLibDAL
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
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DocMethodGroups LJCDeserialize(string fileSpec = null)
    {
      DocMethodGroups retValue;

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
        retValue = NetCommon.XmlDeserialize(typeof(DocMethodGroups), fileSpec)
        as DocMethodGroups;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethodGroups()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
      DocMethodGroup retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      if (docClassID <= 0)
      {
        message += "docClassID must be greater than zero.\r\n";
      }
      NetString.ThrowInvalidArgument(message);

      retValue = LJCSearchUnique(id, docClassID);
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
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethodGroups Clone()
    {
      var retValue = MemberwiseClone() as DocMethodGroups;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DocMethodGroups GetCollection(List<DocMethodGroup> list)
    {
      DocMethodGroups retValue = null;

      if (list != null && list.Count > 0)
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

    // Retrieve the collection element with name.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="docClassID"></param>
    /// <returns></returns>
    public DocMethodGroup LJCSearchUnique(short id, short docClassID)
    {
      DocMethodGroupUniqueComparer comparer;
      DocMethodGroup retValue = null;

      comparer = new DocMethodGroupUniqueComparer();
      LJCSortUnique(comparer);
      DocMethodGroup searchItem = new DocMethodGroup()
      {
        ID = id,
        DocClassID = docClassID
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

