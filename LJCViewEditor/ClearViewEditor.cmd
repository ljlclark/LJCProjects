echo Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
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
