// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// Program.cs
using System;
using System.ServiceProcess;

namespace LJCDBServiceHost
{
  // The program entry point class.
  /// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
  public static class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
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
