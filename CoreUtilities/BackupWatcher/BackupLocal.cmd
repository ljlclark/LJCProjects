echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd
echo:

del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\Build.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\Update.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\ProjectFindLinesHTMLX.cmd "
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\ProjectFindLinesX.cmd"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\ProjectFindLinesXMLX.cmd"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\CountLines.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\FindLines.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\LargeFiles.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCGenDocEdit\LJCGenDocEdit\bin\Debug\ExportFiles\*.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug\HTMLPageCount.txt"
del "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug\Missing.txt"

set sourcePath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev"
set changeFile="bin\ChangeFile.txt"
set multiFilter="*.cs|*.cproj|*.sln|*.config|*.cmd|*.txt"
set startFolder="LJCProjectsDev"
set skipFiles="ChangeFile.txt|BuildAll.cmd|ClearBuild.cmd|UpdateAll.cmd"

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage"
del %changeFile%
echo bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter%
bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
echo bin\LJCBackupChanges %targetPath% %changeFile% %startFolder%
bin\LJCBackupChanges %targetPath% %changeFile% %sourceFolder%

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects"
del %changeFile%
echo bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter%
bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
echo bin\LJCBackupChanges %targetPath% %changeFile% %startFolder%
bin\LJCBackupChanges %targetPath% %changeFile% %sourceFolder%
pause