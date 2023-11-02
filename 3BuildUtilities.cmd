echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildUtilities.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem *************
rem CoreUtilities
rem *************
set /a counter+=1
set marker=----------------- %counter% - LJCBackupCommonLib ----------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCBackupCommonLib >> Build.txt
msbuild CoreUtilities\LJCBackupCommonLib\LJCBackupCommonLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCBackupCreateChanges ----------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCBackupCreateChanges >> Build.txt
rem ***call CoreUtilities\LJCBackupCreateChanges\UpdateCreateChanges.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCBackupCreateChanges\LJCCreateChanges.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCBackupChanges ----------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCBackupChanges >> Build.txt
rem ***call CoreUtilities\LJCBackupChanges\UpdateBackupChanges.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCBackupChanges\LJCBackupChanges.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCCodeLineCounter ----------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCCodeLineCounter >> Build.txt
call CoreUtilities\LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCAddressParserLib ---------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCAddressParserLib >> Build.txt
call CoreUtilities\LJCAddressParserLib\UpdateAddressParserLib.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCAddressParserLib\LJCAddressParserLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCExecuteScript ------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCExecuteScript >> Build.txt
call CoreUtilities\LJCExecuteScript\UpdateExecuteScript.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCExecuteScript\LJCExecuteScript.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCGenText ------------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCGenText >> Build.txt
call CoreUtilities\LJCGenText\UpdateGenText.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCGenText\LJCGenText.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCGenDoc ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCGenDoc >> Build.txt
call CoreUtilities\LJCGenDoc\UpdateGenDoc.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCGenDoc\LJCGenDoc.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCGenDocEdit ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDocLib >> Build.txt
call CoreUtilities\LJCGenDocEdit\UpdateGenDocEdit.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCGenDocEdit\LJCGenDocEdit.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCSQLUtilLib ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCSQLUtilLib >> Build.txt
call CoreUtilities\LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCViewBuilder ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCViewEditor >> Build.txt
call CoreUtilities\LJCViewBuilder\UpdateViewBuilder.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCViewBuilder\LJCViewBuilder.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCViewEditor ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCViewEditor >> Build.txt
call CoreUtilities\LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCViewEditor\LJCViewEditor.sln
