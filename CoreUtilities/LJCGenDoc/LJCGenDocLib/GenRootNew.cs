﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenRootNew.cs
using LJCGenTextLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDocLib
{
  public partial class GenRoot
  {
    #region Public Methods

    // Creates an HTML page that lists the assemblies.
    /// <include path='items/GenRootPage/*' file='Doc/GenRoot.xml'/>
    public void GenRootPageNew()
    {
      CreateRootXml createRootXml = new CreateRootXml(DataRoot.AssemblyGroups);
      string dataXml = createRootXml.GetXmlData();
      // *** Next Statement *** Delete // GenText
      //string dataFileSpec = "XMLFiles\\Root.xml";
      //File.WriteAllText(dataFileSpec, dataXml);  // Testing
      Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
        , dataXml) as Sections;

      // *** Begin *** Delete // GenText
      //GenerateText generateText = new GenerateText("<!--");
      //string[] templateLines = File.ReadAllLines("Templates\\RootTemplate.htm");
      //generateText.Generate(templateLines, sections, dataFileSpec
      //  , HTMLFileSpec, true);
      // *** End   *** Delete

      // *** Begin *** Add // GenText
      string[] templateLines = File.ReadAllLines("Templates\\RootTemplate.htm");
      // Generate text.
      var textGenLib = new TextGenLib();
      var outputText = textGenLib.TextGen(sections, templateLines);

      File.WriteAllText(HTMLFileSpec, outputText);
      // *** End   *** Add
    }
    #endregion
  }
}
