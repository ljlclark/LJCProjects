using LJCNetCommon;

namespace LJCNetCommonTest
{
  internal class NetFileTest
  {
    internal static void Test()
    {
      CreateFolder();
    }

    // Creates a Folder Path if it does not already exist.
    private static void CreateFolder()
    {
      string fileSpec = @"SubFolder\File.txt";

      // Creates folder "SubFolder" from the current folder.
      NetFile.CreateFolder(fileSpec);
    }
  }
}
