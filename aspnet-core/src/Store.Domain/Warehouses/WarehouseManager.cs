using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Store.Warehouses
{
    public class WarehouseManager : DomainService
    {
        private readonly IRepository<Warehouse, Guid> _WarehouseRepository;

        public WarehouseManager(IRepository<Warehouse, Guid> WarehouseRepository)
        {
            _WarehouseRepository = WarehouseRepository;
        }
        public async Task<Warehouse> CreateAsync(string warehouseId, string title, bool? status)
        {
            if (await _WarehouseRepository.AnyAsync(x => x.WarehouseId == warehouseId))
                throw new UserFriendlyException("Mã kho đã tồn tại", StoreDomainErrorCodes.WarehouseIdAlreadyExists);
            if (await _WarehouseRepository.AnyAsync(x => x.Title == title))
                throw new UserFriendlyException("Tên kho đã tồn tại", StoreDomainErrorCodes.WarehouseNameAlreadyExists);
            
            return new Warehouse(Guid.NewGuid(), warehouseId,title, status);
        }
    }
}
