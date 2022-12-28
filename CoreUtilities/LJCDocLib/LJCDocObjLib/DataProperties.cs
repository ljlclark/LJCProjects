// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataProperties.cs
using System.Collections.Generic;

namespace LJCDocObjLib
{
  // Represents a collection of DataProperty objects.
  /// <include path='items/DataProperties/*' file='Doc/DataProperties.xml'/>
  public class DataProperties : List<DataProperty>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataProperties()
    {
    }
    #endregion

    #region Methods

    // Returns the unique name for an overriden property.
    /// <include path='items/GetOverriddenName/*' file='Doc/DataProperties.xml'/>
    public string GetOverriddenName(string name)
    {
      string retValue = name;

      int index = 0;
      DataProperty dataProperty = SearchOverriddenName(retValue);
      while (dataProperty != null)
      {
        index++;
        string searchValue = $"{name}{index}";
        dataProperty = SearchOverriddenName(searchValue);
        if (null == dataProperty)
        {
          retValue = searchValue;
        }
      }
      return retValue;
    }

    // Returns the element with the name matching the supplied value.
    /// <include path='items/SearchOverriddenName/*' file='Doc/DataProperties.xml'/>
    public DataProperty SearchOverriddenName(string name)
    {
      DataProperty retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DataProperty dataProperty = new DataProperty()
      {
        OverriddenName = name
      };
      int index = BinarySearch(dataProperty);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Class Data

    // The previous count value.
    private int mPrevCount;
    #endregion
  }
}
