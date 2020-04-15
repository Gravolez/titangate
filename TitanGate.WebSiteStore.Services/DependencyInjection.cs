using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TitanGate.WebSiteStore.DapperRepository;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebStoreServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IFileAccessService, FileAccessService>()
                .AddTransient<IWebSiteService, WebSiteService>()
                .AddTransient<ICryptoService, CryptoService>()
                .AddWebStoreDapperRepository();
        }
    }
}
