// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDbView.cs
using LJCDBClientLib;
using System.Dynamic;

namespace LJCDBViewDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersDbView
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public ManagersDbView()
    {
    }

    // Sets the DB properties.
    /// <include path='items/SetDbProperties/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public void SetDbProperties(DbServiceRef dbServiceRef
      , string dataConfigName)
    {
      DataConfigName = dataConfigName;
      DbServiceRef = dbServiceRef;
    }
    #endregion

    #region Properties

    /// <summary>Gets the DataConfig name.</summary>
    public string DataConfigName { get; private set; }

    /// <summary>Gets the DbServiceRef object.</summary>
    public DbServiceRef DbServiceRef { get; private set; }

    /// <summary>Gets the ViewDataManager object.</summary>
    public ViewDataManager ViewDataManager
    {
      get
      {
        if (null == mViewDataManager)
        {
          ViewDataManager = new ViewDataManager(DbServiceRef
            , DataConfigName);
        }
        return mViewDataManager;
      }
      private set
      {
        if (value != null)
        {
          mViewDataManager = value;
        }
      }
    }

    /// <summary>Gets the ViewColumnManager object.</summary>
    public ViewColumnManager ViewColumnManager
    {
      get
      {
        if (null == mViewColumnManager)
        {
          ViewColumnManager = new ViewColumnManager(DbServiceRef
            , DataConfigName);
        }
        return mViewColumnManager;
      }
      private set
      {
        if (value != null)
        {
          mViewColumnManager = value;
        }
      }
    }

    /// <summary>Gets the ViewConditionManager object.</summary>
    public ViewConditionManager ViewConditionManager
    {
      get
      {
        if (null == mViewColumnManager)
        {
          ViewConditionManager = new ViewConditionManager(DbServiceRef
            , DataConfigName);
        }
        return mViewConditionManager;
      }
      private set
      {
        if (value != null)
        {
          mViewConditionManager = value;
        }
      }
    }

    /// <summary>Gets the ViewConditionSetManager object.</summary>
    public ViewConditionSetManager ViewConditionSetManager
    {
      get
      {
        if (null == mViewConditionSetManager)
        {
          ViewConditionSetManager = new ViewConditionSetManager(DbServiceRef
            , DataConfigName);
        }
        return mViewConditionSetManager;
      }
      private set
      {
        if (value != null)
        {
          mViewConditionSetManager = value;
        }
      }
    }

    /// <summary>Gets the ViewFilterManager object.</summary>
    public ViewFilterManager ViewFilterManager
    {
      get
      {
        if (null == mViewFilterManager)
        {
          ViewFilterManager = new ViewFilterManager(DbServiceRef
            , DataConfigName);
        }
        return mViewFilterManager;
      }
      private set
      {
        if (value != null)
        {
          mViewFilterManager = value;
        }
      }
    }

    /// <summary>Gets the ViewViewGridColumnManager object.</summary>
    public ViewGridColumnManager ViewGridColumnManager
    {
      get
      {
        if (null == mViewDataManager)
        {
          ViewGridColumnManager = new ViewGridColumnManager(DbServiceRef
            , DataConfigName);
        }
        return mViewGridColumnManager;
      }
      private set
      {
        if (value != null)
        {
          mViewGridColumnManager = value;
        }
      }
    }

    /// <summary>Gets the ViewJoinColumnManager object.</summary>
    public ViewJoinColumnManager ViewJoinColumnManager
    {
      get
      {
        if (null == mViewJoinColumnManager)
        {
          ViewJoinColumnManager = new ViewJoinColumnManager(DbServiceRef
            , DataConfigName);
        }
        return mViewJoinColumnManager;
      }
      private set
      {
        if (value != null)
        {
          mViewJoinColumnManager = value;
        }
      }
    }

    /// <summary>Gets the ViewJoinManager object.</summary>
    public ViewJoinManager ViewJoinManager
    {
      get
      {
        if (null == mViewJoinManager)
        {
          ViewJoinManager = new ViewJoinManager(DbServiceRef
            , DataConfigName);
        }
        return mViewJoinManager;
      }
      private set
      {
        if (value != null)
        {
          mViewJoinManager = value;
        }
      }
    }

    /// <summary>Gets the ViewJoinOnManager object.</summary>
    public ViewJoinOnManager ViewJoinOnManager
    {
      get
      {
        if (null == mViewJoinOnManager)
        {
          ViewJoinOnManager = new ViewJoinOnManager(DbServiceRef
            , DataConfigName);
        }
        return mViewJoinOnManager;
      }
      private set
      {
        if (value != null)
        {
          mViewJoinOnManager = value;
        }
      }
    }

    /// <summary>Gets the ViewOrderByManager object.</summary>
    public ViewOrderByManager ViewOrderByManager
    {
      get
      {
        if (null == mViewOrderByManager)
        {
          ViewOrderByManager = new ViewOrderByManager(DbServiceRef
            , DataConfigName);
        }
        return mViewOrderByManager;
      }
      private set
      {
        if (value != null)
        {
          mViewOrderByManager = value;
        }
      }
    }

    /// <summary>Gets the ViewTableManager object.</summary>
    public ViewTableManager ViewTableManager
    {
      get
      {
        if (null == mViewTableManager)
        {
          ViewTableManager = new ViewTableManager(DbServiceRef
            , DataConfigName);
        }
        return mViewTableManager;
      }
      private set
      {
        if (value != null)
        {
          mViewTableManager = value;
        }
      }
    }
    #endregion

    #region Class Data

    private ViewDataManager mViewDataManager;
    private ViewColumnManager mViewColumnManager;
    private ViewConditionManager mViewConditionManager;
    private ViewConditionSetManager mViewConditionSetManager;
    private ViewFilterManager mViewFilterManager;
    private ViewGridColumnManager mViewGridColumnManager;
    private ViewJoinColumnManager mViewJoinColumnManager;
    private ViewJoinManager mViewJoinManager;
    private ViewJoinOnManager mViewJoinOnManager;
    private ViewOrderByManager mViewOrderByManager;
    private ViewTableManager mViewTableManager;
    #endregion
  }
}
