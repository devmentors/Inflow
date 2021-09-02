using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<OutboxProcessor> _logger;
        private readonly TimeSpan _interval;
        private readonly bool _enabled;
        private readonly TimeSpan _startDelay;
        private int _isProcessing;

        public OutboxProcessor(IServiceScopeFactory serviceScopeFactory, OutboxOptions outboxOptions,
            ILogger<OutboxProcessor> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _enabled = outboxOptions.Enabled;
            _interval = outboxOptions.Interval ?? TimeSpan.FromSeconds(1);
            _startDelay = outboxOptions.StartDelay ?? TimeSpan.FromSeconds(5);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_enabled)
            {
                _logger.LogWarning("Outbox is disabled");
                return;
            }

            _logger.LogInformation($"Outbox is enabled, start delay: {_startDelay}, interval: {_interval}");
            await Task.Delay(_startDelay, stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Interlocked.Exchange(ref _isProcessing, 1) == 1)
                {
                    await Task.Delay(_interval, stoppingToken);
                    continue;
                }
                
                _logger.LogTrace("Started processing outbox messages...");
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var outboxes = scope.ServiceProvider.GetServices<IOutbox>();
                        var tasks = outboxes.Select(outbox => outbox.PublishUnsentAsync());
                        await Task.WhenAll(tasks);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError("There was an error when processing outbox.");
                        _logger.LogError(exception, exception.Message);
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _isProcessing, 0);
                        stopwatch.Stop();
                        _logger.LogTrace($"Finished processing outbox messages in {stopwatch.ElapsedMilliseconds} ms.");
                    }
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}