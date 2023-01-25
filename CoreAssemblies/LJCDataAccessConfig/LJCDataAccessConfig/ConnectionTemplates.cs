// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConnectionTemplates.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDataAccessConfig
{
  // Represents a collection of Connection string templates.
  /// <include path='items/ConnectionTemplates/*' file='Doc/ConnectionTemplatesDoc.xml'/>
  [XmlRoot("ConnectionTemplates")]
  public class ConnectionTemplates : List<ConnectionTemplate>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ConnectionTemplatesC/*' file='Doc/ConnectionTemplatesDoc.xml'/>
    public ConnectionTemplates()
    {
      mTemplateFileName = "ConnectionTemplates.xml";
      string localAssembly = Assembly.GetExecutingAssembly().Location;
      LJCTemplateFileSpec = Path.Combine(Path.GetDirectoryName(localAssembly)
        , mTemplateFileName);
    }
    #endregion

    #region Public Methods

    // Retrieves the config data.
    /// <include path='items/LJCLoadData/*' file='Doc/ConnectionTemplatesDoc.xml'/>
    public void LJCLoadData()
    {
      if (false == File.Exists(LJCTemplateFileSpec))
      {
        WriteDefaultData();
      }

      if (NetCommon.XmlDeserialize(typeof(ConnectionTemplates)
        , LJCTemplateFileSpec) is ConnectionTemplates connectionTemplates)
      {
        Clear();
        foreach (ConnectionTemplate connectionTemplate in connectionTemplates)
        {
          Add(connectionTemplate);
        }
      }
    }

    // Creates and adds the object from the provided valus.
    /// <include path='items/Add/*' file='Doc/ConnectionTemplatesDoc.xml'/>
    public ConnectionTemplate Add(string name, string template)
    {
      ConnectionTemplate retValue = new ConnectionTemplate()
      {
        Name = name,
        Template = template
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the data configuration.
    /// <include path='items/LJCGetByName/*' file='Doc/ConnectionTemplatesDoc.xml'/>
    public ConnectionTemplate LJCGetByName(string name)
    {
      ConnectionTemplate retValue;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      ConnectionTemplate searchData = new ConnectionTemplate()
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
      return retValue;
    }

    // Saves the config data.
    /// <include path='items/LJCSave/*' file='Doc/ConnectionTemplatesDoc.xml'/>
    public void LJCSave()
    {
      NetCommon.XmlSerialize(GetType(), this, null, LJCTemplateFileSpec);
    }
    #endregion

    #region Private Methods

    // Create the default data file.
    private void WriteDefaultData()
    {
      List<string> templates = new List<string>
        {
          "<?xml version='1.0'?>",
          "<ConnectionTemplates xmlns: xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns: xsd='http://www.w3.org/2001/XMLSchema'>",
          "  <ConnectionTemplate>",
          "    <Name>SQLServer</Name>",
          "    <Template>Data Source={DbServer}; Initial Catalog={Database}; "
            + "Integrated Security=True</Template>",
          "  </ConnectionTemplate>",
          "  <ConnectionTemplate>",
          "    <Name>MySQL</Name>",
          "    <Template>server={DbServer}; UserId={UID}; Password={PSWD}; "
            + "database={Database}</Template>",
          "  </ConnectionTemplate>",
          "  <ConnectionTemplate>",
          "    <Name>OLEDB</Name>",
          "    <Template>Provider=SQLOLEDB; Data Source={DbServer}\\instance; "
            + "Initial Catalog={Database}; User Id={UID}; "
            + "Password={PSWD}</Template>",
          "  </ConnectionTemplate>",
          "  <ConnectionTemplate>",
          "    <Name>Access</Name>",
          "    <Template>Provider=Microsoft.ACE.OLEDB.12.0; "
            + "Data Source=C:\\myAccessFile.accdb; Persist Security Info=False;</Template>",
          "  </ConnectionTemplate>",
          "  <ConnectionTemplate>",
          "    <Name>ODBC</Name>",
          "    <Template>Driver={SQL Server}; Server=myServerAddress; "
            + "Database=myDataBase; Uid ={UID}; Pwd={PSWD};</Template>",
          "  </ConnectionTemplate>",
          "</ConnectionTemplates>"
        };
      File.WriteAllLines(LJCTemplateFileSpec, templates);
    }
    #endregion

    #region Properties

    /// <summary>The configuration file path.</summary>
    public string LJCTemplateFileSpec { get; private set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private readonly string mTemplateFileName;
    #endregion
  }
}
