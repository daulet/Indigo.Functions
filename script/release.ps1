param(
    [string]$rootPath,
    [string]$version
)

Write-Host "Running build" -ForegroundColor Blue

dotnet msbuild $rootPath\src\indigo.functions.redis\Indigo.Functions.Redis.csproj '/consoleLoggerParameters:Summary;Verbosity=minimal' /m /t:Rebuild /nologo /p:TreatWarningsAsErrors=true /p:Configuration=Release

Write-Host "Publishing to Nuget" -ForegroundColor Blue

$apiKey = Get-Content "$rootPath\..\vault\keys\nuget\indigo.functions.apikey"
nuget push "$rootPath\src\indigo.functions.redis\bin\Release\Indigo.Functions.Redis.$version.nupkg" $apiKey -source https://api.nuget.org/v3/index.json
