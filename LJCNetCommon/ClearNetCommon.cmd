echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCNetCommon
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCNetCommon
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCNetCommon
set File=LJCNetCommon
call %ClearBuild%
