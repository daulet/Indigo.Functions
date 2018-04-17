using Unity;

namespace Indigo.Functions.Injection
{
    public interface IDependencyConfig
    {
        UnityContainer Container { get; }
    }
}
