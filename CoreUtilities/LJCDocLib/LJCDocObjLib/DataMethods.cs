// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMethods.cs
using LJCNetCommon;
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
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMethods(DataMethods items)
    {
      mPrevCount = -1;
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataMethod(item));
        }
      }
    }
    #endregion

    #region Methods

    // Returns the unique name for an overload method.
    /// <include path='items/GetOverloadName/*' file='Doc/DataMethods.xml'/>
    public string GetOverloadName(string docMethodName)
    {
      string retValue = docMethodName;

      if (retValue.StartsWith("#"))
      {
        retValue = docMethodName.Substring(1);
      }

      int index = 0;
      DataMethod dataMethod = SearchOverloadName(retValue);
      while (dataMethod != null)
      {
        index++;
        string searchValue = $"{retValue}{index}";
        dataMethod = SearchOverloadName(searchValue);
        if (null == dataMethod)
        {
          retValue = searchValue;
        }
      }
      return retValue;
    }

    // Returns the element with the name matching the supplied value.
    /// <include path='items/SearchOverloadName/*' file='Doc/DataMethods.xml'/>
    public DataMethod SearchOverloadName(string overloadName)
    {
      DataMethod retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DataMethod dataMethod = new DataMethod()
      {
        OverloadName = overloadName
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
