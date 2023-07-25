echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocalBKP.cmd
echo:

call DeleteGenFiles.cmd
set sourcePath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev"
set changeFileSpec="bin\ChangeFile.txt"
set multiFilter="*.cs|*.cproj|*.sln|*.config|*.cmd|*.txt"
set skipFiles="ChangeFile.txt|BuildAll.cmd|ClearBuild.cmd|UpdateAll.cmd"
set startFolder="LJCProjectsDev"

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP"
rem del %changeFileSpec%
echo bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFilSpec% %multiFilter% %skipFiles%
bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
echo bin\LJCBackupChanges %targetPath% %changeFileSpec% %startFolder%
bin\LJCBackupChanges %targetPath% %changeFileSpec% %startFolder%
pause