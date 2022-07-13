echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBMessage
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDBMessage
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBMessage
set File=LJCDBMessage
call %ClearBuild%

set Project=CipherLib
set File=CipherLib
call %ClearBuild%

set Project=ConsoleApp1
set File=ConsoleApp1
call %ClearBuild%
