// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenAssembly.cs
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenDocLib
{
  // Generates the Assembly HTML pages.
  /// <include path='items/GenAssembly/*' file='Doc/ProjectDocGenLib.xml'/>
  public class GenAssembly
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenAssemblyC/*' file='Doc/GenAssembly.xml'/>
    public GenAssembly(GenRoot genRoot, DataAssembly dataAssembly)
    {
      GenRoot = genRoot;
      DataAssembly = dataAssembly;
      if (NetString.HasValue(dataAssembly.XmlFileSpec))
      {
        AssemblyReflect = DataAssembly.AssemblyReflect;
        HTMLFolderName = Path.Combine(genRoot.HTMLFolderName
          , $@"HTML\{DataAssembly.Name}");
        HTMLFileName = $"{DataAssembly.Name}.html";
        HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
      }
    }
    #endregion

    #region Methods

    // Creates an HTML page that lists the classes/types in an assembly.
    /// <include path='items/GenAssemblyPage/*' file='Doc/GenAssembly.xml'/>
    public void GenAssemblyPage(DataAssembly dataAssembly
      , string[] templateLines)
    {
      if (dataAssembly.DataTypes != null)
      {
        Console.WriteLine($"Generating Assembly Page: {HTMLFileSpec}");
        CreateAssemblyXml createAssemblyXml
          = new CreateAssemblyXml(dataAssembly);
        string dataFileSpec = $"XMLFiles\\{dataAssembly.Name}.xml";
        string dataXml = createAssemblyXml.GetXmlData();
        if (false == NetString.HasValue(dataXml))
        {
          Console.WriteLine("Missing Assembly XML Data.");
        }
        else
        {
          // Testing
          //if ("LJCNetCommon" == dataAssembly.Name)
          //{
          //  NetFile.CreateFolder("XMLFiles");
          //  File.WriteAllText(dataFileSpec, dataXml);
          //}

          Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
            , dataXml) as Sections;
          GenerateText generateText = new GenerateText("<!--");
          generateText.Generate(templateLines, sections, dataFileSpec
            , HTMLFileSpec, true);
          ValuesDocGen.Instance.GenPageCount++;
        }
      }
    }

    // Generates the class/type pages.
    /// <include path='items/GenTypePages/*' file='Doc/GenAssembly.xml'/>
    public void GenTypePages()
    {
      if (DataAssembly.DataTypes != null)
      {
        string[] typeLines = ReadAllLines("Templates\\TypeTemplate.html");
        string[] methodLines = ReadAllLines("Templates\\MethodTemplate.html");
        string[] propertyLines = ReadAllLines("Templates\\PropertyTemplate.html");
        string[] fieldLines = ReadAllLines("Templates\\FieldTemplate.html");

        GenType genType = new GenType(GenRoot, this, DataAssembly, null
          , AssemblyReflect);
        foreach (DataType dataType in DataAssembly.DataTypes)
        {
          genType.DataType = dataType;
          if (null == dataType.AssemblyReflect)
          {
            Console.WriteLine($"XML file '{DataAssembly.XmlFileSpec}' was not found.");
          }
          else
          {
            genType.GenTypePage(typeLines);
            genType.GenMethodPages(methodLines);
            genType.GenPropertyPages(propertyLines);
            genType.GenFieldPages(fieldLines);
          }
        }
      }
    }

    // Read the text lines from fileSpec.
    private string[] ReadAllLines(string fileSpec)
    {
      string[] retValue;

      retValue = NetFile.ReadAllLines(fileSpec);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the AssemblyReflect value.</summary>
    public LJCAssemblyReflect AssemblyReflect { get; set; }

    /// <summary>Gets the DataAssembly object.</summary>
    public DataAssembly DataAssembly { get; private set; }

    /// <summary>Gets the DataRootGen object.</summary>
    public GenRoot GenRoot { get; private set; }

    /// <summary>Gets or sets the HTML file name value.</summary>
    public string HTMLFileName { get; set; }

    /// <summary>Gets or sets the full HTML file specification value.</summary>
    public string HTMLFileSpec { get; set; }

    /// <summary>Gets or sets the HTML file folder value.</summary>
    public string HTMLFolderName { get; set; }
    #endregion
  }
}
