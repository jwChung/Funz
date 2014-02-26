@ECHO OFF
cd build
"%ProgramFiles(x86)%\MSBuild\12.0\bin\amd64\MSBuild.exe" Funz.proj /v:normal /maxcpucount /nodeReuse:false %*
