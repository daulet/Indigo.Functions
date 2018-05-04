using Microsoft.Azure.WebJobs.Description;
using System;

namespace Indigo.Functions.Redis
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public class RedisAttribute : Attribute
    {
        /// <summary>
        /// Allows Redis config to be defined in app settings
        /// </summary>
        [AppSetting(Default = "RedisConfigurationOptions")]
        public string Configuration { get; set; }

        [AutoResolve]
        public string Key { get; set; }
    }
}
