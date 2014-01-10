@ECHO OFF
set MsBuildPath="%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\msbuild.exe" (
set MsBuildPath="C:\Program Files (x86)\MSBuild\12.0\Bin\msbuild.exe"
)
if exist "C:\Program Files\MSBuild\12.0\Bin\msbuild.exe" (
set MsBuildPath="C:\Program Files\MSBuild\12.0\Bin\msbuild.exe"
)

%MsBuildPath% Funz.proj /v:normal /maxcpucount /nodeReuse:false /t:Verify %*
PAUSE