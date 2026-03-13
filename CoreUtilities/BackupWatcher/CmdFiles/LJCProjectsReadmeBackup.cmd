echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem LJCProjectsReadMeBackup.cmd
echo:

call ..\Common\LJCProjectsDevVars.cmd

echo -----
set targetPath=%mainPath%\LJCProjects"

echo copy %sourcePath%
echo ReadMeLJCProjects.html
echo %targetPath%
copy %sourcePath%\ReadMeLJCProjects.html %targetPath%
echo:

rem LJCProjects
echo copy %sourcePath%
echo index.html
echo %targetPath%
copy %sourcePath%\index.html %targetPath%
echo:

rem LJCProjects\CSS
set baseSource=%sourcePath%\CSS
set baseTarget=%targetPath%\CSS
echo copy %baseSource%
echo CodeDoc.css
echo %baseTarget%
copy %baseSource%\CodeDoc.css %baseTarget%

rem LJCProjects\CoreUtilities
set baseSource=%sourcePath%\CoreUtilities
set baseTarget=%targetPath%\CoreUtilities
echo copy %baseSource%
echo ReadMeCoreUtilities.html
echo %baseTarget%
copy %baseSource%\ReadMeCoreUtilities.html %baseTarget%
echo:

rem LJCProjects\CoreUtilities\LJCGenDoc
set baseSource=%baseSource%\LJCGenDoc
set baseTarget=%baseTarget%\LJCGenDoc
echo copy %baseSource%
echo ReadMeLJCGenDoc.html
echo %baseTarget%
copy %baseSource%\ReadMeLJCGenDoc.html %baseTarget%
echo:

rem LJCProjects\CoreUtilities\LJCDocObjLib
set source=%baseSource%\LJCDocObjLib
set target=%baseTarget%\LJCDocObjLib
echo copy %source%
echo ReadMeDocObjLib.html
echo %target%
copy %source%\ReadMeDocObjLib.html %target%
echo:

rem LJCProjects\CoreUtilities\LJCDocXMLObjLib
set source=%baseSource%\LJCDocXMLObjLib
set target=%baseTarget%\LJCDocXMLObjLib
echo copy %source%
echo ReadMeDocXMLObjLib.html
echo %target%
copy %source%\ReadMeDocXMLObjLib.html %target%
echo:

rem LJCProjects\CoreUtilities\LJCGenDoc
set source=%baseSource%\LJCGenDoc
set target=%baseTarget%\LJCGenDoc
echo copy %source%
echo ReadMeGenDoc.html
echo %target%
copy %source%\ReadMeGenDoc.html %target%
echo:

rem LJCProjects\CoreUtilities\LJCGenDocDAL
set source=%baseSource%\LJCGenDocDAL
set target=%baseTarget%\LJCGenDocDAL
echo copy %source%
echo ReadMeGenDocDAL.html
echo %target%
copy %source%\ReadMeGenDocDAL.html %target%
echo:

rem LJCProjects\CoreUtilities\LJCGenDocLib
set source=%baseSource%\LJCGenDocLib
set target=%baseTarget%\LJCGenDocLib
echo copy %source%
echo ReadMeGenDocLib.html
echo %target%
copy %source%\ReadMeGenDocLib.html %target%
echo:
