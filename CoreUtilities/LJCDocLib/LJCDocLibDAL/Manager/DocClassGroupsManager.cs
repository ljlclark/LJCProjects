// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocClassGroupsManager.cs
using LJCNetCommon;
using System.IO;

namespace LJCDocLibDAL
{
  /// <summary>
  /// 
  /// </summary>
  public class DocClassGroupsManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClassGroupsManager()
    {
      FileName = "LJCCodeDocClasses.xml";
    }
    #endregion

    #region Load/Retrieve Methods

    #region Group

    // Deletes a DocClassGroup and all associated DocClasses.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyObject"></param>
    public void DeleteGroup(DocClassGroup keyObject)
    {
      if (keyObject != null && keyObject.Name != null)
      {
        if (null == DocClassGroups)
        {
          Load();
        }

        var searchRecord = DocClassGroups.LJCSearchName(keyObject.Name);
        if (searchRecord != null)
        {
          DocClassGroups.Remove(searchRecord);
        }
      }
    }

    // Loads a collection of data records from the XML file.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="xmlFileName"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public DocClassGroups Load(string xmlFileName = null)
    {
      DocClassGroups retValue;

      if (false == string.IsNullOrWhiteSpace(xmlFileName))
      {
        FileName = xmlFileName;
      }

      if (File.Exists(FileName))
      {
        retValue = NetCommon.XmlDeserialize(typeof(DocClassGroups)
          , FileName) as DocClassGroups;
      }
      else
      {
        string errorText = $"File '{FileName}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      retValue.LJCSortBySequence(new DocClassGroupSequenceComparer());
      DocClassGroups = retValue;
      return retValue;
    }

    // Retrieves a data record with the supplied value.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DocClassGroup SearchName(string name)
    {
      DocClassGroup retValue;

      if (null == DocClassGroups)
      {
        Load();
      }
      retValue = DocClassGroups.LJCSearchName(name);
      return retValue;
    }
    #endregion

    #region Class

    // Deletes a DocClass entry.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="docClassGroupName"></param>
    /// <param name="keyObject"></param>
    public void DeleteClass(string docClassGroupName, DocClass keyObject)
    {
      DocClassGroup searchRecord;
      DocClasses docClasses;
      DocClass classRecord;

      if (keyObject != null && keyObject.Name != null)
      {
        if (null == DocClassGroups)
        {
          Load();
        }

        searchRecord = DocClassGroups.LJCSearchName(docClassGroupName);
        if (searchRecord != null)
        {
          docClasses = searchRecord.DocClasses;
          classRecord = docClasses.LJCSearchName(keyObject.Name);
          if (classRecord != null)
          {
            docClasses.Remove(classRecord);
          }
        }
      }
    }

    // Loads the group class data records.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public DocClasses LoadClasses(string groupName)
    {
      DocClasses retValue = null;

      var docClassGroup = SearchName(groupName);
      if (docClassGroup != null)
      {
        retValue = docClassGroup.DocClasses;
      }
      retValue.LJCSortBySequence(new DocClassSequenceComparer());
      return retValue;
    }

    // Retrieves a data record with the supplied values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="docClassGroupName"></param>
    /// <param name="docClassName"></param>
    /// <returns></returns>
    public DocClass SearchNameClass(string docClassGroupName
      , string docClassName)
    {
      DocClass retValue = null;

      var docClassGroup = SearchName(docClassGroupName);
      if (docClassGroup != null)
      {
        var docClasses = docClassGroup.DocClasses;
        if (docClasses != null)
        {
          retValue = docClasses.LJCSearchName(docClassName);
        }
      }
      return retValue;
    }
    #endregion

    // Saves the updated XML file.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="xmlFileName"></param>
    /// <returns></returns>
    public bool Save(string xmlFileName = null)
    {
      string fileName;
      bool retValue = true;

      fileName = xmlFileName;
      if (string.IsNullOrWhiteSpace(fileName))
      {
        fileName = FileName;
      }

      if (null == DocClassGroups)
      {
        Load();
      }

      foreach (DocClassGroup docGroup in DocClassGroups)
      {
        var docClasses = docGroup.DocClasses;
        if (null == docGroup.DocClasses)
        {
          docGroup.DocClasses = new DocClasses();
          docClasses = docGroup.DocClasses;
        }
        docClasses.LJCSortBySequence(new DocClassSequenceComparer());
      }
      DocClassGroups.LJCSortBySequence(new DocClassGroupSequenceComparer());

      NetCommon.XmlSerialize(typeof(DocClassGroups), DocClassGroups, null
        , fileName);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DocGenGroups value.</summary>
    public DocClassGroups DocClassGroups { get; set; }

    /// <summary>Gets or sets the File name.</summary>
    public string FileName { get; set; }
    #endregion
  }
}
