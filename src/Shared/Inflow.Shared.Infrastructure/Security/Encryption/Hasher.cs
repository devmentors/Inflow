using System.Security.Cryptography;
using System.Text;

namespace Inflow.Shared.Infrastructure.Security.Encryption;

internal sealed class Hasher : IHasher
{
    public string Hash(string data)
    {
        using var sha512 = SHA512.Create();
        var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
        var builder = new StringBuilder();
        foreach (var @byte in bytes)
        {
            builder.Append(@byte.ToString("x2"));
        }

        return builder.ToString();
    }
}