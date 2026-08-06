// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCItemCombo.cs
using LJCNetCommon;
using LJCWinFormCommon;
using System.IO;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  // Provides custom functionality for a ComboBox control. (R)
  /// <include path='items/LJCItemCombo/*' file='Doc/LJCItemCombo.xml'/>
  public partial class LJCItemCombo : ComboBox
  {
    #region Constructor

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCItemCombo()
    {
      InitializeComponent();
      mArgError = new ArgError("LJCItemCombo");
    }
    #endregion

    #region Methods

    // Adds an Item to the ComboBox.
    /// <include file='Doc/LJCItemCombo.xml'
    ///  path='items/LJCAddItem/*'/>
    public LJCItem LJCAddItem(int id, string text)
    {
      mArgError.MethodName = "LJCAddItem";
      var message = "";
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      mArgError.Add(message);
      mArgError.Add(text, "text");
      NetString.ThrowArgError(mArgError.ToString());

      var retItem = new LJCItem()
      {
        ID = id,
        Text = text
      };
      Items.Add(retItem);
      return retItem;
    }

    // Sets the combo SelectedIndex to the item with the specified ID value.
    /// <include file='Doc/LJCItemCombo.xml'
    ///  path='items/LJCSetByItemID/*'/>
    public void LJCSetByItemID(int id)
    {
      for (int index = 0; index < Items.Count; index++)
      {
        LJCItem item = Items[index] as LJCItem;
        if (id == item.ID)
        {
          SelectedIndex = index;
          break;
        }
      }
    }

    // Gets the combo SelectedItem ID.
    /// <include file='Doc/LJCItemCombo.xml'
    ///  path='items/LJCSelectedItemID/*'/>
    public int LJCSelectedItemID()
    {
      int retValue = 0;

      if (SelectedItem is LJCItem item)
      {
        retValue = item.ID;
      }
      return retValue;
    }

    ////  Gets the combo SelectedItem text.
    ///// <include path='items/LJCSelectedItemText/*' file='Doc/LJCItemCombo.xml'/>
    //private string LJCSelectedItemText()
    //{
    //  string retValue = null;

    //  if (SelectedItem is LJCItem item)
    //  {
    //    retValue = item.Text;
    //  }
    //  return retValue;
    //}

    // Exports the grid values to a data file.
    /// <include file='Doc/LJCDataGrid.xml'
    ///  path='items/LJCExportData/*'/>
    public void LJCExportData(string fileName)
    {
      string separator;
      string line;

      separator = "\t";
      if (".csv" == Path.GetExtension(fileName).ToLower())
      {
        separator = ", ";
      }
      File.WriteAllText(fileName, "");

      // Write data rows.
      foreach (LJCItem ljcItem in Items)
      {
        line = $"{ljcItem.ID}{separator}{ljcItem.Text}\r\n";
        File.AppendAllText(fileName, line);
      }

      FormCommon.ShellProgram(null, fileName);
    }
    #endregion

    #region Class Values

    // The ArgError object.
    private readonly ArgError mArgError;
    #endregion
  }

  // Represents an LJCItemCombo Item.
  /// <include file='Doc/LJCItemCombo.xml'
  ///  path='items/LJCItem/*'/>
  public class LJCItem
  {
    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCItem Clone()
    {
      LJCItem retValue = MemberwiseClone() as LJCItem;
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return Text;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ID value.</summary>
    public int ID { get; set; }

    /// <summary>Gets or sets the Text value.</summary>
    public string Text { get; set; }
    #endregion 
  }
}
