using Microsoft.Azure.WebJobs.Description;
using System;

namespace Indigo.Functions.KeyVault
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SecretAttribute : Attribute
    {
        [AppSetting(Default = "KeyVaultClientId")]
        public string ClientId { get; set; }

        [AppSetting(Default = "KeyVaultClientSecret")]
        public string ClientSecret { get; set; }

        public string SecretIdentifier { get; set; }
    }
}
