// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// Program.cs
using LJCDBServiceLib;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace LJCDBServiceConsoleHost
{
  // The entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectDBServiceConsole.xml'/>
  public class Program
  {
    // The main entry point method.
    /// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main()
    {
      using (ServiceHost serviceHost = new ServiceHost(
        typeof(DbService)))
      {
        serviceHost.Open();

        foreach (ServiceEndpoint endPoint in serviceHost.Description.Endpoints)
        {
          Console.WriteLine("{0} - {1} - {2}", endPoint.Address, endPoint.Binding.Name
            , endPoint.Contract.Name);
        }
        Console.WriteLine("The service is ready.");
        Console.WriteLine("Press the ENTER key to terminate the service.");
        Console.ReadLine();
      }
    }
  }
}
