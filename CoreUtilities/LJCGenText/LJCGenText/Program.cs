// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using LJCGenTextLib;
using System.IO;
using static System.Console;

namespace LJCGenText
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/Program.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../LJCGenDoc/Common/Program.xml'/>
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
      //bool overwrite = true;
      GenerateText genText = new GenerateText();
      var ext = Path.GetExtension(templateSpec);
      if (".html" == ext
        || ".htm" == ext)
      {
        genText.CommentChars = "<!--";
      }
      //genText.Generate(templateSpec, dataSpec, outputFile, overwrite);
    }
  }
}

