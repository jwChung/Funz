@ECHO OFF
cd build
RunMSBuild Funz.proj /v:normal /maxcpucount /nodeReuse:false /t:Verify
