// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLBuilder.cs
using LJCNetCommon;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace LJCDataUtility
{
  /// <summary>
  /// Provides methods for creating XML text.
  /// </summary>
  public class XMLBuilder
  {
    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    public XMLBuilder()
    {
      Indent = 0;
      XML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
    }

    #region Public Methods

    // Retrieves the XML text.
    /// <include path='items/ToString/*' file='Doc/XMLBuilder.xml'/>
    public override string ToString()
    {
      return XML;
    }

    // Creates the element begin tag.
    /// <include path='items/Begin/*' file='Doc/XMLBuilder.xml'/>
    public string Begin(string name
      , string text = null
      , XMLAttributes xmlAttributes = null)
    {
      return CreateElement(name, text, xmlAttributes
        , false);
    }

    // Creates an XML element.
    /// <include path='items/Element/*' file='Doc/XMLBuilder.xml'/>
    public string Element(string name
      , string text = null
      , XMLAttributes xmlAttributes = null)
    {
      return CreateElement(name, text, xmlAttributes);
    }

    // Creates the element end tag.
    /// <include path='items/End/*' file='Doc/XMLBuilder.xml'/>
    public string End(string name)
    {
      string retEnd;

      var builder = new StringBuilder(128);
      if (XML.Length > 0)
      {
        builder.AppendLine();
      }
      if (Indent > 0)
      {
        Indent--;
      }
      if (Indent > 0)
      {
        builder.Append(new string(' ', Indent * 2));
      }
      builder.Append($"<\\{name}>");
      retEnd = builder.ToString();
      XML += retEnd;
      return retEnd;
    }

    // Creates the XML start attributes.
    /// <include path='items/StartAttributes/*' file='Doc/XMLBuilder.xml'/>
    public XMLAttributes StartAttributes()
    {
      var retAttributes = new XMLAttributes();
      retAttributes.Add("xmlns:xsd"
        , "http://www.w3.org/2001/XMLSchema");
      retAttributes.Add("xmlns:xsi"
        , "http://www.w3.org/2001/XMLSchema-instance");
      return retAttributes;
    }
    #endregion

    #region Private Methods

    // Creates the XML element.
    private string CreateElement(string name
      , string text = null
      , XMLAttributes xmlAttributes = null
      , bool close = true)
    {
      var builder = new StringBuilder(128);
      if (XML.Length > 0)
      {
        builder.AppendLine();
      }
      if (Indent > 0)
      {
        builder.Append(new string(' ', Indent * 2));
      }
      builder.Append($"<{name}");
      if (NetCommon.HasItems(xmlAttributes))
      {
        foreach (XMLAttribute xmlAttribute in xmlAttributes)
        {
          if ("xmlns:xsi" == xmlAttribute.Name)
          {
            builder.AppendLine();
          }
          builder.Append($" {xmlAttribute.Name}");
          if (NetString.HasValue(xmlAttribute.Value))
          {
            builder.Append($"=\"{xmlAttribute.Value}\"");
          }
        }
      }
      builder.Append(">");
      if (NetString.HasValue(text))
      {
        builder.Append(text);
      }
      if (close)
      {
        builder.Append($"<\\{name}>");
      }
      string retElement = builder.ToString();
      XML += retElement;
      if (false == close)
      {
        Indent++;
      }
      return retElement;
    }
    #endregion

    #region Properties

    // Gets or sets the current indent value.
    private int Indent { get; set; }

    // Gets or sets the XML text.
    private string XML { get; set; }
    #endregion
  }
}