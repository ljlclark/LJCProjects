// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SelectTable.js

// Contains CityList event handlers.
class SelectTable
{
  // The Constructor function.
  constructor()
  {
    this.PreviousSelectedRow = null;
    this.SelectedTable = null;
    this.SelectedRow = null;
    this.SelectedColor = "lightsteelblue";
    this.InitialColor = "";
  }

  // Checks if row belongs to the table.
  // Used SelectedTAble IDif tableID is missing.
  IsContainedRow(eTableRow, tableID = null)
  {
    let retValue = false;

    let process = true;
    eTableRow = this.DefaultRow(eTableRow);
    if (eTableRow.tagName != "TR")
    {
      process = false;
    }

    let tbody = null;
    if (process)
    {
      tbody = eTableRow.parentElement;
      if (tbody.tagName != "TBODY")
      {
        process = false;
      }
    }

    let table = null;
    if (process)
    {
      table = tbody.parentElement;
      if (table.tagName != "TABLE")
      {
        process = false;
      }
    }

    if (process)
    {
      tableID = this.DefaultTableID(tableID);
      if (table.id == tableID)
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

    let tr = null;
    let tbody = null;
    let table = null;
    let process = true;
    if (element.tagName != "TD")
    {
      process = false;
    }

    if (process)
    {
      // tr/td
      tr = element.parentElement;
      if (tr.tagName != "TR")
      {
        process = false;
      }
    }

    if (process)
    {
      // table/tbody/tr
      this.PreviousSelectedRow = this.SelectedRow;
      this.SelectedRow = tr;
      tbody = tr.parentElement;
      if (tbody.tagName != "TBODY")
      {
        process = false;
      }
    }

    if (process)
    {
      table = tbody.parentElement;
      if (table.tagName != "TABLE")
      {
        process = false;
      }
    }

    if (process)
    {
      this.SelectedTable = table;
      retValue = true;
    }
    return retValue;
  }

  // Sets selected color.
  // Used SelectedTAble IDif tableID is missing.
  SelectRow(eTableRow = null, tableID = null)
  {
    let process = true;
    eTableRow = this.DefaultRow(eTableRow);
    if (eTableRow.tagName != "TR")
    {
      process = false;
    }

    if (process)
    {
      tableID = this.DefaultTableID(tableID);
      if (this.IsContainedRow(eTableRow, tableID))
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

  // Gets current tableID if tableID is null and current ID is available.
  DefaultTableID(tableID)
  {
    let retValue = tableID;

    if (null == tableID)
    {
      if (this.SelectedTable != null)
      {
        retValue = this.SelectedTable.id;
      }
    }
    return retValue;
  }
}