using Autofac;
using Indigo.Functions.Autofac.Internal;
using Microsoft.Azure.WebJobs;

namespace Indigo.Functions.Autofac
{
    public class InjectConverter<T> : IConverter<Anonymous, T>
    {
        private readonly JobHostConfiguration _configuration;
        private readonly IContainer _container;

        public InjectConverter(JobHostConfiguration configuration, IContainer container)
        {
            _configuration = configuration;
            _container = container;
        }

        public T Convert(Anonymous input)
        {
            return _container.Resolve<T>();
        }
    }
}
