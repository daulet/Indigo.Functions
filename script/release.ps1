param(
    [string]$libName,
    [string]$version
)

Write-Host "Running build" -ForegroundColor Blue

dotnet msbuild $PsScriptRoot\..\src\indigo.functions.$libName\Indigo.Functions.$libName.csproj '/consoleLoggerParameters:Summary;Verbosity=minimal' /m /t:Rebuild /nologo /p:TreatWarningsAsErrors=true /p:Configuration=Release

Write-Host "Publishing to Nuget" -ForegroundColor Blue

$apiKey = Get-Content "$PsScriptRoot\..\..\vault\keys\nuget\indigo.functions.apikey"
nuget push "$PsScriptRoot\..\src\indigo.functions.$libName\bin\Release\Indigo.Functions.$libName.$version.nupkg" $apiKey -source https://api.nuget.org/v3/index.json
