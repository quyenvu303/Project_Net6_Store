using AutoMapper;
using Store.Categories;
using Store.Public.Categories;

namespace Store.Public;

public class StorePublicApplicationAutoMapperProfile : Profile
{
    public StorePublicApplicationAutoMapperProfile()
    {
        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryInlistDto>();
    }
}
