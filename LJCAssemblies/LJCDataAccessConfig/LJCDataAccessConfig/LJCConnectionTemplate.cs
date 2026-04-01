// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCConnectionTemplate.cs

namespace LJCDataAccessConfig
{
  // Represents a Connection String template.
  /// <include path="members/LJCConnectionTemplate/*" file="Doc/LJCConnectionTemplate.xml"/>
  public class LJCConnectionTemplate : IComparable<LJCConnectionTemplate>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCConnectionTemplate.xml"/>
    public LJCConnectionTemplate()
    {
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    /// <include path="members/Clone/*" file="Doc/LJCConnectionTemplate.xml"/>
    public LJCConnectionTemplate? Clone()
    {
      LJCConnectionTemplate? retValue = MemberwiseClone()
        as LJCConnectionTemplate;
      return retValue;
    }

    // The object string value.
    /// <include path="members/ToString/*" file="Doc/LJCConnectionTemplate.xml"/>
    public override string? ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include path="members/CompareTo/*" file="Doc/LJCConnectionTemplate.xml"/>
    public int CompareTo(LJCConnectionTemplate? other)
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

    // Gets or sets the connection type name.
    /// <include path="members/Name/*" file="Doc/LJCConnectionTemplate.xml"/>
    public string? Name { get; set; }

    // Gets or sets the connection string template.
    /// <include path="members/Template/*" file="Doc/LJCConnectionTemplate.xml"/>
    public string? Template { get; set; }
    #endregion
  }
}
