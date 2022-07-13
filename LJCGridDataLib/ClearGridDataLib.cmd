echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem ClearGridDataLib.cmd
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCGridDataLib
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCGridDataLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCGridDataLib
set File=LJCGridDataLib
%ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /y
