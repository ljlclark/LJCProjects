using LJCNetCommon5;

namespace LJCSimpleFileTextSort5
{
  internal class SimpleSortProgram
  {
    static void Main(string[] args)
    {
      var success = true;
      if (!LJC.HasArrayElements(args))
      {
        success = false;
        Console.WriteLine("No command line arguments.");
      }

      if (success
        && !File.Exists(args[0]))
      {
        Console.WriteLine($"File {args[0]} was not found.");
      }

      if (success)
      {
        var lines = File.ReadAllLines(args[0]);

        var stripBlankLines = new List<string>();
        foreach (var line in lines)
        {
          if (LJC.HasText(line))
          {
            stripBlankLines.Add(line);
          }
        }
        var saveLines = stripBlankLines.ToArray();

        Array.Sort(saveLines);
        File.WriteAllLines("SortedFile.txt", saveLines);
      }

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}
