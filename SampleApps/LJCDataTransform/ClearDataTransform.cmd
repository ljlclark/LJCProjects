echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDataTransform.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDataTransform
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=SampleApps\LJCDataTransform
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCCommonModuleLib
set File=LJCCommonModuleLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.dll.config
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.xml

set Project=LJCDataTransformDAL
set File=LJCDataTransformDAL
call %ClearBuild%

set Project=LJCDataTransformProcess
set File=LJCDataTransformProcess
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\Logs\ProcessGroup*.txt
del %Solution%\%Project%\%bin%\Logs\*Group.txt
del %Solution%\%Project%\%bin%\Logs\DataProcess*.txt
del %Solution%\%Project%\%bin%\Logs\*Process.txt
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.exe.config
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.dll.config
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.xml

set Project=LJCTransformManager
set File=LJCTransformManager
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\TransformServiceTest.exe
del %Solution%\%Project%\%bin%\TransformServiceTest.exe.config
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.exe
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.dll.config
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.xml
del %Solution%\%Project%\%bin%\Logs /q

set Project=ModuleHost
set File=ModuleHost
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\Logs\ProcessGroup*.txt
del %Solution%\%Project%\%bin%\Logs\*Group.txt
del %Solution%\%Project%\%bin%\Logs\DataProcess*.txt
del %Solution%\%Project%\%bin%\Logs\*Process.txt
del %Solution%\%Project%\%bin%\TransformServiceTest.exe
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.exe
del %Solution%\%Project%\%bin%\LJCTransformManager.exe

set Project=TransformServiceTest
set File=TransformServiceTest
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\Logs /q
del %Solution%\%Project%\%bin%\AddressData /q
del %Solution%\%Project%\%bin%\Logs\ProcessGroup*.txt
del %Solution%\%Project%\%bin%\Logs\*Group.txt
del %Solution%\%Project%\%bin%\Logs\DataProcess*.txt
del %Solution%\%Project%\%bin%\Logs\*Process.txt
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.exe
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.exe.config
del %Solution%\%Project%\%bin%\LJCDataTransformProcess.xml
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.dll.config
del %Solution%\%Project%\%bin%\LJCDataTransformDAL.xml
del %Solution%\%Project%\%bin%\LJCDBMessage.xml
