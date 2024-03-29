echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearUnitMeasure.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCUnitMeasure
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=SampleApps\LJCUnitMeasure
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCUnitMeasure
set File=LJCUnitMeasure
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\LJCUnitMeasureDAL.xml

set Project=LJCUnitMeasureDAL
set File=LJCUnitMeasureDAL
call %ClearBuild%
