// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
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
      StandardSettings = new StandardSettings();
      StandardSettings.SetProperties("LJCRegionManager.exe.config");
    }

    #region Properties

    // The singleton instance.
    internal static ValuesRegion Instance { get; } = new ValuesRegion();

    // Gets or sets the StandardSettings value.
    internal StandardSettings StandardSettings { get; set; }
    #endregion
  }
}
