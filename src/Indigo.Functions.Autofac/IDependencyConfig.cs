using Autofac;

namespace Indigo.Functions.Autofac
{
    public interface IDependencyConfig
    {
        void RegisterComponents(ContainerBuilder builder);
    }
}
