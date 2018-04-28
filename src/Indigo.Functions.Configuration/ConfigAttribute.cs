using Microsoft.Azure.WebJobs.Description;
using System;

namespace Indigo.Functions.Configuration
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public class ConfigAttribute : Attribute
    {
        public ConfigAttribute()
        { }

        public ConfigAttribute(string settingName)
        {
            SettingName = settingName;
        }

        /// <summary>
        /// Name of the setting specified in App settings
        /// </summary>
        public string SettingName { get; set; }
    }
}
