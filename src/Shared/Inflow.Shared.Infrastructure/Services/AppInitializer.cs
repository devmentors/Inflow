using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Inflow.Shared.Infrastructure.Services
{
    public class AppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppInitializer> _logger;

        public AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var initializers = scope.ServiceProvider.GetServices<IInitializer>();
            foreach (var initializer in initializers)
            {
                try
                {
                    _logger.LogInformation($"Running the initializer: {initializer.GetType().Name}...");
                    await initializer.InitAsync();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}