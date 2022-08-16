// GenRootNew.cs
using LJCGenTextLib;
using LJCNetCommon;
using System.IO;

namespace LJCDocGenLib
{
  public partial class GenRoot
  {
    #region Public Methods

    // Creates an HTML page that lists the assemblies.
    /// <include path='items/GenRootPage/*' file='Doc/GenRoot.xml'/>
    public void GenRootPageNew()
    {
      CreateRootXml createRootXml = new CreateRootXml(DataRoot.DocGenGroups);
      string dataXml = createRootXml.GetXmlData();
      string dataFileSpec = "XMLFiles\\Root.xml";
      //File.WriteAllText(dataFileSpec, dataXml);  // Testing
      Sections sections = NetCommon.XmlDeserializeMessage(typeof(Sections)
        , dataXml) as Sections;

      GenerateText generateText = new GenerateText();
      string[] templateLines = File.ReadAllLines("Templates\\RootTemplate.htm");
      generateText.Generate(templateLines, sections, dataFileSpec, HTMLFileSpec
        , true);
    }
    #endregion
  }
}
