// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// TableManagersTemplate.cs
using LJCDBClientLib;

namespace DataDetailDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class DataDetailManagers
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
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

    /// <summary>Gets the DetailConfigManager object.</summary>
    public DetailConfigManager DetailConfigManager
    {
      get
      {
        if (null == mDetailConfigManager)
        {
          DetailConfigManager
            = new DetailConfigManager(mDbServiceRef, mDataConfigName);
        }
        return mDetailConfigManager;
      }

      private set
      {
        if (value != null)
        {
          mDetailConfigManager = value;
        }
      }
    }
    private DetailConfigManager mDetailConfigManager;

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
