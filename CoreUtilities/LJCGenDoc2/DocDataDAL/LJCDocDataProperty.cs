// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataProperty.cs

namespace LJCDocDataDAL
{
  // Represents a DocData property.
  /// <include file='Doc/LJCDocDataProperty.xml'
  ///  path='members/LJCDocDataProperty/*'/>
  public class LJCDocDataProperty
  {
    #region Properties

    // Gets or sets the property name.
    /// <include file='Doc/LJCDocDataProperty.xml'
    ///  path='members/Name/*'/>
    public string Name { get; set; }

    // Gets or sets the remarks.
    /// <include file='Doc/LJCDocDataProperty.xml'
    ///  path='members/Remarks/*'/>
    public string Remarks { get; set; }

    // Gets or sets the returns text.
    /// <include file='Doc/LJCDocDataProperty.xml'
    ///  path='members/Returns/*'/>
    public string Returns { get; set; }

    // Gets or sets the summary text.
    /// <include file='Doc/LJCDocDataProperty.xml'
    ///  path='members/Summary/*'/>
    public string Summary { get; set; }

    // Gets or sets the syntax.
    /// <include file='Doc/LJCDocDataProperty.xml'
    ///  path='members/Syntax/*'/>
    public string Syntax { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/Constructor/*'/>
    public LJCDocDataProperty()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDocDataMethod.xml'
    ///  path='members/ConstructorParams/*'/>
    public LJCDocDataProperty(string name, string summary)
    {
      Name = name;
      Summary = summary;
    }
    #endregion
  }
}
