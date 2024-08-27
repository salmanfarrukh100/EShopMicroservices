﻿
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Products/{Id}",async (Guid Id, ISender sender) => 
            {
                var result = await sender.Send(new DeleteProductCommand(Id));
               
                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);           
            })
            .WithName("Delete Product")
            .WithDescription("Delete Product")
            .WithSummary("Delete Product")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
