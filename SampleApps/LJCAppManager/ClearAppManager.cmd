echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearAppManager.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCAppManager
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=SampleApps\LJCAppManager
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCAppManager
set File=LJCAppManager
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\Logs /q
del %Solution%\%Project%\%bin%\LJCFacilityManager.exe
del %Solution%\%Project%\%bin%\LJCRegionManager.exe
del %Solution%\%Project%\%bin%\LJCRegionForm.exe
del %Solution%\%Project%\%bin%\LJCViewBuilder.exe
del %Solution%\%Project%\%bin%\LJCTransformManager.exe
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.dll
del %Solution%\%Project%\%bin%\LJCProjectManagerDAL.xml
del %Solution%\%Project%\%bin%\TransformServiceTest.exe
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.exe
del %Solution%\%Project%\%bin%\LJCAppManagerDAL.xml
del %Solution%\%Project%\%bin%\ProcessStepModule.txt
del %Solution%\%Project%\%bin%\TestProcess.txt

set Project=LJCAppManagerDAL
set File=LJCAppManagerDAL
call %ClearBuild%
