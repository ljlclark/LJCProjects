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
    /// <include path="members/PropertyName/*" file="Doc/LJCPropertyDelegate.xml"/>
    public string? PropertyName { get; set; }

    // Gets or sets the Delegate reference.
    /// <include path="members/Value/*" file="Doc/LJCPropertyDelegate.xml"/>
    public Func<object, object>? Value { get; set; }
  }
}
