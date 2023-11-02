// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenMethod.cs
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCDocGenLib
{
  // Generates the Method HTML pages.
  /// <include path='items/GenMethod/*' file='Doc/GenMethod.xml'/>
  public class GenMethod
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenMethodC/*' file='Doc/GenMethod.xml'/>
    public GenMethod(GenRoot genRoot, GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, DataMethod dataMethod)
    {
      GenRoot = genRoot;
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      DataMethod = dataMethod;
      HTMLFolderName = Path.Combine(genAssembly.HTMLFolderName, "Methods");
    }
    #endregion

    #region Public Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{DataAssembly.Name}.{DataType.Name}.{DataMethod.Name}";
      return retValue;
    }

    // Creates an HTML page for each Method.
    /// <include path='items/GenMethodPage/*' file='Doc/GenMethod.xml'/>
    public void GenMethodPage(LJCAssemblyReflect assemblyReflect
      , string[] templateLines)
    {
      bool gen = true;
      if (DataMethod.Summary != null
        && "nogen" == DataMethod.Summary.ToLower())
      {
        gen = false;
      }
      if (true == gen)
      {
        CreateMethodXml createMethodXml = new CreateMethodXml(GenAssembly
          , DataAssembly, DataType, DataMethod, assemblyReflect);
        string dataFileSpec = $"XMLFiles\\{DataMethod.Name}.xml";
        string dataXml = createMethodXml.GetXmlData();
        if (false == NetString.HasValue(dataXml))
        {
          Console.WriteLine("Missing Method XML Data.");
        }
        else
        {
          // Testing
          //if ("GenAssemblyPage" == DataType.Name)
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
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the LJCAssemblyReflect object.</summary>
    public LJCAssemblyReflect AssemblyReflect { get; set; }

    /// <summary>Gets or sets the DataAssembly reference.</summary>
    public DataAssembly DataAssembly { get; set; }

    /// <summary>Gets or sets the DataMethod object.</summary>
    public DataMethod DataMethod
    {
      get
      {
        return mDataMethod;
      }
      set
      {
        if (value != null)
        {
          mDataMethod = value;
          // *** Begin *** Change - 9/27/23 #Overload
          var name = mDataMethod.OverloadName;
          if (null == name)
          {
            name = mDataMethod.Name;
          }
          HTMLFileName = $"{DataType.Name}.{name}.html";
          // *** End   *** Change - 9/27/23 #Overload
          HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
        }
      }
    }
    private DataMethod mDataMethod;

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

    /// <summary>Gets or sets the public flag.</summary>
    public bool IsPublic { get; set; }
    #endregion
  }
}

