using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  internal class Program
  {
    static void Main()
    {
      var sectionManager = new SectionManager("DataXML.xml");
      if (LJC.HasListItems(sectionManager.Sections))
      {
        _ = new TestSectionsXML();
        _ = new TestItemsXML();
        _ = new TestReplacementsXML();

        _ = new TestSectionManager();
      }
    }

    // Creates the sample XML.
    internal static string SampleXML()
    {
      string retSample;

      LJCTextBuilder tb = new();
      tb.AddLine("<?xml version='1.0'?>");
      tb.AddLine("<Sections xmlns:xsd='http://www.w3.org/2001/XMLSchema'");
      tb.AddLine(" xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>");
      tb.AddLine("  <Section>");
      tb.AddLine("  <Name>Main</Name>");
      tb.AddLine("  <RepeatItems>");
      tb.AddLine("    <RepeatItem>");
      tb.AddLine("      <Name>Item1</Name>");
      tb.AddLine("      <Replacements>");
      tb.AddLine("        <Replacement>");
      tb.AddLine("          <Name>_Namespace_</Name>");
      tb.AddLine("          <Value>TheNamespace</Value>");
      tb.AddLine("        </Replacement>");
      tb.AddLine("      </Replacements>");
      tb.AddLine("    </RepeatItem>");
      tb.AddLine("  </RepeatItems>");
      tb.AddLine("  <EndProcessing>false</EndProcessing>");
      tb.AddLine("  </Section>");
      tb.AddLine("</Sections>");
      retSample = tb.ToString();
      return retSample;
    }

    // Writes the result to the console.
    internal static void WriteResult(string methodName, string result
      , string compare)
    {
      if (!LJC.HasText(result))
      {
        result = "No Result";
      }
      if (!result.Equals(compare, StringComparison.OrdinalIgnoreCase))
      {
        Console.WriteLine($"TestSectionsXML - {methodName}");
        Console.WriteLine($"{result}");
        Console.WriteLine($" !=");
        Console.WriteLine($"{compare}");
      }
    }
  }
}

