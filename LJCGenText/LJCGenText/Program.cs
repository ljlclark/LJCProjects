// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
// Program.cs
using LJCGenTextLib;
using static System.Console;

namespace LJCGenText
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/Program.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main(string[] args)
    {
      if (args.Length < 2)
      {
        WriteLine("Missing a command line argument.");
        WriteLine("Syntax: LJCGenText TemplateFile.cs DataFile.xml [*.ext]");
        WriteLine("Press ENTER to continue...");
        ReadLine();
      }
      else
      {
        if (3 == args.Length)
        {
          Generate(args[0], args[1], args[2]);
        }
        else
        {
          Generate(args[0], args[1]);
        }
      }
    }

    // Creates the output file.
    private static void Generate(string templateSpec, string dataSpec
      , string outputFile = "*.cs")
    {
      bool overwrite = true;
      GenerateText genText = new GenerateText();
      genText.Generate(templateSpec, dataSpec, outputFile, overwrite);
    }
  }
}

