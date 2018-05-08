function Start-AzureFunction ([int]$port, [string]$workingDir) {
	Start-Process func -ArgumentList "host start --port $port" -WorkingDirectory $workingDir -PassThru
}

####################
# Test Configuration
####################

Start-AzureFunction 7072 -workingDir "test\Indigo.Functions.Configuration.IntegrationTests.Target\bin\Debug\netstandard2.0"
dotnet test test\Indigo.Functions.Configuration.IntegrationTests\Indigo.Functions.Configuration.IntegrationTests.csproj

if ($LastExitCode -ne 0) {
    return $LastExitCode
}

# Kill processes only if previous actions succeeded, in case you need to debug
Stop-Process (Get-Process func).Id

################
# Test Injection
################

Start-AzureFunction 7073 -workingDir "test\Indigo.Functions.Injection.IntegrationTests.CorrectConfig\bin\Debug\netstandard2.0"
Start-AzureFunction 7074 -workingDir "test\Indigo.Functions.Injection.IntegrationTests.NonPublicConfig\bin\Debug\netstandard2.0"
dotnet test test\Indigo.Functions.Injection.IntegrationTests.InjectionTests\Indigo.Functions.Injection.IntegrationTests.InjectionTests.csproj

if ($LastExitCode -ne 0) {
    return $LastExitCode
}

# Kill processes only if previous actions succeeded, in case you need to debug
Stop-Process (Get-Process func).Id

############
# Test Redis
############

Start-AzureFunction 7075 -workingDir "test\Indigo.Functions.Redis.IntegrationTests.Target\bin\Debug\netstandard2.0"
Start-Process redis-server -PassThru
dotnet test test\Indigo.Functions.Redis.IntegrationTests\Indigo.Functions.Redis.IntegrationTests.csproj --no-build

if ($LastExitCode -ne 0) {
    return $LastExitCode
}

# Kill processes only if previous actions succeeded, in case you need to debug
Stop-Process (Get-Process func).Id
