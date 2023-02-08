echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDBClientSQLLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDBClientSQLLib
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCDBClientSQLLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCDBClientSQLLib
set File=LJCSQLManagerLib
call %ClearBuild%
