// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// PersonDMTest.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The PersonDMTest table Data Object.</summary>
  public class PersonDMTest : IComparable<PersonDMTest>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public PersonDMTest()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public PersonDMTest(PersonDMTest item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public PersonDMTest Clone()
    {
      var retValue = MemberwiseClone() as PersonDMTest;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(PersonDMTest other)
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

    /// <summary>Gets or sets the Id value.</summary>
    //[Column("Id", TypeName="_DBType_")]
    public Int64 Id
    {
      get { return mId; }
      set
      {
        mId = ChangedNames.Add(ColumnId, mId, value);
      }
    }
    private Int64 mId;

    /// <summary>Gets or sets the FirstName value.</summary>
    //[Column("FirstName", TypeName="_DBType_(15")]
    public String FirstName
    {
      get { return mFirstName; }
      set
      {
        value = NetString.InitString(value);
        mFirstName = ChangedNames.Add(ColumnFirstName, mFirstName, value);
      }
    }
    private String mFirstName;

    /// <summary>Gets or sets the LastName value.</summary>
    //[Column("LastName", TypeName="_DBType_(15")]
    public String LastName
    {
      get { return mLastName; }
      set
      {
        value = NetString.InitString(value);
        mLastName = ChangedNames.Add(ColumnLastName, mLastName, value);
      }
    }
    private String mLastName;

    /// <summary>Gets or sets the CodeType_Id value.</summary>
    //[Column("CodeType_Id", TypeName="_DBType_")]
    public Int32 CodeType_Id
    {
      get { return mCodeType_Id; }
      set
      {
        mCodeType_Id = ChangedNames.Add(ColumnCodeType_Id, mCodeType_Id, value);
      }
    }
    private Int32 mCodeType_Id;

    /// <summary>Gets or sets the PrincipleFlag value.</summary>
    //[Column("PrincipleFlag", TypeName="_DBType_")]
    public Boolean PrincipleFlag
    {
      get { return mPrincipleFlag; }
      set
      {
        mPrincipleFlag = ChangedNames.Add(ColumnPrincipleFlag, mPrincipleFlag, value);
      }
    }
    private Boolean mPrincipleFlag;
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
    public static string TableName = "PersonDMTest";

    /// <summary>The Id column name.</summary>
    public static string ColumnId = "Id";

    /// <summary>The FirstName column name.</summary>
    public static string ColumnFirstName = "FirstName";

    /// <summary>The LastName column name.</summary>
    public static string ColumnLastName = "LastName";

    /// <summary>The CodeType_Id column name.</summary>
    public static string ColumnCodeType_Id = "CodeType_Id";

    /// <summary>The PrincipleFlag column name.</summary>
    public static string ColumnPrincipleFlag = "PrincipleFlag";

    /// <summary>The FirstName maximum length.</summary>
    public static int LengthFirstName = 15;

    /// <summary>The LastName maximum length.</summary>
    public static int LengthLastName = 15;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class PersonDMTestUniqueComparer : IComparer<PersonDMTest>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(PersonDMTest x, PersonDMTest y)
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
