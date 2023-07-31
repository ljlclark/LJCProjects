echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem LJCWebPagesBackup.cmd
echo:

set mainPath="C:\Users\Les\Documents\Visual Studio 2022
set changeFileSpec="bin\PagesChangeFile.txt"
set changeFilePath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles
set changeFileSpec=%changeFilePath%\PagesChangeFile.txt"
rem set skipFiles="ChangeFile.txt|BuildAll.cmd|ClearBuild.cmd|UpdateAll.cmd"
set startFolder="LJCCodeDoc"

echo -----
set targetPath=%mainPath%\WebPages\LJCCodeDoc"
set binPath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\Bin
echo %binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
%binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
del %changeFileSpec%
