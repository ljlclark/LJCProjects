// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataMethods.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDocDataDAL
{
  // Represents a collection of DataMethod objects.
  /// <include file='Doc/LJCDocDataMethods.xml'
  ///  path='members/LJCDocDataMethods/*'/>
  public class LJCDocDataMethods : List<LJCDocDataMethod>
  {
    // Get custom collection from List<T>.
    /// <include path='items/LJCGetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static LJCDocDataMethods LJCGetCollection(List<LJCDocDataMethod> list)
    {
      LJCDocDataMethods retValue = null;

      if (NetCommon.HasListItems(list))
      {
        retValue = new LJCDocDataMethods();
        foreach (LJCDocDataMethod item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
  }
}
