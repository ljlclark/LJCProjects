using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalcPad
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    #region Event handlers

    // Performs the calculations on double mouse click.
    private void CalcRTB_DoubleClick(object sender, EventArgs e)
    {
      var code = new CalcPadCode();
      code.DoCalcs(CalcsRTB);
      //for (int index = 0; index < CalcsRTB.Lines.Count(); index++)
      //{
      //  var line = CalcsRTB.Lines[index];
      //  if (!string.IsNullOrWhiteSpace(line)
      //    && !line.Trim().StartsWith("//"))
      //  {
      //    var lineItemRef = ParseItem(line, out LineType lineType);
      //    switch (lineType)
      //    {
      //      case LineType.Assignment:
      //        DoCalcs(lineItemRef);
      //        break;

      //      case LineType.Value:
      //        var text = line;
      //        var valueIndex = text.IndexOf("(");
      //        if (valueIndex > 0)
      //        {
      //          text = text.Substring(0, valueIndex - 1);
      //        }

      //        var itemRef = GetItem(lineItemRef.ValueName);
      //        var value = DoRound(lineItemRef.Rounding , itemRef.Value);
      //        text += $" ({value})";
      //        ReplaceLine(CalcsRTB, index, text);
      //        break;
      //    }
      //  }
      //}
    }
    #endregion
  }
}
