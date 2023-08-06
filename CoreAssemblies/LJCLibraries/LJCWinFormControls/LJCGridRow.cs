// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGridRow.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides custom functionality for a DataGridViewRow control.</summary>
  public class LJCGridRow : DataGridViewRow
  {
    #region Constructors

    // Instantiates an instance of the class.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public LJCGridRow()
    {
      mIntInts = new Dictionary<int, int>();
      mStringInts = new Dictionary<string, int>();

      mLongLongs = new Dictionary<long, long>();
      mStringLongs = new Dictionary<string, long>();
      
      mIntStrings = new Dictionary<int, string>();
      mStringStrings = new Dictionary<string, string>();
    }
    #endregion

    #region SetValues Methods

    // Updates a grid row with DbValues.
    /// <include path='items/LJCRowSetValues2/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetValues(DataGridView grid, DbValues dbValues)
    {
      //if ((dbValues != null && dbValues.Count() > 0))
      if (NetCommon.HasItems(dbValues))
      {
        List<object> listValues = new List<object>();
        foreach (DataGridViewColumn gridColumn in grid.Columns)
        {
          listValues.Add(dbValues.LJCGetValue(gridColumn.Name));
        }
        var values = listValues.ToArray();
        SetValues(values);
      }
    }

    // Updates a grid row with the object values.
    /// <include path='items/LJCRowSetValues1/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetValues(DataGridView grid, object[] values)
    {
      if ((values != null && values.Count() > 0)
        && (values.Count() <= grid.Columns.Count))
      {
        SetValues(values);
      }
    }

    // Updates a grid row with the record values.
    /// <include path='items/LJCRowSetValues/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetValues(DataGridView grid, object record
      , DbColumns displayColumns = null)
    {
      LJCReflect reflect;
      string value;

      reflect = new LJCReflect(record);
      if (displayColumns != null)
      {
        // Attempt to populate the specified columns.
        foreach (DbColumn column in displayColumns)
        {
          // Grid columns are named after the object property names.
          value = GetPropertyValue(reflect, column.PropertyName);
          LJCSetCellText(column.PropertyName, value);
        }
      }
      else
      {
        // Attempt to populate all existing columns.
        foreach (DataGridViewColumn column in grid.Columns)
        {
          // Use existing column names which are the object property names.
          value = GetPropertyValue(reflect, column.Name);
          LJCSetCellText(column.Name, value);
        }
      }
    }

    // Gets the Data object property value.
    private string GetPropertyValue(LJCReflect reflect, string propertyName)
    {
      string retValue;

      Type propertyType = reflect.GetPropertyType(propertyName);
      if (propertyType != null
        && propertyType.FullName.Contains("DateTime"))
      {
        DateTime dateValue = reflect.GetDateTime(propertyName);
        retValue = GetUiDateString(dateValue);
      }
      else
      {
        retValue = reflect.GetString(propertyName);
      }
      return retValue;
    }

    // Format the date for display.
    private string GetUiDateString(DateTime dateTime)
    {
      string retVal = null;

      if (false == IsDbMinDate(dateTime))
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
    /// <include path='items/LJCSetInt321/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetInt32(int key, int value)
    {
      if (mIntInts.ContainsKey(key))
      {
        mIntInts[key] = value;
      }
      else
      {
        mIntInts.Add(key, value);
      }
    }

    // Stores a string key and int value pair.
    /// <include path='items/LJCSetInt322/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetInt32(string key, int value)
    {
      if (mStringInts.ContainsKey(key))
      {
        mStringInts[key] = value;
      }
      else
      {
        mStringInts.Add(key, value);
      }
    }

    // Stores an int key and int value pair.
    /// <include path='items/LJCSetInt641/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetInt64(long key, long value)
    {
      if (mLongLongs.ContainsKey(key))
      {
        mLongLongs[key] = value;
      }
      else
      {
        mLongLongs.Add(key, value);
      }
    }

    // Stores a string key and long value pair.
    /// <include path='items/LJCSetInt642/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetInt64(string key, long value)
    {
      if (mStringLongs.ContainsKey(key))
      {
        mStringLongs[key] = value;
      }
      else
      {
        mStringLongs.Add(key, value);
      }
    }

    // Stores a int key and string value pair.
    /// <include path='items/LJCSetString1/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetString(int key, string value)
    {
      if (mIntStrings.ContainsKey(key))
      {
        mIntStrings[key] = value;
      }
      else
      {
        mIntStrings.Add(key, value);
      }
    }

    // Stores a string key and string value pair.
    /// <include path='items/LJCSetString2/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetString(string key, string value)
    {
      if (mStringStrings.ContainsKey(key))
      {
        mStringStrings[key] = value;
      }
      else
      {
        mStringStrings.Add(key, value);
      }
    }

    // Gets the stored int value using an int key.
    /// <include path='items/LJCGetInt321/*' file='Doc/LJCGridRow.xml'/>
    public int LJCGetInt32(int key)
    {
      int retVal = 0;

      if (mIntInts.ContainsKey(key))
      {
        retVal = mIntInts[key];
      }
      return retVal;
    }

    // Gets the stored int value using a string key.
    /// <include path='items/LJCGetInt322/*' file='Doc/LJCGridRow.xml'/>
    public int LJCGetInt32(string key)
    {
      int retVal = 0;

      if (mStringInts.ContainsKey(key))
      {
        retVal = mStringInts[key];
      }
      return retVal;
    }

    // Gets the stored long value using a long key.
    /// <include path='items/LJCGetInt641/*' file='Doc/LJCGridRow.xml'/>
    public long LJCGetInt64(long key)
    {
      long retVal = 0;

      if (mLongLongs.ContainsKey(key))
      {
        retVal = mLongLongs[key];
      }
      return retVal;
    }

    // Gets the stored long value using a string key.
    /// <include path='items/LJCGetInt642/*' file='Doc/LJCGridRow.xml'/>
    public long LJCGetInt64(string key)
    {
      long retVal = 0;

      if (mStringLongs.ContainsKey(key))
      {
        retVal = mStringLongs[key];
      }
      return retVal;
    }

    // Gets the stored string value using an int key.
    /// <include path='items/LJCGetString1/*' file='Doc/LJCGridRow.xml'/>
    public string LJCGetString(int key)
    {
      string retVal = null;

      if (mIntStrings.ContainsKey(key))
      {
        retVal = mIntStrings[key].ToString();
      }
      return retVal;
    }

    // Gets the stored string value using a string key.
    /// <include path='items/LJCGetString2/*' file='Doc/LJCGridRow.xml'/>
    public string LJCGetString(string key)
    {
      string retValue = null;

      if (mStringStrings.ContainsKey(key))
      {
        var val = mStringStrings[key];
        if (val != null)
        {
          retValue = val.ToString();
        }
      }
      return retValue;
    }
    #endregion

    #region Cell Methods

    // Sets the cell value.
    /// <include path='items/LJCGetCellText/*' file='Doc/LJCGridRow.xml'/>
    public string LJCGetCellText(string columnName)
    {
      string retVal = null;

      if (DataGridView != null)
      {
        // Ensure that column exists.
        if (DataGridView.Columns[columnName] != null)
        {
          retVal = Cells[columnName].Value.ToString();
        }
      }
      return retVal;
    }

    // Sets the cell value.
    /// <include path='items/LJCSetCellText/*' file='Doc/LJCGridRow.xml'/>
    public void LJCSetCellText(string columnName, object value)
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
    /// <include path='items/LJCSetCellText1/*' file='Doc/LJCGridRow.xml'/>
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

    #region Member Data

    // Class data.
    private readonly Dictionary<int, int> mIntInts;
    private readonly Dictionary<string, int> mStringInts;

    private readonly Dictionary<long, long> mLongLongs;
    private readonly Dictionary<string, long> mStringLongs;

    private readonly Dictionary<int, string> mIntStrings;
    private readonly Dictionary<string, string> mStringStrings;
    #endregion
  }
}
