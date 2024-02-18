namespace DataAccessLayer;

public interface IKeyVaultRepository
{
 string GetSecret(string secretName); 
}
