// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// GridSetup.cs
using System.Drawing;
using System.Windows.Forms;

namespace LJCGridDataTests
{
  // Represents the DataGridView setup.
  internal class GridSetup
  {
    // Initializes the object instance.
    public GridSetup(DataGridView grid)
    {
      mGrid = grid;
    }

    // Creates the grid columns.
    public void CreateColumns()
    {
      // *** Test Setting ***
      var gridSetupCase = SetupCase.WithAdd;
      switch (gridSetupCase)
      {
        case SetupCase.WithAdd:
          SetColumnsWithAdd();
          break;

        case SetupCase.WithObject:
          SetColumns();
          break;
      }
    }

    // Returns the calculated column unit width.
    private int ColumnUnitWidth(string text = null
      , UnitCase unitCase = UnitCase.Average, int textLength = 0)
    {
      int averageCharWidth;
      Graphics graphics;
      SizeF textSize;
      int retValue = default;

      if (textLength > 0)
      {
        unitCase = UnitCase.Average;
      }
      switch (unitCase)
      {
        case UnitCase.Average:
          if (textLength > 0)
          {
            // With average of measured alpha string.
            graphics = mGrid.CreateGraphics();
            var alphaText = "abcdefghijklmnopqrstuvwxyz";
            textSize = graphics.MeasureString(alphaText, mGrid.Font);
            averageCharWidth = (int)(textSize.Width / 26);
            retValue = textLength * averageCharWidth;
          }
          break;

        case UnitCase.Fixed:
          // With fixed average;
          averageCharWidth = 5;
          retValue = text.Length * averageCharWidth;
          break;

        case UnitCase.Measure:
          // With measured text string.
          graphics = mGrid.CreateGraphics();
          textSize = graphics.MeasureString(text, mGrid.Font);
          retValue = (int)textSize.Width;
          break;
      }
      return retValue;
    }

    // Set grid columns with column definitions.
    private void SetColumns()
    {
      DataGridViewColumnCollection columns = mGrid.Columns;
      var column = new DataGridViewColumn()
      {
        Name = "Name",
        HeaderText = "Name Caption",
        ReadOnly = true,
        Width = ColumnUnitWidth(textLength: 60)
      };
      columns.Add(column);

      column = new DataGridViewColumn()
      {
        Name = "Description",
        HeaderText = "Description",
        ReadOnly = true,
        Width = ColumnUnitWidth(textLength: 100)
      };
      columns.Add(column);

      column = new DataGridViewColumn()
      {
        Name = "Abbreviation",
        HeaderText = "Abbreviation",
        ReadOnly = true,
        Width = ColumnUnitWidth(textLength: 15)
      };
      columns.Add(column);
    }

    // Set grid columns with Add().
    private void SetColumnsWithAdd()
    {
      // Configure grid columns manually with the Add() method.
      DataGridViewColumnCollection columns = mGrid.Columns;
      int index = columns.Add("Name", "Name Caption");
      var column = columns[index];
      column.ReadOnly = true;
      column.Width = ColumnUnitWidth(textLength: 60);

      index = columns.Add("Description", "Description");
      column = columns[index];
      column.ReadOnly = true;
      column.Width = ColumnUnitWidth(textLength: 100);

      index = columns.Add("Abbreviation", "Abbreviation");
      column = columns[index];
      column.ReadOnly = true;
      column.Width = ColumnUnitWidth(textLength: 15);
    }

    private readonly DataGridView mGrid;

    private enum SetupCase
    {
      WithAdd,
      WithObject
    }

    private enum UnitCase
    {
      Average,
      Fixed,
      Measure
    }
  }
}
