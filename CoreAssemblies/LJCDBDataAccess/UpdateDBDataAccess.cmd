echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBDataAccess.cmd

set bin=bin\Debug
set assm=CoreAssemblies\
set util=CoreUtilities\
set app=SampleApps\
if %1%. == BuildAll. goto BuildAll

rem Run from Solution folder.
set assmRoot=..\..\%assm%
set utilRoot=..\..\%util%
set appRoot=..\..\%app%
set toRoot=
set to=External
goto Update

:BuildAll
rem Run from main Projects folder.
set assmRoot=%assm%
set utilRoot=%util%
set appRoot=%app%
set toRoot=%assmRoot%LJCDBDataAccess\
set to=%toRoot%External

:Update
if exist %to%\NUL goto continue
mkdir %to%
:continue

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%toRoot%LJCDBDataAccess\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
