// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesGenDocEdit.cs
using LJCDBClientLib;

namespace LJCGenDocEdit
{
  // Application config values singleton.
  internal sealed class ValuesGenDocEdit
  {
    #region Constructors

    // Initializes an object instance.
    internal ValuesGenDocEdit()
    {
      StandardSettings = new StandardUISettings();
      StandardSettings.SetProperties("LJCGenDocEdit.exe.config");
    }
    #endregion

    #region Properties

    // The singleton instance.
    internal static ValuesGenDocEdit Instance { get; } = new ValuesGenDocEdit();

    // Gets or sets the StandardSettings value.
    internal StandardUISettings StandardSettings { get; set; }
    #endregion
  }
}
