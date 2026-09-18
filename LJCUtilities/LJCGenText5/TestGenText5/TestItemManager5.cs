// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestItemManager5.cs
using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides RepeatItemManager specific test methods.
  internal class TestItemManager
  {
    // Initializes an object instance.
    public TestItemManager()
    {
      // Constructor Methods
      Constructor();

      // Data Methods
      Add();
      Delete();
      DeleteRepeatItems();
      Load();
      Retrieve();
    }

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    private static void Constructor()
    {
      var methodName = "Constructor()";

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
        var repeatItemManager = new RepeatItemManager(sections);

        var sectionName = "Main";
        var itemName = "Item1";
        var repeatItem = repeatItemManager.Retrieve(sectionName, itemName);
        result = "";
        if (repeatItem != null)
        {
          result = repeatItem.Name;
        }
        compare = itemName;
        Program.WriteResult($"{methodName}2", result, compare);
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

        var repeatItemManager = new RepeatItemManager(sections);
        var sectionName = "Main";
        var newItemName = "Item2";
        var repeatItem = new RepeatItem()
        {
          Name = newItemName,
        };

        // Test Method
        repeatItemManager.Add(sectionName, repeatItem);

        repeatItem = repeatItemManager.Retrieve(sectionName, newItemName);
        result = "";
        if (repeatItem != null)
        {
          result = repeatItem.Name;
        }
        compare = newItemName;
        Program.WriteResult($"{methodName}2", result, compare);
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

        var repeatItemManager = new RepeatItemManager(sections);
        var sectionName = "Main";
        var newItemName = "Item2";
        var repeatItem = new RepeatItem()
        {
          Name = newItemName,
        };
        repeatItemManager.Add(sectionName, repeatItem);

        repeatItem = repeatItemManager.Retrieve(sectionName, newItemName);
        if (null == repeatItem)
        {
          result = "";
          compare = newItemName;
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var repeatItems = repeatItemManager.Load(sectionName);
        if (!LJC.HasListItems(repeatItems))
        {
          result = "";
          compare = "No RepeatItems";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        // Child replacements must be deleted first.
        var replacementManager = new ReplacementManager(sections);
        replacementManager.DeleteReplacements(sectionName, "Item1");

        // Test Method
        if (!repeatItemManager.Delete(sectionName, newItemName))
        {
          repeatItem = repeatItemManager.Retrieve(sectionName, newItemName);
          result = "";
          if (repeatItem != null)
          {
            result = repeatItem.Name;
          }
          compare = "No Result";
          Program.WriteResult($"{methodName}4", result, compare);
        }

        repeatItem = repeatItemManager.Retrieve(sectionName, newItemName);
        result = "";
        if (repeatItem != null)
        {
          result = repeatItem.Name;
        }
        compare = "No Result";
        Program.WriteResult($"{methodName}5", result, compare);
        break;
      }
    }

    // Deletes all RepeatItems for the named Section.
    private static void DeleteRepeatItems()
    {
      var methodName = "DeleteRepeatItems()";

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

        var repeatItemManager = new RepeatItemManager(sections);
        var sectionName = "Main";
        var itemName = "Item2";
        var repeatItem = new RepeatItem()
        {
          Name = itemName,
        };
        repeatItemManager.Add(sectionName, repeatItem);

        repeatItem = repeatItemManager.Retrieve(sectionName, itemName);
        if (null == repeatItem)
        {
          result = "";
          compare = itemName;
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var repeatItems = repeatItemManager.Load(sectionName);
        if (!LJC.HasListItems(repeatItems))
        {
          result = "";
          compare = "No RepeatItems";
          Program.WriteResult($"{methodName}3", result, compare);
          break;
        }

        // Child replacements must be deleted first.
        var replacementManager = new ReplacementManager(sections);
        foreach (var item in repeatItems)
        {
          replacementManager.DeleteReplacements(sectionName, item.Name);
        }

        // Test Method
        repeatItemManager.DeleteRepeatItems(sectionName);

        repeatItems = repeatItemManager.Load(sectionName);
        if (LJC.HasListItems(repeatItems))
        {
          result = repeatItems.Count.ToString();
          compare = "0";
          Program.WriteResult($"{methodName}4", result, compare);
        }
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

        var repeatItemManager = new RepeatItemManager(sections);
        var sectionName = "Main";

        // Test Method
        var repeatItems = repeatItemManager.Load(sectionName);

        if (!LJC.HasListItems(repeatItems))
        {
          result = "";
          compare = "No RepeatItems";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        var repeatItem = repeatItems[0];
        result = "";
        if (repeatItem != null)
        {
          result = repeatItem.Name;
        }
        compare = "Item1";
        Program.WriteResult($"{methodName}3", result, compare);
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

        var repeatItemManager = new RepeatItemManager(sections);
        var sectionName = "Main";
        var itemName = "Item1";

        // Test Method
        var repeatItem = repeatItemManager.Retrieve(sectionName, itemName);

        result = "";
        if (repeatItem != null)
        {
          result = repeatItem.Name;
        }
        compare = itemName;
        Program.WriteResult($"{methodName}2", result, compare);
        break;
      }
    }
    #endregion
  }
}
