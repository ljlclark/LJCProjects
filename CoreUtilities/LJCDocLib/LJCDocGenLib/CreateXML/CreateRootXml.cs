// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateRootXml.cs
using System;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocLibDAL;

namespace LJCDocGenLib
{
  /// <summary>Creates the Root Data values.</summary>
  public class CreateRootXml
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CreateRootXmlC/*' file='Doc/CreateRootXml.xml'/>
    public CreateRootXml(DocGenGroups docGenGroups)
    {
      DocGenGroups = docGenGroups;
    }
    #endregion

    #region Public Methods
    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreateRootXml.xml'/>
    public string GetXmlData()
    {
      Section section;
      RepeatItem repeatItem;
      Replacements replacements;
      string retValue;

      Sections sections = new Sections();
      section = sections.Add("Main");
      repeatItem = section.RepeatItems.Add("Main");
      replacements = repeatItem.Replacements;
      replacements.Add("_Title_", "Assembly Groups");
      string assemblyCount = AssemblyCount().ToString();
      replacements.Add("_AssemblyCount_", assemblyCount);
      var copyRight = "Copyright &copy Lester J. Clark and Contributors.<br />\r\n";
      copyRight += "Licensed under the MIT License.";
      replacements.Add("_Copyright_", copyRight);

      Section assemblySection = sections.Add("Assembly");
      section = sections.Add("Group");
      foreach (DocGenGroup docGenGroup in DocGenGroups)
      {
        repeatItem = section.RepeatItems.Add(docGenGroup.Name);
        replacements = repeatItem.Replacements;
        replacements.Add("_GroupDescription_", docGenGroup.Description);
        foreach (DocGenAssembly docGenAssembly in docGenGroup.DocGenAssemblies)
        {
          repeatItem = assemblySection.RepeatItems.Add("");
          replacements = repeatItem.Replacements;
          replacements.Add("_AssemblyName_", docGenAssembly.Name);
          replacements.Add("_AssemblyDescription_", docGenAssembly.Description);
        }
      }
      retValue = NetCommon.XmlSerializeToString(sections.GetType()
        , sections, null);
      return retValue;
    }
    #endregion

    #region Private Methods

    //
    private int AssemblyCount()
    {
      int retValue = 0;

      foreach (DocGenGroup docGenGroup in DocGenGroups)
      {
        foreach (DocGenAssembly docGenAssembly in docGenGroup.DocGenAssemblies)
        {
          retValue++;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    private DocGenGroups DocGenGroups { get; }
    #endregion
  }
}
