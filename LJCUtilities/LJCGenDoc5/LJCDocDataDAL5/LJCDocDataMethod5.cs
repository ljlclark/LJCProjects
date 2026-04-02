// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataMethod5.cs

namespace LJCDocDataDAL5
{
  // Represents a DocData method.
  /// <include path="members/LJCDocDataMethod/*" file="Doc/LJCDocDataMethod.xml"/>
  public class LJCDocDataMethod5
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataMethod5()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataMethod5(string name, string summary)
    {
      Name = name;
      Summary = summary;

      Code = null;
      ParentGroup = null;
      Remarks = null;
      Returns = null;
      Syntax = null;

      Params = null;
    }
    #endregion

    #region Properties

    // Gets or sets the code value.
    /// <include path="members/Code/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Code { get; set; }

    // Gets or sets the name value.
    /// <include path="members/Name/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Name { get; set; }

    // The Param array.
    /// <include path="members/Params/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataParams5? Params { get; set; }

    // Gets or sets the ParentGroup value.
    /// <include path="members/ParentGroup/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? ParentGroup { get; set; }

    // Gets or sets the Remarks value.
    /// <include path="members/Remarks/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Remarks { get; set; }

    // Gets or sets the Returns value.
    /// <include path="members/Returns/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Returns { get; set; }

    // Gets or sets the Summary value.
    /// <include path="members/Summary/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Summary { get; set; }

    // Gets or sets the Syntax value.
    /// <include path="members/Syntax/*" file="Doc/LJCDocDataMethod.xml"/>
    public string? Syntax { get; set; }
    #endregion
  }
}
