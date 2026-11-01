// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenType.cs
using LJCDocDataDAL;
using LJCGenTextLib;
using System.IO;

namespace LJCGenDataGenLib
{
  public class LJCGenType
  {
    public LJCGenType(LJCGenDocConfig config, LJCDocDataClass docDataClass)
    {
      GenDocConfig = config;
      HTMLFolderName = Path.Combine(config.OutputPath
        , $@"HTML\{docDataClass.Name}");
      HTMLFileName = $"{docDataClass.Name}.html";
      HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
    }

    public Sections GenDataClass(LJCDocDataClass docDataClass)
    {
      var retSections = new Sections();

      var mainSection = retSections.Add("Main");
      var repeatItem = mainSection.RepeatItems.Add("Main");
      var mainReplacements = repeatItem.Replacements;
      //mainReplacements.Add("_AssemblyName_", DataAssembly.Name);
      //mainReplacements.Add("_AssemblyHtm_", GenAssembly.HTMLFileName);
      var copyRight = "Copyright &copy Lester J. Clark and Contributors.<br />\r\n";
      copyRight += "Licensed under the MIT License.";
      mainReplacements.Add("_Copyright_", copyRight);
      return retSections;
    }

    /// <summary>
    /// Gets or sets the GenDoc configuration.
    /// </summary>
    public LJCGenDocConfig GenDocConfig { get; set; }

    string HTMLFolderName { get; set; }

    string HTMLFileName { get; set; }

    string HTMLFileSpec { get; set; }
  }
}
