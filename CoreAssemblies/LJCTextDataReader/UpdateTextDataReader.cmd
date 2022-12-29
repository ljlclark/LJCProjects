echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateTextDataReader.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\..\CoreAssemblies\
set runRoot=
set to=External
goto Update

:BuildAll
set root=CoreAssemblies\
set runRoot=CoreAssemblies\LJCTextDataReader\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%runRoot%LJCTextDataReader\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
