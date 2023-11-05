// DataTemplate.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>The DetailConfig table Data Object.</summary>
  public class ControlDetail : IComparable<ControlDetail>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDetail()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDetail(ControlDetail item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      UserID = item.UserID;
      DataConfigName = item.DataConfigName;
      TableName = item.TableName;
      DataValueCount = item.DataValueCount;
      ColumnRowsLimit = item.ColumnRowsLimit;
      PageColumnsLimit = item.PageColumnsLimit;
      CharacterPixels = item.CharacterPixels;
      MaxControlCharacters = item.MaxControlCharacters;
      BorderHorizontal = item.BorderHorizontal;
      BorderVertical = item.BorderVertical;
      ControlRowSpacing = item.ControlRowSpacing;
      ControlRowHeight = item.ControlRowHeight;
      ContentHeight = item.ContentHeight;

      ControlTabItems = new ControlTabItems();
      foreach (ControlTab controlTab in item.ControlTabItems)
      {
        ControlTabItems.Add(new ControlTab(controlTab));
      }
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDetail Clone()
    {
      var retValue = MemberwiseClone() as ControlDetail;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(ControlDetail other)
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
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      return $"{UserID}-{DataConfigName}-{TableName}";
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

    /// <summary>Gets or sets the DataConfigName value.</summary>
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

    /// <summary>Gets or sets the DataConfigName value.</summary>
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

    /// <summary>Gets or sets the DataConfigName value.</summary>
    //[Required]
    //[Column("DataConfigName", TypeName="nvarchar(60")]
    public String DataConfigName
    {
      get { return mDataConfigName; }
      set
      {
        value = NetString.InitString(value);
        mDataConfigName = ChangedNames.Add(ColumnDataConfigName, mDataConfigName, value);
      }
    }
    private String mDataConfigName;

    /// <summary>Gets or sets the TableName value.</summary>
    //[Required]
    //[Column("TableName", TypeName="nvarchar(60")]
    public String TableName
    {
      get { return mTableName; }
      set
      {
        value = NetString.InitString(value);
        mTableName = ChangedNames.Add(ColumnTableName, mTableName, value);
      }
    }
    private String mTableName;

    /// <summary>Gets or sets the UserID value.</summary>
    //[Column("UserID", TypeName="nvarchar(60")]
    public String UserID
    {
      get { return mUserID; }
      set
      {
        value = NetString.InitString(value);
        mUserID = ChangedNames.Add(ColumnUserID, mUserID, value);
      }
    }
    private String mUserID;

    /// <summary>Gets or sets the DataValueCount value.</summary>
    //[Required]
    //[Column("DataValueCount", TypeName="int")]
    public Int32 DataValueCount
    {
      get { return mDataValueCount; }
      set
      {
        mDataValueCount = ChangedNames.Add(ColumnDataValueCount, mDataValueCount, value);
      }
    }
    private Int32 mDataValueCount;

    /// <summary>Gets or sets the ColumnRowsLimit value.</summary>
    //[Required]
    //[Column("ColumnRowsLimit", TypeName="int")]
    public Int32 ColumnRowsLimit
    {
      get { return mColumnRowsLimit; }
      set
      {
        mColumnRowsLimit = ChangedNames.Add(ColumnColumnRowsLimit, mColumnRowsLimit, value);
      }
    }
    private Int32 mColumnRowsLimit;

    /// <summary>Gets or sets the PageColumnsLimit value.</summary>
    //[Required]
    //[Column("PageColumnsLimit", TypeName="int")]
    public Int32 PageColumnsLimit
    {
      get { return mPageColumnsLimit; }
      set
      {
        mPageColumnsLimit = ChangedNames.Add(ColumnPageColumnsLimit, mPageColumnsLimit, value);
      }
    }
    private Int32 mPageColumnsLimit;

    /// <summary>Gets or sets the CharacterPixels value.</summary>
    //[Required]
    //[Column("CharacterPixels", TypeName="int")]
    public Int32 CharacterPixels
    {
      get { return mCharacterPixels; }
      set
      {
        mCharacterPixels = ChangedNames.Add(ColumnCharacterPixels, mCharacterPixels, value);
      }
    }
    private Int32 mCharacterPixels;

    /// <summary>Gets or sets the MaxControlCharacters value.</summary>
    //[Required]
    //[Column("MaxControlCharacters", TypeName="int")]
    public Int32 MaxControlCharacters
    {
      get { return mMaxControlCharacters; }
      set
      {
        mMaxControlCharacters = ChangedNames.Add(ColumnMaxControlCharacters, mMaxControlCharacters, value);
      }
    }
    private Int32 mMaxControlCharacters;

    /// <summary>Gets or sets the BorderHorizontal value.</summary>
    //[Required]
    //[Column("BorderHorizontal", TypeName="int")]
    public Int32 BorderHorizontal
    {
      get { return mBorderHorizontal; }
      set
      {
        mBorderHorizontal = ChangedNames.Add(ColumnBorderHorizontal, mBorderHorizontal, value);
      }
    }
    private Int32 mBorderHorizontal;

    /// <summary>Gets or sets the BorderVertical value.</summary>
    //[Required]
    //[Column("BorderVertical", TypeName="int")]
    public Int32 BorderVertical
    {
      get { return mBorderVertical; }
      set
      {
        mBorderVertical = ChangedNames.Add(ColumnBorderVertical, mBorderVertical, value);
      }
    }
    private Int32 mBorderVertical;

    /// <summary>Gets or sets the ControlRowSpacing value.</summary>
    //[Required]
    //[Column("ControlRowSpacing", TypeName="int")]
    public Int32 ControlRowSpacing
    {
      get { return mControlRowSpacing; }
      set
      {
        mControlRowSpacing = ChangedNames.Add(ColumnControlRowSpacing, mControlRowSpacing, value);
      }
    }
    private Int32 mControlRowSpacing;

    /// <summary>Gets or sets the ControlRowHeight value.</summary>
    //[Required]
    //[Column("ControlRowHeight", TypeName="int")]
    public Int32 ControlRowHeight
    {
      get { return mControlRowHeight; }
      set
      {
        mControlRowHeight = ChangedNames.Add(ColumnControlRowHeight, mControlRowHeight, value);
      }
    }
    private Int32 mControlRowHeight;

    /// <summary>Gets or sets the ControlsHeight value.</summary>
    //[Required]
    //[Column("ControlsHeight", TypeName="int")]
    public Int32 ContentHeight
    {
      get { return mContentHeight; }
      set
      {
        mContentHeight = ChangedNames.Add(PropertyContentHeight, mContentHeight
          , value);
      }
    }
    private Int32 mContentHeight;

    /// <summary>Gets or sets the ColumnRowCount value.</summary>
    //[Required]
    //[Column("ColumnRowCount", TypeName="int")]
    public Int32 ColumnRowCount
    {
      get { return mColumnRowCount; }
      set
      {
        mColumnRowCount = ChangedNames.Add(ColumnColumnRowCount, mColumnRowCount
          , value);
      }
    }
    private Int32 mColumnRowCount;
    #endregion

    #region Join Data and Calculated Properties

    /// <summary>Gets or sets the Calculated ContentWidth value.</summary>
    public Int32 ContentWidth { get; set; }

    #endregion

    #region Related Data Properties

    /// <summary>The ControlDetail Object contained ControlTabItems.</summary>
    public ControlTabItems ControlTabItems { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the Object ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The Name column.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The table name.</summary>
    public static string DataTableName = "DetailDialog";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataConfigName column name.</summary>
    public static string ColumnDataConfigName = "DataConfigName";

    /// <summary>The TableName column name.</summary>
    public static string ColumnTableName = "TableName";

    /// <summary>The UserID column name.</summary>
    public static string ColumnUserID = "UserID";

    /// <summary>The DataValueCount column name.</summary>
    public static string ColumnDataValueCount = "DataValueCount";

    /// <summary>The ColumnRowsLimit column name.</summary>
    public static string ColumnColumnRowsLimit = "ColumnRowsLimit";

    /// <summary>The PageColumnsLimit column name.</summary>
    public static string ColumnPageColumnsLimit = "PageColumnsLimit";

    /// <summary>The CharacterPixels column name.</summary>
    public static string ColumnCharacterPixels = "CharacterPixels";

    /// <summary>The MaxControlCharacters column name.</summary>
    public static string ColumnMaxControlCharacters = "MaxControlCharacters";

    /// <summary>The BorderHorizontal column name.</summary>
    public static string ColumnBorderHorizontal = "BorderHorizontal";

    /// <summary>The BorderVertical column name.</summary>
    public static string ColumnBorderVertical = "BorderVertical";

    /// <summary>The ControlRowSpacing column name.</summary>
    public static string ColumnControlRowSpacing = "ControlRowSpacing";

    /// <summary>The ControlRowHeight column name.</summary>
    public static string ColumnControlRowHeight = "ControlRowHeight";

    /// <summary>The ContentHeight column name.</summary>
    public static string ColumnContentHeight = "ControlsHeight";

    /// <summary>The ContentHeight column name.</summary>
    public static string PropertyContentHeight = "ContentHeight";

    /// <summary>The ColumnColumnRowCount column name.</summary>
    public static string ColumnColumnRowCount = "ColumnRowCount";

    /// <summary>The UserID maximum length.</summary>
    public static int LengthUserID = 60;

    /// <summary>The DataConfigName maximum length.</summary>
    public static int LengthDataConfigName = 60;

    /// <summary>The TableName maximum length.</summary>
    public static int LengthTableName = 60;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Unique key.</summary>
  public class ControlDetailUniqueComparer : IComparer<ControlDetail>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public int Compare(ControlDetail x, ControlDetail y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        // Not case sensitive.
        retValue = string.Compare(x.UserID, y.UserID, true);
        if (0 == retValue)
        {
          // Not case sensitive.
          retValue = string.Compare(x.DataConfigName, y.DataConfigName, true);
          if (0 == retValue)
          {
            // Not case sensitive.
            retValue = string.Compare(x.TableName, y.TableName, true);
          }
        }
      }
      return retValue;
    }
  }
  #endregion
}
