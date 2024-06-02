// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextData.js
// <script src="ArgErr.js"></script>

// Generate output text from a template and data.
class TextData
{
  // 
  static CreateDataText(sections)
  {
    let retValue = "";

    retValue += `Section Name\r\n`;
    for (let index = 0; index < sections.Count(); index++)
    {
      let section = sections.Items(index);
      retValue += `${section.Name}\r\n`;
      retValue += TextData.RepeatItems(section);
    }
    return retValue;
  }

  // 
  static RepeatItems(section)
  {
    let retValue = "";

    let items = section.RepeatItems;
    retValue += `Item Name\r\n`;
    for (let index = 0; index < items.Count(); index++)
    {
      let item = items.Items(index);
      retValue += `${item.Name}\r\n`;
      retValue += TextData.Replacements(item);
    }
    return retValue;
  }

  // 
  static Replacements(repeatItem)
  {
    let retValue = "";

    let replacements = repeatItem.Replacements;
    retValue += `Replacement Name, Value\r\n`;
    for (let index = 0; index < replacements.Count(); index++)
    {
      let replacement = replacements.Items(index);
      retValue += `${replacement.Name}, ${replacement.Value}\r\n`;
    }
    return retValue;
  }

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