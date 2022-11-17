rem LJCCodeDoc.cmd
set target=..\..\..\..\..\WebSitesDev\CodeDoc\LJCCodeDoc
LJCDocGen.exe %target% LJCCodeDoc.xml

set RunCommand=AfterGenRunOnce
set RunPath=HTML\LJCNetCommon
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceFormCommon
set RunPath=HTML\LJCWinFormCommon
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceDataReader
set RunPath=HTML\LJCTextDataReaderLib
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceFormCommon
set RunPath=HTML\LJCWinFormCommon
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt

set RunCommand=AfterGenRunOnceFormControls
set RunPath=HTML\LJCWinFormControls
rename %target%\%RunPath%\%RunCommand%.txt %RunCommand%.cmd
call %target%\%RunPath%\%RunCommand%.cmd
cd ..\..\..\..\..\LJCProjectsDev\LJCDocLib\LJCDocGen\bin\Debug
rename %target%\%RunPath%\%RunCommand%.cmd %RunCommand%.txt
