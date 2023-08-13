echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearNetCommonTest.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCNetCommonTest
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreTestApps\LJCNetCommonTest
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCNetCommonTest
set File=LJCNetCommonTest
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJC.Net.Common.xml
