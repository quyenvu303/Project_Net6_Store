using AutoMapper;
using Store.Banners;
using Store.Blogs;
using Store.Categories;
using Store.Orders;
using Store.Products;
using Store.Public.Banners;
using Store.Public.Blogs;
using Store.Public.Categories;
using Store.Public.Orders;
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

        // Banner
        CreateMap<Banner, BannerDto>();
        CreateMap<Banner, BannerInlistDto>();

        // Banner
        CreateMap<Blog, BlogDto>();
        CreateMap<Blog, BlogInlistDto>();
        //Order
        CreateMap<Order, OrderDto>();
    }
}
