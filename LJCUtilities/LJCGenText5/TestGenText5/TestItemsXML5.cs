// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestItemsXML5.cs
using LJCGenTextXAL5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides RepeatItems specific XML test methods.
  internal class TestItemsXML
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestItemsXML()
    {
      // Methods
      Add();
      Retrieve();
    }
    #endregion

    #region Test Methods

    // Creates and adds the Item object with the supplied values.
    private void Add()
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

        // Verify the first section is "Main".
        var section = sections[0];
        if (null == section)
        {
          result = "";
          compare = "Main";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Verify the section has repeat items.
        var items = section.RepeatItems;
        if (!LJC.HasListItems(items))
        {
          result = "";
          compare = "No RepeatItems";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        // Set test values.
        var name = "OneMore";

        // Test Method
        items.Add(name);

        // Verify item was added.
        var item = items.Retrieve(name);
        result = "";
        if (item != null)
        {
          result = item.Name;
        }
        compare = "OneMore";
        Program.WriteResult($"{methodName}4", result, compare);

        // Verify duplicate is not added.
        result = items.Count.ToString();
        items.Add(name);
        compare = items.Count.ToString();
        Program.WriteResult($"{methodName}5", result, compare);
        break;
      }
    }

    // Retrieve the collection element with name.
    private void Retrieve()
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

        // Verify the first section is "Main".
        section = sections[0];
        if (null == section)
        {
          result = "";
          compare = "Main";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Verify the section has repeat items.
        var items = section.RepeatItems;
        if (!LJC.HasListItems(items))
        {
          result = "";
          compare = "No RepeatItems";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        // Test Method
        var item = items.Retrieve("Item1");

        // Verify retrieved item.
        result = "";
        if (item != null)
        {
          result = item.Name;
        }
        compare = "Item1";
        Program.WriteResult($"{methodName}4", result, compare);
        break;
      }
    }
    #endregion
  }
}
