// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestSectionManager5.cs
using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides SectionManager specific test methods.
  internal class TestSectionManager
  {
    // Initializes an object instance.
    public TestSectionManager()
    {
      // Constructor Methods
      Constructor1();
      Constructor2();

      // Data Methods
      Add();
      Delete();
      Load();
      Retrieve();

      // Methods
      Save();
    }

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    private static void Constructor1()
    {
      var methodName = "Constructor1()";

      string result;
      string compare;
      while (true)
      {
        var fileName = "Sections.xml";
        var xml = Program.SampleXML();
        File.WriteAllText(fileName, xml);

        // Test Method
        var sectionManager = new SectionManager(fileName);

        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var sectionName = "Main";
        var section = sectionManager.Retrieve(sectionName);
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = sectionName;
        Program.WriteResult($"{methodName}2", result, compare);
        break;
      }
    }

    // Initializes an object instance with the supplied values.
    private static void Constructor2()
    {
      var methodName = "Constructor2()";

      string result;
      string compare;
      while (true)
      {
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        // Test Method
        var sectionManager = new SectionManager(sections);

        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var sectionName = "Main";
        var section = sectionManager.Retrieve(sectionName);
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

    #region Data Methods

    // Adds a Section record to the object data.
    private static void Add()
    {
      var methodName = "Add()";

      string result;
      string compare;
      while (true)
      {
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var sectionManager = new SectionManager(sections);
        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var newSectionName = "OneMore";
        var section = new Section(newSectionName);

        // Test Method
        sectionManager.Add(section);

        section = sectionManager.Retrieve(newSectionName);
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = newSectionName;
        Program.WriteResult($"{methodName}4", result, compare);
        break;
      }
    }

    // Deletes the Section record from the object data.
    private static void Delete()
    {
      var methodName = "Delete()";

      string result;
      string compare;
      while (true)
      {
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var sectionManager = new SectionManager(sections);
        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var newSectionName = "OneMore";
        var section = sections.Add(newSectionName);
        if (null == section)
        {
          result = "";
          compare = newSectionName;
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        section = sectionManager.Retrieve(newSectionName);
        if (null == section)
        {
          result = "";
          compare = newSectionName;
          Program.WriteResult($"{methodName}4", result, compare);
          break;
        }

        sections = sectionManager.Load();
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}5", result, compare);
          break;
        }

        // Child replacements must be deleted first.
        var sectionName = "Main";
        var repeatItemName = "Item1";
        var replacementManager = new ReplacementManager(sections);
        replacementManager.DeleteReplacements(newSectionName, repeatItemName);

        // Child repeat items must be deleted first.
        var repeatItemManager = new RepeatItemManager(sections);
        repeatItemManager.DeleteRepeatItems(newSectionName);

        // Test Method
        if (!sectionManager.Delete(newSectionName))
        {
          section = sectionManager.Retrieve(sectionName);
          result = "";
          if (section != null)
          {
            result = section.Name;
          }
          compare = "No Result";
          Program.WriteResult($"{methodName}6", result, compare);
        }

        section = sectionManager.Retrieve(newSectionName);
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "No Result";
        Program.WriteResult($"{methodName}7", result, compare);
        break;
      }
    }

    // Retrieves a collection of data records.
    private static void Load()
    {
      var methodName = "Load()";

      string result;
      string compare;
      while (true)
      {
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var sectionManager = new SectionManager(sections);
        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Test Method
        sections = sectionManager.Load();

        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        var section = sections[0];
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        Program.WriteResult($"{methodName}4", result, compare);
        break;
      }
    }

    // Retrieves a Section record from the object data.
    private static void Retrieve()
    {
      var methodName = "Retrieve()";

      string result;
      string compare;
      while (true)
      {
        var xml = Program.SampleXML();
        var sections = Sections.LJCDeserializeString(xml);

        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var sectionManager = new SectionManager(sections);
        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var sectionName = "Main";

        // Test Method
        var section = sectionManager.Retrieve(sectionName);

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

    // Save the XML data.
    private static void Save()
    {
      var methodName = "Save()";

      var xml = Program.SampleXML();
      var sections = Sections.LJCDeserializeString(xml);

      string result;
      string compare;
      while (true)
      {
        if (!LJC.HasListItems(sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var sectionManager = new SectionManager(sections);
        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        break;
      }
    }
    #endregion
  }
}
