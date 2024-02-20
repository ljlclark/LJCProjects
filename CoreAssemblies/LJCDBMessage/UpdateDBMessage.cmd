echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBMessage.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBMessage\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem ***set src=LJCTextDataReader\LJCTextDataReaderLib\%bin%
rem ***copy %assmRoot%%src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------
set to=%toRoot%LJCDBMessage\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
