﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TitanGate.WebSiteStore.DapperRepository;
using TitanGate.WebSiteStore.Services;
using TitanGate.WebSiteStore.Services.Interface;

namespace TitanGate.WebSiteStore.Services
{
    public static class IoC
    {
        public static IServiceCollection AddWebStoreServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IFileAccessService, FileAccessService>()
                .AddTransient<IWebSiteService, WebSiteService>()
                .AddWebStoreDapperRepository();
        }
    }
}