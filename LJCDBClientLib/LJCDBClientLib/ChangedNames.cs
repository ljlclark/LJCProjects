// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ChangedNames.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBClientLib
{
  // Contains the list of changed property names.
  /// <include path='items/ChangedNames/*' file='Doc/ProjectDBClientLib.xml'/>
  public class ChangedNames : List<string>
  {
    // Adds the property name to the list if the value has changed and it
    // is not already in the list.
    /// <include path='items/Add/*' file='Doc/ChangedNames.xml'/>
    public T Add<T>(string propertyName, T oldValue, T newValue)
    {
      T retValue = newValue;

      if (false == NetCommon.IsEqual(oldValue, newValue))
      {
        // Add value if not already added.
        if (null == FindName(propertyName))
        {
          Add(propertyName);
        }
      }
      return retValue;
    }

    // Returns the existing property name or null if it does not exist.
    /// <include path='items/FindName/*' file='Doc/ChangedNames.xml'/>
    public string FindName(string propertyName)
    {
      return Find(x => 0 == string.Compare(x, propertyName, true));
    }
  }
}
