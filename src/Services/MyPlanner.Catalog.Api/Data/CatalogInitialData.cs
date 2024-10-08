using Marten.Schema;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        // Marten UPSERT will cater for existing records
        session.Store<Company>(GetPreconfiguredCompanies());
        session.Store<Product>(GetPreconfiguredProducts());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Company> GetPreconfiguredCompanies() => new List<Company>()
            {
                new Company()
                {
                    Id = "0f4be0bd-a612-4569-8d92-00d86b536451",
                    Name = "Company 1",
                },
                new Company()
                {
                    Id = "e90a41ff-d6cf-4fec-807f-2b7ebb35f127",
                    Name = "Company 2",
                }                
            };
    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
            {
                new Product()
                {
                    Id = "5334c996-8457-4cf0-815c-ed2b77c4ff61",
                    CompanyId="0f4be0bd-a612-4569-8d92-00d86b536451",
                    Name = "UMS",
                    Description = "User Management System.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = new List<string> { "Common System" }
                },
                new Product()
                {
                    Id = "c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914",
                    CompanyId="0f4be0bd-a612-4569-8d92-00d86b536451",
                    Name = "MMS",
                    Description = "Master Data Management System.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = new List<string> { "Common System" }
                },
                new Product()
                {
                    Id = "4f136e9f-ff8c-4c1f-9a33-d12f689bdab8",
                    CompanyId="0f4be0bd-a612-4569-8d92-00d86b536451",
                    Name = "TMS",
                    Description = "Transportation Management System",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = new List<string> { "Supply Chain System" }
                },
                new Product()
                {
                    Id = "6ec1297b-ec0a-4aa1-be25-6726e3b51a27",
                    CompanyId="e90a41ff-d6cf-4fec-807f-2b7ebb35f127",
                    Name = "WMS",
                    Description = "Warehouse Management System.",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = new List<string> { "Supply Management System" }
                },
                new Product()
                {
                    Id = "b786103d-c621-4f5a-b498-23452610f88c",
                    CompanyId="e90a41ff-d6cf-4fec-807f-2b7ebb35f127",
                    Name = "FTMS",
                    Description = "Foreign Trade Management System",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = new List<string> { "Supply Management System" }
                }                
            };
}
