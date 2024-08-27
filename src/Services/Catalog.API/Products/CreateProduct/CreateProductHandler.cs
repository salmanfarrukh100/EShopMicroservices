﻿using Catalog.API.Models;
using Common.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
     string Name,
     List<string> Category,
     string Description,
     string ImageFile,
     decimal price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.price
            };
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
