// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDocLib.cs
using LJCDBClientLib;
using LJCDBDataAccess;

namespace LJCDocLibDAL
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesDocGen
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ValuesDocGen()
    {
      var dataConfigName = "LJCData";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName)
      };
      Managers = new ManagersDocGen();
      Managers.SetDBProperties(dbServiceRef, dataConfigName);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the generated page count.</summary>
    public int GenPageCount { get; set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDocGen Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    /// <summary>Initialize Singleton.</summary>
    public static readonly ValuesDocGen mInstance = new ValuesDocGen();
    #endregion
  }
}
