echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearGridDataTests.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCGridDataTests
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreTestApps\LJCGridDataTests
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCGridDataTests
set File=LJCGridDataTests
call %ClearBuild%
