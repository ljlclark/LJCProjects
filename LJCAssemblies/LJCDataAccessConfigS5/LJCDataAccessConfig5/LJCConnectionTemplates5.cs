// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCConnectionTemplates.cs
using LJCNetCommon5;
using System.Reflection;
using System.Xml.Serialization;

namespace LJCDataAccessConfig5
{
  // Represents a collection of Connection string templates.
  /// <include file='Doc/LJCConnectionTemplates.xml'
  ///  path='members/LJCConnectionTemplates/*'/>
  [XmlRoot("LJCConnectionTemplates")]
  public class LJCConnectionTemplates : List<LJCConnectionTemplate>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCConnectionTemplates.xml'
    ///  path='members/Constructor/*'/>
    public LJCConnectionTemplates()
    {
      mTemplateFileName = "ConnectionTemplates.xml";
      string? localAssembly = Assembly.GetExecutingAssembly().Location;
      if (LJC.HasText(localAssembly))
      {
#pragma warning disable CS8604 // Possible null reference argument.
        TemplateFileSpec = Path.Combine(Path.GetDirectoryName(localAssembly)
            , mTemplateFileName);
#pragma warning restore CS8604 // Possible null reference argument.
      }
    }

    // Loads the config data.
    /// <include file='Doc/LJCConnectionTemplates.xml'
    ///  path='members/LoadData/*'/>
    public void LoadData()
    {
      if (!File.Exists(TemplateFileSpec))
      {
        WriteDefaultData();
      }

      if (LJC.XmlDeserialize(typeof(LJCConnectionTemplates)
        , TemplateFileSpec) is LJCConnectionTemplates connectionTemplates)
      {
        Clear();
        foreach (LJCConnectionTemplate connectionTemplate in connectionTemplates)
        {
          Add(connectionTemplate);
        }
      }
    }
    #endregion

    #region Methods

    // Creates and adds the object from the supplied valus.
    /// <include file='Doc/LJCConnectionTemplates.xml'
    ///  path='members/Add/*'/>
    public LJCConnectionTemplate Add(string name, string template)
    {
      var retValue = new LJCConnectionTemplate()
      {
        Name = name,
        Template = template
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the connection template.
    /// <include file='Doc/LJCConnectionTemplates.xml'
    ///  path='members/Retrieve/*'/>
    public LJCConnectionTemplate? Retrieve(string? name)
    {
      LJCConnectionTemplate? retValue = null;

      if (LJC.HasText(name))
      {
        if (Count != mPrevCount)
        {
          mPrevCount = Count;
          Sort();
        }

        var searchData = new LJCConnectionTemplate()
        {
          Name = name
        };
        int index = BinarySearch(searchData);
        if (index < 0)
        {
          var errorText = $"Connection template '{name}' was not found.";
          throw new Exception(errorText);
        }
        else
        {
          retValue = this[index];
        }
      }
      return retValue;
    }

    // Saves the config data.
    /// <include file='Doc/LJCConnectionTemplates.xml'
    ///  path='members/Save/*'/>
    public void Save()
    {
      LJC.XmlSerialize(GetType(), this, null, TemplateFileSpec);
    }
    #endregion

    #region Private Methods

    // Create the default data file.
    private void WriteDefaultData()
    {
      var tb = new LJCTextBuilder();
      tb.AddLine("<?xml version='1.0'?>");
      tb.AddLine("<LJCConnectionTemplates xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
      tb.AddLine(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
      tb.AddLine("  <LJCConnectionTemplate>");
      tb.AddLine("    <Name>SQLServer</Name>");
      tb.AddLine("    <Template>Data Source={DbServer}; Initial Catalog={Database};");
      tb.AddLine("     Integrated Security=True</Template>");
      tb.AddLine("  </LJCConnectionTemplate>");
      tb.AddLine("  <LJCConnectionTemplate>");
      tb.AddLine("    <Name>MySQL</Name>");
      tb.AddLine("    <Template>server={DbServer}; UserId={UID}; Password={PSWD};");
      tb.AddLine("     database={Database}</Template>");
      tb.AddLine("  </LJCConnectionTemplate>");
      tb.AddLine("  <LJCConnectionTemplate>");
      tb.AddLine("    <Name>OLEDB</Name>");
      tb.AddLine("    <Template>Provider=SQLOLEDB; Data Source={DbServer}\\instance;");
      tb.AddLine("     Initial Catalog={Database}; User Id={UID};");
      tb.AddLine("     Password={PSWD}</Template>");
      tb.AddLine("  </LJCConnectionTemplate>");
      tb.AddLine("  <LJCConnectionTemplate>");
      tb.AddLine("    <Name>Access</Name>");
      tb.AddLine("    <Template>Provider=Microsoft.ACE.OLEDB.12.0;");
      tb.AddLine("     Data Source=C:\\myAccessFile.accdb; Persist Security Info=False;</Template>");
      tb.AddLine("  </LJCConnectionTemplate>");
      tb.AddLine("  <LJCConnectionTemplate>");
      tb.AddLine("    <Name>ODBC</Name>");
      tb.AddLine("    <Template>Driver={SQL Server}; Server=myServerAddress;");
      tb.AddLine("     Database=myDataBase; Uid ={UID}; Pwd={PSWD};</Template>");
      tb.AddLine("  </LJCConnectionTemplate>");
      tb.AddLine("</LJCConnectionTemplates>");
      var templates = tb.ToString();
      if (LJC.HasText(TemplateFileSpec))
      {
        File.WriteAllText(TemplateFileSpec, templates);
      }
    }
    #endregion

    #region Properties

    // The configuration file path.
    /// <include file='Doc/LJCConnectionTemplates.xml'
    ///  path='members/TemplateFileSpec/*'/>
    public string? TemplateFileSpec { get; private set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private readonly string mTemplateFileName;
    #endregion
  }
}
