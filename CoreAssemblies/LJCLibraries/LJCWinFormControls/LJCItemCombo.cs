// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCItemCombo.cs
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
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public LJCItemCombo()
    {
      InitializeComponent();
    }
    #endregion

    #region Methods

    // Adds an Item to the ComboBox.
    /// <include path='items/LJCAddItem/*' file='Doc/LJCItemCombo.xml'/>
    public LJCItem LJCAddItem(int id, string text)
    {
      LJCItem retValue = null;

      LJCItem item = new LJCItem()
      {
        ID = id,
        Text = text
      };
      Items.Add(item);
      return retValue;
    }

    // Sets the combo SelectedIndex to the item with the specified ID value.
    /// <include path='items/LJCSetByItemID/*' file='Doc/LJCItemCombo.xml'/>
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
    /// <include path='items/LJCSelectedItemID/*' file='Doc/LJCItemCombo.xml'/>
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
    /// <include path='items/LJCExportData/*' file='Doc/LJCDataGrid.xml'/>
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
  }

  // Represents an LJCItemCombo Item.
  /// <include path='items/LJCItem/*' file='Doc/LJCItemCombo.xml'/>
  public class LJCItem
  {
    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public LJCItem Clone()
    {
      LJCItem retValue = MemberwiseClone() as LJCItem;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
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
