echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd
echo:

set changeFile="bin\ChangeFile.txt"
set sourceFolder="LJCProjectsDev"

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP"
echo bin\LJCBackupChanges %targetPath% %changeFile% %sourceFolder%
bin\LJCBackupChanges %targetPath% %changeFile% %sourceFolder%
