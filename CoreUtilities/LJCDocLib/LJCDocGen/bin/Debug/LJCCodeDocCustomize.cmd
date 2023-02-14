rem LJCCodeDocCustomize.cmd

set target=..\..\..\..\..\..\WebSitesDev\CodeDoc\LJCCodeDoc

set RunCommand=AfterGenRunOnceDataAccess
set RunPath=HTML\LJCDataAccess
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceDBClient
set RunPath=HTML\LJCDBClientLib
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceDBDataAccess
set RunPath=HTML\LJCDBDataAccess
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceDBMessage
set RunPath=HTML\LJCDBMessage
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceDBService
set RunPath=HTML\LJCDBServiceLib
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceGridData
set RunPath=HTML\LJCGridDataLib
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnce
set RunPath=HTML\LJCNetCommon
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceDataReader
set RunPath=HTML\LJCTextDataReaderLib
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceFormCommon
set RunPath=HTML\LJCWinFormCommon
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceFormControls
set RunPath=HTML\LJCWinFormControls
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\CoreUtilities\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt
