echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDataAccess.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDataAccess\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCNetCommon\LJCNetCommon
copy %assmRoot%%src%\%bin%\LJCNetCommon.dll %to%

copy %mainRoot%MySql.Data.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%toRoot%LJCDataAccess\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
