del DataFiles\*.* /q
del ManagerFiles\*.* /q
del XMLFiles\*.* /q
set dataConfigName=LJCData
LJCGenTableCode %dataConfigName%
