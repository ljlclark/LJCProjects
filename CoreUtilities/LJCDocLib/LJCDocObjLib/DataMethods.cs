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
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DataMethods(DataMethods items)
    {
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
    // *** Change Method *** 9/26/23 #Overload
    /// <include path='items/GetOverloadName/*' file='Doc/DataMethods.xml'/>
    public string GetOverloadName(string docMethodName)
    {
      string retValue = null;

      var methodName = docMethodName;
      if (methodName.StartsWith("#"))
      {
        retValue = methodName.Substring(1);
        methodName = retValue;
      }

      DataMethod dataMethod;
      if (retValue != null)
      {
        dataMethod = Find(x => x.OverloadName == retValue);
      }
      else
      {
        dataMethod = Find(x => x.Name == methodName);
      }

      // Name already exists.
      int index = 0;
      while (dataMethod != null)
      {
        index++;
        string searchValue = $"{methodName}{index}";
        dataMethod = Find(x => x.OverloadName == searchValue);
        if (null == dataMethod)
        {
          retValue = searchValue;
        }
      }
      return retValue;
    }
    #endregion
  }
}
