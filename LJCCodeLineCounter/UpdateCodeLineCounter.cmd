echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem UpdateCodeLineCounter.cmd
set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCCodeLineCounter\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------------
set to=%runRoot%LJCCodeLineCounter\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
