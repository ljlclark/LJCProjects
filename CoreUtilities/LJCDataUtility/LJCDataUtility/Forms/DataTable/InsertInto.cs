// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InsertInto.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJCDataUtility
{
  // Create the InsertInto procedure.
  internal class InsertInto
  {
    #region Constructors

    // Initializes an object instance.
    public InsertInto(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Managers = Parent.Managers;
    }
    #endregion

    #region Methods

    // Create the Insert Into SQL.
    internal void InsertIntoProc()
    {
      var tableID = Parent.DataTableID();
      var tableName = Parent.DataTableName();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , orderByNames);
      if (NetCommon.HasItems(dataColumns))
      {
        StringBuilder b = new StringBuilder(256);
        string dbName = "LJCDataUtility";
        string newTableName = $"New{tableName}";
        b.AppendLine($"USE [{dbName}]");
        b.AppendLine($"SET IDENTITY_INSERT {newTableName} ON");
        b.AppendLine();

        b.AppendLine($"INSERT INTO {newTableName}");
        var proc = new ProcBuilder(dbName, newTableName);
        // Parens, NewName, IDs.
        var insertList = proc.ColumnsList(dataColumns, true
          , true, true);
        insertList = insertList.Substring(2);
        b.AppendLine(insertList);

        b.AppendLine("select");
        // No parens, NewName value, NewMaxLength value.
        var selectList = InsertSelectList(dataColumns);
        b.AppendLine(selectList);
        b.AppendLine($"FROM {tableName};");

        b.AppendLine();
        b.AppendLine($"SET IDENTITY_INSERT {newTableName} OFF");
        var showText = b.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Create custom InsertSelect list.
    // (No parens, NewName value, NewMaxLength value)
    private string InsertSelectList(DataColumns dataColumns)
    {
      var b = new StringBuilder(256);
      var value = "  ";
      b.Append(value);
      var lineLength = value.Length;

      var first = true;
      foreach (DataUtilColumn column in dataColumns)
      {
        var nameValue = column.Name;
        lineLength += nameValue.Length;

        // Calculate length before adding to insert newline.
        var addNewline = false;
        if ("Name" == nameValue)
        {
          addNewline = true;
          nameValue = "Name = ISNULL(NewName, Name)";
          lineLength = 81;
        }
        if ("MaxLength" == nameValue)
        {
          addNewline = true;
          nameValue = "MaxLength =\r\n";
          nameValue += "      CASE NewMaxLength\r\n";
          nameValue += "        WHEN 0 THEN MaxLength ELSE NewMaxLength\r\n";
          nameValue += "      END";
          lineLength = 81;
        }

        if (lineLength > 80)
        {
          var newLine = "\r\n    ";
          b.Append(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
          lineLength += nameValue.Length;
        }

        if (!first)
        {
          var firstValue = ", ";
          b.Append(firstValue);
          lineLength += firstValue.Length;
        }
        first = false;

        b.Append(nameValue);
        if (addNewline)
        {
          var newLine = "\r\n    ";
          b.Append(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
        }
      }

      var retSelect = b.ToString();
      return retSelect;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
