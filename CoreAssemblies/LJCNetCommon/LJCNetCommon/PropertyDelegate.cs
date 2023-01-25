// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PropertyDelegate.cs
using System;

namespace LJCNetCommon
{
  /// <summary>Represents a PropertyDelegate definition.</summary>
  public class PropertyDelegate
  {
    /// <summary>Gets or sets the PropertyName value.</summary>
    public string PropertyName { get; set; }

    /// <summary>Gets or sets the Delegate reference.</summary>
    public Func<object, object> Value { get; set; }
  }
}
