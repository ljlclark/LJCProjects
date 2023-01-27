// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FilePaths.cs
using LJCNetCommon;
using System.IO;

namespace LJCGenTextEdit
{
  // Represents the last used file paths.
  /// <include path='items/FilePaths/*' file='Doc/FilePaths.xml'/>
  public class FilePaths
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static FilePaths Deserialize(string fileSpec = null)
    {
      FilePaths retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      if (false == File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        retValue = NetCommon.XmlDeserialize(typeof(FilePaths)
          , fileSpec) as FilePaths;
      }
      return retValue;
    }
    #endregion

    #region Methods

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public void Serialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = DefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Data XML Path.</summary>
    public string DataXMLPath
    {
      get { return mDataXMLPath; }
      set { mDataXMLPath = NetString.InitString(value); }
    }
    private string mDataXMLPath;

    /// <summary>Gets or sets the Output Path.</summary>
    public string OutputPath
    {
      get { return mOutputPath; }
      set { mOutputPath = NetString.InitString(value); }
    }
    private string mOutputPath;

    /// <summary>Gets or sets the Template Path.</summary>
    public string TemplatePath
    {
      get { return mTemplatePath; }
      set { mTemplatePath = NetString.InitString(value); }
    }
    private string mTemplatePath;
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
