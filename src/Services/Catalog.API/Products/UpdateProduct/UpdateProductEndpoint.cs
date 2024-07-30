
using Catalog.API.Products.CreateProduct;
using MediatR;

namespace Catalog.API.Products.UpdateProduct
{ 
     public record UpdateProductRequest(
     Guid Id,
     string Name,
     List<string> Category,
     string Description,
     string ImageFile,
     decimal Price);

    public record UpdateProductResponse(Guid Id);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Products", async (UpdateProductRequest product, ISender sender)=>{

                var command = product.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductRequest>();
                return Results.Ok(response.Id);

            })
            .WithName("Update Product")
            .WithDescription("Update Product")
            .WithSummary("Update Product")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
