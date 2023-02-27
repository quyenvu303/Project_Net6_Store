using AutoMapper;
using Store.Categories;
using Store.Products;
using Store.Public.Categories;
using Store.Public.Products;

namespace Store.Public;

public class StorePublicApplicationAutoMapperProfile : Profile
{
    public StorePublicApplicationAutoMapperProfile()
    {
        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryInlistDto>();

        // Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInlistDto>();
    }
}
