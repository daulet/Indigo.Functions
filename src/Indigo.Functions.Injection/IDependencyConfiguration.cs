using Microsoft.Extensions.DependencyInjection;

namespace Indigo.Functions.Injection
{
    public interface IDependencyConfiguration
    {
        void RegisterServices(ServiceCollection collection);
    }
}
