echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd
echo:

call DeleteGenFiles.cmd

set sourcePath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev"
set changeFile="bin\ChangeFile.txt"
set multiFilter="*.cs|*.cproj|*.sln|*.config|*.cmd|*.txt"
set skipFiles="ChangeFile.txt|BuildAll.cmd|ClearBuild.cmd|UpdateAll.cmd"
set startFolder="LJCProjectsDev"

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage"
echo bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
echo bin\LJCBackupChanges %targetPath% %changeFile% %startFolder%
bin\LJCBackupChanges %targetPath% %changeFile% %startFolder%
