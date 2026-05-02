using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace UdemyNewMicroservice.Shared.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        IValidator<T>? validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        //Fast Fail
        if (validator is null)
        {
            return await next(context);
        }

        T? requestModel = context.Arguments.OfType<T>().FirstOrDefault();

        if (requestModel is null)
        {
            return await next(context);
        }

        var validationResult = await validator.ValidateAsync(requestModel);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context);

    }
}
