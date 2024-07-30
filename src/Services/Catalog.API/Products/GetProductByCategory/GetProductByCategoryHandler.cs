
using JasperFx.Core;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryReseult>;
    public record GetProductByCategoryReseult(IEnumerable<Product> Products);
    public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
                                           : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryReseult>
    {
        public async Task<GetProductByCategoryReseult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with @{Query}", query);

            var products = await session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync();
            if (products is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByCategoryReseult(products);
        }
    }
}
