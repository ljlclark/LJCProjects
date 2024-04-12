set solutionPath=..\..\CoreUtilities\UpdateProjectFiles\
copy %solutionPath%SoutionDependencies\bin\Debug\SolutionDependencies.exe
copy %solutionPath%SolutionDependencies.exe.config

copy %solutionPath%UpdateProjectFiles.exe.config
copy %solutionPath%DependenciesCopy.cmd

set solutionPath=..\..\CoreAssemblies\LJCNetCommon\
copy %solutionPath%LJCNetCommon\bin\Debug\LJCNetCommon.dll
Pause