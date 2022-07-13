echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem UpdateGridDataLib.cmd
set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCGridDataLib\
set to=%runRoot%\External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCLibraries\Output
copy %root%%src%\LJCWinFormControls.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem --------------------------------
set to=%runRoot%LJCGridDataLib\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
