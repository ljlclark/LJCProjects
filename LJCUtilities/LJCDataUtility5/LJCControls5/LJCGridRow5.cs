// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGridRow5.cs

using LJCNetCommon5;
using System.Data;

namespace LJCControls5
{
  public class LJCGridRow5 : DataGridViewRow
  {
    #region Constructors

    // Instantiates an instance of the class.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public LJCGridRow5()
    {
      _ShortShorts = [];
      _StringShorts = [];

      _IntInts = [];
      _StringInts = [];

      _LongLongs = [];
      _StringLongs = [];

      _IntStrings = [];
      _StringStrings = [];
    }
    #endregion

    #region SetValues Methods

    // Updates a grid row with the DataRow values.
    /// <include file='Doc/LJCGridRow.xml'
    ///  path='items/LJCSetValues1/*'/>
    public void LJCSetValues(DataRow adoRow
      , LJCDataColumns dataDefinition)
    {
      if (DataGridView != null)
      {
        var listValues = new List<object>();
        var gridColumns = DataGridView.Columns;
        foreach (DataGridViewColumn gridColumn in gridColumns)
        {
          if (null == gridColumn)
          {
            continue;
          }

          var dataColumnName = gridColumn.Name;

          if (dataDefinition != null)
          {
            var dataColumn = dataDefinition[dataColumnName];
            if (dataColumn?.RenameAs != null)
            {
              dataColumnName = dataColumn.RenameAs;
            }
          }

          if (adoRow.Table.Columns.Contains(dataColumnName))
          {
            var value = adoRow[dataColumnName];
            listValues.Add(value);
          }
        }
        var values = listValues.ToArray();
        SetValues(values);
      }
    }

    // Updates a grid row with LJCDataValues.
    /// <include file='Doc/LJCGridRow.xml'
    ///  path='items/LJCSetValues2/*'/>
    public void LJCSetValues(LJCDataValues dataValues)
    {
      if (LJC.HasListItems(dataValues))
      {
        if (DataGridView != null)
        {
          var grid = DataGridView;
          var listValues = new List<object>();
          foreach (DataGridViewColumn gridColumn in grid.Columns)
          {
            if (null == gridColumn
              || !LJC.HasText(gridColumn.Name))
            {
              continue;
            }

            var value = dataValues.LJCGetString(gridColumn.Name);
            if (value != null)
            {
              listValues.Add(value);
            }
          }
          var values = listValues.ToArray();
          SetValues(values);
        }
      }
    }

    // Updates a grid row with the object values.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetValues3/*'/>
    public void LJCSetValues(object[] values)
    {
      var grid = DataGridView;
      if (grid != null
        && values != null && values.Length > 0
        && (values.Length <= grid.Columns.Count))
      {
        SetValues(values);
      }
    }

    // Updates a grid row with the record values.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='items/LJCSetValues4/*'/>
    public void LJCSetValues(object record)
    {
      // Attempt to populate all existing columns.
      if (DataGridView != null)
      {
        var grid = DataGridView;
        var reflect = new LJCReflect(record);

        foreach (DataGridViewColumn gridColumn in grid.Columns)
        {
          if (gridColumn != null
            && LJC.HasText(gridColumn.Name))
          {
            // Use existing column names which are the object property names.
            var value = GetPropertyValue(reflect, gridColumn.Name);
            LJCSetCellText(gridColumn.Name, value);
          }
        }
      }
    }

    // Gets the Data object property value.
    private static string? GetPropertyValue(LJCReflect reflect, string propertyName)
    {
      string? retValue = null;

      var propertyType = reflect.GetPropertyType(propertyName);
      if (propertyType != null)
      {
        var fullName = propertyType.FullName;
        if (fullName != null
          && fullName.Contains("DateTime"))
        {
          DateTime dateValue = reflect.GetDateTime(propertyName);
          retValue = GetUiDateString(dateValue);
        }
        else
        {
          retValue = reflect.GetString(propertyName);
        }
      }
      return retValue;
    }

    // Format the date for display.
    private static string? GetUiDateString(DateTime dateTime)
    {
      string? retVal = null;

      if (!IsDbMinDate(dateTime))
      {
        retVal = dateTime.ToString("MM/dd/yyyy");
      }
      return retVal;
    }

    // Check for DB Minimum date or less.
    private static bool IsDbMinDate(DateTime dateTime)
    {
      bool retValue = false;
      if (dateTime.Year < 1753)
      {
        retValue = true;
      }
      if (1753 == dateTime.Year
        && 1 == dateTime.Month
        && 1 == dateTime.Day)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Stored Value Methods

    // Stores an int key and int value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetInt161/*'/>
    public void LJCSetInt16(short key, short value)
    {
      _ShortShorts.TryAdd(key, value);
    }

    // Stores a string key and int value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetInt16322/*'/>
    public void LJCSetInt16(string key, short value)
    {
      _StringShorts.TryAdd(key, value);
    }

    // Stores an int key and int value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetInt321/*'/>
    public void LJCSetInt32(int key, int value)
    {
      _IntInts.TryAdd(key, value);
    }

    // Stores a string key and int value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetInt322/*'/>
    public void LJCSetInt32(string key, int value)
    {
      _StringInts.TryAdd(key, value);
    }

    // Stores an int key and int value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetInt641/*'/>
    public void LJCSetInt64(long key, long value)
    {
      _LongLongs.TryAdd(key, value);
    }

    // Stores a string key and long value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetInt642/*'/>
    public void LJCSetInt64(string key, long value)
    {
      _StringLongs.TryAdd(key, value);
    }

    // Stores a int key and string value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetString1/*'/>
    public void LJCSetString(int key, string value)
    {
      _IntStrings.TryAdd(key, value);
    }

    // Stores a string key and string value pair.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCSetString2/*'/>
    public void LJCSetString(string key, string value)
    {
      _StringStrings.TryAdd(key, value);
    }

    // Gets the stored int value using an int key.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCGetInt321/*'/>
    public int LJCGetInt32(int key)
    {
      _IntInts.TryGetValue(key, out int retValue);
      return retValue;
    }

    // Gets the stored int value using a string key.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCGetInt322/*'/>
    public int LJCGetInt32(string key)
    {
      _StringInts.TryGetValue(key, out int retValue);
      return retValue;
    }

    // Gets the stored long value using a long key.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCGetInt641/*'/>
    public long LJCGetInt64(long key)
    {
      _LongLongs.TryGetValue(key, out long retValue);
      return retValue;
    }

    // Gets the stored long value using a string key.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCGetInt642/*'/>
    public long LJCGetInt64(string key)
    {
      _StringLongs.TryGetValue(key, out long retValue);
      return retValue;
    }

    // Gets the stored string value using an int key.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCGetString1/*'/>
    public string? LJCGetString(int key)
    {
      _IntStrings.TryGetValue(key, out string? retValue);
      return retValue;
    }

    // Gets the stored string value using a string key.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='members/LJCGetString2/*'/>
    public string? LJCGetString(string key)
    {
      _StringStrings.TryGetValue(key, out string? retValue);
      return retValue;
    }
    #endregion

    #region Cell Methods

    // Sets the cell value.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='items/LJCGetCellText/*'/>
    public string? LJCGetCellText(string columnName)
    {
      string? retValue = null;

      if (DataGridView != null)
      {
        // Ensure that column exists.
        if (DataGridView.Columns[columnName] != null)
        {
          retValue = Cells[columnName].Value.ToString();
        }
      }
      return retValue;
    }

    // Sets the cell value.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='items/LJCSetCellText/*'/>
    public void LJCSetCellText(string columnName, object? value)
    {
      if (DataGridView != null)
      {
        // Ensure that column exists.
        if (DataGridView.Columns[columnName] != null)
        {
          if (null == value)
          {
            value = "";
          }
          Cells[columnName].Value = value.ToString();
        }
      }
    }

    // Sets the cell value by index.
    /// <include file='Doc/LJCGridRow5.xml'
    ///  path='items/LJCSetCellText1/*'/>
    public void LJCSetCellText(int index, object value)
    {
      if (DataGridView != null)
      {
        // Ensure that column exists.
        if (index >= 0 && index < Cells.Count)
        {
          if (null == value)
          {
            value = "";
          }
          Cells[index].Value = value.ToString();
        }
      }
    }
    #endregion

    #region Class Data

    // Class data.
    private readonly Dictionary<short, short> _ShortShorts;
    private readonly Dictionary<string, short> _StringShorts;

    private readonly Dictionary<int, int> _IntInts;
    private readonly Dictionary<string, int> _StringInts;

    private readonly Dictionary<long, long> _LongLongs;
    private readonly Dictionary<string, long> _StringLongs;

    private readonly Dictionary<int, string> _IntStrings;
    private readonly Dictionary<string, string> _StringStrings;
    #endregion
  }
}
