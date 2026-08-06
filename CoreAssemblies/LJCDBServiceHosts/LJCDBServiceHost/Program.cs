// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using System;
using System.ServiceProcess;

namespace LJCDBServiceHost
{
  // The program entry point class.
  /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'
  ///  path='items/Program/*'/>
  public static class Program
  {
    // The program entry point function.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'
    ///  path='items/Main/*'/>
    private static void Main()
    {
      ServiceBase[] ServicesToRun;
      ServicesToRun = new ServiceBase[]
      {
        new DBWindowsService()
      };
      ServiceBase.Run(ServicesToRun);
    }
  }
}
