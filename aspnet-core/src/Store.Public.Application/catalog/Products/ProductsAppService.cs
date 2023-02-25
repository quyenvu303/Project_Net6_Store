using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Categories;
using Store.Products;
using Store.Warehouses;
using System;
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
using Volo.Abp.Uow;
using static System.Net.Mime.MediaTypeNames;

namespace Store.Public.Products
{
    public class ProductsAppService : ReadOnlyAppService<
        Product,
        ProductDto,
        Guid,
        PagedResultRequestDto>, IProductAppService
    {
        private readonly ProductManager _productManager;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IBlobContainer<ProductImageContainer> _fileContainer;
        private readonly ProductCodeGenerator _productCodeGenerator;
        public ProductsAppService(
            IRepository<Product, Guid> repository,
            IRepository<Category> categoryRepository,
            IRepository<Warehouse> warehouseRepository,
            ProductManager productManager,
            IBlobContainer<ProductImageContainer> fileContainer,
            ProductCodeGenerator productCodeGenerator)
            : base(repository)
        {
            _productManager = productManager;
            _categoryRepository = categoryRepository;
            _warehouseRepository = warehouseRepository;
            _fileContainer = fileContainer;
            _productCodeGenerator = productCodeGenerator;
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
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.ProductName.Contains(input.Keyword) || x.CategoryName.Contains(input.Keyword));

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


    }
}
