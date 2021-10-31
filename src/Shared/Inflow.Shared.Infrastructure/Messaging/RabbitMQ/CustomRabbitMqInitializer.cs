using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;
using Inflow.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Inflow.Shared.Infrastructure.Messaging.RabbitMQ
{
    internal sealed class CustomRabbitMqInitializer : IHostedService
    {
        private const string DefaultType = "topic";
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomRabbitMqInitializer> _logger;

        public CustomRabbitMqInitializer(IServiceProvider serviceProvider, ILogger<CustomRabbitMqInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            InitializeExchanges(scope);
            return Task.CompletedTask;
        }

        private void InitializeExchanges(IServiceScope scope)
        {
            var options = scope.ServiceProvider.GetRequiredService<RabbitMqOptions>();
            if (options.Exchange is null || options.Exchange.Declare is false)
            {
                return;
            }
            
            var exchanges = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(typeof(ExternalMessageAttribute), false))
                .Select(t => t.GetCustomAttribute<ExternalMessageAttribute>()?.Topic ?? string.Empty)
                .Distinct()
                .ToList();
            
            if (!exchanges.Any())
            {
                return;
            }
            
            var connection = scope.ServiceProvider.GetRequiredService<ProducerConnection>();
            using var channel = connection.Connection.CreateModel();
            Log(options.Exchange.Name, options.Exchange.Type);
            channel.ExchangeDeclare(options.Exchange.Name, options.Exchange.Type, options.Exchange.Durable,
                options.Exchange.AutoDelete);

            if (options.DeadLetter?.Enabled is true && options.DeadLetter?.Declare is true)
            {
                channel.ExchangeDeclare($"{options.DeadLetter.Prefix}{options.Exchange.Name}{options.DeadLetter.Suffix}",
                    ExchangeType.Direct, options.Exchange.Durable, options.Exchange.AutoDelete);
            }

            foreach (var exchange in exchanges)
            {
                if (string.IsNullOrWhiteSpace(exchange))
                {
                    continue;
                }
                
                if (exchange.Equals(options.Exchange?.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                Log(exchange, DefaultType);
                channel.ExchangeDeclare(exchange, DefaultType, true);
            }

            channel.Close();
        }

        private void Log(string exchange, string type)
            => _logger.LogInformation($"Declaring an exchange: '{exchange}', type: '{type}'");

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}