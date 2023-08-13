// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestForm.cs
using System;
using System.Windows.Forms;

namespace LJCGridDataTests
{
  public partial class TestForm : Form
  {
    public TestForm()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      var gridDataTest = new GridDataTests(TestDataGrid);
      gridDataTest.Run();
    }
  }
}
