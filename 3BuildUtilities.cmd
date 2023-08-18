echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildUtilities.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem *************
rem CoreUtilities
rem *************
set /a counter=1
echo on
echo ----------------- %counter% - LJCBackupCommonLib ---------- > Build.txt
echo LJCBackupCommonLib >> Build.txt
msbuild CoreUtilities\LJCBackupCommonLib\LJCBackupCommonLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCBackupCreateChanges ---------- >> Build.txt
echo LJCBackupCreateChanges >> Build.txt
rem ***call CoreUtilities\LJCBackupCreateChanges\UpdateCreateChanges.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCBackupCreateChanges\LJCCreateChanges.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCBackupChanges ---------- >> Build.txt
echo LJCBackupChanges >> Build.txt
rem ***call CoreUtilities\LJCBackupChanges\UpdateBackupChanges.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCBackupChanges\LJCBackupChanges.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCCodeLineCounter ---------- >> Build.txt
echo LJCCodeLineCounter >> Build.txt
call CoreUtilities\LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCAddressParserLib --------- >> Build.txt
echo LJCAddressParserLib >> Build.txt
call CoreUtilities\LJCAddressParserLib\UpdateAddressParserLib.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCAddressParserLib\LJCAddressParserLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCExecuteScript ------------ >> Build.txt
echo LJCExecuteScript >> Build.txt
call CoreUtilities\LJCExecuteScript\UpdateExecuteScript.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCExecuteScript\LJCExecuteScript.sln

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
echo ----------------- %counter% - LJCGenDocEdit.sln --------------- >> Build.txt
echo LJCDocLib >> Build.txt
call CoreUtilities\LJCGenDocEdit\UpdateGenDocEdit.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCGenDocEdit\LJCGenDocEdit.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCSQLUtilLib --------------- >> Build.txt
echo LJCSQLUtilLib >> Build.txt
call CoreUtilities\LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCViewBuilder --------------- >> Build.txt
echo LJCViewEditor >> Build.txt
call CoreUtilities\LJCViewBuilder\UpdateViewBuilder.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCViewBuilder\LJCViewBuilder.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCViewEditor --------------- >> Build.txt
echo LJCViewEditor >> Build.txt
call CoreUtilities\LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Build.txt
msbuild CoreUtilities\LJCViewEditor\LJCViewEditor.sln
