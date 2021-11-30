using System;
using MessagePack;

namespace Inflow.Shared.Infrastructure.Modules;

internal class MessagePackModuleSerializer : IModuleSerializer
{
    private readonly MessagePackSerializerOptions _options =
        MessagePack.Resolvers.ContractlessStandardResolverAllowPrivate.Options;

    public byte[] Serialize<T>(T value) => MessagePackSerializer.Serialize(value, _options);

    public T Deserialize<T>(byte[] value) => MessagePackSerializer.Deserialize<T>(value, _options);

    public object Deserialize(byte[] value, Type type) => MessagePackSerializer.Deserialize(type, value, _options);
}