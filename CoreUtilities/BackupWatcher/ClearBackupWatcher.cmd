echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBackupWatcher.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\BackupWatcher
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\BackupWatcher
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=Bin
set File=LJCBackupChanges
call %ClearBuild%
del %Solution%\%Project%\*.dll
del %Solution%\%Project%\LJCBackupChanges.exe
s