using Store.Public.Banners;
using Store.Shippings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.OpenIddict;

namespace Store.Public.Shippings
{
    public class ShippingAppService : ReadOnlyAppService<
         Shipping,
         ShippingDto,
         Guid,
         PagedResultRequestDto>, IShippingAppService
    {
        private readonly IRepository<Shipping, Guid> _shippingRepository;
        public ShippingAppService(IReadOnlyRepository<Shipping, Guid> repository, IRepository<Shipping, Guid> shippingRepository) : base(repository)
        {
            _shippingRepository = shippingRepository;
        }

        public async Task<List<ShippingInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Status == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Shipping>, List<ShippingInlistDto>>(data);
        }

        public async Task<ShippingInlistDto> GetShipingFreeAsync()
        {
            var data = await _shippingRepository.GetAsync(x => x.Status == true);
            return ObjectMapper.Map<Shipping, ShippingInlistDto>(data);
        }
    }
}
