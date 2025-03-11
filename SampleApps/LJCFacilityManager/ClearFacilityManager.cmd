echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearFacilityManager.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCFacilityManager
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=SampleApps\LJCFacilityManager
set ClearBuild=ClearBuildDetail.cmd
del %Solution%\%Project%\External\LJCViewEditor.exe

:Clear
set Project=FacilityForm
set File=LJCFacilityForm
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCFacilityManager.exe
del %Solution%\%Project%\%bin%\LJCRegionForm.exe
del %Solution%\%Project%\%bin%\LJCRegionManager.exe
del %Solution%\%Project%\%bin%\LJCViewBuilder.exe
del %Solution%\%Project%\%bin%\LJCViewEditor.exe
del %Solution%\%Project%\%bin%\LJCFacilityManagerDAL.xml
del %Solution%\%Project%\%bin%\LJCFacilityManager.xml

set Project=LJCFacilityManager
set File=LJCFacilityManager
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCRegionForm.exe
del %Solution%\%Project%\%bin%\LJCRegionManager.exe
del %Solution%\%Project%\%bin%\LJCViewBuilder.exe
del %Solution%\%Project%\%bin%\LJCViewEditor.exe
del %Solution%\%Project%\%bin%\LJCFacilityManagerDAL.xml
del %Solution%\Output\*.* /q

set Project=LJCFacilityManagerDAL
set File=LJCFacilityManagerDAL
call %ClearBuild%

set Project=ModuleHost
set File=ModuleHost
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues*.xml
del %Solution%\%Project%\%bin%\LJCFacilityManager.exe
del %Solution%\%Project%\%bin%\LJCRegionForm.exe
del %Solution%\%Project%\%bin%\LJCRegionManager.exe
del %Solution%\%Project%\%bin%\LJCViewBuilder.exe
del %Solution%\%Project%\%bin%\LJCViewEditor.exe
