using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;



namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            string secretName = "MySecret";
            string KeyVaultName = "my-vault-example-test";
            var kvUri = "https://my-vault-example-test.vault.azure.net";
            SecretClientOptions options = new SecretClientOptions(){
                Retry = {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(),options);
            KeyVaultSecret secret = client.GetSecret(secretName);
            Console.WriteLine("GetSecret: " + secret.Value);
            Console.WriteLine("Enter Secret > ");
            String secretValue = Console.ReadLine();
            client.SetSecret(secretName, secretValue);
            Console.WriteLine("SetSecret:");
            Console.WriteLine("SetSecret:");
            Console.WriteLine(" Key: " + secretName);
            Console.WriteLine(" Value:" + secretValue);

            Console.WriteLine("GetSecret: " + secret.Value);

            client.StartDeleteSecret(secretName);
            Console.WriteLine("StartDeleteSecret" + KeyVaultName);

            Console.WriteLine("GetSecret: " + secret.Value);

        } 
    }
}
