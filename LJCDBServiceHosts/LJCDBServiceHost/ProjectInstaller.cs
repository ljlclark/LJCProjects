// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// ProjectInstaller.cs
using System;
using System.ComponentModel;
using System.Configuration.Install;

namespace LJCDBServiceHost
{
  /// <summary>The service installer class.</summary>
  [RunInstaller(true)]
  public partial class ProjectInstaller : Installer
  {
    /// <summary>Initializes the object instance.</summary>
    public ProjectInstaller()
    {
      InitializeComponent();
    }
  }
}
