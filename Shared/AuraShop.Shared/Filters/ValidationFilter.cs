
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AuraShop.Shared.Filters
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService(typeof(IValidator<T>)) as IValidator<T>;

            if (validator == null)
            {
                return await next(context);
            }

            var argumentToValidate = context.Arguments.OfType<T>().FirstOrDefault();

            if (argumentToValidate == null)
            {
                return await next(context);
            }

            var validationResult = await validator.ValidateAsync(argumentToValidate);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
   
}
