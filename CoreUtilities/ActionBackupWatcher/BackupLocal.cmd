echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd
echo:

set changeFile="bin\ChangeFile.txt"
set sourceFolder="LJCProjectsDev"

set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP"
echo -----
echo bin\BackupChanges %targetPath% %changeFile% %sourceFolder%
bin\BackupChanges %targetPath% %changeFile% %sourceFolder%

set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage"
echo -----
echo bin\BackupChanges %targetPath% %changeFile% %sourceFolder%
bin\BackupChanges %targetPath% %changeFile% %sourceFolder%

set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects"
echo -----
echo bin\BackupChanges %targetPath% %changeFile% %sourceFolder%
bin\BackupChanges %targetPath% %changeFile% %sourceFolder%
pause