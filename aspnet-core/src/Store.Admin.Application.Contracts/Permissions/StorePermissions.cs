namespace Store.Admin.Permissions;

public static class StorePermissions
{
    public const string SystemGroupName = "StoreAdminSystem";
    public const string CatalogGroupName = "StoreAdminCatalog";

    public static class Product
    {
        public const string Default = CatalogGroupName + ".Product";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class Category
    {
        public const string Default = CatalogGroupName + ".Category";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class Order
    {
        public const string Default = CatalogGroupName + ".Order";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Warehouse
    {
        public const string Default = CatalogGroupName + ".Warehouse";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
