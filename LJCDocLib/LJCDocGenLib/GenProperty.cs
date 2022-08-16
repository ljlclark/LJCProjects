// GenProperty.cs
using LJCDocObjLib;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCDocGenLib
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
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
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
      string dataFileSpec = $"XMLFiles\\{DataProperty.Name}.xml";
      string dataXml = createPropertyXml.GetXmlData();
      if (false == NetString.HasValue(dataXml))
      {
        Console.WriteLine("Missing Method XML Data.");
      }
      else
      {
        // Testing
        if ("PublicProperty" == DataType.Name)
        {
          //File.WriteAllText(dataFileSpec, dataXml);
        }

        Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
          , dataXml) as Sections;
        GenerateText generateText = new GenerateText();
        generateText.Generate(templateLines, sections, dataFileSpec, HTMLFileSpec
          , true);
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
          OverriddenName = mDataProperty.OverriddenName;
          HTMLFileName = $"{DataType.Name}.{OverriddenName}.html";
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

    /// <summary>The overriden method unique name if required.</summary>
    public string OverriddenName
    {
      get { return mOverriddenName; }
      set { mOverriddenName = NetString.InitString(value); }
    }
    private string mOverriddenName;
    #endregion
  }
}
