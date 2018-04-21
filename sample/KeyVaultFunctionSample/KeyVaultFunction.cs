using Indigo.Functions.KeyVault;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.IO;
using System.Threading.Tasks;

namespace KeyVaultFunctionSample
{
    public static class KeyVaultFunction
    {
        [FunctionName("KeyVault_SetSecret")]
        public static async Task<IActionResult> SetSecretAsync(
            [HttpTrigger("POST", Route = "secret/{secretName}")] HttpRequest req,
            string secretName,
            [Secret] KeyVaultClient vaultClient,
            TraceWriter log)
        {
            string value = null;
            using (var reader = new StreamReader(req.Body))
            {
                value = reader.ReadToEnd();
            }
            if (string.IsNullOrEmpty(value))
            {
                return new BadRequestObjectResult("Pass secret value in request body");
            }

            var secretBundle = await vaultClient.SetSecretAsync(null, secretName, value);

            return new OkObjectResult($"Created secret {secretBundle.SecretIdentifier}, value: {secretBundle.Value}");
        }
    }
}
