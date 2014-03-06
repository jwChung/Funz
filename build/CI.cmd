@ECHO OFF
cd build
Verify.cmd /t:Publish /p:SetApiKey=%SetApiKey%;PrivateKey=%PrivateKey%;GitHubAccount=%GitHubAccount% /logger:"%MSBuildLoggerPath%";"%MSBuildLogs%\buld-log-%Appveyor_ProjectBuildNumber%.xml"
