// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbServiceRef.cs
using LJCDBDataAccess5;
//using LJCDBServiceLib5;

namespace LJCDBClientLib5
{
  // Contains the DbDataAccess, DbService and DbServiceClient proxy references.
  /// <include path='items/DbServiceRef/*' file='Doc/DbServiceRef.xml'/>
  public class LJCDbServiceRef
  {
    public LJCDbServiceRef()
    {
    }

    // Checks for the required object values.
    /// <summary>
    /// Checks for the required object values.
    /// </summary>
    /// <param name="dbServiceRef"></param>
    /// <returns></returns>
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

    // Gets or sets the DbDataAccess reference.
    /// <include path='items/DbDataAccess/*' file='Doc/DbServiceRef.xml'/>
    public DbDataAccess? DbDataAccess { get; set; }

    // Gets or sets the DbService reference.
    /// <include path='items/DbService/*' file='Doc/DbServiceRef.xml'/>
    //public DbService DbService { get; set; }

    // Gets or sets the DbServiceClient proxy reference.
    /// <include path='items/DbServiceClient/*' file='Doc/DbServiceRef.xml'/>
    //public DbServiceClient DbServiceClient { get; set; }
  }
}
