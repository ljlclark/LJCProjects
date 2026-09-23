// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestReplacementManager5.cs
using LJCGenTextXAL5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides ReplacementManager specific test methods.
  internal class TestReplacementManager
  {
    // Initializes an object instance.
    public TestReplacementManager()
    {
      // Constructor Methods
      Constructor();

      // Data Methods
      Add();
      Delete();
      DeleteReplacements();
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

        // Test Method
        var replacementManager = new ReplacementManager(sections);

        // Set test values.
        var sectionName = "Main";
        var itemName = "Item1";
        var replacementName = "_Namespace_";

        // Verify data was loaded.
        var replacement = replacementManager.Retrieve(sectionName, itemName
          , replacementName);
        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = replacementName;
        Program.WriteResult($"{methodName}2", result, compare);
        break;
      }
    }
    #endregion

    #region Data Methods

    // Adds an item to the object data.
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

        // Create the data manager.
        var replacementManager = new ReplacementManager(sections);

        // Create a new Replacement.
        var newReplacementName = "_OneMore_";
        var replacement = new Replacement()
        {
          Name = newReplacementName,
        };

        // Set test values.
        var sectionName = "Main";
        var itemName = "Item1";

        // Test Method
        replacementManager.Add(sectionName, itemName, replacement);

        // Verify Replacement was added.
        replacement = replacementManager.Retrieve(sectionName, itemName
          , newReplacementName);
        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = newReplacementName;
        Program.WriteResult($"{methodName}2", result, compare);
        break;
      }
    }

    // Deletes an item from the object data.
    private static void Delete()
    {
      var methodName = "Delete()";

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

        // Create the data manager.
        var replacementManager = new ReplacementManager(sections);

        // Set test values.
        var sectionName = "Main";
        var itemName = "Item1";

        // Add a new Replacement.
        var newReplacementName = "_NewOne_";
        var replacement = new Replacement()
        {
          Name = newReplacementName,
        };
        replacementManager.Add(sectionName, itemName, replacement);

        // Verify new Replacement was added.
        replacement = replacementManager.Retrieve(sectionName, itemName
          , newReplacementName);
        if (null == replacement)
        {
          result = "";
          compare = newReplacementName;
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Test Method
        if (!replacementManager.Delete(sectionName, itemName
          , newReplacementName))
        {
          // Verify error where Replacement was not deleted.
          replacement = replacementManager.Retrieve(sectionName, itemName
            , newReplacementName);
          result = "";
          if (replacement != null)
          {
            result = replacement.Name;
          }
          compare = "No Result";
          Program.WriteResult($"{methodName}4", result, compare);
        }

        // Verify Replacement was deleted.
        replacement = replacementManager.Retrieve(sectionName, itemName
          , newReplacementName);
        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = "No Result";
        Program.WriteResult($"{methodName}5", result, compare);
        break;
      }
    }

    // Deletes all RepeatItems for the named Section.
    private static void DeleteReplacements()
    {
      var methodName = "DeleteReplacements()";

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

        // Create the data manager.
        var replacementManager = new ReplacementManager(sections);

        // Set test values.
        var sectionName = "Main";
        var itemName = "Item1";

        // Add a new Replacement.
        var replacementName = "_NewOne_";
        var replacement = new Replacement()
        {
          Name = replacementName,
        };
        replacementManager.Add(sectionName, itemName, replacement);

        // Verify new Replacement was added.
        replacement = replacementManager.Retrieve(sectionName, itemName
          , replacementName);
        if (null == replacement)
        {
          result = "";
          compare = "replacementName";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Test Method
        replacementManager.DeleteReplacements(sectionName, itemName);

        // Verify all replacements were deleted.
        var replacements = replacementManager.Load(sectionName, itemName);
        if (LJC.HasListItems(replacements))
        {
          result = replacements.Count.ToString();
          compare = "0";
          Program.WriteResult($"{methodName}4", result, compare);
        }
        break;
      }
    }

    // Retrieves a collection of items.
    private static void Load()
    {
      var methodName = "Load()";

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

        // Create the data manager.
        var replacementManager = new ReplacementManager(sections);

        // Set test values.
        var sectionName = "Main";
        var repeatItemName = "Item1";

        // Test Method
        var replacements = replacementManager.Load(sectionName, repeatItemName);

        // verify the data was loaded.
        if (!LJC.HasListItems(replacements))
        {
          result = "";
          compare = "No Replacements";
          Program.WriteResult($"{methodName}2", result, compare);
          break;
        }

        // Verify the first item is "_Namespace_".
        var replacement = replacements[0];
        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = "_Namespace_";
        Program.WriteResult($"{methodName}3", result, compare);
        break;
      }
    }

    // Retrieves an item from the object data.
    private static void Retrieve()
    {
      var methodName = "Retrieve()";

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

        // Create the data manager.
        var replacementManager = new ReplacementManager(sections);

        // Set test values.
        var sectionName = "Main";
        var itemName = "Item1";
        var replacementName = "_Namespace_";

        // Test Method
        var replacement = replacementManager.Retrieve(sectionName, itemName
          , replacementName);

        // Verify retrieved item.
        result = "";
        if (replacement != null)
        {
          result = replacement.Name;
        }
        compare = replacementName;
        Program.WriteResult($"{methodName}2", result, compare);
        break;
      }
    }
    #endregion
  }
}
