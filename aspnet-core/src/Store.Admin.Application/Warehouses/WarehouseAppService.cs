using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Admin.Permissions;
using Store.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Store.Admin.Warehouses
{
    [Authorize(StorePermissions.Warehouse.Default)]
    public class WarehouseAppService : CrudAppService<
        Warehouse,
        WarehouseDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateWarehouseDto,
        CreateUpdateWarehouseDto>, IWarehouseAppService
    {
        private readonly WarehouseManager _warehouseManager;
        public WarehouseAppService(IRepository<Warehouse, Guid> repository, WarehouseManager warehouseManager) : base(repository)
        {
            _warehouseManager = warehouseManager;

            GetPolicyName = StorePermissions.Warehouse.Default;
            GetListPolicyName = StorePermissions.Warehouse.Default;
            CreatePolicyName = StorePermissions.Warehouse.Create;
            UpdatePolicyName = StorePermissions.Warehouse.Update;
            DeletePolicyName = StorePermissions.Warehouse.Delete;
        }
        [Authorize(StorePermissions.Warehouse.Create)]
        public override async Task<WarehouseDto> CreateAsync(CreateUpdateWarehouseDto input)
        {
            var product = await _warehouseManager.CreateAsync(input.WarehouseId, input.Title, input.Status);
            var result = await Repository.InsertAsync(product);
            return ObjectMapper.Map<Warehouse, WarehouseDto>(result);
        }

        [Authorize(StorePermissions.Warehouse.Update)]
        public override async Task<WarehouseDto> UpdateAsync(Guid id, CreateUpdateWarehouseDto input)
        {
            var wa = await Repository.GetAsync(id);
            if (wa == null)
            {
                throw new BusinessException(StoreDomainErrorCodes.WarehouseIsNotExists);
            }
            wa.WarehouseId = input.WarehouseId;
            wa.Title = input.Title;
            wa.Status = input.Status;
            await Repository.UpdateAsync(wa);
            return ObjectMapper.Map<Warehouse, WarehouseDto>(wa);
        }

        [Authorize(StorePermissions.Warehouse.Delete)]
        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }


        [Authorize(StorePermissions.Warehouse.Default)]
        public async Task<List<WarehouseInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Status == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Warehouse>, List<WarehouseInlistDto>>(data);
        }

        [Authorize(StorePermissions.Warehouse.Default)]
        public async Task<PagedResultDto<WarehouseInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword) || x.WarehouseId.Contains(input.Keyword));
            query = query.Where(x => x.Status == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<WarehouseInlistDto>(totalCount, ObjectMapper.Map<List<Warehouse>, List<WarehouseInlistDto>>(data));
        }
    }
}
