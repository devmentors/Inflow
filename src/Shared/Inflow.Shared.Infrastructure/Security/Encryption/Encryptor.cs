using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Inflow.Shared.Infrastructure.Security.Encryption
{
    public sealed class Encryptor : IEncryptor
    {
        public string Encrypt(string data, string key)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Encryption key cannot be empty.", nameof(key));
            }

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            var iv = Convert.ToBase64String(aes.IV);
            var transform = aes.CreateEncryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(data);
            }

            return iv + Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Decrypt(string data, string key)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Encryption key cannot be empty.", nameof(key));
            }

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Convert.FromBase64String(data.Substring(0, 24));
            var transform = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(Convert.FromBase64String(data.Substring(24)));
            using var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }
    }
}     