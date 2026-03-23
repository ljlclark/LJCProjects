// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataClass.cs

namespace LJCDocDataDAL
{
  // Represents a DocData class.
  /// <include path="members/LJCDocDataClass/*" file="Doc/LJCDocDataClass.xml"/>
  public class LJCDocDataClass
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataClass()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataClass(string name, string summary)
    {
      Name = name;
      Summary = summary;

      Code = null;
      Remarks = null;
      Syntax = null;

      Groups = null;
      Methods = null;
      Properties = null;
    }
    #endregion

    #region Properties

    // Gets or sets the Code value.
    /// <include path="members/Code/*" file="Doc/LJCDocDataClass.xml"/>
    public string? Code;

    // Gets or sets the Method Group collection.
    /// <include path="members/Groups/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataParams? Groups;

    // Gets or sets the Method collection.
    /// <include path="members/Methods/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataMethods? Methods;

    // Gets or sets the Name value.
    /// <include path="members/Name/*" file="Doc/LJCDocDataClass.xml"/>
    public string? Name;

    // Gets or sets the Property collection.
    /// <include path="members/Properties/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataProperties? Properties;

    // Gets or sets the Remarks value.
    /// <include path="members/Remarks/*" file="Doc/LJCDocDataClass.xml"/>
    public string? Remarks;

    // Gets or sets the Summary value.
    /// <include path="members/Summary/*" file="Doc/LJCDocDataClass.xml"/>
    public string? Summary;

    // Gets or sets the class Syntax.
    /// <include path="members/Syntax/*" file="Doc/LJCDocDataClass.xml"/>
    public string? Syntax;
    #endregion
  }
}
