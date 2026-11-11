// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataFile.cs

namespace LJCDocDataDAL
{
  // Represents a DocData Lib file.
  /// <include file='Doc/LJCDocDataFile.xml'
  ///  path='members/LJCDocDataFile/*'/>
  public class LJCDocDataFile
  {
    #region Properties

    // Gets or sets the Classes collection.
    /// <include file='Doc/LJCDocDataFile.xml'
    ///  path='items/Classes/*'/>
    public LJCDocDataClasses Classes { get; set; }

    // Gets or sets the file name.
    /// <include file='Doc/LJCDocDataFile.xml'
    ///  path='items/Name/*'/>
    public string Name { get; set; }

    // Gets or sets the remarks.
    /// <include file='Doc/LJCDocDataFile.xml'
    ///  path='items/Remarks/*'/>
    public string Remarks { get; set; }

    // Gets or sets the summary text.
    /// <include file='Doc/LJCDocDataFile.xml'
    ///  path='items/Summary/*'/>
    public string Summary { get; set; }
    #endregion

    #region Constructor Methods

    // Initialize an object instance.
    /// <include file='Doc/LJCDocDataFile.xml'
    ///  path='items/Constructor/*'/>
    public LJCDocDataFile()
    {
    }

    // Initialize an object instance with the supplied values.
    /// <include file='Doc/LJCDocDataFile.xml'
    ///  path='items/ParamConstructor/*'/>
    public LJCDocDataFile(string name, string summary = null)
    {
      // Initialize Serialize Properties
      Name = name;
      Summary = summary;

      //Classes = new LJCDocDataClasses();
      Classes = null;
      Remarks = null;
    }
    #endregion
  }
}
