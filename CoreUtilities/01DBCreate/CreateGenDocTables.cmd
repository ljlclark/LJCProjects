echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem CreateLJCDocLib.cmd
echo.

set scriptsPath=..\LJCGenDoc\LJCGenDocDAL\bin\Debug\SQLScript
set targetDatabase=LJCData

LJCExecuteScripts %scriptsPath% %targetDatabase%

