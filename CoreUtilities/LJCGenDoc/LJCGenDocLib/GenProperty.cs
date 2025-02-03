// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenProperty.cs
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenDocLib
{
  // Generates the Property HTML pages.
  /// <include path='items/GenProperty/*' file='Doc/GenProperty.xml'/>
  public class GenProperty
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenPropertyC/*' file='Doc/GenProperty.xml'/>
    public GenProperty(GenRoot genRoot, GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, DataProperty dataProperty)
    {
      GenRoot = genRoot;
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      DataProperty = dataProperty;
      HTMLFolderName = Path.Combine(genAssembly.HTMLFolderName, "Properties");
    }
    #endregion

    #region Public Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{DataAssembly.Name}.{DataType.Name}.{DataProperty.Name}";
      return retValue;
    }

    // Creates an HTML page for each Property.
    /// <include path='items/GenPropertyPage/*' file='Doc/GenProperty.xml'/>
    public void GenPropertyPage(LJCAssemblyReflect assemblyReflect
      , string[] templateLines)
    {
      CreatePropertyXml createPropertyXml = new CreatePropertyXml(GenAssembly
        , DataAssembly, DataType, DataProperty, assemblyReflect);
      // *** Next Statement *** Delete // GenText
      //string dataFileSpec = $"XMLFiles\\{DataProperty.Name}.xml";
      string dataXml = createPropertyXml.GetXmlData();
      if (!NetString.HasValue(dataXml))
      {
        Console.WriteLine("Missing Property XML Data.");
      }
      else
      {
        // Testing
        //if ("PublicProperty" == DataType.Name)
        //{
        //  NetFile.CreateFolder("XMLFiles");
        //  File.WriteAllText(dataFileSpec, dataXml);
        //}

        Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
          , dataXml) as Sections;

        // *** Begin *** Delete // GenText
        //GenerateText generateText = new GenerateText("<!--");
        //generateText.Generate(templateLines, sections, dataFileSpec
        //  , HTMLFileSpec, true);
        // *** End   *** Delete

        // *** Begin *** Add // GenText
        // Generate text.
        var textGenLib = new TextGenLib();
        var outputText = textGenLib.TextGen(sections, templateLines);

        NetFile.CreateFolder(HTMLFileSpec);
        File.WriteAllText(HTMLFileSpec, outputText);
        // *** End   *** Add
        ValuesGenDoc.Instance.GenPageCount++;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataAssembly reference.</summary>
    public DataAssembly DataAssembly { get; set; }

    /// <summary>Gets or sets the DataProperty object.</summary>
    public DataProperty DataProperty
    {
      get
      {
        return mDataProperty;
      }
      set
      {
        if (value != null)
        {
          mDataProperty = value;
          HTMLFileName = $"{DataType.Name}.{mDataProperty.Name}.html";
          HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
        }
      }
    }
    private DataProperty mDataProperty;

    /// <summary>Gets or sets the DataType object.</summary>
    public DataType DataType { get; set; }

    /// <summary>Gets the DataRootGen object.</summary>
    public GenRoot GenRoot { get; private set; }

    /// <summary>Gets or sets the GenAssembly reference.</summary>
    public GenAssembly GenAssembly { get; set; }

    /// <summary>Gets or sets the HTML file name value.</summary>
    public string HTMLFileName { get; set; }

    /// <summary>Gets or sets the full HTML file specification value.</summary>
    public string HTMLFileSpec { get; set; }

    /// <summary>Gets or sets the HTML file folder value.</summary>
    public string HTMLFolderName { get; set; }
    #endregion
  }
}
