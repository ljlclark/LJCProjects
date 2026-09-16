using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  internal class TestSectionManager
  {
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

    // Initializes an object instance with the supplied values.
    private void Constructor1()
    {

    }

    // Initializes an object instance with the supplied values.
    private void Constructor2()
    {
    }

    // Adds a Section record to the object data.
    private void Add()
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
    private void Retrieve()
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
    private void Load()
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
    private void Delete()
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
    private void Save()
    {
    }
  }
}
