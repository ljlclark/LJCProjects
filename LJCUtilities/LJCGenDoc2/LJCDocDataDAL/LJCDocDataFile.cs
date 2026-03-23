// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataFile.cs

namespace LJCDocDataDAL
{
  // Represents a DocData Lib file.
  /// <include path="members/LJCDocDataFile/*" file="Doc/LJCDocDataFile.xml"/>
  public class LJCDocDataFile
  {
    #region Constructor Methods

    // Initialize an object instance.
    /// <include path="items/Constructor/*" file="Doc/LJCDocDataFile.xml"/>
    public LJCDocDataFile()
    {
    }

    // Initialize an object instance with the supplied values.
    /// <include path="items/Constructor2/*" file="Doc/LJCDocDataFile.xml"/>
    public LJCDocDataFile(string name, string? summary = null)
    {
      // Initialize Serialize Properties
      Name = name;
      Summary = summary;

      Remarks = null;

      Classes = null;
    }
    #endregion

    #region Properties

    /// <summary>The Class collection.</summary>
    public LJCDocDataClasses? Classes;

    /// <summary>The Name value.</summary>
    public string? Name;

    /// <summary>The Name value.</summary>
    public string? Remarks;

    /// <summary>The Summary value.</summary>
    public string? Summary;
    #endregion
  }
}
