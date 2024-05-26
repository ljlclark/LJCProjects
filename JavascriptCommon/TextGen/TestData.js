// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestData.js
// <script src="ArgErr.js"></script>

// Generate output text from a template and data.
class TestData
{
  // Creates the Sections Data
  static SectionsData()
  {
    let retValue = new Sections();
    let section = retValue.Add("Main");

    let repeatItems = section.RepeatItems;
    let repeatItem = repeatItems.Add("First");

    let replacements = repeatItem.Replacements;
    replacements.Add("_CollectionName_", "GenItems")
    replacements.Add("_ClassName_", "GenItem")
    return retValue;
  }
}