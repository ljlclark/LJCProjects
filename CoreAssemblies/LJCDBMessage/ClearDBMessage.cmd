echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDBMessage.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBMessage
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCDBMessage
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBMessage
set File=LJCDBMessage
call %ClearBuild%

set Project=CipherLib
set File=CipherLib
call %ClearBuild%

set Project=ConsoleApp1
set File=ConsoleApp1
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDBMessage.xml

