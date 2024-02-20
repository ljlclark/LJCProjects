echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateTextDataReader.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCTextDataReader\
call TrgetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCTextDataReader\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
