// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SelectTable.js

// Represents a select capable HTML table.
class SelectTable
{
  // Gets the row parent table.
  static GetRowTable(eRow)
  {
    let retValue = null;

    let success = SelectTable.IsTRow(eRow);
    let tbody = null;
    if (success)
    {
      // table/tbody/tr
      tbody = eRow.parentElement;
      success = SelectTable.IsTBody(tbody);
    }

    let table = null;
    if (success)
    {
      // table/tbody
      table = tbody.parentElement;
      if (SelectTable.IsTable(table))
      {
        retValue = table;
      }
    }
    return retValue;
  }

  // Gets parent table if target is TD.
  static GetTable(eTarget)
  {
    let retValue = null;

    let row = SelectTable.GetTableRow(eTarget);
    if (row != null)
    {
      retValue = SelectTable.GetRowTable(row);
    }
    return retValue;
  }

  // Gets parent row if target is TD.
  static GetTableRow(eTarget)
  {
    let retValue = null;

    let success = SelectTable.IsTData(eTarget);
    if (success)
    {
      // tr/td
      let row = eTarget.parentElement;
      if (SelectTable.IsTRow(row))
      {
        retValue = row;
      }
    }
    return retValue;
  }

  // Checks if element is a table element.
  static IsTable(element)
  {
    let retValue = false;

    if ("TABLE" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if element is a table body element.
  static IsTBody(element)
  {
    let retValue = false;

    if ("TBODY" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if element is a table data element.
  static IsTData(element)
  {
    let retValue = false;

    if ("TD" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if element is a table row element.
  static IsTRow(element)
  {
    let retValue = false;

    if ("TR" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // The Constructor function.
  constructor(eTable)
  {
    this.Table = eTable;
    this.PreviousSelectedRow = null;
    this.SelectedRow = null;
    this.SelectedColor = "lightsteelblue";
    this.InitialColor = ""

    this.Table.addEventListener("click", this.Click.bind(this));
  }

  // Event Handlers

  // Selects the clicked row.
  Click(event)
  {
    this.SelectRow(event.target);
  }

  // Functions

  // Selects the row if in the contained Table element.
  SelectRow(eTarget)
  {
    let retValue = false;

    let table = SelectTable.GetTable(eTarget);
    if (table != null)
    {
      if (table.id == this.Table.id)
      {
        let row = SelectTable.GetTableRow(eTarget);
        if (row != null)
        {
          this.PreviousSelectedRow = this.SelectedRow;
          this.SelectedRow = row;
          this.SetSelectColors(this.SelectedRow);
        }
        retValue = true;
      }
    }
    return retValue;
  }

  // Sets selected color.
  SetSelectColors(eRow)
  {
    let success = true;
    if (!SelectTable.IsTRow(eRow))
    {
      success = false;
    }

    if (success)
    {
      if (this.PreviousSelectedRow != null)
      {
        this.PreviousSelectedRow.style.backgroundColor = this.InitialColor;
      }
      eRow.style.backgroundColor = this.SelectedColor;
    }
  }
}