// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCKeyItems.cs
using System.Collections;
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // <summary>Represents a collection of KeyItem objects.</summary>
  /// <include path="members/KeyItems/*" file="Doc/LJCKeyItems.xml"/>
  public class LJCKeyItems : IEnumerable<LJCKeyItem>
  {
    #region Static Functions

    // Get custom collection from List<T>.
    /// <include path="members/GetCollection/*" file="Doc/LJCKeyItems.xml"/>
    public static LJCKeyItems? GetCollection(List<LJCKeyItem> items)
    {
      LJCKeyItems retValue = null;

      if (LJC.HasItems(items))
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
    /// <include path="members/HasItems1/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
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
    /// <include path="members/DefaultConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public LJCKeyItems()
    {
      Items = [];
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path="members/CopyConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
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
    /// <include path="members/Add1/*" file="Doc/LJCKeyItems.xml"/>
    public void Add(LJCKeyItem item)
    {
      if (item != null)
      {
        Items.Add(item);
      }
    }

    // Creates and adds the object from the provided values.
    /// <include path="members/Add2/*" file="Doc/LJCKeyItems.xml"/>
    public LJCKeyItem? Add(string propertyName, long id, string? description = null
      , int maxLength = 10)
    {
      LJCKeyItem retValue = null;

      if (LJC.HasValue(propertyName))
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
    /// <include path="members/Append/*" file="Doc/LJCKeyItems.xml"/>
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
    /// <include path="members/Clone/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
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
    /// <include path="members/HasItems2/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
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
    /// <include path="members/LJCRetrieve/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    public LJCKeyItem? LJCRetrieve(string propertyName)
    {
      LJCKeyItem retItem = null;

      if (LJC.HasValue(propertyName))
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
    /// <include path="members/GetDescription/*" file="Doc/LJCKeyItems.xml"/>
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
    /// <include path="members/GetIndex/*" file="Doc/LJCKeyItems.xml"/>
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
    /// <include path="members/GetItem/*" file="Doc/LJCKeyItems.xml"/>
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
    /// <include path="members/GetItems/*" file="Doc/LJCKeyItems.xml"/>
    public LJCKeyItems? GetItems(LJCDataColumn dataColumn)
    {
      LJCKeyItems retValue = null;

      if (dataColumn != null
        && LJC.HasValue(dataColumn.PropertyName))
      {
        retValue = SearchPropertyName(dataColumn.PropertyName);
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path="members/SearchName/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
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
    /// <include path="members/SortName/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
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
    /// <include path="members/GetEnumerator/*" file="Doc/LJCKeyItems.xml"/>
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
    /// <include path="members/Count/*" file="Doc/LJCKeyItems.xml"/>
    public int Count
    {
      get { return Items.Count; }
    }

    // Gets the item by index value.
    /// <include path="members/Indexer/*" file="Doc/LJCKeyItems.xml"/>
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
    /// <include path="members/Items/*" file="Doc/LJCKeyItems.xml"/>
    [XmlArray(RootName)]
    public List<LJCKeyItem> Items { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private const string RootName = "KeyItems";
    #endregion
  }
}
