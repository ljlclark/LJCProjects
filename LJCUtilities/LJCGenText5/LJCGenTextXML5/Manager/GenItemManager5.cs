// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenItemManager5.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  public partial class GenDataManager
  {
    #region Methods

    // Adds a RepeatItem record to the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/AddRepeatItem/*'/>
    public void AddRepeatItem(string sectionName, RepeatItem repeatItem)
    {
      if (LJC.HasText(sectionName)
        && repeatItem != null
        && LJC.HasText(repeatItem.Name))
      {
        Section searchSection = GetSection(sectionName);
        if (searchSection != null)
        {
          RepeatItem? searchItem = RetrieveRepeatItem(sectionName
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
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/RetrieveRepeatItem/*'/>
    public RepeatItem? RetrieveRepeatItem(string sectionName, string repeatItemName)
    {
      RepeatItem? retValue = null;

      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName))
      {
        Section section = GetSection(sectionName);
        if (section != null)
        {
          retValue = section.RepeatItems.Retrieve(repeatItemName);
        }
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/LoadRepeatItems/*'/>
    public RepeatItems? LoadRepeatItems(string sectionName)
    {
      RepeatItems? retValue = null;

      if (LJC.HasText(sectionName))
      {
        Section section = GetSection(sectionName);
        if (section != null)
        {
          retValue = section.RepeatItems;
        }
      }
      return retValue;
    }

    // Deletes the RepeatItem record from the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/DeleteRepeatItem/*'/>
    public bool DeleteRepeatItem(string sectionName, string repeatItemName)
    {
      bool retValue = false;

      Section searchSection = GetSection(sectionName);
      if (searchSection != null)
      {
        var searchRepeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (searchRepeatItem != null)
        {
          // Check for child items.
          Replacements replacements = searchRepeatItem.Replacements;
          if (LJC.HasListItems(replacements))
          {
            string errorText = "The RepeatItem cannot be deleted becauses it has child items.";
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
    private RepeatItem? GetRepeatItem(string sectionName, string repeatItemName)
    {
      RepeatItem? retValue = RetrieveRepeatItem(sectionName, repeatItemName);
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
