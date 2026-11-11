// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataMethod.cs

namespace LJCDocDataDAL
{
  // Represents a DocData method.
  /// <include file='Doc/LJCDocDataMethod.xml'
  ///  path='members/LJCDocDataMethod/*'/>
  public class LJCDocDataMethod
  {
    #region Properties

    // Gets or sets the example code.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Code/*'/>
    public string Code { get; set; }

    // Gets or sets the method name.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Name/*'/>
    public string Name { get; set; }

    // Gets or sets the Params collection.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Params/*'/>
    public LJCDocDataParams Params { get; set; }

    // Gets or sets the parent group name.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/ParentGroup/*'/>
    public string ParentGroup { get; set; }

    // Gets or sets the remarks.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Remarks/*'/>
    public string Remarks { get; set; }

    // Gets or sets the returns text.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Returns/*'/>
    public string Returns { get; set; }

    // Gets or sets the summary text.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Summary/*'/>
    public string Summary { get; set; }

    // Gets or sets the syntax.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Syntax/*'/>
    public string Syntax { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Constructor/*'/>
    public LJCDocDataMethod()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCDocDataMethod(string name, string summary)
    {
      Name = name;
      Summary = summary;
      Params = null;
    }
    #endregion
  }
}
