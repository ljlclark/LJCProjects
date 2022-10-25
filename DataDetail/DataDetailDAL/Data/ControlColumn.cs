// ControlColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace DataDetailDAL
{
  /// <summary>The ControlColumn table Data Object.</summary>
  public class ControlColumn : IComparable<ControlColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumn()
    {
      ChangedNames = new ChangedNames();
      ControlRows = new ControlRows();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumn(ControlColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DetailConfigID = item.DetailConfigID;
      ColumnIndex = item.ColumnIndex;
      TabPageIndex = item.TabPageIndex;
      LabelsWidth = item.LabelsWidth;
      ControlsWidth = item.ControlsWidth;

      ControlRows = new ControlRows();
      foreach (ControlRow controlRow in item.ControlRows)
      {
        ControlRows.Add(controlRow);
      }
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumn Clone()
    {
      var retValue = MemberwiseClone() as ControlColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlColumn other)
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
      return $"Config:{DetailConfigID}-Tab:{TabPageIndex}-Column:{ColumnIndex}";
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

    /// <summary>Gets or sets the DetailDialogID value.</summary>
    //[Required]
    //[Column("DetailConfigID", TypeName="bigint")]
    public Int64 DetailConfigID
    {
      get { return mDetailConfigID; }
      set
      {
        mDetailConfigID = ChangedNames.Add(ColumnDetailConfigID, mDetailConfigID, value);
      }
    }
    private Int64 mDetailConfigID;

    /// <summary>Gets or sets the ColumnIndex value.</summary>
    //[Required]
    //[Column("ColumnIndex", TypeName="int")]
    public Int32 ColumnIndex
    {
      get { return mColumnIndex; }
      set
      {
        mColumnIndex = ChangedNames.Add(ColumnColumnIndex, mColumnIndex, value);
      }
    }
    private Int32 mColumnIndex;

    /// <summary>Gets or sets the TabPageIndex value.</summary>
    //[Required]
    //[Column("TabPageIndex", TypeName="int")]
    public Int32 TabPageIndex
    {
      get { return mTabPageIndex; }
      set
      {
        mTabPageIndex = ChangedNames.Add(ColumnTabPageIndex, mTabPageIndex, value);
      }
    }
    private Int32 mTabPageIndex;

    /// <summary>Gets or sets the LabelsWidth value.</summary>
    //[Required]
    //[Column("LabelsWidth", TypeName="int")]
    public Int32 LabelsWidth
    {
      get { return mLabelsWidth; }
      set
      {
        mLabelsWidth = ChangedNames.Add(ColumnLabelsWidth, mLabelsWidth, value);
      }
    }
    private Int32 mLabelsWidth;

    /// <summary>Gets or sets the ControlsWidth value.</summary>
    //[Required]
    //[Column("ControlsWidth", TypeName="int")]
    public Int32 ControlsWidth
    {
      get { return mControlsWidth; }
      set
      {
        mControlsWidth = ChangedNames.Add(ColumnControlsWidth, mControlsWidth, value);
      }
    }
    private Int32 mControlsWidth;
    #endregion

    #region Join Data and Calculated Properties

    /// <summary>Gets the Width value.</summary>
    public int Width
    {
      get { return LabelsWidth + ControlsWidth; }
    }

    /// <summary>Gets or sets the RowCount value.</summary>
    public int RowCount { get; set; }
    #endregion

    #region Related Data Properties

    /// <summary>
    /// The ControlColumn contained ControlRows.
    /// </summary>
    public ControlRows ControlRows { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "ControlColumn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DetailDialogID column name.</summary>
    public static string ColumnDetailConfigID = "DetailConfigID";

    /// <summary>The ColumnIndex column name.</summary>
    public static string ColumnColumnIndex = "ColumnIndex";

    /// <summary>The TabPageIndex column name.</summary>
    public static string ColumnTabPageIndex = "TabPageIndex";

    /// <summary>The LabelsWidth column name.</summary>
    public static string ColumnLabelsWidth = "LabelsWidth";

    /// <summary>The ControlsWidth column name.</summary>
    public static string ColumnControlsWidth = "ControlsWidth";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Unique key.</summary>
  public class ControlColumnUniqueComparer : IComparer<ControlColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ControlColumn x, ControlColumn y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        // Case sensitive.
        retValue = x.DetailConfigID.CompareTo(y.DetailConfigID);
        if (0 == retValue)
        {
          // Case sensitive.
          retValue = x.ColumnIndex.CompareTo(y.ColumnIndex);
        }
      }
      return retValue;
    }
  }
  #endregion
}
