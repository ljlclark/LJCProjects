echo off
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd

set changeFile="ChangeFile.txt"
set sourceFolder="LJCProjectsDev"

set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP"
echo -----
echo BackupChanges %targetPath% %changeFile% %sourceFolder%
BackupChanges %targetPath% %changeFile% %sourceFolder%

set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage"
echo -----
echo BackupChanges %targetPath% %changeFile% %sourceFolder%
BackupChanges %targetPath% %changeFile% %sourceFolder%

set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects"
echo -----
echo BackupChanges %targetPath% %changeFile% %sourceFolder%
BackupChanges %targetPath% %changeFile% %sourceFolder%
pause