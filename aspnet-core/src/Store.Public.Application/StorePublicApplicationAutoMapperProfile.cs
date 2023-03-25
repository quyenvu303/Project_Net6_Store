using AutoMapper;
using Store.Banners;
using Store.Blogs;
using Store.Categories;
using Store.Orders;
using Store.Products;
using Store.Public.Banners;
using Store.Public.Blogs;
using Store.Public.Orders;
using Store.Public.Categories;
using Store.Public.Products;
using Store.Shippings;
using Store.Public.Shippings;

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

        // Blog
        CreateMap<Blog, BlogDto>();
        CreateMap<Blog, BlogInlistDto>();
        //Order
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderItemDto>();
        CreateMap<OrderItem, OrderItemDto>();
        // Blog
        CreateMap<Shipping, ShippingDto>();
        CreateMap<Shipping, ShippingInlistDto>();
    }
}
