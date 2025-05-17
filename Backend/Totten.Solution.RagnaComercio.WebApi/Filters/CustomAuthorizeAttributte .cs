namespace Totten.Solution.RagnaComercio.WebApi.Filters;

using FunctionalConcepts.Enums;
using FunctionalConcepts.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
/// <summary>
/// 
/// </summary>
public enum RoleLevelEnum
{
    /// <summary>
    /// 
    /// </summary>
    user,
    /// <summary>
    /// 
    /// </summary>
    agent,
    /// <summary>
    /// 
    /// </summary>
    admin,
    /// <summary>
    /// 
    /// </summary>
    system
}

/// <summary>
/// 
/// </summary>
/// <param name="roles"></param>
public class CustomAuthorizeAttributte(params RoleLevelEnum[] roles) : ActionFilterAttribute
{
    private readonly RoleLevelEnum[] Roles = roles;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            string roleLevel = GetClaimValue(context.HttpContext, "Role");
            var roleEnum = Enum.Parse<RoleLevelEnum>(roleLevel);

            if (!Roles.Any(x => x == roleEnum))
            {
                context.HttpContext.Response.StatusCode = EErrorCode.Forbidden.GetHashCode();
                context.Result = new JsonResult(UnauthorizedError.New(""));
                return;
            }

            base.OnActionExecuting(context);
        }
        catch
        {
            context.HttpContext.Response.StatusCode = EErrorCode.Unauthorized.GetHashCode(); ;
            context.Result = new JsonResult(UnauthorizedError.New(""));
        }
    }

    private static string GetClaimValue(HttpContext httpContext, string type)
        => ( httpContext.User?.Identity as ClaimsIdentity )?.FindFirst(type)?.Value ?? "";
}