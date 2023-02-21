using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Admin.Permissions;
using Store.Categories;
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

namespace Store.Admin.Categories
{
    [Authorize(StorePermissions.Category.Default)]
    public class CategoriesAppService : CrudAppService<
         Category,
         CategoryDto,
         Guid,
         PagedResultRequestDto,
         CreateUpdateCategoryDto,
         CreateUpdateCategoryDto>, ICategoriesAppService
    {
        private readonly CategoryManager _categoryManager;
        private readonly IRepository<Category> _categoryRepository;
        public CategoriesAppService(IRepository<Category, Guid> repository, CategoryManager categoryManager) 
            : base(repository)
        {
            _categoryManager = categoryManager;
            _categoryRepository = repository;

            GetPolicyName = StorePermissions.Category.Default;
            GetListPolicyName = StorePermissions.Category.Default;
            CreatePolicyName = StorePermissions.Category.Create;
            UpdatePolicyName = StorePermissions.Category.Update;
            DeletePolicyName = StorePermissions.Category.Delete;
        }

        [Authorize(StorePermissions.Category.Create)]
        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            var product = await _categoryManager.CreateAsync(input.CategoryId, input.CategoryName, input.SortOrder, input.Description,
                input.ParentId, input.IsActive);
            var result = await Repository.InsertAsync(product);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }

        [Authorize(StorePermissions.Category.Update)]
        public override async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            var ca = await Repository.GetAsync(id);
            if (ca == null)
            {
                throw new BusinessException(StoreDomainErrorCodes.CategoryIsNotExists);
            }
            ca.CategoryId = input.CategoryId;
            ca.CategoryName = input.CategoryName;
            ca.SortOrder = input.SortOrder;
            ca.Description = input.Description;
            ca.ParentId = input.ParentId;
            ca.IsActive = input.IsActive;
            await Repository.UpdateAsync(ca);
            return ObjectMapper.Map<Category, CategoryDto>(ca);
        }

        [Authorize(StorePermissions.Category.Delete)]
        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
       
        [Authorize(StorePermissions.Category.Default)]
        public async Task<List<CategoryInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data);
        }
       
        [Authorize(StorePermissions.Category.Default)]
        public async Task<PagedResultDto<CategoryInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.CategoryName.Contains(input.Keyword) || x.CategoryId.Contains(input.Keyword) );
            query = query.Where(x => x.IsActive == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<CategoryInlistDto>(totalCount, ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data));
        }


    }
}
