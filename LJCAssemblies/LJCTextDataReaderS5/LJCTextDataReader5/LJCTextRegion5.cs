// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextRegion5.cs

namespace LJCTextDataReader5
{
  // Represents the text region.
  /// <include file='Doc/ProjectTextRegion.xml'
  ///  path='items/LJCTextRegion/*'/>
  public class LJCTextRegion
  {
    #region Properties

    // The region beginning index.
    /// <include file='Doc/TextRegion.xml'
    ///  path='items/BeginIndex/*'/>
    public int BeginIndex { get; set; }

    // The region ending index.
    /// <include file='Doc/TextRegion.xml'
    ///  path='items/EndIndex/*'/>
    public int EndIndex { get; set; }
    #endregion
  }
}
