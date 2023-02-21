using Store.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Store.Admin.Permissions;

public class StorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)   
    {

        //Catalog
        var catalogGroup = context.AddGroup(StorePermissions.CatalogGroupName, L("Permission:Catalog"));
        //Add Category
        var categoryPermission = catalogGroup.AddPermission(StorePermissions.Category.Default, L("Permission:Catalog.Category"));
        categoryPermission.AddChild(StorePermissions.Category.Create, L("Permission:Catalog.Category.Create"));
        categoryPermission.AddChild(StorePermissions.Category.Update, L("Permission:Catalog.Category.Update"));
        categoryPermission.AddChild(StorePermissions.Category.Delete, L("Permission:Catalog.Category.Delete"));
        //Add product
        var productPermission = catalogGroup.AddPermission(StorePermissions.Product.Default, L("Permission:Catalog.Product"));
        productPermission.AddChild(StorePermissions.Product.Create, L("Permission:Catalog.Product.Create"));
        productPermission.AddChild(StorePermissions.Product.Update, L("Permission:Catalog.Product.Update"));
        productPermission.AddChild(StorePermissions.Product.Delete, L("Permission:Catalog.Product.Delete"));
        //Add Order
        var orderPermission = catalogGroup.AddPermission(StorePermissions.Order.Default, L("Permission:Catalog.Order"));
        orderPermission.AddChild(StorePermissions.Order.Create, L("Permission:Catalog.Order.Create"));
        orderPermission.AddChild(StorePermissions.Order.Update, L("Permission:Catalog.Order.Update"));
        orderPermission.AddChild(StorePermissions.Order.Delete, L("Permission:Catalog.Order.Delete"));
        //Add Warehouse
        var warehousePermission = catalogGroup.AddPermission(StorePermissions.Warehouse.Default, L("Permission:Catalog.Warehouse"));
        warehousePermission.AddChild(StorePermissions.Warehouse.Create, L("Permission:Catalog.Warehouse.Create"));
        warehousePermission.AddChild(StorePermissions.Warehouse.Update, L("Permission:Catalog.Warehouse.Update"));
        warehousePermission.AddChild(StorePermissions.Warehouse.Delete, L("Permission:Catalog.Warehouse.Delete"));
        //Add Blog
        var blogPermission = catalogGroup.AddPermission(StorePermissions.Blog.Default, L("Permission:Catalog.Blog"));
        blogPermission.AddChild(StorePermissions.Blog.Create, L("Permission:Catalog.Blog.Create"));
        blogPermission.AddChild(StorePermissions.Blog.Update, L("Permission:Catalog.Blog.Update"));
        blogPermission.AddChild(StorePermissions.Blog.Delete, L("Permission:Catalog.Blog.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StoreResource>(name);
    }
}
