echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearAddressParserLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCAddressParserLib
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCAddressParserLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCAddressParserLib
set File=LJCAddressParserLib
call %ClearBuild%
