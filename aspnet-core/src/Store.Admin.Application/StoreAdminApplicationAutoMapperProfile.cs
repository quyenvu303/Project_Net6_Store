using AutoMapper;
using Store.Admin.Blogs;
using Store.Admin.Categories;
using Store.Admin.Contacts;
using Store.Admin.Orders;
using Store.Admin.Products;
using Store.Admin.Shippings;
using Store.Admin.Warehouses;
using Store.Admin.Roles;
using Store.Admin.System.Users;
using Store.Blogs;
using Store.Categories;
using Store.Contacts;
using Store.Orders;
using Store.Products;
using Store.Roles;
using Store.Shippings;
using Store.Warehouses;
using Volo.Abp.Identity;

namespace Store.Admin;

public class StoreAdminApplicationAutoMapperProfile : Profile
{
    public StoreAdminApplicationAutoMapperProfile()
    {
        //Category
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryInlistDto>();
        CreateMap<CreateUpdateCategoryDto, Category>();

        ////Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInlistDto>();
        CreateMap<CreateUpdateProductDto, Product>();

        //Shipping
        CreateMap<Shipping, ShippingDto>();
        CreateMap<Shipping, ShippingInlistDto>();
        CreateMap<CreateUpdateShippingDto, Shipping>();

        //Contact
        CreateMap<Contact, ContactDto>();
        CreateMap<Contact, ContactInlistDto>();
        CreateMap<CreateUpdateContactDto, Contact>();

        //
        CreateMap<Blog, BlogDto>();
        CreateMap<Blog, BlogInlistDto>();
        CreateMap<CreateUpdateBlogDto, Blog>();

        //Warehouses
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<Warehouse, WarehouseInlistDto>();
        CreateMap<CreateUpdateWarehouseDto, Warehouse>();

        //Order
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderInlistDto>();
        CreateMap<CreateUpdateOrderDto, Order>();

        //Contact
        

        //Roles
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<IdentityRole, RoleInListDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();

        //User
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityUser, UserInListDto>();
    }
}
