echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBServiceLib.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SetupUpdate.cmd %1%
call %mainRoot%SetupFolder.cmd
goto Process:
:BuildAll
call SetupUpdate.cmd %1%
set toRoot=%assm%\LJCDBServiceLib\
call SetupFolder.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCDBServiceLib\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
