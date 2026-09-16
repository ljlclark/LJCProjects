// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItemManager5.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  // Provides RepeatItem specific XML data manipulation methods.
  /// <include file='Doc/RepeatItemManager5.xml'
  ///  path='items/RepeatItemManager/*'/>
  public class RepeatItemManager
  {
    #region Properties

    // Gets or sets the SectionManager reference.
    private SectionManager SectionManager { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/RepeatItemManager5.xml'
    ///  path='items/ParamConstructor/*'/>
    public RepeatItemManager(Sections sections)
    {
      if (sections != null)
      {
        SectionManager = new SectionManager(sections);
      }
    }
    #endregion

    #region Methods

    // Adds a RepeatItem record to the object data.
    /// <include file='Doc/RepeatItemManager5.xml'
    ///  path='items/Add/*'/>
    public void Add(string sectionName, RepeatItem repeatItem)
    {
      if (LJC.HasText(sectionName)
        && repeatItem != null
        && LJC.HasText(repeatItem.Name))
      {
        Section? searchSection = SectionManager.Retrieve(sectionName);
        if (searchSection != null)
        {
          RepeatItem? searchItem = Retrieve(sectionName
            , repeatItem.Name);
          if (searchItem != null)
          {
            var errorText = $"RepeatItem '{repeatItem.Name}' already exists.";
            throw new InvalidOperationException(errorText);
          }
          else
          {
            searchSection.RepeatItems.Add(repeatItem);
          }
        }
      }
    }

    // Retrieves a RepeatItem record from the object data.
    /// <include file='Doc/RepeatItemManager5.xml'
    ///  path='items/Retrieve/*'/>
    public RepeatItem? Retrieve(string sectionName, string repeatItemName)
    {
      RepeatItem? retValue = null;

      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName))
      {
        Section? section = SectionManager.Retrieve(sectionName);
        if (section != null)
        {
          retValue = section.RepeatItems.Retrieve(repeatItemName);
        }
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include file='Doc/RepeatItemManager5.xml'
    ///  path='items/Load/*'/>
    public RepeatItems? Load(string sectionName)
    {
      RepeatItems? retValue = null;

      if (LJC.HasText(sectionName))
      {
        Section? section = SectionManager.Retrieve(sectionName);
        if (section != null)
        {
          retValue = section.RepeatItems;
        }
      }
      return retValue;
    }

    // Deletes the RepeatItem record from the object data.
    /// <include file='Doc/RepeatItemManager5.xml'
    ///  path='items/Delete/*'/>
    public bool Delete(string sectionName, string repeatItemName)
    {
      bool retValue = false;

      Section? searchSection = SectionManager.Retrieve(sectionName);
      if (searchSection != null)
      {
        var searchRepeatItem = GetItem(sectionName, repeatItemName);
        if (searchRepeatItem != null)
        {
          // Check for child items.
          Replacements replacements = searchRepeatItem.Replacements;
          if (LJC.HasListItems(replacements))
          {
            string errorText = "The RepeatItem cannot be deleted becauses it has";
            errorText += " child items.";
            throw new InvalidOperationException(errorText);
          }
          else
          {
            RepeatItems repeatItems = searchSection.RepeatItems;
            retValue = repeatItems.Remove(searchRepeatItem);
          }
        }
      }
      return retValue;
    }

    // Get the RepeatItem object.
    private RepeatItem? GetItem(string sectionName, string repeatItemName)
    {
      RepeatItem? retValue = Retrieve(sectionName, repeatItemName);
      if (null == retValue)
      {
        var errorText = $"The RepeatItem '{repeatItemName}' was not found.";
        throw new InvalidOperationException(errorText);
      }
      return retValue;
    }
  }
  #endregion
}
