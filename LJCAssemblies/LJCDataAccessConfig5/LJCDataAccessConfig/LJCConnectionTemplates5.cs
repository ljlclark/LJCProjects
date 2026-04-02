// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCConnectionTemplates5.cs
using LJCNetCommon5;
using System.Reflection;

namespace LJCDataAccessConfig5
{
  // Represents a collection of Connection string templates.
  /// <include path="members/LJCConnectionTemplates/*" file="Doc/LJCConnectionTemplates.xml"/>
  public class LJCConnectionTemplates5 : List<LJCConnectionTemplate5>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCConnectionTemplates.xml"/>
    public LJCConnectionTemplates5()
    {
      mTemplateFileName = "ConnectionTemplates.xml";
      string? localAssembly = Assembly.GetExecutingAssembly().Location;
      if (LJC5.HasValue(localAssembly))
      {
#pragma warning disable CS8604 // Possible null reference argument.
        TemplateFileSpec = Path.Combine(Path.GetDirectoryName(localAssembly)
            , mTemplateFileName);
#pragma warning restore CS8604 // Possible null reference argument.
      }
    }

    // Loads the config data.
    /// <include path="members/LoadData/*" file="Doc/LJCConnectionTemplates.xml"/>
    public void LoadData()
    {
      if (!File.Exists(TemplateFileSpec))
      {
        WriteDefaultData();
      }

      if (LJC5.XmlDeserialize(typeof(LJCConnectionTemplates5)
        , TemplateFileSpec) is LJCConnectionTemplates5 connectionTemplates)
      {
        Clear();
        foreach (LJCConnectionTemplate5 connectionTemplate in connectionTemplates)
        {
          Add(connectionTemplate);
        }
      }
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided valus.
    /// <include path="members/Add/*" file="Doc/LJCConnectionTemplates.xml"/>
    public LJCConnectionTemplate5 Add(string name, string template)
    {
      var retValue = new LJCConnectionTemplate5()
      {
        Name = name,
        Template = template
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the data configuration.
    /// <include path="members/Retrieve/*" file="Doc/LJCConnectionTemplates.xml"/>
    public LJCConnectionTemplate5? Retrieve(string? name)
    {
      LJCConnectionTemplate5? retValue = null;

      if (LJC5.HasValue(name))
      {
        if (Count != mPrevCount)
        {
          mPrevCount = Count;
          Sort();
        }

        var searchData = new LJCConnectionTemplate5()
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
    /// <include path="members/Save/*" file="Doc/LJCConnectionTemplates.xml"/>
    public void Save()
    {
      LJC5.XmlSerialize(GetType(), this, null, TemplateFileSpec);
    }
    #endregion

    #region Private Methods

    // Create the default data file.
    private void WriteDefaultData()
    {
      var tb = new LJCTextBuilder5();
      tb.AddLine("<?xml version='1.0'?>");
      tb.AddLine("<ConnectionTemplates xmlns: xsi\"http://www.w3.org/2001/XMLSchema-instance\"");
      tb.AddLine(" xmlns: xsd=\"http://www.w3.org/2001/XMLSchema\">");
      tb.AddLine("  <ConnectionTemplate>");
      tb.AddLine("    <Name>SQLServer</Name>");
      tb.AddLine("    <Template>Data Source={DbServer}; Initial Catalog={Database};");
      tb.AddLine("     Integrated Security=True</Template>");
      tb.AddLine("  </ConnectionTemplate>");
      tb.AddLine("  <ConnectionTemplate>");
      tb.AddLine("    <Name>MySQL</Name>");
      tb.AddLine("    <Template>server={DbServer}; UserId={UID}; Password={PSWD};");
      tb.AddLine("     database={Database}</Template>");
      tb.AddLine("  </ConnectionTemplate>");
      tb.AddLine("  <ConnectionTemplate>");
      tb.AddLine("    <Name>OLEDB</Name>");
      tb.AddLine("    <Template>Provider=SQLOLEDB; Data Source={DbServer}\\instance;");
      tb.AddLine("     Initial Catalog={Database}; User Id={UID};");
      tb.AddLine("     Password={PSWD}</Template>");
      tb.AddLine("  </ConnectionTemplate>");
      tb.AddLine("  <ConnectionTemplate>");
      tb.AddLine("    <Name>Access</Name>");
      tb.AddLine("    <Template>Provider=Microsoft.ACE.OLEDB.12.0;");
      tb.AddLine("     Data Source=C:\\myAccessFile.accdb; Persist Security Info=False;</Template>");
      tb.AddLine("  </ConnectionTemplate>");
      tb.AddLine("  <ConnectionTemplate>");
      tb.AddLine("    <Name>ODBC</Name>");
      tb.AddLine("    <Template>Driver={SQL Server}; Server=myServerAddress;");
      tb.AddLine("     Database=myDataBase; Uid ={UID}; Pwd={PSWD};</Template>");
      tb.AddLine("  </ConnectionTemplate>");
      tb.AddLine("</ConnectionTemplates>");
      var templates = tb.ToString().Split('\n');
      if (LJC5.HasValue(TemplateFileSpec))
      {
        File.WriteAllLines(TemplateFileSpec, templates);
      }
    }
    #endregion

    #region Properties

    // The configuration file path.
    /// <include path="members/TemplateFileSpec/*" file="Doc/LJCConnectionTemplates.xml"/>
    public string? TemplateFileSpec { get; private set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private readonly string mTemplateFileName;
    #endregion
  }
}
