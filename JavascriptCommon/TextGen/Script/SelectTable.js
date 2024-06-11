// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SelectTable.js

// Contains CityList event handlers.
class SelectTable
{
  // The Constructor function.
  constructor(eTable)
  {
    this.Table = eTable;

    this.PreviousSelectedRow = null;
    this.SelectedRow = null;
    this.SelectedColor = "lightsteelblue";
    this.InitialColor = "";
  }

  // Checks if row belongs to the table.
  IsContainedRow(eTableRow)
  {
    let retValue = false;

    eTableRow = this.DefaultRow(eTableRow);
    let process = this.IsTRow(eTableRow);

    let tbody = null;
    if (process)
    {
      tbody = eTableRow.parentElement;
      process = this.IsTBody(tbody);
    }

    let table = null;
    if (process)
    {
      table = tbody.parentElement;
      process = this.IsTable(table);
    }

    if (process)
    {
      if (table.id == this.Table.id)
      {
        retValue = true;
      }
    }
    return retValue;
  }

  // Gets the table row element if element is a table data element.
  IsTableData(element)
  {
    let retValue = false;

    let process = this.IsTData(element);

    let tr = null;
    if (process)
    {
      // tr/td
      tr = element.parentElement;
      process = this.IsTRow(tr);
    }

    let tbody = null;
    let previousSelectedRow = this.SelectedRow;
    let selectedRow = tr;
    if (process)
    {
      // table/tbody/tr
      tbody = tr.parentElement;
      process = this.IsTBody(tbody);
    }

    let table = null;
    if (process)
    {
      table = tbody.parentElement;
      process = this.IsTable(table);
    }

    if (process)
    {
      if (table.id == this.Table.id)
      {
        this.PreviousSelectedRow = previousSelectedRow;
        this.SelectedRow = selectedRow;
        retValue = true;
      }
    }
    return retValue;
  }

  // 
  IsTable(element)
  {
    let retValue = false;

    if ("TABLE" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // 
  IsTRow(element)
  {
    let retValue = false;

    if ("TR" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // 
  IsTBody(element)
  {
    let retValue = false;

    if ("TBODY" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // 
  IsTData(element)
  {
    let retValue = false;

    if ("TD" == element.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Sets selected color.
  SelectRow(eTableRow = null)
  {
    let process = true;
    eTableRow = this.DefaultRow(eTableRow);
    if (eTableRow.tagName != "TR")
    {
      process = false;
    }

    if (process)
    {
      if (this.IsContainedRow(eTableRow))
      {
        if (this.PreviousSelectedRow != null)
        {
          this.PreviousSelectedRow.style.backgroundColor = this.InitialColor;
        }
        eTableRow.style.backgroundColor = this.SelectedColor;
      }
    }
  }

  // Gets current table row if tableRow is null and current row is available.
  DefaultRow(tableRow)
  {
    let retValue = tableRow;

    if (null == tableRow)
    {
      retValue = this.SelectedRow;
    }
    return retValue;
  }
}