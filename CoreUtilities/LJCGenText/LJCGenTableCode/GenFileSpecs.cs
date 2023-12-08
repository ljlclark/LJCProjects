// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenFileSpecs.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCGenTableCode
{
  // Represents a collection of GenFileSpec objects.
  /// <include path='items/GenFileSpecs/*' file='Doc/GenFileSpecs.xml'/>
  [XmlRoot("GenFileSpecs")]
  public class GenFileSpecs : List<GenFileSpec>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public static GenFileSpecs LJCDeserialize(string fileSpec = null)
    {
      GenFileSpecs retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (File.Exists(LJCDefaultFileName))
      {
        retValue = NetCommon.XmlDeserialize(typeof(GenFileSpecs), fileSpec)
          as GenFileSpecs;
      }
      else
      {
        retValue = new GenFileSpecs
        {
          { "Data", "Templates\\DataTemplate.cs", "DataFiles\\*.cs"
            , "XMLFiles\\{0}.xml" },
          { "Collection", "Templates\\CollectionTemplate.cs", "DataFiles\\*.cs"
            , "XMLFiles\\{0}.xml", true },
          { "Manager", "Templates\\ObjectManagerTemplate.cs", "ManagerFiles\\*.cs"
            , "XMLFiles\\{0}Manager.xml" },
          { "Detail", "Templates\\DetailTemplate.cs", "FormFiles\\*.cs"
            , "XMLFiles\\{0}Detail.xml" },
          { "Detail", "Templates\\DetailTemplate.Designer.cs","FormFiles\\*.cs"
            , "XMLFiles\\{0}Detail.Designer.xml"  },
          { "Module", "Templates\\ModuleTemplate.cs", "FormFiles\\*.cs"
            , "XMLFiles\\{0}Module.xml" },
          { "List", "Templates\\ListTemplate.cs", "FormFiles\\*.cs"
            , "XMLFiles\\{0}List.xml" }
        };
        NetCommon.XmlSerialize(typeof(GenFileSpecs), retValue, null, fileSpec);
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public GenFileSpecs()
    {
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/GenFileSpecs.xml'/>
    public GenFileSpec Add(string fileTypeName, string templateFileSpec
      , string outputFileSpec, string xmlFormat, bool isPlural = false)
    {
      GenFileSpec retValue;

      string message = "";
      NetString.AddMissingArgument(message, fileTypeName);
      NetString.AddMissingArgument(message, templateFileSpec);
      NetString.AddMissingArgument(message, outputFileSpec);
      NetString.AddMissingArgument(message, xmlFormat);
      NetString.ThrowInvalidArgument(message);

      retValue = new GenFileSpec
      {
        FileTypeName = fileTypeName,
        TemplateFileSpec = templateFileSpec,
        OutputFileSpec = outputFileSpec,
        XMLFormat = xmlFormat,
        IsPlural = isPlural
      };
      Add(retValue);
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataOutputFileSpec value.</summary>
    public string DataOutputFileSpec { get; set; }

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "GenFileSpecs.xml"; }
    }
    #endregion
  }
}
