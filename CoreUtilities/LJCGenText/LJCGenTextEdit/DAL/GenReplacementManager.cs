// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenReplacementManager.cs
using LJCGenTextLib;
using LJCNetCommon;
using System;

namespace LJCGenTextEdit
{
  public partial class GenDataManager
  {
    #region Methods

    // Adds a Replacement record to the object data.
    /// <include path='items/AddReplacement/*' file='Doc/GenReplacementManager.xml'/>
    public void AddReplacement(string sectionName, string repeatItemName
      , Replacement replacement)
    {
      if (NetString.HasValue(sectionName)
        && NetString.HasValue(repeatItemName)
        && replacement != null
        && NetString.HasValue(replacement.Name))
      {
        Section searchSection = GetSection(sectionName);
        if (searchSection != null)
        {
          RepeatItem searchItem = GetRepeatItem(sectionName, repeatItemName);
          if (searchItem != null)
          {
            Replacement searchReplacement = RetrieveReplacement(sectionName
              , repeatItemName, replacement.Name);
            if (searchReplacement != null)
            {
              var errorText = $"Replacement '{replacement.Name}' already exists.";
              throw new InvalidOperationException(errorText);
            }
            else
            {
              searchItem.Replacements.Add(replacement);
            }
          }
        }
      }
    }

    // Retrieves a Replacement record from the object data.
    /// <include path='items/RetrieveReplacement/*' file='Doc/GenReplacementManager.xml'/>
    public Replacement RetrieveReplacement(string sectionName
      , string repeatItemName, string replacementName)
    {
      Replacement retValue = null;

      if (NetString.HasValue(sectionName)
        && NetString.HasValue(repeatItemName)
        && NetString.HasValue(replacementName))
      {
        RepeatItem repeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (repeatItem != null)
        {
          retValue = repeatItem.Replacements.Retrieve(replacementName);
        }
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadReplacements/*' file='Doc/GenReplacementManager.xml'/>
    public Replacements LoadReplacements(string sectionName, string repeatItemName)
    {
      Replacements retValue = null;

      if (NetString.HasValue(sectionName)
        && NetString.HasValue(repeatItemName))
      {
        RepeatItem repeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (repeatItem != null)
        {
          retValue = repeatItem.Replacements;
        }
      }
      return retValue;
    }

    // Delete the Replacement record from the object data.
    /// <include path='items/DeleteReplacement/*' file='Doc/GenReplacementManager.xml'/>
    public bool DeleteReplacement(string sectionName, string repeatItemName
      , string replacementName)
    {
      bool retValue = false;

      Section searchSection = GetSection(sectionName);
      if (searchSection != null)
      {
        RepeatItem searchRepeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (searchRepeatItem != null)
        {
          Replacement replacement = GetReplacement(sectionName, repeatItemName
            , replacementName);
          if (replacement != null)
          {
            Replacements replacements = searchRepeatItem.Replacements;
            retValue = replacements.Remove(replacement);
          }
        }
      }
      return retValue;
    }

    // Get the Replacement object.
    private Replacement GetReplacement(string sectionName, string repeatItemName
      , string replacementName)
    {
      Replacement retValue = RetrieveReplacement(sectionName, repeatItemName
        , replacementName);
      if (null == retValue)
      {
        var errorText = $"The Replacement '{replacementName}' does not exist.";
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }
    #endregion
  }
}
