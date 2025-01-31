// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _ClassName_
// _ClassName_.cs
// #SectionEnd Title
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

// #SectionBegin Class
// #Value _ClassName_
// #Value _ComparerName_
// #Value _CompareToName_
// #Value _Namespace_
// #Value _TableName_
// #Value _ToStringName_
namespace _Namespace_
{
  /// <summary>The _TableName_ Data Object.</summary>
  public class _ClassName_ : IComparable<_ClassName_>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public _ClassName_()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public _ClassName_(_ClassName_ item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      Name = item.Name;
      Description = item.Description;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public _ClassName_ Clone()
    {
      var retValue = MemberwiseClone() as _ClassName_;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(_ClassName_ other)
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
        retValue = _CompareToName_.CompareTo(other._CompareToName_);

        // Not case sensitive.
        //retValue = string.Compare(_CompareToName_, other._CompareToName_, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{mSequence} {m_ToStringName_}:{mID}-{mValue}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.
    // #SectionBegin Properties
    // #Value _ColumnName_
    // #Value _DataType_
    // #Value _MaxLength_
    // #Value _PropertyName_

    /// <summary>Gets or sets the _PropertyName_ value.</summary>
    // #IfBegin _AllowDBNull_ False
    //[Required]
    // #IfEnd
    // #IfBegin _DataType_ String
    //[Column("_ColumnName_", TypeName="_DBType_(_MaxLength_")]
    // #IfElse
    //[Column("_ColumnName_", TypeName="_DBType_")]
    // #IfEnd _DataType_
    public _DataType_ _PropertyName_
    {
      get { return m_PropertyName_; }
      set
      {
        // #IfBegin _DataType_ String
        value = NetString.InitString(value);
        // #IfEnd _DataType_
        m_PropertyName_ = ChangedNames.Add(Column_PropertyName_, m_PropertyName_, value);
      }
    }
    private _DataType_ m_PropertyName_;
    // #SectionEnd Properties
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
    public static string TableName = "_TableName_";
    // #SectionBegin Properties
    // #Value _ColumnName_
    // #Value _DataType_
    // #Value _MaxLength_
    // #Value _PropertyName_

    /// <summary>The _ColumnName_ column name.</summary>
    public static string Column_ColumnName_ = "_ColumnName_";
    // #SectionEnd Properties
    // #SectionBegin Properties
    // #Value _ColumnName_
    // #Value _DataType_
    // #Value _MaxLength_
    // #Value _PropertyName_
    // #IfBegin _DataType_ String

    /// <summary>The _ColumnName_ maximum length.</summary>
    public static int Length_ColumnName_ = _MaxLength_;
    // #IfEnd _DataType_
    // #SectionEnd Properties
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class _ClassName_Unique : IComparer<_ClassName_>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(_ClassName_ x, _ClassName_ y)
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
        retValue = NetCommon.CompareNull(x._ComparerName_, y._ComparerName_);
        if (retValue != -2)
        {
          isContinue = false;
        }
      }

      if (isContinue)
      {
        // Case sensitive and not string.
        retValue = x._ComparerName_.CompareTo(y._ComparerName_);

        // Not case sensitive.
        //retValue = string.Compare(x._ComparerName_, y._ComparerName_, true);
      }
      return retValue;
    }
  }
  #endregion
}
// #SectionEnd Class
