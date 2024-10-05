namespace MyPlanner.Catalog.Api.Products
{
    public class ENDPOINT
    {
        public const string Tag = "Products";
        public class CREATE
        {
            public const string Name = "CreateProduct";
            public const string Summary = "Create Product";
            public const string Description = "Create a new Product.";
        }

        public class GET
        {
            public const string Name = "GetProduct";
            public const string Summary = "Get Product";
            public const string Description = "Get a Product by id.";
        }

        public class GETCAT
        {
            public const string Name = "GetProduct";
            public const string Summary = "Get Product";
            public const string Description = "Get a Product by Category.";

        }

        public class LIST
        {
            public const string Name = "ListCompanies";
            public const string Summary = "List Companies";
            public const string Description = "List all companies.";
        }

        public class UPDATE
        {
            public const string Name = "UpdateProduct";
            public const string Summary = "Update Product";
            public const string Description = "Update a Product by id.";
        }

        public class DELETE
        {
            public const string Name = "DeleteProduct";
            public const string Summary = "Delete Product";
            public const string Description = "Delete a Product by id.";
        }
    }
}
