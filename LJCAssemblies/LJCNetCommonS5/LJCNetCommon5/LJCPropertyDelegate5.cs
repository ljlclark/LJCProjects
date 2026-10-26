// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCPropertyDelegate.cs

namespace LJCNetCommon5
{
  // Represents a PropertyDelegate definition.
  /// <include file='Doc/PropertyDelegate.xml'
  ///  path='members/LJCPropertyDelegate/*'/>
  public class LJCPropertyDelegate
  {
    // Gets or sets the PropertyName value.
    /// <include file='Doc/PropertyDelegate.xml'
    ///  path='members/PropertyName/*'/>
    public string? PropertyName { get; set; }

    // Gets or sets the Delegate reference.
    /// <include file='Doc/PropertyDelegate.xml'
    ///  path='members/Value/*'/>
    public Func<object, object>? Value { get; set; }
  }
}
