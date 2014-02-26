@ECHO OFF
cd build
BuildFunz /v:normal /maxcpucount /nodeReuse:false /t:Publish /p:SetApiKey=%SetApiKey%;PrivateKey=%PrivateKey% /logger:"%MSBuildLoggerPath%";"%MSBuildLogs%\buld-log-%Appveyor_ProjectBuildNumber%.xml"
