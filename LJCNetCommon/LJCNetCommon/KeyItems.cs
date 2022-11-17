// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // <summary>Represents a collection of KeyItem objects.</summary>
  /// <include path='items/KeyItems/*' file='Doc/KeyItems.xml'/>
  public class KeyItems : IEnumerable<KeyItem>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(KeyItems collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public KeyItems()
    {
      Items = new List<KeyItem>();
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public KeyItems(KeyItems items)
    {
      Items = new List<KeyItem>();
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new KeyItem(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the specified object.
    /// <include path='items/Add1/*' file='Doc/KeyItems.xml'/>
    public void Add(KeyItem item)
    {
      if (item != null)
      {
        Items.Add(item);
      }
    }

    // Creates and adds the object from the provided values.
    /// <include path='items/Add2/*' file='Doc/KeyItems.xml'/>
    public KeyItem Add(string propertyName, long id, string description = null
      , int maxLength = 10)
    {
      KeyItem retValue = null;

      if (NetString.HasValue(propertyName))
      {
        retValue = new KeyItem()
        {
          Description = description,
          ID = id,
          MaxLength = maxLength,
          PropertyName = propertyName
        };
        Add(retValue);
      }
      return retValue;
    }

    // Appends the supplied objects to the collection.
    /// <include path='items/Append/*' file='Doc/KeyItems.xml'/>
    public void Append(KeyItems items)
    {
      if (items != null && items.Count > 0)
      {
        foreach (KeyItem item in items)
        {
          Add(new KeyItem(item));
        }
      }
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public KeyItems Clone()
    {
      KeyItems retValue = MemberwiseClone() as KeyItems;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='Doc/KeyItems.xml'/>
    public KeyItems GetCollection(List<KeyItem> items)
    {
      KeyItems retValue = null;

      if (items != null && items.Count > 0)
      {
        retValue = new KeyItems();
        foreach (KeyItem item in items)
        {
          retValue.Items.Add(new KeyItem(item));
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
    #endregion

    #region Other Methods

    // Gets the Item Description with Value as index within PropertyName.
    /// <include path='items/GetDescription/*' file='Doc/KeyItems.xml'/>
    public string GetDescription(DbColumn dataColumn)
    {
      string retValue = null;

      var item = GetItem(dataColumn);
      if (item != null)
      {
        retValue = item.Description;
      }
      return retValue;
    }

    // Get index from Value.
    /// <include path='items/GetIndex/*' file='Doc/KeyItems.xml'/>
    public int GetIndex(DbColumn dataColumn)
    {
      int retValue = -1;

      if (dataColumn.Value != null
        && NetString.IsDigits(dataColumn.Value.ToString()))
      {
        int.TryParse(dataColumn.Value.ToString(), out int index);
        index--;
        if (index >= 0 && index < Items.Count)
        {
          retValue = index;
        }
      }
      return retValue;
    }

    // Gets the KeyItem with Value as index within PropertyName.
    /// <include path='items/GetItem/*' file='Doc/KeyItems.xml'/>
    public KeyItem GetItem(DbColumn dataColumn)
    {
      KeyItem retValue = null;

      int index = GetIndex(dataColumn);
      if (index >= 0)
      {
        var items = GetItems(dataColumn);
        if (items != null)
        {
          if (1 == items.Count)
          {
            retValue = items[0];
          }
          else
          {
            if (index >= 0 && index < items.Count)
            {
              retValue = items[index];
            }
          }
        }
      }
      return retValue;
    }

    // Gets the Items with the PropertyName.
    /// <include path='items/GetItems/*' file='Doc/KeyItems.xml'/>
    public KeyItems GetItems(DbColumn dataColumn)
    {
      KeyItems retValue = null;

      if (dataColumn != null && NetString.HasValue(dataColumn.PropertyName))
      {
        retValue = SearchPropertyName(dataColumn.PropertyName);
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/SearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public KeyItems SearchPropertyName(string name)
    {
      List<KeyItem> items;
      KeyItems retValue = null;

      SortPropertyName();
      items = Items.FindAll(x => x.PropertyName == name);
      retValue = GetCollection(items);
      return retValue;
    }

    // Sort on Name.
    /// <include path='items/SortName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public void SortPropertyName()
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
    /// <include path='items/GetEnumerator/*' file='Doc/KeyItems.xml'/>
    public IEnumerator<KeyItem> GetEnumerator()
    {
      return ((IEnumerable<KeyItem>)Items).GetEnumerator();
    }

    // Gets the Collection Enumerator.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<KeyItem>)Items).GetEnumerator();
    }
    #endregion

    #region IEnumerable Properties

    /// <summary>The Collection count.</summary>
    public int Count
    {
      get { return Items.Count; }
    }

    // Gets the item by index value.
    /// <include path='items/Indexer/*' file='Doc/KeyItems.xml'/>
    public KeyItem this[int index]
    {
      get
      {
        KeyItem retValue = null;

        if (index >= 0 && index < Count)
        {
          retValue = Items[index];
        }
        return retValue;
      }
    }

    /// <summary>The KeyItem items.</summary>
    [XmlArray(RootName)]
    public List<KeyItem> Items { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "KeyItems";
    #endregion
  }
}
