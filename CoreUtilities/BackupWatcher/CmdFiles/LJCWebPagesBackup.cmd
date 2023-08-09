echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ***LJCWebPagesBackup.cmd
echo:

set mainPath="C:\Users\Les\Documents\Visual Studio 2022
set sourcePath=%mainPath%\WebSitesDev\CodeDoc\LJCCodeDoc"
set changeFilePath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles
set changeFileSpec=%changeFilePath%\PagesChangeFile.txt"
set multiFilter="*.html"
set skipFiles="index.html|CodeDoc.html"

set startFolder="LJCCodeDoc"
set binPath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\Bin

echo -----
set targetPath=%mainPath%\WebPages\LJCCodeDoc"

if exist %changeFileSpec% del %changeFileSpec%
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%

if exist %changeFilePath%\BackupLog.txt" del %changeFilePath%\BackupLog.txt
echo %binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
%binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
