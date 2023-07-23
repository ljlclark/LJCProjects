echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupRemote.cmd
echo:

set changeFile="bin\ChangeFile.txt"
set sourceFolder="LJCProjectsDev"

echo -----
set targetPath="\Visual Studio 2022\LJCProjectsDevBackup"
echo bin\LJCBackupChanges %targetPath% %changeFile% %sourceFolder%
bin\LJCBackupChanges %targetPath% %changeFile% %sourceFolder%
pause