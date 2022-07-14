// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
// GenFileSpec.cs

namespace LJCGenTableCode
{
  /// <summary>Contains the file specification data for an output file.</summary>
  public class GenFileSpec
  {
    /// <summary>Gets or sets the FileTypeName value.</summary>
    public string FileTypeName { get; set; }

    /// <summary>Gets or sets the TemplateFileSpec value.</summary>
    public string TemplateFileSpec { get; set; }

    /// <summary>Gets or sets the OutputFileSpec value.</summary>
    public string OutputFileSpec { get; set; }

    /// <summary>Gets or sets the XMLFormat value.</summary>
    public string XMLFormat { get; set; }

    /// <summary>Gets or sets the IsPlural value.</summary>
    public bool IsPlural { get; set; }
  }
}
