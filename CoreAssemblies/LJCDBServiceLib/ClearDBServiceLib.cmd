echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDBServiceLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBServiceLib
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCDBServiceLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=ConfigTestConsole
set File=ConfigTestConsole
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCDataAccessConfig.xml

set Project=LJCDataAccessConfig
set File=LJCDataAccessConfig
call %ClearBuild%

set Project=LJCDBServiceLib
set File=LJCDBServiceLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataAccessConfig.xml
del %Solution%\%Project%\%bin%\LJCDBDataAccessLib.xml

set Project=LJCDBDataAccessLib
set File=LJCDBDataAccessLib
call %ClearBuild%

set Project=TestDbDataAccess
set File=TestDbDataAccess
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDataAccessConfig.xml
del %Solution%\%Project%\%bin%\LJCDbDataAccessLib.xml
