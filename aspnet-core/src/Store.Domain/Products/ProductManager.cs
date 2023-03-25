using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Store.Categories;
using Store.Warehouses;

namespace Store.Products
{
    public class ProductManager : DomainService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Category, Guid> _CategoryRepository;
        private readonly IRepository<Warehouse, Guid> _WarehouseRepository;
        public ProductManager(IRepository<Product, Guid> productRepository,
             IRepository<Category, Guid> CategoryRepository,
             IRepository<Warehouse, Guid> WarehouseRepository)
        {
            _CategoryRepository = CategoryRepository;
            _WarehouseRepository = WarehouseRepository;
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(
            string productId,
            string productName,
            string slug,
            Guid categoryId,
            Guid warehouseGuid,
            string origin,
            //string image,
            int? quantity, 
            int? totalquantity, 
            decimal? price, 
            decimal? priceSale,
            string parameter, 
            string description, 
            bool? isActive,
            bool? status,
            bool? bestSellers,
            bool? trending)
        {
            if (await _productRepository.AnyAsync(x=>x.ProductId == productId))
                throw new UserFriendlyException("Mã sản phẩm đã tồn tại", StoreDomainErrorCodes.ProductIdAlreadyExists);
            if (await _productRepository.AnyAsync(x => x.ProductName == productName))
                throw new UserFriendlyException("Tên phẩm đã tồn tại", StoreDomainErrorCodes.ProductNameAlreadyExists);


            var _categoryName = await _CategoryRepository.GetAsync(categoryId);
            var _warehouseName = await _WarehouseRepository.GetAsync(warehouseGuid);
            return new Product(
                Guid.NewGuid(), 
                productId, 
                productName, 
                slug, 
                categoryId,
                warehouseGuid,
                origin, 
                null,
                quantity, 
                totalquantity, 
                price, 
                priceSale,
                parameter,
                description, 
                isActive,
                status, 
                bestSellers,
                trending,
                _categoryName?.CategoryName,
                _warehouseName?.Title,
                 _categoryName?.Slug,
                 _categoryName.ParentId);
        }
    }
}
