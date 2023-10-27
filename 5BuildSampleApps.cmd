echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildSampleApps.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem **********
rem SampleApps
rem **********
set /a counter+=1
set marker=----------------- %counter% - LJCRegionManager ------------ > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCRegionManager >> Build.txt
call SampleApps\LJCRegionManager\UpdateRegionManager.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCRegionManager\LJCRegionManager.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCUnitMeasure -------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCUnitMeasure >> Build.txt
call SampleApps\LJCUnitMeasure\UpdateUnitMeasure.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCUnitMeasure\LJCUnitMeasure.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - CVRManager ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo CVRManager >> Build.txt
call SampleApps\CVRManager\UpdateCVRManager.cmd BuildAll >> Build.txt
msbuild SampleApps\CVRManager\CVRManager.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCFacilityManager ---------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCFacilityManager >> Build.txt
call SampleApps\LJCFacilityManager\UpdateFacilityManager.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCFacilityManager\LJCFacilityManager.sln
call SampleApps\LJCFacilityManager\UpdateFacilityManagerPost.cmd BuildAll >> Build.txt

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDocAppManager ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDocAppManager >> Build.txt
call SampleApps\LJCDocAppManager\UpdateDocAppManager.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCDocAppManager\LJCDocAppManager.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCTextInvasion ------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCTextInvasion >> Build.txt
call SampleApps\LJCTextInvasion\UpdateTextInvasion.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCTextInvasion\LJCTextInvasion.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDocGroupEditor.sln ------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDocGroupEditor >> Build.txt
call SampleApps\LJCDocGroupEditor\UpdateDocGroupEditor.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCDocGroupEditor\LJCDocGroupEditor.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDataTransform ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataTransform >> Build.txt
call SampleApps\LJCDataTransform\UpdateDataTransform.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCDataTransform\LJCDataTransform.sln
call SampleApps\LJCDataTransform\UpdateDataTransformPost.cmd BuildAll >> Build.txt

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCAppManager --------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCAppManager >> Build.txt
call SampleApps\LJCAppManager\UpdateAppManager.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCAppManager\LJCAppManager.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCFacilityManagerSetup ----- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCFacilityManagerSetup >> Build.txt
call SampleApps\LJCFacilityManagerSetup\UpdateFacilityManagerSetup.cmd BuildAll >> Build.txt
call SampleApps\LJCFacilityManagerSetup\UpdateFacilityManagerResources.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCFacilityManagerSetup\LJCFacilityManagerSetup.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - Genealogy ------------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo Genealogy >> Build.txt
call SampleApps\Genealogy\UpdateGenealogy.cmd BuildAll >> Build.txt
msbuild SampleApps\Genealogy\Genealogy.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCSystemBuilder ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCSystemBuilder >> Build.txt
call SampleApps\LJCSystemBuilder\UpdateSystemBuilder.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCSystemBuilder\LJCSystemBuilder.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCSales ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCSales >> Build.txt
call SampleApps\LJCSales\UpdateSalesManager.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCSales\LJCSales.sln
