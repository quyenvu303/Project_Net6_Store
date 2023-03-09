using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Categories;
using Store.Products;
using Store.Warehouses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.Public.Products
{
    public class ProductsAppService : ReadOnlyAppService<
        Product,
        ProductDto,
        Guid,
        PagedResultRequestDto>, IProductsAppService
    {
        private readonly IBlobContainer<ProductImageContainer> _fileContainer;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        public ProductsAppService(IRepository<Product, Guid> repository,
            IBlobContainer<ProductImageContainer> fileContainer,
            IRepository<Category, Guid> categoryrepository,
            IRepository<Product, Guid> productRepository
            )
            : base(repository)
        {
            _fileContainer = fileContainer;
            _categoryRepository = categoryrepository;
            _productRepository = productRepository;
        }

        public async Task<List<ProductInlistDto>> GetByIdAsync(string CategoryId)
        {
            var category = await _categoryRepository.GetAsync(x => x.CategoryId == CategoryId);

            var product = await Repository.GetQueryableAsync();
            product = product.Where(x => x.CategoryId == category.Id);
            var data = await AsyncExecuter.ToListAsync(product);
            return ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data);

        }

        public async Task<ProductDto> GetBySlugAsync(string slug)
        {
            var product = await _productRepository.GetAsync(x => x.Slug == slug);
            return ObjectMapper.Map<Product, ProductDto>(product);
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

        public async Task<List<ProductInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data);
        }

        public async Task<PagedResult<ProductInlistDto>> GetListFilterAsync(ProductFilter input)
        {
            var query = await Repository.GetQueryableAsync();

            var category = await _categoryRepository.GetAsync(x => x.CategoryId == input.CateId);
            var product = await Repository.GetQueryableAsync();
            product = product.Where(x => x.CategoryId == category.Id);

            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.ProductName.Contains(input.Keyword) || x.CategoryName.Contains(input.Keyword));
            query = query.Where(x => x.CategoryId == category.Id);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
               .ToListAsync(
                  query.Skip((input.CurrentPage - 1) * input.PageSize)
               .Take(input.PageSize));

            return new PagedResult<ProductInlistDto>(
                ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }

        public async Task<List<ProductInlistDto>> GetListTopSellersAsync(int numberOfRecords)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true && x.BestSellers == true)
                .OrderByDescending(x => x.CreationTime)
                .Take(numberOfRecords);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data);
        }
        public async Task<List<ProductInlistDto>> GetListTrendingAsync(int numberOfRecords)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true && x.Trending == true)
                .OrderByDescending(x => x.CreationTime)
                .Take(numberOfRecords);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data);
        }
        public async Task<List<ProductInlistDto>> GetProductBySlugAsync(string slug,string categorySlug)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.CategorySlug == categorySlug && x.Slug != slug)
                .OrderByDescending(x => x.CreationTime)
                .Take(5);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data);
        }
    }
}
