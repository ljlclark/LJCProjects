echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDBDataAccess.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBDataAccess
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDBDataAccess
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBDataAccess
set File=LJCDBDataAccess
call %ClearBuild%
