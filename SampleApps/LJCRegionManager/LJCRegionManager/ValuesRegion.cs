// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesRegion.cs
using System;
using LJCDBClientLib;

namespace LJCRegionManager
{
  // Application config values singleton.
  internal sealed class ValuesRegion
  {
    // Initializes an object instance.
    internal ValuesRegion()
    {
      StandardSettings = new StandardUISettings();
      StandardSettings.SetProperties("LJCRegionManager.exe.config");
    }

    #region Properties

    // The singleton instance.
    internal static ValuesRegion Instance { get; } = new ValuesRegion();

    // Gets or sets the StandardSettings value.
    internal StandardUISettings StandardSettings { get; set; }
    #endregion
  }
}
