// ControlTab.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>The ControlTab table Data Object.</summary>
  public class ControlTab : IComparable<ControlTab>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlTab()
    {
      ChangedNames = new ChangedNames();
      ControlColumns = new ControlColumns();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlTab(ControlTab item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      ControlDetailID = item.ControlDetailID;
      TabIndex = item.TabIndex;
      Caption = item.Caption;
      Description = item.Description;

      ControlColumns = new ControlColumns();
      foreach (ControlColumn controlColumn in item.ControlColumns)
      {
        ControlColumns.Add(new ControlColumn(controlColumn));
      }
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ControlTab Clone()
    {
      var retValue = MemberwiseClone() as ControlTab;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlTab other)
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
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return $"Detail:{ControlDetailID}-Tab:{TabIndex}";
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

    /// <summary>Gets or sets the ControlDetailID value.</summary>
    //[Required]
    //[Column("ControlDetailID", TypeName="bigint")]
    public Int64 ControlDetailID
    {
      get { return mControlDetailID; }
      set
      {
        mControlDetailID = ChangedNames.Add(ColumnControlDetailID, mControlDetailID, value);
      }
    }
    private Int64 mControlDetailID;

    /// <summary>Gets or sets the TabIndex value.</summary>
    //[Required]
    //[Column("TabIndex", TypeName="int")]
    public Int32 TabIndex
    {
      get { return mTabIndex; }
      set
      {
        mTabIndex = ChangedNames.Add(ColumnTabIndex, mTabIndex, value);
      }
    }
    private Int32 mTabIndex;

    /// <summary>Gets or sets the Caption value.</summary>
    //[Required]
    //[Column("Caption", TypeName="nvarchar(40")]
    public String Caption
    {
      get { return mCaption; }
      set
      {
        value = NetString.InitString(value);
        mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
      }
    }
    private String mCaption;

    /// <summary>Gets or sets the Description value.</summary>
    //[Required]
    //[Column("Description", TypeName="nvarchar(60")]
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

    #region Related Data Properties

    /// <summary>The ControlTab Object contained ControlColumns.</summary>
    public ControlColumns ControlColumns { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "ControlTab";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ControlDetailID column name.</summary>
    public static string ColumnControlDetailID = "ControlDetailID";

    /// <summary>The TabIndex column name.</summary>
    public static string ColumnTabIndex = "TabIndex";

    /// <summary>The Caption column name.</summary>
    public static string ColumnCaption = "Caption";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Caption maximum length.</summary>
    public static int LengthCaption = 40;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Unique key.</summary>
  public class ControlTabUniqueComparer : IComparer<ControlTab>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int Compare(ControlTab x, ControlTab y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        // Case sensitive.
        retValue = x.ControlDetailID.CompareTo(y.ControlDetailID);
        if (0 == retValue)
        {
          // Case sensitive.
          retValue = x.TabIndex.CompareTo(y.TabIndex);
        }
      }
      return retValue;
    }
  }
  #endregion
}
