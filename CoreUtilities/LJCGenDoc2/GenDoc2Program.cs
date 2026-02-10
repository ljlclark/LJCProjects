// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenCodeDocProgram.cs
using LJCNetCommon;
using System;

namespace LJCGenDoc2
{
  // The GenDoc2 Program.
  /// <include path="members/GenDoc2Program/*" file="Doc/GenDoc2Program.xml"/>
  internal class GenDoc2Program
  {
    // The program entry method.
    /// <include path="members/Main/*" file="Doc/GenDoc2Program.xml"/>
    static void Main(string[] args)
    {
      var genDoc2 = new LJCGenDoc2();

      // Uncomment to change default values in code.
      //var genDocConfig = new LJCGenCodeDocConfig();
      //genDocConfig.DocDataXMLPath = @"../XMLDocData";
      //genDocConfig.GenDataXMLPath = @"../XMLGenData";
      //genDocConfig.WriteDocDataXML = false;
      //genDocConfig.WriteGenDataXML = false;
      //var parentPath = @"LJCGenCodeDoc\bin\Debug";
      //string prefix = genDocConfig.GetParentPathPrefix(parentPath);
      //genDocConfig.OutputPath = $"{prefix}CodeDoc/HTML";
      //genCodeDoc.SetConfig(genDocConfig);

      // Configuration can be set in the source list.
      // This overrides any default values or values set in code.
      // LJCGenCodeDoc\bin\Debug
      // OutputPath: ../../CodeDoc/HTML
      // DocDataXMLPath: ../XMLDocData
      // GenDataXMLPath: ../XMLGenData
      // WriteDocDataXML: true
      // WriteGenDataXML: true

      var showSyntax = CheckArgs(ref args);
      if (!showSyntax)
      {
        genDoc2.CreateFromList(args[0]);
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
