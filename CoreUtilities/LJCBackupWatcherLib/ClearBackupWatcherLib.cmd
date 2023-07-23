echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBackupWatcherLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\BackupWatcherLib
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\BackupWatcherLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=BackupChanges
set File=BackupChanges
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release

set Project=BackupWatcherLib
set File=BackupWatcherLib
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release
