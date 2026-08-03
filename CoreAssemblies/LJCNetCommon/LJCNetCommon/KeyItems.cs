// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// KeyItems.cs
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // <summary>Represents a collection of KeyItem objects.</summary>
  /// <include file='Doc/KeyItems.xml'
  ///  path='items/KeyItems/*'/>
  public class KeyItems : IEnumerable<KeyItem>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems1/*'/>
    public static bool HasItems(KeyItems collection)
    {
      bool retValue = false;

      if (collection != null && collection.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public KeyItems()
    {
      Items = new List<KeyItem>();
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/Add1/*'/>
    public void Add(KeyItem item)
    {
      if (item != null)
      {
        Items.Add(item);
      }
    }

    // Creates and adds the object from the provided values.
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/Add2/*'/>
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/Append/*'/>
    public void Append(KeyItems items)
    {
      if (HasItems(items))
      {
        foreach (KeyItem item in items)
        {
          Add(new KeyItem(item));
        }
      }
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public KeyItems Clone()
    {
      var retValue = new KeyItems();
      foreach (KeyItem keyItem in this)
      {
        retValue.Add(keyItem.Clone());
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/GetCollection/*'/>
    public KeyItems GetCollection(List<KeyItem> items)
    {
      KeyItems retValue = null;

      if (NetCommon.HasListItems(items))
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems2/*'/>
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/GetDescription/*'/>
    public string GetDescription(LJCDataColumn dataColumn)
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/GetIndex/*'/>
    public int GetIndex(LJCDataColumn dataColumn)
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/GetItem/*'/>
    public KeyItem GetItem(LJCDataColumn dataColumn)
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/GetItems/*'/>
    public KeyItems GetItems(LJCDataColumn dataColumn)
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/SearchName/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/SortName/*'/>
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/GetEnumerator/*'/>
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
    /// <include file='Doc/KeyItems.xml'
    ///  path='items/Indexer/*'/>
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
