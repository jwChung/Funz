@ECHO OFF
cd build
Verify.cmd /t:Publish /p:SetApiKey=%SetApiKey%;PrivateKey=%PrivateKey%;GitHubAccount=%GitHubAccount%
