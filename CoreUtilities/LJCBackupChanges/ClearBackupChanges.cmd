echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBackupChanges.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCBackupChanges
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCBackupWatcherLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCBackupChanges
set File=LJCBackupChanges
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release

set Project=LJCBackupChangesLib
set File=LJCBackupChangesLib
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release

set Project=LJCBackupWatcherLib
set File=LJCBackupWatcherLib
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release
