// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProgramDocGen.cs
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDocGenLib;
using LJCDocLibDAL;
using LJCDocObjLib;
using System;
using System.IO;

namespace LJCDocGen
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectDocGen.xml'/>
  public class ProgramDocGen
  {
    // The program entry point function.
    /// <include path='items/MainArgs/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main(string[] args)
    {
      bool success = true;
      if (args.Length < 0 || args.Length > 2)
      {
        success = false;
        Console.WriteLine("\r\nSyntax: LJCDocGen.exe [outputPath]");
        Console.WriteLine(@"outputPath: defaults to 'CodeDocTest'");
        Console.Write("\r\nPress any key to continue...");
        Console.ReadKey();
      }

      if (success)
      {
        var outputPath = "CodeDocTest";
        if (args.Length > 0)
        {
          outputPath = args[0];
        }
        Console.WriteLine($"\r\nLJCDocGen.exe {outputPath}\r\n");

        var managers = ValuesDocGen.Instance.Managers;
        var assemblyGroupManager = managers.DocAssemblyGroupManager;
        var assemblyGroups = assemblyGroupManager.Load();

        File.WriteAllText("Missing.txt", null);

        // Creates the DataRoot.DataAssemblies collection with the deserialized
        // "Doc" XML converted to the "Data" XML format.
        DataRoot dataRoot = new DataRoot(assemblyGroups);

        GenRoot genRoot = new GenRoot(dataRoot, outputPath);
        genRoot.GenRootPage();

        // Generate each assembly page.
        genRoot.GenAssemblyPages();
        var pageCount = ValuesDocGen.Instance.GenPageCount.ToString();
        File.WriteAllText("HTMLPageCount.txt", pageCount); ;
      }
    }
  }
}
