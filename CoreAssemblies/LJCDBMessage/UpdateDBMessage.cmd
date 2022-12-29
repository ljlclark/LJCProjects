echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBMessage.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\..\CoreAssemblies\
set runRoot=
set to=External
goto Update

:BuildAll
set root=CoreAssemblies\
set runRoot=CoreAssemblies\LJCDBMessage\
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
