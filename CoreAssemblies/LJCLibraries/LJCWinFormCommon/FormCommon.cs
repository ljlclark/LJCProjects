// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FormCommon.cs
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;

namespace LJCWinFormCommon
{
  // Provides common WinForms methods.
  /// <include path='items/FormCommon/*' file='Doc/FormCommon.xml'/>
  public class FormCommon
  {
    #region General Functions

    // Verify create of missing tables.
    /// <include path='items/CreateTablesPrompt/*' file='Doc/FormCommon.xml'/>
    public static bool CreateTablesPrompt(string exceptionMessage, string[] fileSpecs)
    {
      bool retValue = false;

      if (string.IsNullOrWhiteSpace(exceptionMessage))
      {
        throw new ArgumentException("message", nameof(exceptionMessage));
      }
      if (fileSpecs == null)
      {
        throw new ArgumentNullException(nameof(fileSpecs));
      }

      StringBuilder builder = new StringBuilder(128);
      builder.Append("Do you want to create missing tables with:\r\n");
      bool isFirst = true;
      foreach (string fileSpec in fileSpecs)
      {
        if (false == isFirst)
        {
          builder.Append("\r\n");
        }
        isFirst = false;

        builder.Append($" {fileSpec}");
      }
      string text = builder.ToString();

      string message = $"{exceptionMessage}\r\n{text}";
      if (DialogResult.Yes == MessageBox.Show(message, "Missing Tables"
        , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
      {
        retValue = true;
      }
      return retValue;
    }

    // Restore the splitter distance.
    /// <include path='items/RestoreSplitDistance/*' file='Doc/FormCommon.xml'/>
    public static void RestoreSplitDistance(SplitContainer splitContainer
      , ControlValues controlValues)
    {
      if (controlValues != null)
      {
        string name = $"{splitContainer.Name}.SplitterDistance";
        ControlValue controlValue = controlValues.LJCSearchName(name);
        if (controlValue != null)
        {
          splitContainer.SplitterDistance = controlValue.Height;
        }
      }
    }

    // Sets the BackColor for the labels.
    /// <include path='items/SetLabelsBackColor/*' file='Doc/FormCommon.xml'/>
    public static void SetLabelsBackColor(Control.ControlCollection controls
      , Color backColor)
    {
      foreach (Control control in controls)
      {
        if ("Label" == control.GetType().Name)
        {
          control.BackColor = backColor;
        }
        if (control.Controls != null && control.Controls.Count > 0)
        {
          SetLabelsBackColor(control.Controls, backColor);
        }
      }
    }
    #endregion

    #region Action State Functions

    // Sets the enable state for the menu items.
    /// <include path='items/SetMenuState/*' file='Doc/FormCommon.xml'/>
    public static void SetMenuState(ContextMenuStrip contextMenuStrip, bool enableNew
      , bool enableEdit)
    {
      foreach (ToolStripItem menuItem in contextMenuStrip.Items)
      {
        if (menuItem.Name.Contains("New"))
        {
          menuItem.Enabled = enableNew;
        }
        else
        {
          if (false == menuItem.Name.EndsWith("Exit")
            && false == menuItem.Name.EndsWith("Close"))
          {
            menuItem.Enabled = enableEdit;
          }
        }
      }
    }

    // Sets the enable state for the tool items.
    /// <include path='items/SetToolState/*' file='Doc/FormCommon.xml'/>
    public static void SetToolState(ToolStrip toolStrip, bool enableNew, bool enableEdit)
    {
      foreach (ToolStripItem toolItem in toolStrip.Items)
      {
        if (toolItem.Name.Contains("New"))
        {
          toolItem.Enabled = enableNew;
        }
        else
        {
          toolItem.Enabled = enableEdit;
        }
      }
    }
    #endregion

    #region Error Functions

    // Displays the error text if it is not null.
    /// <include path='items/ShowError/*' file='Doc/FormCommon.xml'/>
    public static bool ShowError(string errorText, string caption = null)
    {
      bool retValue = false;

      //if (false == string.IsNullOrWhiteSpace(errorText))
      if (NetString.HasValue(errorText))
      {
        retValue = true;
        MessageBox.Show(errorText, caption, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }

    // Displays the error text if it is not null.
    /// <include path='items/ShowHasError/*' file='Doc/FormCommon.xml'/>
    public static bool ShowHasError(string errorText, int affectedCount = 1
      , string caption = null)
    {
      bool retValue;

      retValue = FormCommon.ShowError(errorText, caption);
      if (affectedCount < 1)
      {
        retValue = true;
        if (string.IsNullOrWhiteSpace(errorText))
        {
          errorText = "No records affected.";
          FormCommon.ShowError(errorText, caption);
        }
      }
      return retValue;
    }
    #endregion

    #region Field Key Handler Functions

    // Checks the key character for a numeric or allowed control value.
    /// <include path='items/HandleNumberOrEditKey/*' file='Doc/FormCommon.xml'/>
    public static bool HandleNumberOrEditKey(char keyChar)
    {
      // Use in KeyDown
      //if (e.KeyData == (Keys.Control | Keys.V))
      bool retValue = true;

      if (char.IsDigit(keyChar)
        || '.' == keyChar
        || (char)Keys.Back == keyChar
        || (char)Keys.Delete == keyChar)
      {
        retValue = false;
      }
      return retValue;
    }

    // Checks the key character for a space.
    /// <include path='items/HandleSpace/*' file='Doc/FormCommon.xml'/>
    public static bool HandleSpace(char keyChar)
    {
      bool retValue = false;

      if (' ' == keyChar)
      {
        retValue = true;
      }
      return retValue;
    }

    // Strips blanks from the string.
    /// <include path='items/StripBlanks/*' file='Doc/FormCommon.xml'/>
    public static string StripBlanks(string text)
    {
      return text.Replace(" ".ToString(), "");
    }

    // Strips non-digits from a string.
    /// <include path='items/StripNonDigits/*' file='Doc/FormCommon.xml'/>
    public static string StripNonDigits(string text)
    {
      string retValue = null;

      foreach (char character in text)
      {
        if (false == char.IsDigit(character))
        {
          retValue = retValue.Replace(character.ToString(), "");
        }
      }
      return retValue;
    }

    // Only allows numbers or edit keys.
    /// <include path='items/TextBoxNumeric_KeyPress/*' file='Doc/FormCommon.xml'/>
    public static void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (false == e.Handled)
      {
        e.Handled = HandleNumberOrEditKey(e.KeyChar);
      }
    }

    // Does not allow spaces.
    /// <include path='items/TextBoxNoSpace_KeyPress/*' file='Doc/FormCommon.xml'/>
    public static void TextBoxNoSpace_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (false == e.Handled)
      {
        e.Handled = HandleSpace(e.KeyChar);
      }
    }

    // Strips blanks from the text value.
    /// <include path='items/TextBoxNoSpace_TextChanged/*' file='Doc/FormCommon.xml'/>
    public static void TextBoxNoSpace_TextChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textBox
        && textBox.Text.Contains(" "))
      {
        int selectionStart = textBox.SelectionStart;
        textBox.Text = StripBlanks(textBox.Text);
        textBox.SelectionStart = selectionStart;
      }
    }
    #endregion

    #region File Functions

    // Execute a program with the selected file.
    /// <summary>
    /// Execute a program with the selected file.
    /// </summary>
    /// <param name="programName">The program name.</param>
    /// <param name="fileSpec">The default file specification.</param>
    /// <param name="initialDirectory">The initial directory.</param>
    /// <remarks>
    /// Defaults to the current directory if the initialDirectory parameter
    /// is null.
    /// </remarks>
    public static void ShellFile(string programName = null, string fileSpec = null
      , string initialDirectory = null)
    {
      string filter = "Text|.txt|XML|*.xml|All|*.*";

      if (null == initialDirectory)
      {
        initialDirectory = Directory.GetCurrentDirectory();
      }
      var filePath = SelectFile(filter, initialDirectory, fileSpec);
      if (filePath != null)
      {
        ShellProgram(programName, filePath);
      }
    }

    // Executes an external program.
    /// <summary>
    /// Executes an external program.
    /// </summary>
    /// <param name="programName">The program name.</param>
    /// <param name="arguments">The program arguments.</param>
    public static void ShellProgram(string programName, string arguments = null)
    {
      ProcessStartInfo startInfo;
      bool success = true;

      startInfo = new ProcessStartInfo()
      {
        Arguments = arguments,
        FileName = programName,
        UseShellExecute = true
      };

      // If no programName, then arguments must contain only a
      // file specification.
      if (null == programName)
      {
        if (false == File.Exists(arguments))
        {
          success = false;
          string message = $"The File '{arguments}'\r\n was not found.";
          MessageBox.Show(message, "ShellProgram Error", MessageBoxButtons.OK
            , MessageBoxIcon.Error);
        }
        else
        {
          string filePath = Path.GetDirectoryName(arguments);
          string fileName = Path.GetFileName(arguments);
          startInfo = new ProcessStartInfo()
          {
            FileName = fileName,
            UseShellExecute = true,
            WorkingDirectory = filePath
          };
        }
      }

      if (success)
      {
        Process.Start(startInfo);
      }
    }

    // Displays the Save dialog to select a file.
    /// <include path='items/SaveFile/*' file='Doc/FormCommon.xml'/>
    public static string SaveFile(string filter, string initialDirectory = null
      , string defaultFileSpec = null)
    {
      SaveFileDialog saveDialog;
      string retValue = null;

      using (saveDialog = new SaveFileDialog())
      {
        saveDialog.Filter = filter;
        saveDialog.RestoreDirectory = true;
        if (initialDirectory != null)
        {
          saveDialog.InitialDirectory = initialDirectory;
        }
        saveDialog.FileName = defaultFileSpec;
        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
          retValue = saveDialog.FileName;
        }
      }
      return retValue;
    }

    // Displays the Open dialog to select a file.
    /// <include path='items/SelectFile/*' file='Doc/FormCommon.xml'/>
    public static string SelectFile(string filter, string initialDirectory = null
      , string defaultFileSpec = null)
    {
      OpenFileDialog openDialog;
      string retValue = null;

      using (openDialog = new OpenFileDialog())
      {
        openDialog.Filter = filter;
        openDialog.RestoreDirectory = true;
        if (initialDirectory != null)
        {
          openDialog.InitialDirectory = initialDirectory;
        }
        openDialog.FileName = defaultFileSpec;
        if (openDialog.ShowDialog() == DialogResult.OK)
        {
          retValue = openDialog.FileName;
        }
      }
      return retValue;
    }
    #endregion

    #region Image Functions

    // Crops an image.
    /// <include path='items/CropImage/*' file='Doc/FormCommon.xml'/>
    public static Image CropImage(Image image, Point cropPoint, Size cropSize)
    {
      Bitmap retValue;

      // Draw the original image into the cropped image.
      retValue = new Bitmap(cropSize.Width, cropSize.Height);
      retValue.SetResolution(image.HorizontalResolution, image.VerticalResolution);
      using (Graphics graphics = Graphics.FromImage(retValue))
      {
        graphics.Clear(Color.Black);
        Rectangle area = new Rectangle(cropPoint.X, cropPoint.Y, cropSize.Width, cropSize.Height);
        graphics.DrawImage(image, 0, 0, area, GraphicsUnit.Pixel);
      }
      return retValue;
    }

    // Draws a gradient in the specified rectangle.
    /// <include path='items/CreateGradient/*' file='Doc/FormCommon.xml'/>
    public static void CreateGradient(Graphics graphics, Rectangle clientRectangle
      , Color beginColor, Color endColor)
    {
      LinearGradientBrush brush;
      Rectangle fillRectangle;

      // Draw the border.
      graphics.DrawRectangle(Pens.LightSlateGray, clientRectangle.X, clientRectangle.Y
        , clientRectangle.Width - 1, clientRectangle.Height - 1);

      // Fill with gradient.
      fillRectangle = new Rectangle(clientRectangle.X + 1, clientRectangle.Y + 1
        , clientRectangle.Width - 2, clientRectangle.Height - 2);
      brush = new LinearGradientBrush(fillRectangle, beginColor, endColor
        , LinearGradientMode.Vertical);
      graphics.FillRectangle(brush, fillRectangle);
      brush.Dispose();
    }

    // Resizes an image.
    /// <include path='items/ResizeImage/*' file='Doc/FormCommon.xml'/>
    public static Image ResizeImage(Image image, Size controlSize
      , bool keepAspectRatio = true)
    {
      int newWidth;
      int newHeight;
      Image retValue = image;

      // Resize if image is larger than control.
      if (image.Width > controlSize.Width
        || image.Height > controlSize.Height)
      {
        if (keepAspectRatio)
        {
          // Calculate percentage of control size to image size.
          float percentWidth = (float)controlSize.Width / image.Width;
          float percentHeight = (float)controlSize.Height / image.Height;

          // Adjust to the smaller control percentage so the wider will still fit.
          // This keeps the aspect ratio.
          float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
          newWidth = (int)(image.Width * percent);
          newHeight = (int)(image.Height * percent);
        }
        else
        {
          newWidth = controlSize.Width;
          newHeight = controlSize.Height;
        }

        // Draw the original image into the resized image.
        retValue = new Bitmap(newWidth, newHeight);
        using (Graphics graphics = Graphics.FromImage(retValue))
        {
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.DrawImage(image, 0, 0, newWidth, newHeight);
        }
      }
      return retValue;
    }

    // Transforms the crop rectangle values of the sample image relative to the
    /// <include path='items/TransformCrop/*' file='Doc/FormCommon.xml'/>
    public static Rectangle TransformCrop(Rectangle selection, Image selectionImage, Image originalImage)
    {
      Rectangle retValue = new Rectangle();

      // Get crop selection percentages.
      float percentX = (float)selection.X / selectionImage.Width;
      float percentWidth = (float)selection.Width / selectionImage.Width;
      float percentY = (float)selection.Y / selectionImage.Height;
      float percentHeight = (float)selection.Height / selectionImage.Height;

      // Calculate crop relative to original image.
      retValue.X = selection.X;
      retValue.Width = selection.Width;
      if (originalImage.Width > selectionImage.Width)
      {
        retValue.X = (int)(originalImage.Width * percentX);
        retValue.Width = (int)(originalImage.Width * percentWidth);
      }
      retValue.Y = selection.Y;
      retValue.Height = selection.Height;
      if (originalImage.Height > selectionImage.Height)
      {
        retValue.Y = (int)(originalImage.Height * percentY);
        retValue.Height = (int)(originalImage.Height * percentHeight);
      }
      return retValue;
    }
    #endregion

    #region Screen Point Funtions

    // Gets the Grid target Dialog screen position.
    /// <include path='items/GetDialogScreenPoint/*' file='Doc/FormCommon.xml'/>
    public static Point GetDialogScreenPoint(DataGridView grid)
    {
      Rectangle rectangle = GetScreenRectangle(grid);
      Point gridPoint = new Point((rectangle.X + rectangle.Width) / 8
        , (rectangle.Y + rectangle.Height) / 8);
      var retValue = grid.Parent.PointToScreen(gridPoint);
      return retValue;
    }

    // Converts the Control point to Screen point.
    /// <include path='items/GetScreenPoint/*' file='Doc/FormCommon.xml'/>
    public static Point GetScreenPoint(Control control, int x, int y)
    {
      Control parent = control.Parent;
      Point controlPoint = new Point(x, y);
      Point retValue = parent.PointToScreen(controlPoint);
      return retValue;
    }

    // Gets the Control screen rectangle.
    /// <include path='items/GetScreenRectangle/*' file='Doc/FormCommon.xml'/>
    public static Rectangle GetScreenRectangle(Control control)
    {
      Point topLeft = GetScreenPoint(control, control.Left, control.Top);
      Point bottomRight = GetScreenPoint(control, control.Left + control.Right
        , control.Top + control.Bottom);
      Rectangle retValue = new Rectangle(topLeft.X, topLeft.Y
        , bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y - control.Top);
      return retValue;
    }

    // Get the control target menu screen position.
    /// <include path='items/GetMenuScreenPoint/*' file='Doc/FormCommon.xml'/>
    public static Point GetMenuScreenPoint(Control control
      , Point mousePosition)
    {
      Point retValue = mousePosition;
      Rectangle rectangle = GetScreenRectangle(control);
      if (false == rectangle.Contains(mousePosition))
      {
        Point point = new Point((control.Left + control.Right) / 4
          , (control.Top + control.Bottom) / 4);
        retValue = GetScreenPoint(control, point.X, point.Y);
      }
      return retValue;
    }
    #endregion

    #region String Value Functions

    // Sets the string to "-" if it is empty or blanks. and to "" if it is "-".
    /// <include path='items/SetString/*' file='Doc/FormCommon.xml'/>
    public static string SetString(string text)
    {
      string retValue = text;

      if (string.IsNullOrWhiteSpace(retValue))
      {
        retValue = "-";
      }
      else
      {
        retValue = text.Trim();
        if ("-" == retValue)
        {
          retValue = "";
        }
      }
      return retValue;
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="text"></param>
    ///// <returns></returns>
    //public static string ReSetString(string text)
    //{
    //	string retValue = text;

    //	if (false == string.IsNullOrWhiteSpace(retValue))
    //	{
    //		if ("-" == text.Trim())
    //		{
    //			retValue = "";
    //		}
    //	}
    //	return retValue;
    //}
    #endregion

    #region Class Data

    /// <summary>The Delete Confirmation message.</summary>
    public static string DeleteConfirm = "Are you sure you want to delete the selected item?";

    /// <summary>The Delete Error message.</summary>
    public static string DeleteError = "Unable to delete the selected item.\r\n"
        + "There may be attached items or referencing items.";
    #endregion
  }
}
