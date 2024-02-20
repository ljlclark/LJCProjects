echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateLibraries.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCLibraries\
call TargetFolders.cmd
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
