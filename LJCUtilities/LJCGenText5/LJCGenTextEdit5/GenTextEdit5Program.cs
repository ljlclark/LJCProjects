// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenTextEdit5Program.cs

// Install-Package Microsoft.Data.SqlClient
// Install-Package System.Configuration.ConfigurationManager

namespace LJCGenTextEdit5
{
  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();
      Application.Run(new EditList());
    }
  }
}