// Copyright(c) Lester J. Clark and Contributors.
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
    //public DataRoot(DocGenGroups assemblyGroups)
    public DataRoot(DocAssemblyGroups assemblyGroups)
    {
      AssemblyGroups = assemblyGroups;
      Managers = ValuesDocGen.Instance.Managers;
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
      foreach (DocAssemblyGroup assemblyGroup in AssemblyGroups)
      {
        var assemblyManager = Managers.DocAssemblyManager;
        var docAssemblies = assemblyManager.LoadWithParent(assemblyGroup.ID);
        foreach (DocAssembly docAssembly in docAssemblies)
        {
          // Check if assembly file is HTML.
          success = false;
          bool isHtml = ".html" == Path.GetExtension(docAssembly.FileSpec).ToLower();
          if (false == isHtml)
          {
            // Check if assembly XML file exists.
            success = true;
            if (false == File.Exists(docAssembly.FileSpec))
            {
              success = false;
              string errorText = $"{DateTime.Now} - File"
                + $" '{docAssembly.FileSpec}' was not found.\r\n";
              File.AppendAllText("LJCDocObjLib.log", errorText);
            }
          }

          if (success)
          {
            // Create the DataAssembly data.
            DataAssembly dataAssembly = new DataAssembly(this
              , docAssembly.FileSpec, docAssembly.Description
              , docAssembly.MainImage);
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
    public DocAssemblyGroups AssemblyGroups { get; set; }

    /// <summary></summary>
    public ManagersDocGen Managers { get; set; }
    #endregion
  }
}
