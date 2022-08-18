echo Copyright (c) Lester J. Clark 2020 - All Rights Reserved
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\DataDetail
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=DataDetail
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=DataDetail
set File=DataDetail
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /y

set Project=LJCDataDetailConsole
set File=LJCDataDetailConsole
call %ClearBuild%
del %Solution%\%Project%\%bin%\DataDetail.exe

set Project=LJCDataDetailLib
set File=LJCDataDetailLib
call %ClearBuild%

set Project=LJCTestDataLib
set File=LJCTestDataLib
call %ClearBuild%
