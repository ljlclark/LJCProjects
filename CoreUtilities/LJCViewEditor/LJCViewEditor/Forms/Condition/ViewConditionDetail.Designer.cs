namespace LJCViewEditor
{
	partial class ViewConditionDetail
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.SecondValueTextbox = new System.Windows.Forms.TextBox();
			this.SecondValueLabel = new System.Windows.Forms.Label();
			this.FirstValueCombo = new System.Windows.Forms.ComboBox();
			this.FirstValueLabel = new System.Windows.Forms.Label();
			this.ParentTextbox = new System.Windows.Forms.TextBox();
			this.ParentLabel = new System.Windows.Forms.Label();
			this.ComparisonTextbox = new System.Windows.Forms.TextBox();
			this.ComparisonLabel = new System.Windows.Forms.Label();
			this.ConditionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ConditionHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ConditionMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(507, 160);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 9;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(386, 160);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 8;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// SecondValueTextbox
			// 
			this.SecondValueTextbox.Location = new System.Drawing.Point(185, 86);
			this.SecondValueTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SecondValueTextbox.Name = "SecondValueTextbox";
			this.SecondValueTextbox.Size = new System.Drawing.Size(433, 26);
			this.SecondValueTextbox.TabIndex = 5;
			// 
			// SecondValueLabel
			// 
			this.SecondValueLabel.Location = new System.Drawing.Point(11, 91);
			this.SecondValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SecondValueLabel.Name = "SecondValueLabel";
			this.SecondValueLabel.Size = new System.Drawing.Size(170, 20);
			this.SecondValueLabel.TabIndex = 4;
			this.SecondValueLabel.Text = "Second Value";
			// 
			// FirstValueCombo
			// 
			this.FirstValueCombo.Location = new System.Drawing.Point(185, 50);
			this.FirstValueCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FirstValueCombo.Name = "FirstValueCombo";
			this.FirstValueCombo.Size = new System.Drawing.Size(433, 28);
			this.FirstValueCombo.TabIndex = 3;
			// 
			// FirstValueLabel
			// 
			this.FirstValueLabel.Location = new System.Drawing.Point(11, 55);
			this.FirstValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FirstValueLabel.Name = "FirstValueLabel";
			this.FirstValueLabel.Size = new System.Drawing.Size(170, 20);
			this.FirstValueLabel.TabIndex = 2;
			this.FirstValueLabel.Text = "First Value";
			// 
			// ParentTextbox
			// 
			this.ParentTextbox.Location = new System.Drawing.Point(185, 14);
			this.ParentTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentTextbox.Name = "ParentTextbox";
			this.ParentTextbox.ReadOnly = true;
			this.ParentTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentTextbox.TabIndex = 1;
			this.ParentTextbox.TabStop = false;
			// 
			// ParentLabel
			// 
			this.ParentLabel.Location = new System.Drawing.Point(11, 20);
			this.ParentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentLabel.Name = "ParentLabel";
			this.ParentLabel.Size = new System.Drawing.Size(170, 20);
			this.ParentLabel.TabIndex = 0;
			this.ParentLabel.Text = "Condition Set Name";
			// 
			// ComparisonTextbox
			// 
			this.ComparisonTextbox.Location = new System.Drawing.Point(185, 122);
			this.ComparisonTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ComparisonTextbox.Name = "ComparisonTextbox";
			this.ComparisonTextbox.Size = new System.Drawing.Size(433, 26);
			this.ComparisonTextbox.TabIndex = 7;
			// 
			// ComparisonLabel
			// 
			this.ComparisonLabel.Location = new System.Drawing.Point(11, 127);
			this.ComparisonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ComparisonLabel.Name = "ComparisonLabel";
			this.ComparisonLabel.Size = new System.Drawing.Size(170, 20);
			this.ComparisonLabel.TabIndex = 6;
			this.ComparisonLabel.Text = "Comparison Operator";
			// 
			// ConditionMenu
			// 
			this.ConditionMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ConditionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConditionHelp});
			this.ConditionMenu.Name = "ConditionMenu";
			this.ConditionMenu.Size = new System.Drawing.Size(153, 34);
			// 
			// ConditionHelp
			// 
			this.ConditionHelp.Name = "ConditionHelp";
			this.ConditionHelp.ShortcutKeyDisplayString = "F1";
			this.ConditionHelp.Size = new System.Drawing.Size(152, 30);
			this.ConditionHelp.Text = "&Help";
			this.ConditionHelp.Click += new System.EventHandler(this.ConditionHelp_Click);
			// 
			// ViewConditionDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 204);
			this.ContextMenuStrip = this.ConditionMenu;
			this.Controls.Add(this.ComparisonTextbox);
			this.Controls.Add(this.ComparisonLabel);
			this.Controls.Add(this.SecondValueTextbox);
			this.Controls.Add(this.SecondValueLabel);
			this.Controls.Add(this.FirstValueCombo);
			this.Controls.Add(this.FirstValueLabel);
			this.Controls.Add(this.ParentTextbox);
			this.Controls.Add(this.ParentLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ViewConditionDetail";
			this.Text = "ViewCondition Detail";
			this.Load += new System.EventHandler(this.ViewConditionDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewConditionDetail_KeyDown);
			this.ConditionMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox SecondValueTextbox;
		private System.Windows.Forms.Label SecondValueLabel;
		private System.Windows.Forms.ComboBox FirstValueCombo;
		private System.Windows.Forms.Label FirstValueLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.TextBox ComparisonTextbox;
		private System.Windows.Forms.Label ComparisonLabel;
		private System.Windows.Forms.ContextMenuStrip ConditionMenu;
		private System.Windows.Forms.ToolStripMenuItem ConditionHelp;
	}
}