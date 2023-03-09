using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Store.Banners
{
    public class BannerManager : DomainService
    {
        private readonly IRepository<Banner, Guid> _BannerRepository;
        public BannerManager(IRepository<Banner, Guid> bannerRepository)
        {
            _BannerRepository = bannerRepository;
        }
        public async Task<Banner> CreateAsync(string title, bool? status)
        {
            return new Banner( Guid.NewGuid(), title,null, status);
        }
    }
}
