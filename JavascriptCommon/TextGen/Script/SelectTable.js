// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SelectTable.js

// Represents a select capable HTML table.
class SelectTable
{
  // Gets the row parent table.
  static GetRowTable(eTRow)
  {
    let retValue = null;

    let success = SelectTable.IsTRow(eTRow);
    let tBody = null;
    if (success)
    {
      // table/tbody/tr
      tBody = eTRow.parentElement;
      success = SelectTable.IsTBody(tBody);
    }

    let table = null;
    if (success)
    {
      // table/tbody
      table = tBody.parentElement;
      if (SelectTable.IsTable(table))
      {
        retValue = table;
      }
    }
    return retValue;
  }

  // Gets parent table if target is TD.
  static GetTable(eTData)
  {
    let retValue = null;

    let tRow = SelectTable.GetTableRow(eTData);
    if (tRow != null)
    {
      retValue = SelectTable.GetRowTable(tRow);
    }
    return retValue;
  }

  // Gets parent row if target is TD.
  static GetTableRow(eTData)
  {
    let retValue = null;

    if (SelectTable.IsTData(eTData))
    {
      // tr/td
      let tRow = eTData.parentElement;
      if (SelectTable.IsTRow(tRow))
      {
        retValue = tRow;
      }
    }
    return retValue;
  }

  // Checks if element is a table element.
  static IsTable(eTable)
  {
    let retValue = false;

    if ("TABLE" == eTable.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if element is a table body element.
  static IsTBody(eTBody)
  {
    let retValue = false;

    if ("TBODY" == eTBody.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if element is a table data element.
  static IsTData(eTData)
  {
    let retValue = false;

    if ("TD" == eTData.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if element is a table row element.
  static IsTRow(eTRow)
  {
    let retValue = false;

    if ("TR" == eTRow.tagName)
    {
      retValue = true;
    }
    return retValue;
  }

  // The Constructor function.
  constructor(eTable, callback)
  {
    this.Table = eTable;
    this.Callback = callback;
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

  // Gets the row data element.
  GetTData(rowIndex = 0, dataIndex = 0)
  {
    let retValue = null;

    let tBody = this.Table.children[rowIndex];
    let tRow = tBody.children[0];
    retValue = this.GetRowTData(tRow, dataIndex);
    return retValue;
  }

  // Gets the cell data.
  GetTDataText(rowIndex = 0, dataIndex = 0)
  {
    let tData = this.GetTData(rowIndex, dataIndex);
    let retValue = tData.innerText;
    return retValue;

  }

  // Gets the row data element.
  GetRowTData(tRow, dataIndex = 0)
  {
    let retValue = null;

    if (SelectTable.IsTRow(tRow))
    {
      retValue = tRow.children[dataIndex];
    }
    return retValue;
  }

  // Gets the row cell data.
  GetRowTDataText(tRow, dataIndex = 0)
  {
    let tData = this.GetRowTData(tRow, dataIndex);
    let retValue = tData.innerText;
    return retValue;
  }

  // Selects the row if in the contained Table element.
  // TextGenCode.CrateItemRows();
  // TextGenCode.CrateSectionsRows();
  // this.Click();
  SelectRow(eTData)
  {
    let retValue = null;

    let table = SelectTable.GetTable(eTData);
    if (table != null)
    {
      if (table.id == this.Table.id)
      {
        let tRow = SelectTable.GetTableRow(eTData);
        if (tRow != null)
        {
          this.PreviousSelectedRow = this.SelectedRow;
          this.SelectedRow = tRow;
          this.SetSelectColors(this.SelectedRow);

          this.Callback(this, "Select", tRow);
          retValue = tRow;
        }
      }
    }
    return retValue;
  }

  // Sets selected color.
  SetSelectColors(eTRow)
  {
    if (SelectTable.IsTRow(eTRow))
    {
      if (this.PreviousSelectedRow != null)
      {
        this.PreviousSelectedRow.style.backgroundColor = this.InitialColor;
      }
      eTRow.style.backgroundColor = this.SelectedColor;
    }
  }
}