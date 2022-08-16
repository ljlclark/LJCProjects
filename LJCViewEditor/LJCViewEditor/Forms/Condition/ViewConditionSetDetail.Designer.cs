namespace LJCViewEditor
{
	partial class ViewConditionSetDetail
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
			this.OperatorCombo = new System.Windows.Forms.ComboBox();
			this.OperatorLabel = new System.Windows.Forms.Label();
			this.ParentTextbox = new System.Windows.Forms.TextBox();
			this.ParentLabel = new System.Windows.Forms.Label();
			this.ConditionSetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ConditionSetHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ConditionSetMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(487, 89);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 5;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(366, 89);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// OperatorCombo
			// 
			this.OperatorCombo.Location = new System.Drawing.Point(165, 50);
			this.OperatorCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OperatorCombo.Name = "OperatorCombo";
			this.OperatorCombo.Size = new System.Drawing.Size(70, 28);
			this.OperatorCombo.TabIndex = 3;
			// 
			// OperatorLabel
			// 
			this.OperatorLabel.Location = new System.Drawing.Point(11, 55);
			this.OperatorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.OperatorLabel.Name = "OperatorLabel";
			this.OperatorLabel.Size = new System.Drawing.Size(153, 20);
			this.OperatorLabel.TabIndex = 2;
			this.OperatorLabel.Text = "Boolean Operator";
			// 
			// ParentTextbox
			// 
			this.ParentTextbox.Location = new System.Drawing.Point(165, 14);
			this.ParentTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentTextbox.Name = "ParentTextbox";
			this.ParentTextbox.ReadOnly = true;
			this.ParentTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentTextbox.TabIndex = 1;
			this.ParentTextbox.TabStop = false;
			// 
			// ParentLabel
			// 
			this.ParentLabel.Location = new System.Drawing.Point(11, 19);
			this.ParentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentLabel.Name = "ParentLabel";
			this.ParentLabel.Size = new System.Drawing.Size(153, 20);
			this.ParentLabel.TabIndex = 0;
			this.ParentLabel.Text = "Filter";
			// 
			// ConditionSetMenu
			// 
			this.ConditionSetMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ConditionSetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConditionSetHelp});
			this.ConditionSetMenu.Name = "ConditionSetMenu";
			this.ConditionSetMenu.Size = new System.Drawing.Size(241, 67);
			// 
			// ConditionSetHelp
			// 
			this.ConditionSetHelp.Name = "ConditionSetHelp";
			this.ConditionSetHelp.ShortcutKeyDisplayString = "F1";
			this.ConditionSetHelp.Size = new System.Drawing.Size(240, 30);
			this.ConditionSetHelp.Text = "&Help";
			this.ConditionSetHelp.Click += new System.EventHandler(this.ConditionSetHelp_Click);
			// 
			// ViewConditionSetDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612, 133);
			this.ContextMenuStrip = this.ConditionSetMenu;
			this.Controls.Add(this.OperatorCombo);
			this.Controls.Add(this.OperatorLabel);
			this.Controls.Add(this.ParentTextbox);
			this.Controls.Add(this.ParentLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ViewConditionSetDetail";
			this.Text = "ViewConditionSet Detail";
			this.Load += new System.EventHandler(this.ViewColumnSetDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewConditionSetDetail_KeyDown);
			this.ConditionSetMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ComboBox OperatorCombo;
		private System.Windows.Forms.Label OperatorLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.ContextMenuStrip ConditionSetMenu;
		private System.Windows.Forms.ToolStripMenuItem ConditionSetHelp;
	}
}