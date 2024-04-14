// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDocLib.cs
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCGenDocDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersGenDoc
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ManagersGenDoc()
    {
      ArgError = new ArgError("LJCGenDocDAL.ManagersGenDoc");
    }

    /// <summary>
    /// Sets the DB properties.
    /// </summary>
    /// <param name="dbServiceRef">The database service reference object.</param>
    /// <param name="dataConfigName">The data configuration name.</param>
    public void SetDBProperties(DbServiceRef dbServiceRef
      , string dataConfigName)
    {
      ArgError.MethodName = "SetDBProperties(dbServiceRef, dataConfigName)";
      ArgError.Add(DbServiceRef.ItemValues(dbServiceRef));
      ArgError.Add(dataConfigName, "dataConfigName");
      NetString.ThrowArgError(ArgError.ToString());

      mDbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
    }
    #endregion

    #region Manager Properties

    /// <summary>Gets the DocAssemblyGroupManager object.</summary>
    public DocAssemblyGroupManager DocAssemblyGroupManager
    {
      get
      {
        if (null == mDocAssemblyGroupManager)
        {
          DocAssemblyGroupManager = new DocAssemblyGroupManager(mDbServiceRef
            , mDataConfigName);
        }
        return mDocAssemblyGroupManager;
      }

      private set
      {
        if (value != null)
        {
          mDocAssemblyGroupManager = value;
        }
      }
    }
    private DocAssemblyGroupManager mDocAssemblyGroupManager;

    /// <summary>Gets the DocAssemblyManager object.</summary>
    public DocAssemblyManager DocAssemblyManager
    {
      get
      {
        if (null == mDocAssemblyManager)
        {
          DocAssemblyManager = new DocAssemblyManager(mDbServiceRef
            , mDataConfigName);
        }
        return mDocAssemblyManager;
      }

      private set
      {
        if (value != null)
        {
          mDocAssemblyManager = value;
        }
      }
    }
    private DocAssemblyManager mDocAssemblyManager;

    /// <summary>Gets the DocClassGroupHeadingManager object.</summary>
    public DocClassGroupHeadingManager DocClassGroupHeadingManager
    {
      get
      {
        if (null == mDocClassGroupHeadingManager)
        {
          DocClassGroupHeadingManager
            = new DocClassGroupHeadingManager(mDbServiceRef, mDataConfigName);
        }
        return mDocClassGroupHeadingManager;
      }

      private set
      {
        if (value != null)
        {
          mDocClassGroupHeadingManager = value;
        }
      }
    }
    private DocClassGroupHeadingManager mDocClassGroupHeadingManager;

    /// <summary>Gets the DocClassGroupManager object.</summary>
    public DocClassGroupManager DocClassGroupManager
    {
      get
      {
        if (null == mDocClassGroupManager)
        {
          DocClassGroupManager = new DocClassGroupManager(mDbServiceRef
            , mDataConfigName);
        }
        return mDocClassGroupManager;
      }

      private set
      {
        if (value != null)
        {
          mDocClassGroupManager = value;
        }
      }
    }
    private DocClassGroupManager mDocClassGroupManager;

    /// <summary>Gets the DocClassManager object.</summary>
    public DocClassManager DocClassManager
    {
      get
      {
        if (null == mDocClassManager)
        {
          DocClassManager = new DocClassManager(mDbServiceRef
            , mDataConfigName);
        }
        return mDocClassManager;
      }

      private set
      {
        if (value != null)
        {
          mDocClassManager = value;
        }
      }
    }
    private DocClassManager mDocClassManager;

    /// <summary>Gets the DocMethodGroupHeadingManager object.</summary>
    public DocMethodGroupHeadingManager DocMethodGroupHeadingManager
    {
      get
      {
        if (null == mDocMethodGroupHeadingManager)
        {
          DocMethodGroupHeadingManager
            = new DocMethodGroupHeadingManager(mDbServiceRef, mDataConfigName);
        }
        return mDocMethodGroupHeadingManager;
      }

      private set
      {
        if (value != null)
        {
          mDocMethodGroupHeadingManager = value;
        }
      }
    }
    private DocMethodGroupHeadingManager mDocMethodGroupHeadingManager;

    /// <summary>Gets the DocMethodGroupManager object.</summary>
    public DocMethodGroupManager DocMethodGroupManager
    {
      get
      {
        if (null == mDocMethodGroupManager)
        {
          DocMethodGroupManager = new DocMethodGroupManager(mDbServiceRef
            , mDataConfigName);
        }
        return mDocMethodGroupManager;
      }

      private set
      {
        if (value != null)
        {
          mDocMethodGroupManager = value;
        }
      }
    }
    private DocMethodGroupManager mDocMethodGroupManager;

    /// <summary>Gets the DocMethodManager object.</summary>
    public DocMethodManager DocMethodManager
    {
      get
      {
        if (null == mDocMethodManager)
        {
          DocMethodManager = new DocMethodManager(mDbServiceRef
            , mDataConfigName);
        }
        return mDocMethodManager;
      }

      private set
      {
        if (value != null)
        {
          mDocMethodManager = value;
        }
      }
    }
    private DocMethodManager mDocMethodManager;
    #endregion

    #region Class Properties

    // Represents Argument errors.
    private ArgError ArgError { get; set; }

    private string FilePath { get; set; }
    #endregion

    #region Class Data

    private string mDataConfigName;
    private DbServiceRef mDbServiceRef;
    #endregion
  }
}
