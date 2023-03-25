using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Categories;
using Store.Products;
using Store.Public.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Store.Public.Categories
{
    public class CategoriesAppService : ReadOnlyAppService<
         Category,
         CategoryDto,
         Guid,
         PagedResultRequestDto>, ICategoriesAppService
    {
        private readonly IBlobContainer<CategoryIconContainer> _fileContainer;
        private readonly IRepository<Category,Guid> _categoryRepository;
        private readonly IRepository<Product> _produtRepository;
        public CategoriesAppService(IRepository<Category, Guid> repository, CategoryManager categoryManager,
            IBlobContainer<CategoryIconContainer> fileContainer,
            IRepository<Product> produtRepository)
            : base(repository)
        {
            _fileContainer = fileContainer;
            _categoryRepository = repository;
            _produtRepository = produtRepository;
        }
        public async Task<List<CategoryInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data);
        }
        
        public async Task<PagedResult<CategoryInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.CategoryName.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
            .ToListAsync(
               query.Skip((input.CurrentPage - 1) * input.PageSize)
            .Take(input.PageSize));

            return new PagedResult<CategoryInlistDto>(
                ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }
        public async Task<string> GetImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
        }

        public async Task<CategoryDto> GetByCategoryIdAsync(string CategoryId)
        {
            var category = await _categoryRepository.GetAsync(x=>x.CategoryId == CategoryId);
            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<CategoryDto> GetBySlugAsync(string slug)
        {
            var Category = await _categoryRepository.GetAsync(x => x.Slug == slug);
            return ObjectMapper.Map<Category, CategoryDto>(Category);
        }

        public async Task<int> GetProductCountByCategory(Guid? Id)
        {
            int categoryCount = await _categoryRepository.CountAsync<Category>();
            int productCount = await _produtRepository.CountAsync(p => p.CategoryId == Id || p.CategoryParentId == Id);

            return productCount;
        }

        public async Task<List<CategoryInlistDto>> GetListItemByCategoryAsync(ProductFilter input)
        {

            var category = await _categoryRepository.GetAsync(x => x.CategoryId == input.CateId);
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.ParentId == category.Id);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data);
        }

       
    }
}
