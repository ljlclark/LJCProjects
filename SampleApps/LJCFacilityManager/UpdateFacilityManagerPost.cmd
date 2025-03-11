echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateFacilityManagerPost.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%apps%\LJCFacilityManager\
call TargetFolders.cmd
:Process

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------
set to=%toRoot%ModuleHost\%bin%
echo.
echo *** %to% ***

set src=LJCFacilityManager\LJCFacilityManager\%bin%
copy %appsRoot%%src%\LJCFacilityManager.exe %to%
copy %appsRoot%%src%\LJCFacilityManager.exe.Config %to%
copy %appsRoot%%src%\LJCFacilityManagerDAL.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %appsRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %appsRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %appsRoot%%src%\LJCRegionManager.exe %to%

set src=LJCViewBuilder\LJCViewBuilder\%bin%
copy %utilRoot%%src%\LJCViewBuilder.exe %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
