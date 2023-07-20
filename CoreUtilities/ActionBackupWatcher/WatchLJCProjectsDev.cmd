echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem WatchLJCProjectsDev.cmd
echo:

set watchPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev"
set changeFile="bin\ChangeFile.txt" 
set multiFilter="*.cs, *.csproj, *.sln, *.config, *.cmd, Doc\*.xml, -ChangeFile.txt, *.txt"

echo bin\BackupWatcher %watchPath% %changeFile%  %multiFilter%
bin\BackupWatcher %watchPath% %changeFile%  %multiFilter%
pause