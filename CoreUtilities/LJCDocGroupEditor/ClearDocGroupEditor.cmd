echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDocGroupEditor.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDocGroupEditor
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCDocGroupEditor
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDocGroupEditor
set File=LJCDocGroupEditor
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
