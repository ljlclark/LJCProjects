// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataMethod.cs

namespace LJCDocDataDAL
{
  // Represents a DocData method.
  /// <include path="members/LJCDocDataMethod/*" file="Doc/LJCDocDataMethod.xml"/>
  public class LJCDocDataMethod
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataMethod()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataMethod.xml"/>
    public LJCDocDataMethod(string name, string summary)
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

    /// <summary>The Code value.</summary>
    public string? Code;

    /// <summary>The Name value.</summary>
    public string? Name;

    /// <summary>The Param array.</summary>
    public LJCDocDataParams? Params;

    // The method group name.
    public string? ParentGroup;

    /// <summary>The Remarks value.</summary>
    public string? Remarks;

    /// <summary>The Returns value.</summary>
    public string? Returns;

    /// <summary>The Summary value.</summary>
    public string? Summary;

    /// <summary>The Syntax value.</summary>
    public string? Syntax;
    #endregion
  }
}
