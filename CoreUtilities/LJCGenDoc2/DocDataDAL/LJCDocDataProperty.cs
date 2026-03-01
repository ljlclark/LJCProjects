// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataProperty.cs

namespace LJCDocDataDAL
{
  // Represents a DocData property.
  /// <include path="members/LJCDocDataProperty/*" file="Doc/LJCDocDataProperty.xml"/>
  public class LJCDocDataProperty
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataProperty()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataProperty(string name, string summary)
    {
      Name = name;
      Summary = summary;
    }
    #endregion

    #region Properties

    /// <summary>The Name value.</summary>
    public string Name;

    /// <summary>The Remarks value.</summary>
    public string Remarks;

    /// <summary>The Returns value.</summary>
    public string Returns;

    /// <summary>The Summary value.</summary>
    public string Summary;

    /// <summary>The Syntax value.</summary>
    public string Syntax;
    #endregion
  }
}
