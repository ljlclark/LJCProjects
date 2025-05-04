echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem  LJCPHPProjectsBackup.cmd
echo:

rem ** Set the code top-level path.
set mainPath="C:\Users\Les\Documents\Visual Studio 2022

rem ** Set the source (from) path.
set startFolder=LJCPHPProjectsDev
set sourcePath=%mainPath%\%startFolder%"

rem ** Set the change file path and name.
set changeFilePath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles
set changeFileSpec=%changeFilePath%\PHPChangeFile.txt"

rem ** Set the files to copy and explicit skips.
set multiFilter="ReadMe*.txt|*.cmd|*.php"
set skipFiles=

echo -----
set targetFolder=
rem set targetPath=%mainPath%\LJCPHPProjects\%targetFolder%"
set targetPath=%mainPath%\LJCPHPProjects"

rem ** Delete the previous changes file PagesChangeFile.txt.
if exist %changeFileSpec% del %changeFileSpec%

set binPath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\Bin

rem ** Create the new changes file PHPChangeFile.txt.
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%

rem ** Delete the previous BackupLog.txt file.
if exist %changeFilePath%\BackupLog.txt" del %changeFilePath%\BackupLog.txt

rem ** Perform the backup using entries from the changes file PHPChangeFile.txt.
echo %binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
%binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
