// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
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
