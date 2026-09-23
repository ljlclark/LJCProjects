// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenText5Program.cs
using LJCGenTextLib5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using static System.Console;

namespace LJCGenText5
{
  internal class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 2)
      {
        WriteLine("Missing a command line argument.");
        WriteLine("Syntax: LJCGenText5 TemplateFile.cs DataFile.xml [*.ext]");
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

    private static void Generate(string templateSpec, string dataSpec
      , string outputSpec = @"Output\*.cs")
    {
      var genText = new GenTextLib();

      // Set values if template extension is "html".
      outputSpec = SetHtmlValues(templateSpec, outputSpec, genText);

      var sections = LJC.XmlDeserialize(typeof(Sections), dataSpec) as Sections;
      if (LJC.HasListItems(sections))
      {
        var templateLines = File.ReadAllLines(templateSpec);
        string output = genText.TextGen(sections, templateLines);
        if (LJC.HasText(output))
        {
          // Create output name from dataSpec.
          outputSpec = GetOutputSpec(outputSpec, dataSpec);

          File.WriteAllText(outputSpec, output);
        }
      }
    }

    // Create output name from dataSpec.
    private static string GetOutputSpec(string outputSpec, string dataSpec)
    {
      string retOutputSpec = outputSpec;

      var outputPath = Path.GetDirectoryName(outputSpec);
      var outputName = Path.GetFileNameWithoutExtension(outputSpec);
      var outputExt = Path.GetExtension(outputSpec);
      if (outputName == "*")
      {
        var dataName = Path.GetFileNameWithoutExtension(dataSpec);
        if (LJC.HasText(dataName))
        {
          retOutputSpec = "";
          if (LJC.HasText(outputPath))
          {
            retOutputSpec += $@"{outputPath}\";
          }
          retOutputSpec += $"{dataName}{outputExt}";
        }
      }
      return retOutputSpec;
    }

    // Set values if template extension is "html".
    private static string SetHtmlValues(string templateSpec, string outputSpec
      , GenTextLib genText)
    {
      string retOutputSpec = outputSpec;

      var ext = Path.GetExtension(templateSpec);
      if (".html" == ext
        || ".htm" == ext)
      {
        genText.CommentChars = "<!--";
        ext = Path.GetExtension(outputSpec);
        if (".html" != ext
          && ".htm" != ext)
        {
          retOutputSpec = "*.html";
        }
      }
      return retOutputSpec;
    }
  }
}
