// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCConnectionTemplate.cs

using LJCNetCommon5;

namespace LJCDBConfig5
{
  // Represents a Connection String template.
  /// <include file="Doc/ProjectConnectionTemplate.xml"
  ///  path="members/LJCConnectionTemplate/*"/>
  public class LJCConnectionTemplate : IComparable<LJCConnectionTemplate>
  {
    #region Properties

    // Gets or sets the connection type name.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='members/Name/*'/>
    public string? Name { get; set; }

    // Gets or sets the connection string template.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='members/Template/*'/>
    public string? Template { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='members/Constructor/*'/>
    public LJCConnectionTemplate()
    {
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='members/Clone/*'/>
    public LJCConnectionTemplate? Clone()
    {
      LJCConnectionTemplate? retValue = MemberwiseClone()
        as LJCConnectionTemplate;
      return retValue;
    }

    // The object string value.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='members/ToString/*'/>
    public override string? ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include file='Doc/ConnectionTemplate.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(LJCConnectionTemplate? other)
    {
      int retValue;

      if (null == other)
      {
        retValue = LJCNetString.CompareGreater;
      }
      else
      {
        retValue = LJC.CompareNull(Name, other.Name);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          //retValue = Name.CompareTo(other.Name);

          // Not case sensitive.
          retValue = string.Compare(Name, other.Name, true);
        }
      }
      return retValue;
    }
    #endregion
  }
}
