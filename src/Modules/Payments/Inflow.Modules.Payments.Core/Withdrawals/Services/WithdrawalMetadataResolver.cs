using System;
using Inflow.Shared.Infrastructure.Serialization;
using Microsoft.Extensions.Logging;

namespace Inflow.Modules.Payments.Core.Withdrawals.Services;

internal sealed class WithdrawalMetadataResolver : IWithdrawalMetadataResolver
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger<WithdrawalMetadataResolver> _logger;

    public WithdrawalMetadataResolver(IJsonSerializer jsonSerializer, ILogger<WithdrawalMetadataResolver> logger)
    {
        _jsonSerializer = jsonSerializer;
        _logger = logger;
    }
        
    public Guid? TryResolveWithdrawalId(string metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata))
        {
            return null;
        }

        try
        {
            return _jsonSerializer.Deserialize<Metadata>(metadata).WithdrawalId;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            _logger.LogError($"Couldn't resolve withdrawal metadata for value{Environment.NewLine}{metadata}");
                
            return null;
        }
    }
        
    private class Metadata
    {
        public Guid WithdrawalId { get; set; }
    }
}