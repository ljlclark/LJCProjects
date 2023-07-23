echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBackupWatcherHosts.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\BackupWatcherHosts
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\BackupWatcherHosts
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=BackupWatcherConsoleHost
set File=BackupWatcherConsoleHost
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release
