// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAppFile.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The DocAppFile table Data Object.</summary>
  public class DocAppFile : IComparable<DocAppFile>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAppFile()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAppFile(DocAppFile item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAppFile Clone()
    {
      var retValue = MemberwiseClone() as DocAppFile;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocAppFile other)
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

    /// <summary>Gets or sets the DocAppID value.</summary>
    //[Column("DocAppID", TypeName="_DBType_")]
    public Int32 DocAppID
    {
      get { return mDocAppID; }
      set
      {
        mDocAppID = ChangedNames.Add(ColumnDocAppID, mDocAppID, value);
      }
    }
    private Int32 mDocAppID;

    /// <summary>Gets or sets the Path value.</summary>
    //[Column("Path", TypeName="_DBType_(300")]
    public String Path
    {
      get { return mPath; }
      set
      {
        value = NetString.InitString(value);
        mPath = ChangedNames.Add(ColumnPath, mPath, value);
      }
    }
    private String mPath;

    /// <summary>Gets or sets the FileName value.</summary>
    //[Column("FileName", TypeName="_DBType_(60")]
    public String FileName
    {
      get { return mFileName; }
      set
      {
        value = NetString.InitString(value);
        mFileName = ChangedNames.Add(ColumnFileName, mFileName, value);
      }
    }
    private String mFileName;

    /// <summary>Gets or sets the Description value.</summary>
    //[Column("Description", TypeName="_DBType_(100")]
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

    /// <summary>Gets or sets the CreateDate value.</summary>
    //[Column("CreateDate", TypeName="_DBType_")]
    public DateTime CreateDate
    {
      get { return mCreateDate; }
      set
      {
        mCreateDate = ChangedNames.Add(ColumnCreateDate, mCreateDate, value);
      }
    }
    private DateTime mCreateDate;

    /// <summary>Gets or sets the CreateUserID value.</summary>
    //[Column("CreateUserID", TypeName="_DBType_(60")]
    public String CreateUserID
    {
      get { return mCreateUserID; }
      set
      {
        value = NetString.InitString(value);
        mCreateUserID = ChangedNames.Add(ColumnCreateUserID, mCreateUserID, value);
      }
    }
    private String mCreateUserID;

    /// <summary>Gets or sets the Workstation value.</summary>
    //[Column("Workstation", TypeName="_DBType_(100")]
    public String Workstation
    {
      get { return mWorkstation; }
      set
      {
        value = NetString.InitString(value);
        mWorkstation = ChangedNames.Add(ColumnWorkstation, mWorkstation, value);
      }
    }
    private String mWorkstation;

    /// <summary>Gets or sets the LocalPath value.</summary>
    //[Column("LocalPath", TypeName="_DBType_(300")]
    public String LocalPath
    {
      get { return mLocalPath; }
      set
      {
        value = NetString.InitString(value);
        mLocalPath = ChangedNames.Add(ColumnLocalPath, mLocalPath, value);
      }
    }
    private String mLocalPath;

    /// <summary>Gets or sets the LocalFileName value.</summary>
    //[Column("LocalFileName", TypeName="_DBType_(100")]
    public String LocalFileName
    {
      get { return mLocalFileName; }
      set
      {
        value = NetString.InitString(value);
        mLocalFileName = ChangedNames.Add(ColumnLocalFileName, mLocalFileName, value);
      }
    }
    private String mLocalFileName;
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
    public static string TableName = "DocAppFile";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DocAppID column name.</summary>
    public static string ColumnDocAppID = "DocAppID";

    /// <summary>The Path column name.</summary>
    public static string ColumnPath = "Path";

    /// <summary>The FileName column name.</summary>
    public static string ColumnFileName = "FileName";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The CreateDate column name.</summary>
    public static string ColumnCreateDate = "CreateDate";

    /// <summary>The CreateUserID column name.</summary>
    public static string ColumnCreateUserID = "CreateUserID";

    /// <summary>The Workstation column name.</summary>
    public static string ColumnWorkstation = "Workstation";

    /// <summary>The LocalPath column name.</summary>
    public static string ColumnLocalPath = "LocalPath";

    /// <summary>The LocalFileName column name.</summary>
    public static string ColumnLocalFileName = "LocalFileName";

    /// <summary>The Path maximum length.</summary>
    public static int LengthPath = 300;

    /// <summary>The FileName maximum length.</summary>
    public static int LengthFileName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The CreateUserID maximum length.</summary>
    public static int LengthCreateUserID = 60;

    /// <summary>The Workstation maximum length.</summary>
    public static int LengthWorkstation = 100;

    /// <summary>The LocalPath maximum length.</summary>
    public static int LengthLocalPath = 300;

    /// <summary>The LocalFileName maximum length.</summary>
    public static int LengthLocalFileName = 100;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocAppFileUniqueComparer : IComparer<DocAppFile>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocAppFile x, DocAppFile y)
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
