using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TitanGate.WebSiteStore.Entities;

namespace TitanGate.WebSiteStore.Services.Test
{
    internal class ServiceTestHelper
    {
        public static ServiceProvider InitProvider(Action<ServiceCollection> init)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddWebStoreServices();

            init?.Invoke(services);

            return services.BuildServiceProvider();
        }

        public static IConfiguration LoadTestConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
        }
    }
}
