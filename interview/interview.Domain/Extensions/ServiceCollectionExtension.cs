using Microsoft.Extensions.DependencyInjection;

using interview.Domain.Services.AuthorService;

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