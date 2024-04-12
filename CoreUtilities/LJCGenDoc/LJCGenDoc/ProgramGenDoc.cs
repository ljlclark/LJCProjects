// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProgramDocGen.cs
using LJCGenDocLib;
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenDoc
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectDocGen.xml'/>
  public class ProgramGenDoc
  {
    // The program entry point function.
    /// <include path='items/MainArgs/*' file='../../LJCGenDoc/Common/Program.xml'/>
    private static void Main(string[] args)
    {
      bool success = true;
      if (args.Length < 0 || args.Length > 1)
      {
        success = false;
        Console.WriteLine("\r\nSyntax: LJCGenDoc.exe [outputPath]");
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
        Console.WriteLine();
        Console.WriteLine($"LJCGenDoc {outputPath}");
        Console.WriteLine();

        // Set DAL config before using anywhere else in the program.
        var configValues = ValuesGenDoc.Instance;
        configValues.SetConfigFile();
        var settings = configValues.StandardSettings;
        NetCommon.ConsoleConfig(settings.DataConfigName);

        var managers = configValues.Managers;
        var assemblyGroupManager = managers.DocAssemblyGroupManager;
        var assemblyGroups = assemblyGroupManager.Load();

        File.WriteAllText("Missing.txt", null);

        // Creates the LJCDocObjLib.DataRoot.DataAssemblies collection with
        // the deserialized "Doc" XML converted to the "Data" XML format.
        DataRoot dataRoot = new DataRoot(assemblyGroups);
        // * Create XML Data from "Doc" XML data
        // LJCDocObjLib.DataType.DataType();
        // LJCDocObjLib.DataType.CreateMethodsData();
        // LJCDocObjLib.DataMethod.DataMethod();
        // * Set Is Public
        // LJCDocObjLib.DataType.CeateMethodsData();
        GenRoot genRoot = new GenRoot(dataRoot, outputPath);
        genRoot.GenRootPage();

        // Generate each assembly page.
        genRoot.GenAssemblyPages();
        // LJCDocGenLib.GenAssembly.GenAssemblyPage();
        // LJCDocGenLib.CreateAssemblyXML.GetXMLData();
        // LJCDocGenLib.GenType.GetTypePage();
        // LJCDocGenLib.CreateTypeXML.GetXMLData();
        // LJCDocGenLib.GenMethod.GenMethodPage();
        // LJCDocGenLib.CrerateMethodXML.GetXMLData();
        // LJCGenTextLib.GenerateText.GenRepeatItem();
        var pageCount = ValuesGenDoc.Instance.GenPageCount.ToString();
        File.WriteAllText("HTMLPageCount.txt", pageCount); ;
      }
    }
  }
}
