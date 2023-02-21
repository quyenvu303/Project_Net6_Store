using Store.EntityFrameworkCore;
using Store.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Store.Public;

[DependsOn(
    typeof(StoreEntityFrameworkCoreTestModule)
    )]
public class PublicDomainTestModule : AbpModule
{

}
