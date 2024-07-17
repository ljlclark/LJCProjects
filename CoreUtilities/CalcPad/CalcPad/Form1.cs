using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CalcPad
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      IDs = new List<ID>();
    }

    #region Methods

    // Creates a Formula ID.
    private ID CreateAssignmentID(string[] tokens, out LineType lineType)
    {
      ID retValue = null;

      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && ("=" == tokens[1].ToLower()))
      {
        lineType = LineType.Assignment;

        // Name is # Name
        retValue = new ID
        {
          Name = tokens[0]
        };

        if (tokens.Length > 2)
        {
          if (double.TryParse(tokens[2], out double value))
          {
            retValue.Value = value;
          }
        }
        retValue = SaveID(retValue, lineType);
      }
      return retValue;
    }

    // Creates a Formula ID.
    private ID CreateFormulaID(string[] tokens, out LineType idType)
    {
      ID retValue = null;

      idType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && "is" == tokens[1].ToLower())
      {
        idType = LineType.Formula;

        // Name is # Name
        retValue = new ID
        {
          Name = tokens[0]
        };

        if (tokens.Length > 2)
        {
          if (double.TryParse(tokens[2], out double value))
          {
            retValue.FormulaValue = value;
          }
        }

        if (tokens.Length > 3)
        {
          retValue.FormulaIDName = tokens[3];
        }
        retValue = SaveID(retValue, idType);
      }
      return retValue;
    }

    private ID CreateID(string line, out LineType lineType)
    {
      ID retValue = null;

      lineType = LineType.None;
      var tokens = SplitChars(" ", line);
      if (tokens.Length > 0)
      {
        retValue = CreateNameID(tokens, out lineType);
        if (null == retValue)
        {
          retValue = CreateFormulaID(tokens, out lineType);
        }

        if (null == retValue)
        {
          retValue = CreateAssignmentID(tokens, out lineType);
        }

        if (null == retValue)
        {
          retValue = CreateTotalID(tokens, out lineType);
        }
      }
      return retValue;
    }

    // Creates a Name ID.
    private ID CreateNameID(string[] tokens, out LineType lineType)
    {
      ID retValue = null;

      lineType = LineType.None;
      foreach (string token in tokens)
      {
        // Name/AltName
        if (token.Contains("/"))
        {
          lineType = LineType.Name;

          retValue = new ID();

          // Remove extra spaces.
          var text = String.Join("", tokens);
          var names = SplitChars("/", text);

          retValue.Name = names[0];
          if (names.Length > 1)
          {
            retValue.AltName = names[1];
          }
          retValue = SaveID(retValue, lineType);
          break;
        }
      }
      return retValue;
    }

    // Creates a Total ID.
    private ID CreateTotalID(string[] tokens, out LineType lineType)
    {
      ID retValue = null;

      // Total: Name
      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && "total:" == tokens[0].ToLower())
      {
        lineType = LineType.Total;

        retValue = new ID
        {
          Name = tokens[0],
        };

        if (tokens.Length > 1)
        {
          retValue.TotalIDName = tokens[1];
        }

        if (tokens.Length > 2)
        {
          ID findID = GetID(retValue.TotalIDName);
          findID.Rounding = tokens[2];
        }
      }
      return retValue;
    }

    // 
    private void DoCalcs(ID lineIDRef)
    {
      // "FirstName" is # "SecondName"
      var idRef = lineIDRef;

      // Calculate SecondID total.
      var formulaIDRef = GetID(idRef.FormulaIDName);
      formulaIDRef.Value = idRef.Value * idRef.FormulaValue;

      // Calculate formula "To" totals.
      var prevIDRef = idRef;
      var nextIDRef = GetFormulaID(idRef);
      while (nextIDRef != null)
      {
        if (nextIDRef != null)
        {
          nextIDRef.Value = prevIDRef.Value / nextIDRef.FormulaValue;
          prevIDRef = nextIDRef;
          nextIDRef = GetFormulaID(nextIDRef);
        }
      }
    }

    // 
    private double DoRound(ID id)
    {
      double retValue = id.Value;

      if (id != null
        && !string.IsNullOrWhiteSpace(id.Rounding))
      {
        switch (id.Rounding.ToLower())
        {
          case "ceiling":
            retValue = Math.Ceiling(id.Value);
            break;

          case "round":
            retValue = Math.Round(id.Value);
            break;

          case "floor":
            retValue = Math.Floor(id.Value);
            break;
        }
      }
      return retValue;
    }

    // Gets the Formula name by ID Name or AltNames
    private ID GetFormulaID(ID id)
    {
      ID retValue = null;

      if (id != null)
      {
        retValue = IDs.Find(x => x.FormulaIDName == id.Name);
        if (null == retValue)
        {
          retValue = IDs.Find(x => x.FormulaIDName == id.AltName);
        }
      }
      return retValue;
    }

    // Gets the ID by Name or AltNames
    private ID GetID(string name)
    {
      ID retValue = null;

      if (!string.IsNullOrWhiteSpace(name))
      {
        retValue = IDs.Find(x => x.Name == name);
        if (null == retValue)
        {
          retValue = IDs.Find(x => x.AltName == name);
        }
      }
      return retValue;
    }

    private ID SaveID(ID id, LineType lineType)
    {
      var retValue = GetID(id.Name);
      if (null == retValue)
      {
        retValue = id;
        IDs.Add(retValue);
      }
      else
      {
        // Update ID values.
        switch (lineType)
        {
          case LineType.Assignment:
            retValue.Value = id.Value;
            break;

          case LineType.Formula:
            retValue.FormulaValue = id.FormulaValue;
            retValue.FormulaIDName = id.FormulaIDName;
            break;

          case LineType.Name:
            retValue.AltName = id.AltName;
            break;

          case LineType.Total:
            retValue.Rounding = id.Rounding;
            break;
        }
      }
      return retValue;
    }

    // Performs a split with no empty entries.
    private string[] SplitChars(string separators, string text)
    {
      return text.Split(separators.ToCharArray()
        , StringSplitOptions.RemoveEmptyEntries);
    }
    #endregion

    #region RTB Methods

    // 
    private int GetCharIndex(RichTextBox rtb, int lineIndex)
    {
      var retValue = rtb.GetFirstCharIndexFromLine(lineIndex);
      return retValue;
    }

    // 
    private void ReplaceLine(RichTextBox rtb, int lineIndex, string text)
    {
      var beginIndex = GetCharIndex(rtb, lineIndex);

      var endIndex = rtb.Text.Length;
      if (lineIndex < rtb.Lines.Count())
      {
        lineIndex++;
        endIndex = GetCharIndex(rtb, lineIndex) - 1;
      }

      var length = endIndex - beginIndex;
      rtb.Select(beginIndex, length);
      rtb.SelectedText = text;
    }
    #endregion

    #region Event handlers

    private void CalcRTB_DoubleClick(object sender, EventArgs e)
    {
      for (int index = 0; index < CalcsRTB.Lines.Count(); index++)
      {
        var line = CalcsRTB.Lines[index];
        if (!string.IsNullOrWhiteSpace(line))
        {
          var lineIDRef = CreateID(line, out LineType lineType);
          switch (lineType)
          {
            case LineType.Assignment:
              DoCalcs(lineIDRef);
              break;

            case LineType.Total:
              var text = line;
              var valueIndex = text.IndexOf("(");
              if (valueIndex > 0)
              {
                text = text.Substring(0, valueIndex - 1);
              }

              var idRef = GetID(lineIDRef.TotalIDName);
              var value = DoRound(idRef);
              text += $" ({value})";
              ReplaceLine(CalcsRTB, index, text);
              break;
          }
        }
      }
    }
    #endregion

    #region Properties

    private List<ID> IDs { get; set; }
    #endregion
  }

  // Represents an Identifier.s
  public class ID
  {
    #region Constructors

    // Initializes an object instance.
    public ID()
    {
    }

    // Initializes an object instance.
    public ID(string name, double value)
    {
      Name = name;
      Value = value;
    }
    #endregion

    #region Data Methods

    public override string ToString()
    {
      return $"{Name}: {Value}";
    }
    #endregion

    #region Methods

    // Gets int value.
    public bool ValueIfInt(out int value)
    {
      bool retValue = false;

      value = 0;
      if (0 == Value % 1)
      {
        Value = (int)Value;
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Properties

    // 
    public string AltName { get; set; }

    // 
    public double FormulaValue { get; set; }

    // 
    public string FormulaIDName { get; set; }

    // 
    public string Name { get; set; }

    // 
    public string Rounding { get; set; }

    // 
    public string TotalIDName { get; set; }

    // 
    public double Value { get; set; }
    #endregion
  }

  public enum LineType
  {
    None,
    Name,
    Formula,
    Assignment,
    Total
  }
}
