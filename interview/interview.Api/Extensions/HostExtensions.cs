using System;
using interview.Application.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace interview.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host, Action<DataContext> seedDb)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<DataContext>();
            seedDb(dbContext);
            return host;
        }
    }
}