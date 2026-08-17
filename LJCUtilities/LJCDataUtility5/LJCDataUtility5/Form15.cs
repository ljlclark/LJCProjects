// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Form1.cs

// Found conflicts between different versions of "System.Configuration.ConfigurationManager"?

namespace LJCDataUtility5
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      // Setup control code.
      MainGridCode = new MainGridCode(this);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToScreen();
    }

    private MainGridCode MainGridCode { get; set; }
  }
}
