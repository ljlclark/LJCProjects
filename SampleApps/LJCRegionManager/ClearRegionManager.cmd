echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearRegionManager.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCRegionManager
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=SampleApps\LJCRegionManager
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCRegionManager
set File=LJCRegionManager
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCRegionManagerDAL.xml
del %Solution%\%Project%\%bin%\LJCFacilityManager.exe
del %Solution%\%Project%\%bin%\LJCRegionManager.exe
del %Solution%\%Project%\%bin%\LJCRegionForm.exe

set Project=LJCRegionDAL
set File=LJCRegionDAL
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCRegionDAL.xml

set Project=LJCRegionForm
set File=LJCRegionForm
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCRegionManagerDAL.xml
del %Solution%\%Project%\%bin%\LJCRegionManager.exe

set Project=LJCRegionItem
set File=LJCRegionItem
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCRegionItem.xml
