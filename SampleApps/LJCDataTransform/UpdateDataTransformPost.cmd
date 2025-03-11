echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDataTransformPost.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%apps%\LJCDataTransform\
call TargetFolders.cmd
:Process

rem *****************************
rem *** Runtime-only Binaries ***

rem --------------------------------
set to=%toRoot%ModuleHost\%bin%
echo.
echo *** %to% ***

set src=LJCAddressParserLib\LJCAddressParserLib
copy %utilRoot%%src%\%bin%\LJCAddressParserLib.dll %to%

rem --- LJCDataTransform
set src=LJCDataTransform\LJCCommonModuleLib
copy %appsRoot%%src%\%bin%\LJCCommonModuleLib.dll %to%

set src=LJCDataTransform\LJCTransformManager
copy %appsRoot%%src%\%bin%\LJCTransformManager.exe %to%
copy %appsRoot%%src%\%bin%\LJCTransformManager.exe.Config %to%
copy %appsRoot%%src%\%bin%\LJCDataTransformDAL.dll %to%

set src=LJCDataTransform\LJCDataTransformProcess
copy %appsRoot%%src%\%bin%\LJCDataTransformProcess.exe %to%
copy %appsRoot%%src%\%bin%\LJCDataTransformProcess.exe.config %to%

set src=LJCDataTransform\TransformServiceTest
copy %appsRoot%%src%\%bin%\TransformServiceTest.exe %to%
copy %appsRoot%%src%\%bin%\TransformServiceTest.exe.config %to%
rem ---

set src=LJCTextDataReader\LJCTextDataReaderLib
copy %assmRoot%%src%\%bin%\LJCTextDataReaderLib.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
