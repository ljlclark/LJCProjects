// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModule.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;
using LJCDataUtilityDAL;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataModule table Data Object.</summary>
  public class DataModule : IComparable<DataModule>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataModule()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataModule(DataModule item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DataSiteID = item.DataSiteID;
      Name = item.Name;
      Description = item.Description;
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
    public DataModule Clone()
    {
      var retValue = MemberwiseClone() as DataModule;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataModule other)
    {
      int retValue = -2;

      var isContinue = true;
      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
        isContinue = false;
      }
      if (isContinue)
      {
        retValue = ID.CompareTo(other.ID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = DataSiteID.CompareTo(other.DataSiteID);
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
    //[Column("ID", TypeName="bigint")]
    public Int64 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int64 mID;

    /// <summary>Gets or sets the DataModuleID value.</summary>
    //[Required]
    //[Column("DataSiteID", TypeName="bigint")]
    public Int64 DataSiteID
    {
      get { return mDataSiteID; }
      set
      {
        mDataSiteID = ChangedNames.Add(ColumnDataSiteID, mDataSiteID, value);
      }
    }
    private Int64 mDataSiteID;

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
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataModule";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 80;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataModuleUniqueComparer : IComparer<DataModule>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataModule x, DataModule y)
    {
      int retValue;

      var isContinue = true;
      retValue = NetCommon.CompareNull(x, y);
      if (retValue != -2)
      {
        isContinue = false;
      }
      if (isContinue)
      {
        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (retValue != -2)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.Name.CompareTo(y.Name);
      }
      return retValue;
    }
  }
  #endregion
}
