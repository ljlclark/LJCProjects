// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCChangedNames.cs

namespace LJCDBMessage5
{
  // Contains the list of changed property names.
  /// <include file='Doc/LJCChangedNames.xml'
  ///  path='items/ChangedNames/*'/>
  public class LJCChangedNames
  {
    #region Properties

    // Returns the ChangedProperties collection.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/ChangedProperties/*'/>
    public List<string> ChangedProperties
    {
      //get => _ChangedProperties.ToList();
      get => [.. _ChangedProperties];
    }

    // Returns the changed property names string.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/ChangedPropertyNames/*'/>
    public string ChangedPropertyNames
    {
      get => string.Join(", ", ChangedProperties);
    }
    #endregion

    #region Class Data

    // Prevents duplicates automatically.
    // Faster than List<T>.
    private readonly HashSet<string> _ChangedProperties;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/Constructor/*'/>
    public LJCChangedNames()
    {
      _ChangedProperties = [];
    }
    #endregion

    #region Collection Methods

    // Adds the property name to the list if the value has changed and it
    // is not already in the list.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/Add/*'/>
    public T Add<T>(string propertyName, T originalValue, T newValue)
    {
      T retValue = newValue;

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

    // Adds a list of names.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/AddNames/*'/>
    public void AddNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        // Add value if not already added.
        _ChangedProperties.Add(propertyName);
      }
    }

    // Clears the changed names.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/AddNames/*'/>
    public void Clear()
    {
      _ChangedProperties.Clear();
    }

    // Checks if the changed names contains a propertyName.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/Contains/*'/>
    public bool Contains(string propertyName)
    {
      return _ChangedProperties.Contains(propertyName);
    }

    // Removes a property name from the changed names.
    /// <include file='Doc/LJCChangedNames.xml'
    ///  path='items/Remove/*'/>
    public void Remove(string propertyName)
    {
      _ChangedProperties.Remove(propertyName);
    }
    #endregion
  }
}
