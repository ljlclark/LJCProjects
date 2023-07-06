// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataAssembly.cs
using LJCDocXMLObjLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace LJCDocObjLib
{
  // Represents the Assembly documentation data.
  /// <include path='items/DataAssembly/*' file='Doc/DataAssembly.xml'/>
  public class DataAssembly : IComparable<DataAssembly>
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public DataAssembly()
    {
    }

    // Initializes an object instance.
    /// <include path='items/DataAssemblyC/*' file='Doc/DataAssembly.xml'/>
    public DataAssembly(DataRoot dataRoot, string xmlFileName, string description
      , string mainImage = null)
    {
      bool isHtml = false;
      bool success = true;

      DataRoot = dataRoot;
      if (false == NetString.HasValue(xmlFileName))
      {
        success = false;
      }

      if (success)
      {
        // Check if assembly file is HTML.
        isHtml = ".html" == Path.GetExtension(xmlFileName).ToLower();
        if (false == isHtml)
        {
          // Check if assembly XML file exists.
          if (false == File.Exists(xmlFileName))
          {
            success = false;
            string errorText = $"{DateTime.Now} - File '{xmlFileName}'"
              + " was not found.\r\n";
            File.AppendAllText("LJCDocObjLib.log", errorText);
          }
        }
      }

      if (success)
      {
        if (false == isHtml)
        {
          // Create the AssemblyReflect reference.
          XmlFileSpec = xmlFileName;
          AssemblyReflect = GetAssemblyReflect(XmlFileSpec);
          Doc = NetCommon.XmlDeserialize(typeof(Doc), xmlFileName) as Doc;
        }
        if (success)
        {
          if (isHtml)
          {
            Name = Path.GetFileNameWithoutExtension(xmlFileName);
          }
          else
          {
            Name = Doc.DocAssembly.Name;
          }
          Description = description;
          MainImage = mainImage;
          if (false == isHtml)
          {
            CreateTypesData();
          }
        }
      }
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{Name}";
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataAssembly other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }

    // Creates the child class/type data.
    /// <include path='items/CreateTypesData/*' file='Doc/DataAssembly.xml'/>
    public void CreateTypesData()
    {
      var typeMembers = Doc.GetTypes();
      DataTypes = new List<DataType>();
      foreach (DocMember typeMember in typeMembers)
      {
        DataTypes.Add(new DataType(this, typeMember, AssemblyReflect));
      }
    }

    // Get the AssemblyReflect object.
    /// <include path='items/GetAssemblyReflect/*' file='Doc/DataAssembly.xml'/>
    public LJCAssemblyReflect GetAssemblyReflect(string xmlFileSpec)
    {
      string assemblyFileSpec;
      string fileSpecFolder;
      string fileSpecRoot;
      LJCAssemblyReflect retValue = null;

      if (false == xmlFileSpec.Contains(@"\"))
      {
        fileSpecFolder = null;
      }
      else
      {
        fileSpecFolder = Path.GetDirectoryName(xmlFileSpec) + @"\";
      }
      string fileName = Path.GetFileNameWithoutExtension(xmlFileSpec);
      if (fileSpecFolder != null)
      {
        fileSpecRoot = Path.Combine(fileSpecFolder, fileName);
      }
      else
      {
        fileSpecRoot = fileName;
      }
      assemblyFileSpec = $"{fileSpecRoot}.exe";
      if (false == File.Exists(assemblyFileSpec))
      {
        assemblyFileSpec = $"{fileSpecRoot}.dll";
      }
      if (File.Exists(assemblyFileSpec))
      {
        retValue = new LJCAssemblyReflect();
        retValue.SetAssembly(assemblyFileSpec);
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the DataTypes list.</summary>
    public List<DataType> DataTypes { get; set; }

    /// <summary>Gets or sets the assembly description.</summary>
    public string Description { get; set; }

    /// <summary>Gets or sets the assembly page image.</summary>
    public string MainImage { get; set; }

    /// <summary>Gets or sets the Assembly Name value.</summary>
    public string Name { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the LJCAssemblyReflect object.</summary>
    public LJCAssemblyReflect AssemblyReflect { get; private set; }

    /// <summary>Gets or sets the DataRoot value.</summary>
    public DataRoot DataRoot { get; private set; }

    // Gets or sets the Doc value.
    /// <include path='items/Doc/*' file='Doc/DataAssembly.xml'/>
    public Doc Doc { get; private set; }

    /// <summary>Gets or sets the XML file specification.</summary>
    public string XmlFileSpec { get; private set; }
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataAssemblyComparer : IComparer<DataAssembly>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataAssembly x, DataAssembly y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.Description, y.Description);
        if (-2 == retValue)
        {
          // Not case sensitive.
          retValue = string.Compare(x.Description, y.Description, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
