@ECHO OFF
cd build
Verify /t:Publish /p:SetApiKey=%SetApiKey%;PrivateKey=%PrivateKey% /logger:"%MSBuildLoggerPath%";"%MSBuildLogs%\buld-log-%Appveyor_ProjectBuildNumber%.xml"
