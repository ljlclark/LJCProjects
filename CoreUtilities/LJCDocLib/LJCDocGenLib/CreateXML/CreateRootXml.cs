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
    public CreateRootXml(DocAssemblyGroups docGenGroups)
    {
      AssemblyGroups = docGenGroups;
      Managers = ValuesDocGen.Instance.Managers;
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
      foreach (DocAssemblyGroup assemblyGroup in AssemblyGroups)
      {
        repeatItem = section.RepeatItems.Add(assemblyGroup.Name);
        replacements = repeatItem.Replacements;
        replacements.Add("_GroupDescription_", assemblyGroup.Heading);

        var assemblyManager = Managers.DocAssemblyManager;
        var docAssemblies = assemblyManager.LoadWithParent(assemblyGroup.ID);
        foreach (DocAssembly docAssembly in docAssemblies)
        {
          repeatItem = assemblySection.RepeatItems.Add("");
          replacements = repeatItem.Replacements;
          replacements.Add("_AssemblyName_", docAssembly.Name);
          replacements.Add("_AssemblyDescription_", docAssembly.Description);
        }
      }
      retValue = NetCommon.XmlSerializeToString(sections.GetType()
        , sections, null);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Returns the assembly count.
    private int AssemblyCount()
    {
      int retValue = 0;

      foreach (DocAssemblyGroup assemblyGroup in AssemblyGroups)
      {
        var assemblyManager = Managers.DocAssemblyManager;
        var docAssemblies = assemblyManager.Load();
        foreach (DocAssembly docAssembly in docAssemblies)
        {
          retValue++;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the AssemblyGroups reference.
    private DocAssemblyGroups AssemblyGroups { get; }

    /// <summary>Gets or sets the Managers object.</summary>
    public ManagersDocGen Managers { get; set; }
    #endregion
  }
}
