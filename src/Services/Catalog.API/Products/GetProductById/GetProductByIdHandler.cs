
using Catalog.API.Models;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    public record GerProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
                                           :IQueryHandler<GerProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GerProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler.Handle called with @{Query}", query);

            var product = await session.LoadAsync<Product>(query.Id,cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(product);
        }
    }
}
