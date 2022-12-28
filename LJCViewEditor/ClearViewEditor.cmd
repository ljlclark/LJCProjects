echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearViewEditor.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCViewEditor
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCViewEditor
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCViewEditor
set File=LJCViewEditor
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\DataDetail.*
del %Solution%\%Project%\%bin%\LJCViewEditorDAL.xml

set Project=LJCViewEditorDAL
set File=LJCViewEditorDAL
call %ClearBuild%
