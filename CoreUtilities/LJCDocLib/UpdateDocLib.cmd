echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDocLib.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SetupUpdate.cmd %1%
call %mainRoot%SetupFolder.cmd
goto Process:
:BuildAll
call SetupUpdate.cmd %1%
set toRoot=%util%\LJCDocLib\
call SetupFolder.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCGenText\LJCGenText\%bin%
copy %utilRoot%%src%\LJCGenTextLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%toRoot%LJCDocGen\%bin%

set src=LJCDBMessage\CipherLib\%bin%
rem copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDocGroupEditor\LJCDocGroupEditor\%bin%
rem copy %utilRoot%%src%\LJCDocGroupEditor.exe %to%
rem copy %utilRoot%%src%\LJCDocGroupEditor.exe.config %to%

set src=LJCDocLib\LJCDocLibDAL\%bin%
copy %utilRoot%%src%\LJCDocLibDAL.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

set src=LJCLibraries\LJCWinFormCommon\%bin%
copy %assmRoot%%src%\LJCWinFormCommon.dll %to%

set src=LJCLibraries\LJCWinFormControls\%bin%
copy %assmRoot%%src%\LJCWinFormControls.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
