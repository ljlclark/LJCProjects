using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpandLeadingTabs
{
  internal class ProgramExpandLeadingTabs
  {
    static void Main(string[] args)
    {
      if (args.Length < 1)
      {
        Console.WriteLine("Syntax: ExpandLeadingTabs.exe fileSpec");
        Console.WriteLine("Press any key to close this window . . .");
        Console.ReadKey();
      }
      else
      {
        var fileSpec = args[0];
        if (File.Exists(fileSpec))
        {
          var outFileSpec = GetOutputFileSpec(fileSpec);
          var outLines = new List<string>();
          var lines = File.ReadAllLines(fileSpec);
          foreach (var line in lines)
          {
            var outLine = line;
            var whiteSpaceLine = "";
            int index = line.TakeWhile(c => char.IsWhiteSpace(c)).Count();
            if (index > 0)
            {
              var saveLine = line.Substring(index);
              whiteSpaceLine = line.Substring(0, index);
              if (whiteSpaceLine.Contains("\t"))
              {
                var spacesLine = whiteSpaceLine.Replace("\t", "  ");
                outLine = spacesLine + saveLine;
              }
            }
            outLines.Add(outLine);
          }
          File.WriteAllLines(outFileSpec, outLines);
        }
      }
    }

    private static string GetOutputFileSpec(string fileSpec)
    {
      var retFileSpec = fileSpec + "New";

      if (fileSpec.Contains("."))
      {
        var dotIndex = fileSpec.IndexOf('.');
        retFileSpec = fileSpec.Substring(0, dotIndex);
        var outExt = fileSpec.Substring(dotIndex);
        retFileSpec = retFileSpec + "New" + outExt;
      }
      return retFileSpec;
    }
  }
}
