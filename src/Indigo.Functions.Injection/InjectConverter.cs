using Indigo.Functions.Injection.Internal;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace Indigo.Functions.Injection
{
    public class InjectConverter<T> : IConverter<Anonymous, T>
    {
        private readonly ServiceProvider _provider;

        public InjectConverter(ServiceProvider provider)
        {
            _provider = provider;
        }

        public T Convert(Anonymous input)
        {
            return _provider.GetService<T>();
        }
    }
}
