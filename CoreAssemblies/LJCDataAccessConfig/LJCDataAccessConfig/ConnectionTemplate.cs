// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConnectionTemplate.cs
using System;

namespace LJCDataAccessConfig
{
  // Represents a Connection String template.
  /// <include file='Doc/ConnectionTemplate.xml'
  ///  path='items/ConnectionTemplate/*'/>
  public class ConnectionTemplate : IComparable<ConnectionTemplate>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='items/Constructor/*'/>
    public ConnectionTemplate()
    {
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='items/Clone/*'/>
    public ConnectionTemplate Clone()
    {
      ConnectionTemplate retValue = MemberwiseClone() as ConnectionTemplate;
      return retValue;
    }

    // The object string identifier.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='items/CompareTo/*'/>
    public int CompareTo(ConnectionTemplate other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        //retValue = Name.CompareTo(other.Name);

        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the connection type name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the connection string template.</summary>
    public string Template { get; set; }
    #endregion
  }
}
