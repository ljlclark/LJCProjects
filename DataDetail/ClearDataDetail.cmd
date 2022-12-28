echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDataDetail.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\DataDetail
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=DataDetail
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=DataDetail
set File=DataDetail
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /y
del %Solution%\%Project%\%bin%\LJCDataDetailDAL.xml
del %Solution%\%Project%\%bin%\LJCDataDetailLib.xml
del %Solution%\%Project%\%bin%\LJCTestDataLib.xml

set Project=LJCDataDetailConsole
set File=LJCDataDetailConsole
call %ClearBuild%
del %Solution%\%Project%\%bin%\DataDetail.exe
del %Solution%\%Project%\%bin%\LJCDataDetailConsole.xml
del %Solution%\%Project%\%bin%\DataDetail.xml
del %Solution%\%Project%\%bin%\LJCDataDetailDAL.xml
del %Solution%\%Project%\%bin%\LJCDataDetailLib.xml
del %Solution%\%Project%\%bin%\LJCTestDataLib.xml

set Project=LJCDataDetailDAL
set File=LJCDataDetailDAL
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataDetailDAL.xml

set Project=LJCDataDetailLib
set File=LJCDataDetailLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataDetailDAL.xml
del %Solution%\%Project%\%bin%\LJCDataDetailLib.xml

set Project=LJCTestDataLib
set File=LJCTestDataLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataDetailDAL.xml
del %Solution%\%Project%\%bin%\LJCDataDetailLib.xml
del %Solution%\%Project%\%bin%\LJCTestDataLib.xml
