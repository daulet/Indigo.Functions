using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.Threading.Tasks;
using Unity;

namespace Indigo.Functions.Injection
{
    class DependencyBinding : IBinding
    {
        private readonly UnityContainer _container;
        private readonly Type _valueType;

        internal DependencyBinding(UnityContainer container, Type valueType)
        {
            _container = container;
            _valueType = valueType;
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            return Task.FromResult<IValueProvider>(new InjectValueProvider(_container, _valueType));
        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            return Task.FromResult<IValueProvider>(new InjectValueProvider(_container, _valueType));
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = _valueType.Name,
                DisplayHints = new ParameterDisplayHints
                {
                    Description = "Dependency Injection",
                    DefaultValue = "Dependency Injection",
                    Prompt = "Dependency Injection"
                }
            };
        }
    }
}
