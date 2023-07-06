// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Doc.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation data.
  /// <include path='items/Doc/*' file='Doc/ProjectDocXMLObjLib.xml'/>
  [XmlRoot("doc")]
  public class Doc
  {
    // T - Type
    // M - Method
    // P - Property
    // F - Field
    // E - Event

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="namespaceValue"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public DocMembers GetFields(string namespaceValue, string typeName)
    {
      // Get all methods for this type.
      string prefix = $"F:{namespaceValue}.{typeName}";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="namespaceValue"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public DocMembers GetMethods(string namespaceValue, string typeName)
    {
      // Get all methods for this type.
      string prefix = $"M:{namespaceValue}.{typeName}";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="namespaceValue"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public DocMembers GetProperties(string namespaceValue, string typeName)
    {
      string prefix = $"P:{namespaceValue}.{typeName}";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DocMembers GetTypes()
    {
      string prefix = "T:";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    // Convert list to collection and sort by Name.
    private DocMembers CreateCollection(string prefix)
    {
      var members = DocMembers.FindAll(x => x.Name.StartsWith(prefix));
      var retValue = new DocMembers();
      retValue.AddFromList(members);
      retValue.Sort();
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The assembly information.</summary>
    [XmlElement("assembly")]
    public DocAssembly DocAssembly { get; set; }

    // The documentation members.
    /// <include path='items/DocMembers/*' file='Doc/Doc.xml'/>
    [XmlArray("members")]
    public DocMembers DocMembers { get; set; }
    #endregion
  }
}
