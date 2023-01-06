echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildAll.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem ***************
rem Core Assemblies
rem ***************
set /a counter=1
echo on
echo ----------------- %counter% - LJCNetCommon --------------- > Build.txt
echo LJC.Net.Common >> Build.txt
msbuild CoreAssemblies\LJCNetCommon\LJCNetCommon.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCTextDataReader ----------- >> Build.txt
echo LJCTextDataReader >> Build.txt
call CoreAssemblies\LJCTextDataReader\UpdateTextDataReader.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCTextDataReader\LJCTextDataReader.sln

set /a counter+=1
echo on
echo ----------------- %counter% - LJCDataAccessConfig --------- >> Build.txt
echo LJCDataAccessConfig >> Build.txt
call CoreAssemblies\LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDataAccess---------------- >> Build.txt
echo LJCDataAccess >> Build.txt
call CoreAssemblies\LJCDataAccess\UpdateDataAccess.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDataAccess\LJCDataAccess.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBMessage ---------------- >> Build.txt
echo LJCDBMessage >> Build.txt
call CoreAssemblies\LJCDBMessage\UpdateDBMessage.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBMessage\LJCDBMessage.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBDataAccess ------------- >> Build.txt
echo LJCDBDataAccess >> Build.txt
call CoreAssemblies\LJCDBDataAccess\UpdateDBDataAccess.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBDataAccess\LJCDBDataAccess.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBServiceLib ------------- >> Build.txt
echo LJCDBServiceLib >> Build.txt
call CoreAssemblies\LJCDBServiceLib\UpdateDBServiceLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBServiceLib\LJCDBServiceLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCLibraries ---------------- >> Build.txt
echo LJCLibraries >> Build.txt
call CoreAssemblies\LJCLibraries\UpdateLibraries.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCLibraries\LJCLibraries.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBClientLib -------------- >> Build.txt
echo LJCDBClientLib >> Build.txt
call CoreAssemblies\LJCDBClientLib\UpdateDBClientLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBClientLib\LJCDBClientLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCGridDataLib --------------- >> Build.txt
echo LJCGridDataLib >> Build.txt
call CoreAssemblies\LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCGridDataLib\LJCGridDataLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBServiceHosts ----------- >> Build.txt
echo LJCDBServiceHosts >> Build.txt
call CoreAssemblies\LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBServiceHosts\LJCDBServiceHosts.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBViewDAL ---------------- >> Build.txt
echo LJCDBViewDAL >> Build.txt
call CoreAssemblies\LJCDBViewDAL\UpdateDBViewDAL.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBViewDAL\LJCDBViewDAL.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - DataDetail ------------------ >> Build.txt
echo DataDetail >> Build.txt
call CoreAssemblies\DataDetail\UpdateDataDetail.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\DataDetail\DataDetail.sln

rem *************
rem CoreUtilities
rem *************
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCCodeLineCounter ---------- >> Build.txt
echo LJCCodeLineCounter >> Build.txt
call CoreUtilities\LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCGenText ------------------ >> Build.txt
echo LJCGenText >> Build.txt
call CoreUtilities\LJCGenText\UpdateGenText.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCGenText\LJCGenText.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDocLib.sln --------------- >> Build.txt
echo LJCDocLib >> Build.txt
call CoreUtilities\LJCDocLib\UpdateDocLib.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCDocLib\LJCDocLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCSQLUtilLib --------------- >> Build.txt
echo LJCSQLUtilLib >> Build.txt
call CoreUtilities\LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCViewEditor --------------- >> Build.txt
echo LJCViewEditor >> Build.txt
call CoreUtilities\LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCViewEditor\LJCViewEditor.sln

rem **********
rem SampleApps
rem **********
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCRegionManager ------------ >> Build.txt
echo LJCRegionManager >> Build.txt
call SampleApps\LJCRegionManager\UpdateRegionManager.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCRegionManager\LJCRegionManager.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCUnitMeasure -------------- >> Build.txt
echo LJCUnitMeasure >> Build.txt
call SampleApps\LJCUnitMeasure\UpdateUnitMeasure.cmd BuildAll >> Build.txt
msbuild SampleApps\LJCUnitMeasure\LJCUnitMeasure.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - CVRManager ------------ >> Build.txt
echo CVRManager >> Build.txt
call SampleApps\CVRManager\UpdateCVRManager.cmd BuildAll >> Build.txt
msbuild SampleApps\CVRManager\CVRManager.sln
