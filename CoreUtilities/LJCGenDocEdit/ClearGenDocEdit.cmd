echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearGenDocEdit.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCGenDocEdit
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCGenDocEdit
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCGenDocEdit
set File=LJCGenDocEdit
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCGenDocEdit.xml
