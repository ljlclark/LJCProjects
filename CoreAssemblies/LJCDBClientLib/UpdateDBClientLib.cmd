echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBClientLib.cmd

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
set toRoot=%assm%\LJCDBClientLib\
set to=%toRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %assmRoot%%src%\LJCGridDataLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

set src=LJCTextDataReader\LJCTextDataReaderLib\%bin%
copy %assmRoot%%src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%TestObjectManager\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %assmRoot%%src%\DataConfigs.xml %to%
copy %assmRoot%%src%\ConnectionTemplates.xml %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

copy %assmRoot%MySql.Data.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
