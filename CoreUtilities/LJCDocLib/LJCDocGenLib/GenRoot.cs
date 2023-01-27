// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenRoot.cs
using LJCDocLibDAL;
using LJCDocObjLib;
using LJCNetCommon;
using System;
using System.IO;
using System.Text;

namespace LJCDocGenLib
{
  // Generates the Root HTML page.
  /// <include path='items/GenRoot/*' file='Doc/GenRoot.xml'/>
  public partial class GenRoot
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenRootC/*' file='Doc/GenRoot.xml'/>
    public GenRoot(DataRoot dataRoot, string targetPath)
    {
      DataRoot = dataRoot;
      HTMLFolderName = targetPath;
      HTMLFileName = "CodeDoc.html";
      HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
    }
    #endregion

    #region Static Methods

    // Logs the missing values.
    /// <include path='items/LogMissing/*' file='Doc/GenRoot.xml'/>
    public static void LogMissing(string itemName, string itemTypeName
      , string itemValue)
    {
      string logName = "Missing.txt";
      string text;

      if (false == File.Exists(logName))
      {
        File.WriteAllText(logName, "");
      }

      // "Summary", "Type", DataType.Name
      text = $"{itemName} - {itemTypeName} '{itemValue}'\r\n";
      File.AppendAllText(logName, text);
    }
    #endregion

    #region Public Methods

    // Generates the child assembly page and class/type pages.
    /// <include path='items/GenAssemblyPages/*' file='Doc/GenRoot.xml'/>
    public void GenAssemblyPages()
    {
      GenAssembly genAssembly;

      string templateFileSpec = "Templates\\AssemblyTemplate.htm";
      string[] templateLines = ReadAllLines(templateFileSpec);

      foreach (DataAssembly dataAssembly in DataRoot.DataAssemblies)
      {
        genAssembly = new GenAssembly(this, dataAssembly);
        genAssembly.GenAssemblyPage(dataAssembly, templateLines);
        genAssembly.GenTypePages();
      }
    }

    // Creates an HTML page that lists the assemblies.
    /// <include path='items/GenRootPage/*' file='Doc/GenRoot.xml'/>
    public void GenRootPage()
    {
      StringBuilder builder = new StringBuilder(128);

      builder.Append(CreateHTMLSection(HTMLSection.Head));
      foreach (DocGenGroup docGenGroup in DataRoot.DocGenGroups)
      {
        builder.Append(CreateGroupSection(HTMLSection.Head, docGenGroup.Description));
        foreach (DocGenAssembly config in docGenGroup.DocGenAssemblies)
        {
          builder.Append(CreateAssemblySection(config.Description));
        }
        builder.Append(CreateGroupSection(HTMLSection.Tail));
      }
      builder.Append(CreateHTMLSection(HTMLSection.Tail, DataRoot.DataAssemblies.Count));

      string htmlText = builder.ToString();
      WriteFile(htmlText);
    }
    #endregion

    #region Private Methods

    // Creates the assembly section.
    // ToDo: Convert to generate from template?
    private string CreateAssemblySection(string description)
    {
      StringBuilder builder = new StringBuilder(128);
      string retValue = null;

      // Create relative path.
      DataAssembly dataAssembly = DataRoot.DataAssemblies.LJCSearchByDescription(description);
      if (dataAssembly != null)
      {
        GenAssembly dataAssemblyGen = new GenAssembly(this, dataAssembly);
        string assemblyPageName = $"{dataAssembly.Name}.html";
        string assemblyPageSpec;
        if (false == NetString.HasValue(dataAssemblyGen.HTMLFileName))
        {
          assemblyPageSpec = assemblyPageName;
        }
        else
        {
          assemblyPageSpec = $@"HTML/{dataAssembly.Name}/{assemblyPageName}";
        }

        string name = dataAssembly.Name;
        builder.AppendLine("      <tr>");
        builder.AppendLine($"        <td id='{name}' class='ListTable' width='25%'>");
        builder.AppendLine($"          <a href='{assemblyPageSpec}'>{name}</a>");
        builder.AppendLine("        </td>");
        builder.AppendLine($"        <td class='ListTable'>{description}</td>");
        builder.AppendLine("      </tr>");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates the group section.
    // ToDo: Convert to generate from template?
    private string CreateGroupSection(HTMLSection section, string description = null)
    {
      StringBuilder builder = new StringBuilder(128);
      string retValue;

      switch (section)
      {
        case HTMLSection.Head:
          builder.AppendLine("    <tr class='ListTable'>");
          builder.AppendLine($"      <td class='ListTable' colspan='2'>{description}</td>");
          builder.AppendLine("    </tr>");
          builder.AppendLine("    <tr>");
          builder.AppendLine("      <td colspan='2'>");
          builder.AppendLine("        <table class='ListTable'>");
          break;

        case HTMLSection.Tail:
          builder.AppendLine("        </table>");
          builder.AppendLine("      </td>");
          builder.AppendLine("    </tr>");
          break;
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Creates the HTML file section.
    // ToDo: Convert to generate from template?
    private string CreateHTMLSection(HTMLSection section, int assemblyCount = 0)
    {
      StringBuilder builder = new StringBuilder(128);
      string retValue;

      switch (section)
      {
        case HTMLSection.Head:
          builder.AppendLine("<!DOCTYPE html>");
          builder.AppendLine("<!-- Copyright(c) Lester J. Clark 2021,2022 - All Rights Reserved -->");
          builder.AppendLine("<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>");
          builder.AppendLine("<head>");
          builder.AppendLine("  <title>LJC Assemblies</title>");
          builder.AppendLine("  <meta charset='utf-8' />");
          builder.AppendLine("  <meta name='author' content='Lester J. Clark' />");
          builder.AppendLine("  <meta name='viewport' content='width=device-width initial-scale=1' />");
          builder.AppendLine("  <link rel='stylesheet' type='text/css' href='CSS/CodeDoc.css' />");
          builder.AppendLine("</head>");
          builder.AppendLine("<body>");
          builder.AppendLine("  <div class=\"page\">");
          builder.AppendLine("  <br />");
          builder.AppendLine("  <div id='Header'>");
          builder.AppendLine("    <div id='Title'>LJC Assemblies</div>");
          builder.AppendLine("  </div>");
          builder.AppendLine("  <br />");
          builder.AppendLine("  <div class='Text'>");
          builder.AppendLine("    <p>");
          builder.AppendLine("      The LJC Libraries, Utilities and Applications are a set of .NET Assemblies");
          builder.AppendLine("      that were developed to work together with the following objectives:");
          builder.AppendLine("    </p>");
          builder.AppendLine("    <ol>");
          builder.AppendLine("      <li>Shorten the development time for building .NET Business Database Applications.</li>");
          builder.AppendLine("      <li>");
          builder.AppendLine("        Provide consistant, reusable code designed to emphasize readability and");
          builder.AppendLine("        simplified maintenance.");
          builder.AppendLine("      </li>");
          builder.AppendLine("      <li>Provide a powerful and flexible set of database libraries.</li>");
          builder.AppendLine("    </ol>");
          builder.AppendLine("    <p>");
          builder.AppendLine("      The Database Libraries were designed to support the following features:");
          builder.AppendLine("    </p>");
          builder.AppendLine("    <ol>");
          builder.AppendLine("      <li>");
          builder.AppendLine("        Provide consistant, reusable code that simplifies database access and");
          builder.AppendLine("        encapsulates database access best practices.");
          builder.AppendLine("      </li>");
          builder.AppendLine("      <li>");
          builder.AppendLine("        Switch between the SQL Client, ODBC, OLEDB and MySQL data providers with only");
          builder.AppendLine("        configuration file changes.");
          builder.AppendLine("      </li>");
          builder.AppendLine("      <li>");
          builder.AppendLine("        Scale between Client/Server, Local Message Based, Remote Data Service");
          builder.AppendLine("        or Web Data Service data access with only configuration changes.");
          builder.AppendLine("      </li>");
          builder.AppendLine("    </ol>");
          builder.AppendLine("    <p>");
          builder.AppendLine("      Most LJC Solutions, Projects and output files are prefixed with \"LJC\" to distinguish");
          builder.AppendLine("      them from other potentially similar named items and help prevent naming conflicts.");
          builder.AppendLine("    </p>");
          builder.AppendLine("  </div>");
          builder.AppendLine("  <br />");
          builder.AppendLine("  <table class='ListTable'>");
          break;

        case HTMLSection.Tail:
          builder.AppendLine("  </table>");
          builder.AppendLine($"  <div class='Text'>{assemblyCount} Assemblies</div>");
          builder.AppendLine("  <br />");
          builder.AppendLine("  <div class='SmallText'>R=Remark, D=Description, O=Object Pages, G=Object Graph, E=Example</div>");
          builder.AppendLine("  <br />");
          builder.AppendLine("  </div>");
          builder.AppendLine("</body>");
          builder.AppendLine("</html>");
          break;
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Read the text lines from fileSpec.
    private string[] ReadAllLines(string fileSpec)
    {
      string[] retValue;

      retValue = NetFile.ReadAllLines(fileSpec);
      return retValue;
    }

    // Write the file.
    private void WriteFile(string htmlText)
    {
      if (false == Directory.Exists(HTMLFolderName))
      {
        Directory.CreateDirectory(HTMLFolderName);
      }
      Console.WriteLine($"Generating: {HTMLFileSpec}");
      File.WriteAllText(HTMLFileSpec, htmlText);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataRoot value.</summary>
    public DataRoot DataRoot { get; set; }

    /// <summary>Gets or sets the HTML file name value.</summary>
    public string HTMLFileName { get; set; }

    /// <summary>Gets or sets the full HTML file specification value.</summary>
    public string HTMLFileSpec { get; set; }

    /// <summary>Gets or sets the HTML file folder value.</summary>
    public string HTMLFolderName { get; set; }
    #endregion

    #region Class Data

    private enum HTMLSection
    {
      Head,
      Tail
    }
    #endregion
  }
}
