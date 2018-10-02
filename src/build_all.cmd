@echo off
setlocal enabledelayedexpansion

REM The list of projects to build. This can be a solution, .csproj, or a folder containing either. These are executed in order
set buildFiles= "IO/Threaded" "MarkSFrancis" "Benchmark" "Collections" "Console" "Core/Configuration" "Data/Data" "Data/EFCore" "Drawing" "IO/IO" "IO/Json" "Random" "Reflection" "Security" "Serialization" "Web" "Windows/Windows" "Windows/Configuration"

REM The build status of the most recently completed build
set buildStatus=0

echo Starting builds...
for %%a in (%buildFiles%) do (
	echo Building %%a...
	
	call :build %%a
	
	REM Exit this process if the last build was not successful
	if !buildStatus! GEQ 1 goto :quit
)

echo Finished builds
goto :quit

:build
dotnet restore %~1
set buildStatus=%ERRORLEVEL%

REM Cancel the build if the restore is faulted
if %buildStatus% GEQ 1 goto :EOF

dotnet build %~1
set buildStatus=%ERRORLEVEL%
goto :EOF

:quit
REM Use the exit code from the last build status as the exit code for this process
exit /b %buildStatus%