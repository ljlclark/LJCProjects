// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItems.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  // Represents a collection of RepeatItem objects.
  /// <include file='Doc/RepeatItems5.xml'
  ///  path='items/RepeatItems/*'/>
  public class RepeatItems : List<RepeatItem>
  {
    #region Class Data

    private int _PrevCount;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public RepeatItems()
    {
    }
    #endregion

    #region Methods

    // Creates the RepeateItem object and adds it to the end of the collection.
    /// <include file='Doc/RepeatItems5.xml'
    ///  path='items/Add/*'/>
    public RepeatItem? Add(string name)
    {
      RepeatItem? retValue = null;

      if (LJC.HasText(name))
      {
        retValue = Retrieve(name);
        if (null == retValue)
        {
          retValue = new RepeatItem()
          {
            Name = name
          };
          Add(retValue);
        }
      }
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCSearchName/*'/>
    public RepeatItem? Retrieve(string name)
    {
      RepeatItem repeatItem;
      int index;
      RepeatItem? retValue = null;

      if (Count != _PrevCount)
      {
        _PrevCount = Count;
        Sort();
      }

      repeatItem = new RepeatItem()
      {
        Name = name
      };
      index = BinarySearch(repeatItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion
  }
}
