// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataDetailManagers.cs
using LJCDataDetailDAL;
using LJCDBClientLib;

namespace LJCDataDetailDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class DataDetailManagers
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DataDetailManagers()
    {
    }
    #endregion

    // Sets the DB properties
    /// <include path='items/SetDBProperties/*' file='Doc/DataDetailManagers.xml'/>
    public void SetDBProperties(DbServiceRef dbServiceRef
      , string dataConfigName)
    {
      mDbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
    }

    #region Properties

    /// <summary>Gets the ControlDetailManager object.</summary>
    public ControlDetailManager ControlDetailManager
    {
      get
      {
        if (null == mControlDetailManager)
        {
          ControlDetailManager
            = new ControlDetailManager(mDbServiceRef, mDataConfigName);
        }
        return mControlDetailManager;
      }

      private set
      {
        if (value != null)
        {
          mControlDetailManager = value;
        }
      }
    }
    private ControlDetailManager mControlDetailManager;

    /// <summary>Gets the ControlTabManager object.</summary>
    public ControlTabManager ControlTabManager
    {
      get
      {
        if (null == mControlTabManager)
        {
          ControlTabManager
            = new ControlTabManager(mDbServiceRef, mDataConfigName);
        }
        return mControlTabManager;
      }

      private set
      {
        if (value != null)
        {
          mControlTabManager = value;
        }
      }
    }
    private ControlTabManager mControlTabManager;

    /// <summary>Gets the ControlColumnManager object.</summary>
    public ControlColumnManager ControlColumnManager
    {
      get
      {
        if (null == mControlColumnManager)
        {
          ControlColumnManager
            = new ControlColumnManager(mDbServiceRef, mDataConfigName);
        }
        return mControlColumnManager;
      }

      private set
      {
        if (value != null)
        {
          mControlColumnManager = value;
        }
      }
    }
    private ControlColumnManager mControlColumnManager;

    /// <summary>Gets the ControlRowManager object.</summary>
    public ControlRowManager ControlRowManager
    {
      get
      {
        if (null == mControlRowManager)
        {
          ControlRowManager
            = new ControlRowManager(mDbServiceRef, mDataConfigName);
        }
        return mControlRowManager;
      }

      private set
      {
        if (value != null)
        {
          mControlRowManager = value;
        }
      }
    }
    private ControlRowManager mControlRowManager;
    #endregion

    #region Class Data

    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    #endregion
  }
}
