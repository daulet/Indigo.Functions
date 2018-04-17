using Microsoft.Azure.WebJobs.Host.Bindings;
using System.Threading.Tasks;
using Unity;

namespace Indigo.Functions.Injection
{
    class DependencyBindingProvider : IBindingProvider
    {
        private readonly UnityContainer _container;

        internal DependencyBindingProvider(UnityContainer container)
        {
            _container = container;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            var valueType = context.Parameter.ParameterType;
            return Task.FromResult<IBinding>(new DependencyBinding(_container, valueType));
        }
    }
}
