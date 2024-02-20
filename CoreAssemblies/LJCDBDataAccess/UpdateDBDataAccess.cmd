echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBDataAccess.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBDataAccess\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%toRoot%LJCDBDataAccess\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
