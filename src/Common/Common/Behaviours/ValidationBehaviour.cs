﻿using Common.CQRS;
using FluentValidation;
using MediatR;

namespace Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
                                    :IPipelineBehavior<TRequest, TResponse>
                                     where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, 
                                      RequestHandlerDelegate<TResponse> next, 
                                      CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context,cancellationToken)));

            var failures = validationResults
                          .Where(x => x.Errors.Any())
                          .SelectMany(y => y.Errors)
                          .ToList();

            if(failures.Any())
            {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
