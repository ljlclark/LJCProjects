echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBClientSQLLib.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBClientSQLLib\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

rem set src=LJCDBMessage\CipherLib\%bin%
rem copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

rem set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
rem copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

rem set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
rem copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %assmRoot%%src%\LJCGridDataLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem set src=LJCTextDataReader\LJCTextDataReaderLib\%bin%
rem copy %assmRoot%%src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCDBClientSQLLib\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
