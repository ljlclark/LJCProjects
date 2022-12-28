echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDBServiceHosts.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBServiceHosts
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDBServiceHosts
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBServiceConsoleHost
set File=LJCDBServiceConsoleHost
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml

set Project=LJCDBServiceHost
set File=LJCDBServiceHost
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml

set Project=TestDBDataAccess
set FileName=TestDBDataAccess
call %ClearBuild%
