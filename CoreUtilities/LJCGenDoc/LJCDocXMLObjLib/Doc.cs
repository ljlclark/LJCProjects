// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Doc.cs
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation data.
  /// <include path='items/Doc/*' file='Doc/ProjectDocXMLObjLib.xml'/>
  [XmlRoot("doc")]
  public class Doc
  {
    #region Static Functions

    // Retrieves the member name from the DocMember name.
    /// <include path='items/GetMemberName/*' file='Doc/Doc.xml'/>
    public static string GetMemberName(string docMemberName, string prefix = null)
    {
      string retValue = null;

      if (prefix != null)
      {
        retValue = docMemberName.Substring(prefix.Length + 1);
        int index = retValue.IndexOf('(');
        if (index > -1)
        {
          // Strip arguments.
          retValue = retValue.Substring(0, index);
        }
      }
      else
      {
        int index = docMemberName.IndexOf('(');
        if (index > -1)
        {
          string text = docMemberName.Substring(0, index);
          index = text.LastIndexOf('.');
          retValue = text.Substring(index + 1);
        }
        else
        {
          index = docMemberName.LastIndexOf('.');
          if (index > -1)
          {
            retValue = docMemberName.Substring(index + 1);
          }
        }
      }
      return retValue;
    }

    // Retrieves the namespace from the DocMember name.
    /// <include path='items/GetNamespace/*' file='Doc/Doc.xml'/>
    public static string GetNamespace(string docMemberName)
    {
      string retValue = null;

      int index = docMemberName.IndexOf('(');
      if (index > -1)
      {
        string text = docMemberName.Substring(0, index);
        index = text.LastIndexOf('.');
        retValue = text.Substring(2, index - 2);
      }
      else
      {
        index = docMemberName.LastIndexOf('.');
        if (index > -1)
        {
          retValue = docMemberName.Substring(2, index - 2);
        }
      }
      return retValue;
    }

    // Gets the Method name.
    /// <include path='items/MethodName/*' file='Doc/Doc.xml'/>
    public string MethodName(string methodMemberName)
    {
      string prefix = $"M:{TypeName}";
      var retValue = Doc.GetMemberName(methodMemberName, prefix);
      return retValue;
    }
    #endregion

    #region Public Methods

    // Gets the Field members for the current Type member.
    /// <include path='items/GetFields/*' file='Doc/Doc.xml'/>
    public DocMembers GetFields()
    {
      // Get all methods for this type.
      string prefix = $"F:{TypeName}";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    // Gets the Method members for the current Type member.
    /// <include path='items/GetMethods/*' file='Doc/Doc.xml'/>
    public DocMembers GetMethods()
    {
      // Get all methods for this type.
      string prefix = $"M:{TypeName}";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    // Gets the Property members for the current Type member.
    /// <include path='items/GetProperties/*' file='Doc/Doc.xml'/>
    public DocMembers GetProperties()
    {
      string prefix = $"P:{TypeName}";
      var retValue = CreateCollection(prefix);
      return retValue;
    }

    // Gets the Type members for the current Doc assembly.
    /// <include path='items/GetTypes/*' file='Doc/Doc.xml'/>
    public DocMembers GetTypes()
    {
      string prefix = "T:";
      var retValue = CreateCollection(prefix, false);
      return retValue;
    }

    // Convert list to collection and sort by Name.
    private DocMembers CreateCollection(string prefix
      , bool checkContained = true)
    {
      var docMembers = DocMembers.FindAll(x => x.Name.StartsWith(prefix));

      var retValue = new DocMembers();
      if (checkContained)
      {
        foreach (DocMember docMember in docMembers)
        {
          // Make sure prefix is not part of a contained type.
          // If "." then only matches beginning of memberName.
          var docMemberName = GetMemberName(docMember.Name, prefix);
          if (-1 == docMemberName.IndexOf('.'))
          {
            retValue.Add(docMember);
          }
        }
      }
      else
      {
        retValue.AddFromList(docMembers);
      }
      retValue.Sort();
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The assembly information.</summary>
    [XmlElement("assembly")]
    public DocXMLAssembly DocAssembly { get; set; }

    // The documentation members.
    /// <include path='items/DocMembers/*' file='Doc/Doc.xml'/>
    [XmlArray("members")]
    public DocMembers DocMembers { get; set; }

    /// <summary>The current type name.</summary>
    [XmlIgnore()]
    public string TypeName { get; set; }
    #endregion
  }
}
