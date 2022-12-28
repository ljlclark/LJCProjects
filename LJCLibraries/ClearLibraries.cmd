echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearLibraries.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCLibraries
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCLibraries
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDataAccess
set File=LJCDataAccess
call %ClearBuild%
del %Solution%\Output\*.* /q
del %Solution%\%Project%\%bin%\LJCNetCommon.xml

set Project=LJCWinFormCommon
set File=LJCWinFormCommon
call %ClearBuild%

set Project=LJCWinFormControls
set File=LJCWinFormControls
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCNetCommon.xml
del %Solution%\%Project%\%bin%\LJCWinFormCommon.xml

set Project=TestDataAccess
set File=TestDataAccess
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataAccess.xml
del %Solution%\%Project%\%bin%\LJCNetCommon.xml

set Project=TestList
set File=TestList
call %ClearBuild%
