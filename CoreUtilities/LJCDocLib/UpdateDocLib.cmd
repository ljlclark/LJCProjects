echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDocLib.cmd

echo Licensed under the MIT License.
rem UpdateDocLib.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\..\CoreAssemblies\
set altRoot=..\..\CoreUtilities\
set runRoot=
set to=External
goto Update

:BuildAll
set root=CoreAssemblies\
set altRoot=CoreUtilities\
set runRoot=CoreUtilities\LJCDocLib\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCGenText\LJCGenText\%bin%
copy %altRoot%%src%\LJCGenTextLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%runRoot%LJCDocGen\%bin%

set src=LJCDBMessage\CipherLib\%bin%
rem copy %root%%src%\CipherLib.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCDocGroupEditor\LJCDocGroupEditor\%bin%
rem copy %altRoot%%src%\LJCDocGroupEditor.exe %to%
rem copy %altRoot%%src%\LJCDocGroupEditor.exe.config %to%

set src=LJCDocLib\LJCDocLibDAL\%bin%
copy %altRoot%%src%\LJCDocLibDAL.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

set src=LJCLibraries\LJCWinFormCommon\%bin%
copy %root%%src%\LJCWinFormCommon.dll %to%

set src=LJCLibraries\LJCWinFormControls\%bin%
copy %root%%src%\LJCWinFormControls.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
