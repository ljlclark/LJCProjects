// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Business.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The Business table Data Object.</summary>
  public class Business : IComparable<Business>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Business()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Business(Business item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Business Clone()
    {
      var retValue = MemberwiseClone() as Business;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Business other)
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
        //retValue = Name.CompareTo(other.Name);

        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      // $"{mSequence}){mName}:{mID}-{mValue}";
      return mDescription;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Column("ID", TypeName="_DBType_")]
    public Int32 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int32 mID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Column("Name", TypeName="_DBType_(60")]
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
    //[Column("Description", TypeName="_DBType_(60")]
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

    /// <summary>Gets or sets the CodeTypeID value.</summary>
    //[Column("CodeTypeID", TypeName="_DBType_")]
    public Int32 CodeTypeID
    {
      get { return mCodeTypeID; }
      set
      {
        mCodeTypeID = ChangedNames.Add(ColumnCodeTypeID, mCodeTypeID, value);
      }
    }
    private Int32 mCodeTypeID;

    /// <summary>Gets or sets the EffectiveDate value.</summary>
    //[Column("EffectiveDate", TypeName="_DBType_")]
    public DateTime EffectiveDate
    {
      get { return mEffectiveDate; }
      set
      {
        mEffectiveDate = ChangedNames.Add(ColumnEffectiveDate, mEffectiveDate, value);
      }
    }
    private DateTime mEffectiveDate;

    /// <summary>Gets or sets the ExpirationDate value.</summary>
    //[Column("ExpirationDate", TypeName="_DBType_")]
    public DateTime ExpirationDate
    {
      get { return mExpirationDate; }
      set
      {
        mExpirationDate = ChangedNames.Add(ColumnExpirationDate, mExpirationDate, value);
      }
    }
    private DateTime mExpirationDate;

    /// <summary>Gets or sets the Phone value.</summary>
    //[Column("Phone", TypeName="_DBType_(18")]
    public String Phone
    {
      get { return mPhone; }
      set
      {
        value = NetString.InitString(value);
        mPhone = ChangedNames.Add(ColumnPhone, mPhone, value);
      }
    }
    private String mPhone;

    /// <summary>Gets or sets the Extension value.</summary>
    //[Column("Extension", TypeName="_DBType_(4")]
    public String Extension
    {
      get { return mExtension; }
      set
      {
        value = NetString.InitString(value);
        mExtension = ChangedNames.Add(ColumnExtension, mExtension, value);
      }
    }
    private String mExtension;

    /// <summary>Gets or sets the Fax value.</summary>
    //[Column("Fax", TypeName="_DBType_(18")]
    public String Fax
    {
      get { return mFax; }
      set
      {
        value = NetString.InitString(value);
        mFax = ChangedNames.Add(ColumnFax, mFax, value);
      }
    }
    private String mFax;
    #endregion

    #region Calculated and Join Data Properties

    ///// <summary>Gets or sets the Join TypeName value.</summary>
    //public string TypeName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "Business";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The CodeTypeID column name.</summary>
    public static string ColumnCodeTypeID = "CodeTypeID";

    /// <summary>The EffectiveDate column name.</summary>
    public static string ColumnEffectiveDate = "EffectiveDate";

    /// <summary>The ExpirationDate column name.</summary>
    public static string ColumnExpirationDate = "ExpirationDate";

    /// <summary>The Phone column name.</summary>
    public static string ColumnPhone = "Phone";

    /// <summary>The Extension column name.</summary>
    public static string ColumnExtension = "Extension";

    /// <summary>The Fax column name.</summary>
    public static string ColumnFax = "Fax";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;

    /// <summary>The Phone maximum length.</summary>
    public static int LengthPhone = 18;

    /// <summary>The Extension maximum length.</summary>
    public static int LengthExtension = 4;

    /// <summary>The Fax maximum length.</summary>
    public static int LengthFax = 18;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class BusinessUniqueComparer : IComparer<Business>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(Business x, Business y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x._ComparerName_, y._ComparerName_);
        if (-2 == retValue)
        {
          // Case sensitive.
          //retValue = x._ComparerName_.CompareTo(y._ComparerName_);

          // Not case sensitive.
          retValue = string.Compare(x._ComparerName_, y._ComparerName_, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
