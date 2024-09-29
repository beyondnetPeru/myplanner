using Marten.Pagination;
using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Products.GetProducts;

public record GetProductsQuery(int? PageNumber, int? PageSize): IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}