// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FormCommon5.cs
using LJCControls5;
using LJCNetCommon5;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Text;

namespace LJControls
{
  // Provides common WinForms methods.
  /// <include file='Doc/FormCommon.xml'
  ///  path='items/FormCommon/*'/>
  public class FormCommon
  {
    #region General Functions

    // Verify create of missing tables.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/CreateTablesPrompt/*'/>
    public static bool CreateTablesPrompt(string exceptionMessage, string[] fileSpecs)
    {
      bool retValue = false;

      if (!LJC.HasText(exceptionMessage))
      {
        throw new ArgumentException("message", nameof(exceptionMessage));
      }
      ArgumentNullException.ThrowIfNull(fileSpecs);

      var builder = new StringBuilder(128);
      builder.Append("Do you want to create missing tables with:\r\n");
      bool isFirst = true;
      foreach (string fileSpec in fileSpecs)
      {
        if (!isFirst)
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

    // Defaults a numeric value to negative one.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/DefaultMinusOne/*'/>
    public static string? DefaultMinusOne(object? value = null)
    {
      var retValue = "-1";

      if (value != null)
      {
        retValue = value.ToString();
      }
      return retValue;
    }

    // Defaults a numeric value to Zero.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/DefaultZero/*'/>
    public static string? DefaultZero(object? value = null)
    {
      var retValue = "0";

      if (value != null)
      {
        retValue = value.ToString();
      }
      return retValue;
    }

    // Sets the grid columns to not sortable.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/NotSortable/*'/>
    public static void NotSortable(DataGridView grid)
    {
      var notSortable = DataGridViewColumnSortMode.NotSortable;
      foreach (DataGridViewColumn column in grid.Columns)
      {
        column.SortMode = notSortable;
      }
    }

    // Restores the menu font size.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/RestoreMenuFontSize/*'/>
    public static void RestoreMenuFontSize(ContextMenuStrip menu
      , ControlValues controlValues)
    {
      var controlValue = controlValues.LJCSearchName($"{menu.Name}.FontSize");
      if (controlValue != null)
      {
        var size = controlValue.Left;
        menu.Font = new Font(menu.Font.FontFamily, size, menu.Font.Style);
      }
    }

    // Restores the splitter distance.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/RestoreSplitDistance/*'/>
    public static void RestoreSplitDistance(SplitContainer splitContainer
      , ControlValues controlValues)
    {
      if (controlValues != null)
      {
        string name = $"{splitContainer.Name}.SplitterDistance";
        var controlValue = controlValues.LJCSearchName(name);
        if (controlValue != null)
        {
          splitContainer.SplitterDistance = controlValue.Height;
        }
      }
    }

    // Restores the tabs font size.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/RestoreTabsFontSize/*'/>
    public static void RestoreTabsFontSize(TabControl tabs
      , ControlValues controlValues)
    {
      var controlValue = controlValues.LJCSearchName($"{tabs.Name}.FontSize");
      if (controlValue != null)
      {
        var size = controlValue.Left;
        tabs.Font = new Font(tabs.Font.FontFamily, size, tabs.Font.Style);
      }
    }

    // Saves the menu font size.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SaveMenuFontSize/*'/>
    public static void SaveMenuFontSize(ContextMenuStrip menu
      , ControlValues controlValues)
    {
      var size = (int)menu.Font.Size;
      controlValues.Add($"{menu.Name}.FontSize", size);
    }

    // Saves the tab font size.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SaveTabFontSize/*'/>
    public static void SaveTabFontSize(TabControl tabs
      , ControlValues controlValues)
    {
      var size = (int)tabs.Font.Size;
      controlValues.Add($"{tabs.Name}.FontSize", size);
    }

    // Sets the BackColor for the labels.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SetLabelsBackColor/*'/>
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
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SetMenuState/*'/>
    public static void SetMenuState(ContextMenuStrip contextMenuStrip, bool enableNew
      , bool enableEdit)
    {
      foreach (ToolStripItem menuItem in contextMenuStrip.Items)
      {
        if (LJC.HasText(menuItem.Name))
        {
          if (menuItem.Name.Contains("New"))
          {
            menuItem.Enabled = enableNew;
          }
          else
          {
            if (!menuItem.Name.EndsWith("Exit")
              && !menuItem.Name.EndsWith("Close"))
            {
              menuItem.Enabled = enableEdit;
            }
          }
        }
      }
    }

    // Sets the enable state for the tool items.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SetToolState/*'/>
    public static void SetToolState(ToolStrip toolStrip, bool enableNew, bool enableEdit)
    {
      foreach (ToolStripItem toolItem in toolStrip.Items)
      {
        if (LJC.HasText(toolItem.Name))
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
    }
    #endregion

    #region Error Functions

    // Standard Add error message.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/AddError/*'/>
    public static bool AddError(Form form, int affectedCount)
    {
      bool retValue = false;

      if (0 == affectedCount)
      {
        retValue = true;
        var title = "Add Error";
        var message = "The Record was not added.";
        form.Cursor = Cursors.Default;
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Information);
      }
      return retValue;
    }

    // Standard Duplicate error message.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/DataError/*'/>
    public static void DataError(Form form)
    {
      var title = "Data Entry Error";
      var message = "The record already exists.";
      form.Cursor = Cursors.Default;
      MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    // Displays the error text if it is not null.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/ShowError/*'/>
    public static bool ShowError(string errorText, string? caption = null)
    {
      bool retValue = false;

      if (LJC.HasText(errorText))
      {
        retValue = true;
        MessageBox.Show(errorText, caption, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }

    // Displays the error text if it is not null.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/ShowHasError/*'/>
    public static bool ShowHasError(string errorText, int affectedCount = 1
      , string? caption = null)
    {
      bool retValue;

      retValue = ShowError(errorText, caption);
      if (affectedCount < 1)
      {
        retValue = true;
        if (!LJC.HasText(errorText))
        {
          errorText = "No records affected.";
          ShowError(errorText, caption);
        }
      }
      return retValue;
    }

    // Standard Update error message.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/UpdateError/*'/>
    public static bool UpdateError(Form form, int affectedCount)
    {
      bool retValue = false;

      if (0 == affectedCount)
      {
        retValue = true;
        var title = "Update Error";
        var message = "The Record was not updated.";
        form.Cursor = Cursors.Default;
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Information);
      }
      return retValue;
    }
    #endregion

    #region Field Key Handler Functions

    // Checks the string for allowed numeric values.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/IsNumber/*'/>
    public static bool IsNumber(string text)
    {
      bool retValue = true;

      foreach (char ch in text)
      {
        if (!char.IsDigit(ch)
          && ch != '-'
          && ch != '.')
        {
          retValue = false;
          break;
        }
      }
      return retValue;
    }

    // Checks the key character for a numeric or allowed control value.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/HandleNumber/*'/>
    public static bool HandleNumber(string text, char keyChar)
    {
      // Use in KeyDown
      //if (e.KeyData == (Keys.Control | Keys.V))
      bool retHandled = true;

      // Keys to let through.
      const char SYN = (char)22;
      if (SYN == keyChar)
      {
        retHandled = false;
      }
      if ('-' == keyChar
        && !text.Contains('-'))
      {
        retHandled = false;
      }
      if ('.' == keyChar
        && !text.Contains('.'))
      {
        retHandled = false;
      }
      if (retHandled)
      {
        if (char.IsDigit(keyChar)
          || (char)Keys.Back == keyChar
          || (char)Keys.Delete == keyChar)
        {
          retHandled = false;
        }
      }
      return retHandled;
    }

    // Checks the key character for a space.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/HandleSpace/*'/>
    public static bool HandleSpace(char keyChar)
    {
      bool retHandled = false;

      if (' ' == keyChar)
      {
        retHandled = true;
      }
      return retHandled;
    }

    // Strips blanks from the string.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/StripBlanks/*'/>
    public static string StripBlanks(string text)
    {
      return text.Replace(" ".ToString(), "");
    }

    // Strips non-digits from a string.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/StripNonDigits/*'/>
    public static string StripNonDigits(string text)
    {
      string retValue = "";

      foreach (char ch in text)
      {
        if (!char.IsDigit(ch))
        {
          retValue = retValue.Replace(ch.ToString(), "");
        }
      }
      return retValue;
    }

    // Does not allow spaces.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/TextNoSpaceKeyPress/*'/>
    public static void TextNoSpaceKeyPress(object sender, KeyPressEventArgs e)
    {
      if (!e.Handled)
      {
        e.Handled = HandleSpace(e.KeyChar);
      }
    }

    // Strips blanks from the text value.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/TextNoSpaceChanged/*'/>
    public static void TextNoSpaceChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textBox
        && textBox.Text.Contains(' '))
      {
        int saveStart = textBox.SelectionStart;
        textBox.Text = StripBlanks(textBox.Text);
        textBox.SelectionStart = saveStart;
      }
      if (sender is ComboBox combobox
        && combobox.Text.Contains(' '))
      {
        int saveStart = combobox.SelectionStart;
        combobox.Text = FormCommon.StripBlanks(combobox.Text);
        combobox.SelectionStart = saveStart;
      }
    }
    #endregion

    #region File Functions

    // Execute a program with the selected file.
    // <param name="programName">The program name.</param>
    // <param name="fileSpec">The default file specification.</param>
    // <param name="initialDirectory">The initial directory.</param>
    // <remarks>
    // Defaults to the current directory if the initialDirectory parameter
    // is null.
    // </remarks>
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/ShellFile/*'/>
    public static void ShellFile(string? programName = null, string? fileSpec = null
      , string? initialDirectory = null)
    {
      string filter = "Text|.txt|XML|*.xml|All|*.*";

      if (null == initialDirectory)
      {
        initialDirectory = Directory.GetCurrentDirectory();
      }
      if (fileSpec != null
        && programName != null)
      {
        var filePath = SelectFile(filter, initialDirectory, fileSpec);
        if (filePath != null)
        {
          ShellProgram(programName, filePath);
        }
      }
    }

    // Executes an external program.
    // Executes an external program.
    // <param name="programName">The program name.</param>
    // <param name="arguments">The program arguments.</param>
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/ShellProgram/*'/>
    public static void ShellProgram(string programName
      , string? arguments = null)
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
        if (!File.Exists(arguments))
        {
          success = false;
          string message = $"The File '{arguments}'\r\n was not found.";
          MessageBox.Show(message, "ShellProgram Error", MessageBoxButtons.OK
            , MessageBoxIcon.Error);
        }
        else
        {
          string? filePath = Path.GetDirectoryName(arguments);
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
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SaveFile/*'/>
    public static string? SaveFile(string filter, string? initialDirectory = null
      , string? defaultFileSpec = null)
    {
      SaveFileDialog saveDialog;
      string? retValue = null;

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
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SelectFile/*'/>
    public static string? SelectFile(string filter, string? initialDirectory = null
      , string? defaultFileSpec = null)
    {
      OpenFileDialog openDialog;
      string? retValue = null;

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
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/CropImage/*'/>
    public static Image CropImage(Image image, Point cropPoint, Size cropSize)
    {
      Bitmap retValue;

      // Draw the original image into the cropped image.
      retValue = new Bitmap(cropSize.Width, cropSize.Height);
      retValue.SetResolution(image.HorizontalResolution, image.VerticalResolution);
      using (Graphics graphics = Graphics.FromImage(retValue))
      {
        graphics.Clear(Color.Black);
        var area = new Rectangle(cropPoint.X, cropPoint.Y, cropSize.Width, cropSize.Height);
        graphics.DrawImage(image, 0, 0, area, GraphicsUnit.Pixel);
      }
      return retValue;
    }

    // Draws a gradient in the specified rectangle.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/CreateGradient/*'/>
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
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/ResizeImage/*'/>
    public static Image ResizeImage(Image image, Size controlSize
      , bool keepAspectRatio = true)
    {
      int newWidth;
      int newHeight;
      var retValue = image;

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
        using var graphics = Graphics.FromImage(retValue);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(image, 0, 0, newWidth, newHeight);
      }
      return retValue;
    }

    // Transforms the crop rectangle values of the sample image relative to the
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/TransformCrop/*'/>
    public static Rectangle TransformCrop(Rectangle selection, Image selectionImage, Image originalImage)
    {
      var retValue = new Rectangle();

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

    // Converts the Control point to Screen point.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/GetScreenPoint/*'/>
    public static Point GetScreenPoint(Control control, int x, int y)
    {
      var retPoint = new Point(-1, -1);

      if (control.Parent != null)
      {
        Control parent = control.Parent;
        var controlPoint = new Point(x, y);
        retPoint = parent.PointToScreen(controlPoint);
      }
      return retPoint;
    }

    // Gets the Control screen rectangle.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/GetScreenRectangle/*'/>
    public static Rectangle GetScreenRectangle(Control control)
    {
      var topLeft = GetScreenPoint(control, control.Left, control.Top);
      var bottomRight = GetScreenPoint(control, control.Left + control.Right
        , control.Top + control.Bottom);
      var retValue = new Rectangle(topLeft.X, topLeft.Y
        , bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y - control.Top);
      return retValue;
    }

    // Get the control target menu screen position.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/GetMenuScreenPoint/*'/>
    public static Point GetMenuScreenPoint(Control control
      , Point mousePosition)
    {
      var retValue = mousePosition;
      var rectangle = GetScreenRectangle(control);
      if (!rectangle.Contains(mousePosition))
      {
        var point = new Point((control.Left + control.Right) / 4
          , (control.Top + control.Bottom) / 4);
        retValue = GetScreenPoint(control, point.X, point.Y);
      }
      return retValue;
    }
    #endregion

    #region String Value Functions

    // Sets the string to "-null" if empty or blanks. and to "" if "-null".
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/SetString/*'/>
    public static string SetString(string text)
    {
      string retValue = text;

      if (!LJC.HasText(retValue))
      {
        retValue = "-null";
      }
      else
      {
        retValue = text.Trim();
        if ("-null" == retValue)
        {
          retValue = "";
        }
      }
      return retValue;
    }
    #endregion

    #region Class Data

    // The Delete Confirmation message.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/DeleteConfirm/*'/>
    public const string DeleteConfirm = "Are you sure you want to delete the selected item?";

    // The Delete Error message.
    /// <include file='Doc/FormCommon.xml'
    ///  path='items/DeleteError/*'/>
    public const string DeleteError = "Unable to delete the selected item.\r\n"
        + "There may be attached items or referencing items.";
    #endregion
  }
}
