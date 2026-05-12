echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearExecuteScript.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCExecuteScript
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCExecuteScript
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCExecuteScript
set File=LJCExecuteScript
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
