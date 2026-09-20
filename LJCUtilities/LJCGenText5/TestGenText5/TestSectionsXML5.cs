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

    #region Static Methods

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
        Program.WriteResult($"{methodName}1", result, compare);
      }

      // Create XML file.
      var fileName = "Sections.xml";
      var xml = Program.SampleXML();
      File.WriteAllText(fileName, xml);

      while (true)
      {
        // Test Method
        sections = Sections.LJCDeserialize(fileName);

        // Check if the collection has items.
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Set test values.
        var sectionName = "Main";

        // Verify the first item is "Main".
        var section = sections[0];
        if (null == section)
        {
          result = "";
          compare = sectionName;
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Verify retrieved item.
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = sectionName;
        Program.WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Deserializes from the supplied XML string.
    private void LJCDeserializeString()
    {
      var methodName = "LJCDeserializeString()";

      string result;
      string compare;
      while (true)
      {
        var xml = Program.SampleXML();

        // Test Method
        var sections = Sections.LJCDeserializeString(xml);

        // Check if the collection has items.
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Verify the first item is "Main".
        var section = sections[0];
        if (null == section)
        {
          result = "";
          compare = "Main";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Verify retrieved item.
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        Program.WriteResult($"{methodName}3", result, compare);
        break;
      }
    }
    #endregion

    #region Methods

    // Creates and adds the Section object with the supplied values.
    private static void Add()
    {
      var methodName = "Add()";

      string result;
      string compare;
      while (true)
      {
        // Get XML and create the Sections collection.
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        // Check if the collection has items.
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Set test values.
        var name = "OneMore";

        // Test Method
        sections.Add(name);

        // Verify item was added.
        var section = sections.Retrieve(name);
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = name;
        Program.WriteResult($"{methodName}2", result, compare);

        // Verify duplicate is not added.
        result = sections.Count.ToString();
        sections.Add(name);
        compare = sections.Count.ToString();
        Program.WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Retrieve the collection element with name.
    private static void Retrieve()
    {
      var methodName = "Retrieve()";

      Section? section;
      string result;
      string compare;
      while (true)
      {
        // Get XML and create the Sections collection.
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        // Check if the collection has items.
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Set test values.
        var sectionName = "Main";

        // Test Method
        section = sections.Retrieve(sectionName);

        // Verify retrieved item.
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = sectionName;
        Program.WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Serializes the collection to a file.
    private static void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      string result;
      string compare;
      while (true)
      {
        // Get XML and create the Sections collection.
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        // Check if the collection has items.
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Set test values.
        var fileName = "Sections.xml";

        // Test Method
        sections.LJCSerialize(fileName);

        // Check if the collection has items.
        sections = Sections.LJCDeserialize(fileName);
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Set test values.
        var sectionName = "Main";

        var section = sections[0];

        // Check if there are no sections.
        if (null == section)
        {
          result = "";
          compare = sectionName;
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        // Verify the first item is "Main".
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = sectionName;
        Program.WriteResult($"{methodName}4", result, compare);
        break;
      }
    }

    // Serializes the collection to a string.
    private static void LJCSerializeString()
    {
      var methodName = "LJCSerializeString()";

      string result;
      string compare;
      while (true)
      {
        // Get XML and create the Sections collection.
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        // Check if the collection has items.
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Set test values.
        var sectionName = "Main";

        var section = sections[0];

        // Check if there are no sections.
        if (null == section)
        {
          result = "";
          compare = sectionName;
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Verify the first item is "Main".
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = sectionName;
        Program.WriteResult($"{methodName}3", result, compare);
        break;
      }
    }
    #endregion
  }
}
