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
        var tokenCount = tokens.Length;
        var line = string.Join(" ", tokens);

        var success = true;
        if (tokenCount < 3
          || tokenCount > 5)
        {
          success = false;
          Errors.Syntax(line, Errors.SyntaxAssignment());
        }

        if (success)
        {
          // Name = name/value operator name/value.
          retValue = new ID
          {
            Name = tokens[0]
          };

          // Get first value.
          ID findID;
          string item;
          var firstValue = 0.0;
          if (tokenCount > 2)
          {
            item = tokens[2];
            if (double.TryParse(item, out double value))
            {
              if (0 == value)
              {
                Errors.IDName("ID name must not start with number", line);
              }
              firstValue = value;
            }
            else
            {
              findID = GetID(item);
              if (null == findID)
              {
                success = false;
                Errors.IDNotFound(item, line);
              }
              if (success)
              {
                firstValue = findID.Value;
              }
            }
          }

          if (3 == tokenCount)
          {
            retValue.Value = firstValue;
          }

          // Get operation.
          string operation = null;
          if (tokenCount > 3)
          {
            item = tokens[3];
            if ("+-*/".Contains(item))
            {
              operation = item;
            }
            else
            {
              success = false;
              Errors.InvalidOperator(line);
            }
          }

          // Get second value.
          var secondValue = 0.0;
          if (tokenCount > 4)
          {
            item = tokens[4];
            if (double.TryParse(item, out double value))
            {
              if (0 == value)
              {
                Errors.IDName("ID name must not start with number", line);
              }
              secondValue = value;
            }
            else
            {
              findID = GetID(item);
              if (null == findID)
              {
                success = false;
                Errors.IDNotFound(item, line);
              }
              else
              {
                secondValue = findID.Value;
              }
            }
          }

          if (firstValue > 0
            && secondValue > 0)
          {
            var value = retValue.Value;
            switch (operation)
            {
              case "+":
                value = firstValue + secondValue;
                break;
              case "-":
                value = firstValue - secondValue;
                break;
              case "*":
                value = firstValue * secondValue;
                break;
              case "/":
                value = firstValue / secondValue;
                break;
            }
            retValue.Value = value;
          }
          if (success)
          {
            retValue = SaveID(retValue, lineType);
          }
        }
      }
      return retValue;
    }

    // Creates a Formula ID.
    private ID CreateFormulaID(string[] tokens, out LineType lineType)
    {
      ID retValue = null;

      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && "is" == tokens[1].ToLower())
      {
        lineType = LineType.Formula;
        var tokenCount = tokens.Length;
        var line = string.Join(" ", tokens);

        var success = true;
        if (tokenCount < 4
          || tokenCount > 4)
        {
          success = false;
          Errors.Syntax(line, Errors.SyntaxFormula());
        }

        // Name is # Name
        retValue = new ID()
        {
          Name = tokens[0]
        };

        if (tokenCount > 2)
        {
          if (double.TryParse(tokens[2], out double value))
          {
            if (0 == value)
            {
              success = false;
              Errors.Value(line);
            }
              retValue.FormulaValue = value;
          }
        }

        if (tokenCount > 3)
        {
          retValue.FormulaIDName = tokens[3];
        }
        if (success)
        {
          retValue = SaveID(retValue, lineType);
        }
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
        if (token.Contains("|"))
        {
          lineType = LineType.Name;
          var line = string.Join(" ", tokens);

          retValue = new ID()
          {
            Value = 0
          };

          // Remove extra spaces.
          var success = true;
          var text = String.Join("", tokens);
          var names = SplitChars("|", text);
          if (0 == names.Length)
          {
            success = false;
            Errors.Declarations(line);
          }

          if (success)
          {
            retValue.Name = names[0];
            if (names.Length > 1)
            {
              retValue.AltName = names[1];
            }
            retValue = SaveID(retValue, lineType);
          }
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
        var tokenCount = tokens.Length;
        var line = string.Join(" ", tokens);

        if (tokenCount < 2
          || tokenCount > 3)
        {
          Errors.Syntax(line, Errors.SyntaxTotal());
        }

        retValue = new ID
        {
          Name = tokens[0],
        };

        if (tokenCount > 1)
        {
          retValue.TotalIDName = tokens[1];
        }

        if (tokenCount > 2)
        {
          ID findID = GetID(retValue.TotalIDName);
          findID.Rounding = tokens[2];
        }
      }
      return retValue;
    }

    // Performs the related conversion calculations.
    private void DoCalcs(ID lineIDRef)
    {
      // "LeftName" is # "RightNames"
      // Calculate RightID total.
      ID prevIDRef = lineIDRef;
      ID nextIDRef = null;
      if (!string.IsNullOrWhiteSpace(lineIDRef.FormulaIDName))
      {
        var formulaIDRef = GetID(lineIDRef.FormulaIDName);
        if (formulaIDRef != null)
        {
          formulaIDRef.Value = lineIDRef.Value * lineIDRef.FormulaValue;
          nextIDRef = GetIDWithFormula(lineIDRef);
        }
      }

      if (null == nextIDRef)
      {
        // Look for related conversion.
        nextIDRef = GetIDWithFormula(lineIDRef);
      }

      // Calculate related formula totals.
      while (nextIDRef != null)
      {
        if (nextIDRef != null)
        {
          nextIDRef.Value = prevIDRef.Value / nextIDRef.FormulaValue;
          prevIDRef = nextIDRef;
          nextIDRef = GetIDWithFormula(nextIDRef);
        }
      }
    }

    // Performs the rounding.
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
    private ID GetIDWithFormula(ID id)
    {
      ID retValue = null;

      if (id != null)
      {
        if (!string.IsNullOrWhiteSpace(id.Name))
        {
          retValue = IDs.Find(x => x.FormulaIDName == id.Name);
        }
        if (null == retValue)
        {
          if (!string.IsNullOrWhiteSpace(id.AltName))
          {
            retValue = IDs.Find(x => x.FormulaIDName == id.AltName);
          }
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
        if (!string.IsNullOrWhiteSpace(name))
        {
          retValue = IDs.Find(x => x.Name == name);
        }
        if (null == retValue)
        {
          if (!string.IsNullOrWhiteSpace(name))
          {
            retValue = IDs.Find(x => x.AltName == name);
          }
        }
      }
      return retValue;
    }

    // Saves or Updates the ID.
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

    // Gets the RTB line first char index.
    private int GetCharIndex(RichTextBox rtb, int lineIndex)
    {
      var retValue = rtb.GetFirstCharIndexFromLine(lineIndex);
      return retValue;
    }

    // Replaces RTB line.
    private void ReplaceLine(RichTextBox rtb, int lineIndex, string text)
    {
      var beginIndex = GetCharIndex(rtb, lineIndex);

      var endIndex = rtb.Text.Length;
      if (lineIndex < rtb.Lines.Count() - 1)
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

    // Performs the calculations on double mouse click.
    private void CalcRTB_DoubleClick(object sender, EventArgs e)
    {
      for (int index = 0; index < CalcsRTB.Lines.Count(); index++)
      {
        var line = CalcsRTB.Lines[index];
        if (!string.IsNullOrWhiteSpace(line)
          && !line.Trim().StartsWith("//"))
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

    // The defined IDs.
    private List<ID> IDs { get; set; }
    #endregion
  }

  // Represents an Identifier.
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

    // The debug display value.
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

    // The Altername name.
    public string AltName { get; set; }

    // The Formula value.
    public double FormulaValue { get; set; }

    // The Formula ID name.
    public string FormulaIDName { get; set; }

    // The ID name.
    public string Name { get; set; }

    // The Rounding value.
    public string Rounding { get; set; }

    // The Total ID name.
    public string TotalIDName { get; set; }

    // The ID value.
    public double Value { get; set; }
    #endregion
  }

  // The line type values.
  public enum LineType
  {
    None,
    Name,
    Formula,
    Assignment,
    Total
  }
}
