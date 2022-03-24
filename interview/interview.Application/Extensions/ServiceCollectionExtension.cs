using interview.Application.Options;
using interview.Application.Services;
using interview.Application.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interview.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            // TODO: REMOVE
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // TODO: CHANGE SECTION NAME TO WRONG
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions").Bind);
        }
    }
}