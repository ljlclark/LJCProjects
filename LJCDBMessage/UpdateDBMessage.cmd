echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem UpdateDBMessage.cmd
set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCDBMessage\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

set src=LJCTextDataReader\LJCTextDataReaderLib\%bin%
copy %root%%src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------
set to=%runRoot%LJCDBMessage\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
