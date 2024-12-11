namespace LJCDataUtility
{
	partial class TableNameSelect
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
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.ConfigNameCombo = new System.Windows.Forms.ComboBox();
      this.ConfigNameLabel = new System.Windows.Forms.Label();
      this.TableNameCombo = new System.Windows.Forms.ComboBox();
      this.TableNameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(417, 91);
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
      this.OKButton.Location = new System.Drawing.Point(295, 91);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 4;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ConfigNameCombo
      // 
      this.ConfigNameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ConfigNameCombo.Location = new System.Drawing.Point(185, 17);
      this.ConfigNameCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ConfigNameCombo.Name = "ConfigNameCombo";
      this.ConfigNameCombo.Size = new System.Drawing.Size(343, 28);
      this.ConfigNameCombo.TabIndex = 1;
      this.ConfigNameCombo.SelectedIndexChanged += new System.EventHandler(this.ConfigNameCombo_SelectedIndexChanged);
      this.ConfigNameCombo.TextChanged += new System.EventHandler(this.ConfigNameTextBox_TextChanged);
      this.ConfigNameCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConfigNameTextBox_KeyPress);
      // 
      // ConfigNameLabel
      // 
      this.ConfigNameLabel.Location = new System.Drawing.Point(18, 20);
      this.ConfigNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.ConfigNameLabel.Name = "ConfigNameLabel";
      this.ConfigNameLabel.Size = new System.Drawing.Size(164, 20);
      this.ConfigNameLabel.TabIndex = 0;
      this.ConfigNameLabel.Text = "Data Config Name";
      // 
      // TableNameCombo
      // 
      this.TableNameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.TableNameCombo.Location = new System.Drawing.Point(185, 53);
      this.TableNameCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TableNameCombo.Name = "TableNameCombo";
      this.TableNameCombo.Size = new System.Drawing.Size(343, 28);
      this.TableNameCombo.TabIndex = 3;
      this.TableNameCombo.TextChanged += new System.EventHandler(this.TableNameTextBox_TextChanged);
      this.TableNameCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TableNameTextBox_KeyPress);
      // 
      // TableNameLabel
      // 
      this.TableNameLabel.Location = new System.Drawing.Point(18, 56);
      this.TableNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.TableNameLabel.Name = "TableNameLabel";
      this.TableNameLabel.Size = new System.Drawing.Size(164, 20);
      this.TableNameLabel.TabIndex = 2;
      this.TableNameLabel.Text = "Table Name";
      // 
      // TableNameSelect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(548, 138);
      this.Controls.Add(this.TableNameLabel);
      this.Controls.Add(this.TableNameCombo);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.ConfigNameCombo);
      this.Controls.Add(this.ConfigNameLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TableNameSelect";
      this.ShowInTaskbar = false;
      this.Text = "Table Name Selection";
      this.Load += new System.EventHandler(this.CreateDataDetail_Load);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ComboBox ConfigNameCombo;
		private System.Windows.Forms.Label ConfigNameLabel;
		private System.Windows.Forms.ComboBox TableNameCombo;
		private System.Windows.Forms.Label TableNameLabel;
	}
}