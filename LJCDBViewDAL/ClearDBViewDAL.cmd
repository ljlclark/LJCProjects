echo Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBViewDAL
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCDBViewDAL
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=ConsoleApp2
set File=ConsoleApp2
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCDBViewDAL.xml

set Project=LJCDBViewDAL
set File=LJCDBViewDAL
call %ClearBuild%
