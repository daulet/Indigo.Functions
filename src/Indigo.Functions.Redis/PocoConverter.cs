using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace Indigo.Functions.Redis
{
    public class PocoConverter<T> : IConverter<string, T>
        where T : class
    {
        public T Convert(string input)
        {
            if (input == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
