using System;

namespace Inflow.Shared.Infrastructure.Security.Encryption
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptedAttribute : Attribute
    {
    }
}