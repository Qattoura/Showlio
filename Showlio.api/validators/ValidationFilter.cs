using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Showlio.api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument == null)
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

            var validator = _serviceProvider.GetService(validatorType);

            if (validator == null)
                continue;

            var result = await ValidateAsync(validator, argument);

            if (!result.IsValid)
            {
                context.Result = new BadRequestObjectResult(result.Errors);
                return;
            }
        }

        await next();
    }

    private static async Task<FluentValidation.Results.ValidationResult> ValidateAsync(
        object validator,
        object model)
    {
        var method = validator.GetType()
            .GetMethod("ValidateAsync", new[] { model.GetType(), typeof(CancellationToken) });

        var task = (Task)method!.Invoke(
            validator,
            new[] { model, CancellationToken.None })!;

        await task;

        return (FluentValidation.Results.ValidationResult)
            task.GetType().GetProperty("Result")!.GetValue(task)!;
    }
}