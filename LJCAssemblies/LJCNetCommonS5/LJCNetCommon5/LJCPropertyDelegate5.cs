// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCPropertyDelegate.cs

namespace LJCNetCommon5
{
  // Represents a PropertyDelegate definition.
  /// <include path="members/LJCPropertyDelegate/*" file="Doc/LJCPropertyDelegate.xml"/>
  public class LJCPropertyDelegate
  {
    // Gets or sets the PropertyName value.
    /// <include file='Doc/LJCPropertyDelegate.xml'
    ///  path='members/PropertyName/*'/>
    public string? PropertyName { get; set; }

    // Gets or sets the Delegate reference.
    /// <include file='Doc/LJCPropertyDelegate.xml'
    ///  path='members/Value/*'/>
    public Func<object, object>? Value { get; set; }
  }
}
