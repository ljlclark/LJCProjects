echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateGenDocEdit.cmd

echo.
if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\LJCGenDocEdit\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %src%\LJCDBClientLib.dll %to%
copy %src%\LJCDBClientLib.dll %to%

set src=%assmRoot%LJCDBMessage\LJCDBMessage\%bin%
echo copy %src%\LJCDBMessage.dll %to%
copy %src%\LJCDBMessage.dll %to%

set src=%utilRoot%LJCDocLib\LJCDocLibDAL\%bin%
echo copy %src%\LJCDocLibDAL.dll %to%
copy %src%\LJCDocLibDAL.dll %to%

set src=%utilRoot%LJCDocLib\LJCDocObjLib\%bin%
echo copy %src%\LJCDocObjLib.dll %to%
copy %src%\LJCDocObjLib.dll %to%

set src=%utilRoot%LJCDocLib\LJCDocXMLObjLib\%bin%
echo copy %src%\LJCDocXMLObjLib.dll %to%
copy %src%\LJCDocXMLObjLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormCommon\%bin%
echo copy %src%\LJCWinFormCommon.dll %to%
copy %src%\LJCWinFormCommon.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormControls\%bin%
echo copy %src%\LJCWinFormControls.dll %to%
copy %src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCGenDocEdit\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %src%\LJCDBClientLib.dll %to%
copy %src%\LJCDBClientLib.dll %to%

set src=%assmRoot%LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %src%\LJCDBDataAccess.dll %to%
copy %src%\LJCDBDataAccess.dll %to%

set src=%assmRoot%LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %src%\LJCDBServiceLib.dll %to%
copy %src%\LJCDBServiceLib.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
