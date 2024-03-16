namespace Totten.Solution.Ragstore.WebApi.Handlers;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.Infra.Cross.Errors;

public class ErrorHandlerAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Exception = context.Exception;
        context.HttpContext.Response.StatusCode = 500;
        context.Result = new JsonResult(ErrorPayload.New(context.Exception));
    }
}