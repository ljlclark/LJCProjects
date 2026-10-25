// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDbServiceRef5.cs
using LJCDBDataAccess5;
//using LJCDBServiceLib5;

namespace LJCDBClientLib5
{
  // Contains the DbDataAccess, DbService and DbServiceClient proxy references.
  /// <include file='Doc/DbServiceRef.xml'
  ///  path='members/LJCDbServiceRef/*'/>
  public class LJCDbServiceRef
  {
    #region Static Methods

    // Checks for the required object values.
    /// <include file='Doc/DbServiceRef.xml'
    ///  path='members/ItemValues/*'/>
    public static string ItemValues(LJCDbServiceRef dbServiceRef)
    {
      string retValue = "";

      if (null == dbServiceRef)
      {
        retValue += "dbServiceRef\r\n";
      }
      else
      {
        if (null == dbServiceRef.DbDataAccess)
        //&& null == dbServiceRef.DbService
        //&& null == dbServiceRef.DbServiceClient)
        {
          retValue += "dbServiceRef.DbDataAccess\r\n";
          //retValue += "dbServiceRef.DbService\r\n";
          //retValue += "dbServiceRef.DbServiceClient\r\n";
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the DbDataAccess reference.
    /// <include file='Doc/DbServiceRef.xml'
    ///  path='members/DbDataAccess/*'/>
    public DbDataAccess? DbDataAccess { get; set; }

    // Gets or sets the DbService reference.
    // <include path='members/DbService/*' file='Doc/DbServiceRef.xml'/>
    //public DbService DbService { get; set; }

    // Gets or sets the DbServiceClient proxy reference.
    // <include file='Doc/DbServiceRef.xml'
    //  path='members/DbServiceClient/*'/>
    //public DbServiceClient DbServiceClient { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/DbServiceRef.xml'
    ///  path='members/Constructor/*'/>
    public LJCDbServiceRef()
    {
    }
    #endregion
  }
}
