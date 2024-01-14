echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearViewControls.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBViewControls
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCDBViewControls
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBViewControls
set File=LJCDBViewControls
call %ClearBuild%
del %Solution%\%Project%\%bin%\ViewEditor.exe

