using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Threading.Tasks;
using Unity;

namespace Indigo.Functions.Injection
{
    class InjectValueProvider : IValueProvider
    {
        private readonly UnityContainer _container;
        private readonly Type _valueType;

        internal InjectValueProvider(UnityContainer container, Type valueType)
        {
            _container = container;
            _valueType = valueType;
        }

        public Type Type => _valueType;

        public Task<object> GetValueAsync()
        {
            var value = _container.Resolve(_valueType);
            return Task.FromResult(value);
        }

        public string ToInvokeString()
        {
            return null;
        }
    }
}
