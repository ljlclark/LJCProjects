echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem UpdateDocLib.cmd
set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCDocLib\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCGenText\LJCGenText\%bin%
copy %root%%src%\LJCGenTextLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%runRoot%LJCDocGen\%bin%

set src=LJCDocGroupEditor\LJCDocGroupEditor\%bin%
copy %root%%src%\LJCDocGroupEditor.exe %to%
set src=LJCDocGroupEditor\LJCDocGroupEditor\%bin%
copy %root%%src%\LJCDocGroupEditor.exe.config %to%

set src=LJCDocLib\LJCDocLibDAL\%bin%
copy %root%%src%\LJCDocLibDAL.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
rem copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
rem copy %root%%src%\CipherLib.dll %to%

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
