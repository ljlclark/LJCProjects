// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenType.cs
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenDocLib
{
  // Generates the Class/Type HTML pages.
  /// <include path='items/GenType/*' file='Doc/GenType.xml'/>
  public class GenType
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenTypeC/*' file='Doc/GenType.xml'/>
    public GenType(GenRoot genRoot, GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, LJCAssemblyReflect assemblyReflect)
    {
      GenRoot = genRoot;
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      AssemblyReflect = assemblyReflect;
      HTMLFolderName = GenAssembly.HTMLFolderName;
    }
    #endregion

    #region Public Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{DataAssembly.Name}.{DataType.Name}";
      return retValue;
    }

    // Generates the field pages.
    /// <include path='items/GenFieldPages/*' file='Doc/GenType.xml'/>
    public void GenFieldPages(string[] templateLines)
    {
      GenField genField = new GenField(GenRoot, GenAssembly, DataAssembly
        , DataType, null);
      foreach (DataField dataField in DataType.DataFields)
      {
        genField.DataField = dataField;
        genField.GenFieldPage(AssemblyReflect, templateLines);
      }
    }

    // Generates the method pages.
    /// <include path='items/GenMethodPages/*' file='Doc/GenType.xml'/>
    public void GenMethodPages(string[] templateLines)
    {
      GenMethod genMethod = new GenMethod(GenRoot, GenAssembly, DataAssembly
        , DataType, null);
      foreach (DataMethod dataMethod in DataType.DataMethods)
      {
        genMethod.DataMethod = dataMethod;
        genMethod.GenMethodPage(AssemblyReflect, templateLines);
      }
    }

    // Generates the property pages.
    /// <include path='items/GenPropertyPages/*' file='Doc/GenType.xml'/>
    public void GenPropertyPages(string[] templateLines)
    {
      GenProperty genProperty = new GenProperty(GenRoot, GenAssembly, DataAssembly
        , DataType, null);
      foreach (DataProperty dataProperty in DataType.DataProperties)
      {
        genProperty.DataProperty = dataProperty;
        genProperty.GenPropertyPage(AssemblyReflect, templateLines);
      }
    }

    // Creates an HTML page that lists the methods, properties and fields for
    /// <include path='items/GenTypePage/*' file='Doc/GenType.xml'/>
    public void GenTypePage(string[] templateLines)
    {
      CreateTypeXml createTypeXml = new CreateTypeXml(GenRoot, GenAssembly
        , DataAssembly, DataType, AssemblyReflect);
      string dataFileSpec = $"XMLFiles\\{DataType.Name}.xml";
      string dataXml = createTypeXml.GetXmlData();
      if (!NetString.HasValue(dataXml))
      {
        Console.WriteLine("Missing Type/Class XML Data.");
      }
      else
      {
        // Testing
        //if ("NetCommon" == DataType.Name)
        //{
        //  NetFile.CreateFolder("XMLFiles");
        //  File.WriteAllText(dataFileSpec, dataXml);
        //}

        Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
          , dataXml) as Sections;
        GenerateText generateText = new GenerateText("<!--");
        generateText.Generate(templateLines, sections, dataFileSpec, HTMLFileSpec
          , true);
        ValuesDocGen.Instance.GenPageCount++;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the LJCAssemblyReflect object.</summary>
    public LJCAssemblyReflect AssemblyReflect { get; set; }

    /// <summary>Gets or sets the DataAssembly reference.</summary>
    public DataAssembly DataAssembly { get; set; }

    /// <summary>Gets or sets the DataType object.</summary>
    public DataType DataType
    {
      get
      {
        return mDataType;
      }
      set
      {
        if (value != null)
        {
          mDataType = value;
          HTMLFileName = $"{mDataType.Name}.html";
          HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
        }
      }
    }
    private DataType mDataType;

    /// <summary>Gets or sets the GenAssembly reference.</summary>
    public GenAssembly GenAssembly { get; set; }

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
