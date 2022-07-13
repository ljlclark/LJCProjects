// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
// LJCGridRow.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides custom functionality for a DataGridViewRow control.</summary>
  public class LJCGridRow : DataGridViewRow
  {
    #region Constructors

    // Instantiates an instance of the class.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LJCGridRow()
    {
      mIntInts = new Dictionary<int, int>();
      mStringInts = new Dictionary<string, int>();
      mStringLongs = new Dictionary<string, long>();
      mIntStrings = new Dictionary<int, string>();
      mStringStrings = new Dictionary<string, string>();
    }
    #endregion

    #region Public Methods

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

    // Stores a string key and int value pair.
    /// <include path='items/LJCSetInt64/*' file='Doc/LJCGridRow.xml'/>
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

    // Gets the stored long value using a string key.
    /// <include path='items/LJCGetInt64/*' file='Doc/LJCGridRow.xml'/>
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
      string retVal = null;

      if (mStringStrings.ContainsKey(key))
      {
        retVal = mStringStrings[key].ToString();
      }
      return retVal;
    }

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
    #endregion

    #region Member Data

    // Class data.
    private readonly Dictionary<int, int> mIntInts;
    private readonly Dictionary<string, int> mStringInts;
    private readonly Dictionary<string, long> mStringLongs;
    private readonly Dictionary<int, string> mIntStrings;
    private readonly Dictionary<string, string> mStringStrings;
    #endregion
  }
}