using System.Security.Cryptography;
using System.Text;

namespace Inflow.Shared.Infrastructure.Security.Encryption;

internal sealed class Md5 : IMd5
{
    public string Calculate(string value)
    {
        using var md5Generator = MD5.Create();
        var hash = md5Generator.ComputeHash(Encoding.ASCII.GetBytes(value));
        var stringBuilder = new StringBuilder();
        foreach (var @byte in hash)
        {
            stringBuilder.Append(@byte.ToString("X2"));
        }

        return stringBuilder.ToString().ToLowerInvariant();
    }
}