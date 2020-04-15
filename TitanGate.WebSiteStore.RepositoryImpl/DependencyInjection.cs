using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebStoreDapperRepository(this IServiceCollection services)
        {
            return services
                .AddTransient<IWebSiteRepository, WebSiteRepository>()
                .AddScoped<IRepositorySession, RepositorySession>();
        }
    }
}
