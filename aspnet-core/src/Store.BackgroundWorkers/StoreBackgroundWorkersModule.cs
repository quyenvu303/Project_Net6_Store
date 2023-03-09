
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Store.EntityFrameworkCore;
using Volo.Abp.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Volo.Abp;
using Store.BackgroundWorkers.MailCampaigns;
using Microsoft.AspNetCore.DataProtection;

namespace Store.BackgroundWorkers
{
    [DependsOn(
      typeof(AbpAutofacModule),
      typeof(AbpBackgroundWorkersModule),
      typeof(StoreEntityFrameworkCoreModule),
      typeof(AbpCachingStackExchangeRedisModule)
  )]
    public class StoreBackgroundWorkersModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<StoreBackgroundWorkersHostedService>();

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "Store:";
            });
            var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Store");
            if (!hostEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Store-Protection-Keys");
            }

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.AddBackgroundWorkerAsync<EmailMarketingWorker>();
        }
    }
}
