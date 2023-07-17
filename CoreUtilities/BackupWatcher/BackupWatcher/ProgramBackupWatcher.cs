using System;
using System.IO;

namespace BackupWatcher
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var watchSpec = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev";
      var multiFilter = @"*.cs, *.cproj, *.sln, *.config, Doc\*.xml";
      if (1 == args.Length)
      {
        watchSpec = args[0];
      }
      if (args.Length >= 2)
      {
        watchSpec = args[0];
        multiFilter = args[1];
      }

      var _ = new BackupWatcher(watchSpec)
      {
        MultiFilter = multiFilter
      };

      Console.WriteLine("Press ENTER to exit.");
      Console.ReadLine();
    }
  }
}
