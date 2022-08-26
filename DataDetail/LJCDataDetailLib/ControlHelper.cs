// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataDetailLib
{
  /// <summary>Contains methods for creating Controls.</summary>
  public class ControlHelper
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ControlHelper()
		{
			DefaultLabelHeight = 16;
			DefaultTextBoxHeight = 26;
			DefaultComboBoxHeight = 28;
			DefaultCheckBoxHeight = 24;
			DefaultButtonWidth = 112;
			DefaultButtonHeight = 35;
		}
		#endregion

		#region Public Methods

		// Creates a new Label control. 
		/// <include path='items/CreateLabel/*' file='Doc/ControlHelper.xml'/>
		public Label CreateLabel(string name, string text, Point location
			, int width = 100)
		{
			Label retValue;

			retValue = new Label()
			{
				Name = name,
				Location = location,
				Size = new Size(width, DefaultLabelHeight),
				TabIndex = 0,
				Text = text,
				AutoSize = false
			};
			return retValue;
		}

		// Creates a new TextBox control. 
		/// <include path='items/CreateTextBox/*' file='Doc/ControlHelper.xml'/>
		public TextBox CreateTextBox(string name, string text, Point location
			, int width = 100)
		{
			TextBox retValue;

			retValue = new TextBox()
			{
				Name = name,
				Location = location,
				Size = new Size(width, DefaultTextBoxHeight),
				TabIndex = 0,
				Text = text
			};
			return retValue;
		}

		// Create a new ComboBox control. 
		/// <include path='items/CreateComboBox/*' file='Doc/ControlHelper.xml'/>
		public ComboBox CreateComboBox(string name, Point location, int width = 121)
		{
			ComboBox retValue;

			retValue = new ComboBox()
			{
				Name = name,
				Location = location,
				Size = new Size(width, DefaultComboBoxHeight),
				TabIndex = 0,
				FormattingEnabled = true,
				DropDownStyle = ComboBoxStyle.DropDownList
			};
			return retValue;
		}

		// Creates a new CheckBox control.
		/// <include path='items/CreateCheckBox/*' file='Doc/ControlHelper.xml'/>
		public CheckBox CreateCheckBox(string name, string text, Point location
			, int width = 100)
		{
			CheckBox retValue;

			retValue = new CheckBox()
			{
				Name = name,
				Location = location,
				Size = new Size(width, DefaultCheckBoxHeight),
				TabIndex = 0,
				Text = text,
				AutoSize = true,
				UseVisualStyleBackColor = true,
			};
			return retValue;
		}

		// Create a new Button control. 
		/// <include path='items/CreateButton/*' file='Doc/ControlHelper.xml'/>
		public Button CreateButton(string name, string text, Point location)
		{
			Button retValue;

			retValue = new Button()
			{
				Name = name,
				Location = location,
				Size = new Size(DefaultButtonWidth, DefaultButtonHeight),
				Margin = new Padding(4, 5, 4, 5),
				TabIndex = 0,
				Text = text,
				UseVisualStyleBackColor = true
			};
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the Default Label height.</summary>
		public int DefaultLabelHeight { get; set; }

		/// <summary>Gets or sets the Default TextBox height.</summary>
		public int DefaultTextBoxHeight { get; set; }

		/// <summary>Gets or sets the Default ComboBox height.</summary>
		public int DefaultComboBoxHeight { get; set; }

		/// <summary>Gets or sets the Default CheckBox height.</summary>
		public int DefaultCheckBoxHeight { get; set; }

		/// <summary>Gets or sets the Default Button width.</summary>
		public int DefaultButtonWidth { get; set; }

		/// <summary>Gets or sets the Default Button height.</summary>
		public int DefaultButtonHeight { get; set; }
		#endregion
	}
}
