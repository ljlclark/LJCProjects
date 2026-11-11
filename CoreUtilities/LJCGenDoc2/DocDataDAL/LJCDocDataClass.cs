// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataClass.cs

namespace LJCDocDataDAL
{
  // Represents a DocData class.
  /// <include file='Doc/LJCDocDataClass.xml'
  ///  path='members/LJCDocDataClass/*'/>
  public class LJCDocDataClass
  {
    #region Properties

    // Gets or sets the example code.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Code/*'/>
    public string Code { get; set; }

    /// Gets or sets the Groups collection.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Groups/*'/>
    public LJCDocDataParams Groups { get; set; }

    // Gets or sets the Methods collection.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Methods/*'/>
    public LJCDocDataMethods Methods { get; set; }

    // Gets or sets the class name.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Name/*'/>
    public string Name { get; set; }

    // Gets or sets the namespace.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Namespace/*'/>
    public string Namespace { get; set; }

    // Gets or sets the Properties collection.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Properties/*'/>
    public LJCDocDataProperties Properties { get; set; }

    // Gets or sets the remarks.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Remarks/*'/>
    public string Remarks { get; set; }

    // Gets or sets the summary text.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Summary/*'/>
    public string Summary { get; set; }

    // Gets or sets the syntax.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Syntax/*'/>
    public string Syntax { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/Constructor/*'/>
    public LJCDocDataClass()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDocDataClass.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCDocDataClass(string name, string summary)
    {
      Name = name;
      Summary = summary;

      Groups = null;
      Methods = null;
      Properties = null;
    }
    #endregion
  }
}
