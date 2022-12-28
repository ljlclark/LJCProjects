echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDataAccess.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDataAccess
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDataAccess
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDataAccess
set File=LJCDataAccess
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCNetCommon.xml
