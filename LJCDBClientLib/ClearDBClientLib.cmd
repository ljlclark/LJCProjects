echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem ClearDBClientLib.cmd
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBClientLib
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDBClientLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBClientLib
set File=LJCDBClientLib
call %ClearBuild%

set Project=TestObjectManager
set File=TestObjectManager
call %ClearBuild%
del %Solution%\%Project%\bin\Debug\LJCDBClientLib.xml