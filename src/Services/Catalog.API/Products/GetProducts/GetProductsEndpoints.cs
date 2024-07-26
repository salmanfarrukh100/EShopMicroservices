using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/products", async (ISender sender) => 
            {   
                var result = await sender.Send(new GetProductsQuery());
                
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("Get Products")
            .WithDescription("Get Products")
            .WithSummary("Get Products")
            .Produces<CreateProductResponse>(StatusCodes.Status302Found)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
