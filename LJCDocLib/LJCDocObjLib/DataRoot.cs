// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataRoot.cs
using LJCDocLibDAL;
using System;
using System.IO;

namespace LJCDocObjLib
{
  // Represents the Root documentation data.
  /// <include path='items/DataRoot/*' file='Doc/DataRoot.xml'/>
  public class DataRoot
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataRootC/*' file='Doc/DataRoot.xml'/>
    public DataRoot(DocGenGroups assemblyGroups)
    {
      DocGenGroups = assemblyGroups;
      File.WriteAllText("LJCDocObjLib.log", "");
      CreateAssembliesData();
    }
    #endregion

    #region Methods

    // Creates the assembly data.
    /// <include path='items/CreateAssembliesData/*' file='Doc/DataRoot.xml'/>
    private void CreateAssembliesData()
    {
      bool success;

      DataAssemblies = new DataAssemblies();
      foreach (DocGenGroup docGenGroup in DocGenGroups)
      {
        foreach (DocGenAssembly docGenAssembly in docGenGroup.DocGenAssemblies)
        {
          // Check if assembly file is HTML.
          success = true;
          bool isHtml = ".html" == Path.GetExtension(docGenAssembly.FileSpec).ToLower();
          if (false == isHtml)
          {
            // Check if assembly XML file exists.
            if (false == File.Exists(docGenAssembly.FileSpec))
            {
              success = false;
              string errorText = $"{DateTime.Now} - File"
                + $" '{docGenAssembly.FileSpec}' was not found.\r\n";
              File.AppendAllText("LJCDocObjLib.log", errorText);
            }
          }

          if (success)
          {
            // Create the DataAssembly data.
            DataAssembly dataAssembly = new DataAssembly(this
              , docGenAssembly.FileSpec, docGenAssembly.Description
              , docGenAssembly.MainImage);
            if (dataAssembly.Name != null)
            {
              DataAssemblies.Add(dataAssembly);
            }
          }
        }
      }
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the DataAssemblies list.</summary>
    public DataAssemblies DataAssemblies { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the AssemblyGroups list.</summary>
    public DocGenGroups DocGenGroups { get; set; }
    #endregion
  }
}
