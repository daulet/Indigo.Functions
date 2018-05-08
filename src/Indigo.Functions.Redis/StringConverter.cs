using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace Indigo.Functions.Redis
{
    public class StringConverter<T> : IConverter<T, string>
    {
        public string Convert(T input)
        {
            if (input == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(input);
        }
    }
}
