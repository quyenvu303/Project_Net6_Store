﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Store.Admin;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Store.Admin.HttpApi.Client.ConsoleTestApp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(StoreAdminHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class StoreConsoleApiClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
            {
                clientBuilder.AddTransientHttpErrorPolicy(
                    policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                );
            });
        });
    }
}
