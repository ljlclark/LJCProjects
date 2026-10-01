// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _CollectionName_
// _CollectionName_.cs
// #SectionEnd Title
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

// #SectionBegin Class
// #Value _ClassName_
// #Value _CollectionName_
// #Value _CompareToName_
// #Value _Namespace_
namespace _Namespace_
{
  /// <summary>Represents a collection of _ClassName_ objects.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  [XmlRoot("_CollectionName_")]
  public class _CollectionName_ : List<_ClassName_>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static _CollectionName_ LJCDeserialize(string fileSpec = null)
    {
      _CollectionName_ retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue = NetCommon.XmlDeserialize(typeof(_CollectionName_), fileSpec)
        as _CollectionName_;
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public _CollectionName_()
    {
      mArgError = new ArgError("_Namespace_._ClassName_");
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public _CollectionName_(_CollectionName_ items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new _ClassName_(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the supplied values.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public _ClassName_ Add(int id, string name)
    {
      _ClassName_ retValue;

      string message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add((object)name, "name");
      NetString.ThrowArgError(message);

      retValue = LJCSearchUnique(name);
      if (null == retValue)
      {
        retValue = new _ClassName_()
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
    public _CollectionName_ Clone()
    {
      var retValue = MemberwiseClone() as _CollectionName_;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/LJCGetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public _CollectionName_ LJCGetCollection(List<_ClassName_> list)
    {
      _CollectionName_ retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new _CollectionName_();
        foreach (_ClassName_ item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/LJCHasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public bool LJCHasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Removes an item by keys.
    /// <include path='items/LJCRemove/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public void LJCRemove(string name)
    {
      _ClassName_ item = Find(x => x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
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

    // Retrieve the collection element.
    /// <include path='items/LJCSearchPrimary/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public _ClassName_ LJCSearchPrimary(long id)
    {
      _ClassName_ retValue = null;

      LJCSortPrimary();
      _ClassName_ searchItem = new _ClassName_()
      {
        ID = id
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include path='items/LJCSearchUnique/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public _ClassName_ LJCSearchUnique(string name)
    {
      _ClassName_ retValue = null;

      var comparer = new _ClassName_Unique();
      LJCSortUnique(comparer);
      _ClassName_ searchItem = new _ClassName_()
      {
        _ComparerName_ = name
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Primary key.</summary>
    public void LJCSortPrimary()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Primary) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.Primary;
      }
    }

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique(_ClassName_UniqueComparer comparer)
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
      get { return "_CollectionName_.xml"; }
    }

    // The item for the specified name.
    /// <include path='items/NameIndexer/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public _ClassName_ this[string name]
    {
      get { return LJCSearchUnique(name); }
    }
    #endregion

    #region Class Data

    private ArgError mArgError;
    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Primary,
      Unique
    }
    #endregion
  }
}
// #SectionEnd Class

