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
      Retrieve();
      Load();
      Delete();

      // Methods
      Save();
    }

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    private static void Constructor1()
    {
      var methodName = "Constructor1()";

      var fileName = "Sections.xml";
      var xml = Program.SampleXML();
      File.WriteAllText(fileName, xml);

      string result;
      string compare;
      while (true)
      {
        // Test Method
        var sectionManager = new SectionManager(fileName);

        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}1", result, compare);
          break;
        }

        var section = sectionManager.Retrieve("Main");
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "Main";
        Program.WriteResult($"{methodName}2", result, compare);
        break;
      }
    }

    // Initializes an object instance with the supplied values.
    private static void Constructor2()
    {
      var methodName = "Constructor2()";

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

        // Test Method
        var sectionManager = new SectionManager(sections);

        if (!LJC.HasListItems(sectionManager.Sections))
        {
          result = "";
          compare = "No Sections";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var section = sectionManager.Retrieve("Main");
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

    #region Data Methods

    // Adds a Section record to the object data.
    private static void Add()
    {
      var methodName = "Add()";

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

        sections = sectionManager.Sections;

        // Test Method
        var section = sections.Add("OneMore");

        if (null == section)
        {
          result = "";
          compare = "OneMore";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        section = sectionManager.Retrieve("OneMore");
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "OneMore";
        Program.WriteResult($"{methodName}4", result, compare);
        break;
      }
    }

    // Retrieves a Section record from the object data.
    private static void Retrieve()
    {
      var methodName = "Retrieve()";

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

        // Test Method
        var section = sectionManager.Retrieve("Main");

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

    // Retrieves a collection of data records.
    private static void Load()
    {
      var methodName = "Load()";

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

        // Test Method
        sections = sectionManager.Load();

        var section = sections[0];
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

    // Deletes the Section record from the object data.
    private static void Delete()
    {
      var methodName = "Delete()";

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

        var section = sections.Add("OneMore");
        if (null == section)
        {
          result = "";
          compare = "OneMore";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        section = sectionManager.Retrieve("OneMore");
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "OneMore";
        Program.WriteResult($"{methodName}4", result, compare);

        // Child replacements must be deleted first.
        var sectionName = "Main";
        var repeatItemName = "Item1";
        var replacementManager = new ReplacementManager(sections);
        replacementManager.DeleteReplacements(sectionName, repeatItemName);

        // Child repeat items must be deleted first.
        var repeatItemManager = new RepeatItemManager(sections);
        repeatItemManager.DeleteRepeatItems(sectionName);

        // Test Method
        if (sectionManager.Delete("Main"))
        {
          section = sections[0];
          result = "";
          if (section != null)
          {
            result = section.Name;
          }
          compare = "OneMore";
          Program.WriteResult($"{methodName}5", result, compare);
        }

        section = sections[0];
        result = "";
        if (section != null)
        {
          result = section.Name;
        }
        compare = "OneMore";
        Program.WriteResult($"{methodName}6", result, compare);
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
