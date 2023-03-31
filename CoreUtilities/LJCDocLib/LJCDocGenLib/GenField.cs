// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenField.cs
using LJCDocLibDAL;
using LJCDocObjLib;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCDocGenLib
{
  // Generates the Field HTML pages.
  /// <include path='items/GenField/*' file='Doc/GenField.xml'/>
  public class GenField
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenFieldC/*' file='Doc/GenField.xml'/>
    public GenField(GenRoot genRoot, GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, DataField dataField)
    {
      GenRoot = genRoot;
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      DataField = dataField;
      HTMLFolderName = Path.Combine(genAssembly.HTMLFolderName, "Fields");
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{DataAssembly.Name}.{DataType.Name}.{DataField.Name}";
      return retValue;
    }

    // Creates an HTML page for each property.
    /// <include path='items/GenFieldPage/*' file='Doc/GenField.xml'/>
    public void GenFieldPage(LJCAssemblyReflect assemblyReflect
      , string[] templateLines)
    {
      CreateFieldXml createFieldXml = new CreateFieldXml(GenAssembly
        , DataAssembly, DataType, DataField, assemblyReflect);
      string dataFileSpec = $"XMLFiles\\{DataField.Name}.xml";
      string dataXml = createFieldXml.GetXmlData();
      if (false == NetString.HasValue(dataXml))
      {
        Console.WriteLine("Missing Field XML Data.");
      }
      else
      {
        // Testing
        //if ("PublicField" == DataType.Name)
        //{
        //  /File.WriteAllText(dataFileSpec, dataXml);
        //}

        Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
          , dataXml) as Sections;
        GenerateText generateText = new GenerateText();
        generateText.Generate(templateLines, sections, dataFileSpec, HTMLFileSpec
          , true);
        ValuesDocGen.Instance.GenPageCount++;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataAssembly reference.</summary>
    public DataAssembly DataAssembly { get; set; }

    /// <summary>Gets or sets the DataField object.</summary>
    public DataField DataField
    {
      get
      {
        return mDataField;
      }
      set
      {
        if (value != null)
        {
          mDataField = value;
          HTMLFileName = $"{DataType.Name}.{mDataField.Name}.html";
          HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
        }
      }
    }
    private DataField mDataField;

    /// <summary>Gets or sets the DataType object.</summary>
    public DataType DataType { get; set; }

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
