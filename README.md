# Indigo Functions
*Write better Azure Functions*

![runtime](https://img.shields.io/badge/Azure%20Functions-v2-orange.svg)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/daulet/Indigo.Functions)

This project aims at increasing usabiltiy of [Azure Functions](https://azure.microsoft.com/en-us/blog/introducing-azure-functions/) in real life applications with usage of custom input and output [bindings](https://github.com/Azure/azure-webjobs-sdk-extensions/wiki/Binding-Extensions-Overview). Azure Functions come with [built in support](https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings) for some triggers, inputs and outputs, mainly for Azure services like Cosmos DB, Azure Storage, Event Grid, Microsoft Graph etc. However, mature applications require more than just that: some sort of dependency injection for testability purposes; use of non-Azure services, like Redis; configurable parameters that are not hardcoded into the function. Custom input and output bindings provided by this project solve these problems in native Azure Functions way.

| Binding | Purpose | Sample | Nuget |
| ------- | ------- | ------ |:-----:|
| ```[Config("key")]``` | [Configuration](#configuration) via Application Settings | [ConfigurationFunction](sample/ConfigurationFunctionSample) | [![Nuget version](https://img.shields.io/nuget/dt/Indigo.Functions.Configuration.svg)](https://www.nuget.org/packages/Indigo.Functions.Configuration) |
| ```[Inject]``` | [Dependency Injection](#dependency-injection) with [Autofac](#autofac) | [AutofacFunction](sample/AutofacFunctionSample) | [![Nuget version](https://img.shields.io/nuget/dt/Indigo.Functions.Autofac.svg)](https://www.nuget.org/packages/Indigo.Functions.Autofac) |
| ```[Inject]``` | [Dependency Injection](#dependency-injection) with [built-in .NET Core container](#servicecollection) | [InjectionFunction](sample/InjectionFunctionSample) | [![Nuget version](https://img.shields.io/nuget/dt/Indigo.Functions.Injection.svg)](https://www.nuget.org/packages/Indigo.Functions.Injection) |
| ```[Inject]``` | [Dependency Injection](#dependency-injection) with [Unity](#unity) containers | [UnityFunction](sample/UnityFunctionSample) |  [![Nuget version](https://img.shields.io/nuget/dt/Indigo.Functions.Unity.svg)](https://www.nuget.org/packages/Indigo.Functions.Unity) |
| ```[Redis("key")]``` | [Redis](#redis) input and output with POCO support | [RedisFunction](sample/RedisFunctionSample) | [![Nuget version](https://img.shields.io/nuget/dt/Indigo.Functions.Redis.svg)](https://www.nuget.org/packages/Indigo.Functions.Redis) |

## Dependency Injection

Use [Inject] attribute to inject all your dependencies in Azure Function declaration.

```cs
[FunctionName("Example")]
public static IActionResult Run(
    [HttpTrigger("GET")] HttpRequest request,
    [Inject] IStorageAccess storageAccess)
{
    ...
}
```

 [Microsoft.Extensions.Configuration.IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration) instance is pre-registered for your convinience that you can use to read settings from [local settings file](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local#local-settings-file) or [application settings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings#settings) depending on whether you running locally or on Azure respectively. In addition, [Microsoft.Extensions.Logging.ILogger](https://github.com/Azure/azure-functions-host/wiki/ILogger) instance is also pre-registered for you to log to file system and App Insights. Just declare it as dependency in your implementation class anywhere in your dependency tree.

```cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 

public class ValueProvider
{
    public ValueProvider(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string ReadSetting(string settingName)
    {
        _logger.LogInformation($"Reading value of '{settingName}'");

        return _configuration[settingName];
    }

    ...
}
```

Supported IoC containers:

* [Autofac](#autofac)
* [.NET Core's ServiceCollection](#servicecollection)
* [Unity container](#unity)

### Autofac

[![Nuget version](https://img.shields.io/nuget/v/Indigo.Functions.Autofac.svg)](https://www.nuget.org/packages/Indigo.Functions.Autofac)
[![Nuget downloads](https://img.shields.io/nuget/dt/Indigo.Functions.Autofac.svg)](https://www.nuget.org/packages/Indigo.Functions.Autofac)

Create implementation of *IDependencyConfig* interface (public visibility) in your function's binary:

```cs
public class DependencyConfig : IDependencyConfig
{
    public void RegisterComponents(ContainerBuilder builder)
    {
        builder
            .RegisterType<StorageAccess>()
            .As<IStorageAccess>();
    }
}
```

For further details see [working sample](sample/AutofacFunctionSample) or [function declarations in tests](test/Indigo.Functions.Autofac.IntegrationTests.Target).

### ServiceCollection

[![Nuget version](https://img.shields.io/nuget/v/Indigo.Functions.Injection.svg)](https://www.nuget.org/packages/Indigo.Functions.Injection)
[![Nuget downloads](https://img.shields.io/nuget/dt/Indigo.Functions.Injection.svg)](https://www.nuget.org/packages/Indigo.Functions.Injection)

Register all your dependencies in Startup class:

```cs
[assembly: WebJobsStartup(typeof(InjectionFunctionSample.Startup))]
namespace InjectionFunctionSample
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<ICache, CacheProvider>();
            builder.Services.AddTransient<ICacheConfigProvider, CacheConfigProvider>();
            builder.Services.AddTransient<IStorageAccess, StorageAccess>();
            builder.Services.AddTransient<ITableAccess, CloudTableAccess>();
        }
    }
}
```

For further details see [working sample](sample/InjectionFunctionSample) or [function declarations in tests](test/Indigo.Functions.Injection.IntegrationTests.Target). For details on how to use ASP.NET Core's ServiceCollection see [official guide](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection).

### Unity

[![Nuget version](https://img.shields.io/nuget/v/Indigo.Functions.Unity.svg)](https://www.nuget.org/packages/Indigo.Functions.Unity)
[![Nuget downloads](https://img.shields.io/nuget/dt/Indigo.Functions.Unity.svg)](https://www.nuget.org/packages/Indigo.Functions.Unity)

Create implementation of *IDependencyConfig* interface (public visibility) in your function's binary:

```cs
public class DependencyInjectionConfig : IDependencyConfig
{
    public void RegisterComponents(UnityContainer container)
    {
        container.RegisterType<IStorageAccess, StorageAccess>();
    }
}
```

For further details see [working sample](sample/UnityFunctionSample) or [function declarations in tests](test/Indigo.Functions.Unity.IntegrationTests.Target).

### FAQ

* What if I need multiple containers for my application?

    *Azure Functions or any Function as a Service is a culmination of decades long effort towards reducing deployment, but more importatnly maintenance complexity by breaking down a monolith into applications to individual functions. So use it right, and separate your other function that needs a different container into a separate binary.*

## Configuration

[![Nuget version](https://img.shields.io/nuget/v/Indigo.Functions.Configuration.svg)](https://www.nuget.org/packages/Indigo.Functions.Configuration)
[![Nuget downloads](https://img.shields.io/nuget/dt/Indigo.Functions.Configuration.svg)](https://www.nuget.org/packages/Indigo.Functions.Configuration)

Some applications might have pre-production environments that require different set of parameters (settings) to be fed into your application, e.g. integration tests might have more aggressive timeouts or different integration URL for external service.

```cs
[FunctionName("ConfigFunctionExample")]
public static IActionResult Run(
    [HttpTrigger("GET")] HttpRequest request,
    [Config("StringSetting")] string stringValue,
    [Config("IntSetting")] int intValue,
    [Config("TimeSpanSetting")] TimeSpan timeSpanValue)
{
    ...
}
```

[Here](sample/ConfigurationFunctionSample) is a working sample. The binding supports [simple types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/built-in-types-table) and *string*. In addition, it supports structs like [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime), [DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset), [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid) and [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan). A full list of supported types can be found [in integration tests](test/Indigo.Functions.Configuration.IntegrationTests.Target/Function.cs).

## Redis

[![Nuget version](https://img.shields.io/nuget/v/Indigo.Functions.Redis.svg)](https://www.nuget.org/packages/Indigo.Functions.Redis)
[![NuGet downloads](https://img.shields.io/nuget/dt/Indigo.Functions.Redis.svg)](https://www.nuget.org/packages/Indigo.Functions.Redis)


*[Redis]* binding enables reading Redis strings:

```cs
[FunctionName("GetCachedString")]
public static IActionResult GetString(
    [HttpTrigger("GET", Route = "cache/{key}")] HttpRequest request,
    [Redis(Key = "{key}")] string cachedValue)
{
    return new OkObjectResult(cachedValue);
}
```

OR you can deserialize (JSON) string keys into custom objects:

```cs
[FunctionName("GetPoco")]
public static IActionResult GetPoco(
    [HttpTrigger("GET", Route = "poco/{key}")] HttpRequest request,
    [Redis(Key = "{key}")] CustomObject cachedValue)
{
    ...
}

public class CustomObject
{
    public int IntegerProperty { get; set; }

    public string StringProperty { get; set; }
}
```

And of course your can write back to Redis:

```cs
[FunctionName("SetPoco")]
public static async Task<IActionResult> SetPoco(
    [HttpTrigger("POST", Route = "poco/{key}")] HttpRequest request,
    [Redis(Key = "{key}")] IAsyncCollector<CustomObject> collector)
{
    string requestBody;
    using (var reader = new StreamReader(request.Body))
    {
        requestBody = reader.ReadToEnd();
        var value = JsonConvert.DeserializeObject<CustomObject>(requestBody);
        await collector.AddAsync(value);
    }
    return new OkObjectResult(requestBody);
}
```

To configure your Redis connection string set it in *RedisConfigurationOptions* setting. See [working sample](sample/RedisFunctionSample) or [integration tests](test/Indigo.Functions.Redis.IntegrationTests.Target) for full range of functionality.

## Real life examples

This project is a consequence of building [rehttp](https://github.com/daulet/rehttp) service using Azure Functions. I quickly came to realization that in order to build a reliable and maintainable service I was missing DI for unit testability, configurability for intergration testing and Redis POCO to keep my test code clean.
