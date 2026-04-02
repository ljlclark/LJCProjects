// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataClass5.cs

namespace LJCDocDataDAL5
{
  // Represents a DocData class.
  /// <include path="members/LJCDocDataClass/*" file="Doc/LJCDocDataClass.xml"/>
  public class LJCDocDataClass5
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataClass5()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataClass5(string name, string summary)
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
    public LJCDocDataParams5? Groups;

    // Gets or sets the Method collection.
    /// <include path="members/Methods/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataMethods5? Methods;

    // Gets or sets the Name value.
    /// <include path="members/Name/*" file="Doc/LJCDocDataClass.xml"/>
    public string? Name;

    // Gets or sets the Property collection.
    /// <include path="members/Properties/*" file="Doc/LJCDocDataClass.xml"/>
    public LJCDocDataProperties5? Properties;

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
