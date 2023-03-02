using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Admin.Permissions;
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

namespace Store.Admin.Products
{
    [Authorize(StorePermissions.Product.Default)]
    public class ProductsAppService : CrudAppService<
        Product,
        ProductDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>, IProductAppService
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

            GetPolicyName = StorePermissions.Product.Default;
            GetListPolicyName = StorePermissions.Product.Default;
            CreatePolicyName = StorePermissions.Product.Create;
            UpdatePolicyName = StorePermissions.Product.Update;
            DeletePolicyName = StorePermissions.Product.Delete;
        }

        [Authorize(StorePermissions.Product.Create)]
        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = await _productManager.CreateAsync(
                input.ProductId,
                input.ProductName,
                input.Slug,
                input.CategoryId,
                input.WarehouseGuid,
                input.Origin,
                //input.Image, 
                input.Quantity,
                input.Price,
                input.PriceSale,
                input.Parameter,
                input.Description,
                input.IsActive,
                input.Status);
            if (input.ImageContent != null && input.ImageContent.Length > 0)
            {
                await SaveImageAsync(input.ImageName, input.ImageContent);
                product.Image = input.ImageName;
            }
            var result = await Repository.InsertAsync(product);
            return ObjectMapper.Map<Product, ProductDto>(result);
        }

        [Authorize(StorePermissions.Product.Update)]
        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var pro = await Repository.GetAsync(id);
            if (pro == null)
            {
                throw new BusinessException(StoreDomainErrorCodes.ProductIsNotExists);
            }
            pro.ProductId = input.ProductId;
            pro.ProductName = input.ProductName;
            pro.Slug = input.Slug;
            if (pro.CategoryId != input.CategoryId)
            {
                pro.CategoryId = input.CategoryId;
                var category = await _categoryRepository.GetAsync(x => x.Id == input.CategoryId);
                pro.CategoryName = category.CategoryName;
                pro.CategorySlug = category.Slug;
            }
            if (pro.WarehouseGuid != input.WarehouseGuid)
            {
                pro.WarehouseGuid = input.WarehouseGuid;
                var Wa = await _warehouseRepository.GetAsync(x => x.Id == input.WarehouseGuid);
                pro.WarehouseName = Wa.Title;
            }
            pro.Origin = input.Origin;
            if (input.ImageContent != null && input.ImageContent.Length > 0)
            {
                await SaveImageAsync(input.ImageName, input.ImageContent);
                pro.Image = input.ImageName;
            }
            pro.Quantity = input.Quantity;
            pro.Price = input.Price;
            pro.PriceSale = input.PriceSale;
            pro.Parameter = input.Parameter;
            pro.Description = input.Description;
            pro.IsActive = input.IsActive;
            pro.Status = input.Status;

            await Repository.UpdateAsync(pro);

            return ObjectMapper.Map<Product, ProductDto>(pro);
        }

        [Authorize(StorePermissions.Product.Default)]
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

        [Authorize(StorePermissions.Product.Update)]
        private async Task SaveImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _fileContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(StorePermissions.Product.Delete)]
        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(StorePermissions.Product.Default)]
        public async Task<List<ProductInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data);
        }

        [Authorize(StorePermissions.Product.Default)]
        public async Task<PagedResultDto<ProductInlistDto>> GetListFilterAsync(ProductFilter input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                x => x.ProductName.Contains(input.Keyword)
                || x.ProductId.Contains(input.Keyword)
                || x.CategoryName.Contains(input.Keyword)
                || x.WarehouseName.Contains(input.Keyword)
                || x.Origin.Contains(input.Keyword));
            query = query.WhereIf(input.CategoryId.HasValue, x => x.CategoryId == input.CategoryId);
            query = query.WhereIf(input.WarehouseGuid.HasValue, x => x.WarehouseGuid == input.WarehouseGuid);
            query = query.Where(x => x.Status == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductInlistDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductInlistDto>>(data));
        }


        public async Task<string> GetSuggestNewCodeAsync()
        {
            return await _productCodeGenerator.GenerateAsync();
        }

    }
}
