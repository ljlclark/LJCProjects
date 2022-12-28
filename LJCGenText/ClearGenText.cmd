echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearGenText.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCGenText
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCGenText
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCGenTableCode
set File=LJCGenTableCode
call %ClearBuild%
call ClearGenTextData.cmd %1%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
rem del %Solution%\LJC.FacilityManager\Output\*.* /q
del %Solution%\%Project%\%bin%\LJCGenTableDAL.xml
del %Solution%\%Project%\%bin%\LJCGenTextLib.xml

set Project=LJCGenText
set File=LJCGenText
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCGenTextLib.xml

set Project=LJCGenTextEdit
set File=LJCGenTextEdit
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\LJCGenTextLib.xml

set Project=LJCGenTextLib
set File=LJCGenTextLib
call %ClearBuild%
