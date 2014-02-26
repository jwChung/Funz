@ECHO OFF
RunMSBuild Funz.proj /v:normal /maxcpucount /nodeReuse:false /t:Verify
