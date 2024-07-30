﻿using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProductByCategory;
using Marten.Linq.SoftDeletes;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(Guid Id);
    public class UpdateProductRequestHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
                                            : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductRequestHandler.Handle called with @{Query}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(product.Id);
        }
    }
}
