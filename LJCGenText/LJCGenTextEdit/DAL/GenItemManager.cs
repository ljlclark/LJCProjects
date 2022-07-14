// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
// GenItemManager.cs
using LJCGenTextLib;
using LJCNetCommon;
using System;

namespace LJCGenTextEdit
{
  public partial class GenDataManager
  {
    #region Methods

    // Adds a RepeatItem record to the object data.
    /// <include path='items/AddRepeatItem/*' file='Doc/GenItemManager.xml'/>
    public void AddRepeatItem(string sectionName, RepeatItem repeatItem)
    {
      if (NetString.HasValue(sectionName)
        && repeatItem != null
        && NetString.HasValue(repeatItem.Name))
      {
        Section searchSection = GetSection(sectionName);
        if (searchSection != null)
        {
          RepeatItem searchItem = RetrieveRepeatItem(sectionName
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
    /// <include path='items/RetrieveRepeatItem/*' file='Doc/GenItemManager.xml'/>
    public RepeatItem RetrieveRepeatItem(string sectionName, string repeatItemName)
    {
      RepeatItem retValue = null;

      if (NetString.HasValue(sectionName)
        && NetString.HasValue(repeatItemName))
      {
        Section section = GetSection(sectionName);
        if (section != null)
        {
          retValue = section.RepeatItems.LJCSearchName(repeatItemName);
        }
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadRepeatItems/*' file='Doc/GenItemManager.xml'/>
    public RepeatItems LoadRepeatItems(string sectionName)
    {
      RepeatItems retValue = null;

      if (NetString.HasValue(sectionName))
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
    /// <include path='items/DeleteRepeatItem/*' file='Doc/GenItemManager.xml'/>
    public bool DeleteRepeatItem(string sectionName, string repeatItemName)
    {
      bool retValue = false;

      Section searchSection = GetSection(sectionName);
      if (searchSection != null)
      {
        RepeatItem searchRepeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (searchRepeatItem != null)
        {
          // Check for child items.
          Replacements replacements = searchRepeatItem.Replacements;
          if (replacements != null
            && replacements.Count > 0)
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
    private RepeatItem GetRepeatItem(string sectionName, string repeatItemName)
    {
      RepeatItem retValue = RetrieveRepeatItem(sectionName, repeatItemName);
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
