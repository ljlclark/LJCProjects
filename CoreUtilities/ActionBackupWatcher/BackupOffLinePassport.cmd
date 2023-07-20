echo off
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupRemote.cmd

set changeFile="bin\ChangeFile.txt"
set sourceFolder="LJCProjectsDev"

set targetPath="\Visual Studio 2022\LJCProjectsDevBackup"
echo -----
echo bin\BackupChanges %targetPath% %changeFile% %sourceFolder%
bin\BackupChanges %targetPath% %changeFile% %sourceFolder%
