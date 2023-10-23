using LJCDocLibDAL;

namespace GenDocScript
{
  // Common GenDocScript Functions.
  internal class CommonGenDocScript
  {
    // Returns the DAL Managers object.
    public static ManagersDocGen GetManagers()
    {
      var configValues = ValuesDocGen.Instance;
      configValues.SetConfigFile("GenDocScript.exe.config");
      var retValue = configValues.Managers;
      return retValue;
    }
  }
}
