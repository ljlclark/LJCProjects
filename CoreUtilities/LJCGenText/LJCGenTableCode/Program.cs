// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCDBServiceLib;
using static System.Console;

namespace LJCGenTableCode
{
  // Program to generate code from database table definitions.
  /// <include path='items/Program/*' file='Doc/ProgramGenTableCode.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main(string[] args)
    {
      if (args.Length < 1)
      {
        WriteLine("Missing a command line argument.");
        WriteLine("Syntax: LJCGenTableCode DataConfigName, [TableName]");
      }
      else
      {
        string dataConfigName = args[0];

        DbServiceRef dbServiceRef = new DbServiceRef
        {
          DbService = new DbService()
        };
        bool hasError = false;

        GenTableFiles genTableFiles = new GenTableFiles();
        GenFileSpecs genFileSpecs = GetFileSpecs();

        DataManager dataManager = new DataManager(dbServiceRef, dataConfigName, null);
        if (2 == args.Length)
        {
          string tableName = args[1];
          dataManager.Reset(null, dataConfigName, tableName);
          genTableFiles.GenFiles(genFileSpecs, dataManager, out hasError);
        }
        else
        {
          DbResult dbResult = dataManager.GetTableNames();
          //if (dbResult != null)
          if (DbResult.HasRows(dbResult))
          {
            foreach (DbRow dbRow in dbResult.Rows)
            {
              string tableName = dbRow.Values.LJCGetValue("TABLE_NAME");
              if (false == tableName.StartsWith("sys"))
              {
                WriteLine($"Generating Table: {tableName}");
                dataManager.Reset(null, dataConfigName, tableName);
                genTableFiles.GenFiles(genFileSpecs, dataManager, out hasError);
                if (hasError)
                {
                  break;
                }
              }
            }
          }
        }
        if (hasError)
        {
          WriteLine("Press any key to continue");
          ReadKey();
        }
      }
    }

    // Creates the Generate File Specs.
    /// <include path='items/GetFileSpecs/*' file='Doc/Program.xml'/>
    public static GenFileSpecs GetFileSpecs()
    {
      GenFileSpecs retValue = GenFileSpecs.LJCDeserialize();
      return retValue;
    }
  }
}
