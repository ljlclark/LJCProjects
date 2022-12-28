echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDataAccessConfig.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDataAccessConfig
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDataAccessConfig
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDataAccessConfig
set File=LJCDataAccessConfig
call %ClearBuild%
