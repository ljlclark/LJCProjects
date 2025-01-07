// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlDataItems.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace LJCDataDetailDAL
{
  /// <summary>Represents a collection of ControlData objects.</summary>
  public class ControlDataItems : List<ControlData>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDataItems()
    {
      mPrevCount = -1;
      mArgError = new ArgError("LJCDataDetail.ControlDataItems");
    }
    private ArgError mArgError;

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='Doc/ControlColumns.xml'/>
    public ControlDataItems(ControlDataItems items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ControlData(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../Doc/ControlDataItems.xml'/>
    public ControlData Add(int id, int controlDetailID)
    {
      ControlData retValue;

      mArgError.MethodName = "Add";
      string message;
      if (id <= 0)
      {
        message = "id must be greater than zero.\r\n";
        mArgError.Add(message);
      }
      if (controlDetailID <= 0)
      {
        message = "controlDetailID must be greater than zero.\r\n";
        mArgError.Add(message);
      }
      NetString.ThrowArgError(mArgError.ToString());

      retValue = new ControlData()
      {
        ID = id,
        ControlDetailID = controlDetailID,
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlDataItems Clone()
    {
      var retValue = new ControlDataItems();
      foreach (ControlData controlData in this)
      {
        retValue.Add(controlData.Clone());
      }
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DbColumns DbColumns()
    {
      DbColumns retValue = null;

      if (HasItems())
      {
        retValue = new DbColumns();
        foreach (ControlData controlData in this)
        {
          var dbColumn = controlData.GetDbColumnValues();
          retValue.Add(dbColumn);
        }
      }
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public ControlDataItems GetCollection(List<ControlData> list)
    {
      ControlDataItems retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new ControlDataItems();
        foreach (ControlData item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchID/*' file='Doc/ControlColumns.xml'/>
    public ControlData LJCSearchID(long id)
    {
      ControlData retValue = null;

      LJCSortID();
      ControlData searchItem = new ControlData()
      {
        ID = id
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on ID.</summary>
    public void LJCSortID()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
