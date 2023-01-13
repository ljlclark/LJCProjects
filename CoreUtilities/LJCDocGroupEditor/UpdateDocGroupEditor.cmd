echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDocGroupEditor.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SetupUpdate.cmd %1%
call %mainRoot%SetupFolder.cmd
goto Process:
:BuildAll
call SetupUpdate.cmd %1%
set toRoot=%util%\LJCDocGroupEditor\
call SetupFolder.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDocLib\LJCDocGenLib\%bin%
copy %utilRoot%%src%\LJCDocGenLib.dll %to%

set src=LJCDocLib\LJCDocLibDAL\%bin%
copy %utilRoot%%src%\LJCDocLibDAL.dll %to%

set src=LJCDocLib\LJCDocObjLib\%bin%
copy %utilRoot%%src%\LJCDocObjLib.dll %to%

set src=LJCDocLib\LJCDocXMLObjLib\%bin%
copy %utilRoot%%src%\LJCDocXMLObjLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

set src=LJCLibraries\LJCWinFormCommon\%bin%
copy %assmRoot%%src%\LJCWinFormCommon.dll %to%

set src=LJCLibraries\LJCWinFormControls\%bin%
copy %assmRoot%%src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCDocGroupEditor\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
