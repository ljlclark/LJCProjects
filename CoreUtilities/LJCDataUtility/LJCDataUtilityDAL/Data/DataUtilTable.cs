// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilTable.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;
using LJCDataUtilityDAL;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataTable table Data Object.</summary>
  public class DataUtilTable : IComparable<DataUtilTable>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilTable()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilTable(DataUtilTable item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DataModuleID = item.DataModuleID;
      Name = item.Name;
      Description = item.Description;
      NewName = item.NewName;
    }
    #endregion

    #region Data Class Methods

    // Adds changed propertynames.
    public void AddChangedNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        var name = ChangedNames.FindName(propertyName);
        if (null == name)
        {
          ChangedNames.Add(propertyName);
        }
      }
    }

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilTable Clone()
    {
      var retValue = MemberwiseClone() as DataUtilTable;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataUtilTable other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{mName}:{mID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="int")]
    public Int32 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int32 mID;

    /// <summary>Gets or sets the DataModuleID value.</summary>
    //[Required]
    //[Column("DataModuleID", TypeName="int")]
    public Int32 DataModuleID
    {
      get { return mDataModuleID; }
      set
      {
        mDataModuleID = ChangedNames.Add(ColumnDataModuleID, mDataModuleID, value);
      }
    }
    private Int32 mDataModuleID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Required]
    //[Column("Name", TypeName="nvarchar(60")]
    public String Name
    {
      get { return mName; }
      set
      {
        value = NetString.InitString(value);
        mName = ChangedNames.Add(ColumnName, mName, value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the Description value.</summary>
    //[Required]
    //[Column("Description", TypeName="nvarchar(80")]
    public String Description
    {
      get { return mDescription; }
      set
      {
        value = NetString.InitString(value);
        mDescription = ChangedNames.Add(ColumnDescription, mDescription, value);
      }
    }
    private String mDescription;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Required]
    //[Column("Sequence", TypeName="int")]
    public Int32 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int32 mSequence;

    /// <summary>Gets or sets the NewName value.</summary>
    //[Column("NewName", TypeName="nvarchar(60")]
    public String NewName
    {
      get { return mNewName; }
      set
      {
        value = NetString.InitString(value);
        mNewName = ChangedNames.Add(ColumnNewName, mNewName, value);
      }
    }
    private String mNewName;
    #endregion

    #region Calculated and Join Data Properties

    /// <summary>Gets or sets the Join ModuleName value.</summary>
    public string ModuleName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Calculated and Join Class Data

    /// <summary>The Join ModuleName column name.</summary>
    public static string ColumnModuleName = "ModuleName";
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataTable";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataModuleID column name.</summary>
    public static string ColumnDataModuleID = "DataModuleID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The NewName column name.</summary>
    public static string ColumnNewName = "NewName";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 80;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataTableUniqueComparer : IComparer<DataUtilTable>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataUtilTable x, DataUtilTable y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (-2 == retValue)
        {
          // Case sensitive.
          retValue = x.DataModuleID.CompareTo(y.DataModuleID);
          if (0 == retValue)
          {
            retValue = x.Name.CompareTo(y.Name);
          }

          // Not case sensitive.
          //retValue = string.Compare(x.Name, y.Name, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
