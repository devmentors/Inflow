namespace Inflow.Shared.Infrastructure.Security.Encryption;

public interface IEncryptor
{
    string Encrypt(string data, string key);
    string Decrypt(string data, string key);
}