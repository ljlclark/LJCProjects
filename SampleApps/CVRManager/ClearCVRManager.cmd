echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearCVRManager.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\CVRManager
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=SampleApps\CVRManager
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=CVRManager
set File=CVRManager
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /y
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\CVRManagerDAL.xml
del %Solution%\%Project%\%bin%\LJCRegionForm.exe
del %Solution%\%Project%\%bin%\LJCRegionManager.exe
del %Solution%\%Project%\%bin%\CVRManager.log

set Project=CVRDAL
set File=CVRDAL
call %ClearBuild%

set Project=CVRItem
set File=CVRItem
call %ClearBuild%
