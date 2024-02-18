using Azure.Identity;

using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class KeyVaultRepository : IKeyVaultRepository
{
    private readonly SecretClient _secretClient;

public KeyVaultRepository(SecretClient secretClient)
{
    _secretClient = secretClient;
}

    public string GetSecret(string secretName){
        // // Retrieve a secret using the secret client.
        return _secretClient.GetSecret(secretName).Value.Value;
    }
}