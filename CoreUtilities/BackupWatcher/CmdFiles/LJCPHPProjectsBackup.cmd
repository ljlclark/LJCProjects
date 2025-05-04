echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ***LJCPHPProjectsBackup.cmd
echo:

set mainPath="C:\Users\Les\Documents\Visual Studio 2022
set sourcePath=%mainPath%\LJCPHPProjectsDev"
set changeFilePath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles
set changeFileSpec=%changeFilePath%\PagesChangeFile.txt"
set multiFilter="ReadMe*.txt|*.cmd|*.php"
set skipFiles="index.html|CodeDoc.html"

set startFolder="GenDoc"
set binPath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\Bin

echo -----
set targetPath=%mainPath%\LJCPHPProjects\GenDoc"

if exist %changeFileSpec% del %changeFileSpec%
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%

if exist %changeFilePath%\BackupLog.txt" del %changeFilePath%\BackupLog.txt
echo %binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
%binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
