// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestSectionsXML5.cs
using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides Sections specific XML test methods.
  internal class TestSectionsXML
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestSectionsXML()
    {
      // Static Methods
      LJCDeserialize();
      LJCDeserializeString();

      // Methods
      Add();
      Retrieve();
      LJCSerialize();
      LJCSerializeString();
    }
    #endregion

    #region Test Methods

    // Deserializes from the specified XML file.
    private void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      string result;
      string compare;

      // Test with no sections.
      var sections = new Sections();
      if (!LJC.HasListItems(sections))
      {
        result = "";
        compare = "No Result";
        WriteResult($"{methodName}1", result, compare);
      }

      var fileName = "Sections.xml";
      var xml = SampleXML();
      File.WriteAllText(fileName, xml);

      // Test Method
      sections = Sections.LJCDeserialize(fileName);

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

        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Deserializes from the supplied XML string.
    private void LJCDeserializeString()
    {
      var methodName = "LJCDeserializeString()";

      var xml = SampleXML();

      // Test Method
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

        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Creates and adds the Section object with the supplied values.
    private void Add()
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

        var name = "OneMore";

        // Test Method
        sections.Add(name);

        var section = sections.Retrieve(name);
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "OneMore";
        WriteResult($"{methodName}2", result, compare);

        result = sections.Count.ToString();
        sections.Add(name);
        compare = sections.Count.ToString();
        WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Retrieve the collection element with name.
    private void Retrieve()
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

        // Test Method
        section = sections.Retrieve("Main");

        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Serializes the collection to a file.
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

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

        var fileName = "Sections.xml";

        // Test Method
        sections.LJCSerialize(fileName);

        sections = Sections.LJCDeserialize(fileName);
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          WriteResult($"{methodName}2", result, compare);
          break;
        }

        var section = sections[0];
        if (null == section)
        {
          result = "";
          compare = "Main";
          WriteResult($"{methodName}3", result, compare);
          break;
        }

        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        WriteResult($"{methodName}4", result, compare);
        break;
      }
    }

    // Serializes the collection to a string.
    private void LJCSerializeString()
    {
      var methodName = "LJCSerializeString()";

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

        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        WriteResult($"{methodName}3", result, compare);
        break;
      }
    }
    #endregion

    #region Other Methods

    // Creates the sample XML.
    private string SampleXML()
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
    private void WriteResult(string methodName, string result
      , string compare)
    {
      if (!LJC.HasText(result))
      {
        result = "No Result";
      }
      if (!result.Equals(compare, StringComparison.OrdinalIgnoreCase))
      {
        Console.WriteLine($"TestSecyionsXML - {methodName}");
        Console.WriteLine($"{result}");
        Console.WriteLine($" !=");
        Console.WriteLine($"{compare}");
      }
    }
    #endregion
  }
}
