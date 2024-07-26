
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/product/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GerProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
           .WithName("Get Product By Id")
           .WithDescription("Get Product By Id")
           .WithSummary("Get Product By Id")
           .Produces<GetProductByIdResponse>(StatusCodes.Status302Found)
           .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
