echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearGridDataLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCGridDataLib
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCGridDataLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCGridDataLib
set File=LJCGridDataLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /y

set Project=GridDataTest
set File=GridDataTest
call %ClearBuild%
