// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCChangedNames.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Contains the list of changed property names.
  /// <include path='items/ChangedNames/*' file='Doc/ProjectDBClientLib.xml'/>
  public class LJCChangedNames : List<string>
  {
    // Adds the property name to the list if the value has changed and it
    // is not already in the list.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='items/Add/*'/>
    public T Add<T>(string propertyName, T oldValue, T newValue)
    {
      T retValue = newValue;

      if (!LJC.IsEqual(oldValue, newValue))
      {
        // Add value if not already added.
        if (null == FindName(propertyName))
        {
          Add(propertyName);
        }
      }
      return retValue;
    }

    // Adds a list of names.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='items/AddNames/*'/>
    public void AddNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        // Add value if not already added.
        if (null == FindName(propertyName))
        {
          Add(propertyName);
        }
      }
    }

    // Returns the existing property name or null if it does not exist.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='items/FindName/*'/>
    public string? FindName(string propertyName)
    {
      return Find(x => 0 == string.Compare(x, propertyName, true));
    }
  }
}
