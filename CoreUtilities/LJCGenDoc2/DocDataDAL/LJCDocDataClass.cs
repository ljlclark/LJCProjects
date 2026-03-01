// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataClass.cs

using System.Collections.Generic;

namespace LJCDocDataDAL
{
  // Represents a DocData class.
  /// <include path="members/LJCDocDataClass/*" file="Doc/LJCDocDataClass.xml"/>
  public class LJCDocDataClass
  {
    #region Constructor Methods

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

      //Groups = new LJCDocDataParams();
      //Methods = new LJCDocDataMethods();
      //Properties = new LJCDocDataProperties();
      Groups = null;
      Methods = null;
      Properties = null;
    }
    #endregion

    #region Properties

    /// <summary>The Code value.</summary>
    public string Code;

    /// <summary>The method groups.</summary>
    public LJCDocDataParams Groups;

    /// <summary>The Method array.</summary>
    public LJCDocDataMethods Methods;

    /// <summary>The Name value.</summary>
    public string Name;

    /// <summary>The Property array.</summary>
    public LJCDocDataProperties Properties;

    /// <summary>The Remarks value.</summary>
    public string Remarks;

    /// <summary>The Summary value.</summary>
    public string Summary;

    /// <summary>The class syntax.</summary>
    public string Syntax;
    #endregion
  }
}
