// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenCodeDocProgram.cs
using LJCDocDataDAL;
using LJCDocDataGenLib;
using LJCNetCommon;
using System;
using System.CodeDom.Compiler;

// The GenDoc2 Program.
/// <include path="members/LJCGenDoc2/*" file="Doc/GenDoc2Program.xml"/>
/// LibName: LJCGenDoc2

namespace LJCGenDoc2
{
  // The program entry class.
  /// <include path="members/GenDoc2Program/*" file="Doc/GenDoc2Program.xml"/>
  internal class GenDoc2Program
  {
    // The program entry method.
    /// <include path="members/Main/*" file="Doc/GenDoc2Program.xml"/>
    static void Main(string[] args)
    {
      var genDoc2 = new LJCGenDoc2();

      var genDocConfig = new LJCGenDocConfig
      {
        DocDataXMLPath = "../XMLDocData",
        GenDataXMLPath = "../XMLGenData",
        WriteDocDataXML = false,
        WriteGenDataXML = false
      };
      var parentPath = @"LJCGenDoc2\bin\Debug";
      string prefix = genDocConfig.GetParentPathPrefix(parentPath);
      genDocConfig.OutputPath = $"{prefix}CodeDoc/HTML";
      genDoc2.SetConfig(genDocConfig);

      // Configuration can be set in the source list.
      // This overrides any default values or values set in code.
      // SourceList.txt
      // // LJCGenDoc2\bin\Debug
      // OutputPath: ../../CodeDoc/HTML
      // DocDataXMLPath: ../XMLDocData
      // GenDataXMLPath: ../XMLGenData
      // WriteDocDataXML: true
      // WriteGenDataXML: true

      var showSyntax = CheckArgs(ref args);
      if (!showSyntax)
      {
        var sourceList = args[0];
        genDoc2.CreateFromList(sourceList);
      }
    }

    // Check the program arguments.
    private static bool CheckArgs(ref string[] args)
    {
      var retShowSyntax = false;

      if (null == args)
      {
        retShowSyntax = true;
      }
      if (!retShowSyntax
        || args.Length < 1
        || !NetString.HasValue(args[0]))
      {
        retShowSyntax = true;
      }

      // Testing
      if (retShowSyntax)
      {
        args = new string[] { "SourceList.txt" };
        retShowSyntax = false;
      }

      if (retShowSyntax)
      {
        Console.WriteLine("Syntax: LJCGenCodeDoc SourceList.txt");
        Console.WriteLine("Press and key to continue . . .");
        Console.ReadKey();
      }
      return retShowSyntax;
    }
  }
}
