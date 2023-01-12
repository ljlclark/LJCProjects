// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocGenGroupManager.cs
using LJCNetCommon;
using System.IO;

namespace LJCDocLibDAL
{
  // Provides DocGenGroup specific data manipulation methods.
  /// <include path='items/DocGenGroupManager/*' file='Doc/DocGenGroupManager.xml'/>
  public class DocGenGroupManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocGenGroupManager()
    {
      FileName = "LJCCodeDoc.xml";
    }
    #endregion

    #region Retrieve/Load Methods

    #region Group

    // Deletes a DocGenGroup and all associated DocGenAssemblies.
    /// <include path='items/DeleteGroup/*' file='Doc/DocGenGroupManager.xml'/>
    public void DeleteGroup(DocGenGroup keyObject)
    {
      if (keyObject != null && keyObject.Name != null)
      {
        if (null == DocGenGroups)
        {
          Load();
        }

        DocGenGroup searchRecord = DocGenGroups.LJCSearchName(keyObject.Name);
        if (searchRecord != null)
        {
          DocGenGroups.Remove(searchRecord);
        }
      }
    }

    // Loads a collection of data records from the XML file.
    /// <include path='items/Load/*' file='Doc/DocGenGroupManager.xml'/>
    public DocGenGroups Load(string xmlFileName = null)
    {
      DocGenGroups retValue;

      if (false == string.IsNullOrWhiteSpace(xmlFileName))
      {
        FileName = xmlFileName;
      }

      if (File.Exists(FileName))
      {
        retValue = NetCommon.XmlDeserialize(typeof(DocGenGroups)
          , FileName) as DocGenGroups;
      }
      else
      {
        string errorText = $"File '{FileName}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue.LJCSortBySequence(new DocGenGroupSequenceComparer());
      DocGenGroups = retValue;
      return retValue;
    }

    // Retrieves a data record with the supplied value.
    /// <include path='items/RetrieveWithName/*' file='Doc/DocGenGroupManager.xml'/>
    public DocGenGroup SearchName(string name)
    {
      DocGenGroup retValue;

      if (null == DocGenGroups)
      {
        Load();
      }
      retValue = DocGenGroups.LJCSearchName(name);
      return retValue;
    }
    #endregion

    #region Assembly

    // Deletes a DocGenAssembly entry.
    /// <include path='items/DeleteAssembly/*' file='Doc/DocGenGroupManager.xml'/>
    public void DeleteAssembly(string docGenGroupName, DocGenAssembly keyObject)
    {
      DocGenGroup searchRecord;
      DocGenAssemblies docAssemblies;
      DocGenAssembly assemblyRecord;

      if (keyObject != null && keyObject.Name != null)
      {
        if (null == DocGenGroups)
        {
          Load();
        }

        searchRecord = DocGenGroups.LJCSearchName(docGenGroupName);
        if (searchRecord != null)
        {
          docAssemblies = searchRecord.DocGenAssemblies;
          assemblyRecord = docAssemblies.LJCSearchName(keyObject.Name);
          if (assemblyRecord != null)
          {
            docAssemblies.Remove(assemblyRecord);
          }
        }
      }
    }

    // Loads the group assembly data records.
    /// <include path='items/LoadAssemblies/*' file='Doc/DocGenGroupManager.xml'/>
    public DocGenAssemblies LoadAssemblies(string groupName)
    {
      DocGenAssemblies retValue = null;

      DocGenGroup docGenGroup = SearchName(groupName);
      if (docGenGroup != null)
      {
        retValue = docGenGroup.DocGenAssemblies;
      }
      retValue.LJCSortBySequence(new DocAssemblySequenceComparer());
      return retValue;
    }

    // Retrieves a data record with the supplied values.
    /// <include path='items/RetrieveAssemblyWithName/*' file='Doc/DocGenGroupManager.xml'/>
    public DocGenAssembly SearchNameAssembly(string docGroupName
      , string docAssemblyName)
    {
      DocGenAssembly retValue = null;

      DocGenGroup docGenGroup = SearchName(docGroupName);
      if (docGenGroup != null)
      {
        DocGenAssemblies docGenAssemblies = docGenGroup.DocGenAssemblies;
        if (docGenAssemblies != null)
        {
          retValue = docGenAssemblies.LJCSearchName(docAssemblyName);
        }
      }
      return retValue;
    }
    #endregion

    // Saves the updated XML file.
    /// <include path='items/Save/*' file='Doc/DocGenGroupManager.xml'/>
    public bool Save(string xmlFileName = null)
    {
      string fileName;
      bool retValue = true;

      fileName = xmlFileName;
      if (string.IsNullOrWhiteSpace(fileName))
      {
        fileName = FileName;
      }

      if (null == DocGenGroups)
      {
        Load();
      }

      foreach (DocGenGroup docGroup in DocGenGroups)
      {
        DocGenAssemblies docAssemblies = docGroup.DocGenAssemblies;
        if (null == docGroup.DocGenAssemblies)
        {
          docGroup.DocGenAssemblies = new DocGenAssemblies();
          docAssemblies = docGroup.DocGenAssemblies;
        }
        docAssemblies.LJCSortBySequence(new DocAssemblySequenceComparer());
      }
      DocGenGroups.LJCSortBySequence(new DocGenGroupSequenceComparer());

      NetCommon.XmlSerialize(typeof(DocGenGroups), DocGenGroups, null
        , fileName);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DocGenGroups value.</summary>
    public DocGenGroups DocGenGroups { get; set; }

    /// <summary>Gets or sets the File name.</summary>
    public string FileName { get; set; }
    #endregion
  }
}
