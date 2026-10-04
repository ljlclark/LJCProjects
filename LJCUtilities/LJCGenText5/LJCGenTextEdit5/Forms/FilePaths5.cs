// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FilePaths5.cs
using LJCNetCommon5;

namespace LJCGenTextEdit5
{
  // Represents the last used file paths.
  /// <include path='items/FilePaths/*' file='Doc/FilePaths.xml'/>
  public class FilePaths
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static FilePaths Deserialize(string? fileSpec = null)
    {
      FilePaths retValue = new();

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = DefaultFileName;
      }

      FilePaths? filePaths = null;
      if (File.Exists(fileSpec))
      {
        filePaths = LJC.XmlDeserialize(typeof(FilePaths)
          , fileSpec) as FilePaths;
        if (filePaths != null)
        {
          retValue = filePaths;
        }
      }
      if (null == filePaths)
      {
        retValue = new FilePaths()
        {
          TemplatePath = "Templates",
          DataXMLPath = "DataXML",
          OutputPath = "Output",
        };
      }
      return retValue;
    }
    #endregion

    #region Methods

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public void Serialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Data XML Path.</summary>
    public string DataXMLPath
    {
      get { return mDataXMLPath; }
      set { mDataXMLPath = value.Trim(); }
    }
    private string mDataXMLPath = null!;

    /// <summary>Gets or sets the Output Path.</summary>
    public string OutputPath
    {
      get { return mOutputPath; }
      set { mOutputPath = value.Trim(); }
    }
    private string mOutputPath = null!;

    /// <summary>Gets or sets the Template Path.</summary>
    public string TemplatePath
    {
      get { return mTemplatePath; }
      set { mTemplatePath = value.Trim(); }
    }
    private string mTemplatePath = null!;
    #endregion

    #region Class Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string DefaultFileName
    {
      get { return "FilePaths.xml"; }
    }
    #endregion
  }
}
