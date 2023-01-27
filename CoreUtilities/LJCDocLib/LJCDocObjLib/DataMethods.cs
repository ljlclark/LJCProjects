// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMethods.cs
using System.Collections.Generic;

namespace LJCDocObjLib
{
  // Represents a collection of DataMethod objects.
  /// <include path='items/DataMethods/*' file='Doc/DataMethods.xml'/>
  public class DataMethods : List<DataMethod>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMethods()
    {
    }
    #endregion

    #region Methods

    // Returns the unique name for an overriden method.
    /// <include path='items/GetOverriddenName/*' file='Doc/DataMethods.xml'/>
    public string GetOverriddenName(string name)
    {
      string retValue = name;

      if (retValue.StartsWith("#"))
      {
        retValue = name.Substring(1);
      }

      int index = 0;
      DataMethod dataMethod = SearchOverriddenName(retValue);
      while (dataMethod != null)
      {
        index++;
        string searchValue = $"{retValue}{index}";
        dataMethod = SearchOverriddenName(searchValue);
        if (null == dataMethod)
        {
          retValue = searchValue;
        }
      }
      return retValue;
    }

    // Returns the element with the name matching the supplied value.
    /// <include path='items/SearchOverriddenName/*' file='Doc/DataMethods.xml'/>
    public DataMethod SearchOverriddenName(string name)
    {
      DataMethod retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DataMethod dataMethod = new DataMethod()
      {
        OverriddenName = name
      };
      int index = BinarySearch(dataMethod);
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
