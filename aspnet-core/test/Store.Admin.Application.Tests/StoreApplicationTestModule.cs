﻿using Volo.Abp.Modularity;

namespace Store.Admin;

[DependsOn(
    typeof(StoreAdminApplicationModule),
    typeof(StoreDomainTestModule)
    )]
public class StoreApplicationTestModule : AbpModule
{

}
