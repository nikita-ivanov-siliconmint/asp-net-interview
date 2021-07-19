using Microsoft.Extensions.DependencyInjection;

using interview.Domain.Services.Interfaces;
using interview.Domain.Services;

namespace interview.Domain.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection collection)
        {
            collection.AddScoped<IAuthorService, AuthorService>();
        }
    }
}