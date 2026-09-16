// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ReplacementManager.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  // Provides Replacement specific XML data manipulation methods.
  /// <include file='Doc/ReplacementManager5.xml'
  ///  path='items/ReplacementManager/*'/>
  public partial class ReplacementManager
  {
    #region Properties

    private SectionManager SectionManager { get; set; } = null!;

    private RepeatItemManager RepeatItemManager { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/RepeatItemManager5.xml'
    ///  path='items/ParamConstructor/*'/>
    public ReplacementManager(SectionManager sectionManager
      , RepeatItemManager repeatItemManager)
    {
      SectionManager = sectionManager;
      RepeatItemManager = repeatItemManager;
    }
    #endregion

    #region Methods

    // Adds a Replacement record to the object data.
    /// <include file='Doc/ReplacementManager5.xml'
    ///  path='items/Add/*'/>
    public void Add(string sectionName, string repeatItemName
      , Replacement replacement)
    {
      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName)
        && replacement != null
        && LJC.HasText(replacement.Name))
      {
        Section? searchSection = SectionManager.Retrieve(sectionName);
        if (searchSection != null)
        {
          var searchItem = RepeatItemManager.Retrieve(sectionName, repeatItemName);
          if (searchItem != null)
          {
            var searchReplacement = Retrieve(sectionName, repeatItemName
              , replacement.Name);
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
    /// <include file='Doc/ReplacementManager5.xml'
    ///  path='items/Retrieve/*'/>
    public Replacement? Retrieve(string sectionName
      , string repeatItemName, string replacementName)
    {
      Replacement? retValue = null;

      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName)
        && LJC.HasText(replacementName))
      {
        var repeatItem = RepeatItemManager.Retrieve(sectionName, repeatItemName);
        if (repeatItem != null)
        {
          retValue = repeatItem.Replacements.Retrieve(replacementName);
        }
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include file='Doc/ReplacementManager5.xml'
    ///  path='items/Load/*'/>
    public Replacements? Load(string sectionName, string repeatItemName)
    {
      Replacements? retValue = null;

      if (LJC.HasText(sectionName)
        && LJC.HasText(repeatItemName))
      {
        var repeatItem = RepeatItemManager.Retrieve(sectionName, repeatItemName);
        if (repeatItem != null)
        {
          retValue = repeatItem.Replacements;
        }
      }
      return retValue;
    }

    // Delete the Replacement record from the object data.
    /// <include file='Doc/ReplacementManager5.xml'
    ///  path='items/Delete/*'/>
    public bool Delete(string sectionName, string repeatItemName
      , string replacementName)
    {
      bool retValue = false;

      Section? searchSection = SectionManager.Retrieve(sectionName);
      if (searchSection != null)
      {
        var searchRepeatItem = RepeatItemManager.Retrieve(sectionName
          , repeatItemName);
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
      var retValue = Retrieve(sectionName, repeatItemName
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
