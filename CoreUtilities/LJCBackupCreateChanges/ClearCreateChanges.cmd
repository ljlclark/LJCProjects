echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearCreateChanges.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCBackupCreateChanges
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCCreateFileChanges
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCCreateFileChanges
set File=LJCCreateFileChanges
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release

set Project=LJCCreateFileChangesLib
set File=LJCCreateFileChangesLib
call %ClearBuild%
rmdir %Solution%\%Project%\bin\Release
