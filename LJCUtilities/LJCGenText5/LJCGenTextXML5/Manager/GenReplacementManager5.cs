// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenReplacementManager.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  public partial class GenDataManager
  {
    #region Methods

    // Adds a Replacement record to the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/AddReplacement/*'/>
    public void AddReplacement(string sectionName, string repeatItemName
      , Replacement replacement)
    {
      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName)
        && replacement != null
        && LJC.HasText(replacement.Name))
      {
        Section searchSection = GetSection(sectionName);
        if (searchSection != null)
        {
          var searchItem = GetRepeatItem(sectionName, repeatItemName);
          if (searchItem != null)
          {
            var searchReplacement = RetrieveReplacement(sectionName
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
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/RetrieveReplacement/*'/>
    public Replacement? RetrieveReplacement(string sectionName
      , string repeatItemName, string replacementName)
    {
      Replacement? retValue = null;

      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName)
        && LJC.HasText(replacementName))
      {
        var repeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (repeatItem != null)
        {
          retValue = repeatItem.Replacements.Retrieve(replacementName);
        }
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/LoadReplacements/*'/>
    public Replacements? LoadReplacements(string sectionName, string repeatItemName)
    {
      Replacements? retValue = null;

      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName))
      {
        var repeatItem = GetRepeatItem(sectionName, repeatItemName);
        if (repeatItem != null)
        {
          retValue = repeatItem.Replacements;
        }
      }
      return retValue;
    }

    // Delete the Replacement record from the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/DeleteReplacement/*'/>
    public bool DeleteReplacement(string sectionName, string repeatItemName
      , string replacementName)
    {
      bool retValue = false;

      Section searchSection = GetSection(sectionName);
      if (searchSection != null)
      {
        var searchRepeatItem = GetRepeatItem(sectionName, repeatItemName);
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
      var retValue = RetrieveReplacement(sectionName, repeatItemName
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
