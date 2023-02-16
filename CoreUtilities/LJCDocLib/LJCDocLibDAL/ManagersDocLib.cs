// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDocLib.cs
using LJCDBClientLib;

namespace LJCDocLibDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersDocLib
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ManagersDocLib()
    {
    }

    /// <summary>
    /// Sets the DB properties.
    /// </summary>
    /// <param name="dbServiceRef">The database service reference object.</param>
    /// <param name="dataConfigName">The data configuration name.</param>
    public void SetDBProperties(DbServiceRef dbServiceRef
      , string dataConfigName)
    {
      mDbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
    }
    #endregion

    #region Properties

    /// <summary>Gets the DocAssemblyGroupManager object.</summary>
    public DocAssemblyGroupManager DocAssemblyGroupManager
    {
      get
      {
        if (null == mDocAssemblyGroupManager)
        {
          DocAssemblyGroupManager = new DocAssemblyGroupManager(mDbServiceRef, mDataConfigName);
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
    #endregion

    #region Class Data

    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    private DocAssemblyGroupManager mDocAssemblyGroupManager;
    #endregion
  }
}
