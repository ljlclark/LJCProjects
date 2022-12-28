echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearNetCommon.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCNetCommon
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreAssemblies\LJCNetCommon
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCNetCommon
set File=LJCNetCommon
call %ClearBuild%

:Clear
set Project=NetCommonTest
set File=NetCommonTest
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCNetCommon.xml
