echo off
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem WatchLJCProjectsDev.cmd

set watchPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev"
set changeFile="ChangeFile.txt" 
set multiFilter="*.cs, *.csproj, *.sln, *.config, *.cmd, Doc\*.xml, -ChangeFile.txt, *.txt"

echo BackupWatcher %watchPath% %changeFile%  %multiFilter%
BackupWatcher %watchPath% %changeFile%  %multiFilter%