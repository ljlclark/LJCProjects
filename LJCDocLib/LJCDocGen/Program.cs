// LJCDocGen-Program.cs
using LJCDocGenLib;
using LJCDocLibDAL;
using LJCDocObjLib;
using System;
using System.IO;

namespace LJCDocGen
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectDocGen.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/MainArgs/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main(string[] args)
    {
      //CreateDocXml();
      //TestCreateGroup();

      // T - Type
      // M - Method
      // P - Property
      // F - Field
      // E - Event

      bool success = true;
      if (args.Length < 0 || args.Length > 2)
      {
        success = false;
        Console.WriteLine("\r\nSyntax: LJCDocGen.exe [outputPath] [groups.xml]");
        Console.WriteLine(@"outputPath: defaults to 'CodeDocTest'");
        Console.WriteLine("groups.xml: default to LJCCodeDocTest.xml");
        Console.Write("\r\nPress any key to continue...");
        Console.ReadKey();
      }

      if (success)
      {
        GetArgs(args, out string outputPath, out string groupXMLFile);

        // Generate the root page with groups and group assemblies.
        DocGenGroupManager groupManager = new DocGenGroupManager
        {
          FileName = groupXMLFile
        };
        DocGenGroups docGenGroups = groupManager.Load();

        // Creates the DataAssemblies collection with the deserialized
        // documentation XML data.
        DataRoot dataRoot = new DataRoot(docGenGroups);

        File.WriteAllText("Missing.txt", null);

        GenRoot genRoot = new GenRoot(dataRoot, outputPath);
        genRoot.GenRootPage();

        // Generate each assembly page.
        genRoot.GenAssemblyPages();
      }
    }

    // Get the Command Line arguments.
    private static void GetArgs(string[] args, out string outputPath, out string groupXMLFile)
    {
      outputPath = "CodeDocTest";
      if (args.Length > 0)
      {
        outputPath = args[0];
      }

      groupXMLFile = "LJCCodeDocTest.xml";
      if (args.Length > 1)
      {
        groupXMLFile = args[1];
      }
      Console.WriteLine($"\r\nLJCDocGen.exe {outputPath} {groupXMLFile}\r\n");
    }

    ///// <summary>A test method to create the Doc data and serialize to XML.</summary>
    //private static void CreateDocXml()
    //{
    //  Doc doc = new Doc();
    //  DocMember docMember;

    //  // assembly
    //  doc.DocAssembly = new DocAssembly
    //  {
    //    Name = "DocTest"
    //  };

    //  doc.DocMembers = new DocMembers();
    //  DocMembers docMembers = doc.DocMembers;

    //  // class Program
    //  docMember = new DocMember()
    //  {
    //    Name = "T:DocTest.Program",
    //    Summary = "The Class Type Summary."
    //  };
    //  docMembers.Add(docMember);

    //  // Main(string[]args)
    //  docMember = new DocMember()
    //  {
    //    Name = "M:DocTest.Program.Main",
    //    Summary = "The Main Entry Method.",
    //    Params = new DocParams()
    //    {
    //      { "args", "The command line arguments." },
    //      { "test", "The testparam" }
    //    },
    //    Returns = "Returns an integer value.",
    //    //Remarks = "These are the remarks.",
    //    Example = new DocExample()
    //    {
    //      Paras = new DocParas()
    //      {
    //        { "This is paragraph 1." },
    //        { "This is paragraph 2." }
    //      },
    //      Code = string.Format("{0}\r\n        ", @"
    //      foreach (int x in y)
    //      {
    //        int x = 0;
    //      }")
    //    }
    //  };
    //  docMembers.Add(docMember);

    //  NetCommon.XmlSerializeToString(doc.GetType(), doc, null);
    //}

    //// A test method to create AssemblyGroups XML.
    //private static void TestCreateGroup()
    //{
    //  DocGenGroup docGenGroup;
    //  DocGenAssembly docGenAssembly;

    //  DocGenGroups docGenGroups = new DocGenGroups();

    //  docGenGroup = new DocGenGroup
    //  {
    //    Name = "CommonLibraries",
    //    Description = "Common Libraries",
    //    DocGenAssemblies = new DocGenAssemblies()
    //  };
    //  docGenGroups.Add(docGenGroup);
    //  docGenAssembly = new DocGenAssembly
    //  {
    //    FileSpec = @"..\..\..\..\LJC.Libraries\LJC.Net.Common\bin\Debug\LJC.Net.Common.xml",
    //    Description = "The .NET Common library."
    //  };
    //  docGenGroup.DocGenAssemblies.Add(docGenAssembly);
    //  docGenAssembly = new DocGenAssembly
    //  {
    //    FileSpec = @"..\..\..\..\LJC.Libraries\LJC.WinForm.Common\bin\Debug\LJC.WinForm.Common.xml",
    //    Description = "The WinForm Common library."
    //  };
    //  docGenGroup.DocGenAssemblies.Add(docGenAssembly);

    //  docGenGroup = new DocGenGroup
    //  {
    //    Name = "DataLibraries",
    //    Description = "Data Libraries",
    //    DocGenAssemblies = new DocGenAssemblies()
    //  };
    //  docGenGroups.Add(docGenGroup);
    //  docGenAssembly = new DocGenAssembly
    //  {
    //    FileSpec = @"..\..\..\..\LJC.DBClientLib\LJC.DBClientLib\bin\Debug\LJC.DBClientLib.xml",
    //    Description = "The Data Service Client library."
    //  };
    //  docGenGroup.DocGenAssemblies.Add(docGenAssembly);
    //  docGenAssembly = new DocGenAssembly
    //  {
    //    FileSpec = @"..\..\..\..\LJC.DBMessage\LJC.DBMessage\bin\Debug\LJC.DBMessage.xml",
    //    Description = "The Data Service Message library.",
    //    MainImage = "DBMessageGraph.jpg"
    //  };
    //  docGenGroup.DocGenAssemblies.Add(docGenAssembly);

    //  NetCommon.XmlSerialize(docGenGroups.GetType(), docGenGroups, null
    //    , "AssemblyGroups.xml");
    //}
  }
}
