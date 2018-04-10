using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Indigo.Functions.KeyVault
{
    public class KeyVaultExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<SecretAttribute>();

            rule.WhenIsNull(nameof(SecretAttribute.SecretIdentifier))
                .BindToInput(GetKeyVaultClient);

            rule.WhenIsNotNull(nameof(SecretAttribute.SecretIdentifier))
                .BindToInput(GetSecretAsync);
        }

        private static KeyVaultClient.AuthenticationCallback GetAuthenticationCallback(string clientId, string clientSecret)
        {
            return new KeyVaultClient.AuthenticationCallback(async (authority, resource, scope) =>
            {
                var authContext = new AuthenticationContext(authority);
                var clientCredential = new ClientCredential(clientId, clientSecret);
                var result = await authContext
                    .AcquireTokenAsync(resource, clientCredential)
                    .ConfigureAwait(false);

                if (result == null)
                {
                    throw new InvalidOperationException("Failed to obtain the JWT token");
                }
                return result.AccessToken;
            });
        }

        private static KeyVaultClient GetKeyVaultClient(SecretAttribute attribute)
        {
            return new KeyVaultClient(GetAuthenticationCallback(attribute.ClientId, attribute.ClientSecret));
        }

        private static async Task<string> GetSecretAsync(SecretAttribute attribute)
        {
            var client = new KeyVaultClient(GetAuthenticationCallback(attribute.ClientId, attribute.ClientSecret));
            var secret = await client.GetSecretAsync(attribute.SecretIdentifier);
            return secret.Value;
        }
    }
}
