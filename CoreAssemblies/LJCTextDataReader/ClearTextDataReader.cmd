echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearTextDataReader.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCTextDataReader
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCTextDataReader
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCTextDataReaderLib
set File=LJCTextDataReaderLib
call %ClearBuild%

set Project=LJCTestConsole
set File=LJCTestConsole
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCTextDataReaderLib.xml
