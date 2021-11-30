using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox;

internal sealed class EfInbox<T> : IInbox where T : DbContext
{
    private readonly T _dbContext;
    private readonly DbSet<InboxMessage> _set;
    private readonly IClock _clock;
    private readonly ILogger<EfInbox<T>> _logger;

    public bool Enabled { get; }

    public EfInbox(T dbContext, IClock clock, OutboxOptions outboxOptions, ILogger<EfInbox<T>> logger)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<InboxMessage>();
        _clock = clock;
        _logger = logger;
        Enabled = outboxOptions.Enabled;
    }

    public async Task HandleAsync(Guid messageId, string name, Func<Task> handler)
    {
        var module = _dbContext.GetModuleName();
        if (!Enabled)
        {
            _logger.LogWarning($"Outbox is disabled ('{module}'), incoming messages won't be processed.");
            return;
        }

        _logger.LogTrace($"Received a message with ID: '{messageId}' to be processed ('{module}').");
        if (await _set.AnyAsync(m => m.Id == messageId && m.ProcessedAt != null))
        {
            _logger.LogTrace($"Message with ID: '{messageId}' was already processed ('{module}').");
            return;
        }

        _logger.LogTrace($"Processing a message with ID: '{messageId}' ('{module}')...");

        var inboxMessage = new InboxMessage
        {
            Id = messageId,
            Name = name,
            ReceivedAt = _clock.CurrentDate()
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await handler();
            inboxMessage.ProcessedAt = _clock.CurrentDate();
            await _set.AddAsync(inboxMessage);
            await _dbContext.SaveChangesAsync();

            if (transaction is not null)
            {
                await transaction.CommitAsync();
            }

            _logger.LogTrace($"Processed a message with ID: '{messageId}' ('{module}').");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"There was an error when processing a message with ID: '{messageId}' ('{module}').");
            if (transaction is not null)
            {
                await transaction.RollbackAsync();
            }

            throw;
        }
        finally
        {
            {
                await transaction.DisposeAsync();
            }
        }
    }

    public async Task CleanupAsync(DateTime? to = null)
    {
        var module = _dbContext.GetModuleName();
        if (!Enabled)
        {
            _logger.LogWarning($"Outbox is disabled ('{module}'), incoming messages won't be cleaned up.");
            return;
        }

        var dateTo = to ?? _clock.CurrentDate();
        var sentMessages = await _set.Where(x => x.ReceivedAt <= dateTo).ToListAsync();
        if (!sentMessages.Any())
        {
            _logger.LogTrace($"No received messages found in inbox ('{module}') till: {dateTo}.");
            return;
        }

        _logger.LogInformation($"Found {sentMessages.Count} received messages in inbox ('{module}') till: {dateTo}, cleaning up...");
        _set.RemoveRange(sentMessages);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Removed {sentMessages.Count} received messages from inbox ('{module}') till: {dateTo}.");
    }
}