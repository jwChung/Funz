SET ToolsVersions=12.0
SET RegPath=HKLM\SOFTWARE\Microsoft\MSBuild\ToolsVersions\%ToolsVersions%
reg.exe query "%RegPath%" /v MSBuildToolsPath > nul 2>&1
if ERRORLEVEL 1 goto MissingMSBuildRegistry
for /f "skip=2 tokens=2,*" %%A in ('reg.exe query "%RegPath%" /v MSBuildToolsPath') do SET MSBUILDDIR=%%B
IF NOT EXIST %MSBUILDDIR%nul goto MissingMSBuildToolsPath
IF NOT EXIST %MSBUILDDIR%msbuild.exe goto MissingMSBuildExe
"%MSBUILDDIR%msbuild.exe" %*
goto:eof

:MissingMSBuildRegistry
echo Cannot obtain path to MSBuild tools from registry
goto:eof

:MissingMSBuildToolsPath
echo The MSBuild tools path from the registry '%MSBUILDDIR%' does not exist
goto:eof

:MissingMSBuildExe
echo The MSBuild executable could not be found at '%MSBUILDDIR%'
goto:eof
