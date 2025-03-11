// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDataSite.cs
using System;
using System.Reflection;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataSiteDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersDataSite
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ManagersDataSite()
    {
      mArgError = new ArgError("LJCDataUtility.ManagersDataSite");
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

    #region Data Methods

    /// <summary>Gets the DataEntry by ID.</summary>
    public DataEntry GetDataEntry(long id)
    {
      DataEntry retDataEntry = null;

      IDError(id, "GetDataEntry(long id)", "id");
      var manager = DataEntryManager;
      if (manager != null)
      {
        retDataEntry = manager.RetrieveWithID(id);
      }
      return retDataEntry;
    }

    /// <summary>Gets the DataColumn by ID.</summary>
    public DataSite GetDataSite(long id)
    {
      DataSite retDataSite = null;

      IDError(id, "GetDataSite(long id)", "id");
      var manager = DataSiteManager;
      if (manager != null)
      {
        retDataSite = manager.RetrieveWithID(id);
      }
      return retDataSite;
    }

    /// <summary>Gets the DataColumn by Name.</summary>
    public DataSite GetDataSiteUnique(string name)
    {
      DataSite retDataSite = null;

      NameError(name, "GetDataSiteUnique(string name)", "name");
      var manager = DataSiteManager;
      if (manager != null)
      {
        retDataSite = manager.RetrieveWithName(name);
      }
      return retDataSite;
    }

    // Throws an ID error if id is less than 1.
    private void IDError(long id, string methodName, string argument)
    {
      if (id < 1)
      {
        mArgError.MethodName = methodName;
        var message = $"Param {argument} must be greater than zero.\r\n";
        mArgError.Add(message);
        NetString.ThrowArgError(mArgError.ToString());
      }
    }

    // Throws a Name error if name is missing.
    private void NameError(string name, string methodName, string argument)
    {
      if (!NetString.HasValue(name))
      {
        mArgError.MethodName = methodName;
        var message = $"Param {argument} must not be null.\r\n";
        mArgError.Add(message);
        NetString.ThrowArgError(mArgError.ToString());
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the DataSiteManager object.</summary>
    public DataSiteManager DataSiteManager
    {
      get
      {
        if (null == mDataSiteManager)
        {
          mDataSiteManager
            = new DataSiteManager(mDbServiceRef, mDataConfigName);
        }
        return mDataSiteManager;
      }
    }
    private DataSiteManager mDataSiteManager;

    /// <summary>Gets the DataEntryManager object.</summary>
    public DataEntryManager DataEntryManager
    {
      get
      {
        if (null == mDataEntryManager)
        {
          mDataEntryManager
            = new DataEntryManager(mDbServiceRef, mDataConfigName);
        }
        return mDataEntryManager;
      }
    }
    private DataEntryManager mDataEntryManager;

    /// <summary>Gets the DataEntryManager object.</summary>
    public DataEntrySiteManager DataEntrySiteManager
    {
      get
      {
        if (null == mDataEntrySiteManager)
        {
          mDataEntrySiteManager
            = new DataEntrySiteManager(mDbServiceRef, mDataConfigName);
        }
        return mDataEntrySiteManager;
      }
    }
    private DataEntrySiteManager mDataEntrySiteManager;
    #endregion

    #region Class Data

    private readonly ArgError mArgError;
    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    #endregion
  }
}
