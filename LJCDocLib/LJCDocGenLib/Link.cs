// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Link.cs

namespace LJCDocGenLib
{
  /// <summary>Represents an HTML Link.</summary>
  public class Link
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Link()
    {
    }
    #endregion

    #region Data Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return Name;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Text value.</summary>
    public string Text { get; set; }
    #endregion
  }
}
