echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateLibraries.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SetupUpdate.cmd %1%
call %mainRoot%SetupFolder.cmd
goto Process:
:BuildAll
call SetupUpdate.cmd %1%
set toRoot=%assm%\LJCLibraries\
call SetupFolder.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCNetCommon\LJCNetCommon
copy %assmRoot%%src%\%bin%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------------
set to=%toRoot%LJCWinFormCommon\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
