using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Inflow.Shared.Infrastructure.Cache
{
    public static class Extensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            var options = services.GetOptions<RedisOptions>("redis");
            services.AddStackExchangeRedisCache(o => o.Configuration = options.ConnectionString);
            services.AddScoped<ICache, RedisCache>();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options.ConnectionString));
            services.AddScoped(ctx => ctx.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
            
            return services;
        }
    }
}