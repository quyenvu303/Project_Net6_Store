using AutoMapper.Internal.Mappers;
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
        }

        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            var product = await _categoryManager.CreateAsync(input.CategoryId, input.CategoryName, input.SortOrder, input.Description,
                input.ParentId, input.IsActive);
            var result = await Repository.InsertAsync(product);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }
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


        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<List<CategoryInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data);
        }

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
