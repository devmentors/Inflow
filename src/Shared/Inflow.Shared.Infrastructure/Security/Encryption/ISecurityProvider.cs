namespace Inflow.Shared.Infrastructure.Security.Encryption
{
    public interface ISecurityProvider
    {
        string Encrypt(string data);
        string Decrypt(string data);
        string Hash(string data);
        string Rng(int length = 50, bool removeSpecialChars = true);
        string Sanitize(string value);
    }
}