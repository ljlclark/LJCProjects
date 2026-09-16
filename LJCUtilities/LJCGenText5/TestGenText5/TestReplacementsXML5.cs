// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestReplacementsXML5.cs
using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides Replacements specific XML test methods.
  internal class TestReplacementsXML
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestReplacementsXML()
    {
      // Methods
      Add();
      Retrieve();
    }
    #endregion

    #region Test Methods

    // Creates and adds the Replacement object with the supplied values.
    private static void Add()
    {
      var methodName = "Add()";

      var xml = SampleXML();
      var sections = Sections.LJCDeserializeString(xml);
      string result;
      string compare;
      while (true)
      {
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          WriteResult($"{methodName}1", result, compare);
          break;
        }

        var section = sections[0];
        if (null == section)
        {
          result = "";
          compare = "Main";
          WriteResult($"{methodName}2", result, compare);
          break;
        }

        var items = section.RepeatItems;
        if (!LJC.HasListItems(items))
        {
          result = "";
          compare = "No RepeatItems";
          WriteResult($"{methodName}3", result, compare);
          break;
        }

        var item = items[0];
        if (null == section)
        {
          result = "";
          compare = "Item1";
          WriteResult($"{methodName}4", result, compare);
          break;
        }

        var replacements = item.Replacements;
        if (!LJC.HasListItems(items))
        {
          result = "";
          compare = "No Replacements";
          WriteResult($"{methodName}5", result, compare);
          break;
        }

        var name = "_OneMore_";

        // Test Method
        replacements.Add(name, "OneMore");

        var replacement = replacements.Retrieve(name);
        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = "_OneMore_";
        WriteResult($"{methodName}6", result, compare);

        result = replacements.Count.ToString();
        items.Add(name);
        compare = replacements.Count.ToString();
        WriteResult($"{methodName}7", result, compare);
        break;
      }
    }

    // Retrieve the collection element with name.
    private static void Retrieve()
    {
      var methodName = "Retrieve()";

      var xml = SampleXML();
      var sections = Sections.LJCDeserializeString(xml);

      Section? section;
      string result;
      string compare;
      while (true)
      {
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          WriteResult($"{methodName}1", result, compare);
          break;
        }

        section = sections[0];
        if (null == section)
        {
          result = "";
          compare = "Main";
          WriteResult($"{methodName}2", result, compare);
          break;
        }

        var items = section.RepeatItems;
        if (!LJC.HasListItems(items))
        {
          result = "";
          compare = "No RepeatItems";
          WriteResult($"{methodName}3", result, compare);
          break;
        }

        var item = items[0];
        if (null == item)
        {
          result = "";
          compare = "Item1";
          WriteResult($"{methodName}4", result, compare);
          break;
        }

        var replacements = item.Replacements;
        if (null == replacements)
        {
          result = "";
          compare = "No Replacements";
          WriteResult($"{methodName}5", result, compare);
          break;
        }

        // Test Method
        var replacement = replacements.Retrieve("_Namespace_");

        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = "_Namespace_";
        WriteResult($"{methodName}6", result, compare);
        break;
      }
    }
    #endregion

    #region Other Methods

    // Creates the sample XML.
    private static string SampleXML()
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
    private static void WriteResult(string methodName, string result
      , string compare)
    {
      if (!LJC.HasText(result))
      {
        result = "No Result";
      }
      if (!result.Equals(compare, StringComparison.OrdinalIgnoreCase))
      {
        Console.WriteLine($"TestReplacementsXML - {methodName}");
        Console.WriteLine($"{result}");
        Console.WriteLine($" !=");
        Console.WriteLine($"{compare}");
      }
    }
    #endregion
  }
}
