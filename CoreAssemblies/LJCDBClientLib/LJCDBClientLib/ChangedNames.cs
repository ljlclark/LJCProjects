// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ChangedNames.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Linq;

namespace LJCDBClientLib
{
  // Contains the list of changed property names.
  /// <include path='items/ChangedNames/*' file='Doc/ProjectDBClientLib.xml'/>
  public class ChangedNames
  {
    /// <summary>Initializes an object instance.</summary>
    public ChangedNames()
    {
      _ChangedProperties = new HashSet<string>();
    }

    #region Methods

    /// <summary>Adds the property name.</summary>
    public void Add(string propertyName)
    {
      _ChangedProperties.Add(propertyName);
    }

    // Adds the property name if the value has changed and not already in list.
    /// <include path='items/Add/*' file='Doc/ChangedNames.xml'/>
    public T Add<T>(string propertyName, T originalValue, T newValue)
    {
      T retValue = newValue;

      //if (!NetCommon.IsEqual(oldValue, newValue))
      if (EqualityComparer<T>.Default.Equals(originalValue, retValue))
      {
        _ChangedProperties.Remove(propertyName);
      }
      else
      {
        // Add value if not already added.
        _ChangedProperties.Add(propertyName);
      }
      return retValue;
    }

    /// <summary>Adds property names.</summary>
    public void AddNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        _ChangedProperties.Add(propertyName);
      }
    }

    /// <summary>Clears the changed names.</summary>
    public void Clear()
    {
      _ChangedProperties.Clear();
    }

    /// <summary>Checks if the changed names contains a propertyName.</summary>
    public bool Contains(string propertyName)
    {
      return _ChangedProperties.Contains(propertyName);
    }

    /// <summary>Removes a property name from the changed names.</summary>
    public void Remove(string propertyName)
    {
      _ChangedProperties.Remove(propertyName);
    }

    //// Returns the existing property name or null if it does not exist.
    ///// <include path='items/FindName/*' file='Doc/ChangedNames.xml'/>
    //public string FindName(string propertyName)
    //{
    //  return Find(x => 0 == string.Compare(x, propertyName, true));
    //}
    #endregion

    #region Properties

    /// <summary>Returns the ChangedProperties collection.</summary>
    public List<string> ChangedProperties
    {
      get => _ChangedProperties.ToList<string>();
    }
    #endregion

    #region Class Data

    // Prevents duplicates automatically.
    // Faster than List<T>.
    private readonly HashSet<string> _ChangedProperties;
    #endregion
  }
}
