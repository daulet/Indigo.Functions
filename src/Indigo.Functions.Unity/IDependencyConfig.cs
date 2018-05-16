using Unity;

namespace Indigo.Functions.Unity
{
    public interface IDependencyConfig
    {
        void RegisterComponents(UnityContainer container);
    }
}
