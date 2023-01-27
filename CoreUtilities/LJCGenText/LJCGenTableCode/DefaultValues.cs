// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DefaultValues.cs

namespace LJCGenTableCode
{
  /// <summary>Contains the deserialized values from the DefaultValues.xml file.</summary>
  public class DefaultValues
  {
    /// <summary>Gets or sets the default Namespace value.</summary>
    public string Namespace { get; set; }

    /// <summary>Gets or sets the default CollectionName value.</summary>
    public string CollectionName { get; set; }

    /// <summary>Gets or sets the default ClassName value.</summary>
    public string ClassName { get; set; }

    /// <summary>Gets or sets the default TableName value.</summary>
    public string TableName { get; set; }

    /// <summary>Gets or sets the default FullAppName value.</summary>
    public string FullAppName { get; set; }

    /// <summary>Gets or sets the default AppName value.</summary>
    public string AppName { get; set; }

    /// <summary>Gets or sets the default DataConfigName value.</summary>
    public string DataConfigName { get; set; }

    /// <summary>Gets or sets the default ToStringName value.</summary>
    public string ToStringName { get; set; }

    /// <summary>Gets or sets the default CompareToName value.</summary>
    public string CompareToName { get; set; }
  }
}
