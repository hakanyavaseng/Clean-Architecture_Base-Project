using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BaseProject.Domain.Models;
using FluentValidation;

namespace BaseProject.API.Filters
{
    /// <summary>
    /// Validation filter for FluentValidation which is used to validate the request model
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                ErrorResponse errorReponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        ErrorModel errorModel = new ErrorModel
                        {
                            FieldName = error.Key,
                            Message = subError
                        };

                        errorReponse.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorReponse);
                return;
            }

            await next();
        }
    }
}
