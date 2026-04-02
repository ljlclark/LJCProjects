// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataFile5.cs

namespace LJCDocDataDAL5
{
  // Represents a DocData Lib file.
  /// <include path="members/LJCDocDataFile/*" file="Doc/LJCDocDataFile.xml"/>
  public class LJCDocDataFile5
  {
    #region Constructor Methods

    // Initialize an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataFile.xml"/>
    public LJCDocDataFile5()
    {
    }

    // Initialize an object instance with the supplied values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataFile.xml"/>
    public LJCDocDataFile5(string name, string? summary = null)
    {
      // Initialize Serialize Properties
      Name = name;
      Summary = summary;

      Remarks = null;

      Classes = null;
    }
    #endregion

    #region Properties

    // Gets or sets the Class collection.
    /// <include path="members/Classes/*" file="Doc/LJCDocDataFile.xml"/>
    public LJCDocDataClasses5? Classes;

    // Gets or sets the Name value.
    /// <include path="members/Name/*" file="Doc/LJCDocDataFile.xml"/>
    public string? Name;

    // Gets or sets the Remarks value.
    /// <include path="members/Remarks/*" file="Doc/LJCDocDataFile.xml"/>
    public string? Remarks;

    // Gets or sets the Summary value.
    /// <include path="members/Summary/*" file="Doc/LJCDocDataFile.xml"/>
    public string? Summary;
    #endregion
  }
}
