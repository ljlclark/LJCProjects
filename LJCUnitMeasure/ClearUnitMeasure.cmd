echo Copyright (c) Lester J. Clark 2021 - All Rights Reserved
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCUnitMeasure
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCUnitMeasure
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCUnitMeasure
set File=LJCUnitMeasure
call %ClearBuild%

set Project=LJCUnitMeasureDAL
set File=LJCUnitMeasureDAL
call %ClearBuild%
