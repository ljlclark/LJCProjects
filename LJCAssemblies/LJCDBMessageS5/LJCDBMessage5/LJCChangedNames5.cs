// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCChangedNames.cs

namespace LJCDBMessage5
{
  // Contains the list of changed property names.
  /// <include file='Doc/ChangedNames.xml'
  ///  path='members/LJCChangedNames/*'/>
  public class LJCChangedNames
  {
    #region Properties

    // Returns the ChangedProperties collection.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/ChangedProperties/*'/>
    public List<string> ChangedProperties
    {
      //get => _ChangedProperties.ToList();
      get => [.. _ChangedProperties];
    }

    // Returns the changed property names string.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/ChangedPropertyNames/*'/>
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
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/Constructor/*'/>
    public LJCChangedNames()
    {
      _ChangedProperties = [];
    }
    #endregion

    #region Collection Methods

    // Adds the property name to the list if the value has changed and it
    // is not already in the list.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/Add/*'/>
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
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/AddNames/*'/>
    public void AddNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        // Add value if not already added.
        _ChangedProperties.Add(propertyName);
      }
    }

    // Clears the changed names.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/Clear/*'/>
    public void Clear()
    {
      _ChangedProperties.Clear();
    }

    // Checks if the changed names contains a propertyName.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/Contains/*'/>
    public bool Contains(string propertyName)
    {
      return _ChangedProperties.Contains(propertyName);
    }

    // Removes a property name from the changed names.
    /// <include file='Doc/ChangedNames.xml'
    ///  path='members/Remove/*'/>
    public void Remove(string propertyName)
    {
      _ChangedProperties.Remove(propertyName);
    }
    #endregion
  }
}
