echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBackupWatcherHosts.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCBackupWatcherHosts
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCBackupWatcherHosts
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCBackupWatcherConsoleHost
set File=LJCBackupWatcherConsoleHost
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release

set Project=LJCBackupWatcherHost
set File=LJCBackupWatcherHost
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release
