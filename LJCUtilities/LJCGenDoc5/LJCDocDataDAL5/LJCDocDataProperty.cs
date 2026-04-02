// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataProperty.cs

namespace LJCDocDataDAL5
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

      Remarks = null;
      Syntax = null;
    }
    #endregion

    #region Properties

    // Gets or sets the Name value.
    /// <include path="members/Name/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Name;

    // Gets or sets the Remarks value.
    /// <include path="members/Remarks/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Remarks;

    // Gets or sets the Returns value.
    /// <include path="members/Returns/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Returns;

    // Gets or sets the Summary value.
    /// <include path="members/Summary/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Summary;

    // Gets or sets the Syntax value.
    /// <include path="members/Syntax/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Syntax;
    #endregion
  }
}
