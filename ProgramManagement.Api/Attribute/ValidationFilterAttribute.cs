using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ProgramManagement.Core.Helpers;

namespace ProgramManagement.Api.Attribute;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var error = new UnprocessableEntityObjectResult(context.ModelState);
            var response = new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.ModelValidationFailed, error.Value);

            context.Result = new JsonResult(response)
            {
                StatusCode = 400
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}