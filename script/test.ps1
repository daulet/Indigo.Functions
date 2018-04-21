function Start-AzureFunction ([int]$port, [string]$workingDir) {
	Start-Process func -ArgumentList "host start --port $port" -WorkingDirectory $workingDir -PassThru
}

Start-AzureFunction 7072 -workingDir "test\Indigo.Functions.Injection.IntegrationTests.ConfiguredFunction\bin\Debug\netstandard2.0"
Start-AzureFunction 7073 -workingDir "test\Indigo.Functions.Injection.IntegrationTests.NonPublicFunction\bin\Debug\netstandard2.0"

dotnet test test\Indigo.Functions.Injection.IntegrationTests.InjectionTests\Indigo.Functions.Injection.IntegrationTests.InjectionTests.csproj

Stop-Process (Get-Process func).Id