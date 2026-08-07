// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCKeyItems.cs
using System.Collections;
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // <summary>Represents a collection of KeyItem objects.</summary>
  /// <include file='Doc/LJCKeyItems.xml'
  ///  path='members/KeyItems/*'/>
  public class LJCKeyItems : IEnumerable<LJCKeyItem>
  {
    #region Static Functions

    // Get custom collection from List<T>.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/GetCollection/*'/>
    public static LJCKeyItems? GetCollection(List<LJCKeyItem> items)
    {
      LJCKeyItems retValue = null;

      if (LJC.HasListItems(items))
      {
        retValue = [];
        foreach (LJCKeyItem item in items)
        {
          retValue.Items.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems1/*'/>
    public static bool HasItems(LJCKeyItems collection)
    {
      bool retValue = false;

      if (collection != null
        && collection.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public LJCKeyItems()
    {
      Items = [];
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCKeyItems(LJCKeyItems items)
    {
      Items = [];
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(item);
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the specified object.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/Add1/*'/>
    public void Add(LJCKeyItem item)
    {
      if (item != null)
      {
        Items.Add(item);
      }
    }

    // Creates and adds the object from the provided values.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/Add2/*'/>
    public LJCKeyItem? Add(string propertyName, long id, string? description = null
      , int maxLength = 10)
    {
      LJCKeyItem retValue = null;

      if (LJC.HasText(propertyName))
      {
        retValue = new LJCKeyItem()
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
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/Append/*'/>
    public void Append(LJCKeyItems items)
    {
      if (HasItems(items))
      {
        foreach (LJCKeyItem item in items)
        {
          Add(item);
        }
      }
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCKeyItems Clone()
    {
      var retValue = new LJCKeyItems();
      foreach (LJCKeyItem keyItem in this)
      {
        var newKeyItem = keyItem.Clone();
        if (newKeyItem != null)
        {
          retValue.Add(newKeyItem);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems2/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Retrieves an item by property name.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCRetrieve/*'/>
    public LJCKeyItem? LJCRetrieve(string propertyName)
    {
      LJCKeyItem retItem = null;

      if (LJC.HasText(propertyName))
      {
        var retItems = SearchPropertyName(propertyName);
        if (retItems != null
          && retItems.Count > 0)
        {
          retItem = retItems[0];
        }
      }
      return retItem;
    }
    #endregion

    #region Other Methods

    // Gets the Item Description with Value as index within PropertyName.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/GetDescription/*'/>
    public string? GetDescription(LJCDataColumn dataColumn)
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
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/GetIndex/*'/>
    public int GetIndex(LJCDataColumn dataColumn)
    {
      int retValue = -1;

      if (dataColumn.Value != null
        && LJCNetString.IsDigits(dataColumn.Value.ToString()))
      {
        _ = int.TryParse(dataColumn.Value.ToString(), out int index);
        index--;
        if (index >= 0
          && index < Items.Count)
        {
          retValue = index;
        }
      }
      return retValue;
    }

    // Gets the KeyItem with Value as index within PropertyName.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/GetItem/*'/>
    public LJCKeyItem? GetItem(LJCDataColumn dataColumn)
    {
      LJCKeyItem retValue = null;

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
            if (index >= 0
              && index < items.Count)
            {
              retValue = items[index];
            }
          }
        }
      }
      return retValue;
    }

    // Gets the Items with the PropertyName.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/GetItems/*'/>
    public LJCKeyItems? GetItems(LJCDataColumn dataColumn)
    {
      LJCKeyItems retValue = null;

      if (dataColumn != null
        && LJC.HasText(dataColumn.PropertyName))
      {
        retValue = SearchPropertyName(dataColumn.PropertyName);
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/SearchName/*'/>
    public LJCKeyItems? SearchPropertyName(string name)
    {
      List<LJCKeyItem> items;
      LJCKeyItems retValue = null;

      SortPropertyName();
      items = Items.FindAll(x => x.PropertyName == name);
      retValue = GetCollection(items);
      return retValue;
    }

    // Sort on Name.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/SortName/*'/>
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
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/GetEnumerator/*'/>
    public IEnumerator<LJCKeyItem> GetEnumerator()
    {
      return ((IEnumerable<LJCKeyItem>)Items).GetEnumerator();
    }

    // Gets the Collection Enumerator.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<LJCKeyItem>)Items).GetEnumerator();
    }
    #endregion

    #region IEnumerable Properties

    // The Collection count.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/Count/*'/>
    public int Count
    {
      get { return Items.Count; }
    }

    // Gets the item by index value.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/Indexer/*'/>
    public LJCKeyItem? this[int index]
    {
      get
      {
        LJCKeyItem retValue = null;

        if (index >= 0
          && index < Count)
        {
          retValue = Items[index];
        }
        return retValue;
      }
    }

    // The KeyItem items.
    /// <include file='Doc/LJCKeyItems.xml'
    ///  path='members/Items/*'/>
    [XmlArray(RootName)]
    public List<LJCKeyItem> Items { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "KeyItems";
    #endregion
  }
}
